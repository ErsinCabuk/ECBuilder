using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ECBuilderGenerator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private Dictionary<string, Type> DataTypeControls { get; set; } = new Dictionary<string, Type>()
        {
            { "int", typeof(NumericUpDown) },
            { "nvarchar", typeof(TextBox) },
            { "datetime", typeof(DateTimePicker) },
            { "date", typeof(DateTimePicker) },
        };


        List<Type> Tools { get; set; } = new List<Type>()
        {
            typeof(TextBox),
            typeof(NumericUpDown),
            typeof(ComboBox),
            typeof(DateTimePicker)
        };

        private SqlConnection SqlConnection { get; set; }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dataGridViewControls.Columns["ColumnControlType"];
            comboBox.DataSource = Tools;
            comboBox.DisplayMember = "Name";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (SqlConnection != null && SqlConnection.State == ConnectionState.Open)
            {
                textBoxFormName.Text = $"Form{comboBoxTables.SelectedItem}";

                string[] restrictionsColumns = new string[4];
                restrictionsColumns[2] = comboBoxTables.SelectedItem.ToString();
                DataTable schemaColumns = SqlConnection.GetSchema("Columns", restrictionsColumns);

                foreach (DataRow rowColumn in schemaColumns.Rows)
                {
                    string columnName = rowColumn["COLUMN_NAME"].ToString();
                    string dataType = rowColumn["DATA_TYPE"].ToString();

                    if(DataTypeControls.TryGetValue(dataType, out Type control))
                    {
                        dataGridViewControls.Rows.Add(new object[] { columnName, control.Name });
                    }
                    else
                    {
                        dataGridViewControls.Rows.Add(new object[] { columnName, "TextBox" });
                    }
                }
            }
            else
            {
                try
                {
                    SqlConnection = new SqlConnection($"Server={textBoxServerName.Text};Database={textBoxDatabase.Text};User Id={textBoxUsername.Text};Password={textBoxPassword.Text};");
                    SqlConnection.Open();

                    DataTable dataTable = SqlConnection.GetSchema("Tables");
                    foreach (DataRow row in dataTable.Rows)
                    {
                        comboBoxTables.Items.Add(row.ItemArray[2].ToString());
                    }

                    textBoxServerName.Enabled = false;
                    textBoxUsername.Enabled = false;
                    textBoxPassword.Enabled = false;
                    textBoxDatabase.Enabled = false;
                    comboBoxTables.Enabled = true;

                    buttonConnect.Text = "Set";
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        int labelBiggerWidth = 0;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            DesignSurface designSurface = new DesignSurface(typeof(Form));
            IDesignerHost host = (IDesignerHost) designSurface.GetService(typeof(IDesignerHost));

            Form root = (Form) host.RootComponent;
            TypeDescriptor.GetProperties(root)["Name"].SetValue(root, textBoxFormName.Text);
            root.Text = textBoxFormName.Text;
            root.AutoScaleDimensions = new SizeF(9F, 21F);
            root.AutoScaleMode = AutoScaleMode.Font;
            root.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            root.Padding = new Padding(10);
            root.StartPosition = FormStartPosition.CenterScreen;

            Control oldControl = new Control();
            foreach (DataGridViewRow row in dataGridViewControls.Rows)
            {
                if (row.Cells["ColumnControlName"].Value == null) continue;

                DataGridViewComboBoxCell comboBox = (DataGridViewComboBoxCell)row.Cells["ColumnControlType"];
                Type type = comboBox.Items.Cast<Type>().First(item => item.Name == comboBox.Value.ToString());

                Control control = (Control)host.CreateComponent(type, row.Cells["ColumnControlName"].Value.ToString());

                string labelText = ConvertToSeparatedWords(control.Name);

                Label labelTitle = (Label)host.CreateComponent(typeof(Label), $"labelTitle{control.Name}");

                labelTitle.Text = labelText;
                labelTitle.Margin = new Padding(0, 0, 10, 0);
                labelTitle.AutoSize = true;
                labelTitle.TabIndex = 0;

                control.Margin = new Padding(0, 0, 0, 10);
                control.Location = new Point(0, oldControl.Size.Height + 10 + oldControl.Location.Y);
                control.Width = 300;
                control.Tag = "use";

                labelTitle.Location = new Point(10, control.Location.Y + (labelTitle.Height - control.Height));

                root.Controls.Add(labelTitle);
                root.Controls.Add(control);

                if(labelTitle.Width > labelBiggerWidth)
                {
                    labelBiggerWidth = labelTitle.Width;
                }

                oldControl = control;
            }


            foreach (Control control in root.Controls)
            {
                if (control.Tag != null)
                {
                    control.Location = new Point(labelBiggerWidth + 10 + 10, control.Location.Y);
                }
            }

            var view = (Control)designSurface.View;
            view.Dock = DockStyle.Fill;
            view.BackColor = Color.White;
            panel1.Controls.Add(view);

            string code = GenerateCSFromDesigner(designSurface);

            using (StreamWriter sw = new StreamWriter($@"C:\Users\SuffaTech_11003\Desktop\{textBoxFormName.Text}.txt"))
            {
                sw.WriteLine(code);
                sw.Close();
            }
        }

        string ConvertToSeparatedWords(string input)
        {
            string result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (char.IsUpper(currentChar) && i > 0)
                {
                    result += " ";
                }
                result += currentChar;
            }
            return result;
        }

        string GenerateCSFromDesigner(DesignSurface designSurface)
        {
            CodeTypeDeclaration type;
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = host.RootComponent;
            var manager = new DesignerSerializationManager(host);
            using (manager.CreateSession())
            {
                var serializer = (TypeCodeDomSerializer)manager.GetSerializer(root.GetType(),
                    typeof(TypeCodeDomSerializer));
                type = serializer.Serialize(manager, root, host.Container.Components);
                type.IsPartial = true;
                type.Members.OfType<CodeConstructor>()
                    .FirstOrDefault().Attributes = MemberAttributes.Public;
            }
            var builder = new StringBuilder();
            CodeGeneratorOptions option = new CodeGeneratorOptions();
            option.BracingStyle = "C";
            option.BlankLinesBetweenMembers = false;
            using (var writer = new StringWriter(builder, CultureInfo.InvariantCulture))
            {
                using (var codeDomProvider = new CSharpCodeProvider())
                {
                    codeDomProvider.GenerateCodeFromType(type, writer, option);
                }
                return builder.ToString();
            }
        }
    }
}

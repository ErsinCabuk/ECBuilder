using ECBuilder.Builders.FormBuilders;
using ECBuilder.Components.Buttons;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECBuilderGenerator
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region Parameters
        private SqlConnection SqlConnection { get; set; }

        private Dictionary<string, Type> DataTypeControls { get; set; } = new Dictionary<string, Type>()
        {
            { "int", typeof(NumericUpDown) },
            { "nvarchar", typeof(TextBox) },
            { "datetime", typeof(DateTimePicker) },
            { "date", typeof(DateTimePicker) },
        };

        List<Type> ControlTypes { get; set; } = new List<Type>()
        {
            typeof(TextBox),
            typeof(NumericUpDown),
            typeof(ComboBox),
            typeof(DateTimePicker)
        };

        List<Type> FormTypes { get; set; } = new List<Type>()
        {
            typeof(InfoFormBuilder),
            typeof(CreateFormBuilder)
        };

        private DesignSurface DesignSurface { get; set; }
        #endregion

        #region Form Load
        private void FormMainV2_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dataGridViewControls.Columns["ControlType"];
            comboBox.DataSource = ControlTypes;
            comboBox.DisplayMember = "Name";

            comboBoxFormType.DataSource = FormTypes;
            comboBoxFormType.DisplayMember = "Name";
        }
        #endregion

        #region Connect
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                SqlConnection = new SqlConnection($"Server={textBoxServerName.Text};Database={textBoxDatabase.Text};User Id={textBoxUsername.Text};Password={textBoxPassword.Text};");
                SqlConnection.Open();

                DataTable dataTable = SqlConnection.GetSchema("Tables");
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxTables.Items.Add(row.ItemArray[2].ToString());
                }

                groupBoxServerInfo.Enabled = false;
                comboBoxTables.Enabled = true;
                buttonStartDesigner.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            Cursor = Cursors.Default;
        }
        #endregion

        #region Start Designer
        private void buttonStartDesigner_Click(object sender, EventArgs e)
        {
            textBoxFormName.Text = $"Form{comboBoxTables.SelectedItem}";

            string[] restrictionsColumns = new string[4] { null, null, comboBoxTables.SelectedItem.ToString(), null };
            DataTable schemaColumns = SqlConnection.GetSchema("Columns", restrictionsColumns);

            foreach (DataRow rowColumn in schemaColumns.Rows)
            {
                string columnName = rowColumn["COLUMN_NAME"].ToString();
                string dataType = rowColumn["DATA_TYPE"].ToString();

                if (DataTypeControls.TryGetValue(dataType, out Type control))
                {
                    dataGridViewControls.Rows.Add(new object[] { columnName, control.Name });
                }
                else
                {
                    dataGridViewControls.Rows.Add(new object[] { columnName, "TextBox" });
                }
            }
        }
        #endregion

        #region Create Designer
        int labelBiggerWidth = 0;
        private void buttonCreateDesigner_Click(object sender, EventArgs e)
        {
            DesignSurface = new DesignSurface((Type)comboBoxFormType.SelectedItem);
            IDesignerHost host = (IDesignerHost)DesignSurface.GetService(typeof(IDesignerHost));

            Form root = (Form)host.RootComponent;
            TypeDescriptor.GetProperties(root)["Name"].SetValue(root, textBoxFormName.Text);
            root.Text = textBoxFormText.Text;
            root.AutoScaleDimensions = new SizeF(9F, 21F);
            root.AutoScaleMode = AutoScaleMode.Font;
            root.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            root.Padding = new Padding(10);
            root.StartPosition = FormStartPosition.CenterScreen;
            root.AutoSize = true;

            Control oldControl = new Control();
            foreach (DataGridViewRow row in dataGridViewControls.Rows)
            {
                if (row.Cells["ControlName"].Value == null) continue;

                DataGridViewComboBoxCell comboBox = (DataGridViewComboBoxCell)row.Cells["ControlType"];
                Type type = comboBox.Items.Cast<Type>().First(item => item.Name == comboBox.Value.ToString());

                Control control = (Control)host.CreateComponent(type, row.Cells["ControlName"].Value.ToString());
                control.Margin = new Padding(0, 0, 0, 10);
                control.Location = new Point(0, oldControl.Size.Height + 10 + oldControl.Location.Y);
                control.Width = 300;

                List<string> tags = new List<string>() { "use" };
                if (row.Cells["Required"].Value is bool requiredValue && requiredValue) tags.Add("required");
                if (row.Cells["Locked"].Value is bool lockedValue && lockedValue) tags.Add("locked");
                control.Tag = string.Join(", ", tags);
                root.Controls.Add(control);

                Label labelTitle = (Label)host.CreateComponent(typeof(Label), $"labelTitle{control.Name}");
                labelTitle.Text = row.Cells["LabelText"].FormattedValue + ":";
                labelTitle.Margin = new Padding(0, 0, 10, 0);
                labelTitle.AutoSize = true;
                labelTitle.TabIndex = 0;
                labelTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                labelTitle.Location = new Point(10, control.Location.Y + (control.Height - labelTitle.Height) - 4);
                root.Controls.Add(labelTitle);

                if (labelTitle.Width > labelBiggerWidth) labelBiggerWidth = labelTitle.Width;
                oldControl = control;
            }

            foreach (Control control in root.Controls.OfType<Control>().Where(whereControl => whereControl.Tag != null && whereControl.Tag.ToString().Contains("use")))
            {
                control.Location = new Point(labelBiggerWidth + 10 + 10, control.Location.Y);
            }

            if (((Type)comboBoxFormType.SelectedItem).IsEquivalentTo(typeof(InfoFormBuilder)))
            {
                CustomButton editButton = (CustomButton)host.CreateComponent(typeof(EditButton), "buttonEdit");
                editButton.Text = "Edit";
                editButton.Width = 145;
                editButton.Height = 31;
                editButton.Margin = new Padding(0);
                editButton.Location = new Point(oldControl.Location.X + oldControl.Width - editButton.Width, oldControl.Size.Height + 10 + oldControl.Location.Y);
                root.Controls.Add(editButton);

                CustomButton deleteButton = (CustomButton)host.CreateComponent(typeof(LogicalDeleteButton), "buttonLogicalDelete");
                deleteButton.Text = "Delete";
                deleteButton.Width = 145;
                deleteButton.Height = 31;
                deleteButton.Margin = new Padding(0);
                deleteButton.Location = new Point(oldControl.Location.X + oldControl.Width - editButton.Width - 10 - deleteButton.Width, oldControl.Size.Height + 10 + oldControl.Location.Y);
                root.Controls.Add(deleteButton);
            }
            else if (((Type)comboBoxFormType.SelectedItem).IsEquivalentTo(typeof(CreateFormBuilder)))
            {
                CustomButton createButton = (CustomButton)host.CreateComponent(typeof(CreateButton), "buttonCreate");
                createButton.Text = "Save";
                createButton.Width = 145;
                createButton.Height = 31;
                createButton.Margin = new Padding(0);
                createButton.Location = new Point(oldControl.Location.X + oldControl.Width - createButton.Width, oldControl.Size.Height + 10 + oldControl.Location.Y);
                root.Controls.Add(createButton);
            }

            groupBoxDesigner.Controls.Clear();
            Control view = (Control)DesignSurface.View;
            view.Dock = DockStyle.Fill;
            view.BackColor = Color.White;
            groupBoxDesigner.Controls.Add(view);
        }
        #endregion

        #region Get File
        private void buttonGetFile_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            string code = GenerateCSFromDesigner();
            StringBuilder stringBuilderDesigner = new StringBuilder();
            stringBuilderDesigner.AppendLine("namespace ECBuilderGenerator");
            stringBuilderDesigner.AppendLine("{");
            stringBuilderDesigner.Append(string.Join("\n", code.Replace("\r\n", "\n").Split('\n').Select(str => $"\t{str}").ToArray()));
            code = stringBuilderDesigner.ToString();
            code = code.Replace($" : {((Type)comboBoxFormType.SelectedItem).FullName}", "");
            code = code.Replace("public " + textBoxFormName.Text + "()\n\t    {\n\t        this.InitializeComponent();\n\t    }", "");
            code = code.Replace($"public partial class {textBoxFormName.Text}", $"partial class {textBoxFormName.Text}");
            code.TrimEnd();
            code += "}";
            StreamWriter streamWriterDesigner = new StreamWriter($@"C:\Users\SuffaTech_11003\Desktop\{textBoxFormName.Text}.Designer.cs");
            streamWriterDesigner.Write(code);
            streamWriterDesigner.Close();

            StringBuilder stringBuilderMain = new StringBuilder();
            stringBuilderMain.AppendLine("namespace ECBuilderGenerator");
            stringBuilderMain.AppendLine("{");
            stringBuilderMain.AppendLine($"\tpublic partial class {textBoxFormName.Text} : {((Type)comboBoxFormType.SelectedItem).FullName}");
            stringBuilderMain.AppendLine("\t{");
            stringBuilderMain.AppendLine($"\t\tpublic {textBoxFormName.Text}()");
            stringBuilderMain.AppendLine("\t\t{");
            stringBuilderMain.AppendLine("\t\t\tInitializeComponent();");
            stringBuilderMain.AppendLine("\t\t}");
            stringBuilderMain.AppendLine("\t}");
            stringBuilderMain.AppendLine("}");

            StreamWriter streamWriterMain = new StreamWriter($@"C:\Users\SuffaTech_11003\Desktop\{textBoxFormName.Text}.cs");
            streamWriterMain.Write(stringBuilderMain.ToString());
            streamWriterMain.Close();

            Cursor = Cursors.Default;
        }
        #endregion

        #region Methods
        private string GenerateCSFromDesigner()
        {
            CodeTypeDeclaration type;
            IDesignerHost host = (IDesignerHost)DesignSurface.GetService(typeof(IDesignerHost));
            IComponent root = host.RootComponent;
            DesignerSerializationManager manager = new DesignerSerializationManager(host);
            using (manager.CreateSession())
            {
                TypeCodeDomSerializer serializer = (TypeCodeDomSerializer)manager.GetSerializer(root.GetType(),
                    typeof(TypeCodeDomSerializer));
                type = serializer.Serialize(manager, root, host.Container.Components);
                type.IsPartial = true;
                type.Members.OfType<CodeConstructor>()
                    .FirstOrDefault().Attributes = MemberAttributes.Public;
            }
            StringBuilder builder = new StringBuilder();
            CodeGeneratorOptions option = new CodeGeneratorOptions()
            {
                BracingStyle = "C",
                BlankLinesBetweenMembers = false,
            };

            using (StringWriter writer = new StringWriter(builder, CultureInfo.InvariantCulture))
            {
                using (CSharpCodeProvider codeDomProvider = new CSharpCodeProvider())
                {
                    codeDomProvider.GenerateCodeFromType(type, writer, option);
                }

                return builder.ToString();
            }
        }

        #endregion

        #region DataGridView Context Menu Strip
        DataGridViewRow selectedRow;
        private void dataGridViewControls_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedRow = dataGridViewControls.Rows[dataGridViewControls.HitTest(e.X, e.Y).RowIndex];
                selectedRow.Selected = true;

                if (selectedRow.IsNewRow) return;

                contextMenuStripDataGridView.Tag = selectedRow;
                contextMenuStripDataGridView.Show(dataGridViewControls, new Point(e.X, e.Y));
            }
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            dataGridViewControls.Rows.Remove(selectedRow);
        }

        private void contextMenuStripDataGridView_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            DataGridViewRow selectedRow = contextMenuStripDataGridView.Tag as DataGridViewRow;

            if (
                selectedRow != null
                && int.TryParse(toolStripTextBoxRepositing.Text, out int index)
                && index < dataGridViewControls.Rows.Count - 1
            )
            {
                dataGridViewControls.Rows.Remove(selectedRow);
                dataGridViewControls.Rows.Insert(index, selectedRow);
            }

            toolStripTextBoxRepositing.Clear();
        }
        #endregion
    }
}

using ECBuilder.Builders.FormBuilders;
using ECBuilder.DataAccess;
using ECBuilder.Helpers;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Builders.DataGridViewBuilders
{
    /// <summary>
    /// Shows the entities of the given type.
    /// </summary>
    public class DataGridViewBuilder : DataGridView
    {
        public DataGridViewBuilder()
        {

        }

        #region Properties
        /// <summary>
        /// Form type in which the information of the selected Entity in the DataGridView will be opened.
        /// </summary>
        public Type InfoForm { get; set; }

        /// <summary>
        /// Form type to open to create entity
        /// </summary>
        public Type CreateForm { get; set; }

        /// <summary>
        /// Method to be run when <see cref="InfoForm">InfoForm</see> is closed.
        /// </summary>
        public Func<DialogResult, Task> InfoFormCloseEvent { get; set; }

        /// <summary>
        /// Method to be run when <see cref="InfoForm">InfoForm</see> is closed.
        /// </summary>
        public Func<DialogResult, Task> CreateFormCloseEvent { get; set; }

        /// <summary>
        /// DataGridView entity list type
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// DataGridView <see cref="Import">Imported</see> entity list.
        /// </summary>
        public List<IEntity> EntityList { get; set; }

        /// <summary>
        /// Import list types
        /// </summary>
        public List<Type> ImportListDefinition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Type, List<IEntity>> ImportLists { get; set; } = new Dictionary<Type, List<IEntity>>();

        /// <summary>
        /// <see langword="true"/>, adds the ID column to the DataGridView and writes the EntityID for each row. Default: <see langword="true"/>
        /// </summary>
        public bool AutoAddIDColumn { get; set; } = true;
        #endregion

        #region Events
        protected override async void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (InfoForm == null)
            {
                BuilderDebug.Error("InfoForm was null.");
                return;
            }

            if (!InfoForm.IsSubclassOf(typeof(InfoFormBuilder)))
            {
                BuilderDebug.Error("InfoForm was not InfoFormBuilder.");
                return;
            }

            InfoFormBuilder infoForm = (InfoFormBuilder)Activator.CreateInstance(InfoForm);

            infoForm.Entity = EntityList.Find(findEntity => findEntity.GetType().GetProperty($"{findEntity.GetType().Name}ID").GetValue(findEntity).Equals(this.Rows[e.RowIndex].Cells[$"{EntityType.Name}ID"].Value));
            DialogResult dialogResult = infoForm.ShowDialog(this);

            if (InfoFormCloseEvent != null)
            {
                await InfoFormCloseEvent(dialogResult);
            }

            if (dialogResult == DialogResult.OK)
            {
                await this.Import();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Imports DataGridView's entities.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task Import()
        {
            #region Import Configurations
            this.Rows.Clear();
            ImportLists.Clear();

            if (AutoAddIDColumn)
            {
                this.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = $"{EntityType.Name}ID",
                    HeaderText = "ID",
                    Visible = false,
                });
            }
            #endregion

            #region Import
            if (ImportListDefinition != null && ImportListDefinition.Count > 0)
            {
                foreach (Type type in ImportListDefinition)
                {
                    ImportLists.Add(type, await API.GetAll(type, 1));
                }
            }

            EntityList = await API.GetAll(EntityType, 1);
            #endregion

            #region Adding Rows
            EntityList.ForEach(item =>
            {
                object[] values = new object[this.ColumnCount];

                for (int columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    DataGridViewColumn column = this.Columns[columnIndex];

                    object itemValue = item.GetType().GetProperty(column.Name).GetValue(item);

                    if (column is DataGridViewTextBoxColumn)
                    {
                        values.SetValue(itemValue, columnIndex);
                    }
                    else if (column is DataGridViewImageColumn)
                    {
                        values.SetValue(AssetsHelper.GetImage(itemValue.ToString(), false, true), columnIndex);
                    }
                }

                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.Rows.Add(values);
                });
            });
            #endregion
        }

        /// <summary>
        /// Opens <see cref="CreateForm">CreateForm</see>.
        /// </summary>
        public async void ShowCreateForm()
        {
            if (CreateForm == null)
            {
                BuilderDebug.Error("CreateForm was null.");
                return;
            }

            if (!CreateForm.IsSubclassOf(typeof(CreateFormBuilder)))
            {
                BuilderDebug.Error("CreateForm was not CreateFormBuilder.");
                return;
            }

            CreateFormBuilder createForm = (CreateFormBuilder)Activator.CreateInstance(CreateForm);

            createForm.Entity = (IEntity)Activator.CreateInstance(EntityType);
            DialogResult dialogResult = createForm.ShowDialog(this);

            if (CreateFormCloseEvent != null)
            {
                await CreateFormCloseEvent(dialogResult);
            }

            if (dialogResult == DialogResult.OK)
            {
                await this.Import();
            }
        }
        #endregion
    }
}

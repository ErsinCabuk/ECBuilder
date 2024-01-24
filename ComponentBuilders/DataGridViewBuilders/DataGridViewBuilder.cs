using ECBuilder.ComponentBuilders.DataGridViewBuilders.Columns;
using ECBuilder.DataAccess;
using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Helpers;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.ComponentBuilders.DataGridViewBuilders
{
    /// <summary>
    /// Displays entities of the given type in <see cref="DataGridView">DataGridView</see>.
    /// </summary>
    public class DataGridViewBuilder : DataGridView, IComponentBuilder
    {
        public DataGridViewBuilder()
        {

        }

        #region Properties
        public Type InfoForm { get; set; }

        public Type CreateForm { get; set; }

        public Func<DialogResult, Task> InfoFormCloseEvent { get; set; }

        public Func<DialogResult, Task> CreateFormCloseEvent { get; set; }

        public Type EntityType { get; set; }

        public List<IEntity> EntityList { get; set; } = new List<IEntity>();

        public List<IEntity> AddList { get; set; } = new List<IEntity>();

        public List<Type> ImportListDefinition { get; set; }

        public Dictionary<Type, List<IEntity>> ImportLists { get; set; } = new Dictionary<Type, List<IEntity>>();

        public bool EnableInfoForm { get; set; } = true;

        /// <summary>
        /// <see langword="true"/>, adds the ID column to the DataGridView and writes the EntityID for each row. Default: <see langword="true"/>
        /// </summary>
        public bool AutoAddIDColumn { get; set; } = true;
        #endregion

        #region Events
        protected override async void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (EnableInfoForm)
            {
                #region Controls
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
                #endregion

                InfoFormBuilder infoForm = (InfoFormBuilder)Activator.CreateInstance(InfoForm);

                infoForm.Entity = EntityList.Find(findEntity => findEntity.GetType().GetProperty($"{findEntity.GetType().Name}ID").GetValue(findEntity).Equals(this.Rows[e.RowIndex].Cells[$"{EntityType.Name}ID"].Value));
                infoForm.ComponentBuilder = this;
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

            base.OnCellDoubleClick(e);
        }
        #endregion

        #region Methods
        public async Task Import(List<IEntity> list = null)
        {
            #region Import Configurations
            this.Rows.Clear();
            ImportLists.Clear();
            EntityList.Clear();

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

            if (list == null)
            {
                EntityList.AddRange(AddList);
                EntityList.AddRange(await API.GetAll(EntityType, 1));
            }
            else
            {
                EntityList.AddRange(AddList);
                EntityList.AddRange(list);
            }
            #endregion

            #region Adding Rows
            EntityList.ForEach(item =>
            {
                object[] values = new object[this.ColumnCount];

                for (int columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                {
                    DataGridViewColumn column = this.Columns[columnIndex];

                    object itemValue = item.GetType().GetProperty(column.Name).GetValue(item);

                    if (column is DataGridViewCustomTextBoxColumn customTextBoxColumn)
                    {
                        itemValue = customTextBoxColumn.Use(itemValue, ImportLists);
                        values.SetValue(itemValue, columnIndex);
                    }
                    else if (column is DataGridViewTextBoxColumn)
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

        public async void ShowCreateForm()
        {
            #region Controls
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
            #endregion

            CreateFormBuilder createForm = (CreateFormBuilder)Activator.CreateInstance(CreateForm);
            createForm.Entity = (IEntity)Activator.CreateInstance(EntityType);
            createForm.ComponentBuilder = this;
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

using ECBuilder.Classes;
using ECBuilder.ComponentBuilders.DataGridViewBuilders.Columns;
using ECBuilder.DataAccess;
using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Helpers;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.ComponentBuilders.DataGridViewBuilders
{
    /// <summary>
    /// Displays entities of the given type in <see cref="DataGridView">DataGridView</see>.
    /// </summary>
    public class DataGridViewBuilder : DataGridView, IComponentBuilder
    {
        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type InfoForm { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type CreateForm { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<DialogResult, Task> InfoFormCloseEvent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<DialogResult, Task> CreateFormCloseEvent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type CreateEntityType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IEntity> EntityList { get; set; } = new List<IEntity>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IEntity> AddList { get; set; } = new List<IEntity>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Type> ImportListDefinition { get; set; } = new List<Type>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImportLists ImportLists { get; set; } = new ImportLists();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, (FilterTypes, object)> Filters { get; set; } = new Dictionary<string, (FilterTypes, object)>();

        public bool EnableInfoForm { get; set; } = true;

        /// <summary>
        /// <see langword="true"/>, adds the ID column to the DataGridView and writes the EntityID for each row. Default: <see langword="true"/>
        /// </summary>
        public bool AutoAddIDColumn { get; set; } = true;

        public IButtonControl CreateButton { get; set; } = null;
        #endregion

        #region Events
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (EnableInfoForm)
            {
                #region Controls
                if (InfoForm == null)
                {
                    BuilderDebug.Error("InfoForm was null.");
                    return;
                }

                if (!InfoForm.IsSubclassOf(typeof(InfoEntityFormBuilder)))
                {
                    BuilderDebug.Error("InfoForm was not InfoFormBuilder.");
                    return;
                }
                #endregion

                ShowInfoForm(EntityList.Find(findEntity => findEntity.GetType().GetProperty($"{findEntity.GetType().Name}ID").GetValue(findEntity).Equals(this.Rows[e.RowIndex].Cells[$"{EntityType.Name}ID"].Value)));
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            ShowCreateForm();
        }
        #endregion

        #region Methods
        public async void ShowCreateForm()
        {
            #region Controls
            if (CreateForm == null)
            {
                BuilderDebug.Error("CreateForm was null.");
                return;
            }

            if (!CreateForm.IsSubclassOf(typeof(CreateEntityFormBuilder)))
            {
                BuilderDebug.Error("CreateForm was not CreateFormBuilder.");
                return;
            }
            #endregion

            CreateEntityFormBuilder createForm = (CreateEntityFormBuilder)Activator.CreateInstance(CreateForm);
            createForm.Entity = CreateEntityType == null
                                ? (IEntity)Activator.CreateInstance(EntityType)
                                : (IEntity)Activator.CreateInstance(CreateEntityType);
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

        public async void ShowInfoForm(IEntity entity)
        {
            InfoEntityFormBuilder infoForm = (InfoEntityFormBuilder)Activator.CreateInstance(InfoForm);

            infoForm.Entity = entity;
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

        public async Task Initialize(List<IEntity> list = null)
        {
            #region Auto Add ID Column
            if (AutoAddIDColumn)
            {
                if(this.Columns.Contains($"{EntityType.Name}ID"))
                {
                    BuilderDebug.Warn($"{this.Name} is already containt ID column.");
                    return;
                }

                this.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = $"{EntityType.Name}ID",
                    HeaderText = "ID",
                    Visible = false,
                });
            }
            #endregion

            #region Import
            foreach (IComponentEntityType componentEntityType in this.Columns.OfType<IComponentEntityType>().Where(x => !ImportLists.ContainsKey(x.EntityType)))
            {
                ImportLists.Add(componentEntityType.EntityType, await API.GetAll(componentEntityType.EntityType));
            }

            foreach (Type type in ImportListDefinition)
            {
                if (!ImportLists.ContainsKey(type))
                {
                    ImportLists.Add(type, await API.GetAll(type));
                }
                else
                {
                    BuilderDebug.Warn($"DataGridViewBuilder.ImportListDefinition.{type.Name} is already contains in DataGridViewBuilder.ImportLists");
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

            #region Filters
            if (Filters.Count > 0)
            {
                EntityList = ListHelper.Filter(EntityList, Filters);
            }
            #endregion

            #region Add Rows
            await AddRows();
            #endregion

            #region Create Button
            if (this.CreateButton != null)
            {
                ((Button)this.CreateButton).Click += CreateButton_Click;
            }
            #endregion
        }

        public async Task Import(List<IEntity> list = null)
        {
            #region Import Confs
            this.Rows.Clear();
            EntityList.Clear();
            #endregion

            #region Import
            foreach (Type type in ImportLists.Keys.ToArray())
            {
                ImportLists[type] = await API.GetAll(type);
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

            #region Filters
            if (Filters.Count > 0)
            {
                EntityList = ListHelper.Filter(EntityList, Filters);
            }
            #endregion

            #region Add Rows
            await AddRows();
            #endregion
        }

        private Task AddRows()
        {
            return Task.Run(() =>
            {
                foreach (IEntity entity in EntityList)
                {
                    object[] values = new object[this.ColumnCount];

                    for (int columnIndex = 0; columnIndex < this.ColumnCount; columnIndex++)
                    {
                        DataGridViewColumn column = this.Columns[columnIndex];

                        object itemValue = entity.GetType().GetProperty(column.Name).GetValue(entity);

                        if (column is DataGridViewCustomTextBoxColumn customTextBoxColumn)
                        {
                            itemValue = customTextBoxColumn.Use(itemValue, ImportLists[customTextBoxColumn.EntityType]);
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
                }
            });
        }
        #endregion
    }
}

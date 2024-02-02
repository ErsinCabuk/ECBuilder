﻿using ECBuilder.Classes;
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

namespace ECBuilder.ComponentBuilders.TreeViewBuilders
{
    /// <summary>
    /// Displays entities of the given type in <see cref="TreeView">TreeView</see>.
    /// </summary>
    public class TreeViewBuilder : TreeView, IComponentBuilder
    {
        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type InfoForm { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type CreateForm { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DialogResult, Task> InfoFormCloseEvent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DialogResult, Task> CreateFormCloseEvent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type CreateEntityType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IEntity> EntityList { get; set; } = new List<IEntity>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IEntity> AddList { get; set; } = new List<IEntity>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Type> ImportListDefinition { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImportLists ImportLists { get; set; } = new ImportLists();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, (FilterTypes, object)> Filters { get; set; } = new Dictionary<string, (FilterTypes, object)>();

        public bool EnableInfoForm { get; set; } = true;

        /// <summary>
        /// If the Entity contains Superior and there will be sub-breaks of nodes, true should be selected. Default is <see langword="true"/>.
        /// </summary>
        public bool UseSuperior { get; set; } = true;

        public IButtonControl CreateButton { get; set; }
        #endregion

        #region Events
        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            if (EnableInfoForm)
            {
                #region Controls
                if (this.SelectedNode == null) return;

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

                ShowInfoForm((IEntity)this.SelectedNode.Tag);
            }

            base.OnNodeMouseDoubleClick(e);
        }
        
        private void CreateButton_Click(object sender, EventArgs e)
        {
            ShowCreateForm();
        }
        #endregion

        #region Methods
        public async Task Import(List<IEntity> list = null)
        {
            #region Import Configurations
            this.Nodes.Clear();
            ImportLists.Clear();
            EntityList.Clear();
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

            #region Filter
            if (Filters.Count > 0)
            {
                EntityList = ListHelper.Filter(EntityList, Filters);
            }
            #endregion

            #region Adding Nodes
            if (UseSuperior)
            {
                foreach (IEntity entity in EntityList.Where(whereEntity => whereEntity.GetType().GetProperty($"{whereEntity.GetType().Name}SuperiorID").GetValue(whereEntity).Equals(0)))
                {
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity).ToString(),
                        Text = entity.GetType().GetProperty($"{entity.GetType().Name}Name").GetValue(entity).ToString(),
                        Tag = entity
                    };

                    //this.BeginInvoke((MethodInvoker)delegate
                    //{
                    this.Nodes.Add(treeNode);
                    //});
                }

                foreach (IEntity entity in EntityList.Where(whereEntity => Convert.ToInt32(whereEntity.GetType().GetProperty($"{whereEntity.GetType().Name}SuperiorID").GetValue(whereEntity)) > 0))
                {
                    //this.BeginInvoke((MethodInvoker)delegate
                    //{
                    this.Nodes.Find(entity.GetType().GetProperty($"{entity.GetType().Name}SuperiorID").GetValue(entity).ToString(), true)[0].Nodes.Add(new TreeNode
                    {
                        Name = entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity).ToString(),
                        Text = entity.GetType().GetProperty($"{entity.GetType().Name}Name").GetValue(entity).ToString(),
                        Tag = entity
                    });
                    //});
                }
            }
            else
            {
                foreach (IEntity entity in EntityList)
                {
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity).ToString(),
                        Text = entity.GetType().GetProperty($"{entity.GetType().Name}Name").GetValue(entity).ToString(),
                        Tag = entity
                    };

                    //this.BeginInvoke((MethodInvoker)delegate
                    //{
                    this.Nodes.Add(treeNode);
                    //});
                }
            }
            #endregion

            #region Create Button
            if (this.CreateButton != null)
            {
                ((Button)this.CreateButton).Click -= CreateButton_Click;
                ((Button)this.CreateButton).Click += CreateButton_Click;
            }
            #endregion
        }

        public async void ShowInfoForm(IEntity entity)
        {
            InfoFormBuilder infoForm = (InfoFormBuilder)Activator.CreateInstance(InfoForm);
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
        #endregion
    }
}

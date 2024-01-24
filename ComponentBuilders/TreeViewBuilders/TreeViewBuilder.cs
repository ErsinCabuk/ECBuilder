﻿using ECBuilder.DataAccess;
using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
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
        public Type InfoForm { get; set; }

        public Type CreateForm { get; set; }

        public Func<DialogResult, Task> InfoFormCloseEvent { get; set; }

        public Func<DialogResult, Task> CreateFormCloseEvent { get; set; }

        public Type EntityType { get; set; }

        public List<IEntity> EntityList { get; set; } = new List<IEntity>();

        public List<Type> ImportListDefinition { get; set; }

        public Dictionary<Type, List<IEntity>> ImportLists { get; set; } = new Dictionary<Type, List<IEntity>>();

        /// <summary>
        /// If the Entity contains Superior and there will be sub-breaks of nodes, true should be selected. Default is <see langword="true"/>.
        /// </summary>
        public bool UseSuperior { get; set; } = true;

        public List<IEntity> AddList { get; set; } = new List<IEntity>();

        public bool EnableInfoForm { get; set; } = true;

        public bool NotImportEntityList { get; set; } = false;
        #endregion

        #region Events
        protected override async void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            if (EnableInfoForm)
            {
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

                InfoFormBuilder infoForm = (InfoFormBuilder)Activator.CreateInstance(InfoForm);

                infoForm.Entity = (IEntity)this.SelectedNode.Tag;
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

            base.OnNodeMouseDoubleClick(e);
        }
        #endregion

        #region Methods
        public async Task Import()
        {
            #region Import Configurations
            this.Nodes.Clear();
            ImportLists.Clear();
            #endregion

            #region Import
            if (ImportListDefinition != null && ImportListDefinition.Count > 0)
            {
                foreach (Type type in ImportListDefinition)
                {
                    ImportLists.Add(type, await API.GetAll(type, 1));
                }
            }

            if (!NotImportEntityList)
            {
                EntityList.Clear();
                EntityList.AddRange(AddList);
                EntityList.AddRange(await API.GetAll(EntityType, 1));
            }
            else
            {
                AddList.AddRange(EntityList);
                EntityList = AddList;

                Console.WriteLine(string.Join(",", EntityList.Select(x => x.GetType().GetProperty(x.GetType().Name + "Name").GetValue(x))));
            }
            #endregion

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

                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.Nodes.Add(treeNode);
                    });
                }

                foreach (IEntity entity in EntityList.Where(whereEntity => Convert.ToInt32(whereEntity.GetType().GetProperty($"{whereEntity.GetType().Name}SuperiorID").GetValue(whereEntity)) > 0))
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.Nodes.Find(entity.GetType().GetProperty($"{entity.GetType().Name}SuperiorID").GetValue(entity).ToString(), true)[0].Nodes.Add(new TreeNode
                        {
                            Name = entity.GetType().GetProperty($"{entity.GetType().Name}ID").GetValue(entity).ToString(),
                            Text = entity.GetType().GetProperty($"{entity.GetType().Name}Name").GetValue(entity).ToString(),
                            Tag = entity
                        });
                    });
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

                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.Nodes.Add(treeNode);
                    });
                }
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

using ECBuilder.ComponentBuilders;
using ECBuilder.ComponentBuilders.DataGridViewBuilders;
using ECBuilder.ComponentBuilders.TreeViewBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.ComponentBuilderFormBuilders
{
    [Serializable]
    public class ComponentBuilderFormBuilder : FormBuilder
    {
        public ComponentBuilderFormBuilder()
        {
            this.LoadEvent = ComponentBuilderFormBuilder_LoadEvent;
        }

        #region Properties
        public Type ComponentBuilderType { get; set; }

        public IComponentBuilder ComponentBuilder { get; set; }

        public IEntity SelectedEntity { get; set; }
        #endregion

        private async Task ComponentBuilderFormBuilder_LoadEvent()
        {
            #region Controls
            Control findPanel = UsingControls.Find(control => control.Tag.ToString().Contains("componentBuilderPanel"));
            if (findPanel == null)
            {
                BuilderDebug.Error("Panel not found in ComponentBuilderFormBuilder");
                return;
            }

            if (!ComponentBuilderType.IsSubclassOf(typeof(TreeViewBuilder)) && !ComponentBuilderType.IsSubclassOf(typeof(DataGridViewBuilder)))
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder.IComponentBuilder was not IComponentBuilder");
                return;
            }
            #endregion

            Panel panel = (Panel)findPanel;

            ComponentBuilder = (IComponentBuilder)Activator.CreateInstance(ComponentBuilderType);
            panel.Controls.Add((Control)ComponentBuilder);

            if (((FormBuilder)this.Owner).ImportLists.TryGetValue(ComponentBuilder.EntityType, out List<IEntity> entityList))
            {
                ComponentBuilder.NotImportEntityList = true;
                ComponentBuilder.EntityList = entityList;
                Console.WriteLine("****" + string.Join(",", ComponentBuilder.EntityList.Select(x => x.GetType().GetProperty(x.GetType().Name + "Name").GetValue(x))));
                await ComponentBuilder.Import();
            }
            else
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder.ComponentBuilder.EntityType is not found in ImportLists.");
            }
        }
    }
}

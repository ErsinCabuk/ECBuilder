using ECBuilder.ComponentBuilders;
using ECBuilder.ComponentBuilders.DataGridViewBuilders;
using ECBuilder.ComponentBuilders.TreeViewBuilders;
using ECBuilder.Test;
using System;
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

        public object SelectedValue { get; set; }
        #endregion

        private async Task ComponentBuilderFormBuilder_LoadEvent()
        {
            Control findPanel = UsingControls.Find(control => control.Tag.ToString().Contains("componentBuilderPanel"));
            if (findPanel == null)
            {
                BuilderDebug.Error("Panel not found in ComponentBuilderFormBuilder");
                return;
            }

            object componentBuilderType = Activator.CreateInstance(ComponentBuilderType);
            if (!componentBuilderType.GetType().IsSubclassOf(typeof(TreeViewBuilder)) && !componentBuilderType.GetType().IsSubclassOf(typeof(DataGridViewBuilder)))
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder.IComponentBuilder was not IComponentBuilder");
                return;
            }

            Panel panel = (Panel)findPanel;
            ComponentBuilder = (IComponentBuilder)componentBuilderType;
            panel.Controls.Add((Control)ComponentBuilder);
            await ComponentBuilder.Import();
        }
    }
}

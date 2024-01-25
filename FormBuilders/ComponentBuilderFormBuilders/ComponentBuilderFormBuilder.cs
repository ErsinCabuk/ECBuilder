using ECBuilder.ComponentBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.ComponentBuilderFormBuilders
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentBuilderFormBuilder : FormBuilder
    {
        public ComponentBuilderFormBuilder()
        {
            this.LoadEvent = ComponentBuilderFormBuilder_LoadEvent;
        }

        #region Properties
        public IComponentBuilder ComponentBuilder { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEntity SelectedEntity { get; set; }
        #endregion

        public async Task ComponentBuilderFormBuilder_LoadEvent()
        {
            Panel panel = (Panel)UsingControls.Find(findControl => findControl.Tag.ToString().Contains("componentBuilderPanel"));
            FormBuilder ownerFormBuilder = (FormBuilder)this.Owner;

            #region Controls
            if (panel == null)
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder componentBuilderPanel was not found.");
                return;
            }

            if (!ownerFormBuilder.ImportLists.ContainsKey(ComponentBuilder.EntityType))
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder.ComponentBuilder.EntityType was not found in FormBuilder.ImportLists");
                return;
            }
            #endregion

            panel.Controls.Add((Control)ComponentBuilder);
            await ComponentBuilder.Import(ownerFormBuilder.ImportLists[ComponentBuilder.EntityType]);
        }
    }
}

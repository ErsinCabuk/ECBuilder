using ECBuilder.ComponentBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.ComponentModel;
using System.Linq;
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
            Control[] findControl = this.Controls.Find("componentBuilderPanel", true);

            Panel panel = (Panel)findControl[0];
            FormBuilder ownerFormBuilder = (FormBuilder)this.Owner;

            #region Controls
            if (findControl.Length == 0)
            {
                BuilderDebug.Error("componentBuilderPanel Panel was not found");
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

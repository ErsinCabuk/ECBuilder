using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// Form that creates an entity.
    /// </summary>
    public class CreateEntityFormBuilder : EntityFormBuilder
    {
        public CreateEntityFormBuilder()
        {
            LoadEvent += CreateFormBuilder_LoadEvent;
        }

        private async Task CreateFormBuilder_LoadEvent()
        {
            foreach (Control control in UsingControls)
            {
                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    await componentBuilderTextBox.Import();
                }
                else if (control is CustomComboBox customComboBox)
                {
                    await customComboBox.Import();
                }
                else if (control is IComponentBuilder componentBuilder)
                {
                    await componentBuilder.Initialize(this.ImportLists[componentBuilder.EntityType]);
                }
            }
        }
    }
}

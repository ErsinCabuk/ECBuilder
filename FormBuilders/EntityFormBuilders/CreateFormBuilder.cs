using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.Test;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// Form that creates an entity.
    /// </summary>
    public class CreateFormBuilder : EntityFormBuilder
    {
        public CreateFormBuilder()
        {
            LoadEvent = CreateFormBuilder_LoadEvent;
        }

        public async Task CreateFormBuilder_LoadEvent()
        {
            if (Entity == null)
            {
                BuilderDebug.Error("Entity was null");
                return;
            }

            foreach (Control control in UsingControls)
            {
                PropertyInfo property = Entity.GetType().GetProperty(control.Name);
                if (property == null && !control.Tag.ToString().Contains("notProperty"))
                {
                    BuilderDebug.Error(this.DesignMode, $"{control.Name} property was not found in the {this.Name} form");
                    continue;
                }

                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    await componentBuilderTextBox.Import();
                }
                else if (control is CustomComboBox customComboBox)
                {
                    await customComboBox.Import();
                }
            }
        }
    }
}

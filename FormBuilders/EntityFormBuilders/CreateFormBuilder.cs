using ECBuilder.Components.ComboBoxes;
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

        public Task CreateFormBuilder_LoadEvent()
        {
            if (Entity == null)
            {
                BuilderDebug.Error("Entity was null");
                return Task.CompletedTask;
            }

            foreach (Control control in UsingControls)
            {
                PropertyInfo property = Entity.GetType().GetProperty(control.Name);
                if (property == null)
                {
                    BuilderDebug.Error(this.DesignMode, $"{control.Name} property was not found in the {this.Name} form");
                    continue;
                }

                else if (control is CustomComboBox customComboBox)
                {
                    customComboBox.FormBuilder = this;
                    customComboBox.Import();
                }
            }

            return Task.CompletedTask;
        }
    }
}

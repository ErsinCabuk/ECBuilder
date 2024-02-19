using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Linq;
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
            if (Entity == null)
            {
                BuilderDebug.Error("Entity was null");
                return;
            }

            foreach (IComponentEntityType componentEntityType in UsingControls.OfType<IComponentEntityType>())
            {
                await AddImportList(componentEntityType.EntityType);
            }

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

        private async Task AddImportList(Type type)
        {
            if (!ImportLists.ContainsKey(type))
            {
                ImportLists.Add(type, await API.GetAll(type));
            }
            else
            {
                BuilderDebug.Warn($"{type.Name} already contains in CreateFormBuilder.ImportLists");
            }
        }
    }
}

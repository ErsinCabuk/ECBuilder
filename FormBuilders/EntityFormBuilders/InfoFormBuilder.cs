using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.FormBuilders.ComponentBuilderFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// An entitys information form.
    /// </summary>
    public class InfoFormBuilder : EntityFormBuilder
    {
        public InfoFormBuilder()
        {
            LoadEvent = InfoFormBuilder_LoadEvent;
        }

        #region Events
        public Task InfoFormBuilder_LoadEvent()
        {
            return Task.Run(() =>
            {
                if (Entity == null)
                {
                    BuilderDebug.Error("Entity was null");
                    return;
                }

                foreach (Control control in UsingControls)
                {
                    PropertyInfo property = Entity.GetType().GetProperty(control.Name);
                    if (property == null)
                    {
                        BuilderDebug.Error(this.DesignMode, $"{control.Name} property was not found in the {this.Name} form");
                        continue;
                    }

                    object value = property.GetValue(Entity);
                    if (value == null)
                    {
                        continue;
                    }

                    if (control is ComponentBuilderTextBox componentBuilderTextBox)
                    {
                        IEntity entity = this.ImportLists[((IComponentBuilder)componentBuilderTextBox.ComponentBuilderType).EntityType].Find(findEntity => findEntity.GetType().GetProperty(componentBuilderTextBox.ValueProperty).GetValue(findEntity).Equals(value));
                        componentBuilderTextBox.Text = entity.GetType().GetProperty(
                            string.IsNullOrEmpty(componentBuilderTextBox.DisplayProperty) 
                                ? $"{entity.GetType().Name}Name" 
                                : componentBuilderTextBox.DisplayProperty
                        ).GetValue(entity).ToString();

                        componentBuilderTextBox.Value = entity.GetType().GetProperty(
                             string.IsNullOrEmpty(componentBuilderTextBox.ValueProperty)
                                ? $"{entity.GetType().Name}ID"
                                : componentBuilderTextBox.ValueProperty
                        ).GetValue(entity).ToString();
                    }
                    else if (control is TextBox || control is RichTextBox)
                    {
                        control.Text = value.ToString();
                    }
                    else if (control is DateTimePicker dateTimePicker)
                    {
                        if (DateTime.TryParse(value.ToString(), out DateTime dateTime))
                        {
                            dateTimePicker.Value = dateTime;
                        }
                        else
                        {
                            BuilderDebug.Error($"{control.Name} property could not be converted to DateTime.");
                        }
                    }
                    else if (control is NumericUpDown numericUpDown)
                    {
                        if (int.TryParse(value.ToString(), out int intValue))
                        {
                            numericUpDown.Value = intValue;
                        }
                        else
                        {
                            BuilderDebug.Error($"{control.Name} property could not be converted to int.");
                        }
                    }
                    else if (control is CustomComboBox customComboBox)
                    {
                        customComboBox.FormBuilder = this;
                        customComboBox.Import(value);
                    }
                }
            });
        }
        #endregion
    }
}

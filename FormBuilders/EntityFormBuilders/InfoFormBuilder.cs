using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
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
        public async Task InfoFormBuilder_LoadEvent()
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
                    await componentBuilderTextBox.Import();
                    componentBuilderTextBox.SetSelectedEntity(value);
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
                    if (value.GetType() == typeof(decimal))
                    {
                        if (decimal.TryParse(value.ToString(), out decimal intValue))
                        {
                            numericUpDown.Value = intValue;
                        }
                        else
                        {
                            BuilderDebug.Error($"{control.Name} property could not be converted to decimal.");
                        }
                    }
                    else if (value.GetType() == typeof(int))
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
                }
                else if (control is CustomComboBox customComboBox)
                {
                    await customComboBox.Import(value);
                }
            }
        }
        #endregion
    }
}

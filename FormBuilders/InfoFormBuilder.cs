using ECBuilder.Components.ComboBoxes;
using ECBuilder.Test;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders
{
    /// <summary>
    /// An entitys information form.
    /// </summary>
    public class InfoFormBuilder : FormBuilder
    {
        public InfoFormBuilder()
        {
            LoadEvent = InfoFormBuilder_LoadEvent;
        }

        #region Events
        public Task InfoFormBuilder_LoadEvent()
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

                object value = property.GetValue(Entity);
                if (value == null)
                {
                    continue;
                }

                if (control is TextBox || control is RichTextBox)
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

            return Task.CompletedTask;
        }
        #endregion
    }
}

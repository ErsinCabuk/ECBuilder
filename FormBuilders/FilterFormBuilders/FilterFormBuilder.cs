using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.FilterFormBuilders
{
    /// <summary>
    /// It performs operations related to an Entity.
    /// </summary>
    [Serializable]
    public class FilterFormBuilder : FormBuilder
    {
        public FilterFormBuilder()
        {
            this.BeforeLoadEvent += FilterFormBuilder_BeforeLoadEvent;
            this.LoadEvent += FilterFormBuilder_LoadEvent;
        }

        #region Properties
        /// <summary>
        /// Owner ComponentBuilder
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentBuilder ComponentBuilder { get; set; }

        /// <summary>
        /// Controls to be used in the form and included in the process.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Control> UsingControls { get; set; } = new List<Control>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Control, Types.FilterTypes> ControlFilterTypes { get; set; }
        #endregion

        private Task FilterFormBuilder_BeforeLoadEvent()
        {
            return Task.Run(() =>
            {
                GetControls(this);
                UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

                foreach (IComponentEntityType componentEntityType in UsingControls.OfType<IComponentEntityType>())
                {
                    if (!ImportListDefinitions.Contains(componentEntityType.EntityType) && !ImportLists.ContainsKey(componentEntityType.EntityType))
                    {
                        ImportListDefinitions.Add(componentEntityType.EntityType);
                    }
                    else
                    {
                        BuilderDebug.Warn($"{componentEntityType.EntityType.Name} already contains in {this.Name}.ImportListDefinitions or {this.Name}.ImportLists");
                    }
                }
            });
        }

        private async Task FilterFormBuilder_LoadEvent()
        {
            await this.InitializeControls();
        }

        #region Methods
        /// <summary>
        /// Initialize controls.
        /// </summary>
        /// <returns>awaitable Task</returns>
        public async Task InitializeControls()
        {
            foreach (Control control in UsingControls)
            {
                object selectedValue = null;
                if (this.ComponentBuilder.Filters.ContainsKey(control.Name))
                {
                    selectedValue = this.ComponentBuilder.Filters[control.Name].Item2;
                }

                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    await componentBuilderTextBox.Import();
                    if (selectedValue != null) componentBuilderTextBox.SetSelectedEntity(selectedValue);
                }
                else if ((control is TextBox || control is RichTextBox) && selectedValue != null)
                {
                    control.Text = selectedValue.ToString();
                }
                else if (control is DateTimePicker dateTimePicker && selectedValue != null)
                {
                    if (DateTime.TryParse(selectedValue.ToString(), out DateTime dateTime))
                    {
                        dateTimePicker.Value = dateTime;
                    }
                    else
                    {
                        BuilderDebug.Error($"{control.Name} property could not be converted to DateTime.");
                    }
                }
                else if (control is NumericUpDown numericUpDown && selectedValue != null)
                {
                    if (selectedValue.GetType() == typeof(decimal))
                    {
                        if (decimal.TryParse(selectedValue.ToString(), out decimal intValue))
                        {
                            numericUpDown.Value = intValue;
                        }
                        else
                        {
                            BuilderDebug.Error($"{control.Name} property could not be converted to decimal.");
                        }
                    }
                    else if (selectedValue.GetType() == typeof(int))
                    {
                        if (int.TryParse(selectedValue.ToString(), out int intValue))
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
                    await customComboBox.Import(selectedValue);
                }
            }
        }
        #endregion

        #region Private Methods
        private void GetControls(Control control)
        {
            foreach (Control forControl in control.Controls)
            {
                if ((forControl.Tag != null) && forControl.Tag.ToString().Contains("use"))
                {
                    UsingControls.Add(forControl);
                }

                if (forControl.HasChildren)
                {
                    GetControls(forControl);
                }
            }
        }
        #endregion
    }
}

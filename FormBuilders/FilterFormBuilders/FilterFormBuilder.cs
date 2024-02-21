using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.FilterFormBuilders
{
    /// <summary>
    /// 
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
        public List<Control> UsingControls { get; set; } = new List<Control>();

        public IComponentBuilder ComponentBuilder { get; set; }

        public IButtonControl FilterButton { get; set; }

        public Dictionary<Control, FilterTypes> ControlFilterTypes { get; set; } = new Dictionary<Control, FilterTypes>();
        #endregion

        #region Before Load Event
        private Task FilterFormBuilder_BeforeLoadEvent()
        {
            return Task.Run(() =>
            {
                GetControls(this);
                UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

                if(FilterButton != null)
                {
                    ((Button)FilterButton).Click += FilterFormBuilder_Click;
                }
            });
        }
        #endregion

        #region Load Event
        private async Task FilterFormBuilder_LoadEvent()
        {
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
            }
        }
        #endregion

        #region Button Click
        private void FilterFormBuilder_Click(object sender, EventArgs e)
        {
            Filter();
        }
        #endregion

        #region Methods
        public void Filter()
        {
            #region Controls
            if (ComponentBuilder == null)
            {
                BuilderDebug.Error("FilterFormBuilder.ComponentBuilder was null.");
                return;
            }
            #endregion

            foreach (Control control in UsingControls)
            {
                #region Controls
                if (!ControlFilterTypes.ContainsKey(control))
                {
                    BuilderDebug.Warn($"FilterFormBuilder.ControlFilterTypes not contains {control.Name}");
                    continue;
                }
                #endregion

                object value = null;

                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    value = componentBuilderTextBox.SelectedValue;
                }
                else if (control is TextBox || control is RichTextBox)
                {
                    value = control.Text;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    value = dateTimePicker.Value;
                }
                else if (control is CustomComboBox customComboBox)
                {
                    IEntity selectedEntity = customComboBox.EntityList[customComboBox.SelectedIndex];
                    value = selectedEntity.GetType().GetProperty(control.Name).GetValue(selectedEntity);
                }
                else if (control is NumericUpDown numericUpDown)
                {
                   value = numericUpDown.Value;
                }

                if(this.ComponentBuilder.Filters.ContainsKey(control.Name))
                {
                    this.ComponentBuilder.Filters[control.Name] = (
                        ControlFilterTypes[control],
                        value
                    );
                }
                else
                {
                    this.ComponentBuilder.Filters.Add(
                        control.Name,
                        (
                            ControlFilterTypes[control],
                            value
                        )
                    );
                }
            }

            DialogResult = DialogResult.OK;
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
        #endregion
    }
}

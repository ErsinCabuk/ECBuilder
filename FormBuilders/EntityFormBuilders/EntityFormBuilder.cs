using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.Interfaces;
using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// It performs operations related to an Entity.
    /// </summary>
    [Serializable]
    public class EntityFormBuilder : FormBuilder
    {
        public EntityFormBuilder()
        {

        }

        #region Properties
        /// <summary>
        /// Entity
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEntity Entity { get; set; }

        /// <summary>
        /// Owner ComponentBuilder
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentBuilder ComponentBuilder { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Validates the <see cref="UsingControls">UsingControls</see>.
        /// </summary>
        /// <returns><see langword="true"/> if no wrong values in the controls; otherwise, <see langword="false"/></returns>
        public bool CheckControls()
        {
            foreach (Control control in UsingControls)
            {
                if (!control.Tag.ToString().Contains("required")) continue;

                bool result = true;
                if ((control is TextBox || control is RichTextBox) && string.IsNullOrEmpty(control.Text)) result = false;
                else if (control is NumericUpDown numericUpDown && string.IsNullOrEmpty(numericUpDown.Value.ToString())) result = false;
                else if (control is ComboBox comboBox && comboBox.SelectedIndex == -1) result = false;

                if (!result)
                {
                    control.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Sets values in controls to <see cref="Entity">Entity</see> properties.
        /// </summary>
        public void SetProperties()
        {
            foreach (Control control in UsingControls)
            {
                PropertyInfo propertyInfo = Entity.GetType().GetProperty(control.Name);
                if (propertyInfo == null) continue;

                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    propertyInfo.SetValue(Entity, componentBuilderTextBox.Value);
                }
                else if (control is TextBox || control is RichTextBox)
                {
                    propertyInfo.SetValue(Entity, control.Text.Trim());
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    propertyInfo.SetValue(Entity, dateTimePicker.Value);
                }
                else if (control is CustomComboBox customComboBox)
                {
                    IEntity selectedEntity = customComboBox.EntityList[customComboBox.SelectedIndex];
                    propertyInfo.SetValue(Entity, selectedEntity.GetType().GetProperty(customComboBox.ValueMember).GetValue(selectedEntity));
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    if (propertyInfo.PropertyType.IsEquivalentTo(typeof(int)))
                    {
                        propertyInfo.SetValue(Entity, Convert.ToInt32(numericUpDown.Value));
                    }
                    else if (propertyInfo.PropertyType.IsEquivalentTo(typeof(decimal)))
                    {
                        propertyInfo.SetValue(Entity, Convert.ToDecimal(numericUpDown.Value));
                    }
                }
            }
        }

        /// <summary>
        /// Sets default values for <see langword="null"/> <see cref="Entity">Entity</see> properties.
        /// </summary>
        public void SetEmptyProperties()
        {
            Entity.GetType().GetProperty($"{Entity.GetType().Name}State").SetValue(Entity, true);

            foreach (PropertyInfo propertyInfo in Entity.GetType().GetProperties().Where(wherePropertyInfo => wherePropertyInfo.GetValue(Entity) == null || wherePropertyInfo.GetValue(Entity).Equals(DateTime.MinValue)))
            {
                Type type = propertyInfo.PropertyType;
                if (type.IsEquivalentTo(typeof(string)))
                {
                    propertyInfo.SetValue(Entity, "");
                }
                else if (type.IsEquivalentTo(typeof(DateTime)))
                {
                    propertyInfo.SetValue(Entity, SqlDateTime.MinValue.Value);
                }
            }
        }
        #endregion
    }
}

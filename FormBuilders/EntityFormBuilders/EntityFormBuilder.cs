﻿using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
            this.BeforeLoadEvent += EntityFormBuilder_BeforeLoadEvent;
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

        /// <summary>
        /// Controls to be used in the form and included in the process.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Control> UsingControls { get; set; } = new List<Control>();
        #endregion

        private Task EntityFormBuilder_BeforeLoadEvent()
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

        #region Methods
        /// <summary>
        /// Initialize controls.
        /// </summary>
        /// <param name="withValues">With <see cref="Entity">Entity</see> property values</param>
        /// <returns>awaitable Task</returns>
        public async Task InitializeControls(bool withValues = false)
        {
            if (withValues)
            {
                if (Entity == null)
                {
                    BuilderDebug.Error("Entity was null.");
                    return;
                }

                foreach (Control control in UsingControls)
                {
                    PropertyInfo property = Entity.GetType().GetProperty(control.Name);
                    object value = null;
                    if (property != null)
                    {
                        value = property.GetValue(Entity);
                    }

                    if (control is ComponentBuilderTextBox componentBuilderTextBox)
                    {
                        await componentBuilderTextBox.Import();
                        componentBuilderTextBox.SetSelectedEntity(value);
                    }
                    else if (control is TextBox || control is RichTextBox || control is MaskedTextBox)
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
                    else if (control is IComponentBuilder componentBuilder)
                    {
                        await componentBuilder.Initialize(this.ImportLists[componentBuilder.EntityType]);
                    }
                }
            }
            else
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
                if (control is ComponentBuilderTextBox componentBuilderTextBox && componentBuilderTextBox.SelectedValue == null) result = false;
                else if ((control is TextBox || control is RichTextBox || control is MaskedTextBox) && string.IsNullOrEmpty(control.Text)) result = false;
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
                    propertyInfo.SetValue(Entity, componentBuilderTextBox.SelectedValue);
                }
                else if (control is TextBox || control is RichTextBox || control is MaskedTextBox)
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

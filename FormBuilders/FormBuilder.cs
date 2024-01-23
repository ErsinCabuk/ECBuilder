using ECBuilder.ComponentBuilders;
using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders
{
    [Serializable]
    public class FormBuilder : Form
    {
        public FormBuilder()
        {

        }

        #region Properties
        /// <summary>
        /// Event to run before the main load event. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> BeforeLoadEvent { get; set; }

        /// <summary>
        /// Event to run after the main load event. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> AfterLoadEvent { get; set; }

        /// <summary>
        /// Controls to be used in the form and included in the process.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Control> UsingControls { get; set; }

        /// <summary>
        /// Entity
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEntity Entity { get; set; }

        /// <summary>
        /// Imported lists. The key gives the type of the list and the value gives the list.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<Type, List<IEntity>> ImportLists { get; set; } = new Dictionary<Type, List<IEntity>>();

        /// <summary>
        ///
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Type> ImportListDefinitions { get; set; }

        /// <summary>
        /// Owner ComponentBuilder
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentBuilder ComponentBuilder { get; set; }
        #endregion

        #region Private Properties
        internal Func<Task> LoadEvent { get; set; }
        #endregion

        #region Events
        protected override async void OnLoad(EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            #region Import Lists
            if (ImportListDefinitions != null && ImportListDefinitions.Count > 0)
            {
                foreach (Type type in ImportListDefinitions)
                {
                    ImportLists.Add(type, await API.GetAll(type, 1));
                }
            }
            #endregion

            #region BeforeLoadEvent
            if (BeforeLoadEvent != null)
            {
                await BeforeLoadEvent();
            }
            #endregion

            #region Load Event
            UsingControls = this.Controls.Cast<Control>().Where(whereControl => (whereControl.Tag != null) && (whereControl.Tag.ToString().Split(',').Select(tag => tag.Trim()).Contains("use"))).ToList();
            UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

            if (LoadEvent != null)
            {
                await LoadEvent();
            }
            #endregion

            #region AfterLoadEvent
            if (AfterLoadEvent != null)
            {
                await AfterLoadEvent();
            }
            #endregion
        }
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

                if (control is TextBox || control is RichTextBox)
                {
                    propertyInfo.SetValue(Entity, control.Text.Trim());
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    propertyInfo.SetValue(Entity, dateTimePicker.Value);
                }
                else if (control is ComboBox comboBox)
                {
                    propertyInfo.SetValue(Entity, comboBox.SelectedValue);
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

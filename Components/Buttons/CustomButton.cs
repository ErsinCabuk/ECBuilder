using ECBuilder.FormBuilders;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.Buttons
{
    public class CustomButton : Button
    {
        public CustomButton() { }

        #region Properties
        /// <summary>
        /// Form containing the button.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FormBuilder ParentForm { get; set; }

        /// <summary>
        /// Method that will work before the button does its main method. If the method returns <see langword="true"/>, the button starts main method. If it returns <see langword="false"/>, not starts main method.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<bool> ControlClickEvent { get; set; }

        /// <summary>
        /// Method to be run before the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> BeforeRunClickEvent { get; set; }

        /// <summary>
        /// Method to be run after the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<int, Task> AfterRunClickEvent { get; set; }
        #endregion

        #region Parameters
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Func<Task> ClickEvent { get; set; }
        #endregion

        #region Events
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            ParentForm = (FormBuilder)this.FindForm();
        }

        protected override async void OnClick(EventArgs e)
        {
            if (ControlClickEvent != null)
            {
                bool controlResult = ControlClickEvent();
                if (!controlResult) return;
            }

            if (BeforeRunClickEvent != null)
            {
                await BeforeRunClickEvent();
            }

            await ClickEvent();

            if (AfterRunClickEvent != null)
            {
                await AfterRunClickEvent(Convert.ToInt32(ParentForm.Entity.GetType().GetProperty($"{ParentForm.Entity.GetType().Name}ID").GetValue(ParentForm.Entity)));
            }
        }
        #endregion
    }
}

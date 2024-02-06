using ECBuilder.FormBuilders.EntityFormBuilders;
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
        public EntityFormBuilder ParentForm { get; set; }

        /// <summary>
        /// Method that will work before the button does its main method. If the method returns <see langword="true"/>, the button starts main method. If it returns <see langword="false"/>, not starts main method.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<bool> ControlClickEvent;

        /// <summary>
        /// Method to be run before the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<Task> BeforeRunClickEvent;

        /// <summary>
        /// Method to be run after the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<int, Task> AfterRunClickEvent;
        #endregion

        #region Parameters
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal event Func<Task> ClickEvent;
        #endregion

        #region Events
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            ParentForm = (EntityFormBuilder)this.FindForm();
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

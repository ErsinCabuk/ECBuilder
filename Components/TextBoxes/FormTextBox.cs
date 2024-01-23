using ECBuilder.FormBuilders.EntityFormBuilders;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    public class FormTextBox : TextBox
    {
        public FormTextBox()
        {
            this.ReadOnly = true;
            this.Margin = new Padding(0, 0, 0, 0);
            this.Size = new System.Drawing.Size(300, 29);
            this.Text = DefaultText;
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string DefaultText { get; set; } = "Seç...";

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<bool> ControlClickEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> BeforeRunClickEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> AfterRunClickEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EntityFormBuilder FormBuilder { get; set; }
        #endregion

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



            if (AfterRunClickEvent != null)
            {
                await AfterRunClickEvent();
            }
        }
    }
}

using ECBuilder.FormBuilders;
using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Test;
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
        public Func<Task> BeforeFormOpening { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DialogResult, Task> FormClosingEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<FormTextBoxFormBuilder, Task> FormLoadingEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type FormBuilder { get; set; }
        #endregion

        protected override async void OnClick(EventArgs e)
        {
            if (!FormBuilder.IsSubclassOf(typeof(FormTextBoxFormBuilder)))
            {
                BuilderDebug.Error("'FormTextBox.FormBuilder' must be of type 'FormTextBoxFormBuilder'");
                return;
            }

            if (ControlClickEvent != null)
            {
                bool controlResult = ControlClickEvent();
                if (!controlResult) return;
            }

            if (BeforeFormOpening != null)
            {
                await BeforeFormOpening();
            }

            FormTextBoxFormBuilder formTextBoxFormBuilder = (FormTextBoxFormBuilder)Activator.CreateInstance(FormBuilder);

            if (FormLoadingEvent != null)
            {
                await FormLoadingEvent(formTextBoxFormBuilder);
            }

            DialogResult dialogResult = formTextBoxFormBuilder.ShowDialog();

            if (FormClosingEvent != null)
            {
                await FormClosingEvent(dialogResult);
            }
        }
    }
}

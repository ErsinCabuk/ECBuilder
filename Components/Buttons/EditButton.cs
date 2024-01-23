using System;

namespace ECBuilder.Components.Buttons
{
    /// <summary>
    /// Button that <see cref="DataAccess.API.Create">edit</see> the <see cref="Builders.FormBuilders.FormBuilder.Entity">Entity</see> in <see cref="Builders.FormBuilders.InfoFormBuilder">InfoFormBuilder</see>.
    /// </summary>
    public class EditButton : CustomButton
    {
        public EditButton()
        {
            ClickEvent += EditButton_ClickEvent;
        }

        #region Properties
        /// <summary>
        /// Edit text
        /// </summary>
        public string EditText { get; private set; }

        /// <summary>
        /// Save Text
        /// </summary>
        public string SaveText { get; set; } = "Kaydet";
        #endregion

        #region Events
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            EditText = Text;
        }


        private ButtonModes buttonMode = ButtonModes.Edit;
        private async System.Threading.Tasks.Task EditButton_ClickEvent()
        {
            if (buttonMode == ButtonModes.Edit)
            {
                this.BackColor = ECBuilderSettings.EditButtonSaveColor;
                this.Text = SaveText;

                buttonMode = ButtonModes.Save;
                ParentForm.UsingControls.ForEach(control =>
                {
                    if (!control.Tag.ToString().Contains("locked"))
                    {
                        control.Enabled = true;
                    }
                });
            }
            else if (buttonMode == ButtonModes.Save)
            {
                bool checkResult = ParentForm.CheckControls();
                if (!checkResult)
                {
                    if (!string.IsNullOrEmpty(ECBuilderSettings.CheckControlsText)) MessageBoxes.Error(ECBuilderSettings.CheckControlsText);
                    return;
                }

                ParentForm.SetProperties();

                await DataAccess.API.Update(ParentForm.Entity);

                this.BackColor = ECBuilderSettings.EditButtonSaveColor;
                this.Text = EditText;
                buttonMode = ButtonModes.Edit;

                ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        #endregion

        #region Enums
        private enum ButtonModes { Edit, Save }
        #endregion
    }
}

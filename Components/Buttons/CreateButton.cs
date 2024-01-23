namespace ECBuilder.Components.Buttons
{
    /// <summary>
    /// Button that <see cref="DataAccess.API.Create">creates</see> the <see cref="Builders.FormBuilders.FormBuilder.Entity">Entity</see> in <see cref="Builders.FormBuilders.CreateFormBuilder">CreateFormBuilder</see>.
    /// </summary>
    public class CreateButton : CustomButton
    {
        public CreateButton()
        {
            ClickEvent += CreateButton_ClickEvent;
        }

        private async System.Threading.Tasks.Task CreateButton_ClickEvent()
        {
            bool checkResult = ParentForm.CheckControls();
            if (!checkResult)
            {
                if (!string.IsNullOrEmpty(ECBuilderSettings.CheckControlsText)) MessageBoxes.Error(ECBuilderSettings.CheckControlsText);
                return;
            }


            ParentForm.SetProperties();
            ParentForm.SetEmptyProperties();

            await DataAccess.API.Create(ParentForm.Entity);

            ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}

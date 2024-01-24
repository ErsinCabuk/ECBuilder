namespace ECBuilder.Components.Buttons
{
    /// <summary>
    /// Button that <see cref="DataAccess.API.Delete(Interfaces.IEntity)">deletes</see> the <see cref="Builders.FormBuilders.FormBuilder.Entity">Entity</see> in <see cref="Builders.FormBuilders.InfoFormBuilder">InfoFormBuilder</see>.
    /// </summary>
    public class DeleteButton : CustomButton
    {
        public DeleteButton()
        {
            ClickEvent += DeleteButton_ClickEvent;
        }

        private async System.Threading.Tasks.Task DeleteButton_ClickEvent()
        {
            await DataAccess.API.Delete(ParentForm.Entity);

            ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}

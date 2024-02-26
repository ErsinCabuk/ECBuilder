namespace ECBuilder.Components.Buttons
{
    /// <summary>
    /// Button that <see cref="DataAccess.API.LogicalDelete(Interfaces.IEntity)">logical deletes</see> the <see cref="Builders.FormBuilders.FormBuilder.Entity">Entity</see> in <see cref="Builders.FormBuilders.InfoFormBuilder">InfoFormBuilder</see>.
    /// </summary>
    public class LogicalDeleteButton : EntityButton
    {
        public LogicalDeleteButton()
        {
            ClickEvent += LogicalDeleteButton_ClickEvent;
        }

        private async System.Threading.Tasks.Task LogicalDeleteButton_ClickEvent()
        {
            await DataAccess.API.LogicalDelete(ParentForm.Entity);

            ParentForm.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}

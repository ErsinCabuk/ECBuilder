namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// An entitys information form.
    /// </summary>
    [System.Serializable]
    public class InfoEntityFormBuilder : EntityFormBuilder
    {
        public InfoEntityFormBuilder()
        {
            LoadEvent += InfoFormBuilder_LoadEvent;
        }

        #region Event
        internal async System.Threading.Tasks.Task InfoFormBuilder_LoadEvent()
        {
            await this.InitializeControls(true);
        }
        #endregion
    }
}

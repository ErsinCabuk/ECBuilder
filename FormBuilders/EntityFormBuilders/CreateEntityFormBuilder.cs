namespace ECBuilder.FormBuilders.EntityFormBuilders
{
    /// <summary>
    /// Form that creates an entity.
    /// </summary>
    [System.Serializable]
    public class CreateEntityFormBuilder : EntityFormBuilder
    {
        public CreateEntityFormBuilder()
        {
            LoadEvent += CreateFormBuilder_LoadEvent;
        }

        private async System.Threading.Tasks.Task CreateFormBuilder_LoadEvent()
        {
            await this.InitializeControls();
        }
    }
}

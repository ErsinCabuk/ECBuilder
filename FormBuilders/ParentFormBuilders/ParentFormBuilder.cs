using System.Windows.Forms;

namespace ECBuilder.FormBuilders.ParentFormBuilders
{
    /// <summary>
    /// 
    /// </summary>
    public class ParentFormBuilder : FormBuilder
    {
        public ParentFormBuilder()
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Owner.DialogResult = DialogResult.OK;
        }
    }
}

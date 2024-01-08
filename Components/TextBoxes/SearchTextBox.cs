using ECBuilder.Builders.DataGridViewBuilders;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    /// <summary>
    /// <see cref="TextBox"/> that can be searched in <see cref="Builders.DataGridViewBuilders.DataGridViewBuilder">DataGridViewBuilder</see>.
    /// </summary>
    public class SearchTextBox : TextBox
    {
        public SearchTextBox()
        {

        }

        #region Properties
        /// <summary>
        /// <see cref="Builders.DataGridViewBuilders.DataGridViewBuilder">DataGridViewBuilder</see> for this.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewBuilder DataGridViewBuilder { get; set; }

        /// <summary>
        /// Search text
        /// </summary>
        public string SearchText { get; private set; }

        /// <summary>
        /// The property name to search for in the <see cref="Builders.DataGridViewBuilders.DataGridViewBuilder">DataGridViewBuilder</see>.
        /// </summary>
        public string SearchProperty { get; set; }
        #endregion

        #region Events
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SearchText = this.Text;
        }

        private bool searchMode = false;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!searchMode)
            {
                this.Text = "";
                searchMode = true;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (searchMode)
            {
                string searchProperty = string.IsNullOrEmpty(SearchProperty) ? $"{DataGridViewBuilder.EntityType.Name}Name" : SearchProperty;

                foreach (DataGridViewRow row in DataGridViewBuilder.Rows)
                {
                    if (row.Cells[searchProperty].Value.ToString().ToLower().Contains(this.Text.ToLower()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (string.IsNullOrEmpty(this.Text) && searchMode)
            {
                searchMode = false;
                this.Text = SearchText;
            }
        }
        #endregion
    }
}

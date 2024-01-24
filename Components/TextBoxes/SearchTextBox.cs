using ECBuilder.ComponentBuilders;
using ECBuilder.ComponentBuilders.DataGridViewBuilders;
using ECBuilder.ComponentBuilders.TreeViewBuilders;
using System;
using System.ComponentModel;
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
        public IComponentBuilder ComponentBuilder { get; set; }

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
            if (!searchMode)
            {
                this.Text = "";
                searchMode = true;
            }

            base.OnClick(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (searchMode)
            {
                string searchProperty = string.IsNullOrEmpty(SearchProperty) ? $"{ComponentBuilder.EntityType.Name}Name" : SearchProperty;

                if (ComponentBuilder is DataGridViewBuilder dataGridViewBuilder)
                {
                    foreach (DataGridViewRow row in dataGridViewBuilder.Rows)
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
                else if (ComponentBuilder is TreeViewBuilder treeViewBuilder)
                {

                }
            }

            base.OnTextChanged(e);
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

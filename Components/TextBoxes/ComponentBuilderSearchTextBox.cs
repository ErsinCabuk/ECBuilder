using ECBuilder.ComponentBuilders;
using ECBuilder.ComponentBuilders.DataGridViewBuilders;
using ECBuilder.ComponentBuilders.TreeViewBuilders;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentBuilderSearchTextBox : TextBox
    {
        public ComponentBuilderSearchTextBox()
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
                    SearchNodes(treeViewBuilder.Nodes);
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

        #region Methods
        private void SearchNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    node.BackColor = Color.Transparent;
                    node.ForeColor = SystemColors.ControlText;
                }
                else if (node.Text.ToString().ToLower().Contains(this.Text.ToLower()))
                {
                    node.BackColor = SystemColors.Highlight;
                    node.ForeColor = SystemColors.HighlightText;
                    node.Expand();
                }
                else
                {
                    node.BackColor = Color.Transparent;
                    node.ForeColor = SystemColors.ControlText;
                }

                SearchNodes(node.Nodes);
            }
        }
        #endregion
    }
}

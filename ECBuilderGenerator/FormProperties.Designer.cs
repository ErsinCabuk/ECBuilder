namespace ECBuilderGenerator
{
    partial class FormProperties
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStripDataGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxRepositing = new System.Windows.Forms.ToolStripTextBox();
            this.dataGridViewProperties = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialogGetFile = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStripDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete,
            this.toolStripTextBoxRepositing});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(161, 51);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemDelete.Text = "Sil";
            // 
            // toolStripTextBoxRepositing
            // 
            this.toolStripTextBoxRepositing.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxRepositing.Name = "toolStripTextBoxRepositing";
            this.toolStripTextBoxRepositing.Size = new System.Drawing.Size(100, 23);
            // 
            // dataGridViewProperties
            // 
            this.dataGridViewProperties.AllowDrop = true;
            this.dataGridViewProperties.AllowUserToResizeColumns = false;
            this.dataGridViewProperties.AllowUserToResizeRows = false;
            this.dataGridViewProperties.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewProperties.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.PropertyValue});
            this.dataGridViewProperties.EnableHeadersVisualStyles = false;
            this.dataGridViewProperties.Location = new System.Drawing.Point(10, 10);
            this.dataGridViewProperties.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewProperties.MultiSelect = false;
            this.dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewProperties.RowHeadersVisible = false;
            this.dataGridViewProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProperties.Size = new System.Drawing.Size(800, 400);
            this.dataGridViewProperties.TabIndex = 24;
            // 
            // PropertyName
            // 
            this.PropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PropertyName.DefaultCellStyle = dataGridViewCellStyle2;
            this.PropertyName.HeaderText = "Property Name";
            this.PropertyName.Name = "PropertyName";
            // 
            // PropertyValue
            // 
            this.PropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyValue.HeaderText = "Property Value";
            this.PropertyValue.Name = "PropertyValue";
            // 
            // folderBrowserDialogGetFile
            // 
            this.folderBrowserDialogGetFile.RootFolder = System.Environment.SpecialFolder.UserProfile;
            this.folderBrowserDialogGetFile.ShowNewFolderButton = false;
            // 
            // FormProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 422);
            this.Controls.Add(this.dataGridViewProperties);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProperties";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECBuilder Generator";
            this.Load += new System.EventHandler(this.FormProperties_Load);
            this.contextMenuStripDataGridView.ResumeLayout(false);
            this.contextMenuStripDataGridView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.DataGridView dataGridViewProperties;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRepositing;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGetFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyValue;
    }
}


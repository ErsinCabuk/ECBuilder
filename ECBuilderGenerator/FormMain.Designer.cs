namespace ECBuilderGenerator
{
    partial class FormMain
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
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.labelTitleServerName = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelTitleServerUsername = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelTitlePassword = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.labelTitleDatabase = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupBoxServerInfo = new System.Windows.Forms.GroupBox();
            this.buttonStartDesigner = new System.Windows.Forms.Button();
            this.labelTitleTable = new System.Windows.Forms.Label();
            this.comboBoxTables = new System.Windows.Forms.ComboBox();
            this.groupBoxFormInfo = new System.Windows.Forms.GroupBox();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.labelTitleNamespace = new System.Windows.Forms.Label();
            this.textBoxFormText = new System.Windows.Forms.TextBox();
            this.labelTitleFormText = new System.Windows.Forms.Label();
            this.textBoxFormName = new System.Windows.Forms.TextBox();
            this.labelTitleFormType = new System.Windows.Forms.Label();
            this.comboBoxFormType = new System.Windows.Forms.ComboBox();
            this.labelTitleFormName = new System.Windows.Forms.Label();
            this.dataGridViewControls = new System.Windows.Forms.DataGridView();
            this.ControlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LabelText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Required = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Locked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OtherProperties = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBoxDesigner = new System.Windows.Forms.GroupBox();
            this.buttonGetFile = new System.Windows.Forms.Button();
            this.buttonCreateDesigner = new System.Windows.Forms.Button();
            this.folderBrowserDialogGetFile = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStripDataGridView.SuspendLayout();
            this.groupBoxServerInfo.SuspendLayout();
            this.groupBoxFormInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControls)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripDataGridView
            // 
            this.contextMenuStripDataGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete,
            this.toolStripTextBoxRepositing});
            this.contextMenuStripDataGridView.Name = "contextMenuStripDataGridView";
            this.contextMenuStripDataGridView.Size = new System.Drawing.Size(161, 51);
            this.contextMenuStripDataGridView.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuStripDataGridView_Closing);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemDelete.Text = "Sil";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripTextBoxRepositing
            // 
            this.toolStripTextBoxRepositing.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxRepositing.Name = "toolStripTextBoxRepositing";
            this.toolStripTextBoxRepositing.Size = new System.Drawing.Size(100, 23);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(115, 32);
            this.textBoxServerName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(200, 29);
            this.textBoxServerName.TabIndex = 8;
            this.textBoxServerName.Text = "212.22.69.114";
            // 
            // labelTitleServerName
            // 
            this.labelTitleServerName.AutoSize = true;
            this.labelTitleServerName.Location = new System.Drawing.Point(10, 35);
            this.labelTitleServerName.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleServerName.Name = "labelTitleServerName";
            this.labelTitleServerName.Size = new System.Drawing.Size(55, 21);
            this.labelTitleServerName.TabIndex = 7;
            this.labelTitleServerName.Text = "Name:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(115, 71);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(200, 29);
            this.textBoxUsername.TabIndex = 10;
            this.textBoxUsername.Text = "ersinDB";
            // 
            // labelTitleServerUsername
            // 
            this.labelTitleServerUsername.AutoSize = true;
            this.labelTitleServerUsername.Location = new System.Drawing.Point(10, 74);
            this.labelTitleServerUsername.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleServerUsername.Name = "labelTitleServerUsername";
            this.labelTitleServerUsername.Size = new System.Drawing.Size(84, 21);
            this.labelTitleServerUsername.TabIndex = 9;
            this.labelTitleServerUsername.Text = "Username:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(115, 110);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(200, 29);
            this.textBoxPassword.TabIndex = 12;
            this.textBoxPassword.Text = "Ersin429253318";
            // 
            // labelTitlePassword
            // 
            this.labelTitlePassword.AutoSize = true;
            this.labelTitlePassword.Location = new System.Drawing.Point(10, 113);
            this.labelTitlePassword.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitlePassword.Name = "labelTitlePassword";
            this.labelTitlePassword.Size = new System.Drawing.Size(79, 21);
            this.labelTitlePassword.TabIndex = 11;
            this.labelTitlePassword.Text = "Password:";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(115, 149);
            this.textBoxDatabase.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(200, 29);
            this.textBoxDatabase.TabIndex = 14;
            // 
            // labelTitleDatabase
            // 
            this.labelTitleDatabase.AutoSize = true;
            this.labelTitleDatabase.Location = new System.Drawing.Point(10, 153);
            this.labelTitleDatabase.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleDatabase.Name = "labelTitleDatabase";
            this.labelTitleDatabase.Size = new System.Drawing.Size(77, 21);
            this.labelTitleDatabase.TabIndex = 13;
            this.labelTitleDatabase.Text = "Database:";
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoSize = true;
            this.buttonConnect.Location = new System.Drawing.Point(212, 188);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(103, 31);
            this.buttonConnect.TabIndex = 15;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupBoxServerInfo
            // 
            this.groupBoxServerInfo.Controls.Add(this.buttonStartDesigner);
            this.groupBoxServerInfo.Controls.Add(this.labelTitleServerName);
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerName);
            this.groupBoxServerInfo.Controls.Add(this.labelTitleServerUsername);
            this.groupBoxServerInfo.Controls.Add(this.textBoxUsername);
            this.groupBoxServerInfo.Controls.Add(this.labelTitlePassword);
            this.groupBoxServerInfo.Controls.Add(this.buttonConnect);
            this.groupBoxServerInfo.Controls.Add(this.textBoxPassword);
            this.groupBoxServerInfo.Controls.Add(this.labelTitleTable);
            this.groupBoxServerInfo.Controls.Add(this.labelTitleDatabase);
            this.groupBoxServerInfo.Controls.Add(this.comboBoxTables);
            this.groupBoxServerInfo.Controls.Add(this.textBoxDatabase);
            this.groupBoxServerInfo.Location = new System.Drawing.Point(10, 10);
            this.groupBoxServerInfo.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxServerInfo.Name = "groupBoxServerInfo";
            this.groupBoxServerInfo.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxServerInfo.Size = new System.Drawing.Size(325, 309);
            this.groupBoxServerInfo.TabIndex = 22;
            this.groupBoxServerInfo.TabStop = false;
            this.groupBoxServerInfo.Text = "Server Config";
            // 
            // buttonStartDesigner
            // 
            this.buttonStartDesigner.AutoSize = true;
            this.buttonStartDesigner.Enabled = false;
            this.buttonStartDesigner.Location = new System.Drawing.Point(212, 268);
            this.buttonStartDesigner.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonStartDesigner.Name = "buttonStartDesigner";
            this.buttonStartDesigner.Size = new System.Drawing.Size(103, 31);
            this.buttonStartDesigner.TabIndex = 20;
            this.buttonStartDesigner.Text = "Set Table";
            this.buttonStartDesigner.UseVisualStyleBackColor = true;
            // 
            // labelTitleTable
            // 
            this.labelTitleTable.AutoSize = true;
            this.labelTitleTable.Location = new System.Drawing.Point(10, 232);
            this.labelTitleTable.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleTable.Name = "labelTitleTable";
            this.labelTitleTable.Size = new System.Drawing.Size(48, 21);
            this.labelTitleTable.TabIndex = 19;
            this.labelTitleTable.Text = "Table:";
            // 
            // comboBoxTables
            // 
            this.comboBoxTables.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxTables.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxTables.Enabled = false;
            this.comboBoxTables.FormattingEnabled = true;
            this.comboBoxTables.Location = new System.Drawing.Point(115, 229);
            this.comboBoxTables.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.comboBoxTables.Name = "comboBoxTables";
            this.comboBoxTables.Size = new System.Drawing.Size(200, 29);
            this.comboBoxTables.TabIndex = 18;
            // 
            // groupBoxFormInfo
            // 
            this.groupBoxFormInfo.Controls.Add(this.textBoxNamespace);
            this.groupBoxFormInfo.Controls.Add(this.labelTitleNamespace);
            this.groupBoxFormInfo.Controls.Add(this.textBoxFormText);
            this.groupBoxFormInfo.Controls.Add(this.labelTitleFormText);
            this.groupBoxFormInfo.Controls.Add(this.textBoxFormName);
            this.groupBoxFormInfo.Controls.Add(this.labelTitleFormType);
            this.groupBoxFormInfo.Controls.Add(this.comboBoxFormType);
            this.groupBoxFormInfo.Controls.Add(this.labelTitleFormName);
            this.groupBoxFormInfo.Location = new System.Drawing.Point(10, 329);
            this.groupBoxFormInfo.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.groupBoxFormInfo.Name = "groupBoxFormInfo";
            this.groupBoxFormInfo.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxFormInfo.Size = new System.Drawing.Size(325, 189);
            this.groupBoxFormInfo.TabIndex = 23;
            this.groupBoxFormInfo.TabStop = false;
            this.groupBoxFormInfo.Text = "Form Config";
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(115, 149);
            this.textBoxNamespace.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(200, 29);
            this.textBoxNamespace.TabIndex = 26;
            // 
            // labelTitleNamespace
            // 
            this.labelTitleNamespace.AutoSize = true;
            this.labelTitleNamespace.Location = new System.Drawing.Point(9, 152);
            this.labelTitleNamespace.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleNamespace.Name = "labelTitleNamespace";
            this.labelTitleNamespace.Size = new System.Drawing.Size(94, 21);
            this.labelTitleNamespace.TabIndex = 27;
            this.labelTitleNamespace.Text = "Namespace:";
            // 
            // textBoxFormText
            // 
            this.textBoxFormText.Location = new System.Drawing.Point(115, 71);
            this.textBoxFormText.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxFormText.Name = "textBoxFormText";
            this.textBoxFormText.Size = new System.Drawing.Size(200, 29);
            this.textBoxFormText.TabIndex = 24;
            // 
            // labelTitleFormText
            // 
            this.labelTitleFormText.AutoSize = true;
            this.labelTitleFormText.Location = new System.Drawing.Point(9, 74);
            this.labelTitleFormText.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleFormText.Name = "labelTitleFormText";
            this.labelTitleFormText.Size = new System.Drawing.Size(80, 21);
            this.labelTitleFormText.TabIndex = 25;
            this.labelTitleFormText.Text = "Form Text:";
            // 
            // textBoxFormName
            // 
            this.textBoxFormName.Location = new System.Drawing.Point(115, 32);
            this.textBoxFormName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxFormName.Name = "textBoxFormName";
            this.textBoxFormName.Size = new System.Drawing.Size(200, 29);
            this.textBoxFormName.TabIndex = 16;
            // 
            // labelTitleFormType
            // 
            this.labelTitleFormType.AutoSize = true;
            this.labelTitleFormType.Location = new System.Drawing.Point(9, 112);
            this.labelTitleFormType.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleFormType.Name = "labelTitleFormType";
            this.labelTitleFormType.Size = new System.Drawing.Size(86, 21);
            this.labelTitleFormType.TabIndex = 23;
            this.labelTitleFormType.Text = "Form Type:";
            // 
            // comboBoxFormType
            // 
            this.comboBoxFormType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxFormType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFormType.FormattingEnabled = true;
            this.comboBoxFormType.Location = new System.Drawing.Point(115, 110);
            this.comboBoxFormType.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.comboBoxFormType.Name = "comboBoxFormType";
            this.comboBoxFormType.Size = new System.Drawing.Size(200, 29);
            this.comboBoxFormType.TabIndex = 22;
            // 
            // labelTitleFormName
            // 
            this.labelTitleFormName.AutoSize = true;
            this.labelTitleFormName.Location = new System.Drawing.Point(9, 35);
            this.labelTitleFormName.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleFormName.Name = "labelTitleFormName";
            this.labelTitleFormName.Size = new System.Drawing.Size(96, 21);
            this.labelTitleFormName.TabIndex = 21;
            this.labelTitleFormName.Text = "Form Name:";
            // 
            // dataGridViewControls
            // 
            this.dataGridViewControls.AllowDrop = true;
            this.dataGridViewControls.AllowUserToResizeColumns = false;
            this.dataGridViewControls.AllowUserToResizeRows = false;
            this.dataGridViewControls.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewControls.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewControls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewControls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ControlName,
            this.ControlType,
            this.LabelText,
            this.Required,
            this.Locked,
            this.OtherProperties});
            this.dataGridViewControls.EnableHeadersVisualStyles = false;
            this.dataGridViewControls.Location = new System.Drawing.Point(345, 20);
            this.dataGridViewControls.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.dataGridViewControls.MultiSelect = false;
            this.dataGridViewControls.Name = "dataGridViewControls";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewControls.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewControls.RowHeadersVisible = false;
            this.dataGridViewControls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewControls.Size = new System.Drawing.Size(829, 457);
            this.dataGridViewControls.TabIndex = 24;
            this.dataGridViewControls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewControls_CellContentClick);
            this.dataGridViewControls.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewControls_RowsAdded);
            this.dataGridViewControls.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewControls_MouseClick);
            // 
            // ControlName
            // 
            this.ControlName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ControlName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ControlName.HeaderText = "Control Name";
            this.ControlName.Name = "ControlName";
            // 
            // ControlType
            // 
            this.ControlType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ControlType.HeaderText = "Control Type";
            this.ControlType.Name = "ControlType";
            // 
            // LabelText
            // 
            this.LabelText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LabelText.HeaderText = "Label";
            this.LabelText.Name = "LabelText";
            // 
            // Required
            // 
            this.Required.HeaderText = "Required";
            this.Required.Name = "Required";
            // 
            // Locked
            // 
            this.Locked.HeaderText = "Locked";
            this.Locked.Name = "Locked";
            // 
            // OtherProperties
            // 
            this.OtherProperties.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.OtherProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OtherProperties.HeaderText = "Other";
            this.OtherProperties.Name = "OtherProperties";
            this.OtherProperties.Text = "Other";
            this.OtherProperties.UseColumnTextForButtonValue = true;
            this.OtherProperties.Width = 55;
            // 
            // groupBoxDesigner
            // 
            this.groupBoxDesigner.Location = new System.Drawing.Point(10, 528);
            this.groupBoxDesigner.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.groupBoxDesigner.Name = "groupBoxDesigner";
            this.groupBoxDesigner.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxDesigner.Size = new System.Drawing.Size(1164, 301);
            this.groupBoxDesigner.TabIndex = 2;
            this.groupBoxDesigner.TabStop = false;
            this.groupBoxDesigner.Text = "Designer";
            // 
            // buttonGetFile
            // 
            this.buttonGetFile.AutoSize = true;
            this.buttonGetFile.Location = new System.Drawing.Point(974, 839);
            this.buttonGetFile.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonGetFile.Name = "buttonGetFile";
            this.buttonGetFile.Size = new System.Drawing.Size(200, 31);
            this.buttonGetFile.TabIndex = 26;
            this.buttonGetFile.Text = "Get File";
            this.buttonGetFile.UseVisualStyleBackColor = true;
            this.buttonGetFile.Click += new System.EventHandler(this.buttonGetFile_Click);
            // 
            // buttonCreateDesigner
            // 
            this.buttonCreateDesigner.AutoSize = true;
            this.buttonCreateDesigner.Location = new System.Drawing.Point(974, 487);
            this.buttonCreateDesigner.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonCreateDesigner.Name = "buttonCreateDesigner";
            this.buttonCreateDesigner.Size = new System.Drawing.Size(200, 31);
            this.buttonCreateDesigner.TabIndex = 27;
            this.buttonCreateDesigner.Text = "Create Designer";
            this.buttonCreateDesigner.UseVisualStyleBackColor = true;
            this.buttonCreateDesigner.Click += new System.EventHandler(this.buttonCreateDesigner_Click);
            // 
            // folderBrowserDialogGetFile
            // 
            this.folderBrowserDialogGetFile.RootFolder = System.Environment.SpecialFolder.UserProfile;
            this.folderBrowserDialogGetFile.ShowNewFolderButton = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 879);
            this.Controls.Add(this.buttonCreateDesigner);
            this.Controls.Add(this.buttonGetFile);
            this.Controls.Add(this.groupBoxDesigner);
            this.Controls.Add(this.dataGridViewControls);
            this.Controls.Add(this.groupBoxFormInfo);
            this.Controls.Add(this.groupBoxServerInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECBuilder Generator";
            this.Load += new System.EventHandler(this.FormMainV2_Load);
            this.contextMenuStripDataGridView.ResumeLayout(false);
            this.contextMenuStripDataGridView.PerformLayout();
            this.groupBoxServerInfo.ResumeLayout(false);
            this.groupBoxServerInfo.PerformLayout();
            this.groupBoxFormInfo.ResumeLayout(false);
            this.groupBoxFormInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelTitleServerName;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelTitleServerUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelTitlePassword;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label labelTitleDatabase;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGridView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.GroupBox groupBoxServerInfo;
        private System.Windows.Forms.GroupBox groupBoxFormInfo;
        private System.Windows.Forms.Label labelTitleTable;
        private System.Windows.Forms.ComboBox comboBoxTables;
        private System.Windows.Forms.Label labelTitleFormType;
        private System.Windows.Forms.ComboBox comboBoxFormType;
        private System.Windows.Forms.Label labelTitleFormName;
        private System.Windows.Forms.DataGridView dataGridViewControls;
        private System.Windows.Forms.TextBox textBoxFormName;
        private System.Windows.Forms.GroupBox groupBoxDesigner;
        private System.Windows.Forms.TextBox textBoxFormText;
        private System.Windows.Forms.Label labelTitleFormText;
        private System.Windows.Forms.Button buttonGetFile;
        private System.Windows.Forms.Button buttonCreateDesigner;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRepositing;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Button buttonStartDesigner;
        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.Label labelTitleNamespace;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGetFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ControlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Required;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Locked;
        private System.Windows.Forms.DataGridViewButtonColumn OtherProperties;
    }
}


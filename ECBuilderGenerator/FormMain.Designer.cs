namespace ECBuilderGenerator
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelTitleFormName = new System.Windows.Forms.Label();
            this.textBoxFormName = new System.Windows.Forms.TextBox();
            this.dataGridViewControls = new System.Windows.Forms.DataGridView();
            this.ColumnControlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnControlType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnRequired = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnLocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxTables = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControls)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitleFormName
            // 
            this.labelTitleFormName.AutoSize = true;
            this.labelTitleFormName.Location = new System.Drawing.Point(318, 13);
            this.labelTitleFormName.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.labelTitleFormName.Name = "labelTitleFormName";
            this.labelTitleFormName.Size = new System.Drawing.Size(77, 21);
            this.labelTitleFormName.TabIndex = 3;
            this.labelTitleFormName.Text = "Form Adı:";
            // 
            // textBoxFormName
            // 
            this.textBoxFormName.Location = new System.Drawing.Point(405, 10);
            this.textBoxFormName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxFormName.Name = "textBoxFormName";
            this.textBoxFormName.Size = new System.Drawing.Size(617, 29);
            this.textBoxFormName.TabIndex = 4;
            // 
            // dataGridViewControls
            // 
            this.dataGridViewControls.AllowUserToOrderColumns = true;
            this.dataGridViewControls.AllowUserToResizeColumns = false;
            this.dataGridViewControls.AllowUserToResizeRows = false;
            this.dataGridViewControls.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewControls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewControls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnControlName,
            this.ColumnControlType,
            this.ColumnRequired,
            this.ColumnLocked});
            this.dataGridViewControls.Location = new System.Drawing.Point(322, 49);
            this.dataGridViewControls.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.dataGridViewControls.Name = "dataGridViewControls";
            this.dataGridViewControls.RowHeadersVisible = false;
            this.dataGridViewControls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewControls.Size = new System.Drawing.Size(700, 300);
            this.dataGridViewControls.TabIndex = 5;
            // 
            // ColumnControlName
            // 
            this.ColumnControlName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnControlName.HeaderText = "Control Name";
            this.ColumnControlName.Name = "ColumnControlName";
            // 
            // ColumnControlType
            // 
            this.ColumnControlType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnControlType.HeaderText = "Control Type";
            this.ColumnControlType.Name = "ColumnControlType";
            // 
            // ColumnRequired
            // 
            this.ColumnRequired.HeaderText = "Required";
            this.ColumnRequired.Name = "ColumnRequired";
            // 
            // ColumnLocked
            // 
            this.ColumnLocked.HeaderText = "Locked";
            this.ColumnLocked.Name = "ColumnLocked";
            // 
            // buttonStart
            // 
            this.buttonStart.AutoSize = true;
            this.buttonStart.Location = new System.Drawing.Point(872, 359);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(150, 31);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start Design";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(112, 10);
            this.textBoxServerName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(200, 29);
            this.textBoxServerName.TabIndex = 8;
            this.textBoxServerName.Text = "212.22.69.114";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sunucu Adı:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(112, 49);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(200, 29);
            this.textBoxUsername.TabIndex = 10;
            this.textBoxUsername.Text = "ersinDB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Kullanıcı:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(112, 88);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(200, 29);
            this.textBoxPassword.TabIndex = 12;
            this.textBoxPassword.Text = "Ersin429253318";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "Şifre:";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(112, 127);
            this.textBoxDatabase.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(200, 29);
            this.textBoxDatabase.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 130);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Veritabanı:";
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoSize = true;
            this.buttonConnect.Location = new System.Drawing.Point(162, 205);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(150, 31);
            this.buttonConnect.TabIndex = 15;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxTables
            // 
            this.comboBoxTables.Enabled = false;
            this.comboBoxTables.FormattingEnabled = true;
            this.comboBoxTables.Location = new System.Drawing.Point(112, 166);
            this.comboBoxTables.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.comboBoxTables.Name = "comboBoxTables";
            this.comboBoxTables.Size = new System.Drawing.Size(200, 29);
            this.comboBoxTables.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 21);
            this.label5.TabIndex = 17;
            this.label5.Text = "Tablo:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(14, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 399);
            this.panel1.TabIndex = 18;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 805);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxTables);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxServerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.dataGridViewControls);
            this.Controls.Add(this.textBoxFormName);
            this.Controls.Add(this.labelTitleFormName);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECBuilder Generator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTitleFormName;
        private System.Windows.Forms.TextBox textBoxFormName;
        private System.Windows.Forms.DataGridView dataGridViewControls;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxTables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnControlName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnControlType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnRequired;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnLocked;
        private System.Windows.Forms.Panel panel1;
    }
}


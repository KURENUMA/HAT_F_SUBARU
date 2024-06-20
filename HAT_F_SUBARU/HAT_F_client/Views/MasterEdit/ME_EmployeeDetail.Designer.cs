namespace HatFClient.Views.MasterEdit
{
    partial class ME_EmployeeDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clbSelectedRole = new System.Windows.Forms.CheckedListBox();
            this.btnResetRoles = new System.Windows.Forms.Button();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpKana = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmpTag = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbDeptCode = new System.Windows.Forms.ComboBox();
            this.chkDeleted = new System.Windows.Forms.CheckBox();
            this.btnChkDuplicate = new System.Windows.Forms.Button();
            this.cmbOccuCode = new System.Windows.Forms.ComboBox();
            this.cmbTitle = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "社員番号*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(481, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "割当権限";
            // 
            // clbSelectedRole
            // 
            this.clbSelectedRole.FormattingEnabled = true;
            this.clbSelectedRole.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.clbSelectedRole.Items.AddRange(new object[] {
            "受発注機能",
            "倉庫機能",
            "○○ワークフロー承認",
            "××ワークフロー承認",
            "マスター編集"});
            this.clbSelectedRole.Location = new System.Drawing.Point(484, 99);
            this.clbSelectedRole.Margin = new System.Windows.Forms.Padding(4);
            this.clbSelectedRole.Name = "clbSelectedRole";
            this.clbSelectedRole.Size = new System.Drawing.Size(254, 238);
            this.clbSelectedRole.TabIndex = 22;
            // 
            // btnResetRoles
            // 
            this.btnResetRoles.Location = new System.Drawing.Point(558, 62);
            this.btnResetRoles.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetRoles.Name = "btnResetRoles";
            this.btnResetRoles.Size = new System.Drawing.Size(180, 29);
            this.btnResetRoles.TabIndex = 23;
            this.btnResetRoles.Text = "役職標準にリセット";
            this.btnResetRoles.UseVisualStyleBackColor = true;
            this.btnResetRoles.Click += new System.EventHandler(this.btnResetRoles_Click);
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEmpCode.Location = new System.Drawing.Point(125, 69);
            this.txtEmpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpCode.MaxLength = 10;
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Size = new System.Drawing.Size(200, 23);
            this.txtEmpCode.TabIndex = 1;
            this.txtEmpCode.Text = "0001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "社員名*";
            // 
            // txtEmpName
            // 
            this.txtEmpName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtEmpName.Location = new System.Drawing.Point(125, 100);
            this.txtEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpName.MaxLength = 20;
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(200, 23);
            this.txtEmpName.TabIndex = 4;
            this.txtEmpName.Text = "山田　太郎";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 135);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "社員名カナ";
            // 
            // txtEmpKana
            // 
            this.txtEmpKana.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtEmpKana.Location = new System.Drawing.Point(125, 131);
            this.txtEmpKana.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpKana.MaxLength = 40;
            this.txtEmpKana.Name = "txtEmpKana";
            this.txtEmpKana.Size = new System.Drawing.Size(200, 23);
            this.txtEmpKana.TabIndex = 6;
            this.txtEmpKana.Text = "ヤマダ　タロウ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "社員指定タグ";
            // 
            // txtEmpTag
            // 
            this.txtEmpTag.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEmpTag.Location = new System.Drawing.Point(125, 162);
            this.txtEmpTag.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpTag.MaxLength = 2;
            this.txtEmpTag.Name = "txtEmpTag";
            this.txtEmpTag.Size = new System.Drawing.Size(200, 23);
            this.txtEmpTag.TabIndex = 8;
            this.txtEmpTag.Text = "Y";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(484, 404);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(123, 29);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(615, 404);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 29);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTel
            // 
            this.txtTel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTel.Location = new System.Drawing.Point(125, 193);
            this.txtTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtTel.MaxLength = 13;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(200, 23);
            this.txtTel.TabIndex = 10;
            this.txtTel.Text = "03-9999-9999";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 197);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "電話番号";
            // 
            // txtFax
            // 
            this.txtFax.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFax.Location = new System.Drawing.Point(125, 224);
            this.txtFax.Margin = new System.Windows.Forms.Padding(4);
            this.txtFax.MaxLength = 13;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(200, 23);
            this.txtFax.TabIndex = 12;
            this.txtFax.Text = "03-8888-8888";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 228);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "FAX番号";
            // 
            // txtEmail
            // 
            this.txtEmail.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEmail.Location = new System.Drawing.Point(125, 255);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.MaxLength = 320;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 14;
            this.txtEmail.Text = "tyamada@example.jp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 259);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "メールアドレス";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 288);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "部門*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 317);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "職種*";
            // 
            // cmbDeptCode
            // 
            this.cmbDeptCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeptCode.FormattingEnabled = true;
            this.cmbDeptCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbDeptCode.Location = new System.Drawing.Point(125, 285);
            this.cmbDeptCode.Name = "cmbDeptCode";
            this.cmbDeptCode.Size = new System.Drawing.Size(200, 23);
            this.cmbDeptCode.TabIndex = 16;
            // 
            // chkDeleted
            // 
            this.chkDeleted.AutoSize = true;
            this.chkDeleted.Location = new System.Drawing.Point(27, 23);
            this.chkDeleted.Name = "chkDeleted";
            this.chkDeleted.Size = new System.Drawing.Size(113, 19);
            this.chkDeleted.TabIndex = 26;
            this.chkDeleted.Text = "この社員を無効化";
            this.chkDeleted.UseVisualStyleBackColor = true;
            // 
            // btnChkDuplicate
            // 
            this.btnChkDuplicate.Location = new System.Drawing.Point(333, 63);
            this.btnChkDuplicate.Margin = new System.Windows.Forms.Padding(4);
            this.btnChkDuplicate.Name = "btnChkDuplicate";
            this.btnChkDuplicate.Size = new System.Drawing.Size(76, 29);
            this.btnChkDuplicate.TabIndex = 2;
            this.btnChkDuplicate.Text = "重複確認";
            this.btnChkDuplicate.UseVisualStyleBackColor = true;
            this.btnChkDuplicate.Click += new System.EventHandler(this.btnChkDuplicate_Click);
            // 
            // cmbOccuCode
            // 
            this.cmbOccuCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOccuCode.FormattingEnabled = true;
            this.cmbOccuCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbOccuCode.Items.AddRange(new object[] {
            "正社員",
            "正社員試用期間",
            "契約社員",
            "アルバイト・パート"});
            this.cmbOccuCode.Location = new System.Drawing.Point(125, 314);
            this.cmbOccuCode.Name = "cmbOccuCode";
            this.cmbOccuCode.Size = new System.Drawing.Size(200, 23);
            this.cmbOccuCode.TabIndex = 18;
            // 
            // cmbTitle
            // 
            this.cmbTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTitle.FormattingEnabled = true;
            this.cmbTitle.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbTitle.Items.AddRange(new object[] {
            "社長",
            "本部長",
            "副本部長",
            "部長",
            "課長",
            "係長",
            "センター長",
            "担当部長",
            "支配人",
            "主任",
            "担当",
            "正社員"});
            this.cmbTitle.Location = new System.Drawing.Point(125, 343);
            this.cmbTitle.Name = "cmbTitle";
            this.cmbTitle.Size = new System.Drawing.Size(200, 23);
            this.cmbTitle.TabIndex = 20;
            this.cmbTitle.SelectedIndexChanged += new System.EventHandler(this.cboTitle_SelectedIndexChanged);
            this.cmbTitle.SelectionChangeCommitted += new System.EventHandler(this.cboTitle_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 346);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "役職*";
            // 
            // ME_EmployeeDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(773, 456);
            this.Controls.Add(this.cmbTitle);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbOccuCode);
            this.Controls.Add(this.btnChkDuplicate);
            this.Controls.Add(this.chkDeleted);
            this.Controls.Add(this.cmbDeptCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtEmpTag);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmpKana);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmpName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmpCode);
            this.Controls.Add(this.btnResetRoles);
            this.Controls.Add(this.clbSelectedRole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ME_EmployeeDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "社員詳細";
            this.Load += new System.EventHandler(this.ME_EmployeeDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbSelectedRole;
        private System.Windows.Forms.Button btnResetRoles;
        private System.Windows.Forms.TextBox txtEmpCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmpKana;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmpTag;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbDeptCode;
        private System.Windows.Forms.CheckBox chkDeleted;
        private System.Windows.Forms.Button btnChkDuplicate;
        private System.Windows.Forms.ComboBox cmbOccuCode;
        private System.Windows.Forms.ComboBox cmbTitle;
        private System.Windows.Forms.Label label11;
    }
}
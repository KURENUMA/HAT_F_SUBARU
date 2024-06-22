namespace HatFClient.Views.MasterEdit
{
    partial class ME_CustomersUserMstDetail
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
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustUserName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDeleted = new System.Windows.Forms.CheckBox();
            this.btnSelectCustomer = new System.Windows.Forms.Button();
            this.btnCheckDuplicate = new System.Windows.Forms.Button();
            this.txtCustUserEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCustUserCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2001;
            this.label1.Text = "顧客*";
            // 
            // txtCustName
            // 
            this.txtCustName.Location = new System.Drawing.Point(141, 70);
            this.txtCustName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.ReadOnly = true;
            this.txtCustName.Size = new System.Drawing.Size(200, 23);
            this.txtCustName.TabIndex = 2002;
            this.txtCustName.Text = "○○商事㈱ (000303)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 135);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 2041;
            this.label4.Text = "担当者(キーマン)名*";
            // 
            // txtCustUserName
            // 
            this.txtCustUserName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCustUserName.Location = new System.Drawing.Point(141, 132);
            this.txtCustUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustUserName.MaxLength = 40;
            this.txtCustUserName.Name = "txtCustUserName";
            this.txtCustUserName.Size = new System.Drawing.Size(200, 23);
            this.txtCustUserName.TabIndex = 2042;
            this.txtCustUserName.Text = "山田太郎";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(170, 228);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(123, 29);
            this.btnOK.TabIndex = 5001;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(301, 228);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 29);
            this.btnCancel.TabIndex = 5002;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkDeleted
            // 
            this.chkDeleted.AutoSize = true;
            this.chkDeleted.Location = new System.Drawing.Point(27, 23);
            this.chkDeleted.Name = "chkDeleted";
            this.chkDeleted.Size = new System.Drawing.Size(125, 19);
            this.chkDeleted.TabIndex = 1001;
            this.chkDeleted.Text = "この担当者を無効化";
            this.chkDeleted.UseVisualStyleBackColor = true;
            // 
            // btnSelectCustomer
            // 
            this.btnSelectCustomer.Location = new System.Drawing.Point(349, 66);
            this.btnSelectCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectCustomer.Name = "btnSelectCustomer";
            this.btnSelectCustomer.Size = new System.Drawing.Size(76, 29);
            this.btnSelectCustomer.TabIndex = 2003;
            this.btnSelectCustomer.Text = "顧客選択";
            this.btnSelectCustomer.UseVisualStyleBackColor = true;
            this.btnSelectCustomer.Click += new System.EventHandler(this.btnSelectCustomer_Click);
            // 
            // btnCheckDuplicate
            // 
            this.btnCheckDuplicate.Location = new System.Drawing.Point(349, 97);
            this.btnCheckDuplicate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckDuplicate.Name = "btnCheckDuplicate";
            this.btnCheckDuplicate.Size = new System.Drawing.Size(76, 29);
            this.btnCheckDuplicate.TabIndex = 2123;
            this.btnCheckDuplicate.Text = "重複確認";
            this.btnCheckDuplicate.UseVisualStyleBackColor = true;
            this.btnCheckDuplicate.Click += new System.EventHandler(this.btnCheckDuplicate_Click);
            // 
            // txtCustUserEmail
            // 
            this.txtCustUserEmail.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCustUserEmail.Location = new System.Drawing.Point(141, 164);
            this.txtCustUserEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustUserEmail.MaxLength = 40;
            this.txtCustUserEmail.Name = "txtCustUserEmail";
            this.txtCustUserEmail.Size = new System.Drawing.Size(200, 23);
            this.txtCustUserEmail.TabIndex = 2052;
            this.txtCustUserEmail.Text = "××××";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 167);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 2051;
            this.label9.Text = "メールアドレス";
            // 
            // txtCustUserCode
            // 
            this.txtCustUserCode.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCustUserCode.Location = new System.Drawing.Point(141, 101);
            this.txtCustUserCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustUserCode.MaxLength = 3;
            this.txtCustUserCode.Name = "txtCustUserCode";
            this.txtCustUserCode.Size = new System.Drawing.Size(200, 23);
            this.txtCustUserCode.TabIndex = 2022;
            this.txtCustUserCode.Text = "123";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 2022;
            this.label2.Text = "担当者(キーマン)CD*";
            // 
            // ME_CustomersUserMstDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(449, 270);
            this.Controls.Add(this.txtCustUserCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCustUserEmail);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCheckDuplicate);
            this.Controls.Add(this.btnSelectCustomer);
            this.Controls.Add(this.chkDeleted);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCustUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ME_CustomersUserMstDetail";
            this.Text = "顧客担当者詳細 (キーマン)";
            this.Load += new System.EventHandler(this.ME_EmployeeDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCustUserName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDeleted;
        private System.Windows.Forms.Button btnSelectCustomer;
        private System.Windows.Forms.Button btnCheckDuplicate;
        private System.Windows.Forms.TextBox txtCustUserEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCustUserCode;
        private System.Windows.Forms.Label label2;
    }
}
namespace HatFClient.Views.MasterEdit
{
    partial class ME_DestinaitonsMstDetail
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
            this.txtDistName1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkDeleted = new System.Windows.Forms.CheckBox();
            this.btnSelectCustomer = new System.Windows.Forms.Button();
            this.btnCheckDuplicate = new System.Windows.Forms.Button();
            this.txtDestFax = new System.Windows.Forms.TextBox();
            this.txtDestTel = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDistName2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGenbaCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustUserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.label4.Location = new System.Drawing.Point(24, 166);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 15);
            this.label4.TabIndex = 2041;
            this.label4.Text = "出荷先名１*";
            // 
            // txtDistName1
            // 
            this.txtDistName1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDistName1.Location = new System.Drawing.Point(141, 163);
            this.txtDistName1.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistName1.MaxLength = 40;
            this.txtDistName1.Name = "txtDistName1";
            this.txtDistName1.Size = new System.Drawing.Size(200, 23);
            this.txtDistName1.TabIndex = 2042;
            this.txtDistName1.Text = "○○現場名";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(573, 451);
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
            this.btnCancel.Location = new System.Drawing.Point(704, 451);
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
            this.chkDeleted.Text = "この出荷先を無効化";
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
            this.btnCheckDuplicate.Location = new System.Drawing.Point(349, 128);
            this.btnCheckDuplicate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckDuplicate.Name = "btnCheckDuplicate";
            this.btnCheckDuplicate.Size = new System.Drawing.Size(76, 29);
            this.btnCheckDuplicate.TabIndex = 2123;
            this.btnCheckDuplicate.Text = "重複確認";
            this.btnCheckDuplicate.UseVisualStyleBackColor = true;
            this.btnCheckDuplicate.Click += new System.EventHandler(this.btnCheckDuplicate_Click);
            // 
            // txtDestFax
            // 
            this.txtDestFax.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtDestFax.Location = new System.Drawing.Point(141, 385);
            this.txtDestFax.Margin = new System.Windows.Forms.Padding(4);
            this.txtDestFax.MaxLength = 15;
            this.txtDestFax.Name = "txtDestFax";
            this.txtDestFax.Size = new System.Drawing.Size(200, 23);
            this.txtDestFax.TabIndex = 2132;
            // 
            // txtDestTel
            // 
            this.txtDestTel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtDestTel.Location = new System.Drawing.Point(141, 353);
            this.txtDestTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtDestTel.MaxLength = 15;
            this.txtDestTel.Name = "txtDestTel";
            this.txtDestTel.Size = new System.Drawing.Size(200, 23);
            this.txtDestTel.TabIndex = 2122;
            this.txtDestTel.Text = "03-9999-9999";
            // 
            // txtAddress3
            // 
            this.txtAddress3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAddress3.Location = new System.Drawing.Point(141, 321);
            this.txtAddress3.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress3.MaxLength = 40;
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(200, 23);
            this.txtAddress3.TabIndex = 2112;
            // 
            // txtAddress2
            // 
            this.txtAddress2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAddress2.Location = new System.Drawing.Point(141, 289);
            this.txtAddress2.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress2.MaxLength = 40;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(200, 23);
            this.txtAddress2.TabIndex = 2102;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 229);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 15);
            this.label12.TabIndex = 2071;
            this.label12.Text = "出荷先郵便番号";
            // 
            // txtZipCode
            // 
            this.txtZipCode.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtZipCode.Location = new System.Drawing.Point(141, 226);
            this.txtZipCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtZipCode.MaxLength = 8;
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(200, 23);
            this.txtZipCode.TabIndex = 2072;
            this.txtZipCode.Text = "160-0022";
            // 
            // txtAddress1
            // 
            this.txtAddress1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAddress1.Location = new System.Drawing.Point(141, 257);
            this.txtAddress1.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress1.MaxLength = 40;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(200, 23);
            this.txtAddress1.TabIndex = 2092;
            this.txtAddress1.Text = "新宿区新宿";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 260);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 15);
            this.label15.TabIndex = 2091;
            this.label15.Text = "出荷先住所１*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 388);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 15);
            this.label16.TabIndex = 2131;
            this.label16.Text = "出荷先Fax";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 292);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 15);
            this.label17.TabIndex = 2101;
            this.label17.Text = "出荷先住所２";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(24, 356);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 15);
            this.label18.TabIndex = 2121;
            this.label18.Text = "出荷先Tel";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 324);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 15);
            this.label19.TabIndex = 2111;
            this.label19.Text = "出荷先住所３";
            // 
            // txtDistName2
            // 
            this.txtDistName2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDistName2.Location = new System.Drawing.Point(141, 195);
            this.txtDistName2.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistName2.MaxLength = 40;
            this.txtDistName2.Name = "txtDistName2";
            this.txtDistName2.Size = new System.Drawing.Size(200, 23);
            this.txtDistName2.TabIndex = 2052;
            this.txtDistName2.Text = "××××";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 198);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 2051;
            this.label9.Text = "出荷先名２";
            // 
            // txtCustUserCode
            // 
            this.txtGenbaCode.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtGenbaCode.Location = new System.Drawing.Point(141, 132);
            this.txtGenbaCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtGenbaCode.MaxLength = 3;
            this.txtGenbaCode.Name = "txtGenbaCode";
            this.txtGenbaCode.Size = new System.Drawing.Size(200, 23);
            this.txtGenbaCode.TabIndex = 2022;
            this.txtGenbaCode.Text = "123";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 135);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 2022;
            this.label2.Text = "現場コード*";
            // 
            // txtRemarks
            // 
            this.txtRemarks.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtRemarks.Location = new System.Drawing.Point(504, 70);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(323, 340);
            this.txtRemarks.TabIndex = 3002;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 3001;
            this.label5.Text = "備考";
            // 
            // txtCustName
            // 
            this.txtCustUserName.Location = new System.Drawing.Point(141, 101);
            this.txtCustUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustUserName.Name = "txtCustUserName";
            this.txtCustUserName.ReadOnly = true;
            this.txtCustUserName.Size = new System.Drawing.Size(200, 23);
            this.txtCustUserName.TabIndex = 5004;
            this.txtCustUserName.Text = "山田 太郎";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 104);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 5003;
            this.label6.Text = "顧客担当者名";
            // 
            // ME_DestinaitonsMstDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(852, 493);
            this.Controls.Add(this.txtCustUserName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.txtGenbaCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDistName2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDestFax);
            this.Controls.Add(this.txtDestTel);
            this.Controls.Add(this.txtAddress3);
            this.Controls.Add(this.txtAddress2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtZipCode);
            this.Controls.Add(this.txtAddress1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnCheckDuplicate);
            this.Controls.Add(this.btnSelectCustomer);
            this.Controls.Add(this.chkDeleted);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDistName1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ME_DestinaitonsMstDetail";
            this.Text = "出荷先詳細 (現場)";
            this.Load += new System.EventHandler(this.ME_EmployeeDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDistName1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDeleted;
        private System.Windows.Forms.Button btnSelectCustomer;
        private System.Windows.Forms.Button btnCheckDuplicate;
        private System.Windows.Forms.TextBox txtDestFax;
        private System.Windows.Forms.TextBox txtDestTel;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtDistName2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGenbaCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustUserName;
        private System.Windows.Forms.Label label6;
    }
}
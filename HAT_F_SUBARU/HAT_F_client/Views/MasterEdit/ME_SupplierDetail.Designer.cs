﻿namespace HatFClient.Views.MasterEdit
{
    partial class ME_SupplierDetail
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
            this.txtSupCode = new HatFClient.CustomControls.TextBoxChar();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSupName = new HatFClient.CustomControls.TextBoxChar();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSupKana = new HatFClient.CustomControls.TextBoxChar();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSupEmpName = new HatFClient.CustomControls.TextBoxChar();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSupZipCode = new HatFClient.CustomControls.TextBoxChar();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSupAddress1 = new HatFClient.CustomControls.TextBoxChar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkDeleted = new System.Windows.Forms.CheckBox();
            this.btnCheckDuplicate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbSupState = new System.Windows.Forms.ComboBox();
            this.txtSupAddress2 = new HatFClient.CustomControls.TextBoxChar();
            this.txtSupTel = new HatFClient.CustomControls.TextBoxChar();
            this.txtSupFax = new HatFClient.CustomControls.TextBoxChar();
            this.txtSupEmail = new HatFClient.CustomControls.TextBoxChar();
            this.txtSupDepName = new HatFClient.CustomControls.TextBoxChar();
            this.txtPayeeName = new HatFClient.CustomControls.TextBoxChar();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 2001;
            this.label1.Text = "仕入先コード*";
            // 
            // txtSupCode
            // 
            this.txtSupCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSupCode.Location = new System.Drawing.Point(138, 61);
            this.txtSupCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupCode.MaxLength = 8;
            this.txtSupCode.Name = "txtSupCode";
            this.txtSupCode.Size = new System.Drawing.Size(200, 23);
            this.txtSupCode.TabIndex = 2002;
            this.txtSupCode.Text = "0001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 2011;
            this.label3.Text = "仕入先名*";
            // 
            // txtSupName
            // 
            this.txtSupName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupName.Location = new System.Drawing.Point(138, 92);
            this.txtSupName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupName.MaxLength = 40;
            this.txtSupName.Name = "txtSupName";
            this.txtSupName.Size = new System.Drawing.Size(200, 23);
            this.txtSupName.TabIndex = 2012;
            this.txtSupName.Text = "株式会社○○製造";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 2021;
            this.label4.Text = "仕入先カナ";
            // 
            // txtSupKana
            // 
            this.txtSupKana.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupKana.Location = new System.Drawing.Point(138, 123);
            this.txtSupKana.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupKana.MaxLength = 40;
            this.txtSupKana.Name = "txtSupKana";
            this.txtSupKana.Size = new System.Drawing.Size(200, 23);
            this.txtSupKana.TabIndex = 2022;
            this.txtSupKana.Text = "○○セイゾウ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 159);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 2031;
            this.label5.Text = "仕入先担当者名";
            // 
            // txtSupEmpName
            // 
            this.txtSupEmpName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupEmpName.Location = new System.Drawing.Point(138, 154);
            this.txtSupEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupEmpName.MaxLength = 20;
            this.txtSupEmpName.Name = "txtSupEmpName";
            this.txtSupEmpName.Size = new System.Drawing.Size(200, 23);
            this.txtSupEmpName.TabIndex = 2032;
            this.txtSupEmpName.Text = "新宿支店";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(168, 501);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(123, 29);
            this.btnOK.TabIndex = 5001;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(299, 501);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 29);
            this.btnCancel.TabIndex = 5002;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 190);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 2041;
            this.label6.Text = "仕入先部門名";
            // 
            // txtSupZipCode
            // 
            this.txtSupZipCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSupZipCode.Location = new System.Drawing.Point(138, 216);
            this.txtSupZipCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupZipCode.MaxLength = 8;
            this.txtSupZipCode.Name = "txtSupZipCode";
            this.txtSupZipCode.Size = new System.Drawing.Size(200, 23);
            this.txtSupZipCode.TabIndex = 2052;
            this.txtSupZipCode.Text = "160-0022";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 221);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 2051;
            this.label7.Text = "郵便番号";
            // 
            // txtSupAddress1
            // 
            this.txtSupAddress1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupAddress1.Location = new System.Drawing.Point(138, 279);
            this.txtSupAddress1.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupAddress1.MaxLength = 40;
            this.txtSupAddress1.Name = "txtSupAddress1";
            this.txtSupAddress1.Size = new System.Drawing.Size(200, 23);
            this.txtSupAddress1.TabIndex = 2072;
            this.txtSupAddress1.Text = "新宿区新宿";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 252);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 2061;
            this.label8.Text = "都道府県";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 281);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 15);
            this.label9.TabIndex = 2071;
            this.label9.Text = "住所１";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 310);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 15);
            this.label10.TabIndex = 2081;
            this.label10.Text = "住所２";
            // 
            // chkDeleted
            // 
            this.chkDeleted.AutoSize = true;
            this.chkDeleted.Location = new System.Drawing.Point(27, 23);
            this.chkDeleted.Name = "chkDeleted";
            this.chkDeleted.Size = new System.Drawing.Size(125, 19);
            this.chkDeleted.TabIndex = 1001;
            this.chkDeleted.Text = "この仕入先を無効化";
            this.chkDeleted.UseVisualStyleBackColor = true;
            // 
            // btnCheckDuplicate
            // 
            this.btnCheckDuplicate.Location = new System.Drawing.Point(346, 55);
            this.btnCheckDuplicate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckDuplicate.Name = "btnCheckDuplicate";
            this.btnCheckDuplicate.Size = new System.Drawing.Size(76, 29);
            this.btnCheckDuplicate.TabIndex = 2003;
            this.btnCheckDuplicate.Text = "重複確認";
            this.btnCheckDuplicate.UseVisualStyleBackColor = true;
            this.btnCheckDuplicate.Click += new System.EventHandler(this.BtnCheckDuplicate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 401);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2111;
            this.label2.Text = "メールアドレス";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 372);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 15);
            this.label12.TabIndex = 2101;
            this.label12.Text = "Fax";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 343);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 15);
            this.label13.TabIndex = 2091;
            this.label13.Text = "Tel";
            // 
            // cmbSupState
            // 
            this.cmbSupState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupState.FormattingEnabled = true;
            this.cmbSupState.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbSupState.Location = new System.Drawing.Point(138, 249);
            this.cmbSupState.Name = "cmbSupState";
            this.cmbSupState.Size = new System.Drawing.Size(200, 23);
            this.cmbSupState.TabIndex = 2062;
            // 
            // txtSupAddress2
            // 
            this.txtSupAddress2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupAddress2.Location = new System.Drawing.Point(138, 307);
            this.txtSupAddress2.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupAddress2.MaxLength = 40;
            this.txtSupAddress2.Name = "txtSupAddress2";
            this.txtSupAddress2.Size = new System.Drawing.Size(200, 23);
            this.txtSupAddress2.TabIndex = 2082;
            // 
            // txtSupTel
            // 
            this.txtSupTel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSupTel.Location = new System.Drawing.Point(138, 338);
            this.txtSupTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupTel.MaxLength = 13;
            this.txtSupTel.Name = "txtSupTel";
            this.txtSupTel.Size = new System.Drawing.Size(200, 23);
            this.txtSupTel.TabIndex = 2092;
            this.txtSupTel.Text = "03-9999-9999";
            // 
            // txtSupFax
            // 
            this.txtSupFax.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSupFax.Location = new System.Drawing.Point(138, 369);
            this.txtSupFax.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupFax.MaxLength = 13;
            this.txtSupFax.Name = "txtSupFax";
            this.txtSupFax.Size = new System.Drawing.Size(200, 23);
            this.txtSupFax.TabIndex = 2102;
            // 
            // txtSupEmail
            // 
            this.txtSupEmail.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSupEmail.Location = new System.Drawing.Point(138, 401);
            this.txtSupEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupEmail.MaxLength = 320;
            this.txtSupEmail.Name = "txtSupEmail";
            this.txtSupEmail.Size = new System.Drawing.Size(200, 23);
            this.txtSupEmail.TabIndex = 2112;
            // 
            // txtSupDepName
            // 
            this.txtSupDepName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSupDepName.Location = new System.Drawing.Point(138, 187);
            this.txtSupDepName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupDepName.MaxLength = 40;
            this.txtSupDepName.Name = "txtSupDepName";
            this.txtSupDepName.Size = new System.Drawing.Size(200, 23);
            this.txtSupDepName.TabIndex = 2042;
            this.txtSupDepName.Text = "〇〇部";
            // 
            // txtPayeeName
            // 
            this.txtPayeeName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPayeeName.Location = new System.Drawing.Point(138, 432);
            this.txtPayeeName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPayeeName.MaxLength = 8;
            this.txtPayeeName.Name = "txtPayeeName";
            this.txtPayeeName.ReadOnly = true;
            this.txtPayeeName.Size = new System.Drawing.Size(200, 23);
            this.txtPayeeName.TabIndex = 5004;
            this.txtPayeeName.Text = "支払先名称";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 436);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 15);
            this.label11.TabIndex = 5003;
            this.label11.Text = "支払先";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 429);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 29);
            this.button1.TabIndex = 5005;
            this.button1.Text = "選択";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ME_SupplierDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(451, 552);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPayeeName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSupEmail);
            this.Controls.Add(this.txtSupFax);
            this.Controls.Add(this.txtSupTel);
            this.Controls.Add(this.txtSupAddress2);
            this.Controls.Add(this.cmbSupState);
            this.Controls.Add(this.chkDeleted);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSupCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSupZipCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSupDepName);
            this.Controls.Add(this.txtSupEmpName);
            this.Controls.Add(this.txtSupAddress1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSupKana);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSupName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckDuplicate);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ME_SupplierDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "仕入先詳細";
            this.Load += new System.EventHandler(this.ME_SupplierDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private HatFClient.CustomControls.TextBoxChar txtSupCode;
        private System.Windows.Forms.Label label3;
        private HatFClient.CustomControls.TextBoxChar txtSupName;
        private System.Windows.Forms.Label label4;
        private HatFClient.CustomControls.TextBoxChar txtSupKana;
        private System.Windows.Forms.Label label5;
        private HatFClient.CustomControls.TextBoxChar txtSupEmpName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private HatFClient.CustomControls.TextBoxChar txtSupZipCode;
        private System.Windows.Forms.Label label7;
        private HatFClient.CustomControls.TextBoxChar txtSupAddress1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkDeleted;
        private System.Windows.Forms.Button btnCheckDuplicate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbSupState;
        private HatFClient.CustomControls.TextBoxChar txtSupAddress2;
        private HatFClient.CustomControls.TextBoxChar txtSupTel;
        private HatFClient.CustomControls.TextBoxChar txtSupFax;
        private HatFClient.CustomControls.TextBoxChar txtSupEmail;
        private HatFClient.CustomControls.TextBoxChar txtSupDepName;
        private HatFClient.CustomControls.TextBoxChar txtPayeeName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
    }
}
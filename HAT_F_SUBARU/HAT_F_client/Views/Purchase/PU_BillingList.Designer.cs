namespace HatFClient.Views.Purchase
{
    partial class PU_BillingList
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
            this.components = new System.ComponentModel.Container();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel出力 = new System.Windows.Forms.Button();
            this.textFilterStr = new System.Windows.Forms.TextBox();
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnDataInput = new System.Windows.Forms.Button();
            this.btnContactEmail = new System.Windows.Forms.Button();
            this.btnPuAmountCollation = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDetailSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchSupplier = new System.Windows.Forms.Button();
            this.cmbPayMonth = new HatFClient.CustomControls.ComboBoxPayDate(this.components);
            this.txtChuban = new HatFClient.CustomControls.TextBoxChar();
            this.txtSupCode = new HatFClient.CustomControls.TextBoxChar();
            this.gridPatternUI = new HatFClient.CustomControls.GridPatternUI();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(724, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(59, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 196);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 707);
            this.panel1.TabIndex = 11;
            // 
            // btnExcel出力
            // 
            this.btnExcel出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel出力.Location = new System.Drawing.Point(1154, 88);
            this.btnExcel出力.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcel出力.Name = "btnExcel出力";
            this.btnExcel出力.Size = new System.Drawing.Size(103, 23);
            this.btnExcel出力.TabIndex = 14;
            this.btnExcel出力.Text = "Excel印刷";
            this.btnExcel出力.UseVisualStyleBackColor = true;
            this.btnExcel出力.Click += new System.EventHandler(this.btnExcel出力_Click);
            // 
            // textFilterStr
            // 
            this.textFilterStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilterStr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textFilterStr.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.textFilterStr.Location = new System.Drawing.Point(121, 48);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(664, 90);
            this.textFilterStr.TabIndex = 9;
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(8, 173);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(90, 21);
            this.lblProjectAllCount.TabIndex = 10;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Location = new System.Drawing.Point(1154, 115);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(103, 23);
            this.btnDetail.TabIndex = 17;
            this.btnDetail.Text = "詳細";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnDataInput
            // 
            this.btnDataInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataInput.Enabled = false;
            this.btnDataInput.Location = new System.Drawing.Point(1045, 115);
            this.btnDataInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDataInput.Name = "btnDataInput";
            this.btnDataInput.Size = new System.Drawing.Size(103, 23);
            this.btnDataInput.TabIndex = 16;
            this.btnDataInput.Text = "請求データ取込";
            this.btnDataInput.UseVisualStyleBackColor = true;
            this.btnDataInput.Click += new System.EventHandler(this.btnDataInput_Click);
            // 
            // btnContactEmail
            // 
            this.btnContactEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContactEmail.Location = new System.Drawing.Point(1045, 88);
            this.btnContactEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnContactEmail.Name = "btnContactEmail";
            this.btnContactEmail.Size = new System.Drawing.Size(103, 23);
            this.btnContactEmail.TabIndex = 13;
            this.btnContactEmail.Text = "担当者へ連絡";
            this.btnContactEmail.UseVisualStyleBackColor = true;
            this.btnContactEmail.Click += new System.EventHandler(this.btnContactEmail_Click);
            // 
            // btnPuAmountCollation
            // 
            this.btnPuAmountCollation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPuAmountCollation.Location = new System.Drawing.Point(936, 115);
            this.btnPuAmountCollation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPuAmountCollation.Name = "btnPuAmountCollation";
            this.btnPuAmountCollation.Size = new System.Drawing.Size(103, 23);
            this.btnPuAmountCollation.TabIndex = 15;
            this.btnPuAmountCollation.Text = "仕入金額照合";
            this.btnPuAmountCollation.UseVisualStyleBackColor = true;
            this.btnPuAmountCollation.Click += new System.EventHandler(this.btnPuAmountCollation_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "仕入先コード*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(302, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "仕入支払月";
            // 
            // btnDetailSearch
            // 
            this.btnDetailSearch.Location = new System.Drawing.Point(12, 45);
            this.btnDetailSearch.Name = "btnDetailSearch";
            this.btnDetailSearch.Size = new System.Drawing.Size(103, 23);
            this.btnDetailSearch.TabIndex = 8;
            this.btnDetailSearch.Text = "詳細検索";
            this.btnDetailSearch.UseVisualStyleBackColor = true;
            this.btnDetailSearch.Click += new System.EventHandler(this.btnDetailSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(478, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hat注文番号";
            // 
            // btnSearchSupplier
            // 
            this.btnSearchSupplier.Location = new System.Drawing.Point(200, 12);
            this.btnSearchSupplier.Name = "btnSearchSupplier";
            this.btnSearchSupplier.Size = new System.Drawing.Size(88, 23);
            this.btnSearchSupplier.TabIndex = 2;
            this.btnSearchSupplier.Text = "仕入先検索";
            this.btnSearchSupplier.UseVisualStyleBackColor = true;
            this.btnSearchSupplier.Click += new System.EventHandler(this.btnSearchSupplier_Click);
            // 
            // cmbPayMonth
            // 
            this.cmbPayMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMonth.FormattingEnabled = true;
            this.cmbPayMonth.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbPayMonth.Location = new System.Drawing.Point(398, 14);
            this.cmbPayMonth.Name = "cmbPayMonth";
            this.cmbPayMonth.Size = new System.Drawing.Size(70, 20);
            this.cmbPayMonth.TabIndex = 4;
            // 
            // txtChuban
            // 
            this.txtChuban.Location = new System.Drawing.Point(583, 14);
            this.txtChuban.MaxLength = 8;
            this.txtChuban.Name = "txtChuban";
            this.txtChuban.Size = new System.Drawing.Size(135, 19);
            this.txtChuban.TabIndex = 6;
            this.txtChuban.TextChanged += new System.EventHandler(this.txtSupCode_TextChanged);
            // 
            // txtSupCode
            // 
            this.txtSupCode.Location = new System.Drawing.Point(116, 14);
            this.txtSupCode.MaxLength = 8;
            this.txtSupCode.Name = "txtSupCode";
            this.txtSupCode.Size = new System.Drawing.Size(78, 19);
            this.txtSupCode.TabIndex = 1;
            this.txtSupCode.TextChanged += new System.EventHandler(this.txtSupCode_TextChanged);
            // 
            // gridPatternUI
            // 
            this.gridPatternUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatternUI.Location = new System.Drawing.Point(813, 12);
            this.gridPatternUI.Name = "gridPatternUI";
            this.gridPatternUI.Size = new System.Drawing.Size(444, 71);
            this.gridPatternUI.TabIndex = 12;
            // 
            // PU_BillingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.cmbPayMonth);
            this.Controls.Add(this.btnDetailSearch);
            this.Controls.Add(this.txtChuban);
            this.Controls.Add(this.txtSupCode);
            this.Controls.Add(this.btnPuAmountCollation);
            this.Controls.Add(this.btnContactEmail);
            this.Controls.Add(this.btnDataInput);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.gridPatternUI);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.btnExcel出力);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearchSupplier);
            this.Controls.Add(this.btnSearch);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 766);
            this.Name = "PU_BillingList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仕入請求一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PU_BillingList_FormClosing);
            this.Load += new System.EventHandler(this.PU_BillingList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PU_BillingList_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textFilterStr;
        private System.Windows.Forms.Button btnExcel出力;
        private System.Windows.Forms.Label lblProjectAllCount;
        private CustomControls.GridPatternUI gridPatternUI;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnDataInput;
        private System.Windows.Forms.Button btnContactEmail;
        private System.Windows.Forms.Button btnPuAmountCollation;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxChar txtSupCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDetailSearch;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxChar txtChuban;
        private CustomControls.ComboBoxPayDate cmbPayMonth;
        private System.Windows.Forms.Button btnSearchSupplier;
    }
}
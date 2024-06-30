namespace HatFClient.Views.SalesCorrection
{
    partial class CreditNoteList
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
            this.btnAdvancedSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcelPrint = new System.Windows.Forms.Button();
            this.textFilterStr = new System.Windows.Forms.TextBox();
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchCustomers = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.ymInvoicedTo = new HatFClient.CustomControls.YearMonthEdit();
            this.ymInvoicedFrom = new HatFClient.CustomControls.YearMonthEdit();
            this.txtCustCode = new HatFClient.CustomControls.TextBoxChar();
            this.gridPatternUI = new HatFClient.CustomControls.GridPatternUI();
            ((System.ComponentModel.ISupportInitialize)(this.ymInvoicedTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ymInvoicedFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdvancedSearch
            // 
            this.btnAdvancedSearch.Enabled = false;
            this.btnAdvancedSearch.Location = new System.Drawing.Point(12, 41);
            this.btnAdvancedSearch.Name = "btnAdvancedSearch";
            this.btnAdvancedSearch.Size = new System.Drawing.Size(93, 23);
            this.btnAdvancedSearch.TabIndex = 8;
            this.btnAdvancedSearch.Text = "詳細検索";
            this.btnAdvancedSearch.UseVisualStyleBackColor = true;
            this.btnAdvancedSearch.Click += new System.EventHandler(this.BtnAdvancedSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 165);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1141, 388);
            this.panel1.TabIndex = 15;
            // 
            // btnExcelPrint
            // 
            this.btnExcelPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelPrint.Location = new System.Drawing.Point(1050, 88);
            this.btnExcelPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcelPrint.Name = "btnExcelPrint";
            this.btnExcelPrint.Size = new System.Drawing.Size(103, 23);
            this.btnExcelPrint.TabIndex = 13;
            this.btnExcelPrint.Text = "Excel印刷";
            this.btnExcelPrint.UseVisualStyleBackColor = true;
            this.btnExcelPrint.Click += new System.EventHandler(this.BtnExcelPrint_Click);
            // 
            // textFilterStr
            // 
            this.textFilterStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilterStr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textFilterStr.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.textFilterStr.Location = new System.Drawing.Point(116, 41);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(564, 90);
            this.textFilterStr.TabIndex = 9;
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(8, 142);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(90, 21);
            this.lblProjectAllCount.TabIndex = 14;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Enabled = false;
            this.btnDetail.Location = new System.Drawing.Point(941, 88);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(103, 23);
            this.btnDetail.TabIndex = 12;
            this.btnDetail.Text = "赤黒詳細";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.BtnDetail_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(429, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(300, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "請求年月";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(8, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "得意先コード*";
            // 
            // btnSearchCustomers
            // 
            this.btnSearchCustomers.Location = new System.Drawing.Point(200, 10);
            this.btnSearchCustomers.Name = "btnSearchCustomers";
            this.btnSearchCustomers.Size = new System.Drawing.Size(88, 23);
            this.btnSearchCustomers.TabIndex = 2;
            this.btnSearchCustomers.Text = "得意先検索";
            this.btnSearchCustomers.UseVisualStyleBackColor = true;
            this.btnSearchCustomers.Click += new System.EventHandler(this.BtnSearchCustomers_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(577, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(832, 88);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(103, 23);
            this.btnNew.TabIndex = 11;
            this.btnNew.Text = "新規赤黒登録";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // ymInvoicedTo
            // 
            this.ymInvoicedTo.CustomFormat = "yy/MM";
            this.ymInvoicedTo.DataType = typeof(System.DateTime);
            this.ymInvoicedTo.DisplayFormat.CustomFormat = "yy/MM";
            this.ymInvoicedTo.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedTo.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.ymInvoicedTo.EditFormat.CustomFormat = "yy/MM";
            this.ymInvoicedTo.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedTo.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.ymInvoicedTo.EditMask = "99/99";
            this.ymInvoicedTo.EmptyAsNull = true;
            this.ymInvoicedTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedTo.Location = new System.Drawing.Point(461, 12);
            this.ymInvoicedTo.Name = "ymInvoicedTo";
            this.ymInvoicedTo.Size = new System.Drawing.Size(50, 19);
            this.ymInvoicedTo.TabIndex = 6;
            this.ymInvoicedTo.Tag = null;
            // 
            // ymInvoicedFrom
            // 
            this.ymInvoicedFrom.CustomFormat = "yy/MM";
            this.ymInvoicedFrom.DataType = typeof(System.DateTime);
            this.ymInvoicedFrom.DisplayFormat.CustomFormat = "yy/MM";
            this.ymInvoicedFrom.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedFrom.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.ymInvoicedFrom.EditFormat.CustomFormat = "yy/MM";
            this.ymInvoicedFrom.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedFrom.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.ymInvoicedFrom.EditMask = "99/99";
            this.ymInvoicedFrom.EmptyAsNull = true;
            this.ymInvoicedFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ymInvoicedFrom.Location = new System.Drawing.Point(373, 12);
            this.ymInvoicedFrom.Name = "ymInvoicedFrom";
            this.ymInvoicedFrom.Size = new System.Drawing.Size(50, 19);
            this.ymInvoicedFrom.TabIndex = 4;
            this.ymInvoicedFrom.Tag = null;
            // 
            // txtCustCode
            // 
            this.txtCustCode.Location = new System.Drawing.Point(116, 12);
            this.txtCustCode.MaxLength = 8;
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(78, 19);
            this.txtCustCode.TabIndex = 1;
            this.txtCustCode.TextChanged += new System.EventHandler(this.TxtCustCode_TextChanged);
            // 
            // gridPatternUI
            // 
            this.gridPatternUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatternUI.Location = new System.Drawing.Point(709, 12);
            this.gridPatternUI.Name = "gridPatternUI";
            this.gridPatternUI.Size = new System.Drawing.Size(444, 71);
            this.gridPatternUI.TabIndex = 10;
            // 
            // CreditNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1165, 564);
            this.Controls.Add(this.ymInvoicedTo);
            this.Controls.Add(this.ymInvoicedFrom);
            this.Controls.Add(this.txtCustCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearchCustomers);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.gridPatternUI);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.btnExcelPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAdvancedSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1181, 603);
            this.Name = "CreditNoteList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "赤黒登録一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreditNoteList_FormClosed);
            this.Load += new System.EventHandler(this.CreditNoteList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ymInvoicedTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ymInvoicedFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAdvancedSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textFilterStr;
        private System.Windows.Forms.Button btnExcelPrint;
        private System.Windows.Forms.Label lblProjectAllCount;
        private CustomControls.GridPatternUI gridPatternUI;
        private System.Windows.Forms.Button btnDetail;
        private CustomControls.TextBoxChar txtCustCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchCustomers;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private CustomControls.YearMonthEdit ymInvoicedFrom;
        private CustomControls.YearMonthEdit ymInvoicedTo;
    }
}
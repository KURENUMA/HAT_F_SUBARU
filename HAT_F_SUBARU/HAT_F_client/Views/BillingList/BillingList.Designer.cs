namespace HatFClient.Views.BillingList
{
    partial class BillingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillingList));
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbInvoiceConfirmation = new System.Windows.Forms.ComboBox();
            this.txtroCount = new HatFClient.CustomControls.TextBoxReadOnly();
            this.cmbPaymentMonth = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.grdList.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.grdList.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdList.Footers.Fixed = true;
            this.grdList.Location = new System.Drawing.Point(12, 48);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 1;
            this.grdList.Rows.DefaultSize = 30;
            this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.grdList.Size = new System.Drawing.Size(1244, 802);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.grdList.TabIndex = 5;
            this.grdList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GrdList_MouseDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(1136, 876);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(1002, 876);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCount.Location = new System.Drawing.Point(101, 887);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(24, 19);
            this.lblCount.TabIndex = 140;
            this.lblCount.Text = "件";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPrint.Location = new System.Drawing.Point(1136, 12);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 30);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "印刷";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDetail.Location = new System.Drawing.Point(1002, 12);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(120, 30);
            this.btnDetail.TabIndex = 3;
            this.btnDetail.TabStop = false;
            this.btnDetail.Text = "詳細";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.BtnDetail_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(345, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cmbInvoiceConfirmation
            // 
            this.cmbInvoiceConfirmation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceConfirmation.FormattingEnabled = true;
            this.cmbInvoiceConfirmation.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cmbInvoiceConfirmation.Location = new System.Drawing.Point(12, 15);
            this.cmbInvoiceConfirmation.Name = "cmbInvoiceConfirmation";
            this.cmbInvoiceConfirmation.Size = new System.Drawing.Size(150, 27);
            this.cmbInvoiceConfirmation.TabIndex = 0;
            // 
            // txtroCount
            // 
            this.txtroCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtroCount.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroCount.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroCount.Location = new System.Drawing.Point(12, 882);
            this.txtroCount.Name = "txtroCount";
            this.txtroCount.ReadOnly = true;
            this.txtroCount.Size = new System.Drawing.Size(85, 27);
            this.txtroCount.TabIndex = 139;
            this.txtroCount.TabStop = false;
            this.txtroCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbPaymentMonth
            // 
            this.cmbPaymentMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMonth.FormattingEnabled = true;
            this.cmbPaymentMonth.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cmbPaymentMonth.Location = new System.Drawing.Point(168, 15);
            this.cmbPaymentMonth.Name = "cmbPaymentMonth";
            this.cmbPaymentMonth.Size = new System.Drawing.Size(150, 27);
            this.cmbPaymentMonth.TabIndex = 1;
            // 
            // BillingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.cmbPaymentMonth);
            this.Controls.Add(this.cmbInvoiceConfirmation);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtroCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grdList);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BillingList";
            this.Text = "請求一覧";
            this.Load += new System.EventHandler(this.BillingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private CustomControls.TextBoxReadOnly txtroCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbInvoiceConfirmation;
        private System.Windows.Forms.ComboBox cmbPaymentMonth;
    }
}
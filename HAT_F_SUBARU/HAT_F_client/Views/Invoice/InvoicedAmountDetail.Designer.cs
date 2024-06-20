namespace HatFClient.Views.Invoice
{
    partial class InvoicedAmountDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoicedAmountDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblPayment = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdateInvoice = new System.Windows.Forms.Button();
            this.lblInvoiceTotal = new System.Windows.Forms.Label();
            this.groupInvoiceState = new System.Windows.Forms.GroupBox();
            this.radioCompleted = new System.Windows.Forms.RadioButton();
            this.radioPaymentReceived = new System.Windows.Forms.RadioButton();
            this.radioInvoiceSent = new System.Windows.Forms.RadioButton();
            this.radioInvoiceIssued = new System.Windows.Forms.RadioButton();
            this.radioUnbilled = new System.Windows.Forms.RadioButton();
            this.gridInvoiceDetail = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblCompCode = new System.Windows.Forms.Label();
            this.lblCompName = new System.Windows.Forms.Label();
            this.gridInvoiceTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.gridCompany = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.gridPayment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnDesignatedOutPut = new System.Windows.Forms.Button();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.groupInvoiceState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(-66, -80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 36;
            this.label1.Text = "検索条件";
            // 
            // lblInvoice
            // 
            this.lblInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblInvoice.Location = new System.Drawing.Point(16, 57);
            this.lblInvoice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(67, 15);
            this.lblInvoice.TabIndex = 46;
            this.lblInvoice.Text = "請求書明細";
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCompany.Location = new System.Drawing.Point(461, 668);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(55, 15);
            this.lblCompany.TabIndex = 48;
            this.lblCompany.Text = "自社情報";
            // 
            // lblPayment
            // 
            this.lblPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPayment.AutoSize = true;
            this.lblPayment.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPayment.Location = new System.Drawing.Point(15, 668);
            this.lblPayment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(43, 15);
            this.lblPayment.TabIndex = 50;
            this.lblPayment.Text = "振込先";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(1130, 879);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 52;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "F11：閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdateInvoice
            // 
            this.btnUpdateInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateInvoice.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdateInvoice.Location = new System.Drawing.Point(1288, 879);
            this.btnUpdateInvoice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpdateInvoice.Name = "btnUpdateInvoice";
            this.btnUpdateInvoice.Size = new System.Drawing.Size(120, 30);
            this.btnUpdateInvoice.TabIndex = 51;
            this.btnUpdateInvoice.TabStop = false;
            this.btnUpdateInvoice.Text = "F12：更新";
            this.btnUpdateInvoice.UseVisualStyleBackColor = true;
            // 
            // lblInvoiceTotal
            // 
            this.lblInvoiceTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoiceTotal.AutoSize = true;
            this.lblInvoiceTotal.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblInvoiceTotal.Location = new System.Drawing.Point(1050, 57);
            this.lblInvoiceTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInvoiceTotal.Name = "lblInvoiceTotal";
            this.lblInvoiceTotal.Size = new System.Drawing.Size(67, 15);
            this.lblInvoiceTotal.TabIndex = 53;
            this.lblInvoiceTotal.Text = "請求書総計";
            // 
            // groupInvoiceState
            // 
            this.groupInvoiceState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupInvoiceState.Controls.Add(this.radioCompleted);
            this.groupInvoiceState.Controls.Add(this.radioPaymentReceived);
            this.groupInvoiceState.Controls.Add(this.radioInvoiceSent);
            this.groupInvoiceState.Controls.Add(this.radioInvoiceIssued);
            this.groupInvoiceState.Controls.Add(this.radioUnbilled);
            this.groupInvoiceState.Location = new System.Drawing.Point(1053, 484);
            this.groupInvoiceState.Name = "groupInvoiceState";
            this.groupInvoiceState.Size = new System.Drawing.Size(350, 164);
            this.groupInvoiceState.TabIndex = 54;
            this.groupInvoiceState.TabStop = false;
            this.groupInvoiceState.Text = "請求書送付状態";
            // 
            // radioCompleted
            // 
            this.radioCompleted.AutoSize = true;
            this.radioCompleted.Location = new System.Drawing.Point(109, 99);
            this.radioCompleted.Name = "radioCompleted";
            this.radioCompleted.Size = new System.Drawing.Size(47, 16);
            this.radioCompleted.TabIndex = 4;
            this.radioCompleted.TabStop = true;
            this.radioCompleted.Text = "完了";
            this.radioCompleted.UseVisualStyleBackColor = true;
            // 
            // radioPaymentReceived
            // 
            this.radioPaymentReceived.AutoSize = true;
            this.radioPaymentReceived.Location = new System.Drawing.Point(22, 99);
            this.radioPaymentReceived.Name = "radioPaymentReceived";
            this.radioPaymentReceived.Size = new System.Drawing.Size(59, 16);
            this.radioPaymentReceived.TabIndex = 3;
            this.radioPaymentReceived.TabStop = true;
            this.radioPaymentReceived.Text = "入金済";
            this.radioPaymentReceived.UseVisualStyleBackColor = true;
            // 
            // radioInvoiceSent
            // 
            this.radioInvoiceSent.AutoSize = true;
            this.radioInvoiceSent.Location = new System.Drawing.Point(229, 48);
            this.radioInvoiceSent.Name = "radioInvoiceSent";
            this.radioInvoiceSent.Size = new System.Drawing.Size(95, 16);
            this.radioInvoiceSent.TabIndex = 2;
            this.radioInvoiceSent.TabStop = true;
            this.radioInvoiceSent.Text = "請求書送付済";
            this.radioInvoiceSent.UseVisualStyleBackColor = true;
            // 
            // radioInvoiceIssued
            // 
            this.radioInvoiceIssued.AutoSize = true;
            this.radioInvoiceIssued.Location = new System.Drawing.Point(109, 48);
            this.radioInvoiceIssued.Name = "radioInvoiceIssued";
            this.radioInvoiceIssued.Size = new System.Drawing.Size(95, 16);
            this.radioInvoiceIssued.TabIndex = 1;
            this.radioInvoiceIssued.TabStop = true;
            this.radioInvoiceIssued.Text = "請求書発行済";
            this.radioInvoiceIssued.UseVisualStyleBackColor = true;
            // 
            // radioUnbilled
            // 
            this.radioUnbilled.AutoSize = true;
            this.radioUnbilled.Location = new System.Drawing.Point(22, 48);
            this.radioUnbilled.Name = "radioUnbilled";
            this.radioUnbilled.Size = new System.Drawing.Size(59, 16);
            this.radioUnbilled.TabIndex = 0;
            this.radioUnbilled.TabStop = true;
            this.radioUnbilled.Text = "未請求";
            this.radioUnbilled.UseVisualStyleBackColor = true;
            // 
            // gridInvoiceDetail
            // 
            this.gridInvoiceDetail.AllowEditing = false;
            this.gridInvoiceDetail.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.gridInvoiceDetail.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.gridInvoiceDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInvoiceDetail.AutoGenerateColumns = false;
            this.gridInvoiceDetail.ColumnInfo = resources.GetString("gridInvoiceDetail.ColumnInfo");
            this.gridInvoiceDetail.Location = new System.Drawing.Point(18, 75);
            this.gridInvoiceDetail.Name = "gridInvoiceDetail";
            this.gridInvoiceDetail.Rows.Count = 1;
            this.gridInvoiceDetail.Size = new System.Drawing.Size(986, 573);
            this.gridInvoiceDetail.TabIndex = 56;
            // 
            // lblCompCode
            // 
            this.lblCompCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompCode.AutoSize = true;
            this.lblCompCode.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCompCode.Location = new System.Drawing.Point(15, 9);
            this.lblCompCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompCode.Name = "lblCompCode";
            this.lblCompCode.Size = new System.Drawing.Size(131, 24);
            this.lblCompCode.TabIndex = 57;
            this.lblCompCode.Text = "得意先コード：";
            // 
            // lblCompName
            // 
            this.lblCompName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompName.AutoSize = true;
            this.lblCompName.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCompName.Location = new System.Drawing.Point(310, 9);
            this.lblCompName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompName.Name = "lblCompName";
            this.lblCompName.Size = new System.Drawing.Size(105, 24);
            this.lblCompName.TabIndex = 58;
            this.lblCompName.Text = "得意先名：";
            // 
            // gridInvoiceTotal
            // 
            this.gridInvoiceTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridInvoiceTotal.AllowEditing = false;
            this.gridInvoiceTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInvoiceTotal.AutoGenerateColumns = false;
            this.gridInvoiceTotal.ColumnInfo = "2,1,0,0,0,350,Columns:0{Style:\"Font:Meiryo UI, 9pt;\";}\t1{Width:220;AllowFiltering" +
    ":ByValue;AllowDragging:False;Style:\"DataType:System.String;TextAlign:LeftCenter;" +
    "\";}\t";
            this.gridInvoiceTotal.Location = new System.Drawing.Point(1053, 75);
            this.gridInvoiceTotal.Name = "gridInvoiceTotal";
            this.gridInvoiceTotal.Rows.Count = 1;
            this.gridInvoiceTotal.Rows.DefaultSize = 30;
            this.gridInvoiceTotal.Rows.Fixed = 0;
            this.gridInvoiceTotal.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridInvoiceTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridInvoiceTotal.Size = new System.Drawing.Size(350, 154);
            this.gridInvoiceTotal.TabIndex = 59;
            // 
            // gridCompany
            // 
            this.gridCompany.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gridCompany.AutoGenerateColumns = false;
            this.gridCompany.ColumnInfo = "2,1,0,0,0,450,Columns:0{Style:\"Font:Meiryo UI, 9pt;\";}\t1{Width:220;AllowFiltering" +
    ":ByValue;AllowDragging:False;Style:\"DataType:System.String;TextAlign:LeftCenter;" +
    "\";}\t";
            this.gridCompany.Location = new System.Drawing.Point(464, 686);
            this.gridCompany.Name = "gridCompany";
            this.gridCompany.Rows.Count = 1;
            this.gridCompany.Rows.DefaultSize = 30;
            this.gridCompany.Rows.Fixed = 0;
            this.gridCompany.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridCompany.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridCompany.Size = new System.Drawing.Size(412, 154);
            this.gridCompany.TabIndex = 60;
            // 
            // gridPayment
            // 
            this.gridPayment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.gridPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gridPayment.AutoGenerateColumns = false;
            this.gridPayment.ColumnInfo = "2,1,0,0,0,450,Columns:0{Style:\"Font:Meiryo UI, 9pt;\";}\t1{Width:220;AllowFiltering" +
    ":ByValue;AllowDragging:False;Style:\"DataType:System.String;TextAlign:LeftCenter;" +
    "\";}\t";
            this.gridPayment.Location = new System.Drawing.Point(18, 686);
            this.gridPayment.Name = "gridPayment";
            this.gridPayment.Rows.Count = 1;
            this.gridPayment.Rows.DefaultSize = 30;
            this.gridPayment.Rows.Fixed = 0;
            this.gridPayment.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gridPayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridPayment.Size = new System.Drawing.Size(412, 154);
            this.gridPayment.TabIndex = 61;
            // 
            // btnInput
            // 
            this.btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput.Location = new System.Drawing.Point(1240, 254);
            this.btnInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(163, 39);
            this.btnInput.TabIndex = 68;
            this.btnInput.Text = "入力内容の反映";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAuto.Location = new System.Drawing.Point(1053, 254);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(163, 39);
            this.btnAuto.TabIndex = 67;
            this.btnAuto.Text = "自動反映";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // btnDesignatedOutPut
            // 
            this.btnDesignatedOutPut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDesignatedOutPut.Location = new System.Drawing.Point(1053, 392);
            this.btnDesignatedOutPut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDesignatedOutPut.Name = "btnDesignatedOutPut";
            this.btnDesignatedOutPut.Size = new System.Drawing.Size(350, 39);
            this.btnDesignatedOutPut.TabIndex = 66;
            this.btnDesignatedOutPut.Text = "入金チェックリストを出力";
            this.btnDesignatedOutPut.UseVisualStyleBackColor = true;
            // 
            // blobStrageForm1
            // 
            this.blobStrageForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(897, 654);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(529, 210);
            this.blobStrageForm1.StrageId = null;
            this.blobStrageForm1.TabIndex = 55;
            // 
            // InvoicedAmountDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1428, 921);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.btnDesignatedOutPut);
            this.Controls.Add(this.gridPayment);
            this.Controls.Add(this.gridCompany);
            this.Controls.Add(this.gridInvoiceTotal);
            this.Controls.Add(this.lblCompName);
            this.Controls.Add(this.lblCompCode);
            this.Controls.Add(this.gridInvoiceDetail);
            this.Controls.Add(this.blobStrageForm1);
            this.Controls.Add(this.groupInvoiceState);
            this.Controls.Add(this.lblInvoiceTotal);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdateInvoice);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblInvoice);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 960);
            this.Name = "InvoicedAmountDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "入金照合";
            this.Load += new System.EventHandler(this.InvoiceDetail_Load);
            this.groupInvoiceState.ResumeLayout(false);
            this.groupInvoiceState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdateInvoice;
        private System.Windows.Forms.Label lblInvoiceTotal;
        private System.Windows.Forms.GroupBox groupInvoiceState;
        private System.Windows.Forms.RadioButton radioUnbilled;
        private System.Windows.Forms.RadioButton radioCompleted;
        private System.Windows.Forms.RadioButton radioPaymentReceived;
        private System.Windows.Forms.RadioButton radioInvoiceSent;
        private System.Windows.Forms.RadioButton radioInvoiceIssued;
        private CustomControls.BlobStrage.BlobStrageForm blobStrageForm1;
        private C1.Win.C1FlexGrid.C1FlexGrid gridInvoiceDetail;
        private System.Windows.Forms.Label lblCompCode;
        private System.Windows.Forms.Label lblCompName;
        private C1.Win.C1FlexGrid.C1FlexGrid gridInvoiceTotal;
        private C1.Win.C1FlexGrid.C1FlexGrid gridCompany;
        private C1.Win.C1FlexGrid.C1FlexGrid gridPayment;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnDesignatedOutPut;
    }
}
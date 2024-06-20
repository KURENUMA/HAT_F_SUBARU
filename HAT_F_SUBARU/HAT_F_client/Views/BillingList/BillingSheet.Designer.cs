namespace HatFClient.Views.BillingList
{
    partial class BillingSheet
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
            this.lblORDER_NO = new System.Windows.Forms.Label();
            this.lblORDER_TOTAL_AMOUNT = new System.Windows.Forms.Label();
            this.lblCONSUMPTION_TAX_AMOUNT = new System.Windows.Forms.Label();
            this.lblSUPPLIER_PAYMENT_MONTH = new System.Windows.Forms.Label();
            this.lblSUPPLIER_PAYMENT_DATE = new System.Windows.Forms.Label();
            this.lblSUPPLIER_PAYMENT_CATEGORY = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtroSUPPLIER_PAYMENT_CATEGORY = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroSUPPLIER_PAYMENT_DATE = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroSUPPLIER_PAYMENT_MONTH = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroCONSUMPTION_TAX_AMOUNT = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroORDER_TOTAL_AMOUNT = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroORDER_NO = new HatFClient.CustomControls.TextBoxReadOnly();
            this.textBoxReadOnly1 = new HatFClient.CustomControls.TextBoxReadOnly();
            this.cmbInvoiceConfirmation = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblORDER_NO
            // 
            this.lblORDER_NO.AutoSize = true;
            this.lblORDER_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblORDER_NO.Location = new System.Drawing.Point(20, 29);
            this.lblORDER_NO.Name = "lblORDER_NO";
            this.lblORDER_NO.Size = new System.Drawing.Size(69, 19);
            this.lblORDER_NO.TabIndex = 54;
            this.lblORDER_NO.Text = "受注番号";
            // 
            // lblORDER_TOTAL_AMOUNT
            // 
            this.lblORDER_TOTAL_AMOUNT.AutoSize = true;
            this.lblORDER_TOTAL_AMOUNT.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblORDER_TOTAL_AMOUNT.Location = new System.Drawing.Point(20, 73);
            this.lblORDER_TOTAL_AMOUNT.Name = "lblORDER_TOTAL_AMOUNT";
            this.lblORDER_TOTAL_AMOUNT.Size = new System.Drawing.Size(84, 19);
            this.lblORDER_TOTAL_AMOUNT.TabIndex = 115;
            this.lblORDER_TOTAL_AMOUNT.Text = "合計発注額";
            // 
            // lblCONSUMPTION_TAX_AMOUNT
            // 
            this.lblCONSUMPTION_TAX_AMOUNT.AutoSize = true;
            this.lblCONSUMPTION_TAX_AMOUNT.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCONSUMPTION_TAX_AMOUNT.Location = new System.Drawing.Point(242, 73);
            this.lblCONSUMPTION_TAX_AMOUNT.Name = "lblCONSUMPTION_TAX_AMOUNT";
            this.lblCONSUMPTION_TAX_AMOUNT.Size = new System.Drawing.Size(69, 19);
            this.lblCONSUMPTION_TAX_AMOUNT.TabIndex = 117;
            this.lblCONSUMPTION_TAX_AMOUNT.Text = "消費税額";
            // 
            // lblSUPPLIER_PAYMENT_MONTH
            // 
            this.lblSUPPLIER_PAYMENT_MONTH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSUPPLIER_PAYMENT_MONTH.AutoSize = true;
            this.lblSUPPLIER_PAYMENT_MONTH.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSUPPLIER_PAYMENT_MONTH.Location = new System.Drawing.Point(20, 393);
            this.lblSUPPLIER_PAYMENT_MONTH.Name = "lblSUPPLIER_PAYMENT_MONTH";
            this.lblSUPPLIER_PAYMENT_MONTH.Size = new System.Drawing.Size(54, 19);
            this.lblSUPPLIER_PAYMENT_MONTH.TabIndex = 119;
            this.lblSUPPLIER_PAYMENT_MONTH.Text = "支払月";
            // 
            // lblSUPPLIER_PAYMENT_DATE
            // 
            this.lblSUPPLIER_PAYMENT_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSUPPLIER_PAYMENT_DATE.AutoSize = true;
            this.lblSUPPLIER_PAYMENT_DATE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSUPPLIER_PAYMENT_DATE.Location = new System.Drawing.Point(20, 435);
            this.lblSUPPLIER_PAYMENT_DATE.Name = "lblSUPPLIER_PAYMENT_DATE";
            this.lblSUPPLIER_PAYMENT_DATE.Size = new System.Drawing.Size(54, 19);
            this.lblSUPPLIER_PAYMENT_DATE.TabIndex = 121;
            this.lblSUPPLIER_PAYMENT_DATE.Text = "支払日";
            // 
            // lblSUPPLIER_PAYMENT_CATEGORY
            // 
            this.lblSUPPLIER_PAYMENT_CATEGORY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSUPPLIER_PAYMENT_CATEGORY.AutoSize = true;
            this.lblSUPPLIER_PAYMENT_CATEGORY.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSUPPLIER_PAYMENT_CATEGORY.Location = new System.Drawing.Point(20, 477);
            this.lblSUPPLIER_PAYMENT_CATEGORY.Name = "lblSUPPLIER_PAYMENT_CATEGORY";
            this.lblSUPPLIER_PAYMENT_CATEGORY.Size = new System.Drawing.Size(69, 19);
            this.lblSUPPLIER_PAYMENT_CATEGORY.TabIndex = 123;
            this.lblSUPPLIER_PAYMENT_CATEGORY.Text = "支払方法";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(20, 119);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 21;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(548, 255);
            this.dgvList.TabIndex = 124;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(882, 503);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 125;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // txtroSUPPLIER_PAYMENT_CATEGORY
            // 
            this.txtroSUPPLIER_PAYMENT_CATEGORY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtroSUPPLIER_PAYMENT_CATEGORY.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroSUPPLIER_PAYMENT_CATEGORY.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroSUPPLIER_PAYMENT_CATEGORY.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroSUPPLIER_PAYMENT_CATEGORY.Location = new System.Drawing.Point(110, 474);
            this.txtroSUPPLIER_PAYMENT_CATEGORY.Name = "txtroSUPPLIER_PAYMENT_CATEGORY";
            this.txtroSUPPLIER_PAYMENT_CATEGORY.ReadOnly = true;
            this.txtroSUPPLIER_PAYMENT_CATEGORY.Size = new System.Drawing.Size(115, 27);
            this.txtroSUPPLIER_PAYMENT_CATEGORY.TabIndex = 122;
            this.txtroSUPPLIER_PAYMENT_CATEGORY.TabStop = false;
            this.txtroSUPPLIER_PAYMENT_CATEGORY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtroSUPPLIER_PAYMENT_DATE
            // 
            this.txtroSUPPLIER_PAYMENT_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtroSUPPLIER_PAYMENT_DATE.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroSUPPLIER_PAYMENT_DATE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroSUPPLIER_PAYMENT_DATE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroSUPPLIER_PAYMENT_DATE.Location = new System.Drawing.Point(110, 432);
            this.txtroSUPPLIER_PAYMENT_DATE.Name = "txtroSUPPLIER_PAYMENT_DATE";
            this.txtroSUPPLIER_PAYMENT_DATE.ReadOnly = true;
            this.txtroSUPPLIER_PAYMENT_DATE.Size = new System.Drawing.Size(115, 27);
            this.txtroSUPPLIER_PAYMENT_DATE.TabIndex = 120;
            this.txtroSUPPLIER_PAYMENT_DATE.TabStop = false;
            this.txtroSUPPLIER_PAYMENT_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtroSUPPLIER_PAYMENT_MONTH
            // 
            this.txtroSUPPLIER_PAYMENT_MONTH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtroSUPPLIER_PAYMENT_MONTH.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroSUPPLIER_PAYMENT_MONTH.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroSUPPLIER_PAYMENT_MONTH.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroSUPPLIER_PAYMENT_MONTH.Location = new System.Drawing.Point(110, 390);
            this.txtroSUPPLIER_PAYMENT_MONTH.Name = "txtroSUPPLIER_PAYMENT_MONTH";
            this.txtroSUPPLIER_PAYMENT_MONTH.ReadOnly = true;
            this.txtroSUPPLIER_PAYMENT_MONTH.Size = new System.Drawing.Size(115, 27);
            this.txtroSUPPLIER_PAYMENT_MONTH.TabIndex = 118;
            this.txtroSUPPLIER_PAYMENT_MONTH.TabStop = false;
            this.txtroSUPPLIER_PAYMENT_MONTH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtroCONSUMPTION_TAX_AMOUNT
            // 
            this.txtroCONSUMPTION_TAX_AMOUNT.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroCONSUMPTION_TAX_AMOUNT.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroCONSUMPTION_TAX_AMOUNT.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroCONSUMPTION_TAX_AMOUNT.Location = new System.Drawing.Point(315, 70);
            this.txtroCONSUMPTION_TAX_AMOUNT.Name = "txtroCONSUMPTION_TAX_AMOUNT";
            this.txtroCONSUMPTION_TAX_AMOUNT.ReadOnly = true;
            this.txtroCONSUMPTION_TAX_AMOUNT.Size = new System.Drawing.Size(124, 27);
            this.txtroCONSUMPTION_TAX_AMOUNT.TabIndex = 116;
            this.txtroCONSUMPTION_TAX_AMOUNT.TabStop = false;
            this.txtroCONSUMPTION_TAX_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroORDER_TOTAL_AMOUNT
            // 
            this.txtroORDER_TOTAL_AMOUNT.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroORDER_TOTAL_AMOUNT.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroORDER_TOTAL_AMOUNT.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroORDER_TOTAL_AMOUNT.Location = new System.Drawing.Point(110, 70);
            this.txtroORDER_TOTAL_AMOUNT.Name = "txtroORDER_TOTAL_AMOUNT";
            this.txtroORDER_TOTAL_AMOUNT.ReadOnly = true;
            this.txtroORDER_TOTAL_AMOUNT.Size = new System.Drawing.Size(124, 27);
            this.txtroORDER_TOTAL_AMOUNT.TabIndex = 114;
            this.txtroORDER_TOTAL_AMOUNT.TabStop = false;
            this.txtroORDER_TOTAL_AMOUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroORDER_NO
            // 
            this.txtroORDER_NO.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroORDER_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroORDER_NO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroORDER_NO.Location = new System.Drawing.Point(110, 26);
            this.txtroORDER_NO.Name = "txtroORDER_NO";
            this.txtroORDER_NO.ReadOnly = true;
            this.txtroORDER_NO.Size = new System.Drawing.Size(121, 27);
            this.txtroORDER_NO.TabIndex = 53;
            this.txtroORDER_NO.TabStop = false;
            this.txtroORDER_NO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxReadOnly1
            // 
            this.textBoxReadOnly1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReadOnly1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBoxReadOnly1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxReadOnly1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxReadOnly1.Location = new System.Drawing.Point(605, 73);
            this.textBoxReadOnly1.Multiline = true;
            this.textBoxReadOnly1.Name = "textBoxReadOnly1";
            this.textBoxReadOnly1.ReadOnly = true;
            this.textBoxReadOnly1.Size = new System.Drawing.Size(397, 423);
            this.textBoxReadOnly1.TabIndex = 126;
            this.textBoxReadOnly1.TabStop = false;
            this.textBoxReadOnly1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbInvoiceConfirmation
            // 
            this.cmbInvoiceConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInvoiceConfirmation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceConfirmation.FormattingEnabled = true;
            this.cmbInvoiceConfirmation.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cmbInvoiceConfirmation.Location = new System.Drawing.Point(605, 510);
            this.cmbInvoiceConfirmation.Name = "cmbInvoiceConfirmation";
            this.cmbInvoiceConfirmation.Size = new System.Drawing.Size(150, 23);
            this.cmbInvoiceConfirmation.TabIndex = 127;
            // 
            // BillingSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1031, 556);
            this.Controls.Add(this.cmbInvoiceConfirmation);
            this.Controls.Add(this.textBoxReadOnly1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.lblSUPPLIER_PAYMENT_CATEGORY);
            this.Controls.Add(this.txtroSUPPLIER_PAYMENT_CATEGORY);
            this.Controls.Add(this.lblSUPPLIER_PAYMENT_DATE);
            this.Controls.Add(this.txtroSUPPLIER_PAYMENT_DATE);
            this.Controls.Add(this.lblSUPPLIER_PAYMENT_MONTH);
            this.Controls.Add(this.txtroSUPPLIER_PAYMENT_MONTH);
            this.Controls.Add(this.lblCONSUMPTION_TAX_AMOUNT);
            this.Controls.Add(this.txtroCONSUMPTION_TAX_AMOUNT);
            this.Controls.Add(this.lblORDER_TOTAL_AMOUNT);
            this.Controls.Add(this.txtroORDER_TOTAL_AMOUNT);
            this.Controls.Add(this.lblORDER_NO);
            this.Controls.Add(this.txtroORDER_NO);
            this.Name = "BillingSheet";
            this.Text = "発注明細";
            this.Load += new System.EventHandler(this.BillingSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblORDER_NO;
        private CustomControls.TextBoxReadOnly txtroORDER_NO;
        private System.Windows.Forms.Label lblORDER_TOTAL_AMOUNT;
        private CustomControls.TextBoxReadOnly txtroORDER_TOTAL_AMOUNT;
        private System.Windows.Forms.Label lblCONSUMPTION_TAX_AMOUNT;
        private CustomControls.TextBoxReadOnly txtroCONSUMPTION_TAX_AMOUNT;
        private System.Windows.Forms.Label lblSUPPLIER_PAYMENT_MONTH;
        private CustomControls.TextBoxReadOnly txtroSUPPLIER_PAYMENT_MONTH;
        private System.Windows.Forms.Label lblSUPPLIER_PAYMENT_DATE;
        private CustomControls.TextBoxReadOnly txtroSUPPLIER_PAYMENT_DATE;
        private System.Windows.Forms.Label lblSUPPLIER_PAYMENT_CATEGORY;
        private CustomControls.TextBoxReadOnly txtroSUPPLIER_PAYMENT_CATEGORY;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnClose;
        private CustomControls.TextBoxReadOnly textBoxReadOnly1;
        private System.Windows.Forms.ComboBox cmbInvoiceConfirmation;
    }
}
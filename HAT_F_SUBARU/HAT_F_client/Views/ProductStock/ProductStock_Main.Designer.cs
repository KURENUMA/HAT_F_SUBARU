namespace HatFClient.Views.ProductStock
{
    partial class ProductStock_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductStock_Main));
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblBeginningBalance = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pager = new templateApp.Controls.Pagination();
            this.txtroCount = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroBalance = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroPrice = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroStockOutputTotal = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroStockReceiptTotal = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroBeginningBalance = new HatFClient.CustomControls.TextBoxReadOnly();
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
            this.grdList.TabIndex = 0;
            this.grdList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdList_AfterEdit);
            // 
            // btnFnc12
            // 
            this.btnFnc12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(1136, 12);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(120, 30);
            this.btnFnc12.TabIndex = 2;
            this.btnFnc12.TabStop = false;
            this.btnFnc12.Text = "F12:閉じる";
            this.btnFnc12.UseVisualStyleBackColor = true;
            this.btnFnc12.Click += new System.EventHandler(this.BtnFnc12_Click);
            // 
            // btnFnc11
            // 
            this.btnFnc11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc11.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc11.Location = new System.Drawing.Point(1002, 12);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(120, 30);
            this.btnFnc11.TabIndex = 1;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Visible = false;
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
            // lblBeginningBalance
            // 
            this.lblBeginningBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBeginningBalance.AutoSize = true;
            this.lblBeginningBalance.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBeginningBalance.Location = new System.Drawing.Point(498, 859);
            this.lblBeginningBalance.Name = "lblBeginningBalance";
            this.lblBeginningBalance.Size = new System.Drawing.Size(69, 19);
            this.lblBeginningBalance.TabIndex = 141;
            this.lblBeginningBalance.Text = "期首残高";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(640, 859);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 142;
            this.label1.Text = "入庫数量計";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(782, 859);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 143;
            this.label2.Text = "出庫数量計";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(928, 859);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 144;
            this.label3.Text = "期末残高";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(1132, 859);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 145;
            this.label4.Text = "金額";
            // 
            // pager
            // 
            this.pager.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pager.Location = new System.Drawing.Point(132, 877);
            this.pager.Margin = new System.Windows.Forms.Padding(4);
            this.pager.Name = "pager";
            this.pager.Size = new System.Drawing.Size(359, 34);
            this.pager.TabIndex = 146;
            this.pager.TotalPages = 0;
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
            // txtroBalance
            // 
            this.txtroBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtroBalance.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroBalance.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroBalance.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroBalance.Location = new System.Drawing.Point(928, 882);
            this.txtroBalance.Name = "txtroBalance";
            this.txtroBalance.ReadOnly = true;
            this.txtroBalance.Size = new System.Drawing.Size(124, 27);
            this.txtroBalance.TabIndex = 138;
            this.txtroBalance.TabStop = false;
            this.txtroBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroPrice
            // 
            this.txtroPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtroPrice.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroPrice.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroPrice.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroPrice.Location = new System.Drawing.Point(1132, 882);
            this.txtroPrice.Name = "txtroPrice";
            this.txtroPrice.ReadOnly = true;
            this.txtroPrice.Size = new System.Drawing.Size(124, 27);
            this.txtroPrice.TabIndex = 137;
            this.txtroPrice.TabStop = false;
            this.txtroPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroStockOutputTotal
            // 
            this.txtroStockOutputTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtroStockOutputTotal.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroStockOutputTotal.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroStockOutputTotal.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroStockOutputTotal.Location = new System.Drawing.Point(782, 882);
            this.txtroStockOutputTotal.Name = "txtroStockOutputTotal";
            this.txtroStockOutputTotal.ReadOnly = true;
            this.txtroStockOutputTotal.Size = new System.Drawing.Size(124, 27);
            this.txtroStockOutputTotal.TabIndex = 136;
            this.txtroStockOutputTotal.TabStop = false;
            this.txtroStockOutputTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroStockReceiptTotal
            // 
            this.txtroStockReceiptTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtroStockReceiptTotal.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroStockReceiptTotal.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroStockReceiptTotal.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroStockReceiptTotal.Location = new System.Drawing.Point(640, 882);
            this.txtroStockReceiptTotal.Name = "txtroStockReceiptTotal";
            this.txtroStockReceiptTotal.ReadOnly = true;
            this.txtroStockReceiptTotal.Size = new System.Drawing.Size(124, 27);
            this.txtroStockReceiptTotal.TabIndex = 135;
            this.txtroStockReceiptTotal.TabStop = false;
            this.txtroStockReceiptTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroBeginningBalance
            // 
            this.txtroBeginningBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtroBeginningBalance.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroBeginningBalance.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtroBeginningBalance.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroBeginningBalance.Location = new System.Drawing.Point(498, 882);
            this.txtroBeginningBalance.Name = "txtroBeginningBalance";
            this.txtroBeginningBalance.ReadOnly = true;
            this.txtroBeginningBalance.Size = new System.Drawing.Size(124, 27);
            this.txtroBeginningBalance.TabIndex = 134;
            this.txtroBeginningBalance.TabStop = false;
            this.txtroBeginningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ProductStock_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.pager);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBeginningBalance);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtroCount);
            this.Controls.Add(this.txtroBalance);
            this.Controls.Add(this.txtroPrice);
            this.Controls.Add(this.txtroStockOutputTotal);
            this.Controls.Add(this.txtroStockReceiptTotal);
            this.Controls.Add(this.txtroBeginningBalance);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.Controls.Add(this.grdList);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProductStock_Main";
            this.Text = "在庫一覧表";
            this.Load += new System.EventHandler(this.ProductStock_Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductStock_Main_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.Button btnFnc12;
        private System.Windows.Forms.Button btnFnc11;
        private CustomControls.TextBoxReadOnly txtroBeginningBalance;
        private CustomControls.TextBoxReadOnly txtroStockReceiptTotal;
        private CustomControls.TextBoxReadOnly txtroStockOutputTotal;
        private CustomControls.TextBoxReadOnly txtroPrice;
        private CustomControls.TextBoxReadOnly txtroBalance;
        private CustomControls.TextBoxReadOnly txtroCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblBeginningBalance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private templateApp.Controls.Pagination pager;
    }
}
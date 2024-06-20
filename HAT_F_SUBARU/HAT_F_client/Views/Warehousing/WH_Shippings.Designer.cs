namespace HatFClient.Views.Warehousing
{
    partial class WH_Shippings
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rowsCount = new System.Windows.Forms.Label();
            this.btnExcel出力 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textFilterStr = new System.Windows.Forms.TextBox();
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnShipping = new System.Windows.Forms.Button();
            this.chkContainsPrinted = new System.Windows.Forms.CheckBox();
            this.pnlShipping = new System.Windows.Forms.Panel();
            this.dtpShippedTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpShippedFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchPrinted = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gridPatternUI = new HatFClient.CustomControls.GridPatternUI();
            this.pnlShipping.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(488, 82);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 130);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 780);
            this.panel1.TabIndex = 23;
            // 
            // rowsCount
            // 
            this.rowsCount.AutoSize = true;
            this.rowsCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rowsCount.Location = new System.Drawing.Point(14, 86);
            this.rowsCount.Name = "rowsCount";
            this.rowsCount.Size = new System.Drawing.Size(74, 21);
            this.rowsCount.TabIndex = 22;
            this.rowsCount.Text = "表示件数";
            // 
            // btnExcel出力
            // 
            this.btnExcel出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel出力.Location = new System.Drawing.Point(1152, 88);
            this.btnExcel出力.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcel出力.Name = "btnExcel出力";
            this.btnExcel出力.Size = new System.Drawing.Size(103, 23);
            this.btnExcel出力.TabIndex = 38;
            this.btnExcel出力.Text = "Excel印刷";
            this.btnExcel出力.UseVisualStyleBackColor = true;
            this.btnExcel出力.Click += new System.EventHandler(this.btnExcel出力_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(-225, -80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 36;
            this.label1.Text = "検索条件";
            // 
            // textFilterStr
            // 
            this.textFilterStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilterStr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textFilterStr.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.textFilterStr.Location = new System.Drawing.Point(588, 77);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(175, 34);
            this.textFilterStr.TabIndex = 35;
            this.textFilterStr.Visible = false;
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(14, 57);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(90, 21);
            this.lblProjectAllCount.TabIndex = 34;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Location = new System.Drawing.Point(934, 88);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(103, 23);
            this.btnDetail.TabIndex = 41;
            this.btnDetail.Text = "詳細";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnShipping
            // 
            this.btnShipping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShipping.Location = new System.Drawing.Point(1043, 88);
            this.btnShipping.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShipping.Name = "btnShipping";
            this.btnShipping.Size = new System.Drawing.Size(103, 23);
            this.btnShipping.TabIndex = 42;
            this.btnShipping.Text = "出荷指示書印刷";
            this.btnShipping.UseVisualStyleBackColor = true;
            this.btnShipping.Click += new System.EventHandler(this.btnShipping_Click);
            // 
            // chkContainsPrinted
            // 
            this.chkContainsPrinted.AutoSize = true;
            this.chkContainsPrinted.Location = new System.Drawing.Point(3, 3);
            this.chkContainsPrinted.Name = "chkContainsPrinted";
            this.chkContainsPrinted.Size = new System.Drawing.Size(153, 16);
            this.chkContainsPrinted.TabIndex = 44;
            this.chkContainsPrinted.Text = "出荷指示書印刷済も表示";
            this.chkContainsPrinted.UseVisualStyleBackColor = true;
            this.chkContainsPrinted.CheckedChanged += new System.EventHandler(this.chkContainsPrinted_CheckedChanged);
            // 
            // pnlShipping
            // 
            this.pnlShipping.Controls.Add(this.dtpShippedTo);
            this.pnlShipping.Controls.Add(this.label4);
            this.pnlShipping.Controls.Add(this.dtpShippedFrom);
            this.pnlShipping.Controls.Add(this.label3);
            this.pnlShipping.Location = new System.Drawing.Point(162, 3);
            this.pnlShipping.Name = "pnlShipping";
            this.pnlShipping.Size = new System.Drawing.Size(359, 23);
            this.pnlShipping.TabIndex = 50;
            this.pnlShipping.Visible = false;
            // 
            // dtpShippedTo
            // 
            this.dtpShippedTo.Location = new System.Drawing.Point(212, 0);
            this.dtpShippedTo.Name = "dtpShippedTo";
            this.dtpShippedTo.Size = new System.Drawing.Size(129, 19);
            this.dtpShippedTo.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "～";
            // 
            // dtpShippedFrom
            // 
            this.dtpShippedFrom.Location = new System.Drawing.Point(54, 0);
            this.dtpShippedFrom.Name = "dtpShippedFrom";
            this.dtpShippedFrom.Size = new System.Drawing.Size(129, 19);
            this.dtpShippedFrom.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "出荷日";
            // 
            // btnSearchPrinted
            // 
            this.btnSearchPrinted.Location = new System.Drawing.Point(527, 3);
            this.btnSearchPrinted.Name = "btnSearchPrinted";
            this.btnSearchPrinted.Size = new System.Drawing.Size(75, 23);
            this.btnSearchPrinted.TabIndex = 54;
            this.btnSearchPrinted.Text = "検索";
            this.btnSearchPrinted.UseVisualStyleBackColor = true;
            this.btnSearchPrinted.Click += new System.EventHandler(this.btnSearchPrinted_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chkContainsPrinted);
            this.flowLayoutPanel1.Controls.Add(this.pnlShipping);
            this.flowLayoutPanel1.Controls.Add(this.btnSearchPrinted);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(627, 31);
            this.flowLayoutPanel1.TabIndex = 55;
            // 
            // gridPatternUI
            // 
            this.gridPatternUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatternUI.Location = new System.Drawing.Point(811, 12);
            this.gridPatternUI.Name = "gridPatternUI";
            this.gridPatternUI.Size = new System.Drawing.Size(444, 71);
            this.gridPatternUI.TabIndex = 39;
            // 
            // WH_Shippings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnShipping);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.gridPatternUI);
            this.Controls.Add(this.rowsCount);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcel出力);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 960);
            this.Name = "WH_Shippings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出庫指示一覧";
            this.Load += new System.EventHandler(this.WH_Shippings_Load);
            this.pnlShipping.ResumeLayout(false);
            this.pnlShipping.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label rowsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFilterStr;
        private System.Windows.Forms.Button btnExcel出力;
        private System.Windows.Forms.Label lblProjectAllCount;
        private CustomControls.GridPatternUI gridPatternUI;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnShipping;
        private System.Windows.Forms.CheckBox chkContainsPrinted;
        private System.Windows.Forms.Panel pnlShipping;
        private System.Windows.Forms.DateTimePicker dtpShippedTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpShippedFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchPrinted;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
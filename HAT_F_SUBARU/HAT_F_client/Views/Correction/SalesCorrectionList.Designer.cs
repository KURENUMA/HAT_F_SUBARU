namespace HatFClient.Views.SalesCorrection
{
    partial class SalesCorrectionList
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
            this.gridPatternUI = new HatFClient.CustomControls.GridPatternUI();
            this.btnDetail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(18, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 124);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 786);
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
            this.btnExcel出力.Location = new System.Drawing.Point(1127, 88);
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
            this.textFilterStr.Location = new System.Drawing.Point(222, 17);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(515, 90);
            this.textFilterStr.TabIndex = 35;
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
            // gridPatternUI
            // 
            this.gridPatternUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatternUI.Location = new System.Drawing.Point(786, 12);
            this.gridPatternUI.Name = "gridPatternUI";
            this.gridPatternUI.Size = new System.Drawing.Size(444, 71);
            this.gridPatternUI.TabIndex = 39;
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Location = new System.Drawing.Point(1018, 88);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(103, 23);
            this.btnDetail.TabIndex = 41;
            this.btnDetail.Text = "詳細";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // SalesCorrectionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.gridPatternUI);
            this.Controls.Add(this.rowsCount);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcel出力);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 766);
            this.Name = "SalesCorrectionList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "売上訂正一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SalesCorrectionList_FormClosed);
            this.Load += new System.EventHandler(this.SalesCorrectionList_Load);
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
    }
}
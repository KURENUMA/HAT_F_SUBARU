namespace HatFClient.Views.Warehousing
{
    partial class WH_StockRefill
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
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProdCode = new System.Windows.Forms.TextBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoOrderedAll = new System.Windows.Forms.RadioButton();
            this.rdoOrderedHide = new System.Windows.Forms.RadioButton();
            this.pnlConditions = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefillSettings = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlConditions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(347, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "表示";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 241);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 669);
            this.panel1.TabIndex = 23;
            // 
            // rowsCount
            // 
            this.rowsCount.AutoSize = true;
            this.rowsCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.rowsCount.Location = new System.Drawing.Point(8, 216);
            this.rowsCount.Name = "rowsCount";
            this.rowsCount.Size = new System.Drawing.Size(74, 21);
            this.rowsCount.TabIndex = 22;
            this.rowsCount.Text = "表示件数";
            // 
            // btnExcel出力
            // 
            this.btnExcel出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel出力.Location = new System.Drawing.Point(1151, 214);
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
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(8, 187);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(90, 21);
            this.lblProjectAllCount.TabIndex = 34;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 56;
            this.label2.Text = "倉庫";
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(75, 21);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(253, 20);
            this.cboWarehouse.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 59;
            this.label3.Text = "絞込";
            // 
            // txtProdCode
            // 
            this.txtProdCode.Location = new System.Drawing.Point(77, 58);
            this.txtProdCode.Name = "txtProdCode";
            this.txtProdCode.Size = new System.Drawing.Size(150, 19);
            this.txtProdCode.TabIndex = 60;
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.Location = new System.Drawing.Point(1042, 214);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(103, 23);
            this.btnOrder.TabIndex = 61;
            this.btnOrder.Text = "選択商品を発注";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 62;
            this.label4.Text = "商品コード";
            // 
            // rdoOrderedAll
            // 
            this.rdoOrderedAll.AutoSize = true;
            this.rdoOrderedAll.Checked = true;
            this.rdoOrderedAll.Location = new System.Drawing.Point(77, 18);
            this.rdoOrderedAll.Name = "rdoOrderedAll";
            this.rdoOrderedAll.Size = new System.Drawing.Size(47, 16);
            this.rdoOrderedAll.TabIndex = 63;
            this.rdoOrderedAll.TabStop = true;
            this.rdoOrderedAll.Text = "表示";
            this.rdoOrderedAll.UseVisualStyleBackColor = true;
            // 
            // rdoOrderedHide
            // 
            this.rdoOrderedHide.AutoSize = true;
            this.rdoOrderedHide.Location = new System.Drawing.Point(141, 18);
            this.rdoOrderedHide.Name = "rdoOrderedHide";
            this.rdoOrderedHide.Size = new System.Drawing.Size(59, 16);
            this.rdoOrderedHide.TabIndex = 64;
            this.rdoOrderedHide.Text = "非表示";
            this.rdoOrderedHide.UseVisualStyleBackColor = true;
            // 
            // pnlConditions
            // 
            this.pnlConditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConditions.Controls.Add(this.label5);
            this.pnlConditions.Controls.Add(this.rdoOrderedHide);
            this.pnlConditions.Controls.Add(this.rdoOrderedAll);
            this.pnlConditions.Controls.Add(this.label4);
            this.pnlConditions.Controls.Add(this.txtProdCode);
            this.pnlConditions.Location = new System.Drawing.Point(75, 57);
            this.pnlConditions.Name = "pnlConditions";
            this.pnlConditions.Size = new System.Drawing.Size(253, 93);
            this.pnlConditions.TabIndex = 66;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 65;
            this.label5.Text = "発注済分";
            // 
            // btnRefillSettings
            // 
            this.btnRefillSettings.Location = new System.Drawing.Point(1124, 12);
            this.btnRefillSettings.Name = "btnRefillSettings";
            this.btnRefillSettings.Size = new System.Drawing.Size(133, 28);
            this.btnRefillSettings.TabIndex = 67;
            this.btnRefillSettings.Text = "補充条件設定";
            this.btnRefillSettings.UseVisualStyleBackColor = true;
            this.btnRefillSettings.Click += new System.EventHandler(this.btnRefillSettings_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(316, 12);
            this.label6.TabIndex = 68;
            this.label6.Text = "※商品発注後、入庫処理されるとこの一覧に表示されなくなります";
            // 
            // WH_StockRefill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRefillSettings);
            this.Controls.Add(this.cboWarehouse);
            this.Controls.Add(this.pnlConditions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rowsCount);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcel出力);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 960);
            this.Name = "WH_StockRefill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "在庫補充リスト";
            this.Load += new System.EventHandler(this.WH_Shippings_Load);
            this.pnlConditions.ResumeLayout(false);
            this.pnlConditions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label rowsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExcel出力;
        private System.Windows.Forms.Label lblProjectAllCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProdCode;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoOrderedAll;
        private System.Windows.Forms.RadioButton rdoOrderedHide;
        private System.Windows.Forms.Panel pnlConditions;
        private System.Windows.Forms.Button btnRefillSettings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
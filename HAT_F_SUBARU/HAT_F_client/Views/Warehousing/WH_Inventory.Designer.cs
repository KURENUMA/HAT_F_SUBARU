namespace HatFClient.Views.MasterEdit
{
    partial class WH_Inventory
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
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCreateInventoryStockInfo = new System.Windows.Forms.Button();
            this.btnOutputInventoryList = new System.Windows.Forms.Button();
            this.cboYearMonth = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoFilterAll = new System.Windows.Forms.RadioButton();
            this.rdoFilterDiff = new System.Windows.Forms.RadioButton();
            this.rdoFilterNoDiff = new System.Windows.Forms.RadioButton();
            this.rdoFilterAbnormalClass = new System.Windows.Forms.RadioButton();
            this.rdoFilterAbnormalProduct = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadAmazon = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProdCode = new System.Windows.Forms.TextBox();
            this.pnlFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(422, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(161, 30);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "棚卸用データ検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 270);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 640);
            this.panel1.TabIndex = 23;
            // 
            // rowsCount
            // 
            this.rowsCount.AutoSize = true;
            this.rowsCount.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rowsCount.Location = new System.Drawing.Point(20, 253);
            this.rowsCount.Name = "rowsCount";
            this.rowsCount.Size = new System.Drawing.Size(67, 15);
            this.rowsCount.TabIndex = 22;
            this.rowsCount.Text = "表示件数";
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(21, 228);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(75, 15);
            this.lblProjectAllCount.TabIndex = 34;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 60;
            this.label2.Text = "倉庫";
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(78, 41);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(156, 20);
            this.cboWarehouse.TabIndex = 61;
            this.cboWarehouse.SelectedIndexChanged += new System.EventHandler(this.cboWarehouse_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "絞込";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 64;
            this.label4.Text = "棚卸年月";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1132, 247);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 35);
            this.btnSave.TabIndex = 66;
            this.btnSave.Text = "棚卸入力数保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCreateInventoryStockInfo
            // 
            this.btnCreateInventoryStockInfo.Location = new System.Drawing.Point(422, 54);
            this.btnCreateInventoryStockInfo.Name = "btnCreateInventoryStockInfo";
            this.btnCreateInventoryStockInfo.Size = new System.Drawing.Size(161, 30);
            this.btnCreateInventoryStockInfo.TabIndex = 67;
            this.btnCreateInventoryStockInfo.Text = "棚卸用データ作成";
            this.btnCreateInventoryStockInfo.UseVisualStyleBackColor = true;
            this.btnCreateInventoryStockInfo.Click += new System.EventHandler(this.btnCreateInventoryStockInfo_Click);
            // 
            // btnOutputInventoryList
            // 
            this.btnOutputInventoryList.Location = new System.Drawing.Point(422, 90);
            this.btnOutputInventoryList.Name = "btnOutputInventoryList";
            this.btnOutputInventoryList.Size = new System.Drawing.Size(161, 30);
            this.btnOutputInventoryList.TabIndex = 68;
            this.btnOutputInventoryList.Text = "棚卸記入用紙出力";
            this.btnOutputInventoryList.UseVisualStyleBackColor = true;
            this.btnOutputInventoryList.Click += new System.EventHandler(this.btnOutputInventoryList_Click);
            // 
            // cboYearMonth
            // 
            this.cboYearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYearMonth.FormattingEnabled = true;
            this.cboYearMonth.Location = new System.Drawing.Point(78, 10);
            this.cboYearMonth.Name = "cboYearMonth";
            this.cboYearMonth.Size = new System.Drawing.Size(156, 20);
            this.cboYearMonth.TabIndex = 69;
            this.cboYearMonth.SelectedIndexChanged += new System.EventHandler(this.cboYearMonth_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(603, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(290, 12);
            this.label6.TabIndex = 71;
            this.label6.Text = "棚卸をするには、対象倉庫の棚卸用データを作成してください";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(603, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 12);
            this.label7.TabIndex = 72;
            this.label7.Text = "棚卸記入用紙出力を出力します";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.rdoFilterAll);
            this.pnlFilter.Controls.Add(this.rdoFilterDiff);
            this.pnlFilter.Controls.Add(this.rdoFilterNoDiff);
            this.pnlFilter.Controls.Add(this.rdoFilterAbnormalClass);
            this.pnlFilter.Controls.Add(this.rdoFilterAbnormalProduct);
            this.pnlFilter.Location = new System.Drawing.Point(78, 70);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(244, 50);
            this.pnlFilter.TabIndex = 74;
            // 
            // rdoFilterAll
            // 
            this.rdoFilterAll.AutoSize = true;
            this.rdoFilterAll.Checked = true;
            this.rdoFilterAll.Location = new System.Drawing.Point(3, 3);
            this.rdoFilterAll.Name = "rdoFilterAll";
            this.rdoFilterAll.Size = new System.Drawing.Size(47, 16);
            this.rdoFilterAll.TabIndex = 5;
            this.rdoFilterAll.TabStop = true;
            this.rdoFilterAll.Tag = "";
            this.rdoFilterAll.Text = "全件";
            this.rdoFilterAll.UseVisualStyleBackColor = true;
            // 
            // rdoFilterDiff
            // 
            this.rdoFilterDiff.AutoSize = true;
            this.rdoFilterDiff.Location = new System.Drawing.Point(56, 3);
            this.rdoFilterDiff.Name = "rdoFilterDiff";
            this.rdoFilterDiff.Size = new System.Drawing.Size(65, 16);
            this.rdoFilterDiff.TabIndex = 6;
            this.rdoFilterDiff.Text = "差異あり";
            this.rdoFilterDiff.UseVisualStyleBackColor = true;
            // 
            // rdoFilterNoDiff
            // 
            this.rdoFilterNoDiff.AutoSize = true;
            this.rdoFilterNoDiff.Location = new System.Drawing.Point(127, 3);
            this.rdoFilterNoDiff.Name = "rdoFilterNoDiff";
            this.rdoFilterNoDiff.Size = new System.Drawing.Size(66, 16);
            this.rdoFilterNoDiff.TabIndex = 7;
            this.rdoFilterNoDiff.Text = "差異なし";
            this.rdoFilterNoDiff.UseVisualStyleBackColor = true;
            // 
            // rdoFilterAbnormalClass
            // 
            this.rdoFilterAbnormalClass.AutoSize = true;
            this.rdoFilterAbnormalClass.Location = new System.Drawing.Point(3, 25);
            this.rdoFilterAbnormalClass.Name = "rdoFilterAbnormalClass";
            this.rdoFilterAbnormalClass.Size = new System.Drawing.Size(95, 16);
            this.rdoFilterAbnormalClass.TabIndex = 8;
            this.rdoFilterAbnormalClass.Text = "異常値 (分類)";
            this.rdoFilterAbnormalClass.UseVisualStyleBackColor = true;
            // 
            // rdoFilterAbnormalProduct
            // 
            this.rdoFilterAbnormalProduct.AutoSize = true;
            this.rdoFilterAbnormalProduct.Location = new System.Drawing.Point(104, 25);
            this.rdoFilterAbnormalProduct.Name = "rdoFilterAbnormalProduct";
            this.rdoFilterAbnormalProduct.Size = new System.Drawing.Size(95, 16);
            this.rdoFilterAbnormalProduct.TabIndex = 9;
            this.rdoFilterAbnormalProduct.Text = "異常値 (商品)";
            this.rdoFilterAbnormalProduct.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtProdCode);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cboYearMonth);
            this.panel2.Controls.Add(this.pnlFilter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cboWarehouse);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 175);
            this.panel2.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(79, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(231, 12);
            this.label8.TabIndex = 79;
            this.label8.Text = "※条件を変更する場合は検索しなおしてください";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "対象倉庫の棚卸用データが作成されていれば表示します";
            // 
            // btnLoadAmazon
            // 
            this.btnLoadAmazon.Location = new System.Drawing.Point(422, 126);
            this.btnLoadAmazon.Name = "btnLoadAmazon";
            this.btnLoadAmazon.Size = new System.Drawing.Size(161, 30);
            this.btnLoadAmazon.TabIndex = 77;
            this.btnLoadAmazon.Text = "Amazon棚卸ファイル読込";
            this.btnLoadAmazon.UseVisualStyleBackColor = true;
            this.btnLoadAmazon.Click += new System.EventHandler(this.btnLoadAmazon_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(603, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 12);
            this.label5.TabIndex = 78;
            this.label5.Text = "Amazon棚卸ファイルを読み込む画面を開きます";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(79, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "商品コード";
            // 
            // txtProdCode
            // 
            this.txtProdCode.Location = new System.Drawing.Point(141, 124);
            this.txtProdCode.Name = "txtProdCode";
            this.txtProdCode.Size = new System.Drawing.Size(136, 19);
            this.txtProdCode.TabIndex = 81;
            // 
            // WH_Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnLoadAmazon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOutputInventoryList);
            this.Controls.Add(this.btnCreateInventoryStockInfo);
            this.Controls.Add(this.rowsCount);
            this.Controls.Add(this.lblProjectAllCount);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 960);
            this.Name = "WH_Inventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "棚卸入力";
            this.Load += new System.EventHandler(this.ME_Supplier_Load);
            this.Shown += new System.EventHandler(this.WH_Inventory_Shown);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label rowsCount;
        private System.Windows.Forms.Label lblProjectAllCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCreateInventoryStockInfo;
        private System.Windows.Forms.Button btnOutputInventoryList;
        private System.Windows.Forms.ComboBox cboYearMonth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel pnlFilter;
        private System.Windows.Forms.RadioButton rdoFilterAll;
        private System.Windows.Forms.RadioButton rdoFilterDiff;
        private System.Windows.Forms.RadioButton rdoFilterNoDiff;
        private System.Windows.Forms.RadioButton rdoFilterAbnormalClass;
        private System.Windows.Forms.RadioButton rdoFilterAbnormalProduct;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadAmazon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProdCode;
        private System.Windows.Forms.Label label9;
    }
}
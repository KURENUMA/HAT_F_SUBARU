namespace HatFClient.Views.Warehousing
{
    partial class WH_InventoryAmazonImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WH_InventoryAmazonImport));
            this.c1gAmazon = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnOpenAmazonExcel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLoadItemCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWarehouse = new System.Windows.Forms.TextBox();
            this.txtYearMonth = new System.Windows.Forms.TextBox();
            this.rdoFilterAll = new System.Windows.Forms.RadioButton();
            this.rdoFilterErrorOnly = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1gAmazon)).BeginInit();
            this.SuspendLayout();
            // 
            // c1gAmazon
            // 
            this.c1gAmazon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1gAmazon.ColumnInfo = resources.GetString("c1gAmazon.ColumnInfo");
            this.c1gAmazon.Location = new System.Drawing.Point(12, 118);
            this.c1gAmazon.Name = "c1gAmazon";
            this.c1gAmazon.Size = new System.Drawing.Size(983, 428);
            this.c1gAmazon.TabIndex = 8;
            // 
            // btnOpenAmazonExcel
            // 
            this.btnOpenAmazonExcel.Location = new System.Drawing.Point(242, 12);
            this.btnOpenAmazonExcel.Name = "btnOpenAmazonExcel";
            this.btnOpenAmazonExcel.Size = new System.Drawing.Size(168, 34);
            this.btnOpenAmazonExcel.TabIndex = 4;
            this.btnOpenAmazonExcel.Text = "Amazon棚卸ファイルを開く";
            this.btnOpenAmazonExcel.UseVisualStyleBackColor = true;
            this.btnOpenAmazonExcel.Click += new System.EventHandler(this.btnOpenAmazonExcel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(689, 552);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 34);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "登録";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(845, 552);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 34);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblLoadItemCount
            // 
            this.lblLoadItemCount.AutoSize = true;
            this.lblLoadItemCount.Location = new System.Drawing.Point(12, 98);
            this.lblLoadItemCount.Name = "lblLoadItemCount";
            this.lblLoadItemCount.Size = new System.Drawing.Size(77, 12);
            this.lblLoadItemCount.TabIndex = 5;
            this.lblLoadItemCount.Text = "読込件数: 0件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "倉庫";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "棚卸年月";
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.Location = new System.Drawing.Point(91, 47);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.ReadOnly = true;
            this.txtWarehouse.Size = new System.Drawing.Size(121, 19);
            this.txtWarehouse.TabIndex = 3;
            // 
            // txtYearMonth
            // 
            this.txtYearMonth.Location = new System.Drawing.Point(91, 15);
            this.txtYearMonth.Name = "txtYearMonth";
            this.txtYearMonth.ReadOnly = true;
            this.txtYearMonth.Size = new System.Drawing.Size(121, 19);
            this.txtYearMonth.TabIndex = 1;
            // 
            // rdoFilterAll
            // 
            this.rdoFilterAll.AutoSize = true;
            this.rdoFilterAll.Checked = true;
            this.rdoFilterAll.Location = new System.Drawing.Point(164, 96);
            this.rdoFilterAll.Name = "rdoFilterAll";
            this.rdoFilterAll.Size = new System.Drawing.Size(47, 16);
            this.rdoFilterAll.TabIndex = 6;
            this.rdoFilterAll.TabStop = true;
            this.rdoFilterAll.Text = "全件";
            this.rdoFilterAll.UseVisualStyleBackColor = true;
            this.rdoFilterAll.CheckedChanged += new System.EventHandler(this.rdoFilterAll_CheckedChanged);
            // 
            // rdoFilterErrorOnly
            // 
            this.rdoFilterErrorOnly.AutoSize = true;
            this.rdoFilterErrorOnly.Location = new System.Drawing.Point(217, 96);
            this.rdoFilterErrorOnly.Name = "rdoFilterErrorOnly";
            this.rdoFilterErrorOnly.Size = new System.Drawing.Size(68, 16);
            this.rdoFilterErrorOnly.TabIndex = 7;
            this.rdoFilterErrorOnly.Text = "エラーあり";
            this.rdoFilterErrorOnly.UseVisualStyleBackColor = true;
            this.rdoFilterErrorOnly.CheckedChanged += new System.EventHandler(this.rdoFilterErrorOnly_CheckedChanged);
            // 
            // WH_InventoryAmazonImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 592);
            this.Controls.Add(this.rdoFilterErrorOnly);
            this.Controls.Add(this.rdoFilterAll);
            this.Controls.Add(this.txtYearMonth);
            this.Controls.Add(this.txtWarehouse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLoadItemCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOpenAmazonExcel);
            this.Controls.Add(this.c1gAmazon);
            this.Name = "WH_InventoryAmazonImport";
            this.Text = "Amazon棚卸ファイル(Excel)の読込";
            this.Load += new System.EventHandler(this.WH_InventoryAmazonImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1gAmazon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1gAmazon;
        private System.Windows.Forms.Button btnOpenAmazonExcel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLoadItemCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWarehouse;
        private System.Windows.Forms.TextBox txtYearMonth;
        private System.Windows.Forms.RadioButton rdoFilterAll;
        private System.Windows.Forms.RadioButton rdoFilterErrorOnly;
    }
}
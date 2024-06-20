namespace HatFClient.Views.Sales
{
    partial class SalesEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesEdit));
            this.c1FlexGridSalesEdit = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxHDIFF = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxM_TOTAL = new System.Windows.Forms.TextBox();
            this.textBoxH_TOTAL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonDiff = new System.Windows.Forms.Button();
            this.textBoxMDIFF = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCALCU = new System.Windows.Forms.Button();
            this.textBoxAfterM_TOTAL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAfterH_TOTAL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textboxH_NUMBER = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPU_NAME = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPU_CODE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_FILTER_RESET = new System.Windows.Forms.Button();
            this.buttonFILTER = new System.Windows.Forms.Button();
            this.FromDate = new C1.Win.Calendar.C1DateEdit();
            this.ToDate = new C1.Win.Calendar.C1DateEdit();
            this.lblLockInfo = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.buttonCONTACT_EMAIL = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxHATNUMBER = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemand = new System.Windows.Forms.Button();
            this.btnAapproval = new System.Windows.Forms.Button();
            this.btnApplication = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.CmbAuthorizer2 = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.CmbAuthorizer = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.txtApprovalStatus = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridSalesEdit)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlexGridSalesEdit
            // 
            this.c1FlexGridSalesEdit.AllowFiltering = true;
            this.c1FlexGridSalesEdit.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGridSalesEdit.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGridSalesEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGridSalesEdit.AutoGenerateColumns = false;
            this.c1FlexGridSalesEdit.ColumnInfo = resources.GetString("c1FlexGridSalesEdit.ColumnInfo");
            this.c1FlexGridSalesEdit.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c1FlexGridSalesEdit.Location = new System.Drawing.Point(12, 109);
            this.c1FlexGridSalesEdit.Name = "c1FlexGridSalesEdit";
            this.c1FlexGridSalesEdit.Size = new System.Drawing.Size(1280, 341);
            this.c1FlexGridSalesEdit.TabIndex = 16;
            this.c1FlexGridSalesEdit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGridSalesEdit_AfterEdit);
            this.c1FlexGridSalesEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1FlexGridSalesEdit_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBoxHDIFF);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBoxM_TOTAL);
            this.panel1.Controls.Add(this.textBoxH_TOTAL);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonDiff);
            this.panel1.Controls.Add(this.textBoxMDIFF);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonCALCU);
            this.panel1.Controls.Add(this.textBoxAfterM_TOTAL);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxAfterH_TOTAL);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(420, 456);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 67);
            this.panel1.TabIndex = 29;
            // 
            // textBoxHDIFF
            // 
            this.textBoxHDIFF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHDIFF.BackColor = System.Drawing.Color.LightGray;
            this.textBoxHDIFF.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxHDIFF.Location = new System.Drawing.Point(738, 37);
            this.textBoxHDIFF.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxHDIFF.Name = "textBoxHDIFF";
            this.textBoxHDIFF.ReadOnly = true;
            this.textBoxHDIFF.Size = new System.Drawing.Size(100, 23);
            this.textBoxHDIFF.TabIndex = 57;
            this.textBoxHDIFF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label12.Location = new System.Drawing.Point(645, 37);
            this.label12.MinimumSize = new System.Drawing.Size(0, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 20);
            this.label12.TabIndex = 56;
            this.label12.Text = "H合計金額差分";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxM_TOTAL
            // 
            this.textBoxM_TOTAL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxM_TOTAL.BackColor = System.Drawing.Color.LightGray;
            this.textBoxM_TOTAL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxM_TOTAL.Location = new System.Drawing.Point(317, 6);
            this.textBoxM_TOTAL.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxM_TOTAL.Name = "textBoxM_TOTAL";
            this.textBoxM_TOTAL.ReadOnly = true;
            this.textBoxM_TOTAL.Size = new System.Drawing.Size(100, 23);
            this.textBoxM_TOTAL.TabIndex = 55;
            this.textBoxM_TOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxH_TOTAL
            // 
            this.textBoxH_TOTAL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxH_TOTAL.BackColor = System.Drawing.Color.LightGray;
            this.textBoxH_TOTAL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxH_TOTAL.Location = new System.Drawing.Point(317, 37);
            this.textBoxH_TOTAL.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxH_TOTAL.Name = "textBoxH_TOTAL";
            this.textBoxH_TOTAL.ReadOnly = true;
            this.textBoxH_TOTAL.Size = new System.Drawing.Size(100, 23);
            this.textBoxH_TOTAL.TabIndex = 26;
            this.textBoxH_TOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(246, 8);
            this.label11.MinimumSize = new System.Drawing.Size(0, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 20);
            this.label11.TabIndex = 54;
            this.label11.Text = "M合計金額";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(246, 37);
            this.label3.MinimumSize = new System.Drawing.Size(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "H合計金額";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDiff
            // 
            this.buttonDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDiff.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonDiff.Location = new System.Drawing.Point(16, 15);
            this.buttonDiff.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonDiff.Name = "buttonDiff";
            this.buttonDiff.Size = new System.Drawing.Size(94, 34);
            this.buttonDiff.TabIndex = 24;
            this.buttonDiff.Text = "差分確認";
            this.buttonDiff.UseVisualStyleBackColor = true;
            this.buttonDiff.Click += new System.EventHandler(this.buttonDiff_Click);
            // 
            // textBoxMDIFF
            // 
            this.textBoxMDIFF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMDIFF.BackColor = System.Drawing.Color.LightGray;
            this.textBoxMDIFF.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxMDIFF.Location = new System.Drawing.Point(738, 6);
            this.textBoxMDIFF.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxMDIFF.Name = "textBoxMDIFF";
            this.textBoxMDIFF.ReadOnly = true;
            this.textBoxMDIFF.Size = new System.Drawing.Size(100, 23);
            this.textBoxMDIFF.TabIndex = 23;
            this.textBoxMDIFF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(643, 7);
            this.label1.MinimumSize = new System.Drawing.Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "M合計金額差分";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCALCU
            // 
            this.buttonCALCU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCALCU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCALCU.Location = new System.Drawing.Point(133, 15);
            this.buttonCALCU.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonCALCU.Name = "buttonCALCU";
            this.buttonCALCU.Size = new System.Drawing.Size(92, 36);
            this.buttonCALCU.TabIndex = 16;
            this.buttonCALCU.Text = "再計算";
            this.buttonCALCU.UseVisualStyleBackColor = true;
            this.buttonCALCU.Click += new System.EventHandler(this.buttonCALCU_Click);
            // 
            // textBoxAfterM_TOTAL
            // 
            this.textBoxAfterM_TOTAL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAfterM_TOTAL.BackColor = System.Drawing.Color.LightGray;
            this.textBoxAfterM_TOTAL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxAfterM_TOTAL.Location = new System.Drawing.Point(530, 5);
            this.textBoxAfterM_TOTAL.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxAfterM_TOTAL.Name = "textBoxAfterM_TOTAL";
            this.textBoxAfterM_TOTAL.ReadOnly = true;
            this.textBoxAfterM_TOTAL.Size = new System.Drawing.Size(100, 23);
            this.textBoxAfterM_TOTAL.TabIndex = 21;
            this.textBoxAfterM_TOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(423, 6);
            this.label9.MinimumSize = new System.Drawing.Size(0, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "変更後M合計金額";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxAfterH_TOTAL
            // 
            this.textBoxAfterH_TOTAL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAfterH_TOTAL.BackColor = System.Drawing.Color.LightGray;
            this.textBoxAfterH_TOTAL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxAfterH_TOTAL.Location = new System.Drawing.Point(530, 39);
            this.textBoxAfterH_TOTAL.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxAfterH_TOTAL.Name = "textBoxAfterH_TOTAL";
            this.textBoxAfterH_TOTAL.ReadOnly = true;
            this.textBoxAfterH_TOTAL.Size = new System.Drawing.Size(100, 23);
            this.textBoxAfterH_TOTAL.TabIndex = 19;
            this.textBoxAfterH_TOTAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(424, 37);
            this.label8.MinimumSize = new System.Drawing.Size(0, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "変更後H合計金額";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textboxH_NUMBER
            // 
            this.textboxH_NUMBER.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textboxH_NUMBER.Location = new System.Drawing.Point(502, 77);
            this.textboxH_NUMBER.MinimumSize = new System.Drawing.Size(4, 20);
            this.textboxH_NUMBER.Name = "textboxH_NUMBER";
            this.textboxH_NUMBER.Size = new System.Drawing.Size(148, 23);
            this.textboxH_NUMBER.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(456, 78);
            this.label2.MinimumSize = new System.Drawing.Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 39;
            this.label2.Text = "H注番";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPU_NAME
            // 
            this.textBoxPU_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPU_NAME.Location = new System.Drawing.Point(292, 77);
            this.textBoxPU_NAME.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxPU_NAME.Name = "textBoxPU_NAME";
            this.textBoxPU_NAME.Size = new System.Drawing.Size(148, 23);
            this.textBoxPU_NAME.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(231, 78);
            this.label7.MinimumSize = new System.Drawing.Size(0, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 20);
            this.label7.TabIndex = 38;
            this.label7.Text = "仕入先名";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(828, 78);
            this.label6.MinimumSize = new System.Drawing.Size(0, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 37;
            this.label6.Text = "～";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(676, 78);
            this.label5.MinimumSize = new System.Drawing.Size(0, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "H納日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPU_CODE
            // 
            this.textBoxPU_CODE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPU_CODE.Location = new System.Drawing.Point(108, 77);
            this.textBoxPU_CODE.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxPU_CODE.Name = "textBoxPU_CODE";
            this.textBoxPU_CODE.Size = new System.Drawing.Size(100, 23);
            this.textBoxPU_CODE.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 78);
            this.label4.MinimumSize = new System.Drawing.Size(0, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "仕入先コード";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_FILTER_RESET
            // 
            this.button_FILTER_RESET.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_FILTER_RESET.Location = new System.Drawing.Point(1050, 78);
            this.button_FILTER_RESET.MinimumSize = new System.Drawing.Size(0, 20);
            this.button_FILTER_RESET.Name = "button_FILTER_RESET";
            this.button_FILTER_RESET.Size = new System.Drawing.Size(75, 23);
            this.button_FILTER_RESET.TabIndex = 43;
            this.button_FILTER_RESET.Text = "絞込解除";
            this.button_FILTER_RESET.UseVisualStyleBackColor = true;
            this.button_FILTER_RESET.Click += new System.EventHandler(this.button_FILTER_RESET_Click);
            // 
            // buttonFILTER
            // 
            this.buttonFILTER.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonFILTER.Location = new System.Drawing.Point(969, 78);
            this.buttonFILTER.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonFILTER.Name = "buttonFILTER";
            this.buttonFILTER.Size = new System.Drawing.Size(75, 23);
            this.buttonFILTER.TabIndex = 42;
            this.buttonFILTER.Text = "絞込";
            this.buttonFILTER.UseVisualStyleBackColor = true;
            this.buttonFILTER.Click += new System.EventHandler(this.buttonFILTER_Click);
            // 
            // FromDate
            // 
            this.FromDate.AutoSize = false;
            this.FromDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.FromDate.CustomFormat = "yyyy/MM/dd";
            this.FromDate.EmptyAsNull = true;
            this.FromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.FromDate.GapHeight = 0;
            this.FromDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.FromDate.Location = new System.Drawing.Point(722, 78);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(100, 23);
            this.FromDate.TabIndex = 44;
            this.FromDate.Tag = null;
            this.FromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // ToDate
            // 
            this.ToDate.AutoSize = false;
            this.ToDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.ToDate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ToDate.CustomFormat = "yyyy/MM/dd";
            this.ToDate.EmptyAsNull = true;
            this.ToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.ToDate.GapHeight = 0;
            this.ToDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.ToDate.Location = new System.Drawing.Point(853, 77);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(100, 23);
            this.ToDate.TabIndex = 45;
            this.ToDate.Tag = null;
            this.ToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblLockInfo
            // 
            this.lblLockInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockInfo.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.lblLockInfo.Location = new System.Drawing.Point(939, 29);
            this.lblLockInfo.Name = "lblLockInfo";
            this.lblLockInfo.Size = new System.Drawing.Size(218, 42);
            this.lblLockInfo.TabIndex = 48;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.btnUnlock.Location = new System.Drawing.Point(1172, 36);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(120, 23);
            this.btnUnlock.TabIndex = 47;
            this.btnUnlock.Text = "読み取り専用解除";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Visible = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // buttonCONTACT_EMAIL
            // 
            this.buttonCONTACT_EMAIL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCONTACT_EMAIL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCONTACT_EMAIL.Location = new System.Drawing.Point(1172, 78);
            this.buttonCONTACT_EMAIL.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonCONTACT_EMAIL.Name = "buttonCONTACT_EMAIL";
            this.buttonCONTACT_EMAIL.Size = new System.Drawing.Size(120, 23);
            this.buttonCONTACT_EMAIL.TabIndex = 46;
            this.buttonCONTACT_EMAIL.Text = "担当者へ連絡";
            this.buttonCONTACT_EMAIL.UseVisualStyleBackColor = true;
            this.buttonCONTACT_EMAIL.Click += new System.EventHandler(this.buttonCONTACT_EMAIL_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonSearch.Location = new System.Drawing.Point(232, 38);
            this.buttonSearch.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 52;
            this.buttonSearch.Text = "再検索";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click_1);
            // 
            // textBoxHATNUMBER
            // 
            this.textBoxHATNUMBER.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxHATNUMBER.Location = new System.Drawing.Point(108, 36);
            this.textBoxHATNUMBER.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxHATNUMBER.Name = "textBoxHATNUMBER";
            this.textBoxHATNUMBER.Size = new System.Drawing.Size(100, 23);
            this.textBoxHATNUMBER.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(12, 36);
            this.label10.MinimumSize = new System.Drawing.Size(0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 51;
            this.label10.Text = "HAT注文番号";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScreenMode
            // 
            this.lblScreenMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScreenMode.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScreenMode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblScreenMode.Location = new System.Drawing.Point(416, 30);
            this.lblScreenMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScreenMode.Name = "lblScreenMode";
            this.lblScreenMode.Size = new System.Drawing.Size(425, 26);
            this.lblScreenMode.TabIndex = 49;
            this.lblScreenMode.Text = "売上額訂正";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label13.Location = new System.Drawing.Point(43, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 12);
            this.label13.TabIndex = 59;
            this.label13.Text = "コメント";
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowFiltering = true;
            this.c1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows;
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.AutoClipboard = true;
            this.c1FlexGrid1.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndColumnHeaders;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
            this.c1FlexGrid1.Location = new System.Drawing.Point(619, 29);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(624, 299);
            this.c1FlexGrid1.TabIndex = 60;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label14.Location = new System.Drawing.Point(43, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 55;
            this.label14.Text = "承認者";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label15.Location = new System.Drawing.Point(17, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 57;
            this.label15.Text = "最終承認者";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnRemand);
            this.flowLayoutPanel1.Controls.Add(this.btnAapproval);
            this.flowLayoutPanel1.Controls.Add(this.btnApplication);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(811, 354);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 29);
            this.flowLayoutPanel1.TabIndex = 53;
            // 
            // btnRemand
            // 
            this.btnRemand.Location = new System.Drawing.Point(3, 3);
            this.btnRemand.Name = "btnRemand";
            this.btnRemand.Size = new System.Drawing.Size(99, 23);
            this.btnRemand.TabIndex = 0;
            this.btnRemand.Text = "差し戻し";
            this.btnRemand.UseVisualStyleBackColor = true;
            this.btnRemand.Click += new System.EventHandler(this.btnRemand_Click);
            // 
            // btnAapproval
            // 
            this.btnAapproval.Location = new System.Drawing.Point(108, 3);
            this.btnAapproval.Name = "btnAapproval";
            this.btnAapproval.Size = new System.Drawing.Size(99, 23);
            this.btnAapproval.TabIndex = 2;
            this.btnAapproval.Text = "承認";
            this.btnAapproval.UseVisualStyleBackColor = true;
            this.btnAapproval.Click += new System.EventHandler(this.btnAapproval_Click);
            // 
            // btnApplication
            // 
            this.btnApplication.Location = new System.Drawing.Point(213, 3);
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.Size = new System.Drawing.Size(99, 23);
            this.btnApplication.TabIndex = 1;
            this.btnApplication.Text = "申請";
            this.btnApplication.UseVisualStyleBackColor = true;
            this.btnApplication.Click += new System.EventHandler(this.btnApplication_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(318, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CmbAuthorizer2
            // 
            this.CmbAuthorizer2.FormattingEnabled = true;
            this.CmbAuthorizer2.Items.AddRange(new object[] {
            "荻原　功",
            "田中　健二",
            "岡野　誠司",
            "生田　慎",
            "石川　光彦",
            "後藤　和久",
            "大沼　良太",
            "冨田　賢一",
            "渡来　千宜",
            "三石　隆浩",
            "和知　拓哉",
            "鎌倉　隆志",
            "油井　栄次郎",
            "井上　潤一",
            "岩木　優太",
            "本田　創平",
            "名取　達裕",
            "山崎　康輝",
            "山本　雄大",
            "福原　達也",
            "宮野　賢人",
            "深見　隆仁"});
            this.CmbAuthorizer2.Location = new System.Drawing.Point(96, 56);
            this.CmbAuthorizer2.Name = "CmbAuthorizer2";
            this.CmbAuthorizer2.Size = new System.Drawing.Size(161, 20);
            this.CmbAuthorizer2.TabIndex = 58;
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.SystemColors.Window;
            this.txtComment.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtComment.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtComment.Location = new System.Drawing.Point(96, 86);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(470, 61);
            this.txtComment.TabIndex = 54;
            // 
            // CmbAuthorizer
            // 
            this.CmbAuthorizer.FormattingEnabled = true;
            this.CmbAuthorizer.Items.AddRange(new object[] {
            "荻原　功",
            "田中　健二",
            "岡野　誠司",
            "生田　慎",
            "石川　光彦",
            "後藤　和久",
            "大沼　良太",
            "冨田　賢一",
            "渡来　千宜",
            "三石　隆浩",
            "和知　拓哉",
            "鎌倉　隆志",
            "油井　栄次郎",
            "井上　潤一",
            "岩木　優太",
            "本田　創平",
            "名取　達裕",
            "山崎　康輝",
            "山本　雄大",
            "福原　達也",
            "宮野　賢人",
            "深見　隆仁"});
            this.CmbAuthorizer.Location = new System.Drawing.Point(96, 26);
            this.CmbAuthorizer.Name = "CmbAuthorizer";
            this.CmbAuthorizer.Size = new System.Drawing.Size(161, 20);
            this.CmbAuthorizer.TabIndex = 56;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.blobStrageForm1);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtApprovalStatus);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.CmbAuthorizer2);
            this.groupBox1.Controls.Add(this.c1FlexGrid1);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.CmbAuthorizer);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(15, 529);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1273, 389);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "売上確定済み情報の訂正承認";
            // 
            // blobStrageForm1
            // 
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(45, 153);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(529, 210);
            this.blobStrageForm1.StrageId = null;
            this.blobStrageForm1.TabIndex = 63;
            // 
            // txtApprovalStatus
            // 
            this.txtApprovalStatus.BackColor = System.Drawing.SystemColors.Window;
            this.txtApprovalStatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtApprovalStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtApprovalStatus.Location = new System.Drawing.Point(348, 26);
            this.txtApprovalStatus.Name = "txtApprovalStatus";
            this.txtApprovalStatus.Size = new System.Drawing.Size(218, 20);
            this.txtApprovalStatus.TabIndex = 62;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label16.Location = new System.Drawing.Point(289, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 61;
            this.label16.Text = "承認状況";
            // 
            // SalesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1304, 921);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxHATNUMBER);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblScreenMode);
            this.Controls.Add(this.lblLockInfo);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.buttonCONTACT_EMAIL);
            this.Controls.Add(this.ToDate);
            this.Controls.Add(this.FromDate);
            this.Controls.Add(this.button_FILTER_RESET);
            this.Controls.Add(this.buttonFILTER);
            this.Controls.Add(this.textboxH_NUMBER);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPU_NAME);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxPU_CODE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1FlexGridSalesEdit);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1320, 766);
            this.Name = "SalesEdit";
            this.Text = "売上額訂正";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesEdit_FormClosing);
            this.Load += new System.EventHandler(this.SalesEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridSalesEdit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridSalesEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCALCU;
        private System.Windows.Forms.TextBox textBoxAfterM_TOTAL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAfterH_TOTAL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDiff;
        private System.Windows.Forms.TextBox textBoxMDIFF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxH_NUMBER;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPU_NAME;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPU_CODE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_FILTER_RESET;
        private System.Windows.Forms.Button buttonFILTER;
        private C1.Win.Calendar.C1DateEdit FromDate;
        private C1.Win.Calendar.C1DateEdit ToDate;
        private System.Windows.Forms.TextBox textBoxH_TOTAL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLockInfo;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button buttonCONTACT_EMAIL;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxHATNUMBER;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblScreenMode;
        private System.Windows.Forms.TextBox textBoxHDIFF;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxM_TOTAL;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        public C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRemand;
        private System.Windows.Forms.Button btnApplication;
        private System.Windows.Forms.Button btnAapproval;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox CmbAuthorizer2;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ComboBox CmbAuthorizer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtApprovalStatus;
        private System.Windows.Forms.Label label16;
        private CustomControls.BlobStrage.BlobStrageForm blobStrageForm1;
    }
}
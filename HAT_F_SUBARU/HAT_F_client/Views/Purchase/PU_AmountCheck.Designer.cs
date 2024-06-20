namespace HatFClient.Views.Purchase
{
    partial class PU_AmountCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PU_AmountCheck));
            this.txtPuCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.grdPuAmountCheck = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnContactEmail = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHChuban = new System.Windows.Forms.TextBox();
            this.btnFilterReset = new System.Windows.Forms.Button();
            this.btnDifferenceCheck = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.lblLockInfo = new System.Windows.Forms.Label();
            this.txtHatOrderNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSetStatus = new System.Windows.Forms.Button();
            this.btnPuEdit = new System.Windows.Forms.Button();
            this.btnSearchSupplier = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCalculation = new System.Windows.Forms.Button();
            this.txtMTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtHNoukiTo = new HatFClient.CustomControls.C1DateInputEx();
            this.dtHNoukiFrom = new HatFClient.CustomControls.C1DateInputEx();
            this.dtPayDateTo = new HatFClient.CustomControls.C1DateInputEx();
            this.dtPayDateFrom = new HatFClient.CustomControls.C1DateInputEx();
            this.txtPuName = new HatFClient.CustomControls.TextBoxReadOnly();
            ((System.ComponentModel.ISupportInitialize)(this.grdPuAmountCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPuCode
            // 
            this.txtPuCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPuCode.Location = new System.Drawing.Point(94, 55);
            this.txtPuCode.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtPuCode.Name = "txtPuCode";
            this.txtPuCode.Size = new System.Drawing.Size(100, 23);
            this.txtPuCode.TabIndex = 3;
            this.txtPuCode.TextChanged += new System.EventHandler(this.txtPuCode_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 56);
            this.label4.MinimumSize = new System.Drawing.Size(0, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "仕入先コード*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(272, 174);
            this.label5.MinimumSize = new System.Drawing.Size(0, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "H納日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(429, 174);
            this.label6.MinimumSize = new System.Drawing.Size(0, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "～";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilter.Location = new System.Drawing.Point(575, 173);
            this.btnFilter.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 21;
            this.btnFilter.Text = "絞込";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // grdPuAmountCheck
            // 
            this.grdPuAmountCheck.AllowFiltering = true;
            this.grdPuAmountCheck.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.grdPuAmountCheck.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.grdPuAmountCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPuAmountCheck.AutoGenerateColumns = false;
            this.grdPuAmountCheck.ColumnInfo = resources.GetString("grdPuAmountCheck.ColumnInfo");
            this.grdPuAmountCheck.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdPuAmountCheck.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.grdPuAmountCheck.Location = new System.Drawing.Point(14, 202);
            this.grdPuAmountCheck.Name = "grdPuAmountCheck";
            this.grdPuAmountCheck.Rows.Count = 1;
            this.grdPuAmountCheck.Size = new System.Drawing.Size(1238, 407);
            this.grdPuAmountCheck.TabIndex = 23;
            this.grdPuAmountCheck.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdPuAmountCheck_AfterEdit);
            this.grdPuAmountCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdPuAmountCheck_KeyDown);
            // 
            // btnContactEmail
            // 
            this.btnContactEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContactEmail.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnContactEmail.Location = new System.Drawing.Point(1132, 58);
            this.btnContactEmail.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnContactEmail.Name = "btnContactEmail";
            this.btnContactEmail.Size = new System.Drawing.Size(120, 23);
            this.btnContactEmail.TabIndex = 14;
            this.btnContactEmail.Text = "担当者へ連絡";
            this.btnContactEmail.UseVisualStyleBackColor = true;
            this.btnContactEmail.Click += new System.EventHandler(this.btnContactEmail_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(1177, 615);
            this.btnSave.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(32, 174);
            this.label1.MinimumSize = new System.Drawing.Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "H注番";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHChuban
            // 
            this.txtHChuban.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHChuban.Location = new System.Drawing.Point(94, 173);
            this.txtHChuban.MaxLength = 10;
            this.txtHChuban.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtHChuban.Name = "txtHChuban";
            this.txtHChuban.Size = new System.Drawing.Size(148, 23);
            this.txtHChuban.TabIndex = 16;
            // 
            // btnFilterReset
            // 
            this.btnFilterReset.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilterReset.Location = new System.Drawing.Point(656, 173);
            this.btnFilterReset.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnFilterReset.Name = "btnFilterReset";
            this.btnFilterReset.Size = new System.Drawing.Size(75, 23);
            this.btnFilterReset.TabIndex = 22;
            this.btnFilterReset.Text = "絞込解除";
            this.btnFilterReset.UseVisualStyleBackColor = true;
            this.btnFilterReset.Click += new System.EventHandler(this.btnFilterResetClick);
            // 
            // btnDifferenceCheck
            // 
            this.btnDifferenceCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDifferenceCheck.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDifferenceCheck.Location = new System.Drawing.Point(552, 615);
            this.btnDifferenceCheck.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnDifferenceCheck.Name = "btnDifferenceCheck";
            this.btnDifferenceCheck.Size = new System.Drawing.Size(117, 23);
            this.btnDifferenceCheck.TabIndex = 26;
            this.btnDifferenceCheck.Text = "差分チェック";
            this.btnDifferenceCheck.UseVisualStyleBackColor = true;
            this.btnDifferenceCheck.Click += new System.EventHandler(this.btnDifferenceCheck_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.btnUnlock.Location = new System.Drawing.Point(1132, 16);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(120, 23);
            this.btnUnlock.TabIndex = 13;
            this.btnUnlock.Text = "読み取り専用解除";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Visible = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // lblScreenMode
            // 
            this.lblScreenMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScreenMode.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScreenMode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblScreenMode.Location = new System.Drawing.Point(416, 9);
            this.lblScreenMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScreenMode.Name = "lblScreenMode";
            this.lblScreenMode.Size = new System.Drawing.Size(343, 26);
            this.lblScreenMode.TabIndex = 0;
            this.lblScreenMode.Text = "仕入金額照合";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLockInfo
            // 
            this.lblLockInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockInfo.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.lblLockInfo.Location = new System.Drawing.Point(912, 9);
            this.lblLockInfo.Name = "lblLockInfo";
            this.lblLockInfo.Size = new System.Drawing.Size(205, 42);
            this.lblLockInfo.TabIndex = 1;
            // 
            // txtHatOrderNo
            // 
            this.txtHatOrderNo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHatOrderNo.Location = new System.Drawing.Point(94, 84);
            this.txtHatOrderNo.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtHatOrderNo.Name = "txtHatOrderNo";
            this.txtHatOrderNo.Size = new System.Drawing.Size(100, 23);
            this.txtHatOrderNo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.MinimumSize = new System.Drawing.Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "HAT注文番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(423, 111);
            this.btnSearch.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSetStatus
            // 
            this.btnSetStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetStatus.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetStatus.Location = new System.Drawing.Point(413, 615);
            this.btnSetStatus.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSetStatus.Name = "btnSetStatus";
            this.btnSetStatus.Size = new System.Drawing.Size(117, 23);
            this.btnSetStatus.TabIndex = 25;
            this.btnSetStatus.Text = "一括確認";
            this.btnSetStatus.UseVisualStyleBackColor = true;
            this.btnSetStatus.Click += new System.EventHandler(this.btnSetStatus_Click);
            // 
            // btnPuEdit
            // 
            this.btnPuEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPuEdit.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPuEdit.Location = new System.Drawing.Point(15, 615);
            this.btnPuEdit.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnPuEdit.Name = "btnPuEdit";
            this.btnPuEdit.Size = new System.Drawing.Size(117, 23);
            this.btnPuEdit.TabIndex = 24;
            this.btnPuEdit.Text = "仕入登録";
            this.btnPuEdit.UseVisualStyleBackColor = true;
            this.btnPuEdit.Click += new System.EventHandler(this.btnPuEdit_Click);
            // 
            // btnSearchSupplier
            // 
            this.btnSearchSupplier.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearchSupplier.Location = new System.Drawing.Point(423, 54);
            this.btnSearchSupplier.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSearchSupplier.Name = "btnSearchSupplier";
            this.btnSearchSupplier.Size = new System.Drawing.Size(75, 23);
            this.btnSearchSupplier.TabIndex = 5;
            this.btnSearchSupplier.Text = "仕入先検索";
            this.btnSearchSupplier.UseVisualStyleBackColor = true;
            this.btnSearchSupplier.Click += new System.EventHandler(this.btnSearchSupplier_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(200, 113);
            this.label3.MinimumSize = new System.Drawing.Size(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(12, 113);
            this.label10.MinimumSize = new System.Drawing.Size(0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "仕入支払日";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCalculation
            // 
            this.btnCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculation.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCalculation.Location = new System.Drawing.Point(1064, 615);
            this.btnCalculation.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnCalculation.Name = "btnCalculation";
            this.btnCalculation.Size = new System.Drawing.Size(75, 23);
            this.btnCalculation.TabIndex = 31;
            this.btnCalculation.Text = "再計算";
            this.btnCalculation.UseVisualStyleBackColor = true;
            this.btnCalculation.Click += new System.EventHandler(this.btnCalculation_Click);
            // 
            // txtMTotal
            // 
            this.txtMTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMTotal.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtMTotal.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMTotal.Location = new System.Drawing.Point(958, 615);
            this.txtMTotal.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtMTotal.Name = "txtMTotal";
            this.txtMTotal.ReadOnly = true;
            this.txtMTotal.Size = new System.Drawing.Size(100, 23);
            this.txtMTotal.TabIndex = 30;
            this.txtMTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(882, 616);
            this.label9.MinimumSize = new System.Drawing.Size(0, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 29;
            this.label9.Text = "M合計金額";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHTotal
            // 
            this.txtHTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHTotal.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtHTotal.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHTotal.Location = new System.Drawing.Point(765, 615);
            this.txtHTotal.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtHTotal.Name = "txtHTotal";
            this.txtHTotal.ReadOnly = true;
            this.txtHTotal.Size = new System.Drawing.Size(100, 23);
            this.txtHTotal.TabIndex = 28;
            this.txtHTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(695, 616);
            this.label8.MinimumSize = new System.Drawing.Size(0, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "H合計金額";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtHNoukiTo
            // 
            this.dtHNoukiTo.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtHNoukiTo.DateTimeInput = false;
            this.dtHNoukiTo.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtHNoukiTo.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiTo.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtHNoukiTo.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtHNoukiTo.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiTo.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtHNoukiTo.EditMask = "90/90/90";
            this.dtHNoukiTo.EmptyAsNull = true;
            this.dtHNoukiTo.ExitOnLastChar = true;
            this.dtHNoukiTo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtHNoukiTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiTo.GapHeight = 0;
            this.dtHNoukiTo.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtHNoukiTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtHNoukiTo.Location = new System.Drawing.Point(454, 173);
            this.dtHNoukiTo.LoopPosition = false;
            this.dtHNoukiTo.MaskInfo.AutoTabWhenFilled = true;
            this.dtHNoukiTo.MaxLength = 8;
            this.dtHNoukiTo.Name = "dtHNoukiTo";
            this.dtHNoukiTo.Size = new System.Drawing.Size(100, 23);
            this.dtHNoukiTo.TabIndex = 20;
            this.dtHNoukiTo.Tag = null;
            this.dtHNoukiTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtHNoukiFrom
            // 
            this.dtHNoukiFrom.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtHNoukiFrom.DateTimeInput = false;
            this.dtHNoukiFrom.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtHNoukiFrom.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiFrom.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtHNoukiFrom.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtHNoukiFrom.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiFrom.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtHNoukiFrom.EditMask = "90/90/90";
            this.dtHNoukiFrom.EmptyAsNull = true;
            this.dtHNoukiFrom.ExitOnLastChar = true;
            this.dtHNoukiFrom.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtHNoukiFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtHNoukiFrom.GapHeight = 0;
            this.dtHNoukiFrom.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtHNoukiFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtHNoukiFrom.Location = new System.Drawing.Point(323, 173);
            this.dtHNoukiFrom.LoopPosition = false;
            this.dtHNoukiFrom.MaskInfo.AutoTabWhenFilled = true;
            this.dtHNoukiFrom.MaxLength = 8;
            this.dtHNoukiFrom.Name = "dtHNoukiFrom";
            this.dtHNoukiFrom.Size = new System.Drawing.Size(100, 23);
            this.dtHNoukiFrom.TabIndex = 18;
            this.dtHNoukiFrom.Tag = null;
            this.dtHNoukiFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtPayDateTo
            // 
            this.dtPayDateTo.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtPayDateTo.DateTimeInput = false;
            this.dtPayDateTo.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtPayDateTo.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateTo.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtPayDateTo.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtPayDateTo.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateTo.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtPayDateTo.EditMask = "90/90/90";
            this.dtPayDateTo.EmptyAsNull = true;
            this.dtPayDateTo.ExitOnLastChar = true;
            this.dtPayDateTo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPayDateTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateTo.GapHeight = 0;
            this.dtPayDateTo.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtPayDateTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtPayDateTo.Location = new System.Drawing.Point(225, 112);
            this.dtPayDateTo.LoopPosition = false;
            this.dtPayDateTo.MaskInfo.AutoTabWhenFilled = true;
            this.dtPayDateTo.MaxLength = 8;
            this.dtPayDateTo.Name = "dtPayDateTo";
            this.dtPayDateTo.Size = new System.Drawing.Size(100, 23);
            this.dtPayDateTo.TabIndex = 11;
            this.dtPayDateTo.Tag = null;
            this.dtPayDateTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtPayDateFrom
            // 
            this.dtPayDateFrom.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtPayDateFrom.DateTimeInput = false;
            this.dtPayDateFrom.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtPayDateFrom.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateFrom.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtPayDateFrom.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtPayDateFrom.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateFrom.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtPayDateFrom.EditMask = "90/90/90";
            this.dtPayDateFrom.EmptyAsNull = true;
            this.dtPayDateFrom.ExitOnLastChar = true;
            this.dtPayDateFrom.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtPayDateFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtPayDateFrom.GapHeight = 0;
            this.dtPayDateFrom.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtPayDateFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtPayDateFrom.Location = new System.Drawing.Point(94, 112);
            this.dtPayDateFrom.LoopPosition = false;
            this.dtPayDateFrom.MaskInfo.AutoTabWhenFilled = true;
            this.dtPayDateFrom.MaxLength = 8;
            this.dtPayDateFrom.Name = "dtPayDateFrom";
            this.dtPayDateFrom.Size = new System.Drawing.Size(100, 23);
            this.dtPayDateFrom.TabIndex = 9;
            this.dtPayDateFrom.Tag = null;
            this.dtPayDateFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtPuName
            // 
            this.txtPuName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtPuName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPuName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPuName.Location = new System.Drawing.Point(203, 55);
            this.txtPuName.Name = "txtPuName";
            this.txtPuName.ReadOnly = true;
            this.txtPuName.Size = new System.Drawing.Size(214, 23);
            this.txtPuName.TabIndex = 4;
            this.txtPuName.TabStop = false;
            // 
            // PU_AmountCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1264, 650);
            this.Controls.Add(this.btnCalculation);
            this.Controls.Add(this.txtMTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtHTotal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtHNoukiTo);
            this.Controls.Add(this.dtHNoukiFrom);
            this.Controls.Add(this.dtPayDateTo);
            this.Controls.Add(this.dtPayDateFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSearchSupplier);
            this.Controls.Add(this.btnPuEdit);
            this.Controls.Add(this.txtPuName);
            this.Controls.Add(this.btnSetStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtHatOrderNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLockInfo);
            this.Controls.Add(this.lblScreenMode);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnDifferenceCheck);
            this.Controls.Add(this.btnFilterReset);
            this.Controls.Add(this.txtHChuban);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnContactEmail);
            this.Controls.Add(this.grdPuAmountCheck);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPuCode);
            this.Controls.Add(this.label4);
            this.Name = "PU_AmountCheck";
            this.Text = "仕入金額照合";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PU_AmountCheck_FormClosing);
            this.Load += new System.EventHandler(this.PU_AmountCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPuAmountCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPuCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFilter;
        private C1.Win.C1FlexGrid.C1FlexGrid grdPuAmountCheck;
        private System.Windows.Forms.Button btnContactEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHChuban;
        private System.Windows.Forms.Button btnFilterReset;
        private System.Windows.Forms.Button btnDifferenceCheck;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label lblScreenMode;
        private System.Windows.Forms.Label lblLockInfo;
        private System.Windows.Forms.TextBox txtHatOrderNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSetStatus;
        private CustomControls.TextBoxReadOnly txtPuName;
        private System.Windows.Forms.Button btnPuEdit;
        private System.Windows.Forms.Button btnSearchSupplier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private CustomControls.C1DateInputEx dtPayDateFrom;
        private CustomControls.C1DateInputEx dtPayDateTo;
        private CustomControls.C1DateInputEx dtHNoukiFrom;
        private CustomControls.C1DateInputEx dtHNoukiTo;
        private System.Windows.Forms.Button btnCalculation;
        private System.Windows.Forms.TextBox txtMTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHTotal;
        private System.Windows.Forms.Label label8;
    }
}
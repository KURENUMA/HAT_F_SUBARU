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
            this.btnPuEdit = new System.Windows.Forms.Button();
            this.grdTotalAmount = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.dtHNoukiTo = new HatFClient.CustomControls.C1DateInputEx();
            this.dtHNoukiFrom = new HatFClient.CustomControls.C1DateInputEx();
            this.txtPuName = new HatFClient.CustomControls.TextBoxReadOnly();
            ((System.ComponentModel.ISupportInitialize)(this.grdPuAmountCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTotalAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPuCode
            // 
            this.txtPuCode.Enabled = false;
            this.txtPuCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPuCode.Location = new System.Drawing.Point(94, 56);
            this.txtPuCode.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtPuCode.Name = "txtPuCode";
            this.txtPuCode.Size = new System.Drawing.Size(100, 23);
            this.txtPuCode.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 57);
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
            this.label5.Location = new System.Drawing.Point(272, 128);
            this.label5.MinimumSize = new System.Drawing.Size(0, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "H納日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(429, 128);
            this.label6.MinimumSize = new System.Drawing.Size(0, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "～";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilter.Location = new System.Drawing.Point(575, 127);
            this.btnFilter.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 19;
            this.btnFilter.Text = "絞込";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // grdPuAmountCheck
            // 
            this.grdPuAmountCheck.AllowAddNew = true;
            this.grdPuAmountCheck.AllowDelete = true;
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
            this.grdPuAmountCheck.Location = new System.Drawing.Point(14, 156);
            this.grdPuAmountCheck.Name = "grdPuAmountCheck";
            this.grdPuAmountCheck.Rows.Count = 1;
            this.grdPuAmountCheck.Size = new System.Drawing.Size(1238, 392);
            this.grdPuAmountCheck.TabIndex = 24;
            this.grdPuAmountCheck.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdPuAmountCheck_AfterEdit);
            this.grdPuAmountCheck.Click += new System.EventHandler(this.grdPuAmountCheck_Click);
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
            this.btnContactEmail.TabIndex = 22;
            this.btnContactEmail.Text = "担当者へ連絡";
            this.btnContactEmail.UseVisualStyleBackColor = true;
            this.btnContactEmail.Click += new System.EventHandler(this.btnContactEmail_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(1174, 668);
            this.btnSave.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(32, 128);
            this.label1.MinimumSize = new System.Drawing.Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "H注番";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHChuban
            // 
            this.txtHChuban.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHChuban.Location = new System.Drawing.Point(94, 127);
            this.txtHChuban.MaxLength = 10;
            this.txtHChuban.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtHChuban.Name = "txtHChuban";
            this.txtHChuban.Size = new System.Drawing.Size(148, 23);
            this.txtHChuban.TabIndex = 14;
            // 
            // btnFilterReset
            // 
            this.btnFilterReset.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFilterReset.Location = new System.Drawing.Point(656, 127);
            this.btnFilterReset.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnFilterReset.Name = "btnFilterReset";
            this.btnFilterReset.Size = new System.Drawing.Size(75, 23);
            this.btnFilterReset.TabIndex = 20;
            this.btnFilterReset.Text = "絞込解除";
            this.btnFilterReset.UseVisualStyleBackColor = true;
            this.btnFilterReset.Click += new System.EventHandler(this.btnFilterResetClick);
            // 
            // btnDifferenceCheck
            // 
            this.btnDifferenceCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDifferenceCheck.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDifferenceCheck.Location = new System.Drawing.Point(1051, 669);
            this.btnDifferenceCheck.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnDifferenceCheck.Name = "btnDifferenceCheck";
            this.btnDifferenceCheck.Size = new System.Drawing.Size(117, 23);
            this.btnDifferenceCheck.TabIndex = 27;
            this.btnDifferenceCheck.Text = "差分チェック";
            this.btnDifferenceCheck.UseVisualStyleBackColor = true;
            this.btnDifferenceCheck.Click += new System.EventHandler(this.BtnDifferenceCheck_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.btnUnlock.Location = new System.Drawing.Point(1132, 16);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(120, 23);
            this.btnUnlock.TabIndex = 21;
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
            this.txtHatOrderNo.Enabled = false;
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
            // btnPuEdit
            // 
            this.btnPuEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPuEdit.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPuEdit.Location = new System.Drawing.Point(1132, 98);
            this.btnPuEdit.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnPuEdit.Name = "btnPuEdit";
            this.btnPuEdit.Size = new System.Drawing.Size(117, 23);
            this.btnPuEdit.TabIndex = 23;
            this.btnPuEdit.Text = "仕入登録";
            this.btnPuEdit.UseVisualStyleBackColor = true;
            this.btnPuEdit.Click += new System.EventHandler(this.btnPuEdit_Click);
            // 
            // grdTotalAmount
            // 
            this.grdTotalAmount.AllowEditing = false;
            this.grdTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTotalAmount.AutoGenerateColumns = false;
            this.grdTotalAmount.ColumnInfo = resources.GetString("grdTotalAmount.ColumnInfo");
            this.grdTotalAmount.Location = new System.Drawing.Point(949, 560);
            this.grdTotalAmount.Name = "grdTotalAmount";
            this.grdTotalAmount.Rows.Count = 1;
            this.grdTotalAmount.Size = new System.Drawing.Size(300, 103);
            this.grdTotalAmount.TabIndex = 25;
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
            this.dtHNoukiTo.Location = new System.Drawing.Point(454, 127);
            this.dtHNoukiTo.LoopPosition = false;
            this.dtHNoukiTo.MaskInfo.AutoTabWhenFilled = true;
            this.dtHNoukiTo.MaxLength = 8;
            this.dtHNoukiTo.Name = "dtHNoukiTo";
            this.dtHNoukiTo.Size = new System.Drawing.Size(100, 23);
            this.dtHNoukiTo.TabIndex = 18;
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
            this.dtHNoukiFrom.Location = new System.Drawing.Point(323, 127);
            this.dtHNoukiFrom.LoopPosition = false;
            this.dtHNoukiFrom.MaskInfo.AutoTabWhenFilled = true;
            this.dtHNoukiFrom.MaxLength = 8;
            this.dtHNoukiFrom.Name = "dtHNoukiFrom";
            this.dtHNoukiFrom.Size = new System.Drawing.Size(100, 23);
            this.dtHNoukiFrom.TabIndex = 16;
            this.dtHNoukiFrom.Tag = null;
            this.dtHNoukiFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtPuName
            // 
            this.txtPuName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtPuName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPuName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPuName.Location = new System.Drawing.Point(203, 56);
            this.txtPuName.Name = "txtPuName";
            this.txtPuName.ReadOnly = true;
            this.txtPuName.Size = new System.Drawing.Size(214, 23);
            this.txtPuName.TabIndex = 5;
            this.txtPuName.TabStop = false;
            // 
            // PU_AmountCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1264, 704);
            this.Controls.Add(this.grdTotalAmount);
            this.Controls.Add(this.dtHNoukiTo);
            this.Controls.Add(this.dtHNoukiFrom);
            this.Controls.Add(this.btnPuEdit);
            this.Controls.Add(this.txtPuName);
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
            ((System.ComponentModel.ISupportInitialize)(this.grdTotalAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtHNoukiFrom)).EndInit();
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
        private CustomControls.TextBoxReadOnly txtPuName;
        private System.Windows.Forms.Button btnPuEdit;
        private CustomControls.C1DateInputEx dtHNoukiFrom;
        private CustomControls.C1DateInputEx dtHNoukiTo;
        private C1.Win.C1FlexGrid.C1FlexGrid grdTotalAmount;
    }
}
namespace HatFClient.Views.CreditNote
{
    partial class CreditNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditNote));
            this.grdSalesAdjustments = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblLockInfo = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnContact = new System.Windows.Forms.Button();
            this.txtTokuiCd = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.grdApprovalHistory = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemand = new System.Windows.Forms.Button();
            this.btnApproval = new System.Windows.Forms.Button();
            this.btnApplication = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.cmbAuthorizer2 = new System.Windows.Forms.ComboBox();
            this.cmbAuthorizer = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtApprovalStatus = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTokuiName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInvoicedDate = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesAdjustments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdApprovalHistory)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSalesAdjustments
            // 
            this.grdSalesAdjustments.AllowAddNew = true;
            this.grdSalesAdjustments.AllowDelete = true;
            this.grdSalesAdjustments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.grdSalesAdjustments.AllowFiltering = true;
            this.grdSalesAdjustments.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None;
            this.grdSalesAdjustments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSalesAdjustments.AutoGenerateColumns = false;
            this.grdSalesAdjustments.ColumnInfo = resources.GetString("grdSalesAdjustments.ColumnInfo");
            this.grdSalesAdjustments.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdSalesAdjustments.Location = new System.Drawing.Point(12, 83);
            this.grdSalesAdjustments.Name = "grdSalesAdjustments";
            this.grdSalesAdjustments.Rows.Count = 1;
            this.grdSalesAdjustments.Size = new System.Drawing.Size(1199, 355);
            this.grdSalesAdjustments.TabIndex = 9;
            this.grdSalesAdjustments.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdSalesAdjustments_AfterEdit);
            this.grdSalesAdjustments.AfterAddRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdSalesAdjustments_AfterAddRow);
            this.grdSalesAdjustments.BeforeDeleteRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdSalesAdjustments_BeforeDeleteRow);
            this.grdSalesAdjustments.AfterDeleteRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdSalesAdjustments_AfterDeleteRow);
            // 
            // lblLockInfo
            // 
            this.lblLockInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockInfo.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.lblLockInfo.Location = new System.Drawing.Point(863, 25);
            this.lblLockInfo.Name = "lblLockInfo";
            this.lblLockInfo.Size = new System.Drawing.Size(218, 42);
            this.lblLockInfo.TabIndex = 1;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.btnUnlock.Location = new System.Drawing.Point(1091, 25);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(120, 23);
            this.btnUnlock.TabIndex = 7;
            this.btnUnlock.Text = "読み取り専用解除";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Visible = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnContact
            // 
            this.btnContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContact.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnContact.Location = new System.Drawing.Point(1091, 54);
            this.btnContact.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(120, 23);
            this.btnContact.TabIndex = 8;
            this.btnContact.Text = "担当者へ連絡";
            this.btnContact.UseVisualStyleBackColor = true;
            this.btnContact.Click += new System.EventHandler(this.BtnContact_Click);
            // 
            // txtTokuiCd
            // 
            this.txtTokuiCd.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTokuiCd.Location = new System.Drawing.Point(73, 55);
            this.txtTokuiCd.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtTokuiCd.Name = "txtTokuiCd";
            this.txtTokuiCd.ReadOnly = true;
            this.txtTokuiCd.Size = new System.Drawing.Size(73, 23);
            this.txtTokuiCd.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(20, 56);
            this.label10.MinimumSize = new System.Drawing.Size(0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "得意先";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScreenMode
            // 
            this.lblScreenMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScreenMode.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScreenMode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblScreenMode.Location = new System.Drawing.Point(536, 9);
            this.lblScreenMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScreenMode.Name = "lblScreenMode";
            this.lblScreenMode.Size = new System.Drawing.Size(153, 26);
            this.lblScreenMode.TabIndex = 0;
            this.lblScreenMode.Text = "赤黒登録";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdApprovalHistory
            // 
            this.grdApprovalHistory.AllowEditing = false;
            this.grdApprovalHistory.AllowFiltering = true;
            this.grdApprovalHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdApprovalHistory.AutoClipboard = true;
            this.grdApprovalHistory.AutoGenerateColumns = false;
            this.grdApprovalHistory.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndColumnHeaders;
            this.grdApprovalHistory.ColumnInfo = resources.GetString("grdApprovalHistory.ColumnInfo");
            this.grdApprovalHistory.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
            this.grdApprovalHistory.Location = new System.Drawing.Point(580, 26);
            this.grdApprovalHistory.Name = "grdApprovalHistory";
            this.grdApprovalHistory.Rows.Count = 1;
            this.grdApprovalHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.grdApprovalHistory.Size = new System.Drawing.Size(563, 322);
            this.grdApprovalHistory.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnRemand);
            this.flowLayoutPanel1.Controls.Add(this.btnApproval);
            this.flowLayoutPanel1.Controls.Add(this.btnApplication);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(754, 354);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 29);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // btnRemand
            // 
            this.btnRemand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemand.Location = new System.Drawing.Point(3, 3);
            this.btnRemand.Name = "btnRemand";
            this.btnRemand.Size = new System.Drawing.Size(99, 23);
            this.btnRemand.TabIndex = 0;
            this.btnRemand.Text = "差し戻し";
            this.btnRemand.UseVisualStyleBackColor = true;
            this.btnRemand.Click += new System.EventHandler(this.BtnRemand_Click);
            // 
            // btnApproval
            // 
            this.btnApproval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApproval.Location = new System.Drawing.Point(108, 3);
            this.btnApproval.Name = "btnApproval";
            this.btnApproval.Size = new System.Drawing.Size(99, 23);
            this.btnApproval.TabIndex = 1;
            this.btnApproval.Text = "承認";
            this.btnApproval.UseVisualStyleBackColor = true;
            this.btnApproval.Click += new System.EventHandler(this.BtnAapproval_Click);
            // 
            // btnApplication
            // 
            this.btnApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplication.Location = new System.Drawing.Point(213, 3);
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.Size = new System.Drawing.Size(99, 23);
            this.btnApplication.TabIndex = 2;
            this.btnApplication.Text = "申請";
            this.btnApplication.UseVisualStyleBackColor = true;
            this.btnApplication.Click += new System.EventHandler(this.BtnApplication_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(318, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.blobStrageForm1);
            this.groupBox1.Controls.Add(this.cmbAuthorizer2);
            this.groupBox1.Controls.Add(this.cmbAuthorizer);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.txtApprovalStatus);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.grdApprovalHistory);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(15, 478);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1196, 389);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "売上確定済み情報の訂正承認";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label13.Location = new System.Drawing.Point(43, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "コメント";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label14.Location = new System.Drawing.Point(43, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "承認者";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label15.Location = new System.Drawing.Point(17, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 4;
            this.label15.Text = "最終承認者";
            // 
            // blobStrageForm1
            // 
            this.blobStrageForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(45, 173);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(529, 210);
            this.blobStrageForm1.StrageId = null;
            this.blobStrageForm1.TabIndex = 8;
            // 
            // cmbAuthorizer2
            // 
            this.cmbAuthorizer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAuthorizer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthorizer2.FormattingEnabled = true;
            this.cmbAuthorizer2.Items.AddRange(new object[] {
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
            this.cmbAuthorizer2.Location = new System.Drawing.Point(96, 55);
            this.cmbAuthorizer2.Name = "cmbAuthorizer2";
            this.cmbAuthorizer2.Size = new System.Drawing.Size(161, 20);
            this.cmbAuthorizer2.TabIndex = 5;
            this.cmbAuthorizer2.SelectedIndexChanged += new System.EventHandler(this.CmbAuthorizer_SelectionChangeCommitted);
            // 
            // cmbAuthorizer
            // 
            this.cmbAuthorizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAuthorizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthorizer.FormattingEnabled = true;
            this.cmbAuthorizer.Items.AddRange(new object[] {
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
            this.cmbAuthorizer.Location = new System.Drawing.Point(96, 26);
            this.cmbAuthorizer.Name = "cmbAuthorizer";
            this.cmbAuthorizer.Size = new System.Drawing.Size(161, 20);
            this.cmbAuthorizer.TabIndex = 1;
            this.cmbAuthorizer.SelectionChangeCommitted += new System.EventHandler(this.CmbAuthorizer_SelectionChangeCommitted);
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.BackColor = System.Drawing.SystemColors.Window;
            this.txtComment.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtComment.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtComment.Location = new System.Drawing.Point(96, 85);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(464, 61);
            this.txtComment.TabIndex = 7;
            // 
            // txtApprovalStatus
            // 
            this.txtApprovalStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApprovalStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtApprovalStatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtApprovalStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtApprovalStatus.Location = new System.Drawing.Point(348, 26);
            this.txtApprovalStatus.Name = "txtApprovalStatus";
            this.txtApprovalStatus.ReadOnly = true;
            this.txtApprovalStatus.Size = new System.Drawing.Size(212, 20);
            this.txtApprovalStatus.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label16.Location = new System.Drawing.Point(289, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 2;
            this.label16.Text = "承認状況";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalAmount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTotalAmount.Location = new System.Drawing.Point(1052, 449);
            this.txtTotalAmount.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(159, 23);
            this.txtTotalAmount.TabIndex = 11;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(991, 450);
            this.label2.MinimumSize = new System.Drawing.Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "合計金額";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTokuiName
            // 
            this.txtTokuiName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTokuiName.Location = new System.Drawing.Point(152, 55);
            this.txtTokuiName.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtTokuiName.Name = "txtTokuiName";
            this.txtTokuiName.ReadOnly = true;
            this.txtTokuiName.Size = new System.Drawing.Size(205, 23);
            this.txtTokuiName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(376, 56);
            this.label3.MinimumSize = new System.Drawing.Size(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "請求日";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInvoicedDate
            // 
            this.txtInvoicedDate.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtInvoicedDate.Location = new System.Drawing.Point(429, 55);
            this.txtInvoicedDate.MinimumSize = new System.Drawing.Size(4, 20);
            this.txtInvoicedDate.Name = "txtInvoicedDate";
            this.txtInvoicedDate.ReadOnly = true;
            this.txtInvoicedDate.Size = new System.Drawing.Size(73, 23);
            this.txtInvoicedDate.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(866, 450);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "[デバッグ用]保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CreditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1225, 879);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTokuiName);
            this.Controls.Add(this.txtInvoicedDate);
            this.Controls.Add(this.txtTokuiCd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblScreenMode);
            this.Controls.Add(this.lblLockInfo);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnContact);
            this.Controls.Add(this.grdSalesAdjustments);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1241, 918);
            this.Name = "CreditNote";
            this.Text = "赤黒登録";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreditNote_FormClosing);
            this.Load += new System.EventHandler(this.CreditNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesAdjustments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdApprovalHistory)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdSalesAdjustments;
        private System.Windows.Forms.Label lblLockInfo;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnContact;
        private System.Windows.Forms.TextBox txtTokuiCd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblScreenMode;
        public C1.Win.C1FlexGrid.C1FlexGrid grdApprovalHistory;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnApplication;
        private System.Windows.Forms.Button btnApproval;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.BlobStrage.BlobStrageForm blobStrageForm1;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTokuiName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInvoicedDate;
        private System.Windows.Forms.Button btnRemand;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbAuthorizer2;
        private System.Windows.Forms.ComboBox cmbAuthorizer;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtApprovalStatus;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSave;
    }
}
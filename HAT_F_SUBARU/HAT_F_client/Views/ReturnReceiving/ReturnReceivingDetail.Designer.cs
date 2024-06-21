using HatFClient.Views.Purchase;

namespace HatFClient.Views.ReturnReceiving
{
    partial class ReturnReceivingDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReturnReceivingDetail));
            this.c1FlexGrid_ReturnReceivingDetail = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.buttonCONTACT_EMAIL = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.lblLockInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.label14 = new System.Windows.Forms.Label();
            this.txtApprovalStatus = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.CmbAuthorizer2 = new System.Windows.Forms.ComboBox();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.CmbAuthorizer = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemand = new System.Windows.Forms.Button();
            this.btnAapproval = new System.Windows.Forms.Button();
            this.btnApplication = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBoxDenNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxHATNUMBER = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_ReturnReceivingDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlexGrid_ReturnReceivingDetail
            // 
            this.c1FlexGrid_ReturnReceivingDetail.AllowFiltering = true;
            this.c1FlexGrid_ReturnReceivingDetail.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGrid_ReturnReceivingDetail.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGrid_ReturnReceivingDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid_ReturnReceivingDetail.AutoGenerateColumns = false;
            this.c1FlexGrid_ReturnReceivingDetail.ColumnInfo = resources.GetString("c1FlexGrid_ReturnReceivingDetail.ColumnInfo");
            this.c1FlexGrid_ReturnReceivingDetail.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c1FlexGrid_ReturnReceivingDetail.Location = new System.Drawing.Point(14, 67);
            this.c1FlexGrid_ReturnReceivingDetail.Name = "c1FlexGrid_ReturnReceivingDetail";
            this.c1FlexGrid_ReturnReceivingDetail.Size = new System.Drawing.Size(1278, 411);
            this.c1FlexGrid_ReturnReceivingDetail.TabIndex = 15;
            this.c1FlexGrid_ReturnReceivingDetail.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlexGrid_ReturnReceivingDetail_AfterEdit);
            // 
            // buttonCONTACT_EMAIL
            // 
            this.buttonCONTACT_EMAIL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCONTACT_EMAIL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCONTACT_EMAIL.Location = new System.Drawing.Point(1172, 38);
            this.buttonCONTACT_EMAIL.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonCONTACT_EMAIL.Name = "buttonCONTACT_EMAIL";
            this.buttonCONTACT_EMAIL.Size = new System.Drawing.Size(120, 23);
            this.buttonCONTACT_EMAIL.TabIndex = 15;
            this.buttonCONTACT_EMAIL.Text = "担当者へ連絡";
            this.buttonCONTACT_EMAIL.UseVisualStyleBackColor = true;
            this.buttonCONTACT_EMAIL.Click += new System.EventHandler(this.buttonCONTACT_EMAIL_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(1171, 496);
            this.button1.MinimumSize = new System.Drawing.Size(0, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "差分チェック";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.btnUnlock.Location = new System.Drawing.Point(1172, 9);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(120, 23);
            this.btnUnlock.TabIndex = 34;
            this.btnUnlock.Text = "読み取り専用解除";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Visible = false;
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
            this.lblScreenMode.Size = new System.Drawing.Size(383, 26);
            this.lblScreenMode.TabIndex = 35;
            this.lblScreenMode.Text = "返品入庫明細";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLockInfo
            // 
            this.lblLockInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockInfo.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.lblLockInfo.Location = new System.Drawing.Point(915, 13);
            this.lblLockInfo.Name = "lblLockInfo";
            this.lblLockInfo.Size = new System.Drawing.Size(205, 42);
            this.lblLockInfo.TabIndex = 36;
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
            this.groupBox1.Location = new System.Drawing.Point(15, 525);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1273, 389);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "売上確定済み情報の訂正承認";
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
            // blobStrageForm1
            // 
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(45, 153);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(529, 210);
            this.blobStrageForm1.StrageId = null;
            this.blobStrageForm1.TabIndex = 63;
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
            // txtApprovalStatus
            // 
            this.txtApprovalStatus.BackColor = System.Drawing.SystemColors.Window;
            this.txtApprovalStatus.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtApprovalStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtApprovalStatus.Location = new System.Drawing.Point(348, 26);
            this.txtApprovalStatus.Name = "txtApprovalStatus";
            this.txtApprovalStatus.ReadOnly = true;
            this.txtApprovalStatus.Size = new System.Drawing.Size(218, 20);
            this.txtApprovalStatus.TabIndex = 62;
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
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowFiltering = true;
            this.c1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows;
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.AutoClipboard = true;
            this.c1FlexGrid1.AutoGenerateColumns = false;
            this.c1FlexGrid1.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndColumnHeaders;
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
            this.c1FlexGrid1.Location = new System.Drawing.Point(619, 29);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(624, 299);
            this.c1FlexGrid1.TabIndex = 60;
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
            // textBoxDenNo
            // 
            this.textBoxDenNo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxDenNo.Location = new System.Drawing.Point(282, 34);
            this.textBoxDenNo.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxDenNo.Name = "textBoxDenNo";
            this.textBoxDenNo.ReadOnly = true;
            this.textBoxDenNo.Size = new System.Drawing.Size(100, 23);
            this.textBoxDenNo.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(221, 35);
            this.label1.MinimumSize = new System.Drawing.Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 70;
            this.label1.Text = "伝票番号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxHATNUMBER
            // 
            this.textBoxHATNUMBER.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxHATNUMBER.Location = new System.Drawing.Point(102, 34);
            this.textBoxHATNUMBER.MinimumSize = new System.Drawing.Size(4, 20);
            this.textBoxHATNUMBER.Name = "textBoxHATNUMBER";
            this.textBoxHATNUMBER.ReadOnly = true;
            this.textBoxHATNUMBER.Size = new System.Drawing.Size(100, 23);
            this.textBoxHATNUMBER.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.MinimumSize = new System.Drawing.Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 68;
            this.label2.Text = "HAT注文番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReturnReceivingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1304, 921);
            this.Controls.Add(this.textBoxDenNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxHATNUMBER);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLockInfo);
            this.Controls.Add(this.lblScreenMode);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCONTACT_EMAIL);
            this.Controls.Add(this.c1FlexGrid_ReturnReceivingDetail);
            this.MinimumSize = new System.Drawing.Size(1320, 766);
            this.Name = "ReturnReceivingDetail";
            this.Text = "返品入庫明細";
            this.Load += new System.EventHandler(this.ReturnReceivingDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid_ReturnReceivingDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid_ReturnReceivingDetail;
        private System.Windows.Forms.Button buttonCONTACT_EMAIL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label lblScreenMode;
        private System.Windows.Forms.Label lblLockInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private CustomControls.BlobStrage.BlobStrageForm blobStrageForm1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtApprovalStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CmbAuthorizer2;
        public C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ComboBox CmbAuthorizer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRemand;
        private System.Windows.Forms.Button btnAapproval;
        private System.Windows.Forms.Button btnApplication;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBoxDenNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHATNUMBER;
        private System.Windows.Forms.Label label2;
    }
}
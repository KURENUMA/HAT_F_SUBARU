namespace HatFClient.Views.CorrectionDelivery
{
    partial class CorrectionDeliveryDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CorrectionDeliveryDetail));
            this.c1FlexGridCorrectionDeliveryDetail = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.buttonCONTACT_EMAIL = new System.Windows.Forms.Button();
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.lblLockInfo = new System.Windows.Forms.Label();
            this.labelCode = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.grpComment = new System.Windows.Forms.GroupBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSaveComment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridCorrectionDeliveryDetail)).BeginInit();
            this.grpComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlexGridCorrectionDeliveryDetail
            // 
            this.c1FlexGridCorrectionDeliveryDetail.AllowFiltering = true;
            this.c1FlexGridCorrectionDeliveryDetail.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGridCorrectionDeliveryDetail.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.c1FlexGridCorrectionDeliveryDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGridCorrectionDeliveryDetail.AutoGenerateColumns = false;
            this.c1FlexGridCorrectionDeliveryDetail.ColumnInfo = resources.GetString("c1FlexGridCorrectionDeliveryDetail.ColumnInfo");
            this.c1FlexGridCorrectionDeliveryDetail.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.c1FlexGridCorrectionDeliveryDetail.Location = new System.Drawing.Point(14, 58);
            this.c1FlexGridCorrectionDeliveryDetail.Name = "c1FlexGridCorrectionDeliveryDetail";
            this.c1FlexGridCorrectionDeliveryDetail.Size = new System.Drawing.Size(1491, 362);
            this.c1FlexGridCorrectionDeliveryDetail.TabIndex = 15;
            this.c1FlexGridCorrectionDeliveryDetail.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1FlexGrid1_CellChecked);
            this.c1FlexGridCorrectionDeliveryDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1FlexGridCorrectionDeliveryDetail_KeyDown);
            // 
            // buttonCONTACT_EMAIL
            // 
            this.buttonCONTACT_EMAIL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCONTACT_EMAIL.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCONTACT_EMAIL.Location = new System.Drawing.Point(1385, 28);
            this.buttonCONTACT_EMAIL.MinimumSize = new System.Drawing.Size(0, 20);
            this.buttonCONTACT_EMAIL.Name = "buttonCONTACT_EMAIL";
            this.buttonCONTACT_EMAIL.Size = new System.Drawing.Size(120, 23);
            this.buttonCONTACT_EMAIL.TabIndex = 15;
            this.buttonCONTACT_EMAIL.Text = "担当者へ連絡";
            this.buttonCONTACT_EMAIL.UseVisualStyleBackColor = true;
            this.buttonCONTACT_EMAIL.Click += new System.EventHandler(this.buttonCONTACT_EMAIL_Click);
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
            this.lblScreenMode.Size = new System.Drawing.Size(596, 26);
            this.lblScreenMode.TabIndex = 35;
            this.lblScreenMode.Text = "納品一覧表（訂正・返品）";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLockInfo
            // 
            this.lblLockInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockInfo.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.lblLockInfo.Location = new System.Drawing.Point(1165, 9);
            this.lblLockInfo.Name = "lblLockInfo";
            this.lblLockInfo.Size = new System.Drawing.Size(205, 42);
            this.lblLockInfo.TabIndex = 36;
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCode.Location = new System.Drawing.Point(23, 16);
            this.labelCode.MinimumSize = new System.Drawing.Size(0, 20);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(69, 20);
            this.labelCode.TabIndex = 37;
            this.labelCode.Text = "得意先コード";
            this.labelCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelName.Location = new System.Drawing.Point(236, 16);
            this.labelName.MinimumSize = new System.Drawing.Size(0, 20);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 20);
            this.labelName.TabIndex = 39;
            this.labelName.Text = "得意先名";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpComment
            // 
            this.grpComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpComment.Controls.Add(this.txtComment);
            this.grpComment.Controls.Add(this.btnSaveComment);
            this.grpComment.Location = new System.Drawing.Point(970, 426);
            this.grpComment.Name = "grpComment";
            this.grpComment.Size = new System.Drawing.Size(535, 178);
            this.grpComment.TabIndex = 40;
            this.grpComment.TabStop = false;
            this.grpComment.Text = "確認コメント";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(6, 18);
            this.txtComment.MaxLength = 1000;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(523, 125);
            this.txtComment.TabIndex = 0;
            this.txtComment.TextChanged += new System.EventHandler(this.txtComment_TextChanged);
            // 
            // btnSaveComment
            // 
            this.btnSaveComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveComment.Enabled = false;
            this.btnSaveComment.Location = new System.Drawing.Point(454, 149);
            this.btnSaveComment.Name = "btnSaveComment";
            this.btnSaveComment.Size = new System.Drawing.Size(75, 23);
            this.btnSaveComment.TabIndex = 1;
            this.btnSaveComment.Text = "保存";
            this.btnSaveComment.UseVisualStyleBackColor = true;
            this.btnSaveComment.Click += new System.EventHandler(this.btnSaveComment_Click);
            // 
            // CorrectionDeliveryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1517, 606);
            this.Controls.Add(this.grpComment);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.lblLockInfo);
            this.Controls.Add(this.lblScreenMode);
            this.Controls.Add(this.buttonCONTACT_EMAIL);
            this.Controls.Add(this.c1FlexGridCorrectionDeliveryDetail);
            this.Name = "CorrectionDeliveryDetail";
            this.Text = "納品一覧表（訂正・返品）";
            this.Load += new System.EventHandler(this.CorrectionDeliveryDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridCorrectionDeliveryDetail)).EndInit();
            this.grpComment.ResumeLayout(false);
            this.grpComment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridCorrectionDeliveryDetail;
        private System.Windows.Forms.Button buttonCONTACT_EMAIL;
        private System.Windows.Forms.Label lblScreenMode;
        private System.Windows.Forms.Label lblLockInfo;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.GroupBox grpComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnSaveComment;
    }
}
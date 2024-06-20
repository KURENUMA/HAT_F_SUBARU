namespace HatFClient.Views.Warehousing
{
    partial class WH_ShippingsDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WH_ShippingsDetail));
            this.grdDataView = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDenNo = new System.Windows.Forms.Label();
            this.txtDenNo = new System.Windows.Forms.TextBox();
            this.lblShippedDate = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblDetail = new System.Windows.Forms.Label();
            this.dtDueDate = new HatFClient.CustomControls.C1DateInputEx();
            this.dtShippedDate = new HatFClient.CustomControls.C1DateInputEx();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtShippedDate)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDataView
            // 
            this.grdDataView.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.grdDataView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataView.ColumnInfo = resources.GetString("grdDataView.ColumnInfo");
            this.grdDataView.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdDataView.Location = new System.Drawing.Point(12, 86);
            this.grdDataView.Name = "grdDataView";
            this.grdDataView.Size = new System.Drawing.Size(868, 138);
            this.grdDataView.TabIndex = 3001;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(625, 340);
            this.btnSave.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 25);
            this.btnSave.TabIndex = 2001;
            this.btnSave.Text = "修正";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancel.Location = new System.Drawing.Point(749, 340);
            this.btnCancel.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 25);
            this.btnCancel.TabIndex = 2002;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDenNo
            // 
            this.lblDenNo.AutoSize = true;
            this.lblDenNo.Location = new System.Drawing.Point(15, 27);
            this.lblDenNo.Name = "lblDenNo";
            this.lblDenNo.Size = new System.Drawing.Size(53, 12);
            this.lblDenNo.TabIndex = 25;
            this.lblDenNo.Text = "伝票番号";
            // 
            // txtDenNo
            // 
            this.txtDenNo.Location = new System.Drawing.Point(90, 27);
            this.txtDenNo.Name = "txtDenNo";
            this.txtDenNo.ReadOnly = true;
            this.txtDenNo.Size = new System.Drawing.Size(200, 19);
            this.txtDenNo.TabIndex = 1001;
            // 
            // lblShippedDate
            // 
            this.lblShippedDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblShippedDate.AutoSize = true;
            this.lblShippedDate.Location = new System.Drawing.Point(17, 271);
            this.lblShippedDate.Name = "lblShippedDate";
            this.lblShippedDate.Size = new System.Drawing.Size(41, 12);
            this.lblShippedDate.TabIndex = 27;
            this.lblShippedDate.Text = "出荷日";
            // 
            // lblDueDate
            // 
            this.lblDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(17, 302);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(65, 12);
            this.lblDueDate.TabIndex = 30;
            this.lblDueDate.Text = "到着予定日";
            // 
            // lblDetail
            // 
            this.lblDetail.AutoSize = true;
            this.lblDetail.Location = new System.Drawing.Point(15, 71);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(53, 12);
            this.lblDetail.TabIndex = 3002;
            this.lblDetail.Text = "出荷詳細";
            // 
            // dtDueDate
            // 
            this.dtDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtDueDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtDueDate.DateTimeInput = false;
            this.dtDueDate.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtDueDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtDueDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtDueDate.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtDueDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtDueDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtDueDate.EditMask = "90/90/90";
            this.dtDueDate.EmptyAsNull = true;
            this.dtDueDate.ExitOnLastChar = true;
            this.dtDueDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtDueDate.GapHeight = 0;
            this.dtDueDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtDueDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtDueDate.Location = new System.Drawing.Point(90, 299);
            this.dtDueDate.LoopPosition = false;
            this.dtDueDate.MaskInfo.AutoTabWhenFilled = true;
            this.dtDueDate.MaxLength = 8;
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(200, 17);
            this.dtDueDate.TabIndex = 1003;
            this.dtDueDate.Tag = null;
            this.dtDueDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtShippedDate
            // 
            this.dtShippedDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtShippedDate.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtShippedDate.DateTimeInput = false;
            this.dtShippedDate.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtShippedDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtShippedDate.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtShippedDate.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtShippedDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtShippedDate.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtShippedDate.EditMask = "90/90/90";
            this.dtShippedDate.EmptyAsNull = true;
            this.dtShippedDate.ExitOnLastChar = true;
            this.dtShippedDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtShippedDate.GapHeight = 0;
            this.dtShippedDate.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtShippedDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtShippedDate.Location = new System.Drawing.Point(90, 268);
            this.dtShippedDate.LoopPosition = false;
            this.dtShippedDate.MaskInfo.AutoTabWhenFilled = true;
            this.dtShippedDate.MaxLength = 8;
            this.dtShippedDate.Name = "dtShippedDate";
            this.dtShippedDate.Size = new System.Drawing.Size(200, 17);
            this.dtShippedDate.TabIndex = 1002;
            this.dtShippedDate.Tag = null;
            this.dtShippedDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // WH_ShippingsDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(892, 377);
            this.Controls.Add(this.lblDetail);
            this.Controls.Add(this.dtDueDate);
            this.Controls.Add(this.lblDueDate);
            this.Controls.Add(this.dtShippedDate);
            this.Controls.Add(this.lblShippedDate);
            this.Controls.Add(this.txtDenNo);
            this.Controls.Add(this.lblDenNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grdDataView);
            this.Name = "WH_ShippingsDetail";
            this.Text = "出荷詳細";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WH_ShippingsDetail_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtShippedDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1FlexGrid.C1FlexGrid grdDataView;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDenNo;
        private System.Windows.Forms.TextBox txtDenNo;
        private System.Windows.Forms.Label lblShippedDate;
        private CustomControls.C1DateInputEx dtShippedDate;
        private System.Windows.Forms.Label lblDueDate;
        private CustomControls.C1DateInputEx dtDueDate;
        private System.Windows.Forms.Label lblDetail;
    }
}
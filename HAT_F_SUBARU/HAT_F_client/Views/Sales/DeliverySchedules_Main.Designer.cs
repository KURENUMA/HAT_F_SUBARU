namespace HatFClient.Views.Sales
{
    partial class DeliverySchedules_Main
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
            this.btnAdvancedSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel出力 = new System.Windows.Forms.Button();
            this.textFilterStr = new System.Windows.Forms.TextBox();
            this.lblProjectAllCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchCustomer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtNoubiFrom = new HatFClient.CustomControls.C1DateInputEx();
            this.dtNoubiTo = new HatFClient.CustomControls.C1DateInputEx();
            this.txtCompCode = new HatFClient.CustomControls.TextBoxChar();
            this.gridPatternUI = new HatFClient.CustomControls.GridPatternUI();
            ((System.ComponentModel.ISupportInitialize)(this.dtNoubiFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNoubiTo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdvancedSearch
            // 
            this.btnAdvancedSearch.Location = new System.Drawing.Point(12, 41);
            this.btnAdvancedSearch.Name = "btnAdvancedSearch";
            this.btnAdvancedSearch.Size = new System.Drawing.Size(93, 23);
            this.btnAdvancedSearch.TabIndex = 8;
            this.btnAdvancedSearch.Text = "詳細検索";
            this.btnAdvancedSearch.UseVisualStyleBackColor = true;
            this.btnAdvancedSearch.Click += new System.EventHandler(this.BtnAdvancedSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 165);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 745);
            this.panel1.TabIndex = 13;
            // 
            // btnExcel出力
            // 
            this.btnExcel出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel出力.Location = new System.Drawing.Point(1154, 88);
            this.btnExcel出力.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcel出力.Name = "btnExcel出力";
            this.btnExcel出力.Size = new System.Drawing.Size(103, 23);
            this.btnExcel出力.TabIndex = 11;
            this.btnExcel出力.Text = "Excel印刷";
            this.btnExcel出力.UseVisualStyleBackColor = true;
            this.btnExcel出力.Click += new System.EventHandler(this.BtnExcel出力_Click);
            // 
            // textFilterStr
            // 
            this.textFilterStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFilterStr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textFilterStr.Font = new System.Drawing.Font("Yu Gothic UI", 12F);
            this.textFilterStr.Location = new System.Drawing.Point(116, 41);
            this.textFilterStr.Multiline = true;
            this.textFilterStr.Name = "textFilterStr";
            this.textFilterStr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textFilterStr.Size = new System.Drawing.Size(564, 90);
            this.textFilterStr.TabIndex = 9;
            // 
            // lblProjectAllCount
            // 
            this.lblProjectAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProjectAllCount.AutoSize = true;
            this.lblProjectAllCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProjectAllCount.Location = new System.Drawing.Point(8, 142);
            this.lblProjectAllCount.Name = "lblProjectAllCount";
            this.lblProjectAllCount.Size = new System.Drawing.Size(90, 21);
            this.lblProjectAllCount.TabIndex = 12;
            this.lblProjectAllCount.Text = "検索結果：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(8, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "得意先コード*";
            // 
            // btnSearchCustomer
            // 
            this.btnSearchCustomer.Location = new System.Drawing.Point(200, 10);
            this.btnSearchCustomer.Name = "btnSearchCustomer";
            this.btnSearchCustomer.Size = new System.Drawing.Size(88, 23);
            this.btnSearchCustomer.TabIndex = 2;
            this.btnSearchCustomer.Text = "得意先検索";
            this.btnSearchCustomer.UseVisualStyleBackColor = true;
            this.btnSearchCustomer.Click += new System.EventHandler(this.BtnSearchCustomer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(314, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "納日";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(448, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "～";
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(577, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // dtNoubiFrom
            // 
            this.dtNoubiFrom.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtNoubiFrom.DateTimeInput = false;
            this.dtNoubiFrom.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtNoubiFrom.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiFrom.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNoubiFrom.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtNoubiFrom.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiFrom.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNoubiFrom.EditMask = "90/90/90";
            this.dtNoubiFrom.EmptyAsNull = true;
            this.dtNoubiFrom.ExitOnLastChar = true;
            this.dtNoubiFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiFrom.GapHeight = 0;
            this.dtNoubiFrom.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtNoubiFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtNoubiFrom.Location = new System.Drawing.Point(362, 12);
            this.dtNoubiFrom.LoopPosition = false;
            this.dtNoubiFrom.MaskInfo.AutoTabWhenFilled = true;
            this.dtNoubiFrom.MaxLength = 8;
            this.dtNoubiFrom.Name = "dtNoubiFrom";
            this.dtNoubiFrom.Size = new System.Drawing.Size(80, 19);
            this.dtNoubiFrom.TabIndex = 4;
            this.dtNoubiFrom.Tag = null;
            this.dtNoubiFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtNoubiTo
            // 
            this.dtNoubiTo.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtNoubiTo.DateTimeInput = false;
            this.dtNoubiTo.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtNoubiTo.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiTo.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNoubiTo.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtNoubiTo.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiTo.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNoubiTo.EditMask = "90/90/90";
            this.dtNoubiTo.EmptyAsNull = true;
            this.dtNoubiTo.ExitOnLastChar = true;
            this.dtNoubiTo.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNoubiTo.GapHeight = 0;
            this.dtNoubiTo.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtNoubiTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtNoubiTo.Location = new System.Drawing.Point(480, 12);
            this.dtNoubiTo.LoopPosition = false;
            this.dtNoubiTo.MaskInfo.AutoTabWhenFilled = true;
            this.dtNoubiTo.MaxLength = 8;
            this.dtNoubiTo.Name = "dtNoubiTo";
            this.dtNoubiTo.Size = new System.Drawing.Size(80, 19);
            this.dtNoubiTo.TabIndex = 6;
            this.dtNoubiTo.Tag = null;
            this.dtNoubiTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtCompCode
            // 
            this.txtCompCode.Location = new System.Drawing.Point(116, 12);
            this.txtCompCode.MaxLength = 8;
            this.txtCompCode.Name = "txtCompCode";
            this.txtCompCode.Size = new System.Drawing.Size(78, 19);
            this.txtCompCode.TabIndex = 1;
            this.txtCompCode.TextChanged += new System.EventHandler(this.TxtCompCode_TextChanged);
            // 
            // gridPatternUI
            // 
            this.gridPatternUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatternUI.Location = new System.Drawing.Point(813, 12);
            this.gridPatternUI.Name = "gridPatternUI";
            this.gridPatternUI.Size = new System.Drawing.Size(444, 71);
            this.gridPatternUI.TabIndex = 10;
            // 
            // DeliverySchedules_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.dtNoubiFrom);
            this.Controls.Add(this.dtNoubiTo);
            this.Controls.Add(this.txtCompCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearchCustomer);
            this.Controls.Add(this.gridPatternUI);
            this.Controls.Add(this.textFilterStr);
            this.Controls.Add(this.lblProjectAllCount);
            this.Controls.Add(this.btnExcel出力);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAdvancedSearch);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1285, 960);
            this.Name = "DeliverySchedules_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "得意先別納品予定一覧";
            this.Load += new System.EventHandler(this.DeliverySchedules_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtNoubiFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNoubiTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAdvancedSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textFilterStr;
        private System.Windows.Forms.Button btnExcel出力;
        private System.Windows.Forms.Label lblProjectAllCount;
        private CustomControls.GridPatternUI gridPatternUI;
        private CustomControls.TextBoxChar txtCompCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CustomControls.C1DateInputEx dtNoubiTo;
        private CustomControls.C1DateInputEx dtNoubiFrom;
        private System.Windows.Forms.Button btnSearch;
    }
}
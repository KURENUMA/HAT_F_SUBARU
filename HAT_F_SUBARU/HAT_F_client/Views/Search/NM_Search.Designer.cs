namespace HatFClient.Views.Search
{
    partial class NM_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NM_Search));
            this.txtTOKUI_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblTOKUI_CD = new System.Windows.Forms.Label();
            this.txtTEAM_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblTEAM_CD = new System.Windows.Forms.Label();
            this.dateHAT_NYUKABI = new HatFClient.CustomControls.C1DateInputEx();
            this.lblHAT_NYUKABI = new System.Windows.Forms.Label();
            this.btnFnc10 = new System.Windows.Forms.Button();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.txtSHIRESAKI_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblSHIRESAKI_CD = new System.Windows.Forms.Label();
            this.txtNYU2_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblNYU2_CD = new System.Windows.Forms.Label();
            this.txtJYU2_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblJYU2_CD = new System.Windows.Forms.Label();
            this.txtTANTO_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblTANTO_CD = new System.Windows.Forms.Label();
            this.txtDEN_NO = new HatFClient.CustomControls.TextBoxChar();
            this.lblDEN_NO = new System.Windows.Forms.Label();
            this.txtHAT_ORDER_NO = new HatFClient.CustomControls.TextBoxChar();
            this.lblHAT_ORDER_NO = new System.Windows.Forms.Label();
            this.txtCUST_ORDERNO = new HatFClient.CustomControls.TextBoxChar();
            this.lblCUST_ORDERNO = new System.Windows.Forms.Label();
            this.btnFnc08 = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dateHAT_NYUKABI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTOKUI_CD
            // 
            this.txtTOKUI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOKUI_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTOKUI_CD.Location = new System.Drawing.Point(264, 9);
            this.txtTOKUI_CD.MaxLength = 6;
            this.txtTOKUI_CD.Name = "txtTOKUI_CD";
            this.txtTOKUI_CD.Size = new System.Drawing.Size(65, 27);
            this.txtTOKUI_CD.TabIndex = 1;
            this.txtTOKUI_CD.Text = "999999";
            this.txtTOKUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTOKUI_CD
            // 
            this.lblTOKUI_CD.AutoSize = true;
            this.lblTOKUI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_CD.Location = new System.Drawing.Point(182, 12);
            this.lblTOKUI_CD.Name = "lblTOKUI_CD";
            this.lblTOKUI_CD.Size = new System.Drawing.Size(75, 19);
            this.lblTOKUI_CD.TabIndex = 76;
            this.lblTOKUI_CD.Text = "得意先CD";
            // 
            // txtTEAM_CD
            // 
            this.txtTEAM_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTEAM_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTEAM_CD.Location = new System.Drawing.Point(88, 9);
            this.txtTEAM_CD.MaxLength = 3;
            this.txtTEAM_CD.Name = "txtTEAM_CD";
            this.txtTEAM_CD.Size = new System.Drawing.Size(40, 27);
            this.txtTEAM_CD.TabIndex = 0;
            this.txtTEAM_CD.Text = "999";
            this.txtTEAM_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTEAM_CD
            // 
            this.lblTEAM_CD.AutoSize = true;
            this.lblTEAM_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTEAM_CD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTEAM_CD.Location = new System.Drawing.Point(3, 12);
            this.lblTEAM_CD.Name = "lblTEAM_CD";
            this.lblTEAM_CD.Size = new System.Drawing.Size(66, 19);
            this.lblTEAM_CD.TabIndex = 75;
            this.lblTEAM_CD.Text = "チームCD";
            // 
            // dateHAT_NYUKABI
            // 
            this.dateHAT_NYUKABI.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dateHAT_NYUKABI.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dateHAT_NYUKABI.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateHAT_NYUKABI.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dateHAT_NYUKABI.EditFormat.CustomFormat = "yy/MM/dd";
            this.dateHAT_NYUKABI.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateHAT_NYUKABI.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dateHAT_NYUKABI.EmptyAsNull = true;
            this.dateHAT_NYUKABI.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dateHAT_NYUKABI.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateHAT_NYUKABI.GapHeight = 0;
            this.dateHAT_NYUKABI.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dateHAT_NYUKABI.Location = new System.Drawing.Point(623, 69);
            this.dateHAT_NYUKABI.Name = "dateHAT_NYUKABI";
            this.dateHAT_NYUKABI.Size = new System.Drawing.Size(104, 25);
            this.dateHAT_NYUKABI.TabIndex = 9;
            this.dateHAT_NYUKABI.Tag = null;
            this.dateHAT_NYUKABI.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblHAT_NYUKABI
            // 
            this.lblHAT_NYUKABI.AutoSize = true;
            this.lblHAT_NYUKABI.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHAT_NYUKABI.Location = new System.Drawing.Point(556, 72);
            this.lblHAT_NYUKABI.Name = "lblHAT_NYUKABI";
            this.lblHAT_NYUKABI.Size = new System.Drawing.Size(54, 19);
            this.lblHAT_NYUKABI.TabIndex = 135;
            this.lblHAT_NYUKABI.Text = "入荷日";
            // 
            // btnFnc10
            // 
            this.btnFnc10.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc10.Location = new System.Drawing.Point(1135, 70);
            this.btnFnc10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc10.Name = "btnFnc10";
            this.btnFnc10.Size = new System.Drawing.Size(120, 30);
            this.btnFnc10.TabIndex = 138;
            this.btnFnc10.TabStop = false;
            this.btnFnc10.Text = "F10:条件クリア";
            this.btnFnc10.UseVisualStyleBackColor = true;
            this.btnFnc10.Click += new System.EventHandler(this.BtnFnc10_Click);
            // 
            // btnFnc09
            // 
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(1001, 70);
            this.btnFnc09.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc09.Name = "btnFnc09";
            this.btnFnc09.Size = new System.Drawing.Size(120, 30);
            this.btnFnc09.TabIndex = 137;
            this.btnFnc09.TabStop = false;
            this.btnFnc09.Text = "F9:検索";
            this.btnFnc09.UseVisualStyleBackColor = true;
            this.btnFnc09.Click += new System.EventHandler(this.BtnFnc09_Click);
            // 
            // grdList
            // 
            this.grdList.BackColor = System.Drawing.Color.DarkGray;
            this.grdList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.grdList.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.grdList.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdList.Location = new System.Drawing.Point(5, 106);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 1;
            this.grdList.Rows.DefaultSize = 30;
            this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.grdList.Size = new System.Drawing.Size(1252, 772);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.TabIndex = 10;
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxCount.Location = new System.Drawing.Point(11, 888);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(111, 19);
            this.lblMaxCount.TabIndex = 142;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // btnFnc12
            // 
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(1135, 884);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(120, 30);
            this.btnFnc12.TabIndex = 141;
            this.btnFnc12.TabStop = false;
            this.btnFnc12.Text = "F12:閉じる";
            this.btnFnc12.UseVisualStyleBackColor = true;
            this.btnFnc12.Click += new System.EventHandler(this.BtnFnc12_Click);
            // 
            // btnFnc11
            // 
            this.btnFnc11.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc11.Location = new System.Drawing.Point(1001, 884);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(120, 30);
            this.btnFnc11.TabIndex = 140;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // txtSHIRESAKI_CD
            // 
            this.txtSHIRESAKI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSHIRESAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSHIRESAKI_CD.Location = new System.Drawing.Point(459, 9);
            this.txtSHIRESAKI_CD.MaxLength = 6;
            this.txtSHIRESAKI_CD.Name = "txtSHIRESAKI_CD";
            this.txtSHIRESAKI_CD.Size = new System.Drawing.Size(65, 27);
            this.txtSHIRESAKI_CD.TabIndex = 2;
            this.txtSHIRESAKI_CD.Text = "999999";
            this.txtSHIRESAKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSHIRESAKI_CD
            // 
            this.lblSHIRESAKI_CD.AutoSize = true;
            this.lblSHIRESAKI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSHIRESAKI_CD.Location = new System.Drawing.Point(374, 12);
            this.lblSHIRESAKI_CD.Name = "lblSHIRESAKI_CD";
            this.lblSHIRESAKI_CD.Size = new System.Drawing.Size(75, 19);
            this.lblSHIRESAKI_CD.TabIndex = 149;
            this.lblSHIRESAKI_CD.Text = "仕入先CD";
            // 
            // txtNYU2_CD
            // 
            this.txtNYU2_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNYU2_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNYU2_CD.Location = new System.Drawing.Point(88, 39);
            this.txtNYU2_CD.MaxLength = 4;
            this.txtNYU2_CD.Name = "txtNYU2_CD";
            this.txtNYU2_CD.Size = new System.Drawing.Size(50, 27);
            this.txtNYU2_CD.TabIndex = 3;
            this.txtNYU2_CD.Text = "9999";
            this.txtNYU2_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNYU2_CD
            // 
            this.lblNYU2_CD.AutoSize = true;
            this.lblNYU2_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNYU2_CD.Location = new System.Drawing.Point(3, 42);
            this.lblNYU2_CD.Name = "lblNYU2_CD";
            this.lblNYU2_CD.Size = new System.Drawing.Size(75, 19);
            this.lblNYU2_CD.TabIndex = 151;
            this.lblNYU2_CD.Text = "入力者CD";
            // 
            // txtJYU2_CD
            // 
            this.txtJYU2_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJYU2_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtJYU2_CD.Location = new System.Drawing.Point(264, 39);
            this.txtJYU2_CD.MaxLength = 4;
            this.txtJYU2_CD.Name = "txtJYU2_CD";
            this.txtJYU2_CD.Size = new System.Drawing.Size(50, 27);
            this.txtJYU2_CD.TabIndex = 4;
            this.txtJYU2_CD.Text = "9999";
            this.txtJYU2_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblJYU2_CD
            // 
            this.lblJYU2_CD.AutoSize = true;
            this.lblJYU2_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblJYU2_CD.Location = new System.Drawing.Point(182, 42);
            this.lblJYU2_CD.Name = "lblJYU2_CD";
            this.lblJYU2_CD.Size = new System.Drawing.Size(75, 19);
            this.lblJYU2_CD.TabIndex = 153;
            this.lblJYU2_CD.Text = "受注者CD";
            // 
            // txtTANTO_CD
            // 
            this.txtTANTO_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTANTO_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTANTO_CD.Location = new System.Drawing.Point(459, 39);
            this.txtTANTO_CD.MaxLength = 4;
            this.txtTANTO_CD.Name = "txtTANTO_CD";
            this.txtTANTO_CD.Size = new System.Drawing.Size(50, 27);
            this.txtTANTO_CD.TabIndex = 5;
            this.txtTANTO_CD.Text = "9999";
            this.txtTANTO_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTANTO_CD
            // 
            this.lblTANTO_CD.AutoSize = true;
            this.lblTANTO_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTANTO_CD.Location = new System.Drawing.Point(374, 42);
            this.lblTANTO_CD.Name = "lblTANTO_CD";
            this.lblTANTO_CD.Size = new System.Drawing.Size(75, 19);
            this.lblTANTO_CD.TabIndex = 155;
            this.lblTANTO_CD.Text = "担当者CD";
            // 
            // txtDEN_NO
            // 
            this.txtDEN_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDEN_NO.Location = new System.Drawing.Point(623, 39);
            this.txtDEN_NO.MaxLength = 6;
            this.txtDEN_NO.Name = "txtDEN_NO";
            this.txtDEN_NO.Size = new System.Drawing.Size(65, 27);
            this.txtDEN_NO.TabIndex = 6;
            this.txtDEN_NO.Text = "123456";
            // 
            // lblDEN_NO
            // 
            this.lblDEN_NO.AutoSize = true;
            this.lblDEN_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDEN_NO.Location = new System.Drawing.Point(544, 42);
            this.lblDEN_NO.Name = "lblDEN_NO";
            this.lblDEN_NO.Size = new System.Drawing.Size(69, 19);
            this.lblDEN_NO.TabIndex = 157;
            this.lblDEN_NO.Text = "伝票番号";
            // 
            // txtHAT_ORDER_NO
            // 
            this.txtHAT_ORDER_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHAT_ORDER_NO.Location = new System.Drawing.Point(88, 69);
            this.txtHAT_ORDER_NO.MaxLength = 7;
            this.txtHAT_ORDER_NO.Name = "txtHAT_ORDER_NO";
            this.txtHAT_ORDER_NO.Size = new System.Drawing.Size(82, 27);
            this.txtHAT_ORDER_NO.TabIndex = 7;
            this.txtHAT_ORDER_NO.Text = "1234567";
            // 
            // lblHAT_ORDER_NO
            // 
            this.lblHAT_ORDER_NO.AutoSize = true;
            this.lblHAT_ORDER_NO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHAT_ORDER_NO.Location = new System.Drawing.Point(3, 72);
            this.lblHAT_ORDER_NO.Name = "lblHAT_ORDER_NO";
            this.lblHAT_ORDER_NO.Size = new System.Drawing.Size(69, 19);
            this.lblHAT_ORDER_NO.TabIndex = 159;
            this.lblHAT_ORDER_NO.Text = "HAT注番";
            // 
            // txtCUST_ORDERNO
            // 
            this.txtCUST_ORDERNO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCUST_ORDERNO.Location = new System.Drawing.Point(264, 69);
            this.txtCUST_ORDERNO.MaxLength = 20;
            this.txtCUST_ORDERNO.Name = "txtCUST_ORDERNO";
            this.txtCUST_ORDERNO.Size = new System.Drawing.Size(190, 27);
            this.txtCUST_ORDERNO.TabIndex = 8;
            this.txtCUST_ORDERNO.Text = "12345678901234567890";
            // 
            // lblCUST_ORDERNO
            // 
            this.lblCUST_ORDERNO.AutoSize = true;
            this.lblCUST_ORDERNO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCUST_ORDERNO.Location = new System.Drawing.Point(188, 72);
            this.lblCUST_ORDERNO.Name = "lblCUST_ORDERNO";
            this.lblCUST_ORDERNO.Size = new System.Drawing.Size(69, 19);
            this.lblCUST_ORDERNO.TabIndex = 161;
            this.lblCUST_ORDERNO.Text = "客先注番";
            // 
            // btnFnc08
            // 
            this.btnFnc08.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc08.Location = new System.Drawing.Point(866, 884);
            this.btnFnc08.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc08.Name = "btnFnc08";
            this.btnFnc08.Size = new System.Drawing.Size(120, 30);
            this.btnFnc08.TabIndex = 162;
            this.btnFnc08.TabStop = false;
            this.btnFnc08.Text = "F8:印刷";
            this.btnFnc08.UseVisualStyleBackColor = true;
            this.btnFnc08.Click += new System.EventHandler(this.BtnFnc08_Click);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.Location = new System.Drawing.Point(156, 888);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(96, 19);
            this.lblNote.TabIndex = 180;
            this.lblNote.Text = "※入荷日が…";
            // 
            // NM_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnFnc08);
            this.Controls.Add(this.txtCUST_ORDERNO);
            this.Controls.Add(this.lblCUST_ORDERNO);
            this.Controls.Add(this.txtHAT_ORDER_NO);
            this.Controls.Add(this.lblHAT_ORDER_NO);
            this.Controls.Add(this.txtDEN_NO);
            this.Controls.Add(this.lblDEN_NO);
            this.Controls.Add(this.txtTANTO_CD);
            this.Controls.Add(this.lblTANTO_CD);
            this.Controls.Add(this.txtJYU2_CD);
            this.Controls.Add(this.lblJYU2_CD);
            this.Controls.Add(this.txtNYU2_CD);
            this.Controls.Add(this.lblNYU2_CD);
            this.Controls.Add(this.txtSHIRESAKI_CD);
            this.Controls.Add(this.lblSHIRESAKI_CD);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.btnFnc10);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.dateHAT_NYUKABI);
            this.Controls.Add(this.lblHAT_NYUKABI);
            this.Controls.Add(this.txtTOKUI_CD);
            this.Controls.Add(this.lblTOKUI_CD);
            this.Controls.Add(this.txtTEAM_CD);
            this.Controls.Add(this.lblTEAM_CD);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NM_Search";
            this.Text = "未照合リスト画面";
            this.Load += new System.EventHandler(this.M_Search_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.M_Search_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dateHAT_NYUKABI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.TextBoxChar txtTOKUI_CD;
        private System.Windows.Forms.Label lblTOKUI_CD;
        private CustomControls.TextBoxChar txtTEAM_CD;
        private System.Windows.Forms.Label lblTEAM_CD;
        private CustomControls.C1DateInputEx dateHAT_NYUKABI;
        private System.Windows.Forms.Label lblHAT_NYUKABI;
        private System.Windows.Forms.Button btnFnc10;
        private System.Windows.Forms.Button btnFnc09;
        private C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.Button btnFnc12;
        private System.Windows.Forms.Button btnFnc11;
        private CustomControls.TextBoxChar txtSHIRESAKI_CD;
        private System.Windows.Forms.Label lblSHIRESAKI_CD;
        private CustomControls.TextBoxChar txtNYU2_CD;
        private System.Windows.Forms.Label lblNYU2_CD;
        private CustomControls.TextBoxChar txtJYU2_CD;
        private System.Windows.Forms.Label lblJYU2_CD;
        private CustomControls.TextBoxChar txtTANTO_CD;
        private System.Windows.Forms.Label lblTANTO_CD;
        private CustomControls.TextBoxChar txtDEN_NO;
        private System.Windows.Forms.Label lblDEN_NO;
        private CustomControls.TextBoxChar txtHAT_ORDER_NO;
        private System.Windows.Forms.Label lblHAT_ORDER_NO;
        private CustomControls.TextBoxChar txtCUST_ORDERNO;
        private System.Windows.Forms.Label lblCUST_ORDERNO;
        private System.Windows.Forms.Button btnFnc08;
        private System.Windows.Forms.Label lblNote;
    }
}
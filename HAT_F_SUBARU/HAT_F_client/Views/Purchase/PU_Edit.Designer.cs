namespace HatFClient.Views.Purchase
{
    partial class PU_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PU_Edit));
            this.lblScreenMode = new System.Windows.Forms.Label();
            this.grdPuImports = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSAVE = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSearchSupplier = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearcyPaySupplier = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetPaySupCode = new System.Windows.Forms.Button();
            this.btnSetDspNo = new System.Windows.Forms.Button();
            this.btnSetNouki = new System.Windows.Forms.Button();
            this.labelTOTAL_PRICE = new System.Windows.Forms.Label();
            this.txtDeliveryNo = new HatFClient.CustomControls.TextBoxChar();
            this.txtPaySupCode = new HatFClient.CustomControls.TextBoxChar();
            this.txtHatOrderNo = new HatFClient.CustomControls.TextBoxChar();
            this.txtPuCode = new HatFClient.CustomControls.TextBoxChar();
            this.txtTotalPrice = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtPaySupName = new HatFClient.CustomControls.TextBoxReadOnly();
            this.dtPayDateTo = new HatFClient.CustomControls.C1DateInputEx();
            this.dtNouki = new HatFClient.CustomControls.C1DateInputEx();
            this.dtPayDateFrom = new HatFClient.CustomControls.C1DateInputEx();
            this.txtPuName = new HatFClient.CustomControls.TextBoxReadOnly();
            ((System.ComponentModel.ISupportInitialize)(this.grdPuImports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNouki)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScreenMode
            // 
            this.lblScreenMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScreenMode.BackColor = System.Drawing.Color.Transparent;
            this.lblScreenMode.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblScreenMode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblScreenMode.Location = new System.Drawing.Point(328, 9);
            this.lblScreenMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScreenMode.Name = "lblScreenMode";
            this.lblScreenMode.Size = new System.Drawing.Size(343, 26);
            this.lblScreenMode.TabIndex = 0;
            this.lblScreenMode.Text = "仕入編集";
            this.lblScreenMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdPuImports
            // 
            this.grdPuImports.AllowAddNew = true;
            this.grdPuImports.AllowDelete = true;
            this.grdPuImports.AllowFiltering = true;
            this.grdPuImports.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.grdPuImports.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictCols;
            this.grdPuImports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPuImports.AutoGenerateColumns = false;
            this.grdPuImports.ColumnInfo = resources.GetString("grdPuImports.ColumnInfo");
            this.grdPuImports.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdPuImports.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.grdPuImports.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.grdPuImports.Location = new System.Drawing.Point(11, 137);
            this.grdPuImports.Name = "grdPuImports";
            this.grdPuImports.Rows.Count = 1;
            this.grdPuImports.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.CellRange;
            this.grdPuImports.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.WhenEditing;
            this.grdPuImports.Size = new System.Drawing.Size(909, 424);
            this.grdPuImports.StyleInfo = resources.GetString("grdPuImports.StyleInfo");
            this.grdPuImports.TabIndex = 12;
            this.grdPuImports.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdPuImports_CellButtonClick);
            this.grdPuImports.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdPuImports_CellChecked);
            this.grdPuImports.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdPuImports_CellChanged);
            this.grdPuImports.DataSourceChanged += new System.EventHandler(this.GrdPuImports_DataSourceChanged);
            this.grdPuImports.AfterAddRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdPuImports_AfterAddRow);
            this.grdPuImports.AfterDeleteRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.GrdPuImports_AfterDeleteRow);
            // 
            // btnSAVE
            // 
            this.btnSAVE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSAVE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSAVE.Location = new System.Drawing.Point(845, 624);
            this.btnSAVE.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSAVE.Name = "btnSAVE";
            this.btnSAVE.Size = new System.Drawing.Size(75, 23);
            this.btnSAVE.TabIndex = 26;
            this.btnSAVE.Text = "保存";
            this.btnSAVE.UseVisualStyleBackColor = true;
            this.btnSAVE.Click += new System.EventHandler(this.BtnSAVE_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(200, 109);
            this.label3.MinimumSize = new System.Drawing.Size(0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(12, 109);
            this.label10.MinimumSize = new System.Drawing.Size(0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "仕入支払日";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearchSupplier
            // 
            this.btnSearchSupplier.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearchSupplier.Location = new System.Drawing.Point(423, 50);
            this.btnSearchSupplier.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSearchSupplier.Name = "btnSearchSupplier";
            this.btnSearchSupplier.Size = new System.Drawing.Size(75, 23);
            this.btnSearchSupplier.TabIndex = 4;
            this.btnSearchSupplier.Text = "仕入先検索";
            this.btnSearchSupplier.UseVisualStyleBackColor = true;
            this.btnSearchSupplier.Click += new System.EventHandler(this.BtnSearchSupplier_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(423, 107);
            this.btnSearch.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.MinimumSize = new System.Drawing.Size(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "HAT注文番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.MinimumSize = new System.Drawing.Size(0, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "仕入先コード*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearcyPaySupplier
            // 
            this.btnSearcyPaySupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearcyPaySupplier.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearcyPaySupplier.Location = new System.Drawing.Point(423, 566);
            this.btnSearcyPaySupplier.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSearcyPaySupplier.Name = "btnSearcyPaySupplier";
            this.btnSearcyPaySupplier.Size = new System.Drawing.Size(75, 23);
            this.btnSearcyPaySupplier.TabIndex = 16;
            this.btnSearcyPaySupplier.Text = "支払先検索";
            this.btnSearcyPaySupplier.UseVisualStyleBackColor = true;
            this.btnSearcyPaySupplier.Click += new System.EventHandler(this.BtnSearcyPaySupplier_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(12, 568);
            this.label1.MinimumSize = new System.Drawing.Size(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "支払先コード";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(12, 597);
            this.label5.MinimumSize = new System.Drawing.Size(0, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "納品書番号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(12, 626);
            this.label6.MinimumSize = new System.Drawing.Size(0, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "納入日";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetPaySupCode
            // 
            this.btnSetPaySupCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetPaySupCode.Enabled = false;
            this.btnSetPaySupCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetPaySupCode.Location = new System.Drawing.Point(504, 565);
            this.btnSetPaySupCode.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSetPaySupCode.Name = "btnSetPaySupCode";
            this.btnSetPaySupCode.Size = new System.Drawing.Size(105, 23);
            this.btnSetPaySupCode.TabIndex = 17;
            this.btnSetPaySupCode.Text = "選択行に反映";
            this.btnSetPaySupCode.UseVisualStyleBackColor = true;
            this.btnSetPaySupCode.Click += new System.EventHandler(this.BtnSetPaySupCode_Click);
            // 
            // btnSetDspNo
            // 
            this.btnSetDspNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDspNo.Enabled = false;
            this.btnSetDspNo.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetDspNo.Location = new System.Drawing.Point(504, 594);
            this.btnSetDspNo.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSetDspNo.Name = "btnSetDspNo";
            this.btnSetDspNo.Size = new System.Drawing.Size(105, 23);
            this.btnSetDspNo.TabIndex = 20;
            this.btnSetDspNo.Text = "選択行に反映";
            this.btnSetDspNo.UseVisualStyleBackColor = true;
            this.btnSetDspNo.Click += new System.EventHandler(this.BtnSetDspNo_Click);
            // 
            // btnSetNouki
            // 
            this.btnSetNouki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetNouki.Enabled = false;
            this.btnSetNouki.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetNouki.Location = new System.Drawing.Point(504, 623);
            this.btnSetNouki.MinimumSize = new System.Drawing.Size(0, 20);
            this.btnSetNouki.Name = "btnSetNouki";
            this.btnSetNouki.Size = new System.Drawing.Size(105, 23);
            this.btnSetNouki.TabIndex = 23;
            this.btnSetNouki.Text = "選択行に反映";
            this.btnSetNouki.UseVisualStyleBackColor = true;
            this.btnSetNouki.Click += new System.EventHandler(this.BtnSetNouki_Click);
            // 
            // labelTOTAL_PRICE
            // 
            this.labelTOTAL_PRICE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTOTAL_PRICE.AutoSize = true;
            this.labelTOTAL_PRICE.BackColor = System.Drawing.Color.Transparent;
            this.labelTOTAL_PRICE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTOTAL_PRICE.Location = new System.Drawing.Point(701, 570);
            this.labelTOTAL_PRICE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTOTAL_PRICE.Name = "labelTOTAL_PRICE";
            this.labelTOTAL_PRICE.Size = new System.Drawing.Size(55, 15);
            this.labelTOTAL_PRICE.TabIndex = 24;
            this.labelTOTAL_PRICE.Text = "合計金額";
            // 
            // txtDeliveryNo
            // 
            this.txtDeliveryNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDeliveryNo.Location = new System.Drawing.Point(94, 596);
            this.txtDeliveryNo.MaxLength = 8;
            this.txtDeliveryNo.Name = "txtDeliveryNo";
            this.txtDeliveryNo.Size = new System.Drawing.Size(404, 23);
            this.txtDeliveryNo.TabIndex = 19;
            this.txtDeliveryNo.TextChanged += new System.EventHandler(this.TxtDspNo_TextChanged);
            // 
            // txtPaySupCode
            // 
            this.txtPaySupCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPaySupCode.Location = new System.Drawing.Point(94, 567);
            this.txtPaySupCode.MaxLength = 8;
            this.txtPaySupCode.Name = "txtPaySupCode";
            this.txtPaySupCode.Size = new System.Drawing.Size(100, 23);
            this.txtPaySupCode.TabIndex = 14;
            this.txtPaySupCode.TextChanged += new System.EventHandler(this.TxtPaySupCode_TextChanged);
            // 
            // txtHatOrderNo
            // 
            this.txtHatOrderNo.Location = new System.Drawing.Point(94, 80);
            this.txtHatOrderNo.MaxLength = 10;
            this.txtHatOrderNo.Name = "txtHatOrderNo";
            this.txtHatOrderNo.Size = new System.Drawing.Size(100, 23);
            this.txtHatOrderNo.TabIndex = 6;
            // 
            // txtPuCode
            // 
            this.txtPuCode.Location = new System.Drawing.Point(94, 49);
            this.txtPuCode.MaxLength = 8;
            this.txtPuCode.Name = "txtPuCode";
            this.txtPuCode.Size = new System.Drawing.Size(100, 23);
            this.txtPuCode.TabIndex = 2;
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalPrice.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtTotalPrice.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTotalPrice.Location = new System.Drawing.Point(762, 565);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(158, 23);
            this.txtTotalPrice.TabIndex = 25;
            this.txtTotalPrice.TabStop = false;
            this.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPaySupName
            // 
            this.txtPaySupName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPaySupName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtPaySupName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPaySupName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPaySupName.Location = new System.Drawing.Point(203, 567);
            this.txtPaySupName.Name = "txtPaySupName";
            this.txtPaySupName.ReadOnly = true;
            this.txtPaySupName.Size = new System.Drawing.Size(214, 23);
            this.txtPaySupName.TabIndex = 15;
            this.txtPaySupName.TabStop = false;
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
            this.dtPayDateTo.Location = new System.Drawing.Point(225, 108);
            this.dtPayDateTo.LoopPosition = false;
            this.dtPayDateTo.MaskInfo.AutoTabWhenFilled = true;
            this.dtPayDateTo.MaxLength = 8;
            this.dtPayDateTo.Name = "dtPayDateTo";
            this.dtPayDateTo.Size = new System.Drawing.Size(100, 23);
            this.dtPayDateTo.TabIndex = 10;
            this.dtPayDateTo.Tag = null;
            this.dtPayDateTo.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtNouki
            // 
            this.dtNouki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtNouki.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dtNouki.DateTimeInput = false;
            this.dtNouki.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dtNouki.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNouki.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNouki.EditFormat.CustomFormat = "yy/MM/dd";
            this.dtNouki.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNouki.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dtNouki.EditMask = "90/90/90";
            this.dtNouki.EmptyAsNull = true;
            this.dtNouki.ExitOnLastChar = true;
            this.dtNouki.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dtNouki.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtNouki.GapHeight = 0;
            this.dtNouki.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dtNouki.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtNouki.Location = new System.Drawing.Point(94, 625);
            this.dtNouki.LoopPosition = false;
            this.dtNouki.MaskInfo.AutoTabWhenFilled = true;
            this.dtNouki.MaxLength = 8;
            this.dtNouki.Name = "dtNouki";
            this.dtNouki.Size = new System.Drawing.Size(100, 23);
            this.dtNouki.TabIndex = 22;
            this.dtNouki.Tag = null;
            this.dtNouki.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtNouki.ValueChanged += new System.EventHandler(this.DtNouki_ValueChanged);
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
            this.dtPayDateFrom.Location = new System.Drawing.Point(94, 108);
            this.dtPayDateFrom.LoopPosition = false;
            this.dtPayDateFrom.MaskInfo.AutoTabWhenFilled = true;
            this.dtPayDateFrom.MaxLength = 8;
            this.dtPayDateFrom.Name = "dtPayDateFrom";
            this.dtPayDateFrom.Size = new System.Drawing.Size(100, 23);
            this.dtPayDateFrom.TabIndex = 8;
            this.dtPayDateFrom.Tag = null;
            this.dtPayDateFrom.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtPuName
            // 
            this.txtPuName.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtPuName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPuName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPuName.Location = new System.Drawing.Point(203, 51);
            this.txtPuName.Name = "txtPuName";
            this.txtPuName.ReadOnly = true;
            this.txtPuName.Size = new System.Drawing.Size(214, 23);
            this.txtPuName.TabIndex = 3;
            this.txtPuName.TabStop = false;
            // 
            // PU_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(933, 659);
            this.Controls.Add(this.txtDeliveryNo);
            this.Controls.Add(this.txtPaySupCode);
            this.Controls.Add(this.txtHatOrderNo);
            this.Controls.Add(this.txtPuCode);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.labelTOTAL_PRICE);
            this.Controls.Add(this.btnSetNouki);
            this.Controls.Add(this.btnSetDspNo);
            this.Controls.Add(this.btnSetPaySupCode);
            this.Controls.Add(this.btnSearcyPaySupplier);
            this.Controls.Add(this.txtPaySupName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtPayDateTo);
            this.Controls.Add(this.dtNouki);
            this.Controls.Add(this.dtPayDateFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnSearchSupplier);
            this.Controls.Add(this.txtPuName);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSAVE);
            this.Controls.Add(this.grdPuImports);
            this.Controls.Add(this.lblScreenMode);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(867, 433);
            this.Name = "PU_Edit";
            this.Text = "仕入編集";
            this.Load += new System.EventHandler(this.PU_Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPuImports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNouki)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPayDateFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblScreenMode;
        private C1.Win.C1FlexGrid.C1FlexGrid grdPuImports;
        private System.Windows.Forms.Button btnSAVE;
        private CustomControls.C1DateInputEx dtPayDateTo;
        private CustomControls.C1DateInputEx dtPayDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSearchSupplier;
        private CustomControls.TextBoxReadOnly txtPuName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearcyPaySupplier;
        private CustomControls.TextBoxReadOnly txtPaySupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private CustomControls.C1DateInputEx dtNouki;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSetPaySupCode;
        private System.Windows.Forms.Button btnSetDspNo;
        private System.Windows.Forms.Button btnSetNouki;
        private CustomControls.TextBoxReadOnly txtTotalPrice;
        private System.Windows.Forms.Label labelTOTAL_PRICE;
        private CustomControls.TextBoxChar txtPuCode;
        private CustomControls.TextBoxChar txtHatOrderNo;
        private CustomControls.TextBoxChar txtPaySupCode;
        private CustomControls.TextBoxChar txtDeliveryNo;
    }
}
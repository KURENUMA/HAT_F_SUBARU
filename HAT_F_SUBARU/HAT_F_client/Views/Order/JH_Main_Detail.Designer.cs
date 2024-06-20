using System;

namespace HatFClient.Views.Order
{
    partial class JH_Main_Detail
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSiireBikou = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSiireBikou = new System.Windows.Forms.TextBox();
            this.lblDEL_FLG = new System.Windows.Forms.Label();
            this.cmbSyohinSuggest = new HatFClient.CustomControls.ComboBoxSuggest();
            this.dateNOUKI = new HatFClient.CustomControls.C1DateInputEx();
            this.txtTAX_FLG = new HatFClient.CustomControls.TextBoxChar();
            this.txtSII_KIGOU = new HatFClient.CustomControls.TextBoxChar();
            this.txtURI_KIGOU = new HatFClient.CustomControls.TextBoxChar();
            this.txtKoban = new HatFClient.CustomControls.TextBoxChar();
            this.decSII_KAKE = new HatFClient.CustomControls.TextBoxPer();
            this.decURI_KAKE = new HatFClient.CustomControls.TextBoxPer();
            this.numBARA = new HatFClient.CustomControls.TextBoxNum();
            this.cmbSOKO_CD = new HatFClient.CustomControls.ComboBoxEx();
            this.cmbURIKUBN = new HatFClient.CustomControls.ComboBoxEx();
            this.txtSHIRESAKI_CD = new HatFClient.CustomControls.TextBoxChar();
            this.txtroRowNo = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtroNoTankaWariai = new HatFClient.CustomControls.TextBoxReadOnly();
            this.txtLBIKO = new HatFClient.CustomControls.TextBoxChar();
            this.decSII_TAN = new HatFClient.CustomControls.TextBoxDeci();
            this.decURI_TAN = new HatFClient.CustomControls.TextBoxDeci();
            this.txtroRiritsu = new HatFClient.CustomControls.TextBoxReadOnly();
            this.decSII_ANSW_TAN = new HatFClient.CustomControls.TextBoxDeci();
            this.decTEI_TAN = new HatFClient.CustomControls.TextBoxDeci();
            this.txtTANI = new HatFClient.CustomControls.TextBoxChar();
            this.txtroZaikoSuu = new HatFClient.CustomControls.TextBoxReadOnly();
            this.numSURYO = new HatFClient.CustomControls.TextBoxNum();
            this.txtSYOHIN_CD = new HatFClient.CustomControls.TextBoxChar();
            this.txtSYOBUN_CD = new HatFClient.CustomControls.TextBoxChar();
            ((System.ComponentModel.ISupportInitialize)(this.dateNOUKI)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSiireBikou
            // 
            this.btnSiireBikou.BackgroundImage = global::HatFClient.Properties.Resources.transparency_notes_icon;
            this.btnSiireBikou.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSiireBikou.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSiireBikou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSiireBikou.Location = new System.Drawing.Point(1159, 34);
            this.btnSiireBikou.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSiireBikou.Name = "btnSiireBikou";
            this.btnSiireBikou.Size = new System.Drawing.Size(54, 30);
            this.btnSiireBikou.TabIndex = 124;
            this.btnSiireBikou.TabStop = false;
            this.btnSiireBikou.UseVisualStyleBackColor = true;
            this.btnSiireBikou.Click += new System.EventHandler(this.BtnSiireBikou_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::HatFClient.Properties.Resources.transparency_search_icon;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(318, 6);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(27, 26);
            this.btnSearch.TabIndex = 104;
            this.btnSearch.TabStop = false;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSiireBikou
            // 
            this.txtSiireBikou.Location = new System.Drawing.Point(1159, 37);
            this.txtSiireBikou.Name = "txtSiireBikou";
            this.txtSiireBikou.Size = new System.Drawing.Size(16, 23);
            this.txtSiireBikou.TabIndex = 141;
            this.txtSiireBikou.TabStop = false;
            this.txtSiireBikou.Visible = false;
            this.txtSiireBikou.TextChanged += new System.EventHandler(this.TxtSiireBikou_TextChanged);
            // 
            // lblDEL_FLG
            // 
            this.lblDEL_FLG.AutoSize = true;
            this.lblDEL_FLG.ForeColor = System.Drawing.Color.Red;
            this.lblDEL_FLG.Location = new System.Drawing.Point(5, 40);
            this.lblDEL_FLG.Name = "lblDEL_FLG";
            this.lblDEL_FLG.Size = new System.Drawing.Size(31, 15);
            this.lblDEL_FLG.TabIndex = 144;
            this.lblDEL_FLG.Text = "削除";
            this.lblDEL_FLG.Visible = false;
            // 
            // cmbSyohinSuggest
            // 
            this.cmbSyohinSuggest.CustomEventsEnabled = true;
            this.cmbSyohinSuggest.DropDownWidth = 121;
            this.cmbSyohinSuggest.FormattingEnabled = true;
            this.cmbSyohinSuggest.Location = new System.Drawing.Point(114, 32);
            this.cmbSyohinSuggest.MinimumSearchKeywordLength = 1;
            this.cmbSyohinSuggest.Name = "cmbSyohinSuggest";
            this.cmbSyohinSuggest.Size = new System.Drawing.Size(447, 23);
            this.cmbSyohinSuggest.SuggestItemFetchingEnabled = true;
            this.cmbSyohinSuggest.TabIndex = 142;
            // 
            // dateNOUKI
            // 
            this.dateNOUKI.Calendar.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.dateNOUKI.DateTimeInput = false;
            this.dateNOUKI.DisplayFormat.CustomFormat = "yy/MM/dd";
            this.dateNOUKI.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateNOUKI.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dateNOUKI.EditFormat.CustomFormat = "yy/MM/dd";
            this.dateNOUKI.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateNOUKI.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) 
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd) 
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            this.dateNOUKI.EditMask = "90/90/90";
            this.dateNOUKI.EmptyAsNull = true;
            this.dateNOUKI.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dateNOUKI.GapHeight = 0;
            this.dateNOUKI.ImagePadding = new System.Windows.Forms.Padding(0);
            this.dateNOUKI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dateNOUKI.Location = new System.Drawing.Point(945, 5);
            this.dateNOUKI.LoopPosition = false;
            this.dateNOUKI.MaxLength = 8;
            this.dateNOUKI.Name = "dateNOUKI";
            this.dateNOUKI.Size = new System.Drawing.Size(104, 21);
            this.dateNOUKI.TabIndex = 111;
            this.dateNOUKI.Tag = null;
            this.dateNOUKI.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // txtTAX_FLG
            // 
            this.txtTAX_FLG.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTAX_FLG.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtTAX_FLG.Location = new System.Drawing.Point(1217, 36);
            this.txtTAX_FLG.MaxLength = 1;
            this.txtTAX_FLG.Name = "txtTAX_FLG";
            this.txtTAX_FLG.Size = new System.Drawing.Size(27, 23);
            this.txtTAX_FLG.TabIndex = 125;
            this.txtTAX_FLG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSII_KIGOU
            // 
            this.txtSII_KIGOU.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSII_KIGOU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSII_KIGOU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSII_KIGOU.Location = new System.Drawing.Point(501, 36);
            this.txtSII_KIGOU.MaxLength = 1;
            this.txtSII_KIGOU.Name = "txtSII_KIGOU";
            this.txtSII_KIGOU.Size = new System.Drawing.Size(27, 23);
            this.txtSII_KIGOU.TabIndex = 119;
            this.txtSII_KIGOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSII_KIGOU.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            this.txtSII_KIGOU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKigou_KeyPress);
            // 
            // txtURI_KIGOU
            // 
            this.txtURI_KIGOU.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtURI_KIGOU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtURI_KIGOU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtURI_KIGOU.Location = new System.Drawing.Point(82, 36);
            this.txtURI_KIGOU.MaxLength = 1;
            this.txtURI_KIGOU.Name = "txtURI_KIGOU";
            this.txtURI_KIGOU.Size = new System.Drawing.Size(27, 23);
            this.txtURI_KIGOU.TabIndex = 114;
            this.txtURI_KIGOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtURI_KIGOU.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            this.txtURI_KIGOU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKigou_KeyPress);
            // 
            // txtKoban
            // 
            this.txtKoban.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKoban.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKoban.Location = new System.Drawing.Point(29, 5);
            this.txtKoban.MaxLength = 1;
            this.txtKoban.Name = "txtKoban";
            this.txtKoban.Size = new System.Drawing.Size(27, 23);
            this.txtKoban.TabIndex = 101;
            this.txtKoban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKoban.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            this.txtKoban.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxKoban_KeyPress);
            // 
            // decSII_KAKE
            // 
            this.decSII_KAKE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decSII_KAKE.Location = new System.Drawing.Point(533, 36);
            this.decSII_KAKE.MaxLength = 5;
            this.decSII_KAKE.Name = "decSII_KAKE";
            this.decSII_KAKE.Size = new System.Drawing.Size(106, 23);
            this.decSII_KAKE.TabIndex = 120;
            this.decSII_KAKE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // decURI_KAKE
            // 
            this.decURI_KAKE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decURI_KAKE.Location = new System.Drawing.Point(114, 36);
            this.decURI_KAKE.MaxLength = 5;
            this.decURI_KAKE.Name = "decURI_KAKE";
            this.decURI_KAKE.Size = new System.Drawing.Size(106, 23);
            this.decURI_KAKE.TabIndex = 115;
            this.decURI_KAKE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numBARA
            // 
            this.numBARA.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numBARA.Location = new System.Drawing.Point(641, 5);
            this.numBARA.MaxLength = 7;
            this.numBARA.Name = "numBARA";
            this.numBARA.Size = new System.Drawing.Size(74, 23);
            this.numBARA.TabIndex = 109;
            this.numBARA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbSOKO_CD
            // 
            this.cmbSOKO_CD.DropDownWidth = 140;
            this.cmbSOKO_CD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSOKO_CD.FormattingEnabled = true;
            this.cmbSOKO_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbSOKO_CD.Location = new System.Drawing.Point(354, 36);
            this.cmbSOKO_CD.Name = "cmbSOKO_CD";
            this.cmbSOKO_CD.Size = new System.Drawing.Size(45, 23);
            this.cmbSOKO_CD.TabIndex = 117;
            this.cmbSOKO_CD.Visible = false;
            // 
            // cmbURIKUBN
            // 
            this.cmbURIKUBN.DropDownWidth = 140;
            this.cmbURIKUBN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbURIKUBN.FormattingEnabled = true;
            this.cmbURIKUBN.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbURIKUBN.Location = new System.Drawing.Point(354, 5);
            this.cmbURIKUBN.Name = "cmbURIKUBN";
            this.cmbURIKUBN.Size = new System.Drawing.Size(45, 23);
            this.cmbURIKUBN.TabIndex = 105;
            // 
            // txtSHIRESAKI_CD
            // 
            this.txtSHIRESAKI_CD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSHIRESAKI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSHIRESAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSHIRESAKI_CD.Location = new System.Drawing.Point(425, 36);
            this.txtSHIRESAKI_CD.MaxLength = 6;
            this.txtSHIRESAKI_CD.Name = "txtSHIRESAKI_CD";
            this.txtSHIRESAKI_CD.Size = new System.Drawing.Size(69, 23);
            this.txtSHIRESAKI_CD.TabIndex = 118;
            this.txtSHIRESAKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSHIRESAKI_CD.Visible = false;
            this.txtSHIRESAKI_CD.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            this.txtSHIRESAKI_CD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxCharType_KeyPress);
            // 
            // txtroRowNo
            // 
            this.txtroRowNo.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroRowNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroRowNo.Location = new System.Drawing.Point(3, 5);
            this.txtroRowNo.Name = "txtroRowNo";
            this.txtroRowNo.ReadOnly = true;
            this.txtroRowNo.Size = new System.Drawing.Size(20, 23);
            this.txtroRowNo.TabIndex = 100;
            this.txtroRowNo.TabStop = false;
            this.txtroRowNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtroNoTankaWariai
            // 
            this.txtroNoTankaWariai.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroNoTankaWariai.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroNoTankaWariai.Location = new System.Drawing.Point(774, 36);
            this.txtroNoTankaWariai.Name = "txtroNoTankaWariai";
            this.txtroNoTankaWariai.ReadOnly = true;
            this.txtroNoTankaWariai.Size = new System.Drawing.Size(124, 23);
            this.txtroNoTankaWariai.TabIndex = 122;
            this.txtroNoTankaWariai.TabStop = false;
            this.txtroNoTankaWariai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLBIKO
            // 
            this.txtLBIKO.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtLBIKO.Location = new System.Drawing.Point(945, 36);
            this.txtLBIKO.Name = "txtLBIKO";
            this.txtLBIKO.Size = new System.Drawing.Size(208, 23);
            this.txtLBIKO.TabIndex = 123;
            // 
            // decSII_TAN
            // 
            this.decSII_TAN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decSII_TAN.Location = new System.Drawing.Point(641, 36);
            this.decSII_TAN.MaxLength = 13;
            this.decSII_TAN.Name = "decSII_TAN";
            this.decSII_TAN.Size = new System.Drawing.Size(124, 23);
            this.decSII_TAN.TabIndex = 121;
            this.decSII_TAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.decSII_TAN.Validated += new System.EventHandler(this.CalcPer_Validated);
            // 
            // decURI_TAN
            // 
            this.decURI_TAN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decURI_TAN.Location = new System.Drawing.Point(222, 36);
            this.decURI_TAN.MaxLength = 13;
            this.decURI_TAN.Name = "decURI_TAN";
            this.decURI_TAN.Size = new System.Drawing.Size(124, 23);
            this.decURI_TAN.TabIndex = 116;
            this.decURI_TAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.decURI_TAN.Validated += new System.EventHandler(this.CalcPer_Validated);
            // 
            // txtroRiritsu
            // 
            this.txtroRiritsu.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroRiritsu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroRiritsu.Location = new System.Drawing.Point(1184, 5);
            this.txtroRiritsu.Name = "txtroRiritsu";
            this.txtroRiritsu.ReadOnly = true;
            this.txtroRiritsu.Size = new System.Drawing.Size(60, 23);
            this.txtroRiritsu.TabIndex = 113;
            this.txtroRiritsu.TabStop = false;
            this.txtroRiritsu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // decSII_ANSW_TAN
            // 
            this.decSII_ANSW_TAN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decSII_ANSW_TAN.Location = new System.Drawing.Point(1054, 5);
            this.decSII_ANSW_TAN.MaxLength = 13;
            this.decSII_ANSW_TAN.Name = "decSII_ANSW_TAN";
            this.decSII_ANSW_TAN.Size = new System.Drawing.Size(124, 23);
            this.decSII_ANSW_TAN.TabIndex = 112;
            this.decSII_ANSW_TAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // decTEI_TAN
            // 
            this.decTEI_TAN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.decTEI_TAN.Location = new System.Drawing.Point(774, 5);
            this.decTEI_TAN.MaxLength = 13;
            this.decTEI_TAN.Name = "decTEI_TAN";
            this.decTEI_TAN.Size = new System.Drawing.Size(124, 23);
            this.decTEI_TAN.TabIndex = 110;
            this.decTEI_TAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.decTEI_TAN.Validated += new System.EventHandler(this.CalcPer_Validated);
            // 
            // txtTANI
            // 
            this.txtTANI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtTANI.Location = new System.Drawing.Point(592, 5);
            this.txtTANI.MaxLength = 3;
            this.txtTANI.Name = "txtTANI";
            this.txtTANI.Size = new System.Drawing.Size(40, 23);
            this.txtTANI.TabIndex = 108;
            this.txtTANI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTANI.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            // 
            // txtroZaikoSuu
            // 
            this.txtroZaikoSuu.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtroZaikoSuu.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtroZaikoSuu.Location = new System.Drawing.Point(425, 5);
            this.txtroZaikoSuu.Name = "txtroZaikoSuu";
            this.txtroZaikoSuu.ReadOnly = true;
            this.txtroZaikoSuu.Size = new System.Drawing.Size(69, 23);
            this.txtroZaikoSuu.TabIndex = 106;
            this.txtroZaikoSuu.TabStop = false;
            this.txtroZaikoSuu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numSURYO
            // 
            this.numSURYO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numSURYO.Location = new System.Drawing.Point(501, 5);
            this.numSURYO.MaxLength = 5;
            this.numSURYO.Name = "numSURYO";
            this.numSURYO.Size = new System.Drawing.Size(60, 23);
            this.numSURYO.TabIndex = 107;
            this.numSURYO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSYOHIN_CD
            // 
            this.txtSYOHIN_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSYOHIN_CD.Location = new System.Drawing.Point(114, 5);
            this.txtSYOHIN_CD.MaxLength = 50;
            this.txtSYOHIN_CD.Name = "txtSYOHIN_CD";
            this.txtSYOHIN_CD.Size = new System.Drawing.Size(199, 23);
            this.txtSYOHIN_CD.TabIndex = 103;
            this.txtSYOHIN_CD.TextChanged += new System.EventHandler(this.TxtSYOHIN_CD_TextChanged);
            // 
            // txtSYOBUN_CD
            // 
            this.txtSYOBUN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSYOBUN_CD.Location = new System.Drawing.Point(68, 5);
            this.txtSYOBUN_CD.MaxLength = 3;
            this.txtSYOBUN_CD.Name = "txtSYOBUN_CD";
            this.txtSYOBUN_CD.Size = new System.Drawing.Size(40, 23);
            this.txtSYOBUN_CD.TabIndex = 102;
            this.txtSYOBUN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSYOBUN_CD.TextChanged += new System.EventHandler(this.ForTabOrder_TextChanged);
            this.txtSYOBUN_CD.Validated += new System.EventHandler(this.TxtShobunCd_Validated);
            // 
            // JH_Main_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.lblDEL_FLG);
            this.Controls.Add(this.cmbSyohinSuggest);
            this.Controls.Add(this.dateNOUKI);
            this.Controls.Add(this.txtTAX_FLG);
            this.Controls.Add(this.txtSII_KIGOU);
            this.Controls.Add(this.txtURI_KIGOU);
            this.Controls.Add(this.txtKoban);
            this.Controls.Add(this.decSII_KAKE);
            this.Controls.Add(this.decURI_KAKE);
            this.Controls.Add(this.numBARA);
            this.Controls.Add(this.cmbSOKO_CD);
            this.Controls.Add(this.cmbURIKUBN);
            this.Controls.Add(this.txtSiireBikou);
            this.Controls.Add(this.txtSHIRESAKI_CD);
            this.Controls.Add(this.txtroRowNo);
            this.Controls.Add(this.btnSiireBikou);
            this.Controls.Add(this.txtroNoTankaWariai);
            this.Controls.Add(this.txtLBIKO);
            this.Controls.Add(this.decSII_TAN);
            this.Controls.Add(this.decURI_TAN);
            this.Controls.Add(this.txtroRiritsu);
            this.Controls.Add(this.decSII_ANSW_TAN);
            this.Controls.Add(this.decTEI_TAN);
            this.Controls.Add(this.txtTANI);
            this.Controls.Add(this.txtroZaikoSuu);
            this.Controls.Add(this.numSURYO);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSYOHIN_CD);
            this.Controls.Add(this.txtSYOBUN_CD);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "JH_Main_Detail";
            this.Size = new System.Drawing.Size(1255, 70);
            ((System.ComponentModel.ISupportInitialize)(this.dateNOUKI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        public System.Windows.Forms.Button btnSiireBikou;
        public CustomControls.TextBoxReadOnly txtroNoTankaWariai;
        public CustomControls.TextBoxChar txtLBIKO;
        public CustomControls.TextBoxDeci decSII_TAN;
        public CustomControls.TextBoxDeci decURI_TAN;
        public CustomControls.TextBoxReadOnly txtroRiritsu;
        public CustomControls.TextBoxDeci decSII_ANSW_TAN;
        public CustomControls.TextBoxDeci decTEI_TAN;
        public CustomControls.TextBoxChar txtTANI;
        public CustomControls.TextBoxNum numSURYO;
        public System.Windows.Forms.Button btnSearch;
        public CustomControls.TextBoxChar txtSYOHIN_CD;
        public CustomControls.TextBoxChar txtSYOBUN_CD;
        public CustomControls.TextBoxReadOnly txtroRowNo;
        public CustomControls.TextBoxReadOnly txtroZaikoSuu;
        public CustomControls.TextBoxChar txtSHIRESAKI_CD;
        public System.Windows.Forms.TextBox txtSiireBikou;
        public CustomControls.ComboBoxEx cmbURIKUBN;
        public CustomControls.ComboBoxEx cmbSOKO_CD;
        public CustomControls.TextBoxNum numBARA;
        public CustomControls.TextBoxPer decURI_KAKE;
        public CustomControls.TextBoxPer decSII_KAKE;
        public CustomControls.TextBoxChar txtKoban;
        public CustomControls.TextBoxChar txtURI_KIGOU;
        public CustomControls.TextBoxChar txtSII_KIGOU;
        public CustomControls.TextBoxChar txtTAX_FLG;
        public CustomControls.C1DateInputEx dateNOUKI;
        public CustomControls.ComboBoxSuggest cmbSyohinSuggest;
        public System.Windows.Forms.Label lblDEL_FLG;
    }
}

namespace HatFClient.Views.MasterSearch
{
    partial class MS_Genba
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTOKUI_KANA = new System.Windows.Forms.Label();
            this.lblTOKUI_NAME = new System.Windows.Forms.Label();
            this.lblGENBA_CD = new System.Windows.Forms.Label();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.clmCustCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmKeymanCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmKeymanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGenbaCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGenbaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGENBA_ADDRESS = new HatFClient.CustomControls.TextBoxChar();
            this.txtGENBA_NAME = new HatFClient.CustomControls.TextBoxChar();
            this.txtKEYMAN_CODE = new HatFClient.CustomControls.TextBoxChar();
            this.txtCUST_CODE = new HatFClient.CustomControls.TextBoxChar();
            this.txtGENBA_CD = new HatFClient.CustomControls.TextBoxChar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTOKUI_KANA
            // 
            this.lblTOKUI_KANA.AutoSize = true;
            this.lblTOKUI_KANA.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_KANA.Location = new System.Drawing.Point(14, 73);
            this.lblTOKUI_KANA.Name = "lblTOKUI_KANA";
            this.lblTOKUI_KANA.Size = new System.Drawing.Size(31, 15);
            this.lblTOKUI_KANA.TabIndex = 8;
            this.lblTOKUI_KANA.Text = "住所";
            // 
            // lblTOKUI_NAME
            // 
            this.lblTOKUI_NAME.AutoSize = true;
            this.lblTOKUI_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_NAME.Location = new System.Drawing.Point(14, 44);
            this.lblTOKUI_NAME.Name = "lblTOKUI_NAME";
            this.lblTOKUI_NAME.Size = new System.Drawing.Size(43, 15);
            this.lblTOKUI_NAME.TabIndex = 6;
            this.lblTOKUI_NAME.Text = "現場名";
            // 
            // lblGENBA_CD
            // 
            this.lblGENBA_CD.AutoSize = true;
            this.lblGENBA_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGENBA_CD.Location = new System.Drawing.Point(254, 15);
            this.lblGENBA_CD.Name = "lblGENBA_CD";
            this.lblGENBA_CD.Size = new System.Drawing.Size(48, 15);
            this.lblGENBA_CD.TabIndex = 4;
            this.lblGENBA_CD.Text = "現場CD";
            // 
            // btnFnc09
            // 
            this.btnFnc09.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(982, 12);
            this.btnFnc09.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc09.Name = "btnFnc09";
            this.btnFnc09.Size = new System.Drawing.Size(112, 30);
            this.btnFnc09.TabIndex = 10;
            this.btnFnc09.TabStop = false;
            this.btnFnc09.Text = "F9:検索";
            this.btnFnc09.UseVisualStyleBackColor = true;
            this.btnFnc09.Click += new System.EventHandler(this.BtnFnc09_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeight = 60;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCustCode,
            this.clmKeymanCode,
            this.clmKeymanName,
            this.clmGenbaCode,
            this.clmGenbaName,
            this.clmAddress});
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.Location = new System.Drawing.Point(13, 100);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 30;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1081, 432);
            this.dgvList.TabIndex = 11;
            this.dgvList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GrdList_MouseDoubleClick);
            // 
            // clmCustCode
            // 
            this.clmCustCode.DataPropertyName = "CustomerCode";
            this.clmCustCode.HeaderText = "得意先コード";
            this.clmCustCode.Name = "clmCustCode";
            this.clmCustCode.ReadOnly = true;
            // 
            // clmKeymanCode
            // 
            this.clmKeymanCode.DataPropertyName = "KeymanCode";
            this.clmKeymanCode.HeaderText = "担";
            this.clmKeymanCode.Name = "clmKeymanCode";
            this.clmKeymanCode.ReadOnly = true;
            this.clmKeymanCode.Width = 40;
            // 
            // clmKeymanName
            // 
            this.clmKeymanName.DataPropertyName = "KeymanName";
            this.clmKeymanName.HeaderText = "顧客担当者名";
            this.clmKeymanName.Name = "clmKeymanName";
            this.clmKeymanName.ReadOnly = true;
            this.clmKeymanName.Width = 130;
            // 
            // clmGenbaCode
            // 
            this.clmGenbaCode.DataPropertyName = "GenbaCode";
            this.clmGenbaCode.HeaderText = "現場CD";
            this.clmGenbaCode.Name = "clmGenbaCode";
            this.clmGenbaCode.ReadOnly = true;
            // 
            // clmGenbaName
            // 
            this.clmGenbaName.DataPropertyName = "GenbaName";
            this.clmGenbaName.HeaderText = "現場名";
            this.clmGenbaName.Name = "clmGenbaName";
            this.clmGenbaName.ReadOnly = true;
            this.clmGenbaName.Width = 350;
            // 
            // clmAddress
            // 
            this.clmAddress.DataPropertyName = "Address";
            this.clmAddress.HeaderText = "住所";
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            this.clmAddress.Width = 350;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(158, 554);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(129, 15);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "現場情報が存在しません";
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxCount.Location = new System.Drawing.Point(14, 554);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(88, 15);
            this.lblMaxCount.TabIndex = 12;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // btnFnc12
            // 
            this.btnFnc12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc12.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(982, 546);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(112, 30);
            this.btnFnc12.TabIndex = 15;
            this.btnFnc12.TabStop = false;
            this.btnFnc12.Text = "F12:閉じる";
            this.btnFnc12.UseVisualStyleBackColor = true;
            // 
            // btnFnc11
            // 
            this.btnFnc11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc11.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFnc11.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc11.Location = new System.Drawing.Point(867, 546);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(112, 30);
            this.btnFnc11.TabIndex = 14;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "得意先";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(168, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "担";
            // 
            // txtGENBA_ADDRESS
            // 
            this.txtGENBA_ADDRESS.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGENBA_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtGENBA_ADDRESS.Location = new System.Drawing.Point(63, 70);
            this.txtGENBA_ADDRESS.MaxLength = 24;
            this.txtGENBA_ADDRESS.Name = "txtGENBA_ADDRESS";
            this.txtGENBA_ADDRESS.Size = new System.Drawing.Size(516, 23);
            this.txtGENBA_ADDRESS.TabIndex = 9;
            this.txtGENBA_ADDRESS.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtGENBA_NAME
            // 
            this.txtGENBA_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtGENBA_NAME.Location = new System.Drawing.Point(63, 41);
            this.txtGENBA_NAME.MaxLength = 24;
            this.txtGENBA_NAME.Name = "txtGENBA_NAME";
            this.txtGENBA_NAME.Size = new System.Drawing.Size(516, 23);
            this.txtGENBA_NAME.TabIndex = 7;
            this.txtGENBA_NAME.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtKEYMAN_CODE
            // 
            this.txtKEYMAN_CODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKEYMAN_CODE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKEYMAN_CODE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtKEYMAN_CODE.Location = new System.Drawing.Point(193, 12);
            this.txtKEYMAN_CODE.MaxLength = 2;
            this.txtKEYMAN_CODE.Name = "txtKEYMAN_CODE";
            this.txtKEYMAN_CODE.Size = new System.Drawing.Size(30, 23);
            this.txtKEYMAN_CODE.TabIndex = 3;
            this.txtKEYMAN_CODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKEYMAN_CODE.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtCUST_CODE
            // 
            this.txtCUST_CODE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCUST_CODE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCUST_CODE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCUST_CODE.Location = new System.Drawing.Point(63, 12);
            this.txtCUST_CODE.MaxLength = 6;
            this.txtCUST_CODE.Name = "txtCUST_CODE";
            this.txtCUST_CODE.Size = new System.Drawing.Size(79, 23);
            this.txtCUST_CODE.TabIndex = 1;
            this.txtCUST_CODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCUST_CODE.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtGENBA_CD
            // 
            this.txtGENBA_CD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGENBA_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtGENBA_CD.Location = new System.Drawing.Point(308, 12);
            this.txtGENBA_CD.MaxLength = 3;
            this.txtGENBA_CD.Name = "txtGENBA_CD";
            this.txtGENBA_CD.Size = new System.Drawing.Size(43, 23);
            this.txtGENBA_CD.TabIndex = 5;
            this.txtGENBA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGENBA_CD.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // MS_Genba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1107, 592);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.txtGENBA_ADDRESS);
            this.Controls.Add(this.lblTOKUI_KANA);
            this.Controls.Add(this.txtGENBA_NAME);
            this.Controls.Add(this.lblTOKUI_NAME);
            this.Controls.Add(this.txtKEYMAN_CODE);
            this.Controls.Add(this.txtCUST_CODE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGENBA_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGENBA_CD);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(733, 325);
            this.Name = "MS_Genba";
            this.Text = "現場検索画面";
            this.Load += new System.EventHandler(this.MS_Genba_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MS_Genba_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.TextBoxChar txtGENBA_ADDRESS;
        private System.Windows.Forms.Label lblTOKUI_KANA;
        private CustomControls.TextBoxChar txtGENBA_NAME;
        private System.Windows.Forms.Label lblTOKUI_NAME;
        private CustomControls.TextBoxChar txtGENBA_CD;
        private System.Windows.Forms.Label lblGENBA_CD;
        private System.Windows.Forms.Button btnFnc09;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.Button btnFnc12;
        private System.Windows.Forms.Button btnFnc11;
        private System.Windows.Forms.Label label1;
        private CustomControls.TextBoxChar txtCUST_CODE;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxChar txtKEYMAN_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmKeymanCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmKeymanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGenbaCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGenbaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
    }
}
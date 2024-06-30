namespace HatFClient.Views.MasterSearch
{
    partial class MS_Tokui2
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.lblTOKUI_CD = new System.Windows.Forms.Label();
            this.lblTOKUI_NAME = new System.Windows.Forms.Label();
            this.lblTOKUI_KANA = new System.Windows.Forms.Label();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtCustKana = new HatFClient.CustomControls.TextBoxChar();
            this.txtCustName = new HatFClient.CustomControls.TextBoxChar();
            this.txtCustCode = new HatFClient.CustomControls.TextBoxChar();
            this.clmCustCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustKana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeight = 60;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCustCode,
            this.clmCustName,
            this.clmCustKana,
            this.clmFax,
            this.clmTel});
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.Location = new System.Drawing.Point(10, 50);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 30;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(809, 377);
            this.dgvList.TabIndex = 3;
            this.dgvList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GrdList_MouseDoubleClick);
            // 
            // btnFnc12
            // 
            this.btnFnc12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc12.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(735, 440);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(84, 24);
            this.btnFnc12.TabIndex = 5;
            this.btnFnc12.TabStop = false;
            this.btnFnc12.Text = "F12:閉じる";
            this.btnFnc12.UseVisualStyleBackColor = true;
            this.btnFnc12.Click += new System.EventHandler(this.BtnFnc12_Click);
            // 
            // btnFnc11
            // 
            this.btnFnc11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc11.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFnc11.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc11.Location = new System.Drawing.Point(649, 440);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(84, 24);
            this.btnFnc11.TabIndex = 4;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // lblTOKUI_CD
            // 
            this.lblTOKUI_CD.AutoSize = true;
            this.lblTOKUI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_CD.Location = new System.Drawing.Point(9, 14);
            this.lblTOKUI_CD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTOKUI_CD.Name = "lblTOKUI_CD";
            this.lblTOKUI_CD.Size = new System.Drawing.Size(60, 15);
            this.lblTOKUI_CD.TabIndex = 77;
            this.lblTOKUI_CD.Text = "得意先CD";
            // 
            // lblTOKUI_NAME
            // 
            this.lblTOKUI_NAME.AutoSize = true;
            this.lblTOKUI_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_NAME.Location = new System.Drawing.Point(128, 14);
            this.lblTOKUI_NAME.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTOKUI_NAME.Name = "lblTOKUI_NAME";
            this.lblTOKUI_NAME.Size = new System.Drawing.Size(55, 15);
            this.lblTOKUI_NAME.TabIndex = 126;
            this.lblTOKUI_NAME.Text = "得意先名";
            // 
            // lblTOKUI_KANA
            // 
            this.lblTOKUI_KANA.AutoSize = true;
            this.lblTOKUI_KANA.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_KANA.Location = new System.Drawing.Point(379, 14);
            this.lblTOKUI_KANA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTOKUI_KANA.Name = "lblTOKUI_KANA";
            this.lblTOKUI_KANA.Size = new System.Drawing.Size(55, 15);
            this.lblTOKUI_KANA.TabIndex = 128;
            this.lblTOKUI_KANA.Text = "得意先ｶﾅ";
            // 
            // btnFnc09
            // 
            this.btnFnc09.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(735, 10);
            this.btnFnc09.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFnc09.Name = "btnFnc09";
            this.btnFnc09.Size = new System.Drawing.Size(84, 24);
            this.btnFnc09.TabIndex = 131;
            this.btnFnc09.TabStop = false;
            this.btnFnc09.Text = "F9:検索";
            this.btnFnc09.UseVisualStyleBackColor = true;
            this.btnFnc09.Click += new System.EventHandler(this.BtnFnc09_Click);
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxCount.Location = new System.Drawing.Point(20, 444);
            this.lblMaxCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(88, 15);
            this.lblMaxCount.TabIndex = 135;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(128, 444);
            this.lblNote.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(141, 15);
            this.lblNote.TabIndex = 137;
            this.lblNote.Text = "得意先情報が存在しません";
            // 
            // txtCustKana
            // 
            this.txtCustKana.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCustKana.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtCustKana.Location = new System.Drawing.Point(436, 12);
            this.txtCustKana.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustKana.MaxLength = 24;
            this.txtCustKana.Name = "txtCustKana";
            this.txtCustKana.Size = new System.Drawing.Size(187, 23);
            this.txtCustKana.TabIndex = 2;
            this.txtCustKana.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCustName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCustName.Location = new System.Drawing.Point(184, 12);
            this.txtCustName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustName.MaxLength = 24;
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(187, 23);
            this.txtCustName.TabIndex = 1;
            this.txtCustName.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // txtCustCode
            // 
            this.txtCustCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCustCode.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCustCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtCustCode.Location = new System.Drawing.Point(70, 12);
            this.txtCustCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustCode.MaxLength = 6;
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(52, 23);
            this.txtCustCode.TabIndex = 0;
            this.txtCustCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCustCode.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // clmCustCode
            // 
            this.clmCustCode.DataPropertyName = "CustCode";
            this.clmCustCode.HeaderText = "得意先コード";
            this.clmCustCode.Name = "clmCustCode";
            this.clmCustCode.ReadOnly = true;
            this.clmCustCode.Width = 150;
            // 
            // clmCustName
            // 
            this.clmCustName.DataPropertyName = "CustName";
            this.clmCustName.HeaderText = "得意先名＿漢字";
            this.clmCustName.Name = "clmCustName";
            this.clmCustName.ReadOnly = true;
            this.clmCustName.Width = 300;
            // 
            // clmCustKana
            // 
            this.clmCustKana.DataPropertyName = "Custkana";
            this.clmCustKana.HeaderText = "得意先名_カナ";
            this.clmCustKana.Name = "clmCustKana";
            this.clmCustKana.ReadOnly = true;
            this.clmCustKana.Width = 300;
            // 
            // clmFax
            // 
            this.clmFax.DataPropertyName = "CustTel";
            this.clmFax.HeaderText = "ＦＡＸ番号";
            this.clmFax.Name = "clmFax";
            this.clmFax.ReadOnly = true;
            this.clmFax.Width = 150;
            // 
            // clmTel
            // 
            this.clmTel.DataPropertyName = "CustFax";
            this.clmTel.HeaderText = "電話番号";
            this.clmTel.Name = "clmTel";
            this.clmTel.ReadOnly = true;
            this.clmTel.Width = 150;
            // 
            // MS_Tokui2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(829, 474);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.txtCustKana);
            this.Controls.Add(this.lblTOKUI_KANA);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.lblTOKUI_NAME);
            this.Controls.Add(this.txtCustCode);
            this.Controls.Add(this.lblTOKUI_CD);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MS_Tokui2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "得意先検索画面";
            this.Load += new System.EventHandler(this.Fm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnFnc12;
        private System.Windows.Forms.Button btnFnc11;
        private CustomControls.TextBoxChar txtCustCode;
        private System.Windows.Forms.Label lblTOKUI_CD;
        private CustomControls.TextBoxChar txtCustName;
        private System.Windows.Forms.Label lblTOKUI_NAME;
        private CustomControls.TextBoxChar txtCustKana;
        private System.Windows.Forms.Label lblTOKUI_KANA;
        private System.Windows.Forms.Button btnFnc09;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustKana;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTel;
    }
}
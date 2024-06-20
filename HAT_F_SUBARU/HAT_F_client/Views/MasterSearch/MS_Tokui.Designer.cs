namespace HatFClient.Views.MasterSearch
{
    partial class MS_Tokui
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.txtTOKUI_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblTOKUI_CD = new System.Windows.Forms.Label();
            this.txtTOKUI_NAME = new HatFClient.CustomControls.TextBoxChar();
            this.lblTOKUI_NAME = new System.Windows.Forms.Label();
            this.txtTOKUI_KANA = new HatFClient.CustomControls.TextBoxChar();
            this.lblTOKUI_KANA = new System.Windows.Forms.Label();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
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
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(13, 62);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 21;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1079, 471);
            this.dgvList.TabIndex = 3;
            this.dgvList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GrdList_MouseDoubleClick);
            // 
            // btnFnc12
            // 
            this.btnFnc12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc12.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(980, 550);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(112, 30);
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
            this.btnFnc11.Location = new System.Drawing.Point(865, 550);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(112, 30);
            this.btnFnc11.TabIndex = 4;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // txtTOKUI_CD
            // 
            this.txtTOKUI_CD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTOKUI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOKUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtTOKUI_CD.Location = new System.Drawing.Point(94, 15);
            this.txtTOKUI_CD.MaxLength = 6;
            this.txtTOKUI_CD.Name = "txtTOKUI_CD";
            this.txtTOKUI_CD.Size = new System.Drawing.Size(68, 27);
            this.txtTOKUI_CD.TabIndex = 0;
            this.txtTOKUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTOKUI_CD.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            this.txtTOKUI_CD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxCharType_KeyPress);
            // 
            // lblTOKUI_CD
            // 
            this.lblTOKUI_CD.AutoSize = true;
            this.lblTOKUI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_CD.Location = new System.Drawing.Point(12, 18);
            this.lblTOKUI_CD.Name = "lblTOKUI_CD";
            this.lblTOKUI_CD.Size = new System.Drawing.Size(75, 19);
            this.lblTOKUI_CD.TabIndex = 77;
            this.lblTOKUI_CD.Text = "得意先CD";
            // 
            // txtTOKUI_NAME
            // 
            this.txtTOKUI_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOKUI_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTOKUI_NAME.Location = new System.Drawing.Point(245, 15);
            this.txtTOKUI_NAME.MaxLength = 24;
            this.txtTOKUI_NAME.Name = "txtTOKUI_NAME";
            this.txtTOKUI_NAME.Size = new System.Drawing.Size(248, 27);
            this.txtTOKUI_NAME.TabIndex = 1;
            this.txtTOKUI_NAME.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // lblTOKUI_NAME
            // 
            this.lblTOKUI_NAME.AutoSize = true;
            this.lblTOKUI_NAME.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_NAME.Location = new System.Drawing.Point(170, 18);
            this.lblTOKUI_NAME.Name = "lblTOKUI_NAME";
            this.lblTOKUI_NAME.Size = new System.Drawing.Size(69, 19);
            this.lblTOKUI_NAME.TabIndex = 126;
            this.lblTOKUI_NAME.Text = "得意先名";
            // 
            // txtTOKUI_KANA
            // 
            this.txtTOKUI_KANA.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTOKUI_KANA.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtTOKUI_KANA.Location = new System.Drawing.Point(582, 15);
            this.txtTOKUI_KANA.MaxLength = 24;
            this.txtTOKUI_KANA.Name = "txtTOKUI_KANA";
            this.txtTOKUI_KANA.Size = new System.Drawing.Size(248, 27);
            this.txtTOKUI_KANA.TabIndex = 2;
            this.txtTOKUI_KANA.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // lblTOKUI_KANA
            // 
            this.lblTOKUI_KANA.AutoSize = true;
            this.lblTOKUI_KANA.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTOKUI_KANA.Location = new System.Drawing.Point(505, 18);
            this.lblTOKUI_KANA.Name = "lblTOKUI_KANA";
            this.lblTOKUI_KANA.Size = new System.Drawing.Size(70, 19);
            this.lblTOKUI_KANA.TabIndex = 128;
            this.lblTOKUI_KANA.Text = "得意先ｶﾅ";
            // 
            // btnFnc09
            // 
            this.btnFnc09.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(980, 13);
            this.btnFnc09.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc09.Name = "btnFnc09";
            this.btnFnc09.Size = new System.Drawing.Size(112, 30);
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
            this.lblMaxCount.Location = new System.Drawing.Point(26, 555);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(111, 19);
            this.lblMaxCount.TabIndex = 135;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(170, 555);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(177, 19);
            this.lblNote.TabIndex = 137;
            this.lblNote.Text = "得意先情報が存在しません";
            // 
            // MS_Tokui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1105, 592);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.txtTOKUI_KANA);
            this.Controls.Add(this.lblTOKUI_KANA);
            this.Controls.Add(this.txtTOKUI_NAME);
            this.Controls.Add(this.lblTOKUI_NAME);
            this.Controls.Add(this.txtTOKUI_CD);
            this.Controls.Add(this.lblTOKUI_CD);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.KeyPreview = true;
            this.Name = "MS_Tokui";
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
        private CustomControls.TextBoxChar txtTOKUI_CD;
        private System.Windows.Forms.Label lblTOKUI_CD;
        private CustomControls.TextBoxChar txtTOKUI_NAME;
        private System.Windows.Forms.Label lblTOKUI_NAME;
        private CustomControls.TextBoxChar txtTOKUI_KANA;
        private System.Windows.Forms.Label lblTOKUI_KANA;
        private System.Windows.Forms.Button btnFnc09;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.Label lblNote;
    }
}
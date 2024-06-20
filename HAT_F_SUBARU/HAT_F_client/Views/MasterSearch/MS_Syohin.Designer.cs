namespace HatFClient.Views.MasterSearch
{
    partial class MS_Syohin
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
            this.txtSHIRESAKI_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblSHIRESAKI_CD = new System.Windows.Forms.Label();
            this.txtSYOHIN_CD = new HatFClient.CustomControls.TextBoxChar();
            this.lblSYOHIN = new System.Windows.Forms.Label();
            this.btnFnc04 = new System.Windows.Forms.Button();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.lblSideNote = new System.Windows.Forms.Label();
            this.btnFnc05 = new System.Windows.Forms.Button();
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
            this.dgvList.Location = new System.Drawing.Point(13, 112);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 21;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1079, 421);
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
            // txtSHIRESAKI_CD
            // 
            this.txtSHIRESAKI_CD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSHIRESAKI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSHIRESAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSHIRESAKI_CD.Location = new System.Drawing.Point(143, 15);
            this.txtSHIRESAKI_CD.MaxLength = 6;
            this.txtSHIRESAKI_CD.Name = "txtSHIRESAKI_CD";
            this.txtSHIRESAKI_CD.Size = new System.Drawing.Size(68, 27);
            this.txtSHIRESAKI_CD.TabIndex = 0;
            this.txtSHIRESAKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSHIRESAKI_CD.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            this.txtSHIRESAKI_CD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxCharType_KeyPress);
            // 
            // lblSHIRESAKI_CD
            // 
            this.lblSHIRESAKI_CD.AutoSize = true;
            this.lblSHIRESAKI_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSHIRESAKI_CD.Location = new System.Drawing.Point(12, 18);
            this.lblSHIRESAKI_CD.Name = "lblSHIRESAKI_CD";
            this.lblSHIRESAKI_CD.Size = new System.Drawing.Size(75, 19);
            this.lblSHIRESAKI_CD.TabIndex = 77;
            this.lblSHIRESAKI_CD.Text = "仕入先CD";
            // 
            // txtSYOHIN_CD
            // 
            this.txtSYOHIN_CD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSYOHIN_CD.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSYOHIN_CD.Location = new System.Drawing.Point(143, 48);
            this.txtSYOHIN_CD.MaxLength = 24;
            this.txtSYOHIN_CD.Name = "txtSYOHIN_CD";
            this.txtSYOHIN_CD.Size = new System.Drawing.Size(491, 27);
            this.txtSYOHIN_CD.TabIndex = 1;
            this.txtSYOHIN_CD.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // lblSYOHIN
            // 
            this.lblSYOHIN.AutoSize = true;
            this.lblSYOHIN.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSYOHIN.Location = new System.Drawing.Point(12, 51);
            this.lblSYOHIN.Name = "lblSYOHIN";
            this.lblSYOHIN.Size = new System.Drawing.Size(129, 19);
            this.lblSYOHIN.TabIndex = 126;
            this.lblSYOHIN.Text = "品番／名（規格）";
            // 
            // btnFnc04
            // 
            this.btnFnc04.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc04.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc04.Location = new System.Drawing.Point(921, 13);
            this.btnFnc04.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc04.Name = "btnFnc04";
            this.btnFnc04.Size = new System.Drawing.Size(170, 30);
            this.btnFnc04.TabIndex = 131;
            this.btnFnc04.TabStop = false;
            this.btnFnc04.Text = "F4:HAT商品M検索";
            this.btnFnc04.UseVisualStyleBackColor = true;
            this.btnFnc04.Click += new System.EventHandler(this.BtnFnc04_Click);
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
            // lblSideNote
            // 
            this.lblSideNote.AutoSize = true;
            this.lblSideNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSideNote.Location = new System.Drawing.Point(14, 82);
            this.lblSideNote.Name = "lblSideNote";
            this.lblSideNote.Size = new System.Drawing.Size(121, 19);
            this.lblSideNote.TabIndex = 136;
            this.lblSideNote.Text = "スペース区切りで…";
            // 
            // btnFnc05
            // 
            this.btnFnc05.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc05.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc05.Location = new System.Drawing.Point(921, 51);
            this.btnFnc05.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc05.Name = "btnFnc05";
            this.btnFnc05.Size = new System.Drawing.Size(170, 30);
            this.btnFnc05.TabIndex = 137;
            this.btnFnc05.TabStop = false;
            this.btnFnc05.Text = "F5:メーカー商品M検索";
            this.btnFnc05.UseVisualStyleBackColor = true;
            this.btnFnc05.Click += new System.EventHandler(this.BtnFnc05_Click);
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(167, 555);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(162, 19);
            this.lblNote.TabIndex = 150;
            this.lblNote.Text = "商品情報が存在しません";
            // 
            // MS_Syohin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1105, 592);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.btnFnc05);
            this.Controls.Add(this.lblSideNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.btnFnc04);
            this.Controls.Add(this.txtSYOHIN_CD);
            this.Controls.Add(this.lblSYOHIN);
            this.Controls.Add(this.txtSHIRESAKI_CD);
            this.Controls.Add(this.lblSHIRESAKI_CD);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.KeyPreview = true;
            this.Name = "MS_Syohin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "商品検索画面";
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
        private CustomControls.TextBoxChar txtSHIRESAKI_CD;
        private System.Windows.Forms.Label lblSHIRESAKI_CD;
        private CustomControls.TextBoxChar txtSYOHIN_CD;
        private System.Windows.Forms.Label lblSYOHIN;
        private System.Windows.Forms.Button btnFnc04;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.Label lblSideNote;
        private System.Windows.Forms.Button btnFnc05;
        private System.Windows.Forms.Label lblNote;
    }
}
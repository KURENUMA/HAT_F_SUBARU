namespace HatFClient.Views.MasterSearch
{
    partial class MS_Yuban
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.lblPOSTCODE = new System.Windows.Forms.Label();
            this.txtPOSTCODE = new HatFClient.CustomControls.TextBoxChar();
            this.txtAddress = new HatFClient.CustomControls.TextBoxChar();
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
            this.dgvList.Size = new System.Drawing.Size(773, 471);
            this.dgvList.TabIndex = 3;
            this.dgvList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GrdList_MouseDoubleClick);
            // 
            // btnFnc12
            // 
            this.btnFnc12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc12.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(668, 550);
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
            this.btnFnc11.Location = new System.Drawing.Point(553, 550);
            this.btnFnc11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc11.Name = "btnFnc11";
            this.btnFnc11.Size = new System.Drawing.Size(112, 30);
            this.btnFnc11.TabIndex = 4;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAddress.Location = new System.Drawing.Point(272, 18);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(129, 19);
            this.lblAddress.TabIndex = 126;
            this.lblAddress.Text = "住所（中間一致）";
            // 
            // btnFnc09
            // 
            this.btnFnc09.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(668, 13);
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
            this.lblMaxCount.Location = new System.Drawing.Point(12, 555);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(111, 19);
            this.lblMaxCount.TabIndex = 135;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // lblPOSTCODE
            // 
            this.lblPOSTCODE.AutoSize = true;
            this.lblPOSTCODE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPOSTCODE.Location = new System.Drawing.Point(12, 18);
            this.lblPOSTCODE.Name = "lblPOSTCODE";
            this.lblPOSTCODE.Size = new System.Drawing.Size(159, 19);
            this.lblPOSTCODE.TabIndex = 137;
            this.lblPOSTCODE.Text = "郵便番号（完全一致）";
            // 
            // txtPOSTCODE
            // 
            this.txtPOSTCODE.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtPOSTCODE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPOSTCODE.Location = new System.Drawing.Point(173, 15);
            this.txtPOSTCODE.MaxLength = 8;
            this.txtPOSTCODE.Name = "txtPOSTCODE";
            this.txtPOSTCODE.Size = new System.Drawing.Size(84, 27);
            this.txtPOSTCODE.TabIndex = 0;
            this.txtPOSTCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPOSTCODE.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            this.txtPOSTCODE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHyphen_KeyPress);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAddress.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAddress.Location = new System.Drawing.Point(405, 15);
            this.txtAddress.MaxLength = 24;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(248, 27);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.TextChanged += new System.EventHandler(this.Condition_TextChanged);
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(169, 555);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(192, 19);
            this.lblNote.TabIndex = 140;
            this.lblNote.Text = "郵便番号情報が存在しません";
            // 
            // MS_Yuban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(799, 592);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtPOSTCODE);
            this.Controls.Add(this.lblPOSTCODE);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.KeyPreview = true;
            this.Name = "MS_Yuban";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "郵便番号検索画面";
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
        private CustomControls.TextBoxChar txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnFnc09;
        private System.Windows.Forms.Label lblMaxCount;
        private CustomControls.TextBoxChar txtPOSTCODE;
        private System.Windows.Forms.Label lblPOSTCODE;
        private System.Windows.Forms.Label lblNote;
    }
}
namespace HatFClient.Views.Search
{
    partial class U_Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(U_Search));
            this.txtEAMCD = new HatFClient.CustomControls.TextBoxChar();
            this.lblEAMCD = new System.Windows.Forms.Label();
            this.txtSNAIBU = new HatFClient.CustomControls.TextBoxChar();
            this.lblSNAIBU = new System.Windows.Forms.Label();
            this.txtTORIKOMI = new HatFClient.CustomControls.TextBoxChar();
            this.lblTORIKOMI = new System.Windows.Forms.Label();
            this.txtKYAKUCHU = new HatFClient.CustomControls.TextBoxChar();
            this.lblKYAKUCHU = new System.Windows.Forms.Label();
            this.txtDNO = new HatFClient.CustomControls.TextBoxChar();
            this.lblDNO = new System.Windows.Forms.Label();
            this.txtREC_TERM = new HatFClient.CustomControls.TextBoxChar();
            this.lblREC_TERM = new System.Windows.Forms.Label();
            this.btnFnc09 = new System.Windows.Forms.Button();
            this.btnFnc10 = new System.Windows.Forms.Button();
            this.btnFnc12 = new System.Windows.Forms.Button();
            this.btnFnc11 = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.txtDKUBUN = new HatFClient.CustomControls.TextBoxChar();
            this.lblDKUBUN = new System.Windows.Forms.Label();
            this.lblsnKYAKUCHU = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEAMCD
            // 
            this.txtEAMCD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtEAMCD.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEAMCD.Location = new System.Drawing.Point(93, 9);
            this.txtEAMCD.MaxLength = 3;
            this.txtEAMCD.Name = "txtEAMCD";
            this.txtEAMCD.Size = new System.Drawing.Size(40, 27);
            this.txtEAMCD.TabIndex = 0;
            this.txtEAMCD.Text = "999";
            this.txtEAMCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEAMCD
            // 
            this.lblEAMCD.AutoSize = true;
            this.lblEAMCD.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblEAMCD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEAMCD.Location = new System.Drawing.Point(3, 12);
            this.lblEAMCD.Name = "lblEAMCD";
            this.lblEAMCD.Size = new System.Drawing.Size(45, 19);
            this.lblEAMCD.TabIndex = 70;
            this.lblEAMCD.Text = "チーム";
            // 
            // txtSNAIBU
            // 
            this.txtSNAIBU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSNAIBU.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSNAIBU.Location = new System.Drawing.Point(244, 9);
            this.txtSNAIBU.MaxLength = 6;
            this.txtSNAIBU.Name = "txtSNAIBU";
            this.txtSNAIBU.Size = new System.Drawing.Size(65, 27);
            this.txtSNAIBU.TabIndex = 1;
            this.txtSNAIBU.Text = "999999";
            this.txtSNAIBU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSNAIBU
            // 
            this.lblSNAIBU.AutoSize = true;
            this.lblSNAIBU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSNAIBU.Location = new System.Drawing.Point(169, 12);
            this.lblSNAIBU.Name = "lblSNAIBU";
            this.lblSNAIBU.Size = new System.Drawing.Size(69, 19);
            this.lblSNAIBU.TabIndex = 72;
            this.lblSNAIBU.Text = "内部番号";
            // 
            // txtTORIKOMI
            // 
            this.txtTORIKOMI.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTORIKOMI.Location = new System.Drawing.Point(533, 42);
            this.txtTORIKOMI.MaxLength = 20;
            this.txtTORIKOMI.Name = "txtTORIKOMI";
            this.txtTORIKOMI.Size = new System.Drawing.Size(212, 27);
            this.txtTORIKOMI.TabIndex = 6;
            this.txtTORIKOMI.Text = "12345678901234567890";
            // 
            // lblTORIKOMI
            // 
            this.lblTORIKOMI.AutoSize = true;
            this.lblTORIKOMI.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTORIKOMI.Location = new System.Drawing.Point(488, 45);
            this.lblTORIKOMI.Name = "lblTORIKOMI";
            this.lblTORIKOMI.Size = new System.Drawing.Size(39, 19);
            this.lblTORIKOMI.TabIndex = 88;
            this.lblTORIKOMI.Text = "取込";
            // 
            // txtKYAKUCHU
            // 
            this.txtKYAKUCHU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKYAKUCHU.Location = new System.Drawing.Point(696, 9);
            this.txtKYAKUCHU.MaxLength = 10;
            this.txtKYAKUCHU.Name = "txtKYAKUCHU";
            this.txtKYAKUCHU.Size = new System.Drawing.Size(82, 27);
            this.txtKYAKUCHU.TabIndex = 4;
            this.txtKYAKUCHU.Text = "1234567890";
            // 
            // lblKYAKUCHU
            // 
            this.lblKYAKUCHU.AutoSize = true;
            this.lblKYAKUCHU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKYAKUCHU.Location = new System.Drawing.Point(619, 13);
            this.lblKYAKUCHU.Name = "lblKYAKUCHU";
            this.lblKYAKUCHU.Size = new System.Drawing.Size(69, 19);
            this.lblKYAKUCHU.TabIndex = 96;
            this.lblKYAKUCHU.Text = "HAT注番";
            // 
            // txtDNO
            // 
            this.txtDNO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDNO.Location = new System.Drawing.Point(533, 9);
            this.txtDNO.MaxLength = 6;
            this.txtDNO.Name = "txtDNO";
            this.txtDNO.Size = new System.Drawing.Size(65, 27);
            this.txtDNO.TabIndex = 3;
            this.txtDNO.Text = "123456";
            // 
            // lblDNO
            // 
            this.lblDNO.AutoSize = true;
            this.lblDNO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDNO.Location = new System.Drawing.Point(458, 13);
            this.lblDNO.Name = "lblDNO";
            this.lblDNO.Size = new System.Drawing.Size(69, 19);
            this.lblDNO.TabIndex = 98;
            this.lblDNO.Text = "伝票番号";
            // 
            // txtREC_TERM
            // 
            this.txtREC_TERM.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtREC_TERM.Location = new System.Drawing.Point(93, 43);
            this.txtREC_TERM.MaxLength = 12;
            this.txtREC_TERM.Name = "txtREC_TERM";
            this.txtREC_TERM.Size = new System.Drawing.Size(131, 27);
            this.txtREC_TERM.TabIndex = 5;
            this.txtREC_TERM.Text = "123456789012";
            // 
            // lblREC_TERM
            // 
            this.lblREC_TERM.AutoSize = true;
            this.lblREC_TERM.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblREC_TERM.Location = new System.Drawing.Point(3, 46);
            this.lblREC_TERM.Name = "lblREC_TERM";
            this.lblREC_TERM.Size = new System.Drawing.Size(84, 19);
            this.lblREC_TERM.TabIndex = 100;
            this.lblREC_TERM.Text = "メーカー区分";
            // 
            // btnFnc09
            // 
            this.btnFnc09.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc09.Location = new System.Drawing.Point(1001, 46);
            this.btnFnc09.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc09.Name = "btnFnc09";
            this.btnFnc09.Size = new System.Drawing.Size(120, 30);
            this.btnFnc09.TabIndex = 128;
            this.btnFnc09.TabStop = false;
            this.btnFnc09.Text = "F9:検索";
            this.btnFnc09.UseVisualStyleBackColor = true;
            this.btnFnc09.Click += new System.EventHandler(this.BtnFnc09_Click);
            // 
            // btnFnc10
            // 
            this.btnFnc10.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc10.Location = new System.Drawing.Point(1135, 46);
            this.btnFnc10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc10.Name = "btnFnc10";
            this.btnFnc10.Size = new System.Drawing.Size(120, 30);
            this.btnFnc10.TabIndex = 129;
            this.btnFnc10.TabStop = false;
            this.btnFnc10.Text = "F10:条件クリア";
            this.btnFnc10.UseVisualStyleBackColor = true;
            this.btnFnc10.Click += new System.EventHandler(this.BtnFnc10_Click);
            // 
            // btnFnc12
            // 
            this.btnFnc12.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFnc12.Location = new System.Drawing.Point(1135, 884);
            this.btnFnc12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFnc12.Name = "btnFnc12";
            this.btnFnc12.Size = new System.Drawing.Size(120, 30);
            this.btnFnc12.TabIndex = 131;
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
            this.btnFnc11.TabIndex = 130;
            this.btnFnc11.TabStop = false;
            this.btnFnc11.Text = "F11:決定";
            this.btnFnc11.UseVisualStyleBackColor = true;
            this.btnFnc11.Click += new System.EventHandler(this.BtnFnc11_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDelete.Location = new System.Drawing.Point(165, 884);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 133;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete);
            // 
            // grdList
            // 
            this.grdList.BackColor = System.Drawing.Color.DarkGray;
            this.grdList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.grdList.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.grdList.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grdList.Location = new System.Drawing.Point(5, 82);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 1;
            this.grdList.Rows.DefaultSize = 30;
            this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.grdList.Size = new System.Drawing.Size(1252, 796);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.TabIndex = 7;
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxCount.Location = new System.Drawing.Point(11, 888);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(111, 19);
            this.lblMaxCount.TabIndex = 134;
            this.lblMaxCount.Text = "最大999件表示";
            // 
            // txtDKUBUN
            // 
            this.txtDKUBUN.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDKUBUN.Location = new System.Drawing.Point(375, 9);
            this.txtDKUBUN.MaxLength = 6;
            this.txtDKUBUN.Name = "txtDKUBUN";
            this.txtDKUBUN.Size = new System.Drawing.Size(65, 27);
            this.txtDKUBUN.TabIndex = 2;
            this.txtDKUBUN.Text = "123456";
            // 
            // lblDKUBUN
            // 
            this.lblDKUBUN.AutoSize = true;
            this.lblDKUBUN.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDKUBUN.Location = new System.Drawing.Point(330, 12);
            this.lblDKUBUN.Name = "lblDKUBUN";
            this.lblDKUBUN.Size = new System.Drawing.Size(39, 19);
            this.lblDKUBUN.TabIndex = 137;
            this.lblDKUBUN.Text = "伝区";
            // 
            // lblsnKYAKUCHU
            // 
            this.lblsnKYAKUCHU.AutoSize = true;
            this.lblsnKYAKUCHU.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblsnKYAKUCHU.Location = new System.Drawing.Point(784, 13);
            this.lblsnKYAKUCHU.Name = "lblsnKYAKUCHU";
            this.lblsnKYAKUCHU.Size = new System.Drawing.Size(99, 19);
            this.lblsnKYAKUCHU.TabIndex = 138;
            this.lblsnKYAKUCHU.Text = "（前方一致）";
            // 
            // U_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1269, 921);
            this.Controls.Add(this.lblsnKYAKUCHU);
            this.Controls.Add(this.txtDKUBUN);
            this.Controls.Add(this.lblDKUBUN);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnFnc12);
            this.Controls.Add(this.btnFnc11);
            this.Controls.Add(this.btnFnc10);
            this.Controls.Add(this.btnFnc09);
            this.Controls.Add(this.txtREC_TERM);
            this.Controls.Add(this.lblREC_TERM);
            this.Controls.Add(this.txtDNO);
            this.Controls.Add(this.lblDNO);
            this.Controls.Add(this.txtKYAKUCHU);
            this.Controls.Add(this.lblKYAKUCHU);
            this.Controls.Add(this.txtTORIKOMI);
            this.Controls.Add(this.lblTORIKOMI);
            this.Controls.Add(this.txtSNAIBU);
            this.Controls.Add(this.lblSNAIBU);
            this.Controls.Add(this.txtEAMCD);
            this.Controls.Add(this.lblEAMCD);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "U_Search";
            this.Text = "請書取込検索画面";
            this.Load += new System.EventHandler(this.JH_Search_Jyutyu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JH_Search_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.TextBoxChar txtEAMCD;
        private System.Windows.Forms.Label lblEAMCD;
        private CustomControls.TextBoxChar txtSNAIBU;
        private System.Windows.Forms.Label lblSNAIBU;
        private CustomControls.TextBoxChar txtTORIKOMI;
        private System.Windows.Forms.Label lblTORIKOMI;
        private CustomControls.TextBoxChar txtKYAKUCHU;
        private System.Windows.Forms.Label lblKYAKUCHU;
        private CustomControls.TextBoxChar txtDNO;
        private System.Windows.Forms.Label lblDNO;
        private CustomControls.TextBoxChar txtREC_TERM;
        private System.Windows.Forms.Label lblREC_TERM;
        private System.Windows.Forms.Button btnFnc09;
        private System.Windows.Forms.Button btnFnc10;
        private System.Windows.Forms.Button btnFnc12;
        private System.Windows.Forms.Button btnFnc11;
        private System.Windows.Forms.Button btnDelete;
        private C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.Label lblMaxCount;
        private CustomControls.TextBoxChar txtDKUBUN;
        private System.Windows.Forms.Label lblDKUBUN;
        private System.Windows.Forms.Label lblsnKYAKUCHU;
    }
}
namespace HatFClient.Views.MasterSearch
{
    partial class MS_Customer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MS_Customer));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustCode = new System.Windows.Forms.TextBox();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.txtCustKana = new System.Windows.Forms.TextBox();
            this.c1gCustomers = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.txtCustUserDepName = new System.Windows.Forms.TextBox();
            this.txtCustUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1gCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(535, 411);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(118, 27);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "F11:決定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(659, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 27);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "F12:閉じる";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(659, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(118, 27);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "F9:検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工事店コード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "工事店名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(431, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "工事店名カナ";
            // 
            // txtCustCode
            // 
            this.txtCustCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCustCode.Location = new System.Drawing.Point(102, 16);
            this.txtCustCode.Name = "txtCustCode";
            this.txtCustCode.Size = new System.Drawing.Size(86, 19);
            this.txtCustCode.TabIndex = 1;
            // 
            // txtCustName
            // 
            this.txtCustName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCustName.Location = new System.Drawing.Point(293, 16);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(115, 19);
            this.txtCustName.TabIndex = 3;
            // 
            // txtCustKana
            // 
            this.txtCustKana.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txtCustKana.Location = new System.Drawing.Point(509, 16);
            this.txtCustKana.Name = "txtCustKana";
            this.txtCustKana.Size = new System.Drawing.Size(115, 19);
            this.txtCustKana.TabIndex = 5;
            // 
            // c1gCustomers
            // 
            this.c1gCustomers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1gCustomers.ColumnInfo = resources.GetString("c1gCustomers.ColumnInfo");
            this.c1gCustomers.Location = new System.Drawing.Point(22, 78);
            this.c1gCustomers.Name = "c1gCustomers";
            this.c1gCustomers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1gCustomers.Size = new System.Drawing.Size(755, 300);
            this.c1gCustomers.TabIndex = 11;
            this.c1gCustomers.DoubleClick += new System.EventHandler(this.c1gEmployee_DoubleClick);
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(135, 416);
            this.lblNote.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(141, 15);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "工事店情報が存在しません";
            // 
            // lblMaxCount
            // 
            this.lblMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxCount.Location = new System.Drawing.Point(27, 416);
            this.lblMaxCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(88, 15);
            this.lblMaxCount.TabIndex = 12;
            this.lblMaxCount.Text = "最大{0}件表示";
            // 
            // txtCustUserDepName
            // 
            this.txtCustUserDepName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCustUserDepName.Location = new System.Drawing.Point(509, 41);
            this.txtCustUserDepName.Name = "txtCustUserDepName";
            this.txtCustUserDepName.Size = new System.Drawing.Size(115, 19);
            this.txtCustUserDepName.TabIndex = 9;
            // 
            // txtCustUserName
            // 
            this.txtCustUserName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCustUserName.Location = new System.Drawing.Point(293, 41);
            this.txtCustUserName.Name = "txtCustUserName";
            this.txtCustUserName.Size = new System.Drawing.Size(115, 19);
            this.txtCustUserName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(431, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "顧客部門名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "顧客担当者名";
            // 
            // MS_Customer
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCustUserDepName);
            this.Controls.Add(this.txtCustUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.c1gCustomers);
            this.Controls.Add(this.txtCustKana);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.txtCustCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.Name = "MS_Customer";
            this.Text = "工事店検索";
            this.Load += new System.EventHandler(this.MS_Employee_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MS_Employee_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.c1gCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustCode;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.TextBox txtCustKana;
        private C1.Win.C1FlexGrid.C1FlexGrid c1gCustomers;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.TextBox txtCustUserDepName;
        private System.Windows.Forms.TextBox txtCustUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
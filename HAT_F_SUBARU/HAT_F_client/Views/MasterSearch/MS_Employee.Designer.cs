namespace HatFClient.Views.MasterSearch
{
    partial class MS_Employee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MS_Employee));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.txtEmpNameKana = new System.Windows.Forms.TextBox();
            this.c1gEmployee = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMaxCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1gEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(535, 411);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(118, 27);
            this.btnOK.TabIndex = 8;
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
            this.btnCancel.TabIndex = 9;
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
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "F9:検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "社員番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "社員名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "社員カナ";
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtEmpCode.Location = new System.Drawing.Point(90, 16);
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.Size = new System.Drawing.Size(86, 19);
            this.txtEmpCode.TabIndex = 1;
            // 
            // txtEmpName
            // 
            this.txtEmpName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtEmpName.Location = new System.Drawing.Point(255, 16);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(115, 19);
            this.txtEmpName.TabIndex = 3;
            // 
            // txtEmpNameKana
            // 
            this.txtEmpNameKana.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txtEmpNameKana.Location = new System.Drawing.Point(467, 16);
            this.txtEmpNameKana.Name = "txtEmpNameKana";
            this.txtEmpNameKana.Size = new System.Drawing.Size(105, 19);
            this.txtEmpNameKana.TabIndex = 5;
            // 
            // c1gEmployee
            // 
            this.c1gEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1gEmployee.ColumnInfo = resources.GetString("c1gEmployee.ColumnInfo");
            this.c1gEmployee.Location = new System.Drawing.Point(22, 78);
            this.c1gEmployee.Name = "c1gEmployee";
            this.c1gEmployee.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1gEmployee.Size = new System.Drawing.Size(755, 300);
            this.c1gEmployee.TabIndex = 7;
            this.c1gEmployee.DoubleClick += new System.EventHandler(this.c1gEmployee_DoubleClick);
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
            this.lblNote.Size = new System.Drawing.Size(129, 15);
            this.lblNote.TabIndex = 139;
            this.lblNote.Text = "社員情報が存在しません";
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
            this.lblMaxCount.TabIndex = 138;
            this.lblMaxCount.Text = "最大{0}件表示";
            // 
            // MS_Employee
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblMaxCount);
            this.Controls.Add(this.c1gEmployee);
            this.Controls.Add(this.txtEmpNameKana);
            this.Controls.Add(this.txtEmpName);
            this.Controls.Add(this.txtEmpCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.Name = "MS_Employee";
            this.Text = "社員検索";
            this.Load += new System.EventHandler(this.MS_Employee_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MS_Employee_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.c1gEmployee)).EndInit();
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
        private System.Windows.Forms.TextBox txtEmpCode;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.TextBox txtEmpNameKana;
        private C1.Win.C1FlexGrid.C1FlexGrid c1gEmployee;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblMaxCount;
    }
}
namespace HatFClient.Views.Cooperate
{
    partial class ContactEmail
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
            this.lblTO = new System.Windows.Forms.Label();
            this.lblSUBJECT = new System.Windows.Forms.Label();
            this.lblBODY = new System.Windows.Forms.Label();
            this.txtSUBJECT = new System.Windows.Forms.TextBox();
            this.lblFILES = new System.Windows.Forms.Label();
            this.txtBODY = new System.Windows.Forms.TextBox();
            this.btnSEND = new System.Windows.Forms.Button();
            this.btnCLOSE = new System.Windows.Forms.Button();
            this.cmbTO = new HatFClient.CustomControls.ComboBoxEx();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.SuspendLayout();
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTO.Location = new System.Drawing.Point(12, 18);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(43, 15);
            this.lblTO.TabIndex = 76;
            this.lblTO.Text = "連絡先";
            // 
            // lblSUBJECT
            // 
            this.lblSUBJECT.AutoSize = true;
            this.lblSUBJECT.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSUBJECT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSUBJECT.Location = new System.Drawing.Point(12, 52);
            this.lblSUBJECT.Name = "lblSUBJECT";
            this.lblSUBJECT.Size = new System.Drawing.Size(31, 15);
            this.lblSUBJECT.TabIndex = 77;
            this.lblSUBJECT.Text = "件名";
            // 
            // lblBODY
            // 
            this.lblBODY.AutoSize = true;
            this.lblBODY.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBODY.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBODY.Location = new System.Drawing.Point(12, 85);
            this.lblBODY.Name = "lblBODY";
            this.lblBODY.Size = new System.Drawing.Size(40, 15);
            this.lblBODY.TabIndex = 77;
            this.lblBODY.Text = "コメント";
            // 
            // txtSUBJECT
            // 
            this.txtSUBJECT.Location = new System.Drawing.Point(84, 50);
            this.txtSUBJECT.Name = "txtSUBJECT";
            this.txtSUBJECT.Size = new System.Drawing.Size(673, 19);
            this.txtSUBJECT.TabIndex = 80;
            // 
            // lblFILES
            // 
            this.lblFILES.AutoSize = true;
            this.lblFILES.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFILES.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFILES.Location = new System.Drawing.Point(12, 189);
            this.lblFILES.Name = "lblFILES";
            this.lblFILES.Size = new System.Drawing.Size(65, 15);
            this.lblFILES.TabIndex = 81;
            this.lblFILES.Text = "添付ファイル";
            // 
            // txtBODY
            // 
            this.txtBODY.Location = new System.Drawing.Point(84, 85);
            this.txtBODY.Multiline = true;
            this.txtBODY.Name = "txtBODY";
            this.txtBODY.Size = new System.Drawing.Size(673, 87);
            this.txtBODY.TabIndex = 83;
            // 
            // btnSEND
            // 
            this.btnSEND.Location = new System.Drawing.Point(537, 426);
            this.btnSEND.Name = "btnSEND";
            this.btnSEND.Size = new System.Drawing.Size(107, 23);
            this.btnSEND.TabIndex = 84;
            this.btnSEND.Text = "メーラーを起動";
            this.btnSEND.UseVisualStyleBackColor = true;
            this.btnSEND.Click += new System.EventHandler(this.btnSEND_Click);
            // 
            // btnCLOSE
            // 
            this.btnCLOSE.Location = new System.Drawing.Point(650, 426);
            this.btnCLOSE.Name = "btnCLOSE";
            this.btnCLOSE.Size = new System.Drawing.Size(107, 23);
            this.btnCLOSE.TabIndex = 85;
            this.btnCLOSE.Text = "閉じる";
            this.btnCLOSE.UseVisualStyleBackColor = true;
            this.btnCLOSE.Click += new System.EventHandler(this.btnCLOSE_Click);
            // 
            // cmbTO
            // 
            this.cmbTO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTO.FormattingEnabled = true;
            this.cmbTO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cmbTO.Location = new System.Drawing.Point(84, 15);
            this.cmbTO.Name = "cmbTO";
            this.cmbTO.Size = new System.Drawing.Size(336, 20);
            this.cmbTO.TabIndex = 86;
            // 
            // blobStrageForm1
            // 
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(12, 207);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(760, 210);
            this.blobStrageForm1.StrageId = "contact_email";
            this.blobStrageForm1.TabIndex = 82;
            // 
            // ContactEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.cmbTO);
            this.Controls.Add(this.btnCLOSE);
            this.Controls.Add(this.btnSEND);
            this.Controls.Add(this.txtBODY);
            this.Controls.Add(this.blobStrageForm1);
            this.Controls.Add(this.lblFILES);
            this.Controls.Add(this.txtSUBJECT);
            this.Controls.Add(this.lblBODY);
            this.Controls.Add(this.lblSUBJECT);
            this.Controls.Add(this.lblTO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContactEmail";
            this.Text = "担当者への連絡";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTO;
        private System.Windows.Forms.Label lblSUBJECT;
        private System.Windows.Forms.Label lblBODY;
        private System.Windows.Forms.TextBox txtSUBJECT;
        private System.Windows.Forms.Label lblFILES;
        private CustomControls.BlobStrage.BlobStrageForm blobStrageForm1;
        private System.Windows.Forms.TextBox txtBODY;
        private System.Windows.Forms.Button btnSEND;
        private System.Windows.Forms.Button btnCLOSE;
        private CustomControls.ComboBoxEx cmbTO;
    }
}
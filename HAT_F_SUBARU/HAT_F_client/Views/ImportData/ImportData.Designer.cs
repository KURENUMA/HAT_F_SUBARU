namespace HatFClient.Views.ImportDeliveryData
{
    partial class ImportData
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCOPY_CLIPBOARD_IMG = new System.Windows.Forms.Button();
            this.btnIMG_EXCEL = new System.Windows.Forms.Button();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnCOPY_CRIPBADRD_GRID = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdateConstructio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(30, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(490, 487);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCOPY_CLIPBOARD_IMG
            // 
            this.btnCOPY_CLIPBOARD_IMG.Location = new System.Drawing.Point(30, 21);
            this.btnCOPY_CLIPBOARD_IMG.Name = "btnCOPY_CLIPBOARD_IMG";
            this.btnCOPY_CLIPBOARD_IMG.Size = new System.Drawing.Size(172, 30);
            this.btnCOPY_CLIPBOARD_IMG.TabIndex = 1;
            this.btnCOPY_CLIPBOARD_IMG.Text = "クリップボードの画像をコピー";
            this.btnCOPY_CLIPBOARD_IMG.UseVisualStyleBackColor = true;
            this.btnCOPY_CLIPBOARD_IMG.Click += new System.EventHandler(this.btnCOPY_CLIPBOARD_IMG_Click);
            // 
            // btnIMG_EXCEL
            // 
            this.btnIMG_EXCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIMG_EXCEL.Enabled = false;
            this.btnIMG_EXCEL.Location = new System.Drawing.Point(348, 553);
            this.btnIMG_EXCEL.Name = "btnIMG_EXCEL";
            this.btnIMG_EXCEL.Size = new System.Drawing.Size(172, 30);
            this.btnIMG_EXCEL.TabIndex = 2;
            this.btnIMG_EXCEL.Text = "画像をExcel化";
            this.btnIMG_EXCEL.UseVisualStyleBackColor = true;
            this.btnIMG_EXCEL.Click += new System.EventHandler(this.btnIMG_EXCEL_ClickAsync);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.Location = new System.Drawing.Point(559, 60);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Size = new System.Drawing.Size(532, 487);
            this.c1FlexGrid1.TabIndex = 3;
            // 
            // btnCOPY_CRIPBADRD_GRID
            // 
            this.btnCOPY_CRIPBADRD_GRID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCOPY_CRIPBADRD_GRID.Location = new System.Drawing.Point(559, 21);
            this.btnCOPY_CRIPBADRD_GRID.Name = "btnCOPY_CRIPBADRD_GRID";
            this.btnCOPY_CRIPBADRD_GRID.Size = new System.Drawing.Size(172, 30);
            this.btnCOPY_CRIPBADRD_GRID.TabIndex = 4;
            this.btnCOPY_CRIPBADRD_GRID.Text = "クリップボードからコピー";
            this.btnCOPY_CRIPBADRD_GRID.UseVisualStyleBackColor = true;
            this.btnCOPY_CRIPBADRD_GRID.Click += new System.EventHandler(this.btnCOPY_CRIPBADRD_GRID_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancel.Location = new System.Drawing.Point(971, 603);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "F12：閉じる";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdateConstructio
            // 
            this.btnUpdateConstructio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateConstructio.Enabled = false;
            this.btnUpdateConstructio.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdateConstructio.Location = new System.Drawing.Point(818, 603);
            this.btnUpdateConstructio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpdateConstructio.Name = "btnUpdateConstructio";
            this.btnUpdateConstructio.Size = new System.Drawing.Size(120, 30);
            this.btnUpdateConstructio.TabIndex = 18;
            this.btnUpdateConstructio.TabStop = false;
            this.btnUpdateConstructio.Text = "F11：更新";
            this.btnUpdateConstructio.UseVisualStyleBackColor = true;
            // 
            // ImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 659);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateConstructio);
            this.Controls.Add(this.btnCOPY_CRIPBADRD_GRID);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.btnIMG_EXCEL);
            this.Controls.Add(this.btnCOPY_CLIPBOARD_IMG);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ImportData";
            this.Text = "データ取込";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCOPY_CLIPBOARD_IMG;
        private System.Windows.Forms.Button btnIMG_EXCEL;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Button btnCOPY_CRIPBADRD_GRID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdateConstructio;
    }
}
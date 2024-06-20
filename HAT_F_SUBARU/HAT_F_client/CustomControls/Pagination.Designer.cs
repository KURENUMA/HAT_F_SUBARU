namespace templateApp.Controls
{
    partial class Pagination
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.lblPageIndicator = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Location = new System.Drawing.Point(4, 4);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(37, 23);
            this.btnFirstPage.TabIndex = 0;
            this.btnFirstPage.Text = "<<";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(47, 4);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(37, 23);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.Text = "<";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            // 
            // lblPageIndicator
            // 
            this.lblPageIndicator.AutoSize = true;
            this.lblPageIndicator.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPageIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPageIndicator.Location = new System.Drawing.Point(90, 7);
            this.lblPageIndicator.Name = "lblPageIndicator";
            this.lblPageIndicator.Size = new System.Drawing.Size(82, 17);
            this.lblPageIndicator.TabIndex = 2;
            this.lblPageIndicator.Text = "ページ 1/100";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(184, 4);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(37, 23);
            this.btnNextPage.TabIndex = 3;
            this.btnNextPage.Text = ">";
            this.btnNextPage.UseVisualStyleBackColor = true;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Location = new System.Drawing.Point(227, 4);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(37, 23);
            this.btnLastPage.TabIndex = 4;
            this.btnLastPage.Text = ">>";
            this.btnLastPage.UseVisualStyleBackColor = true;
            // 
            // Pagination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.lblPageIndicator);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnFirstPage);
            this.Name = "Pagination";
            this.Size = new System.Drawing.Size(269, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Label lblPageIndicator;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnLastPage;
    }
}

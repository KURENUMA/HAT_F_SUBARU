namespace HatFClient.CustomControls
{
    partial class GridPatternUI
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPatternDelete = new System.Windows.Forms.Button();
            this.btnPatternCopy = new System.Windows.Forms.Button();
            this.btnPatternNew = new System.Windows.Forms.Button();
            this.btnPatternEdit = new System.Windows.Forms.Button();
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPatternDelete);
            this.groupBox1.Controls.Add(this.btnPatternCopy);
            this.groupBox1.Controls.Add(this.btnPatternNew);
            this.groupBox1.Controls.Add(this.btnPatternEdit);
            this.groupBox1.Controls.Add(this.cmbPattern);
            this.groupBox1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "表示パターン";
            // 
            // btnPatternDelete
            // 
            this.btnPatternDelete.Enabled = false;
            this.btnPatternDelete.Location = new System.Drawing.Point(368, 24);
            this.btnPatternDelete.Name = "btnPatternDelete";
            this.btnPatternDelete.Size = new System.Drawing.Size(60, 23);
            this.btnPatternDelete.TabIndex = 8;
            this.btnPatternDelete.Text = "削除";
            this.btnPatternDelete.UseVisualStyleBackColor = true;
            this.btnPatternDelete.Click += new System.EventHandler(this.btnPatternDelete_Click);
            // 
            // btnPatternCopy
            // 
            this.btnPatternCopy.Enabled = false;
            this.btnPatternCopy.Location = new System.Drawing.Point(302, 24);
            this.btnPatternCopy.Name = "btnPatternCopy";
            this.btnPatternCopy.Size = new System.Drawing.Size(60, 23);
            this.btnPatternCopy.TabIndex = 7;
            this.btnPatternCopy.Text = "複製";
            this.btnPatternCopy.UseVisualStyleBackColor = true;
            this.btnPatternCopy.Click += new System.EventHandler(this.btnPatternCopy_Click);
            // 
            // btnPatternNew
            // 
            this.btnPatternNew.Enabled = false;
            this.btnPatternNew.Location = new System.Drawing.Point(236, 24);
            this.btnPatternNew.Name = "btnPatternNew";
            this.btnPatternNew.Size = new System.Drawing.Size(60, 23);
            this.btnPatternNew.TabIndex = 6;
            this.btnPatternNew.Text = "新規";
            this.btnPatternNew.UseVisualStyleBackColor = true;
            this.btnPatternNew.Click += new System.EventHandler(this.btnPatternNew_Click);
            // 
            // btnPatternEdit
            // 
            this.btnPatternEdit.Enabled = false;
            this.btnPatternEdit.Location = new System.Drawing.Point(170, 24);
            this.btnPatternEdit.Name = "btnPatternEdit";
            this.btnPatternEdit.Size = new System.Drawing.Size(60, 23);
            this.btnPatternEdit.TabIndex = 5;
            this.btnPatternEdit.Text = "編集";
            this.btnPatternEdit.UseVisualStyleBackColor = true;
            this.btnPatternEdit.Click += new System.EventHandler(this.btnPatternEdit_Click);
            // 
            // cmbPattern
            // 
            this.cmbPattern.Enabled = false;
            this.cmbPattern.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Location = new System.Drawing.Point(15, 24);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(140, 23);
            this.cmbPattern.TabIndex = 4;
            this.cmbPattern.SelectedValueChanged += new System.EventHandler(this.cmbPattern_SelectedValueChanged);
            // 
            // GridPatternUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "GridPatternUI";
            this.Size = new System.Drawing.Size(444, 62);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPatternDelete;
        private System.Windows.Forms.Button btnPatternCopy;
        private System.Windows.Forms.Button btnPatternNew;
        private System.Windows.Forms.Button btnPatternEdit;
        private System.Windows.Forms.ComboBox cmbPattern;
    }
}

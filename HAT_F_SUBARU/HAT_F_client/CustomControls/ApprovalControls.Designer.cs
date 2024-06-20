namespace HatFClient.CustomControls
{
    partial class ApprovalControls
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemand = new System.Windows.Forms.Button();
            this.btnApplication = new System.Windows.Forms.Button();
            this.btnAapproval = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blobStrageForm1 = new HatFClient.CustomControls.BlobStrage.BlobStrageForm();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnRemand);
            this.flowLayoutPanel1.Controls.Add(this.btnApplication);
            this.flowLayoutPanel1.Controls.Add(this.btnAapproval);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(710, 337);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnRemand
            // 
            this.btnRemand.Location = new System.Drawing.Point(3, 3);
            this.btnRemand.Name = "btnRemand";
            this.btnRemand.Size = new System.Drawing.Size(99, 23);
            this.btnRemand.TabIndex = 0;
            this.btnRemand.Text = "差し戻し";
            this.btnRemand.UseVisualStyleBackColor = true;
            // 
            // btnApplication
            // 
            this.btnApplication.Location = new System.Drawing.Point(108, 3);
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.Size = new System.Drawing.Size(99, 23);
            this.btnApplication.TabIndex = 1;
            this.btnApplication.Text = "申請";
            this.btnApplication.UseVisualStyleBackColor = true;
            // 
            // btnAapproval
            // 
            this.btnAapproval.Location = new System.Drawing.Point(213, 3);
            this.btnAapproval.Name = "btnAapproval";
            this.btnAapproval.Size = new System.Drawing.Size(99, 23);
            this.btnAapproval.TabIndex = 2;
            this.btnAapproval.Text = "承認";
            this.btnAapproval.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(318, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowFiltering = true;
            this.c1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows;
            this.c1FlexGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1FlexGrid1.AutoClipboard = true;
            this.c1FlexGrid1.ClipboardCopyMode = C1.Win.C1FlexGrid.ClipboardCopyModeEnum.DataAndColumnHeaders;
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,-1,Columns:";
            this.c1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
            this.c1FlexGrid1.Location = new System.Drawing.Point(627, 89);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(508, 242);
            this.c1FlexGrid1.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label3.Location = new System.Drawing.Point(46, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "コメント";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label5.Location = new System.Drawing.Point(46, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "承認者";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label4.Location = new System.Drawing.Point(20, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "最終承認者";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "荻原　功",
            "田中　健二",
            "岡野　誠司",
            "生田　慎",
            "石川　光彦",
            "後藤　和久",
            "大沼　良太",
            "冨田　賢一",
            "渡来　千宜",
            "三石　隆浩",
            "和知　拓哉",
            "鎌倉　隆志",
            "油井　栄次郎",
            "井上　潤一",
            "岩木　優太",
            "本田　創平",
            "名取　達裕",
            "山崎　康輝",
            "山本　雄大",
            "福原　達也",
            "宮野　賢人",
            "深見　隆仁"});
            this.comboBox1.Location = new System.Drawing.Point(99, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 20);
            this.comboBox1.TabIndex = 34;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "荻原　功",
            "田中　健二",
            "岡野　誠司",
            "生田　慎",
            "石川　光彦",
            "後藤　和久",
            "大沼　良太",
            "冨田　賢一",
            "渡来　千宜",
            "三石　隆浩",
            "和知　拓哉",
            "鎌倉　隆志",
            "油井　栄次郎",
            "井上　潤一",
            "岩木　優太",
            "本田　創平",
            "名取　達裕",
            "山崎　康輝",
            "山本　雄大",
            "福原　達也",
            "宮野　賢人",
            "深見　隆仁"});
            this.comboBox2.Location = new System.Drawing.Point(99, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(161, 20);
            this.comboBox2.TabIndex = 32;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox2.Location = new System.Drawing.Point(99, 89);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(470, 61);
            this.textBox2.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.blobStrageForm1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.c1FlexGrid1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Location = new System.Drawing.Point(20, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1149, 378);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "売上確定済み情報の訂正承認";
            // 
            // blobStrageForm1
            // 
            this.blobStrageForm1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.blobStrageForm1.Location = new System.Drawing.Point(39, 156);
            this.blobStrageForm1.Name = "blobStrageForm1";
            this.blobStrageForm1.Size = new System.Drawing.Size(530, 210);
            this.blobStrageForm1.StrageId = null;
            this.blobStrageForm1.TabIndex = 43;
            // 
            // ApprovalControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.groupBox1);
            this.Name = "ApprovalControls";
            this.Size = new System.Drawing.Size(1184, 420);
            this.Load += new System.EventHandler(this.ApprovalControls_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRemand;
        private System.Windows.Forms.Button btnApplication;
        private System.Windows.Forms.Button btnAapproval;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox2;
        public C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private BlobStrage.BlobStrageForm blobStrageForm1;
    }
}

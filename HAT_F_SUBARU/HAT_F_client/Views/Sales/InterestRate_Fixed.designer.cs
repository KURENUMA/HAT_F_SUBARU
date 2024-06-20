namespace HatFClient.Views.Sales
{
    partial class InterestRate_Fixed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterestRate_Fixed));
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.grpDetail = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new C1.Win.C1Input.C1Button();
            this.txtComment = new C1.Win.C1Input.C1TextBox();
            this.grdDetail = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRateUnder = new HatFClient.CustomControls.TextBoxNum();
            this.txtUriKinOver = new HatFClient.CustomControls.TextBoxNum();
            this.txtSuryoOver = new HatFClient.CustomControls.TextBoxNum();
            this.txtRateOver = new HatFClient.CustomControls.TextBoxNum();
            this.label5 = new System.Windows.Forms.Label();
            this.lblConditionUriKin = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblConditionSuryo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblConditionRate = new System.Windows.Forms.Label();
            this.chkUriTanZero = new System.Windows.Forms.CheckBox();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.grpDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.Location = new System.Drawing.Point(979, 6);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtCondition.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCondition.Location = new System.Drawing.Point(575, 6);
            this.txtCondition.Margin = new System.Windows.Forms.Padding(4);
            this.txtCondition.Multiline = true;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondition.Size = new System.Drawing.Size(396, 140);
            this.txtCondition.TabIndex = 1;
            // 
            // grpList
            // 
            this.grpList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpList.Controls.Add(this.grdList);
            this.grpList.Location = new System.Drawing.Point(18, 153);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(544, 522);
            this.grpList.TabIndex = 3;
            this.grpList.TabStop = false;
            this.grpList.Text = "売上一覧";
            // 
            // grdList
            // 
            this.grdList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.grdList.AllowEditing = false;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.AutoGenerateColumns = false;
            this.grdList.ColumnInfo = resources.GetString("grdList.ColumnInfo");
            this.grdList.Location = new System.Drawing.Point(17, 22);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 1;
            this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.grdList.Size = new System.Drawing.Size(521, 486);
            this.grdList.TabIndex = 0;
            // 
            // grpDetail
            // 
            this.grpDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetail.Controls.Add(this.label1);
            this.grpDetail.Controls.Add(this.btnSave);
            this.grpDetail.Controls.Add(this.txtComment);
            this.grpDetail.Controls.Add(this.grdDetail);
            this.grpDetail.Location = new System.Drawing.Point(575, 153);
            this.grpDetail.Name = "grpDetail";
            this.grpDetail.Size = new System.Drawing.Size(625, 522);
            this.grpDetail.TabIndex = 4;
            this.grpDetail.TabStop = false;
            this.grpDetail.Text = "明細";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "コメント";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(499, 492);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 24);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.UseVisualStyleForeColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtComment.Location = new System.Drawing.Point(9, 373);
            this.txtComment.MaxLength = 1000;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(610, 113);
            this.txtComment.TabIndex = 2;
            this.txtComment.Tag = null;
            this.txtComment.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            this.txtComment.TextChanged += new System.EventHandler(this.TxtComment_TextChanged);
            // 
            // grdDetail
            // 
            this.grdDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDetail.AutoGenerateColumns = false;
            this.grdDetail.ColumnInfo = resources.GetString("grdDetail.ColumnInfo");
            this.grdDetail.Location = new System.Drawing.Point(15, 22);
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.Rows.Count = 1;
            this.grdDetail.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.grdDetail.Size = new System.Drawing.Size(604, 311);
            this.grdDetail.TabIndex = 0;
            this.grdDetail.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdDetail_CellChecked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUriTanZero);
            this.groupBox1.Controls.Add(this.txtRateUnder);
            this.groupBox1.Controls.Add(this.txtUriKinOver);
            this.groupBox1.Controls.Add(this.txtSuryoOver);
            this.groupBox1.Controls.Add(this.txtRateOver);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblConditionUriKin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblConditionSuryo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblConditionRate);
            this.groupBox1.Location = new System.Drawing.Point(18, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本検索条件";
            // 
            // txtRateUnder
            // 
            this.txtRateUnder.Enabled = false;
            this.txtRateUnder.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtRateUnder.Location = new System.Drawing.Point(244, 21);
            this.txtRateUnder.MaxLength = 10;
            this.txtRateUnder.Name = "txtRateUnder";
            this.txtRateUnder.Size = new System.Drawing.Size(54, 23);
            this.txtRateUnder.TabIndex = 3;
            this.txtRateUnder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUriKinOver
            // 
            this.txtUriKinOver.Enabled = false;
            this.txtUriKinOver.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtUriKinOver.Location = new System.Drawing.Point(107, 79);
            this.txtUriKinOver.MaxLength = 3;
            this.txtUriKinOver.Name = "txtUriKinOver";
            this.txtUriKinOver.Size = new System.Drawing.Size(103, 23);
            this.txtUriKinOver.TabIndex = 9;
            this.txtUriKinOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSuryoOver
            // 
            this.txtSuryoOver.Enabled = false;
            this.txtSuryoOver.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSuryoOver.Location = new System.Drawing.Point(107, 50);
            this.txtSuryoOver.MaxLength = 3;
            this.txtSuryoOver.Name = "txtSuryoOver";
            this.txtSuryoOver.Size = new System.Drawing.Size(54, 23);
            this.txtSuryoOver.TabIndex = 6;
            this.txtSuryoOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRateOver
            // 
            this.txtRateOver.Enabled = false;
            this.txtRateOver.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtRateOver.Location = new System.Drawing.Point(107, 21);
            this.txtRateOver.MaxLength = 10;
            this.txtRateOver.Name = "txtRateOver";
            this.txtRateOver.Size = new System.Drawing.Size(54, 23);
            this.txtRateOver.TabIndex = 1;
            this.txtRateOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "円以上";
            // 
            // lblConditionUriKin
            // 
            this.lblConditionUriKin.AutoSize = true;
            this.lblConditionUriKin.Location = new System.Drawing.Point(14, 83);
            this.lblConditionUriKin.Name = "lblConditionUriKin";
            this.lblConditionUriKin.Size = new System.Drawing.Size(87, 15);
            this.lblConditionUriKin.TabIndex = 8;
            this.lblConditionUriKin.Text = "行単位での金額";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "個以上";
            // 
            // lblConditionSuryo
            // 
            this.lblConditionSuryo.AutoSize = true;
            this.lblConditionSuryo.Location = new System.Drawing.Point(14, 54);
            this.lblConditionSuryo.Name = "lblConditionSuryo";
            this.lblConditionSuryo.Size = new System.Drawing.Size(87, 15);
            this.lblConditionSuryo.TabIndex = 5;
            this.lblConditionSuryo.Text = "行単位での数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "％以下";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "％以上または";
            // 
            // lblConditionRate
            // 
            this.lblConditionRate.AutoSize = true;
            this.lblConditionRate.Location = new System.Drawing.Point(14, 25);
            this.lblConditionRate.Name = "lblConditionRate";
            this.lblConditionRate.Size = new System.Drawing.Size(87, 15);
            this.lblConditionRate.TabIndex = 0;
            this.lblConditionRate.Text = "行単位での利率";
            // 
            // chkUriTanZero
            // 
            this.chkUriTanZero.AutoSize = true;
            this.chkUriTanZero.Enabled = false;
            this.chkUriTanZero.Location = new System.Drawing.Point(107, 110);
            this.chkUriTanZero.Name = "chkUriTanZero";
            this.chkUriTanZero.Size = new System.Drawing.Size(115, 19);
            this.chkUriTanZero.TabIndex = 11;
            this.chkUriTanZero.Text = "売上単価がゼロ円";
            this.chkUriTanZero.UseVisualStyleBackColor = true;
            // 
            // InterestRate_Fixed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1212, 687);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpDetail);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.btnSearch);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1228, 726);
            this.Name = "InterestRate_Fixed";
            this.ShowIcon = false;
            this.Text = "売上確定後　利率異常チェック";
            this.Load += new System.EventHandler(this.InterestRate_Fixed_Load);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.grpDetail.ResumeLayout(false);
            this.grpDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.GroupBox grpList;
        private C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.GroupBox grpDetail;
        private C1.Win.C1FlexGrid.C1FlexGrid grdDetail;
        private C1.Win.C1Input.C1TextBox txtComment;
        private C1.Win.C1Input.C1Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblConditionUriKin;
        private System.Windows.Forms.Label lblConditionSuryo;
        private System.Windows.Forms.Label lblConditionRate;
        private CustomControls.TextBoxNum txtRateUnder;
        private CustomControls.TextBoxNum txtRateOver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private CustomControls.TextBoxNum txtUriKinOver;
        private CustomControls.TextBoxNum txtSuryoOver;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkUriTanZero;
    }
}
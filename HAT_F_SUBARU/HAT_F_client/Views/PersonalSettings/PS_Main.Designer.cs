namespace HatFClient.Views.PersonalSettings
{
    partial class PS_Main
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabOrder = new System.Windows.Forms.TabPage();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnTryCopy = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNOTE = new HatFClient.CustomControls.TextBoxChar();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOnOff = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoOFF = new System.Windows.Forms.RadioButton();
            this.rdoON = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabInterestRate = new System.Windows.Forms.TabPage();
            this.txtUriKin = new HatFClient.CustomControls.TextBoxNum();
            this.txtSuryo = new HatFClient.CustomControls.TextBoxNum();
            this.txtRateUnder = new HatFClient.CustomControls.TextBoxNum();
            this.txtRateOver = new HatFClient.CustomControls.TextBoxNum();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSettings.SuspendLayout();
            this.tabOrder.SuspendLayout();
            this.tabInterestRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(753, 516);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnConfirm.Location = new System.Drawing.Point(619, 516);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(120, 30);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.TabStop = false;
            this.btnConfirm.Text = "決定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Meiryo UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(30, 29);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "個人設定";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Font = new System.Drawing.Font("Meiryo UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLoginName.Location = new System.Drawing.Point(141, 29);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(153, 24);
            this.lblLoginName.TabIndex = 1;
            this.lblLoginName.Text = "（〇さんの設定）";
            // 
            // tabSettings
            // 
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabOrder);
            this.tabSettings.Controls.Add(this.tabInterestRate);
            this.tabSettings.Location = new System.Drawing.Point(34, 70);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(839, 440);
            this.tabSettings.TabIndex = 2;
            // 
            // tabOrder
            // 
            this.tabOrder.Controls.Add(this.btnDefault);
            this.tabOrder.Controls.Add(this.btnTryCopy);
            this.tabOrder.Controls.Add(this.label9);
            this.tabOrder.Controls.Add(this.txtNOTE);
            this.tabOrder.Controls.Add(this.label8);
            this.tabOrder.Controls.Add(this.label7);
            this.tabOrder.Controls.Add(this.lblOnOff);
            this.tabOrder.Controls.Add(this.label6);
            this.tabOrder.Controls.Add(this.label5);
            this.tabOrder.Controls.Add(this.rdoOFF);
            this.tabOrder.Controls.Add(this.rdoON);
            this.tabOrder.Controls.Add(this.label4);
            this.tabOrder.Controls.Add(this.label3);
            this.tabOrder.Location = new System.Drawing.Point(4, 24);
            this.tabOrder.Name = "tabOrder";
            this.tabOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrder.Size = new System.Drawing.Size(831, 412);
            this.tabOrder.TabIndex = 0;
            this.tabOrder.Text = "受発注";
            // 
            // btnDefault
            // 
            this.btnDefault.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDefault.Location = new System.Drawing.Point(154, 310);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(120, 30);
            this.btnDefault.TabIndex = 12;
            this.btnDefault.TabStop = false;
            this.btnDefault.Text = "初期状態へ戻す";
            this.btnDefault.UseVisualStyleBackColor = true;
            // 
            // btnTryCopy
            // 
            this.btnTryCopy.Font = new System.Drawing.Font("Meiryo UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTryCopy.Location = new System.Drawing.Point(20, 310);
            this.btnTryCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTryCopy.Name = "btnTryCopy";
            this.btnTryCopy.Size = new System.Drawing.Size(120, 30);
            this.btnTryCopy.TabIndex = 11;
            this.btnTryCopy.TabStop = false;
            this.btnTryCopy.Text = "コピーを試す";
            this.btnTryCopy.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(30, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(370, 36);
            this.label9.TabIndex = 10;
            this.label9.Text = "※[伝票番号]は伝票番号。[客先注番]は客先注番を表します。\r\n※テキストの長さは100文字まで。";
            // 
            // txtNOTE
            // 
            this.txtNOTE.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNOTE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtNOTE.Location = new System.Drawing.Point(32, 204);
            this.txtNOTE.Name = "txtNOTE";
            this.txtNOTE.Size = new System.Drawing.Size(771, 25);
            this.txtNOTE.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label8.Location = new System.Drawing.Point(29, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(454, 36);
            this.label8.TabIndex = 8;
            this.label8.Text = "「自動クリップボード機能」が発動した時や、「伝番客注コピー」ボタンを押した時に、\r\nクリップボードへコピーされるテキストの内容をここで編集できます。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(9, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(173, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "■クリップボードコピーの内容";
            // 
            // lblOnOff
            // 
            this.lblOnOff.AutoSize = true;
            this.lblOnOff.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOnOff.Location = new System.Drawing.Point(210, 78);
            this.lblOnOff.Name = "lblOnOff";
            this.lblOnOff.Size = new System.Drawing.Size(130, 18);
            this.lblOnOff.TabIndex = 6;
            this.lblOnOff.Text = "この機能を有効にする";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(173, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "】";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(9, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "【";
            // 
            // rdoOFF
            // 
            this.rdoOFF.AutoSize = true;
            this.rdoOFF.Location = new System.Drawing.Point(108, 78);
            this.rdoOFF.Name = "rdoOFF";
            this.rdoOFF.Size = new System.Drawing.Size(48, 19);
            this.rdoOFF.TabIndex = 4;
            this.rdoOFF.TabStop = true;
            this.rdoOFF.Text = "OFF";
            this.rdoOFF.UseVisualStyleBackColor = true;
            // 
            // rdoON
            // 
            this.rdoON.AutoSize = true;
            this.rdoON.Location = new System.Drawing.Point(34, 78);
            this.rdoON.Name = "rdoON";
            this.rdoON.Size = new System.Drawing.Size(43, 19);
            this.rdoON.TabIndex = 3;
            this.rdoON.TabStop = true;
            this.rdoON.Text = "ON";
            this.rdoON.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(505, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "受注確定や発注照合の時に自動でクリップボードへ伝票番号と客注をコピーする機能です。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "■自動クリップボードコピー機能";
            // 
            // tabInterestRate
            // 
            this.tabInterestRate.Controls.Add(this.txtUriKin);
            this.tabInterestRate.Controls.Add(this.txtSuryo);
            this.tabInterestRate.Controls.Add(this.txtRateUnder);
            this.tabInterestRate.Controls.Add(this.txtRateOver);
            this.tabInterestRate.Controls.Add(this.label10);
            this.tabInterestRate.Controls.Add(this.label14);
            this.tabInterestRate.Controls.Add(this.label12);
            this.tabInterestRate.Controls.Add(this.label15);
            this.tabInterestRate.Controls.Add(this.label2);
            this.tabInterestRate.Controls.Add(this.label13);
            this.tabInterestRate.Controls.Add(this.label11);
            this.tabInterestRate.Controls.Add(this.label1);
            this.tabInterestRate.Location = new System.Drawing.Point(4, 24);
            this.tabInterestRate.Name = "tabInterestRate";
            this.tabInterestRate.Padding = new System.Windows.Forms.Padding(3);
            this.tabInterestRate.Size = new System.Drawing.Size(831, 412);
            this.tabInterestRate.TabIndex = 1;
            this.tabInterestRate.Text = "利率異常チェック";
            // 
            // txtUriKin
            // 
            this.txtUriKin.BackColor = System.Drawing.SystemColors.Window;
            this.txtUriKin.Font = new System.Drawing.Font("Meiryo UI", 10.2F);
            this.txtUriKin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUriKin.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtUriKin.Location = new System.Drawing.Point(48, 203);
            this.txtUriKin.Name = "txtUriKin";
            this.txtUriKin.Size = new System.Drawing.Size(140, 25);
            this.txtUriKin.TabIndex = 9;
            this.txtUriKin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSuryo
            // 
            this.txtSuryo.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSuryo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSuryo.Location = new System.Drawing.Point(48, 125);
            this.txtSuryo.Name = "txtSuryo";
            this.txtSuryo.Size = new System.Drawing.Size(65, 25);
            this.txtSuryo.TabIndex = 6;
            this.txtSuryo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRateUnder
            // 
            this.txtRateUnder.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtRateUnder.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtRateUnder.Location = new System.Drawing.Point(210, 45);
            this.txtRateUnder.Name = "txtRateUnder";
            this.txtRateUnder.Size = new System.Drawing.Size(65, 25);
            this.txtRateUnder.TabIndex = 3;
            this.txtRateUnder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRateOver
            // 
            this.txtRateOver.BackColor = System.Drawing.Color.Yellow;
            this.txtRateOver.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.txtRateOver.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtRateOver.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtRateOver.Location = new System.Drawing.Point(48, 45);
            this.txtRateOver.Name = "txtRateOver";
            this.txtRateOver.Size = new System.Drawing.Size(65, 25);
            this.txtRateOver.TabIndex = 1;
            this.txtRateOver.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(281, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 18);
            this.label10.TabIndex = 4;
            this.label10.Text = "％以下";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(194, 206);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 18);
            this.label14.TabIndex = 10;
            this.label14.Text = "円以上";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(119, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 18);
            this.label12.TabIndex = 7;
            this.label12.Text = "個以上";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(6, 247);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(410, 18);
            this.label15.TabIndex = 11;
            this.label15.Text = "以上の条件をすべて満たす売上が利率異常チェック画面に表示されます。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(119, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "％以上または";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(6, 169);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 18);
            this.label13.TabIndex = 8;
            this.label13.Text = "■行単位での売上額";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(6, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 18);
            this.label11.TabIndex = 5;
            this.label11.Text = "■行単位での数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "■伝票単位での利率";
            // 
            // PS_Main
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(900, 570);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PS_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "個人設定";
            this.Load += new System.EventHandler(this.PS_Main_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabOrder.ResumeLayout(false);
            this.tabOrder.PerformLayout();
            this.tabInterestRate.ResumeLayout(false);
            this.tabInterestRate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabOrder;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnTryCopy;
        private System.Windows.Forms.Label label9;
        private CustomControls.TextBoxChar txtNOTE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOnOff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoOFF;
        private System.Windows.Forms.RadioButton rdoON;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabInterestRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private CustomControls.TextBoxNum txtUriKin;
        private CustomControls.TextBoxNum txtSuryo;
        private CustomControls.TextBoxNum txtRateUnder;
        private CustomControls.TextBoxNum txtRateOver;
    }
}
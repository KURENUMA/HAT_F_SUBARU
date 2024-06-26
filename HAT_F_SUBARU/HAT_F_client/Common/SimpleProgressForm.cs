using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Common
{
    /// <summary>Marqueeタイプのプログレスバーを表示するダイアログ</summary>
    /// <remarks>
    /// 前提：
    /// 《時間がかかる処理＝外部サービス呼出》と想定し、非同期処理中にプログレスバーを表示します。
    /// UI スレッドがビジーになっている場合はプログレスバーを表示することができません。
    /// </remarks>
    public partial class SimpleProgressForm : Form
    {
        private TimeSpan ProgressShowDelay = new TimeSpan(0, 0, 0, 1, 500);  // 少し待ってからプログレスバーを表示する
        private Form _parentForm;
        private bool _parentFormEnabled = true;

        /// <summary>コンストラクタ</summary>
        public SimpleProgressForm()
        {
            InitializeComponent();

            if (!this.DesignMode) 
            {
                this.Disposed += SimpleProgressForm_Disposed;

                // 時間がかかっているときだけ表示するため初期状態は非表示
                this.Visible = false;

                showingDelayTimer.Interval = (int)ProgressShowDelay.TotalMilliseconds;
            }
        }

        public void Start(Form form)
        {
            if (form != null) 
            {
                _parentFormEnabled = form.Enabled;
                _parentForm = form;
                _parentForm.Enabled = false;
                _parentForm.Show();
            }

            showingDelayTimer.Start();
        }

        private void showingDelayTimer_Tick(object sender, EventArgs e)
        {
            showingDelayTimer.Stop();
            ShowProgress(_parentForm);
        }


        private void SimpleProgressForm_Disposed(object sender, EventArgs e)
        {
            if (_parentForm != null)
            {
                _parentForm.Enabled = _parentFormEnabled;
                _parentForm.Focus();
            }
        }

        private void ShowProgress(Form form) 
        {
            if (form != null && form.Modal)
            {
#if !DEBUG
                this.TopMost = true;
#endif
                this.Show();
            }
            else
            {
                this.Show(form);
            }

            CenteringToParent();
            this.Invalidate();
        }

        /// <summary>
        /// 親フォームの中央に移動します
        /// </summary>
        private void CenteringToParent()
        {
            if (_parentForm == null)
            {
                return;
            }
            else
            {
                int left = _parentForm.Location.X + (_parentForm.Width - this.Width) / 2;
                int top = _parentForm.Location.Y + (_parentForm.Height - this.Height) / 2;

                this.Location = new System.Drawing.Point(left, top);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls;
using HatFClient.Extensions;
using HatFClient.Repository;
using HatFClient.Soap;

namespace HatFClient.Views.Order
{
    /// <summary>受発注検索画面</summary>
    public partial class JH_Search_Jyutyu : Form
    {
        private readonly List<string> ListLayOut = new();       //リストレイアウト
        private DataTable DtLayOut = new();                     //データテーブルレイアウト
        private DataTable DtList = new();                       //FlexGrid用データテーブル
        private DataTable dtHeader_jhSearch = new();            // 基本情報
        private DataTable dtDetail_jhSearch = new();            // 明細情報
        private const int IntShowMaxConunt = 200;               // 表示最大件数
        private const string StrNote = @"※「発注方法」に★マークがついている行は、一貫化Ｖ３で作成されたものです。";
        /// <summary>確認なしでフォームを閉じる</summary>
        private bool _forceClose = false;

        /* 引継用 */
        private JH_Main _jh_Main_Search;

        public JH_Main JH_Main_Search
        {
            get { return _jh_Main_Search; }
            set { _jh_Main_Search = value; }
        }

        /* チームコード */
        private string _jh_Main_TeamCd;

        public string JH_Main_TeamCd
        {
            get { return _jh_Main_TeamCd; }
            set { _jh_Main_TeamCd = value; }
        }

        /* メイン画面データ有無 */
        private bool _jh_Main_DataExist;
        public bool JH_Main_DataExist
        {
            get { return _jh_Main_DataExist; }
            set { _jh_Main_DataExist = value; }
        }

        private FosJyuchuRepo jyuchuRepo;
        private ClientRepo clientRepo;
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private HatF_HattyuJyoutaiRepo hattyuJyoutaiRepo;
        private HatF_OrderFlagRepo hatfOrderFlagRepo;

        public JH_Search_Jyutyu()
        {
            InitializeComponent();
        }

        private async void JH_Search_Jyutyu_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            this.jyuchuRepo = FosJyuchuRepo.GetInstance();
            this.clientRepo = ClientRepo.GetInstance();
            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.hattyuJyoutaiRepo = HatF_HattyuJyoutaiRepo.GetInstance();
            this.hatfOrderFlagRepo = HatF_OrderFlagRepo.GetInstance();

            ClearForm();

            SetTextBoxCharType();
            SetInputCheckOnFocusOut();

            SetLayOutData();            // FelxGrid 構成情報セット
            InitDrLayOutForGridCols();  // FlexGrid 表示項目情報セット
            SetGridDataName();          // FlexGrid DataTable 紐づけ
            SetGridCols();              // FlexGrid グリッド項目セット
            SetC1FlexGridStyleForRowBackColor(grdList);

            SetDataHeaderInit();        // ヘッダテーブル設定
            SetDataDetailInit();        // 明細テーブル設定

            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            this.lblNote.Text = StrNote;

            this.jyuchuRepo.SearchResults = null;
            ShowListData();
            SetConditionInit();
            ActiveControl = txtTEAM_CD;


            if (!await LoadFormDataAsync())
            {
                _forceClose = true;
                this.Close();
            }
        }

        /// <summary>画面表示時</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void JH_Search_Jyutyu_Shown(object sender, EventArgs e)
        {
            //// Loadイベントで検索すると検索画面より先にプログレスバーが表示されるため
            //// 検索画面表示直後に検索を開始する
            //if (!await LoadFormDataAsync())
            //{
            //     _formClosingWithError = true;
            //    this.Close();
            //}
        }

        /// <summary>確認なしでフォームを閉じる</summary>
        public void ForceClose()
        {
            _forceClose = true;
            Close();
        }

        #region << 検索実行 >>

        /// <summary>
        /// 画面のデータ読み込み（メソッド名称共通）
        /// </summary>
        private async Task<bool> LoadFormDataAsync()
        {
            /* 条件設定 */
            var param = GetSearchCondition();

            ApiResponse<List<FosJyuchuSearch>> apiResponse;

            // プログレスバーを表示する処理
            using (var progressForm = new SimpleProgressForm())
            {
                // この using ブロック内には、画面にアクセス(参照/設定)するコードを記述しないでください
                progressForm.Start(this);

                // 原則、APIアクセスのみ記述します
                apiResponse = await jyuchuRepo.SearchAsync(param);

                //// APIアクセスが同期メソッドの場合は Task.Run で囲ってください
                //await Task.Run(() =>
                //{
                //    // Program.HatFApiClient.Get();
                //});
            }

            // 通信処理後に成否判定
            if (false == ApiHelper.AfterDataFetchBehavior(this, apiResponse))
            {
                // 戻り値がfalseの場合はAPI呼出の失敗なので処理を切り上げます。
                return false;
            }

            // 画面更新
            ShowGridData();
            ShowListData();

            return true;
        }

        private FosJyuchuSearchCondition GetSearchCondition()
        {
            var param = new FosJyuchuSearchCondition();

            param.rows = IntShowMaxConunt;
            param.TeamCd = this.txtTEAM_CD.Text.Trim().Length.Equals(2) ? this.txtTEAM_CD.Text.Trim() : null;
            param.TokuCd = this.txtTOKUI_CD.Text.Trim().Length > 0 ? this.txtTOKUI_CD.Text.Trim() : null;
            param.KmanCd = this.txtKMAN_CD.Text.Trim().Length.Equals(1) ? this.txtKMAN_CD.Text.Trim() : null;
            param.ShiresakiCd = this.txtSHIRESAKI_CD.Text.Trim().Length.Equals(6) ? this.txtSHIRESAKI_CD.Text.Trim() : null;
            param.Jyu2 = this.txtJYU2.Text.Trim().Length.Equals(1) ? this.txtJYU2.Text.Trim() : null;
            param.Nyu2 = this.txtNYU2.Text.Trim().Length.Equals(1) ? this.txtNYU2.Text.Trim() : null;
            param.GenbaCd = this.txtGENBA_CD.Text.Trim().Length > 0 ? this.txtGENBA_CD.Text.Trim() : null;
            param.GenbaName = this.txtGENBA_NAME.Text.Trim().Length > 0 ? this.txtGENBA_NAME.Text.Trim() : null;
            param.CustOrderNo = this.txtCUST_ORDERNO.Text.Trim().Length > 0 ? this.txtCUST_ORDERNO.Text.Trim() : null;
            param.HatOrderNo = this.txtHAT_ORDER_NO.Text.Trim().Length > 0 ? this.txtHAT_ORDER_NO.Text.Trim() : null;
            param.Hkbn = this.txtHKBN.Text.Trim().Length.Equals(1) ? this.txtHKBN.Text.Trim() : null;
            param.DenNo = this.txtDEN_NO.Text.Trim().Length.Equals(6) ? this.txtDEN_NO.Text.Trim() : null;
            param.OrderNo = this.txtORDER_NO.Text.Trim().Length > 0 ? this.txtORDER_NO.Text.Trim() : null;
            param.State = this.txtDEL_FLG.Text.Trim().Length.Equals(1) ? this.txtDEL_FLG.Text.Trim() : null;
            param.SyohinCd = this.txtSYOHIN_CD.Text.Trim().Length > 0 ? this.txtSYOHIN_CD.Text.Trim() : null;
            param.SyohinName = this.txtSYOHIN_NAME.Text.Trim().Length > 0 ? this.txtSYOHIN_NAME.Text.Trim() : null;
            param.RecYmdFrom = this.dateREC_YMDFrom.Text.Trim().Length.Equals(8) ? (DateTime)this.dateREC_YMDFrom.Value : null;
            param.RecYmdTo = this.dateREC_YMDTo.Text.Trim().Length.Equals(8) ? (DateTime)this.dateREC_YMDTo.Value : null;
            param.NoukiFrom = this.dateNOUKIFrom.Text.Trim().Length.Equals(8) ? (DateTime)this.dateNOUKIFrom.Value : null;
            param.NoukiTo = this.dateNOUKITo.Text.Trim().Length.Equals(8) ? (DateTime)this.dateNOUKITo.Value : null;
            param.OrderFlag = this.txtORDER_FLAG.Text.Trim().Length.Equals(1) ? this.txtORDER_FLAG.Text.Trim() : null;
            param.OpsOrderNo = this.txtORDER_NO.Text.Trim().Length.Equals(6) ? this.txtORDER_NO.Text.Trim() : null;
            param.OpsRecYMDFrom = this.dateOPS_REC_YMDFrom.Text.Trim().Length.Equals(8) ? (DateTime)this.dateOPS_REC_YMDFrom.Value : null;
            param.OpsRecYMDTo = this.dateOPS_REC_YMDTo.Text.Trim().Length.Equals(8) ? (DateTime)this.dateOPS_REC_YMDTo.Value : null;
            param.EpukoKanriNo = this.txtEPUKO_KANRI_NO.Text.Trim().Length.Equals(12) ? this.txtEPUKO_KANRI_NO.Text.Trim() : null;

            return param;
        }

        #endregion << 検索実行 >>

        #region << 詳細画面 >>
        private async Task GoDetailAsync(bool boolNewFlg)
        {
            var saveKey = (string)grdList.GetData(grdList.RowSel, 17);
            int intDenSort = int.Parse(grdList.GetData(grdList.RowSel, 16).ToString());

            var pages = await ApiHelper.FetchAsync(this, () =>
            {
                return jyuchuRepo.GetPages(saveKey);
            });
            if (pages.Failed)
            {
                return;
            }

            _jh_Main_Search.SetDataSelectedPage(pages.Value, intDenSort);

            await _jh_Main_Search.ShowJH_MainAsync(boolNewFlg);
        }
        #endregion

        #region << ボタンイベント >>

        private async void BtnFnc01_Click(object sender, System.EventArgs e)
        {
            this.btnFnc01.Focus();

            if (grdList.Rows.Count <= 1) { return; }
            if (grdList.RowSel < 1) { return; }

            this.btnFnc11.Focus();
            if (!DialogHelper.YesNoQuestion(this, "新規コピーしますか？", true))
            {
                return;
            }
            else
            {
                if (_jh_Main_DataExist)
                {
                    if (!DialogHelper.YesNoQuestion(this, "入力内容を破棄しますか？", true))
                    {
                        return;
                    }
                }
            }
            await GoDetailAsync(true);
            this.Close();
        }

        /// <summary>F4:エプコ取込ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFnc04_Click(object sender, EventArgs e)
        {
            this.btnFnc04.Focus();

            if (string.IsNullOrEmpty(txtEPUKO_KANRI_NO.Text))
            {
                DialogHelper.WarningMessage(this, "エプコ管理番号は必須入力項目です。");
                return;
            }

            var soapClient = new SoapClient();
            var parameters = MakeRequestParameters(txtEPUKO_KANRI_NO.Text, "");

#if DEBUG
            // エプコへリクエストせず、ダミーの応答情報を使用する
            soapClient.UseDummyResponse = true;
            soapClient.DummyWsdl = DummyWsdl;
            soapClient.DummyResponse = DummyEpukoReturn;
#endif

            DataTable result;

            // プログレスバーを表示する処理
            using (var progressForm = new SimpleProgressForm())
            {
                // エプコから受注情報を取得する
                result = await soapClient.GetAsync(Properties.Settings.Default.EPUKO_URL, ApiResources.Epuko.ReturnEpukoOrder, parameters);
            }

            if (result is null || result.Rows.Count <= 0)
            {
                var message = soapClient.HasError ? soapClient.ErrorMessage : "送信処理中にエラーが発生しました。";
                DialogHelper.WarningMessage(this, message);
                return;
            }

            var pages = new FosJyuchuPage[((result.Rows.Count / 6) + (result.Rows.Count % 6 > 0 ? 1 : 0))].ToList();
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i] = new FosJyuchuPage();
            }
            SetEpukoHeaders(pages, result, txtEPUKO_KANRI_NO.Text);
            SetEpukoDetails(pages, result, txtEPUKO_KANRI_NO.Text);
            SetEpukoFooters(pages, result, txtEPUKO_KANRI_NO.Text);

            _jh_Main_Search.SetData(pages);

            //TODO: パラメータ true でよいか確認
            await _jh_Main_Search.ShowJH_MainAsync(true);

            Close();
        }

        private  async void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            this.btnFnc09.Focus();

            if (!DialogHelper.YesNoQuestion(this, "検索しますか？"))
            {
                return;
            }

            if (!BoolInputCondCheck()) { return; }   // 入力条件チェック

            await LoadFormDataAsync();
        }

        private void BtnFnc10_Click(object sender, System.EventArgs e)
        {
            this.btnFnc10.Focus();
            
            if (!DialogHelper.YesNoQuestion(this, "条件をクリアしますか？", true))
            {
                return;
            }

            ClearForm();
            ShowGridData();
            SetConditionInit();
            txtTEAM_CD.Focus();
        }
        private async void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            if (grdList.Rows.Count <= 1) { return; }
            if (grdList.RowSel < 1) { return; }

            this.btnFnc11.Focus();

            if (!DialogHelper.YesNoQuestion(this, "決定しますか？"))
            {
                return;
            }

            await GoDetailAsync(false);
            this.Close();
        }

        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            // 閉じる確認は FormClosing で行う
            this.Close();
        }

        private void JH_Search_Jyutyu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_forceClose)
            {
                // 確認なしで閉じる
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!DialogHelper.OkCancelQuestion(this, "本画面を閉じますか？", true))
                {
                    e.Cancel = true;
                }
            }
        }

        private async void BtnDelete(object sender, System.EventArgs e)
        {
            this.btnDelete.Focus();

            List<(string SaveKey, string DenSort)> targetList = new List<(string SaveKey, string DenSort)>();
            foreach (Row row in grdList.Rows)
            {
                if (row.Index > 0)
                {
                    if ((string)row["del"] == "True")
                    {
                        targetList.Add((
                            SaveKey: row["SaveKey"].ToString(),
                            DenSort: row["DenSort"].ToString()
                        ));
                    }
                }
            }

            if (targetList.Count == 0)
            {
                return;
            }

            if (!DialogHelper.YesNoQuestion(this, "選択した伝票を削除しても良いですか？", true))
            {
                return;
            }

            ApiResponse<int> apiResponse;

            // プログレスバーを表示する処理
            using (var progressForm = new SimpleProgressForm())
            {
                // 通信処理だけ囲う
                apiResponse = await jyuchuRepo.Delete(targetList);
            }

            // 楽観的排他ロックでエラー時のリロード処理を表す
            Func<Task> updateConflictReloadLogic = async () => await LoadFormDataAsync();

            // 処理状況結果メッセージを出し、楽観的排他ロックエラー時は画面再読み込みまでします
            if (false == await ApiHelper.AfterDataUpdateBehaviorAsync(this, apiResponse, updateConflictReloadLogic))
            {
                return;
            }
            //else 
            //{
            //    // データ更新成功で画面を閉じる場合
            //    this.Close();
            //}

            await LoadFormDataAsync();
        }

#endregion << ボタンイベント >>

        #region << ショートカット制御 >>

        private void JH_Search_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnFnc01.Enabled == true)
                        btnFnc01.PerformClick();
                    break;

                case Keys.F4:
                    if (btnFnc04.Enabled == true)
                        btnFnc04.PerformClick();
                    break;

                case Keys.F9:
                    if (btnFnc09.Enabled == true)
                        btnFnc09.PerformClick();
                    break;

                case Keys.F10:
                    if (btnFnc10.Enabled == true)
                        btnFnc10.PerformClick();
                    break;

                case Keys.F11:
                    if (btnFnc11.Enabled == true)
                        btnFnc11.PerformClick();
                    break;

                case Keys.F12:
                    if (btnFnc12.Enabled == true)
                        btnFnc12.PerformClick();
                    break;
            }
        }

        #endregion << ショートカット制御 >>

        #region << フォーム初期化（検索条件） >>

        private void ClearForm()
        {
            this.txtTEAM_CD.Clear();
            this.txtTOKUI_CD.Clear();
            this.txtKMAN_CD.Clear();
            this.txtSHIRESAKI_CD.Clear();
            this.txtJYU2.Clear();
            this.txtNYU2.Clear();
            this.txtEPUKO_KANRI_NO.Clear();
            this.txtGENBA_CD.Clear();
            this.txtGENBA_NAME.Clear();
            this.txtCUST_ORDERNO.Clear();
            this.txtHAT_ORDER_NO.Clear();
            this.txtHKBN.Clear();
            this.txtDEN_NO.Clear();
            this.txtORDER_NO.Clear();
            this.txtDEL_FLG.Clear();
            this.txtSYOHIN_CD.Clear();
            this.txtSYOHIN_NAME.Clear();
            this.dateREC_YMDFrom.Value = DateTime.Today;
            this.dateREC_YMDFrom.Clear();
            this.dateREC_YMDTo.Value = DateTime.Today;
            this.dateREC_YMDTo.Clear();
            this.dateNOUKIFrom.Value = DateTime.Today;
            this.dateNOUKIFrom.Clear();
            this.dateNOUKITo.Value = DateTime.Today;
            this.dateNOUKITo.Clear();
            this.txtORDER_FLAG.Clear();
            this.txtOPS_ORDER_NO.Clear();
            this.dateOPS_REC_YMDFrom.Value = DateTime.Today;
            this.dateOPS_REC_YMDFrom.Clear();
            this.dateOPS_REC_YMDTo.Value = DateTime.Today;
            this.dateOPS_REC_YMDTo.Clear();
        }

        private void SetConditionInit()
        {
            this.dateNOUKIFrom.Value = HatFComParts.GetDateAddMonth(DateTime.Now, -3);
            this.dateNOUKIFrom.Text = HatFComParts.DoFormatYYMMDD(this.dateNOUKIFrom.Value);
            this.dateNOUKITo.Value = DateTime.Now;
            this.dateNOUKITo.Text = HatFComParts.DoFormatYYMMDD(this.dateNOUKITo.Value);
            this.txtTEAM_CD.Text = _jh_Main_TeamCd;
        }

        #endregion << フォーム初期化（検索条件） >>

        #region << 入力文字制限 >>

        private void SetTextBoxCharType()
        {
            this.txtTEAM_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);
            this.txtTOKUI_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);
            this.txtSHIRESAKI_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);
            this.txtDEN_NO.KeyPress += new KeyPressEventHandler(TextBoxNumOnly_KeyPress);
        }

        private void TextBoxCharType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumAlphabet(e.KeyChar);
        }

        private void TxtBox1Char_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strLine = "";
            switch (((TextBoxCharSize1)sender).Name)
            {
                case nameof(txtHKBN):        //発注方法
                    strLine = "013456";
                    break;

                case nameof(txtDEL_FLG):     //状態
                    strLine = "0123456";
                    break;

                case nameof(txtORDER_FLAG):  //受注区分
                    strLine = "012345678";
                    break;
            }
            bool bolFlg = false;
            if (e.KeyChar == '\b')
            {
                bolFlg = true;
            }
            else
            {
                for (int i = 0; i < strLine.Length; i++)
                {
                    if (e.KeyChar.ToString().ToUpper().Equals(strLine.Substring(i, 1)))
                    {
                        bolFlg = true;
                        break;
                    }
                }
            }
            if (bolFlg == false) { e.Handled = true; }
        }

        private void TextBoxNumOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumOnly(e.KeyChar);
        }

        private void TxtDEN_NO_Validated(object sender, EventArgs e)
        {
            txtDEN_NO.Text = HatFComParts.DoZeroFill(txtDEN_NO.Text, 6);
        }

        #endregion << 入力文字制限 >>

        #region << 入力チェック（フォーカスアウト） >>

        private void IsInputCheckFocusOut_Validated(object sender, System.EventArgs e)
        {
            ((System.Windows.Forms.TextBox)sender).Font = new Font(((System.Windows.Forms.TextBox)sender).Font, FontStyle.Regular);
            HatFComParts.InitMessageArea(lblNote);
            this.lblNote.Text = StrNote;
            if (((System.Windows.Forms.TextBox)sender).Text.Length == 0) { return; }

            bool boolChk = true;
            string strId = @"";

            switch (((System.Windows.Forms.TextBox)sender).Name)
            {
                case nameof(txtJYU2):
                case nameof(txtNYU2):
                case nameof(txtTEAM_CD):
                case nameof(txtTOKUI_CD):
                case nameof(txtSHIRESAKI_CD):
                    strId = @"FO002";
                    break;

                case nameof(txtKMAN_CD):
                case nameof(txtGENBA_CD):
                    strId = @"FO004";
                    break;

                default:
                    break;
            }
            if (strId != @"")
            {
                switch (strId)
                {
                    case @"FO002":
                        boolChk = HatFComParts.BoolIsAlphabetNumByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        break;

                    case @"FO004":
                        boolChk = HatFComParts.BoolIsHalfByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        break;

                    case @"FO006":
                        boolChk = HatFComParts.BoolIsHalfByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        if (boolChk) { boolChk = HatFComParts.BoolIsChar4Or7(((System.Windows.Forms.TextBox)sender).Text); }
                        else { strId = @"FO004"; }
                        break;
                }
            }
            if (!boolChk)
            {
                ((System.Windows.Forms.TextBox)sender).Font = new Font(((System.Windows.Forms.TextBox)sender).Font, FontStyle.Bold);
                HatFComParts.ShowMessageAreaError(lblNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId));
                ((System.Windows.Forms.TextBox)sender).Focus();
            }
            else
            {
                HatFComParts.InitMessageArea(lblNote);
                this.lblNote.Text = StrNote;
            }
        }

        private void SetInputCheckOnFocusOut()
        {
            // 半角英大文字数字チェック
            this.txtJYU2.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtNYU2.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtTEAM_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtTOKUI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtSHIRESAKI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            // 半角チェック
            this.txtKMAN_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtGENBA_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            // 半角チェック+4or7
            this.txtHAT_ORDER_NO.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
        }

        #endregion << 入力チェック（フォーカスアウト） >>

        #region << 入力チェック（検索ボタン） >>

        private bool BoolInputCondCheck()
        {
            DateTime? DateFrom;
            DateTime? DateTo;
            DateFrom = HatFComParts.DoParseDateTime(dateREC_YMDFrom.Value);
            DateTo = HatFComParts.DoParseDateTime(dateREC_YMDTo.Value);
            if (DateFrom != null && DateTo != null)
            {
                if (DateFrom > DateTo)
                {
                    DialogHelper.WarningMessage(this, $"自至が不正です{Environment.NewLine}受注日");
                    return false;
                }
            }
            DateFrom = HatFComParts.DoParseDateTime(dateNOUKIFrom.Value);
            DateTo = HatFComParts.DoParseDateTime(dateNOUKITo.Value);
            if (DateFrom != null && DateTo != null)
            {
                if (DateFrom > DateTo)
                {
                    DialogHelper.WarningMessage(this, $"自至が不正です{Environment.NewLine}納期");
                    return false;
                }
            }
            DateFrom = HatFComParts.DoParseDateTime(dateOPS_REC_YMDFrom.Value);
            DateTo = HatFComParts.DoParseDateTime(dateOPS_REC_YMDTo.Value);
            if (DateFrom != null && DateTo != null)
            {
                if (DateFrom > DateTo)
                {
                    DialogHelper.WarningMessage(this, $"自至が不正です{Environment.NewLine}OPS／OCR入力日");
                    return false;
                }
            }
            return true;
        }

        #endregion << 入力チェック（検索ボタン） >>

        #region << グリッド >>

        private void SetLayOutData()
        {
            // ID,Caption,DataName,Width,Format,TextAlign,Flg,Sort
            ListLayOut.Add("1,削除,del,40,,M,1,1");
            ListLayOut.Add("2,発注方法,HkbnDesc,100,,L,1,1");
            ListLayOut.Add("3,伝票No,DenNo,70,,M,1,1");
            ListLayOut.Add("4,内部No,Dseq,70,,M,1,1");
            ListLayOut.Add("5,得意先,TokuiCd,70,,M,1,1");
            ListLayOut.Add("6,客先注番,CustOrderno,120,,L,1,1");
            ListLayOut.Add("7,現場,GenbaCd,50,,M,1,1");
            ListLayOut.Add("8,仕入先,ShiresakiCd,70,,M,1,1");
            ListLayOut.Add("9,受注日,RecYmd,90,d,M,1,1");
            ListLayOut.Add("10,HAT注番,HatOrderNo,80,,M,1,1");
            ListLayOut.Add("11,受注者名,Jyu2Name,120,,L,1,1");
            ListLayOut.Add("12,状態,StateDesc,140,,L,1,1");
            ListLayOut.Add("13,受注区分,OrderFlagDesc,80,,L,1,1");
            ListLayOut.Add("14,OPSNo,OpsOrderNo,70,,M,1,1");
            ListLayOut.Add("15,OPS/OCR\r\n入力日,OpsRecYmd,90,d,M,1,1");
            ListLayOut.Add("16,希望納期,Nouki,90,d,M,1,1");
            ListLayOut.Add("17,頁,Page,30,,R,1,1");
            ListLayOut.Add("18,SaveKey,SaveKey,0,,M,1,1");
            ListLayOut.Add("19,DenNoLine,DenNoLine,0,,M,0,1");
            ListLayOut.Add("20,State,State,0,,M,0,1");
            ListLayOut.Add("21,DenSort,DenSort,0,,M,0,1");
        }

        private void InitDrLayOutForGridCols()
        {
            DtLayOut.Columns.Add("Id", typeof(int));
            DtLayOut.Columns.Add("Caption", typeof(string));
            DtLayOut.Columns.Add("DataName", typeof(string));
            DtLayOut.Columns.Add("Width", typeof(int));
            DtLayOut.Columns.Add("Format", typeof(string));
            DtLayOut.Columns.Add("TextAlign", typeof(string));
            DtLayOut.Columns.Add("Flg", typeof(int));
            DtLayOut.Columns.Add("Sort", typeof(int));
            DtLayOut.Columns.Add("SortNull", typeof(int));

            DataRow DrLayOut;
            for (int i = 0; i < ListLayOut.Count; i++)
            {
                string[] arrList = ListLayOut[i].Split(',');
                DrLayOut = DtLayOut.NewRow();
                DrLayOut["Id"] = int.Parse(arrList[0]);
                DrLayOut["Caption"] = arrList[1];
                DrLayOut["DataName"] = arrList[2];
                DrLayOut["Width"] = int.Parse(arrList[3]);
                DrLayOut["Format"] = arrList[4];
                DrLayOut["TextAlign"] = arrList[5];
                DrLayOut["Flg"] = int.Parse(arrList[6]);
                DrLayOut["SortNull"] = 1;
                if (int.TryParse(arrList[7], out int intSort))
                {
                    DrLayOut["Sort"] = intSort;
                    DrLayOut["SortNull"] = 0;
                }
                DtLayOut.Rows.Add(DrLayOut);
            }
        }

        private void SetGridDataName()
        {
            DataView dv = new DataView(DtLayOut);
            dv.Sort = "SortNull,Sort";
            DataTable result = dv.ToTable();
            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
            {
                if (rowIndex == 0)
                {
                    DtList.Columns.Add(result.Rows[rowIndex]["DataName"].ToString(), typeof(bool));
                }
                else
                {
                    DtList.Columns.Add(result.Rows[rowIndex]["DataName"].ToString(), typeof(string));
                }
            }
        }

        private void SetGridCols()
        {
            DataView dv = new DataView(DtLayOut);
            dv.Sort = "SortNull,Sort";
            DataTable result = dv.ToTable();

            grdList.Cols.Fixed = 0;
            grdList.Cols.Count = result.Rows.Count;
            grdList.Rows[0].Height = 50;
            grdList.AllowFiltering = true;      //フィルタ設定
            grdList.AllowEditing = true;        //編集可

            for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
            {
                if ((rowIndex) != 0)
                {
                    grdList.Cols[rowIndex].AllowEditing = false;    //編集不可
                }
                else
                {
                    grdList.Cols[rowIndex].DataType = typeof(bool);
                }
                grdList.Cols[rowIndex].Caption = result.Rows[rowIndex]["Caption"].ToString();
                grdList.Cols[rowIndex].Name = result.Rows[rowIndex]["DataName"].ToString();
                grdList.Cols[rowIndex].Width = (int)result.Rows[rowIndex]["Width"];
                grdList.Cols[rowIndex].Format = result.Rows[rowIndex]["Format"].ToString();
                switch (result.Rows[rowIndex]["TextAlign"].ToString())
                {
                    case "L":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                        break;

                    case "M":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        break;

                    case "R":
                        grdList.Cols[rowIndex].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        grdList.Cols[rowIndex].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        break;

                    default:
                        break;
                }
                if ((int)result.Rows[rowIndex]["Flg"] == 1)
                {
                    grdList.Cols[rowIndex].Visible = true;
                }
                else
                {
                    grdList.Cols[rowIndex].Visible = false;
                }
            }
        }

        private void ShowGridData()
        {
            dtHeader_jhSearch.Clear();
            dtDetail_jhSearch.Clear();
            DataView dv = new(dtHeader_jhSearch);
            DataTable result = dv.ToTable();
            grdList.ClearFilter();
            grdList.SetDataBinding(result, "", true);
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnFnc11.Enabled == true)
            {
                btnFnc11.PerformClick();
            }
        }

        private void GrdList_CellChecked(object sender, RowColEventArgs e)
        {
            var state = grdList.Rows[e.Row]["State"].ToString();
            if (state == "3")
            {
                grdList.Rows[e.Row]["del"] = false;
                DialogHelper.WarningMessage(this, "状態が「ACOS済」のデータは削除できません。");
            }
            else if (state == "4")
            {
                grdList.Rows[e.Row]["del"] = false;
                DialogHelper.WarningMessage(this, "状態が「削除」のデータは削除できません。");
            }
        }

        #endregion << グリッド >>

        #region << グリッド(行背景色変更) >>

        private const int IntConstC1FlexGridColNo = 19;

        private void SetC1FlexGridStyleForRowBackColor(C1FlexGrid grdName)
        {
            this.hattyuJyoutaiRepo.Entities.ForEach(x =>
            {
                string strColor = x.Color;
                string strStyle = x.Style;
                Color colColor = Color.FromName(strColor);
                grdName.Styles.Add(strStyle);
                grdName.Styles[strStyle].BackColor = colColor;
            });
            grdName.Styles.Add("StyleDefaultRowColor");
            grdName.Styles["StyleDefaultRowColor"].BackColor = Color.White;
        }

        private void SetC1FlexGridRowBackColor(C1FlexGrid grdName)
        {
            for (int i = grdName.Rows.Fixed; i < grdName.Rows.Count; i++)
            {
                string strVal = grdName.GetData(i, IntConstC1FlexGridColNo).ToString();
                var varStyle = hattyuJyoutaiRepo.Entities.Find(opt => opt.Key.Equals(strVal));
                if (varStyle != null)
                {
                    grdName.Rows[i].Style = grdName.Styles[varStyle.Style];
                }
            }
        }

        private void C1FlexGrid_BeforeSort(object sender, SortColEventArgs e)
        {
            for (int i = ((C1FlexGrid)sender).Rows.Fixed; i < ((C1FlexGrid)sender).Rows.Count; i++)
            {
                ((C1FlexGrid)sender).Rows[i].Style = ((C1FlexGrid)sender).Styles["StyleDefaultRowColor"];
            }
        }

        private void C1FlexGrid_AfterSort(object sender, SortColEventArgs e)
        {
            SetC1FlexGridRowBackColor((C1FlexGrid)sender);
        }

        #endregion << グリッド(行背景色変更) >>

        #region << データテーブル処理（グリッド用） >>

        private void SetDataHeaderInit()
        {
            dtHeader_jhSearch.Columns.Add("Jyu2", typeof(string));            // 受発注者
            dtHeader_jhSearch.Columns.Add("Nyu2", typeof(string));            // 入力者
            dtHeader_jhSearch.Columns.Add("Jyu2Cd", typeof(string));            // 受発注者
            dtHeader_jhSearch.Columns.Add("Nyu2Cd", typeof(string));            // 入力者
            dtHeader_jhSearch.Columns.Add("OrderFlag", typeof(string));     // 受注区分
            dtHeader_jhSearch.Columns.Add("OpsOrderNo", typeof(string));        // OPSNo.
            dtHeader_jhSearch.Columns.Add("EstimateNo", typeof(string));        // 見積番号
            dtHeader_jhSearch.Columns.Add("Dseq", typeof(string));          // 内部番号
            dtHeader_jhSearch.Columns.Add("DseqTime", typeof(string));      // 内部番号時刻
            dtHeader_jhSearch.Columns.Add("DelFlg", typeof(string));      // 状態
            dtHeader_jhSearch.Columns.Add("DenFlg", typeof(string));            // 伝票区分
            dtHeader_jhSearch.Columns.Add("DenNo", typeof(string));         // 伝票番号
            dtHeader_jhSearch.Columns.Add("TeamCd", typeof(string));           // 販課
            dtHeader_jhSearch.Columns.Add("TokuiCd", typeof(string));           // 得意先ＣＤ
            dtHeader_jhSearch.Columns.Add("KmanCd", typeof(string));            // 担
            dtHeader_jhSearch.Columns.Add("KmanName", typeof(string));      // 担名称
            dtHeader_jhSearch.Columns.Add("OkuriFlag", typeof(string));     // 送元
            dtHeader_jhSearch.Columns.Add("TokuiName", typeof(string));     // 得意先名
            dtHeader_jhSearch.Columns.Add("SokoCd", typeof(string));            // 倉庫
            dtHeader_jhSearch.Columns.Add("GenbaCd", typeof(string));           // 現場
            dtHeader_jhSearch.Columns.Add("Bincd", typeof(string));         // 扱便
            dtHeader_jhSearch.Columns.Add("Unchin", typeof(string));            // 運賃
            dtHeader_jhSearch.Columns.Add("Nohin", typeof(string));         // 区分
            dtHeader_jhSearch.Columns.Add("Nouki", typeof(string));         // 納日
            dtHeader_jhSearch.Columns.Add("CustOrderno", typeof(string));       // 客注
            dtHeader_jhSearch.Columns.Add("NoteHouse", typeof(string));     // 社内備考
            dtHeader_jhSearch.Columns.Add("Hkbn", typeof(string));          // 発注区分
            dtHeader_jhSearch.Columns.Add("ShiresakiCd", typeof(string));       // 仕入先ＣＤ
            dtHeader_jhSearch.Columns.Add("ShiresakiName", typeof(string)); // 仕入先名
            dtHeader_jhSearch.Columns.Add("Sirainm", typeof(string));           // 依頼
            dtHeader_jhSearch.Columns.Add("Sfax", typeof(string));          // ＦＡＸ
            dtHeader_jhSearch.Columns.Add("HatNyukabi", typeof(string));	// 入荷日
            dtHeader_jhSearch.Columns.Add("HatOrderNo", typeof(string));        // ＨＡＴ注番
            dtHeader_jhSearch.Columns.Add("TelRenrakuFlg", typeof(string)); // 電話連絡済
            dtHeader_jhSearch.Columns.Add("Kessai", typeof(string));            // 決済
            dtHeader_jhSearch.Columns.Add("Raikan", typeof(string));            // 来勘
            dtHeader_jhSearch.Columns.Add("KoujitenCd", typeof(string));        // 工事店ＣＤ
            dtHeader_jhSearch.Columns.Add("KoujitenName", typeof(string));  // 工事店名
            dtHeader_jhSearch.Columns.Add("AnswerName", typeof(string));        // 回答者
            dtHeader_jhSearch.Columns.Add("RecvPostcode", typeof(string));  // 〒
            dtHeader_jhSearch.Columns.Add("RecvAdd1", typeof(string));      // 住所1
            dtHeader_jhSearch.Columns.Add("RecvAdd2", typeof(string));      // 住所2
            dtHeader_jhSearch.Columns.Add("RecvAdd3", typeof(string));      // 住所3
            dtHeader_jhSearch.Columns.Add("RecvTel", typeof(string));           // TEL
            dtHeader_jhSearch.Columns.Add("RecvName1", typeof(string));     // 宛先1
            dtHeader_jhSearch.Columns.Add("RecvName2", typeof(string));     // 宛先2
            dtHeader_jhSearch.Columns.Add("OrderMemo1", typeof(string));        // 発注時メモ

            dtHeader_jhSearch.Columns.Add("Jyu2Name", typeof(string));            // 受発注者
            dtHeader_jhSearch.Columns.Add("OrderFlagDesc", typeof(string));     // 受注区分
            dtHeader_jhSearch.Columns.Add("Page", typeof(string));            // 頁
            dtHeader_jhSearch.Columns.Add("del", typeof(string));            // 削除
            dtHeader_jhSearch.Columns.Add("HkbnDesc", typeof(string));          // 発注区分
            dtHeader_jhSearch.Columns.Add("RecYmd", typeof(string));          //
            dtHeader_jhSearch.Columns.Add("OpsRecYmd", typeof(string));          //

            dtHeader_jhSearch.Columns.Add("SaveKey", typeof(string));          // PK1
            dtHeader_jhSearch.Columns.Add("DenSort", typeof(string));          // PK2

            dtHeader_jhSearch.Columns.Add("State", typeof(string));     // 発注状態
            dtHeader_jhSearch.Columns.Add("StateDesc", typeof(string)); // 発注状態名
        }

        private void SetDataDetailInit()
        {
            dtDetail_jhSearch.Columns.Add("OrderState", typeof(string));
            dtDetail_jhSearch.Columns.Add("SaveKey", typeof(string));
            dtDetail_jhSearch.Columns.Add("DenSort", typeof(string));
            dtDetail_jhSearch.Columns.Add("DenNoLine", typeof(string));
            dtDetail_jhSearch.Columns.Add("SyobunCd", typeof(string));
            dtDetail_jhSearch.Columns.Add("SyohinCd", typeof(string));
            dtDetail_jhSearch.Columns.Add("SyohinName", typeof(string));
            dtDetail_jhSearch.Columns.Add("Urikubn", typeof(string));
            dtDetail_jhSearch.Columns.Add("Suryo", typeof(string));
            dtDetail_jhSearch.Columns.Add("Tani", typeof(string));
            dtDetail_jhSearch.Columns.Add("Bara", typeof(string));
            dtDetail_jhSearch.Columns.Add("TeiTan", typeof(string));
            dtDetail_jhSearch.Columns.Add("Nouki", typeof(string));
            dtDetail_jhSearch.Columns.Add("SiiAnswTan", typeof(string));
            dtDetail_jhSearch.Columns.Add("UriKigou", typeof(string));
            dtDetail_jhSearch.Columns.Add("UriKake", typeof(string));
            dtDetail_jhSearch.Columns.Add("UriTan", typeof(string));
            dtDetail_jhSearch.Columns.Add("SiiKigou", typeof(string));
            dtDetail_jhSearch.Columns.Add("SiiKake", typeof(string));
            dtDetail_jhSearch.Columns.Add("SiiTan", typeof(string));
            dtDetail_jhSearch.Columns.Add("Lbiko", typeof(string));
            dtDetail_jhSearch.Columns.Add("TaxFlg", typeof(string));
        }

        private void ShowListData()
        {
            DataRow dr;

            if (this.jyuchuRepo.SearchResults == null)
            {
                return;
            }

            this.jyuchuRepo.SearchResults.ForEach(x =>
            {
                dr = dtHeader_jhSearch.NewRow();
                dr["del"] = false;
                dr["Hkbn"] = x.Hkbn;
                var hkbnDesc = this.clientRepo.Options.DivHachus.ToList().Find(op => op.Code.Equals(x.Hkbn));
                if (hkbnDesc != null) {
                    dr["HkbnDesc"] = hkbnDesc.Name;
                }
                else
                {
                    dr["HkbnDesc"] = "*" + x.Hkbn;
                }
                dr["DenNo"] = x.DenNo;
                dr["Dseq"] = x.Dseq;
                dr["TokuiCd"] = x.TokuiCd;
                dr["CustOrderno"] = x.CustOrderno;
                dr["GenbaCd"] = x.GenbaCd;
                dr["ShiresakiCd"] = x.ShiresakiCd;

                // TODO: DivShiresakisを参照している箇所を削除し代替処理が必要
                // 仕入先は数万件に及ぶためClientInitとして取得するのは間違っている
                System.Diagnostics.Debugger.Break();    // 強制ブレークポイント
                var shiiresaki = this.clientRepo.Options.DivShiresakis.ToList().Find(op => op.Code.Equals(x.ShiresakiCd));
                if (shiiresaki != null)
                {
                    dr["ShiresakiName"] = shiiresaki.Name;
                }
                else
                {
                    dr["ShiresakiName"] = "*" + x.ShiresakiCd;
                }

                dr["RecYmd"] = HatFComParts.DoFormatYYMMDD(x.RecYmd);
                dr["HatOrderNo"] = x.HatOrderNo;
                var jyu2name = this.clientRepo.Options.DivEmployee.ToList().Find(op => op.Code.Equals(x.Jyu2Cd));
                if (jyu2name != null)
                {
                    dr["Jyu2Name"] = jyu2name.Name;
                }
                else
                {
                    dr["Jyu2Name"] = "*" + x.Jyu2Cd;
                }

                string strState = x.State; //  HatFComParts.GetHattyuJyoutai(x.DelFlg, x.IchuFlg, x.UkeshoFlg,x.DenState);
                dr["State"] = strState;
                var delFlgDesc = this.hattyuJyoutaiRepo.Entities.Find(op => op.Key.Equals(strState));
                if (delFlgDesc != null)
                {
                    dr["StateDesc"] = delFlgDesc.Name.ToString();
                }
                else
                {
                    dr["StateDesc"] = "??";
                }

                dr["OrderFlag"] = x.OrderFlag;
                var orderflagdesc = this.hatfOrderFlagRepo.Entities.Find(op => op.Key.ToString() == x.OrderFlag);
                if (orderflagdesc != null)
                {
                    dr["OrderFlagDesc"] = orderflagdesc.Name;
                }
                else
                {
                    dr["OrderFlagDesc"] = "*" + x.OrderFlag;
                }
                dr["OpsOrderNo"] = x.OpsOrderNo;
                dr["OpsRecYmd"] = HatFComParts.DoFormatYYMMDD(x.OpsRecYmd);
                dr["Nouki"] = HatFComParts.DoFormatYYMMDD(x.Nouki);
                dr["Page"] = x.DenSort;

                dr["SaveKey"] = x.SaveKey;
                dr["DenSort"] = x.DenSort;

                dr["DelFlg"] = x.DelFlg;

                dtHeader_jhSearch.Rows.Add(dr);
            });

            DataView dv = new(dtHeader_jhSearch);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);
            SetC1FlexGridRowBackColor(grdList);
        }

        #endregion << データテーブル処理（グリッド用） >>

        #region <<エプコ取込>>

        private void SetEpukoHeaders(List<FosJyuchuPage> pages, DataTable epuko, string epukoNo)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                var row = epuko.Rows[i * 6];

                pages[i].FosJyuchuH.DenSort = (i + 1).ToString();
                pages[i].FosJyuchuH.OrderState = "0";
                pages[i].FosJyuchuH.DenState = "0";

                // パナホームの場合
                if (epukoNo.StartsWith("PNH"))
                {
                    // 発注者
                    pages[i].FosJyuchuH.Jyu2 = "E";
                    // 入力者
                    pages[i].FosJyuchuH.Nyu2 = "E";

                    // エプコ管理番号の末尾が「3、4」の場合
                    if (epukoNo.EndsWith("3") || epukoNo.EndsWith("4"))
                    {
                        // 伝区
                        pages[i].FosJyuchuH.DenFlg = "11";
                        // 倉庫(コード)
                        pages[i].FosJyuchuH.SokoCd = "44";
                        // 扱便(コード)
                        pages[i].FosJyuchuH.Bincd = "423";
                        // 納入区分
                        pages[i].FosJyuchuH.Nohin = "2";
                    }
                    else
                    {
                        // 伝区
                        pages[i].FosJyuchuH.DenFlg = "21";

                        // 倉庫(コード)
                        pages[i].FosJyuchuH.SokoCd = "07";

                        // 扱便(コード)
                        pages[i].FosJyuchuH.Bincd = "000";
                        // 納入区分
                        pages[i].FosJyuchuH.Nohin = "0";
                        // HAT注番
                        pages[i].FosJyuchuH.HatOrderNo = row.GetString(EpukoParameter.OrderInquiryResponseOrderNumber);
                    }

                    // 販課
                    pages[i].FosJyuchuH.TeamCd = "35";
                    // 得意
                    pages[i].FosJyuchuH.TokuiCd = row.GetString(EpukoParameter.OrderInquiryResponsePartyID);
                    // 担
                    pages[i].FosJyuchuH.KmanCd = "S";
                    // 現場
                    pages[i].FosJyuchuH.GenbaCd = row.GetString(EpukoParameter.OrderInquiryResponseSellersCD);
                    // 運賃区分
                    pages[i].FosJyuchuH.Unchin = "1";
                    // 客注
                    pages[i].FosJyuchuH.CustOrderno = row.GetString(EpukoParameter.OrderInquiryResponseID);

                    // エプコ管理番号の末尾が「2」の場合
                    if (epukoNo.EndsWith("2"))
                    {
                        // 発注方法
                        pages[i].FosJyuchuH.Hkbn = "1";
                        // 仕入先コード
                        pages[i].FosJyuchuH.ShiresakiCd = "479M02";
                        // 仕入先依頼者
                        pages[i].FosJyuchuH.Sirainm = "ご担当者";
                    }
                    else if (!epukoNo.EndsWith("3") && !epukoNo.EndsWith("4"))
                    {
                        // 発注方法
                        pages[i].FosJyuchuH.Hkbn = row.GetString(EpukoParameter.OrderInquiryResponseOrder);
                        // 仕入先コード
                        pages[i].FosJyuchuH.ShiresakiCd = row.GetString(EpukoParameter.OrderInquiryResponseSupplier);
                        // 仕入先依頼者
                        pages[i].FosJyuchuH.Sirainm = "ご担当者";
                    }
                }
                else
                {
                    // TODO ★現在ログインしている人のTypeCdが入る
                    pages[i].FosJyuchuH.Jyu2 = "";
                    pages[i].FosJyuchuH.Nyu2 = "";
                    // 受区
                    pages[i].FosJyuchuH.OrderFlag = "6";
                    // 伝区
                    pages[i].FosJyuchuH.DenFlg = row.GetString(EpukoParameter.OrderInquiryResponseTempID).SafeSubstring(0, 2);
                    // 販課
                    pages[i].FosJyuchuH.TeamCd = row.GetString(EpukoParameter.OrderInquiryResponseCID);
                    // 得意
                    pages[i].FosJyuchuH.TokuiCd = row.GetString(EpukoParameter.OrderInquiryResponsePartyID);
                    // 倉庫(コード)
                    pages[i].FosJyuchuH.SokoCd = row.GetString(EpukoParameter.OrderInquiryResponseWarehouseCD);
                    // 現場
                    pages[i].FosJyuchuH.GenbaCd = row.GetString(EpukoParameter.OrderInquiryResponseSellersCD);
                    // 扱便(コード)
                    pages[i].FosJyuchuH.Bincd = row.GetString(EpukoParameter.OrderInquiryResponseTerms);
                    // 指定着納期
                    var requestedDateString = row.GetString(EpukoParameter.OrderInquiryResponseRequestedDate);
                    var requestedDate = HatFComParts.DoParseDateTime(requestedDateString);
                    // 納日
                    if (pages[i].FosJyuchuH.DenFlg == "21")
                    {
                        pages[i].FosJyuchuH.Nouki = requestedDate;
                    }
                    // 客注
                    pages[i].FosJyuchuH.CustOrderno = row.GetString(EpukoParameter.OrderInquiryResponseID);
                    // 入荷
                    if (pages[i].FosJyuchuH.DenFlg == "15")
                    {
                        pages[i].FosJyuchuH.HatNyukabi = requestedDate;
                    }
                    // エプコ管理番号が「-3」以外の場合
                    if (!epukoNo.EndsWith("-3"))
                    {
                        // 運賃区分
                        pages[i].FosJyuchuH.Unchin = row.GetString(EpukoParameter.OrderInquiryResponseFare);
                        // 納入区分
                        pages[i].FosJyuchuH.Nohin = row.GetString(EpukoParameter.OrderInquiryResponseKbn);
                    }

                    // エプコ管理番号が「-2」、「-3」の場合
                    if (epukoNo.EndsWith("-2") || epukoNo.EndsWith("-3"))
                    {
                        // 発注方法
                        pages[i].FosJyuchuH.Hkbn = row.GetString(EpukoParameter.OrderInquiryResponseOrder);
                        // 仕入先コード
                        pages[i].FosJyuchuH.ShiresakiCd = row.GetString(EpukoParameter.OrderInquiryResponseSupplier);
                        // HAT注番
                        pages[i].FosJyuchuH.HatOrderNo = row.GetString(EpukoParameter.OrderInquiryResponseOrderNumber);
                    }

                    // エプコ管理番号が「-3」の場合
                    if (epukoNo.EndsWith("-3"))
                    {
                        // 仕入先依頼者
                        pages[i].FosJyuchuH.Sirainm = row.GetString(EpukoParameter.OrderInquiryResponseCommission);
                    }
                }
            }
        }

        private void SetEpukoDetails(List<FosJyuchuPage> pages, DataTable epuko, string epukoNo)
        {
            for (int i = 0; i < epuko.Rows.Count; i++)
            {
                var row = epuko.Rows[i];
                var page = pages[i / 6];
                page.FosJyuchuDs = page.FosJyuchuDs ?? new List<HAT_F_api.Models.FosJyuchuD>();
                var detail = new HAT_F_api.Models.FosJyuchuD();

                detail.DenSort = page.FosJyuchuH.DenSort;
                detail.DenNoLine = (i + 1).ToString();
                // パナホームの場合
                if (epukoNo.StartsWith("PNH"))
                {
                    if (!epukoNo.EndsWith("3") && !epukoNo.EndsWith("4"))
                    {
                        // 子
                        detail.Chuban = ((i % 6) + 1.0).ToString();
                        // TODO ★detail.KoBan = "1";
                        // エプコ管理番号の末尾が「2」の場合
                        if (epukoNo.EndsWith("2"))
                        {
                            // 販　単価
                            detail.UriTan = 3800m;
                            // 仕　単価
                            detail.SiiTan = 3100m;
                        }
                        else
                        {
                            // 販　単価
                            detail.UriTan = HatFComParts.DoParseDecimal(row.GetString(EpukoParameter.OrderInquiryResponseSellingUnitPrice));
                        }
                        // 商品コード・名称
                        detail.SyohinCd = row.GetString(EpukoParameter.OrderInquiryResponseSellersItemIdentification);

                        // エプコ管理番号の末尾が「3、4、2」の場合
                        if (epukoNo.EndsWith("3") || epukoNo.EndsWith("4") || epukoNo.EndsWith("2"))
                        {
                            // 数量
                            detail.Suryo = int.Parse(row.GetString(EpukoParameter.OrderInquiryResponseQuantity));
                        }
                        else
                        {
                            // 数量
                            detail.Suryo = 1;
                            // 分類マスター（SYOBUNM）を検索
                            string strBunCd = string.Empty;
                            // TODO 分類マスタAPI/api/inquiry-order/getSyobunを実行する
                            // var dtSyobun = await GetSyobunData(stru[iPageCount].OrderInputH.ShiresakiCdH);
                            var dtSyobun = new DataTable();
                            if (dtSyobun?.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtSyobun.Rows.Count; j++)
                                {
                                    if (dtSyobun.Rows[j].GetString("CODE5").EndsWith("320"))
                                    {
                                        strBunCd = "320";
                                    }
                                    else if (dtSyobun.Rows[j].GetString("CODE5").EndsWith("451"))
                                    {
                                        strBunCd = string.IsNullOrEmpty(strBunCd) ? "451" : string.Empty;
                                    }
                                }
                            }

                            // 分類
                            detail.SyobunCd = strBunCd;
                        }
                    }
                }
                else
                {
                    // 商品コード・名称
                    detail.SyohinCd = row.GetString(EpukoParameter.OrderInquiryResponseSellersItemIdentification);
                    // 数量
                    detail.Suryo = int.Parse(row.GetString(EpukoParameter.OrderInquiryResponseQuantity));
                    // 販　単価
                    detail.UriTan = HatFComParts.DoParseDecimal(row.GetString(EpukoParameter.OrderInquiryResponseSellingUnitPrice));
                }

                page.FosJyuchuDs.Add(detail);
                // stru[iPageCount].DetailNum = iDenNoLineDCount;
            }
        }

        private void SetEpukoFooters(List<FosJyuchuPage> pages, DataTable epuko, string epukoNo)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                var row = epuko.Rows[i * 6];
                // 〒
                pages[i].FosJyuchuH.RecvPostcode = row.GetString(EpukoParameter.OrderInquiryResponsePostalZone);
                // 住所
                int nextCutIndex = 0;
                var recvAdd = row.GetString(EpukoParameter.OrderInquiryResponseAddressLine1);
                // 住所１
                pages[i].FosJyuchuH.RecvAdd1 = recvAdd.SubStringByte(0, 30, ref nextCutIndex);
                // 住所２
                pages[i].FosJyuchuH.RecvAdd2 = recvAdd.SubStringByte(nextCutIndex, 12, ref nextCutIndex);
                // TEL
                pages[i].FosJyuchuH.RecvTel = row.GetString(EpukoParameter.OrderInquiryResponseConsigneeContactTEL);

                // 宛先
                nextCutIndex = 0;
                var recvName = row.GetString(EpukoParameter.OrderInquiryResponseConsigneeContactName);
                // 宛先１
                pages[i].FosJyuchuH.RecvName1 = recvName.SubStringByte(0, 30, ref nextCutIndex);
                // 宛先２
                pages[i].FosJyuchuH.RecvName2 = recvName.SubStringByte(nextCutIndex, 20, ref nextCutIndex);

                // TODO ★エプコ管理番号
                //pages[i].FosJyuchuHRecv.EpukoKanriNo = epuko.Rows[epukoIndex].GetString(EpukoParameter.OrderInquiryResponseBuyersCD);
            }
        }

        /// <summary>リクエストパラメータのXMLを作成する</summary>
        /// <param name="epukoKanriNo">エプコ管理番号</param>
        /// <param name="shainNo">社員番号</param>
        /// <returns>リクエストパラメータXML</returns>
        private string MakeRequestParameters(string epukoKanriNo, string shainNo)
        {
            var otherParameters = new[]
            {
                // 邸コードAs String =As String =>OrderInquiryResponse.受注番号
                EpukoParameter.OrderInquiryResponseID,
                // 伝票区分As String =As String =>OrderInquiryResponse.仮受注番号
                EpukoParameter.OrderInquiryResponseTempID,
                // チームAs String =As String =>OrderInquiryResponse.受注者管理番号
                EpukoParameter.OrderInquiryResponseCID,
                // 入荷日As String =As String =>OrderInquiryResponse.指定着納期
                EpukoParameter.OrderInquiryResponseRequestedDate,
                // 送り先郵便番号As String =As String =>OrderInquiryResponse.納入先郵便番号
                EpukoParameter.OrderInquiryResponsePostalZone,
                // 送り先住所As String =As String =>OrderInquiryResponse.納入先住所１
                EpukoParameter.OrderInquiryResponseAddressLine1,
                // 邸番名称As String =As String =>OrderInquiryResponse.納入先名称
                EpukoParameter.OrderInquiryResponseName,
                // 便As String =As String =>OrderInquiryResponse.納入条件
                EpukoParameter.OrderInquiryResponseTerms,
                // 送り先電話番号As String =As String =>OrderInquiryResponse.連絡先電話番号
                EpukoParameter.OrderInquiryResponseConsigneeContactTEL,
                // 宛先As String =As String =>OrderInquiryResponse.荷受人名称
                EpukoParameter.OrderInquiryResponseConsigneeContactName,
                // 現場As String =As String =>OrderInquiryResponse.受注者側現場コード
                EpukoParameter.OrderInquiryResponseSellersCD,
                // エプコ管理番号As String =As String =>OrderInquiryResponse.発注者側現場コード
                EpukoParameter.OrderInquiryResponseBuyersCD,
                // 倉庫As String =As String =>OrderInquiryResponse.引取倉庫コード
                EpukoParameter.OrderInquiryResponseWarehouseCD,
                // 得意先コードAs String =As String =>OrderInquiryResponse.転売先コード
                EpukoParameter.OrderInquiryResponsePartyID,
                // 得意先名称As String =As String =>OrderInquiryResponse.転売先名称
                EpukoParameter.OrderInquiryResponsePartyName,
                // 数量As String =As String =>OrderInquiryResponse.入力数量
                EpukoParameter.OrderInquiryResponseQuantity,
                // ＨＡＴ商品コードAs String =As String =>OrderInquiryResponse.商品コード・名称
                EpukoParameter.OrderInquiryResponseSellersItemIdentification,
                // 納入日As String =As String =>OrderInquiryResponse.指定着納期
                EpukoParameter.OrderInquiryResponseDate,
                // 売単価As String =As String =>OrderInquiryResponse.回答単価
                EpukoParameter.OrderInquiryResponseSellingUnitPrice,
                // 売金額As String =As String =>OrderInquiryResponse.回答金額
                EpukoParameter.OrderInquiryResponseSellingPrice,
                // 運賃As String =As String =>OrderInquiryResponse.Fare
                EpukoParameter.OrderInquiryResponseFare,
                // 区分As String =As String =>OrderInquiryResponse.Kbn
                EpukoParameter.OrderInquiryResponseKbn,
                // 発注欄As String =As String =>OrderInquiryResponse.Order
                EpukoParameter.OrderInquiryResponseOrder,
                // 仕入先As String =As String =>OrderInquiryResponse.Supplier
                EpukoParameter.OrderInquiryResponseSupplier,
                // 依頼As String =As String =>OrderInquiryResponse.Commission
                EpukoParameter.OrderInquiryResponseCommission,
                // 注番As String =As String =>OrderInquiryResponse.OrderNumber
                EpukoParameter.OrderInquiryResponseOrderNumber,
            };
            var otherParameter = string.Join(Environment.NewLine, otherParameters.Select(p => XmlHelper.String(p)));

            return string.Join(string.Empty, new[]
            {
                // メッセージバージョン番号
                XmlHelper.Tag("strVerNo", EpukoParameter.OrderResultVerNo),
                // メッセージID
                XmlHelper.Tag("strGuId", EpukoParameter.OrderResultInquiryGuId),
                // メッセージ詳細ID
                XmlHelper.Tag("strGudId", EpukoParameter.OrderResultGudId01),
                // メッセージ区別
                XmlHelper.Tag("strMessMode", 9),
                // セッションID
                XmlHelper.Tag("strSessID", EpukoParameter.OrderResultSessID),
                // セッション.シーケンス番号
                XmlHelper.Tag("strSessSeqNO", EpukoParameter.OrderResultSessSeqNO),
                // メッセージ継続区別
                XmlHelper.Tag("strMessContinue", EpukoParameter.OrderResultMessContinue),
                // メッセージ応答種別
                XmlHelper.Tag("strMessResp", EpukoParameter.OrderResultMessResp),
                // トランザクションID
                XmlHelper.Tag("strTranID", EpukoParameter.OrderResultTranID),
                // 送信処理日付
                XmlHelper.Tag("strTransmitDate", EpukoParameter.OrderResultTransmitDate),
                // 受信処理時間
                XmlHelper.Tag("strTransmitTime", EpukoParameter.OrderResultTransmitTime),
                // 受信処理日付
                XmlHelper.Tag("strReceiveDate", string.Empty),
                // 受信処理時間
                XmlHelper.Tag("strReceiveTime", string.Empty),
                // 発注者コード
                XmlHelper.Tag("strBuyersPartyId", string.Empty),
                // 受注者コード
                XmlHelper.Tag("strSellersPartyId", string.Empty),
                // 受注者発行パスワード
                XmlHelper.Tag("strSellersPassword", string.Empty),
                // 発注者発行パスワード
                XmlHelper.Tag("strBuyersPassword", string.Empty),
                // 発注者口座コード(エプコ管理番号)
                XmlHelper.Tag("strBuyersAccountID", epukoKanriNo),
                // 受注番号
                XmlHelper.Tag("strSellersID", string.Empty),
                // 問合せ項目
                XmlHelper.Tag("strOtherParamer", otherParameter),
                // 社員コード
                XmlHelper.Tag("strShainCd", shainNo),
            });
        }

        #region ダミーXML

#if DEBUG

        /// <summary>ダミーのWSDLレスポンスを返却する</summary>
        /// <returns>ダミーのWSDLレスポンス</returns>
        private string DummyWsdl()
        {
            return @"
<wsdl:definitions xmlns:s=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://schemas.xmlsoap.org/wsdl/soap12/"" xmlns:http=""http://schemas.xmlsoap.org/wsdl/http/"" xmlns:mime=""http://schemas.xmlsoap.org/wsdl/mime/"" xmlns:tns=""http://HAT-WS.com/"" xmlns:soap=""http://schemas.xmlsoap.org/wsdl/soap/"" xmlns:tm=""http://microsoft.com/wsdl/mime/textMatching/"" xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:wsdl=""http://schemas.xmlsoap.org/wsdl/"" targetNamespace=""http://HAT-WS.com/"">
<wsdl:types>
<s:schema elementFormDefault=""qualified"" targetNamespace=""http://HAT-WS.com/"">
<s:element name=""ReturnOrderInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCustermerIndexCD"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:complexType name=""ArrayOfString"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""unbounded"" name=""string"" nillable=""true"" type=""s:string""/>
</s:sequence>
</s:complexType>
<s:element name=""ReturnOrderInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnOrderInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:complexType name=""HATResult"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ErrMsg"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""Tresponses"" type=""tns:ArrayOfTResponse""/>
</s:sequence>
</s:complexType>
<s:complexType name=""ArrayOfTResponse"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""unbounded"" name=""TResponse"" nillable=""true"" type=""tns:TResponse""/>
</s:sequence>
</s:complexType>
<s:complexType name=""TResponse"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""Asks"" type=""tns:ArrayOfAsk""/>
</s:sequence>
</s:complexType>
<s:complexType name=""ArrayOfAsk"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""unbounded"" name=""Ask"" nillable=""true"" type=""tns:Ask""/>
</s:sequence>
</s:complexType>
<s:complexType name=""Ask"">
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""Name"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""Value"" type=""s:string""/>
</s:sequence>
</s:complexType>
<s:element name=""ReturnOrderDecisionInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXSendingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXReceivingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strFormID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOrderNOReportID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersContactID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToPostalZone"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCountrySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCitySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToName"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToTel"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strResalePartyID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersNote"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnOrderDecisionInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnOrderDecisionInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnOrderConfirmationInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXSendingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXReceivingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strFormID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOrderNOReportID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersContactID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToPostalZone"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCountrySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCitySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToName"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToTel"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strResalePartyID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersNote"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnOrderConfirmationInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnOrderConfirmationInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnPanasonicOrderDecisionInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersContactID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToPostalZone"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToCountrySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToCtiySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToName"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strResalePartyID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNoteNote1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnPanasonicOrderDecisionInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnPanasonicOrderDecisionInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnQuoteInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strQuoteDocumentReference"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCommodityINFCD"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strRequestDivision"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strRequestDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strQuoteType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strEstimateNo"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnQuoteInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnQuoteInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnPanasonicProductsDecisionInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellerItemIdentification"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strLineNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnPanasonicProductsDecisionInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnPanasonicProductsDecisionInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnEpukoOrder"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnEpukoOrderResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnEpukoOrderResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnTotoOrderConfirmation"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXSendingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXReceivingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strFormID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOrderNOReportID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersContactID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToPostalZone"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCountrySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCitySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToName"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToTel"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strResalePartyID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersNote"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDiscountApplication"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTokujyu"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strYouto"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGenba"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnTotoOrderConfirmationResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnTotoOrderConfirmationResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnTotoOrderDecision"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXSendingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersMAXReceivingDetails"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strFormID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOrderNOReportID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersContactID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryType"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToPostalZone"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCountrySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCitySubentity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressLine2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToName"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToTel"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strResalePartyID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler1"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strImportantWholesaler2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersNote"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDiscountApplication"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTokujyu"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strYouto"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGenba"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnTotoOrderDecisionResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnTotoOrderDecisionResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnNebikiConfirmation"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDirectionsClassification"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strRetailCreditRate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDiscountRate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSeihinItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strHinbanItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDealershipPerson"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersItemCD"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strQuantity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnNebikiConfirmationResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnNebikiConfirmationResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnNebikiDecision"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDirectionsClassification"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strRetailCreditRate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDiscountRate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSeihinItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strHinbanItemLine"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDealershipPerson"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryToAddressID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo2"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSpotNameKana"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDeliveryRequestedDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersItemCD"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strQuantity"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnNebikiDecisionResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnNebikiDecisionResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnSessyouInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strNegotiationNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnSessyouInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnSessyouInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnProductsInfo"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellerItemIdentification"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strLineNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strShainCd"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strDenno"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnProductsInfoResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnProductsInfoResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnOrderInfoWithLogin"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strUserId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strVerNo"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGuId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strGudId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessMode"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSessSeqNO"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessContinue"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMessResp"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTranID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strTransmitTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveDate"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strReceiveTime"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPartyId"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersPassword"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strBuyersAccountID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strSellersID"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strCustermerIndexCD"" type=""s:string""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strOtherParamer"" type=""tns:ArrayOfString""/>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""strMethod"" type=""s:string""/>
</s:sequence>
</s:complexType>
</s:element>
<s:element name=""ReturnOrderInfoWithLoginResponse"">
<s:complexType>
<s:sequence>
<s:element minOccurs=""0"" maxOccurs=""1"" name=""ReturnOrderInfoWithLoginResult"" type=""tns:HATResult""/>
</s:sequence>
</s:complexType>
</s:element>
</s:schema>
</wsdl:types>
<wsdl:message name=""ReturnOrderInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderDecisionInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderDecisionInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderDecisionInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderDecisionInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderConfirmationInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderConfirmationInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderConfirmationInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderConfirmationInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnPanasonicOrderDecisionInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnPanasonicOrderDecisionInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnPanasonicOrderDecisionInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnPanasonicOrderDecisionInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnQuoteInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnQuoteInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnQuoteInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnQuoteInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnPanasonicProductsDecisionInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnPanasonicProductsDecisionInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnPanasonicProductsDecisionInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnPanasonicProductsDecisionInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnEpukoOrderSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnEpukoOrder""/>
</wsdl:message>
<wsdl:message name=""ReturnEpukoOrderSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnEpukoOrderResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnTotoOrderConfirmationSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnTotoOrderConfirmation""/>
</wsdl:message>
<wsdl:message name=""ReturnTotoOrderConfirmationSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnTotoOrderConfirmationResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnTotoOrderDecisionSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnTotoOrderDecision""/>
</wsdl:message>
<wsdl:message name=""ReturnTotoOrderDecisionSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnTotoOrderDecisionResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnNebikiConfirmationSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnNebikiConfirmation""/>
</wsdl:message>
<wsdl:message name=""ReturnNebikiConfirmationSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnNebikiConfirmationResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnNebikiDecisionSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnNebikiDecision""/>
</wsdl:message>
<wsdl:message name=""ReturnNebikiDecisionSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnNebikiDecisionResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnSessyouInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnSessyouInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnSessyouInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnSessyouInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnProductsInfoSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnProductsInfo""/>
</wsdl:message>
<wsdl:message name=""ReturnProductsInfoSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnProductsInfoResponse""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderInfoWithLoginSoapIn"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderInfoWithLogin""/>
</wsdl:message>
<wsdl:message name=""ReturnOrderInfoWithLoginSoapOut"">
<wsdl:part name=""parameters"" element=""tns:ReturnOrderInfoWithLoginResponse""/>
</wsdl:message>
<wsdl:portType name=""ServiceSoap"">
<wsdl:operation name=""ReturnOrderInfo"">
<wsdl:input message=""tns:ReturnOrderInfoSoapIn""/>
<wsdl:output message=""tns:ReturnOrderInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderDecisionInfo"">
<wsdl:input message=""tns:ReturnOrderDecisionInfoSoapIn""/>
<wsdl:output message=""tns:ReturnOrderDecisionInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderConfirmationInfo"">
<wsdl:input message=""tns:ReturnOrderConfirmationInfoSoapIn""/>
<wsdl:output message=""tns:ReturnOrderConfirmationInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicOrderDecisionInfo"">
<wsdl:input message=""tns:ReturnPanasonicOrderDecisionInfoSoapIn""/>
<wsdl:output message=""tns:ReturnPanasonicOrderDecisionInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnQuoteInfo"">
<wsdl:input message=""tns:ReturnQuoteInfoSoapIn""/>
<wsdl:output message=""tns:ReturnQuoteInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicProductsDecisionInfo"">
<wsdl:input message=""tns:ReturnPanasonicProductsDecisionInfoSoapIn""/>
<wsdl:output message=""tns:ReturnPanasonicProductsDecisionInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnEpukoOrder"">
<wsdl:input message=""tns:ReturnEpukoOrderSoapIn""/>
<wsdl:output message=""tns:ReturnEpukoOrderSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderConfirmation"">
<wsdl:input message=""tns:ReturnTotoOrderConfirmationSoapIn""/>
<wsdl:output message=""tns:ReturnTotoOrderConfirmationSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderDecision"">
<wsdl:input message=""tns:ReturnTotoOrderDecisionSoapIn""/>
<wsdl:output message=""tns:ReturnTotoOrderDecisionSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiConfirmation"">
<wsdl:input message=""tns:ReturnNebikiConfirmationSoapIn""/>
<wsdl:output message=""tns:ReturnNebikiConfirmationSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiDecision"">
<wsdl:input message=""tns:ReturnNebikiDecisionSoapIn""/>
<wsdl:output message=""tns:ReturnNebikiDecisionSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnSessyouInfo"">
<wsdl:input message=""tns:ReturnSessyouInfoSoapIn""/>
<wsdl:output message=""tns:ReturnSessyouInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnProductsInfo"">
<wsdl:input message=""tns:ReturnProductsInfoSoapIn""/>
<wsdl:output message=""tns:ReturnProductsInfoSoapOut""/>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderInfoWithLogin"">
<wsdl:input message=""tns:ReturnOrderInfoWithLoginSoapIn""/>
<wsdl:output message=""tns:ReturnOrderInfoWithLoginSoapOut""/>
</wsdl:operation>
</wsdl:portType>
<wsdl:binding name=""ServiceSoap"" type=""tns:ServiceSoap"">
<soap:binding transport=""http://schemas.xmlsoap.org/soap/http""/>
<wsdl:operation name=""ReturnOrderInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnOrderInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderDecisionInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnOrderDecisionInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderConfirmationInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnOrderConfirmationInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicOrderDecisionInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnPanasonicOrderDecisionInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnQuoteInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnQuoteInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicProductsDecisionInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnPanasonicProductsDecisionInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnEpukoOrder"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnEpukoOrder"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderConfirmation"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnTotoOrderConfirmation"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderDecision"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnTotoOrderDecision"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiConfirmation"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnNebikiConfirmation"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiDecision"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnNebikiDecision"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnSessyouInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnSessyouInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnProductsInfo"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnProductsInfo"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderInfoWithLogin"">
<soap:operation soapAction=""http://HAT-WS.com/ReturnOrderInfoWithLogin"" style=""document""/>
<wsdl:input>
<soap:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
</wsdl:binding>
<wsdl:binding name=""ServiceSoap12"" type=""tns:ServiceSoap"">
<soap12:binding transport=""http://schemas.xmlsoap.org/soap/http""/>
<wsdl:operation name=""ReturnOrderInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnOrderInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderDecisionInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnOrderDecisionInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderConfirmationInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnOrderConfirmationInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicOrderDecisionInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnPanasonicOrderDecisionInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnQuoteInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnQuoteInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnPanasonicProductsDecisionInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnPanasonicProductsDecisionInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnEpukoOrder"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnEpukoOrder"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderConfirmation"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnTotoOrderConfirmation"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnTotoOrderDecision"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnTotoOrderDecision"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiConfirmation"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnNebikiConfirmation"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnNebikiDecision"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnNebikiDecision"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnSessyouInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnSessyouInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnProductsInfo"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnProductsInfo"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
<wsdl:operation name=""ReturnOrderInfoWithLogin"">
<soap12:operation soapAction=""http://HAT-WS.com/ReturnOrderInfoWithLogin"" style=""document""/>
<wsdl:input>
<soap12:body use=""literal""/>
</wsdl:input>
<wsdl:output>
<soap12:body use=""literal""/>
</wsdl:output>
</wsdl:operation>
</wsdl:binding>
<wsdl:service name=""Service"">
<wsdl:port name=""ServiceSoap"" binding=""tns:ServiceSoap"">
<soap:address location=""http://10.7.2.1:88/Service.asmx""/>
</wsdl:port>
<wsdl:port name=""ServiceSoap12"" binding=""tns:ServiceSoap12"">
<soap12:address location=""http://10.7.2.1:88/Service.asmx""/>
</wsdl:port>
</wsdl:service>
</wsdl:definitions>
";
        }

        /// <summary>ダミーのSOAPレスポンスを返却する</summary>
        /// <returns>ダミーのSOAPレスポンス</returns>
        private string DummyEpukoReturn()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <soap:Body>
    <ReturnEpukoOrderResponse xmlns=""http://HAT-WS.com/"">
      <ReturnEpukoOrderResult>
        <ErrMsg />
        <Tresponses>
          <TResponse>
            <Asks>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.ID</Name>
                <Value>S1ｵｻﾞﾜｼﾝ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.TempID</Name>
                <Value>11</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.CID</Name>
                <Value>35</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.RequestedDate</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.PostalZone</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.AddressLine1</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Name</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Terms</Name>
                <Value>301</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactTEL</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.SellersCD</Name>
                <Value>375</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.BuyersCD</Name>
                <Value>CCS034631-1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Receipt.WarehouseCD</Name>
                <Value>44</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyID</Name>
                <Value>533701</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineItem.Quantity</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.Item.SellersItemIdentification</Name>
                <Value>MS KSA50HJ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.RequestedDelivery.Date</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingUnitPrice</Name>
                <Value>2760</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingPrice</Name>
                <Value>2760</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Fare</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Kbn</Name>
                <Value>2</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Order</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Supplier</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Commission</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.OrderNumber</Name>
                <Value />
              </Ask>
              <Ask xsi:nil=""true"" />
            </Asks>
          </TResponse>
          <TResponse>
            <Asks>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.ID</Name>
                <Value>S1ｵｻﾞﾜｼﾝ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.TempID</Name>
                <Value>11</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.CID</Name>
                <Value>35</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.RequestedDate</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.PostalZone</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.AddressLine1</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Name</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Terms</Name>
                <Value>301</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactTEL</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.SellersCD</Name>
                <Value>375</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.BuyersCD</Name>
                <Value>CCS034631-1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Receipt.WarehouseCD</Name>
                <Value>44</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyID</Name>
                <Value>533701</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineItem.Quantity</Name>
                <Value>3</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.Item.SellersItemIdentification</Name>
                <Value>MS KSA75HJ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.RequestedDelivery.Date</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingUnitPrice</Name>
                <Value>3680</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingPrice</Name>
                <Value>11040</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Fare</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Kbn</Name>
                <Value>2</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Order</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Supplier</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Commission</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.OrderNumber</Name>
                <Value />
              </Ask>
              <Ask xsi:nil=""true"" />
            </Asks>
          </TResponse>
          <TResponse>
            <Asks>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.ID</Name>
                <Value>S1ｵｻﾞﾜｼﾝ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.TempID</Name>
                <Value>11</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.CID</Name>
                <Value>35</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.RequestedDate</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.PostalZone</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.AddressLine1</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Name</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Terms</Name>
                <Value>301</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactTEL</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.SellersCD</Name>
                <Value>375</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.BuyersCD</Name>
                <Value>CCS034631-1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Receipt.WarehouseCD</Name>
                <Value>44</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyID</Name>
                <Value>533701</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineItem.Quantity</Name>
                <Value>2</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.Item.SellersItemIdentification</Name>
                <Value>MS KSS45L50PHJ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.RequestedDelivery.Date</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingUnitPrice</Name>
                <Value>825</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingPrice</Name>
                <Value>1650</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Fare</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Kbn</Name>
                <Value>2</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Order</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Supplier</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Commission</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.OrderNumber</Name>
                <Value />
              </Ask>
              <Ask xsi:nil=""true"" />
            </Asks>
          </TResponse>
          <TResponse>
            <Asks>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.ID</Name>
                <Value>S1ｵｻﾞﾜｼﾝ</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.TempID</Name>
                <Value>11</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Sellers.CID</Name>
                <Value>35</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.RequestedDate</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.PostalZone</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.AddressLine1</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Name</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.Terms</Name>
                <Value>301</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactTEL</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.SellersCD</Name>
                <Value>375</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.SpotLocation.BuyersCD</Name>
                <Value>CCS034631-1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Receipt.WarehouseCD</Name>
                <Value>44</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyID</Name>
                <Value>533701</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Resale.PartyName</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineItem.Quantity</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.Item.SellersItemIdentification</Name>
                <Value>ﾄｳﾚ SH780-330ML</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.RequestedDelivery.Date</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingUnitPrice</Name>
                <Value>450</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingPrice</Name>
                <Value>450</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Fare</Name>
                <Value>1</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Kbn</Name>
                <Value>2</Value>
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Order</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Supplier</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.Commission</Name>
                <Value />
              </Ask>
              <Ask>
                <Name>OrderInquiryResponse.OrderResult.Delivery.OrderNumber</Name>
                <Value />
              </Ask>
              <Ask xsi:nil=""true"" />
            </Asks>
          </TResponse>
          <TResponse xsi:nil=""true"" />
        </Tresponses>
      </ReturnEpukoOrderResult>
    </ReturnEpukoOrderResponse>
  </soap:Body>
</soap:Envelope>
";
        }

#endif

        #endregion ダミーXML

        #endregion <<エプコ取込>>
    }
}

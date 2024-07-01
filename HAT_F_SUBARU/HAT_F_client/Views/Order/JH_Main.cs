using ClosedXML.Excel;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_client.Views.BlobStorage;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls;
using HatFClient.Extensions;
using HatFClient.Repository;
using HatFClient.ValueObject;
using HatFClient.Views.MasterSearch;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Order
{
    /// <summary>受発注画面</summary>
    public partial class JH_Main : Form
    {
        // TODO 暫定的にデフォルトOFF
        /// <summary>[商品コード・名称]チェックボックスの初期値</summary>
        private const bool UseSuggestDefault = false;

        private ClientRepo clientRepo;
        private LoginRepo loginRepo;
        private FosJyuchuRepo fosJyuchuRepo;

        private DataTable dtHeader_jhMain = new();  // 基本情報
        private DataTable dtDetail_jhMain = new();  // 明細情報
        private string strSaveKey = "";             // PK1
        private string strDenSort = "";             // PK2
        private string strRowNo = "";               // 明細選択行番号
        private string strDenSortCopy = "";         // 明細複写対象PK2
        private string strPageNoCopy = "";          // 明細複写対象頁番号
        private string strRowNoCopy = "";           // 明細複写対象行番号
        private int IntDenSort;                     // 検索引継PK2
        private string strDenSortAddPage = "";      // 頁追加時PK2
        private DataTable dtErrorList = new();      // エラーリスト
        private DataTable dtErrorListDetail = new();// エラーリスト明細用
        private int IntCheckPtn;                    // エラーチェック 1:確認 2:F11 3:F12

        /// <summary>倉庫コード</summary>
        private ValueHandler<string> _sokoCode = new ValueHandler<string>();

        /// <summary>明細行コントロール</summary>
        private List<JH_Main_Detail> _detailRows;

        #region << マスタ用データテーブル >>
        private HatF_ErrorMessageFocusOutRepo hatfErrorMessageFocusOutRepo;
        private HatF_ErrorMessageButtonRepo hatfErrorMessageButtonRepo;
        private HatF_HattyuJyoutaiRepo _orderStateRepo;
        #endregion

        #region DataTableへのアクセス用プロパティ
        /// <summary>出荷指示書印刷済み</summary>
        private bool? DenShippingPrinted
        {
            get => GetDTNullableValue<bool>(getCurHeader(), nameof(FosJyuchuH.DenShippingPrinted));
            set => getCurHeader()[nameof(FosJyuchuH.DenShippingPrinted)] = (object)value ?? DBNull.Value;
        }

        /// <summary>DataTableから値型の値を取得する</summary>
        /// <typeparam name="T">取得する型</typeparam>
        /// <param name="row">DataRow</param>
        /// <param name="name">列名</param>
        /// <returns>値</returns>
        private T? GetDTNullableValue<T>(DataRow row, string name)
            where T : struct
        {
            var value = row[name];
            return value == DBNull.Value ? null : (T)value;
        }

        /// <summary>DataTableから参照型の値を取得する</summary>
        /// <typeparam name="T">取得する型</typeparam>
        /// <param name="row">DataRow</param>
        /// <param name="name">列名</param>
        /// <returns>値</returns>
        private T GetDTValue<T>(DataRow row, string name)
            where T : class
        {
            var value = row[name];
            return value == DBNull.Value ? null : (T)value;
        }
        #endregion

        public JH_Main()
        {
            InitializeComponent();

            // アプリ実行時か、GUIデザイナーからの呼び出しかを判定
            if (!this.DesignMode)
            {
                timerDSEQTime.Start();

                // すべてのテキストボックスでEnter時に次のコントロールに移動するよう設定
                foreach (var textbox in FormHelper.GetAllControls(this).OfType<TextBox>())
                {
                    textbox.KeyDown += (_, e) =>
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            this.SelectNextControl(textbox, true, true, true, true);
                        }
                    };
                }
                // 明細部分のフォーカスに応じて明細ラベルの書式を自動変更
                SetDetailLabelAutoColor();

                // ラジオボタンはチェック状態変化時に自動的にTabStopを変更しているので、TabStopが変化したタイミングで強制的にfalseに上書きする
                new List<RadioButton>() { radioHAT, radioHAT_EXCEPT }.ForEach(r =>
                {
                    r.TabStopChanged += (_, _) => r.TabStop = false;
                });

                _detailRows = new List<JH_Main_Detail>() { ucRow1, ucRow2, ucRow3, ucRow4, ucRow5, ucRow6 };
            }
        }

        private void JH_Main_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            // オプション情報取得
            this.clientRepo = ClientRepo.GetInstance();
            this.loginRepo = LoginRepo.GetInstance();
            this.fosJyuchuRepo = FosJyuchuRepo.GetInstance();

            this.hatfErrorMessageFocusOutRepo = HatF_ErrorMessageFocusOutRepo.GetInstance();
            this.hatfErrorMessageButtonRepo = HatF_ErrorMessageButtonRepo.GetInstance();
            this._orderStateRepo = HatF_HattyuJyoutaiRepo.GetInstance();

            SetDataHeaderInit();
            SetDataDetailInit();
            SetErrorListInit();

            SetCombo();
            SetF9Activate();
            SetMoveNextControl();
            SetTextBoxCharType();
            SetInputCheckOnFocusOut();
            SetCalcFooterSummaryAreaValidated();
            SetNextRowActivate();
            SetEditModeCng();
            SetErrorMessageEvent();

            JH_Main_FormSize();

            InitForm(0);
            InitNewForm();

            this.chkHeaderSyouhin.Checked = UseSuggestDefault;
            ActiveControl = txtJYU2;

            radioHAT.TabStop = false;
            radioHAT_EXCEPT.TabStop = false;
        }

        public async Task ShowJH_MainAsync(bool boolNewFlg)
        {
            dtHeader_jhMain.DefaultView.Sort = "SaveKey,DenSortInt";
            dtHeader_jhMain = dtHeader_jhMain.DefaultView.ToTable(true);
            dtDetail_jhMain.DefaultView.Sort = "SaveKey,DenSortInt,DenNoLine";
            dtDetail_jhMain = dtDetail_jhMain.DefaultView.ToTable(true);
            this.txtroFooterPageNo.Text = this.GetPageNoFromDenSort(IntDenSort).ToString();
            this.txtroFooterPageCount.Text = dtHeader_jhMain.Rows.Count.ToString();
            if (this.txtroFooterPageCount.Text.Equals("0")) { this.txtroFooterPageCount.Text = @"1"; }

            IntCheckPtn = 0;
            dtErrorList.Clear();
            dtErrorListDetail.Clear();

            InitControlStyles(this);
            this.chkHeaderBunrui.Checked = false;
            var headerSyohinChecked = chkHeaderSyouhin.Checked;
            this.chkHeaderSyouhin.Checked = false;
            using (new Scope(() => chkHeaderSyouhin.Checked = headerSyohinChecked))
            {
                if (boolNewFlg)
                {
                    SetNewSaveKey(@"");
                    dtHeader_jhMain.AsEnumerable().Select(r => r["DelFlg"] = 0).ToList();
                    //dtHeader_jhMain.AsEnumerable().Select(r => r["IchuFlg"] = 0).ToList();
                    dtHeader_jhMain.AsEnumerable().Select(r => r["UkeshoFlg"] = null).ToList();
                    dtHeader_jhMain.AsEnumerable().Select(r => r["DenState"] = 0).ToList();
                    SetOrderState(JHOrderState.PreOrder);
                }

                await ShowDataHeaderAsync();

                RowUpdateBegin();
                ClearFormDetail();
                ShowDetailPageData();
                SetNextRowActivateOnPageShow();

                CngEditModeHeader(this.txtroHattyuJyoutai.Text);
                CngEditModeHeaderDenFlg(this.txtroHattyuJyoutai.Text);
                CngEditModeDetail(this.txtroHattyuJyoutai.Text);
                CngEditModeDetailDenFlg(this.txtroHattyuJyoutai.Text);
                CngEditModeHeaderHKBN();

                if (txtroHattyuJyoutai.Text == JHOrderState.PreOrder)
                {
                    CngShowDetailParts(false);
                    this.btnFooterF7.Text = @"F7:詳細画面";
                }
                else
                {
                    CngShowDetailParts(true);
                    this.btnFooterF7.Text = @"F7:簡易画面";
                }

                RowUpdateEnd();

                this.chkHeaderSyouhin.Checked = true;   // (HAT-103) 20240305
                // フォーカス可能な、最初のコントロールにフォーカスを移動する
                SelectNextControl(this, true, true, true, true);
            }
        }
        private void InitForm(int iPtn)
        {

            if (iPtn == 0)
            {
                dtHeader_jhMain.Clear();
                var dtHeader = dtHeader_jhMain.NewRow();
                dtHeader["DenSort"] = @"1";
                dtHeader["DenSortInt"] = 1;

                dtHeader["OrderNo"] = DBNull.Value;
                dtHeader["OrderState"] = "0";

                dtHeader_jhMain.Rows.Add(dtHeader);
                dtHeader_jhMain.AcceptChanges();

                dtDetail_jhMain.Clear();
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["DenSort"] = @"1";
                dtHeader["DenSortInt"] = 1;
                dtDetail["DenNoLine"] = @"1";
                dtDetail_jhMain.Rows.Add(dtDetail);
                dtDetail_jhMain.AcceptChanges();

                strSaveKey = @"";
                strDenSort = @"1";
                strRowNo = @"1";
            }
            strDenSortCopy = @"";
            strPageNoCopy = @"";
            strRowNoCopy = @"";
            strDenSortAddPage = @"";

            RowUpdateBegin();

            ClearFormMain();
            ClearFormDetail();
            CngButtonStsDetail(false);

            CngShowDetailParts(false);
            this.btnFooterF7.Text = @"F7:詳細画面";

            this.txtroFooterPageNo.Text = @"1";
            this.txtroFooterPageCount.Text = @"1";
            ShowDetailPageData();
            RowUpdateEnd();

            this.btnFnc09.Enabled = false;
            this.btnDenbanCopy.Enabled = Properties.Settings.Default.personal_on == 1;

            this.txtJYU2.Focus();

        }

        private void JH_Main_FormSize()
        {
            int intWidth = Properties.Settings.Default.jh_main_width;
            if (intWidth == 0) { intWidth = 1287; }

            int intHeight = Properties.Settings.Default.jh_main_height;
            if (intHeight == 0) { intHeight = 968; }

            // ResizeKit が 2回動かないようにする
            this.Size = new Size(intWidth, intHeight);
        }

        private void JH_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ユーザー操作以外の、強制終了時や親ウィンドウ終了時は確認せずに終了する
            // デバッグ時は復帰しやすいようにする
#if !DEBUG
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }
#endif
            //if (MessageBox.Show(@"終了しますか？", @"終了", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            if (!DialogHelper.OkCancelQuestion(this, "終了しますか？"))
            {
                e.Cancel = true;
                return;
            }

            // 受注検索画面が開かれている場合は先に閉じる
            FormFactory.GetModelessFormCache<JH_Search_Jyutyu>()?.ForceClose();

            if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
            {
                Properties.Settings.Default.jh_main_width = (int)this.Width;
                Properties.Settings.Default.jh_main_height = (int)this.Height;
                Properties.Settings.Default.Save();
            }
        }
        private void InitNewForm()
        {
            FosJyuchuRepo.GetInstance().ClearPages();

            SetOrderState(JHOrderState.PreOrder);
            CngButtonStsMain(true);
            CngButtonStsDetail(false);
            CngEditModeHeader(this.txtroHattyuJyoutai.Text);
            CngEditModeHeaderDenFlg(this.txtroHattyuJyoutai.Text);
            CngEditModeDetail(this.txtroHattyuJyoutai.Text);
            CngEditModeDetailDenFlg(this.txtroHattyuJyoutai.Text);
            CngEditModeHeaderHKBN();
        }

        #region <<< メインファンクションボタン >>>
        private void BtnFnc01_Click(object sender, System.EventArgs e)
        {
            this.btnFnc01.Focus();
            if (!DialogHelper.YesNoQuestion(this, "新規入力しますか？"))
            {
                return;
            }
            InitForm(0);
            InitNewForm();
            this.chkHeaderSyouhin.Checked = UseSuggestDefault;
            this.txtJYU2.Focus();
        }

        /// <summary>F3:受注検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnFnc03_Click(object sender, EventArgs e)
        {
            this.btnFnc03.Focus();
            var searchForm = FormFactory.GetModelessForm<JH_Search_Jyutyu>();
            searchForm.JH_Main_Search = this;
            searchForm.JH_Main_DataExist = BoolIsChkStatusOnInit() || BoolChkDetailDataInput();
            searchForm.JH_Main_TeamCd = this.txtTEAM_CD.Text;
            if (searchForm.Visible)
            {
                searchForm.Activate();
            }
            else
            {
                searchForm.Show();
            }
            //using (JH_Search_Jyutyu JH_Search_Jyutyu = new() { JH_Main_Search = this })
            //{
            //    JH_Search_Jyutyu.JH_Main_DataExist = true;
            //    if (!BoolIsChkStatusOnInit() && !BoolChkDetailDataInput()) { JH_Search_Jyutyu.JH_Main_DataExist = false; }
            //    JH_Search_Jyutyu.JH_Main_TeamCd = this.txtTEAM_CD.Text;
            //    JH_Search_Jyutyu.ShowDialog();
            //}
        }

        private void BtnFnc05_Click(object sender, System.EventArgs e)
        {
            this.btnFnc05.Focus();
            SetDataHeader();

            using (var TmpDialog = new Temp_Save())
            {
                TmpDialog.jh_Main = this;
                TmpDialog.ShowDialog();
            }
        }
        private void BtnFnc06_Click(object sender, System.EventArgs e)
        {
            this.btnFnc06.Focus();

            using (Views.Search.U_Search U_Search = new())
            {
                U_Search.ShowDialog();
            }
        }
        private void BtnFnc07_Click(object sender, System.EventArgs e)
        {
            this.btnFnc07.Focus();

            using (Views.PriceBatch.PB_Main dlg = new())
            {
                if (DialogHelper.IsPositiveResult(dlg.ShowDialog()))
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        JH_Main_Detail jH_Main_Detail = GetUcName(i);
                        if (jH_Main_Detail.Enabled && BoolChkDetailDataInputRow(jH_Main_Detail))
                        {
                            if (dlg.BoolPriceBatchUri)
                            {
                                jH_Main_Detail.txtURI_KIGOU.Text = dlg.StrPriceBatchKigouUri;
                                jH_Main_Detail.decURI_KAKE.Text = dlg.StrPriceBatchPerUri;
                            }
                            if (dlg.BoolPriceBatchSii)
                            {
                                jH_Main_Detail.txtSII_KIGOU.Text = dlg.StrPriceBatchKigouSii;
                                jH_Main_Detail.decSII_KAKE.Text = dlg.StrPriceBatchPerSii;
                            }
                        }
                    }
                }
            }
        }
        private void BtnFnc08_Click(object sender, System.EventArgs e)
        {
            this.btnFnc08.Focus();
            MessageBox.Show(this.btnFnc08.Text);
        }
        private void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            ShowSerchWindow();
        }
        private void ForbtnFnc09_Enter(object sender, EventArgs e)
        {
            btnFnc09.Enabled = true;
        }
        private void ForbtnFnc09_Leave(object sender, EventArgs e)
        {
            btnFnc09.Enabled = false;
        }
        private void BtnFnc10_Click(object sender, System.EventArgs e)
        {
            this.btnFnc10.Focus();

            using (Views.Search.C_Search C_Search = new())
            {
                C_Search.ShowDialog();
            }
        }

        /// <summary>F11:受注確定ボタン</summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFnc11_Click(object sender, EventArgs e)
        {
            // 補完とバリデーション
            this.btnFnc11.Focus();
            if (!await CompleteAndValidateAsync(2))
            {
                return;
            }

            // 画面の項目をオブジェクトとして取得
            SetDataHeader();
            DelEmptyDataTableRecord();

            HatFComParts.InitMessageArea(txtroNote);

            // 確定
            var pages = await ApiHelper.FetchAsync(this, () =>
            {
                return CommitPagesAsync(getFosJyuchPages());
            });
            if (pages.Failed)
            {
                return;
            }

            // 再表示
            SetDataSelectedPage(pages.Value, IntDenSort);
            await ShowJH_MainAsync(false);

            // TODO: 「発注書」「注文書」の揺れを正しい方に統一
            if (MessageBox.Show(@"発注書を作成しますか？", @"発注書", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string denNo = this.txtDEN_NO.Text;
            string outputFileName = $"注文書_{denNo}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            //outputFileName = ExcelReportUtil.ToExcelReportTempFileName(outputFileName);

            // 初期化済ファイル保存ダイアログ(注文書用)を生成
            using (var dialog = ExcelReportUtil.CreateOrderFormSaveFileDialog(outputFileName))
            {
                // 「名前を付けて保存」ダイアログを表示
                DialogResult result = dialog.ShowDialog(this);
                if (DialogHelper.IsPositiveResult(result))
                {
                    outputFileName = dialog.FileName;
                    MakePurchaseOrder(outputFileName);
                    AppLauncher.OpenExcel(outputFileName);
                }
            }
        }

        /// <summary>F12:発注照合ボタン</summary>
        /// <param name="sender">イベント送信元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFnc12_Click(object sender, EventArgs e)
        {
            // 補完とバリデーション
            this.btnFnc12.Focus();

            if (!await CompleteAndValidateAsync(3))
            {
                return;
            }

            // 画面の項目をオブジェクトとして取得
            SetDataHeader();
            DelEmptyDataTableRecord();

            // 発注照合
            var pages = await ApiHelper.UpdateAsync(this, () =>
            {
                return CollationPagesAsync(getFosJyuchPages());
            });
            if (pages.Failed)
            {
                return;
            }

            // 再表示
            SetDataSelectedPage(pages.Value, IntDenSort);
            await ShowJH_MainAsync(false);
        }
        #endregion

        #region <<< メイン上部ボタン >>>
        private async void BtnKakunin_Click(object sender, System.EventArgs e)
        {
            this.btnKakunin.Focus();
            await CompleteAndValidateAsync(1);
        }

        private void BtnBlob_Click(object sender, System.EventArgs e)
        {
            this.btnBlob.Focus();
            if (txtDEN_NO.Text.Length == 6)
            {
                BlobStorageForm blobStorageForm = new BlobStorageForm();
                blobStorageForm.DenNo = txtDEN_NO.Text;
                blobStorageForm.Show();

            }
            else
            {
                MessageBox.Show("伝票番号を指定して下さい");
            }
        }
        private async void BtnClose_Click(object sender, System.EventArgs e)
        {
            if (!BoolIsChkStatusOnInit() && !BoolChkDetailDataInput())
            {
                // 閉じる確認は FormClosing で行う
                this.Close();
                return;
            }

            using (JH_Main_Save dlg = new())
            {
                DialogResult result = dlg.ShowDialog();
                if (DialogHelper.IsNegativeResult(result))
                {
                    // 閉じる確認は FormClosing で行う
                    this.Close();
                }
                else
                {
                    //if (MessageBox.Show(@"保存しますか？", @"保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    if (!DialogHelper.YesNoQuestion(this, "保存しますか？"))
                    {
                        return;
                    }

                    SetDataHeader();
                    DelEmptyDataTableRecord();
                    // 更新処理
                    var fosJyuchRepo = FosJyuchuRepo.GetInstance();
                    var pages = getFosJyuchPages();
                    FosJyuchuPages fosJyuchuPages = new FosJyuchuPages
                    {
                        Pages = pages,
                    };

                    var url = string.Format(ApiResources.HatF.Client.UpdateOrder, strSaveKey);
                    var savedPages = await ApiHelper.UpdateAsync(this, async () =>
                    {
                        return await Program.HatFApiClient.PutAsync<List<FosJyuchuPage>>(url, fosJyuchuPages);
                    });
                    if (savedPages.Failed)
                    {
                        return;
                    }

                    // 再表示
                    SetDataSelectedPage(savedPages.Value, IntDenSort);
                    await ShowJH_MainAsync(false);
                }
            }

        }

        /// <summary>H注番の発番ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnNewHatOrderNo_Click(object sender, EventArgs e)
        {
            // H注番が空白の場合は初期値設定
            if (string.IsNullOrEmpty(txtHAT_ORDER_NO.Text))
            {
                this.txtHAT_ORDER_NO.Text = InitialHatOrderNo();
                DialogHelper.WarningMessage(this, "得意先を表す記号を1文字追記後に再度実行してください。");
                return;
            }
            // 空白ではないが４文字未満
            if (txtHAT_ORDER_NO.Text.Length < 4)
            {
                DialogHelper.WarningMessage(this, "販課、受発注者、得意先記号を入力後に再度実行してください。");
                return;
            }
            // H注番が5文字以上の場合
            if ((txtHAT_ORDER_NO.Text.Length > 4) && !DialogHelper.YesNoQuestion(this, "H注番を新規に発行しますか？", true))
            {
                return;
            }
            var denFlg = cmbDEN_FLG.GetSelectedCode();
            var key = txtHAT_ORDER_NO.Text.SafeSubstring(0, 4);
            var hatOrderNo = await ApiHelper.FetchAsync(this, () =>
            {
                return Program.HatFApiClient.PostAsync<string>(ApiResources.HatF.Client.GetNextHatOrderNo, new Dictionary<string, object>()
                {
                    {nameof(key), key },
                    {nameof(denFlg), denFlg },
                }, null);
            });
            if (hatOrderNo.Successed)
            {
                txtHAT_ORDER_NO.Text = hatOrderNo.Value;
                txtHAT_ORDER_NO.Focus();
                btnNewHatOrderNo.Focus();
            }
        }

        /// <summary>Hat注文番号の初期値を取得</summary>
        /// <returns>Hat注文番号の初期値</returns>
        private string InitialHatOrderNo()
        {
            return $"{loginRepo.CurrentUser.TeamCode}{loginRepo.CurrentUser.EmployeeTag}";
        }

        #endregion

        #region <<< メイン下部ボタン >>>
        private void BtnClearAddress_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(@"クリアしますか？", @"住所・宛先", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.txtRECV_POSTCODE.Clear();
            this.txtRECV_ADD1.Clear();
            this.txtRECV_ADD2.Clear();
            this.txtRECV_ADD3.Clear();
            this.txtRECV_TEL.Clear();
            this.txtRECV_NAME1.Clear();
            this.txtRECV_NAME2.Clear();
        }
        private void BtnDenbanCopy_Click(object sender, System.EventArgs e)
        {
            this.btnDenbanCopy.Focus();

            string strDesc = @"伝票番号と客注番号をコピーしました。" + "\r\n";
            string strNote = Properties.Settings.Default.personal_note;
            strNote = strNote.Replace(@"[伝票番号]", this.txtDEN_NO.Text);
            strNote = strNote.Replace(@"[客先注番]", this.txtCUST_ORDERNO.Text);
            this.txtroNote.Text = strDesc + strNote;

            Clipboard.SetText(strNote);

            // ボタン押下時にtxtroNoteに表示して、次のメッセージが出るまでは表示し続ける
            //MessageBox.Show(@"コピーしました", @"伝番客注コピー");
            //this.txtroNote.Clear();
        }
        private void BtnOkurisaki_Click(object sender, System.EventArgs e)
        {
            this.btnOkurisaki.Focus();
            MessageBox.Show(this.btnOkurisaki.Text);
        }
        private void BtnUkesyoBikou_Click(object sender, System.EventArgs e)
        {
            this.btnUkesyoBikou.Focus();
            MessageBox.Show(this.btnUkesyoBikou.Text);
            MessageBox.Show(@"最大化した後、このボタン押下で画面サイズをデフォルトに戻す(TEST用)");
            Properties.Settings.Default.jh_main_width = 1287;
            Properties.Settings.Default.jh_main_height = 968;
            Properties.Settings.Default.Save();
        }
        private void BtnRenrakuJikou_Click(object sender, System.EventArgs e)
        {
            this.btnRenrakuJikou.Focus();
            MessageBox.Show(this.btnRenrakuJikou.Text);
        }
        #endregion

        #region <<< 明細ボタン >>>
        private async void BtnFooterLeft_Click(object sender, System.EventArgs e)
        {
            this.btnFooterLeft.Focus();
            if (int.Parse(this.txtroFooterPageNo.Text) < 2)
            {
                return;
            }
            if (this.txtroFooterPageNo.Text.Equals(this.txtroFooterPageCount.Text))
            {
                if (!BoolChkDetailDataInput())
                {
                    for (int i = 0; i < dtHeader_jhMain.Rows.Count; i++)
                    {
                        if (dtHeader_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort))
                        {
                            dtHeader_jhMain.Rows[i].Delete();
                            dtHeader_jhMain.AcceptChanges();
                            break;
                        }
                    }
                    for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
                    {
                        if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort))
                        {
                            dtDetail_jhMain.Rows[i].Delete();
                            dtDetail_jhMain.AcceptChanges();
                        }
                    }
                    this.txtroFooterPageCount.Text = (int.Parse(this.txtroFooterPageCount.Text) - 1).ToString();
                }
                else
                {
                    SetDataHeader();
                }
            }
            else
            {
                SetDataHeader();
            }
            this.txtroFooterPageNo.Text = @"1";
            await ShowDataHeaderAsync();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            SetNextRowActivateOnPageShow();
            RowUpdateEnd();
        }

        private async void BtnFooterRight_Click(object sender, System.EventArgs e)
        {
            this.btnFooterRight.Focus();
            if (int.Parse(this.txtroFooterPageNo.Text) == int.Parse(this.txtroFooterPageCount.Text))
            {
                return;
            }
            SetDataHeader();
            this.txtroFooterPageNo.Text = (int.Parse(this.txtroFooterPageCount.Text)).ToString();
            await ShowDataHeaderAsync();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            SetNextRowActivateOnPageShow();
            RowUpdateEnd();
        }
        private void BtnFooterTotal_Click(object sender, System.EventArgs e)
        {
            this.btnFooterTotal.Focus();

            if (dtDetail_jhMain.Rows.Count == 0) { return; }
            decimal decTeiSum = 0;
            decimal decUriSum = 0;
            decimal decSiiSum = 0;
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["Delflg"].ToString().Equals(@"1")) { continue; }    // (HAT-42) 20240309

                var suryo = HatFComParts.DoParseInt(dtDetail_jhMain.Rows[i]["Suryo"]);
                int intSuryo = (int)((suryo == null) ? 0 : suryo);

                //フッター.定価 Σ([行.定価単価]×[行.数量]）現在の伝票ページの定価合計額を表示する
                var tei = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["TeiTan"]);
                decimal decTei = (decimal)((tei == null) ? 0 : tei);
                decTeiSum += (decTei * intSuryo);

                //フッター.売額 Σ([行.売上単価]×[行.数量]）現在の伝票ページの売上合計額を表示する
                var uri = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["UriTan"]);
                decimal decUri = (decimal)((uri == null) ? 0 : uri);
                decUriSum += (decUri * intSuryo);

                //フッター.仕額 Σ([行.仕入単価]×[行.数量]）現在の伝票ページの仕入合計額を表示する
                var sii = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["SiiTan"]);
                decimal decSii = (decimal)((sii == null) ? 0 : sii);
                decSiiSum += (decSii * intSuryo);
            }
            //フッター.粗利 [フッター.売額]-[フッター.仕額]現在の伝票ページの粗利を表示する
            decimal decArari = decUriSum - decSiiSum;
            //フッター.利率 {[フッター.粗利]÷[フッター.売額]×100}※小数点第2位を四捨五入 現在の伝票ページの利率を表示する
            decimal decRiritu = 0;
            if (decUriSum != 0)
            {
                decRiritu = decArari / decUriSum * 100;
            }

            using (JH_Main_Detail_Total dlg = new())
            {
                dlg.StrTei = HatFComParts.DoFormatN0(decTeiSum);
                dlg.StrUri = HatFComParts.DoFormatN0(decUriSum);
                dlg.StrSii = HatFComParts.DoFormatN0(decSiiSum);
                dlg.StrArari = HatFComParts.DoFormatN0(decArari);
                dlg.StrRiritu = HatFComParts.DoFormatN1(decRiritu);
                switch (dlg.ShowDialog())
                {
                    case DialogResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>前頁ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFooterPreviousPage_Click(object sender, System.EventArgs e)
        {
            this.btnFooterPreviousPage.Focus();

            var currentPageNo = int.Parse(this.txtroFooterPageNo.Text);
            // 現在が1ページ目の場合は何もしない
            if (currentPageNo < 2)
            {
                return;
            }

            // 現在が最終ページで、明細部分に何も入力されていない場合、ページを削除する
            if (txtroFooterPageNo.Text == txtroFooterPageCount.Text &&
                strDenSort == strDenSortAddPage &&
                !BoolChkDetailDataInput())
            {
                for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
                {
                    if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort))
                    {
                        dtDetail_jhMain.Rows[i].Delete();
                        dtDetail_jhMain.AcceptChanges();
                    }
                }
                this.txtroFooterPageCount.Text = (int.Parse(this.txtroFooterPageCount.Text) - 1).ToString();
            }
            else
            {
                SetDataHeader();
            }

            DelEmptyDataTableRecord();
            this.txtroFooterPageNo.Text = (int.Parse(this.txtroFooterPageNo.Text) - 1).ToString();
            await ShowDataHeaderAsync();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            SetNextRowActivateOnPageShow();
            RowUpdateEnd();
            // フォーカス可能な、最初のコントロールにフォーカスを移動する
            SelectNextControl(this, true, true, true, true);
        }

        /// <summary>次頁ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private async void BtnFooterNextPage_Click(object sender, EventArgs e)
        {
            this.btnFooterNextPage.Focus();
            var currentPageNo = int.Parse(this.txtroFooterPageNo.Text);
            var pageCount = int.Parse(this.txtroFooterPageCount.Text);

            // 最終ページの場合
            if (currentPageNo == pageCount)
            {
                // 商品コードが１つも登録されいていない場合は何もしない
                var hasDetailItem = Enumerable.Range(1, 6)
                    .Select(i => GetUcName(i))
                    .Any(d => d.Enabled && !string.IsNullOrEmpty(d.SyohinCode));
                if (!hasDetailItem)
                {
                    return;
                }
                if (!DialogHelper.YesNoQuestion(this, "ページを追加しますか？", true))
                {
                    return;
                }
                // ページ追加
                DataPageAdd();
                SetDataHeader();

                IntCheckPtn = 0;
                dtErrorList.Clear();
                dtErrorListDetail.Clear();
                InitControlStyles(this);

                this.txtroFooterPageCount.Text = (int.Parse(this.txtroFooterPageCount.Text) + 1).ToString();
                this.txtroFooterPageNo.Text = txtroFooterPageCount.Text;
                this.txtDEN_NO.Clear();
                this.txtANSWER_NAME.Clear();
                this.txtHAT_ORDER_NO.Text = InitialHatOrderNo();
                SetOrderState(JHOrderState.PreOrder);
                RowUpdateBegin();
                ClearFormDetail();
                ShowDetailPageData();
                RowUpdateEnd();
            }
            else
            {
                // 次のページに切り替え
                DelEmptyDataTableRecord();
                SetDataHeader();
                this.txtroFooterPageNo.Text = (int.Parse(this.txtroFooterPageNo.Text) + 1).ToString();
                await ShowDataHeaderAsync();
                RowUpdateBegin();
                ClearFormDetail();
                ShowDetailPageData();
                RowUpdateEnd();

            }
            SetNextRowActivateOnPageShow();

            // フォーカス可能な、最初のコントロールにフォーカスを移動する
            SelectNextControl(this, true, true, true, true);
        }
        private void BtnFooterF2_Click(object sender, System.EventArgs e)
        {
            if (this.ucRow6.Enabled) { return; }

            int intRowNo = 0;
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled) { intRowNo = i; };
            }
            if (GetUcName(intRowNo).txtSYOHIN_CD.Text.Length == 0)
            {
                return;
            }

            this.btnFooterF2.Focus();
            if (MessageBox.Show(@"行追加しますか？", this.btnFooterF2.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataDetailAdd();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            RowUpdateEnd();
            SetFocusLastRow();
        }
        private void BtnFooterF3_Click(object sender, System.EventArgs e)
        {
            if (strDenSort == "" || strRowNo == "") { return; }
            if (strRowNo != @"1")
            {
                if (int.Parse(strRowNo) > dtDetail_jhMain.Rows.Count) { return; }
            }
            this.btnFooterF3.Focus();
            MessageBox.Show(@"行番号 " + strRowNo + @" をコピーします");
            strDenSortCopy = strDenSort;
            strPageNoCopy = this.txtroFooterPageNo.Text;
            strRowNoCopy = strRowNo;
        }
        private void BtnFooterF4_Click(object sender, System.EventArgs e)
        {
            if (strRowNo == "" || strRowNoCopy == "") { return; }
            this.btnFooterF4.Focus();
            MessageBox.Show(strPageNoCopy + @"ページ　行番号 " + strRowNoCopy + @" を" + "\r\n" + this.txtroFooterPageNo.Text + @"ページ　行番号 " + strRowNo + @" に貼り付けます");
            SetDataDetailPaste();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            RowUpdateEnd();
        }
        private void BtnFooterF5_Click(object sender, System.EventArgs e)
        {
            this.btnFooterF5.Focus();
            if (dtDetail_jhMain.Rows.Count < 1) { return; }
            if (int.Parse(strRowNo) > dtDetail_jhMain.Rows.Count) { return; }

            if (MessageBox.Show(@"行削除しますか？" + "\r\n" + strRowNo + @"行目", @"行削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DataDetailDel();
            DelEmptyDataTableRecord();
            RowUpdateBegin();
            ClearFormDetail();
            ShowDetailPageData();
            SetNextRowActivateOnPageShow();
            RowUpdateEnd();
        }
        private void BtnFooterF6_Click(object sender, System.EventArgs e)
        {
            this.btnFooterF6.Focus();
            if (!ucRow1.txtKoban.Enabled)
            {
                MessageBox.Show(@"現在のステータスでは運賃を入力することができません", this.btnFooterF6.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (ucRow6.Enabled && BoolChkDetailDataInputRow(ucRow6))
                {
                    MessageBox.Show(@"これ以上行は追加できません", this.btnFooterF6.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        JH_Main_Detail jH_Main_Detail = GetUcName(i);
                        if (jH_Main_Detail.txtKoban.Text.Equals(@"9"))
                        {
                            MessageBox.Show(@"既に運賃行が存在します", this.btnFooterF6.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (MessageBox.Show(@"運賃行を追加しますか？", this.btnFooterF6.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    int intRowNo = 0;
                    for (int i = 1; i <= 6; i++)
                    {
                        JH_Main_Detail jH_Main_Detail = GetUcName(i);
                        if (jH_Main_Detail.Enabled) { intRowNo = i; }
                    }
                    if (intRowNo > 1)
                    {
                        if (BoolChkDetailDataInputRow(GetUcName(intRowNo)))
                        {
                            DataDetailAdd();
                        }
                        else
                        {
                            intRowNo--;
                        }
                    }
                    else
                    {
                        // 先頭行
                        intRowNo--;
                    }
                    for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
                    {
                        if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[i]["DenNoLine"].ToString().Equals((intRowNo + 1).ToString()))
                        {
                            dtDetail_jhMain.Rows[i]["Koban"] = @"9";
                            dtDetail_jhMain.Rows[i]["SyohinCd"] = @"ｳﾝﾁﾝ";
                            break;
                        }
                    }
                    RowUpdateBegin();
                    ClearFormDetail();
                    ShowDetailPageData();
                    RowUpdateEnd();
                    SetNextRowActivateOnPageShow();

                }
            }
        }
        private void BtnFooterF7_Click(object sender, System.EventArgs e)
        {
            this.btnFooterF7.Focus();
            // 伝票分割非表示対応 ここから
            //if (this.ucRow1.cmbSOKO_CD.Visible)
            //{
            //    CngShowDetailParts(false);
            //    this.btnFooterF7.Text = @"F7:詳細画面";
            //}
            if (sender is Button && (sender as Button).Text.Contains("簡易"))
            {
                CngShowDetailParts(false);
                this.btnFooterF7.Text = @"F7:詳細画面";
            }
            // 伝票分割非表示対応 ここまで
            else
            {
                CngShowDetailParts(true);
                this.btnFooterF7.Text = @"F7:簡易画面";
            }
        }
        private void BtnFooterF8_Click(object sender, System.EventArgs e)
        {
            this.btnFooterF8.Focus();
            MessageBox.Show(this.btnFooterF8.Text);
        }
        private void BtnFooterF10_Click(object sender, System.EventArgs e)
        {
            this.btnFooterF10.Focus();
            MessageBox.Show(this.btnFooterF10.Text);
        }
        #endregion

        #region << フォームクリア >>
        private void ClearFormMain()
        {
            this.txtJYU2.Text = loginRepo.CurrentUser.EmployeeTag;
            this.txtNYU2.Clear();
            this.txtroORDER_FLAG.Clear();
            this.txtroOPS_ORDER_NO.Clear();
            this.txtroESTIMATE_NO.Clear();
            this.txtroDSEQ.Clear();
            this.txtroDSEQTime.Clear();
            this.txtroHattyuJyoutai.Clear();
            this.txtroHattyuJyoutaiName.Clear();
            this.cmbDEN_FLG.SelectedIndex = -1;
            this.cmbDEN_FLG.Text = null;
            this.txtDEN_NO.Clear();
            this.txtTEAM_CD.Text = loginRepo.CurrentUser.TeamCode;
            this.txtTOKUI_CD.Clear();
            this.txtKMAN_CD.Clear();
            this.txtroKMAN_NAME.Clear();
            this.chkOKURI_FLAG.Checked = false;
            this.txtroTOKUI_NAME.Clear();
            this.chkTokuisakiHihyouji.Checked = false;
            this.chkTEL_RENRAKU_FLG.Checked = false;
            this.cmbSOKO_CD.SelectedIndex = -1;
            this.cmbSOKO_CD.Text = null;
            this.txtGENBA_CD.Clear();
            this.cmbBINCD.SelectedIndex = -1;
            this.cmbBINCD.Text = null;
            this.cmbUNCHIN.SelectedIndex = -1;
            this.cmbUNCHIN.Text = null;
            this.cmbNOHIN.SelectedIndex = -1;
            this.cmbNOHIN.Text = null;
            this.chkKESSAI.Checked = false;
            this.dateNOUKI.Value = DateTime.Today;
            this.dateNOUKI.Clear();
            this.txtCUST_ORDERNO.Clear();
            this.txtNOTE_HOUSE.Clear();
            this.cmbHKBN.SelectedIndex = -1;
            this.cmbHKBN.Text = null;
            this.txtSHIRESAKI_CD.Clear();
            this.txtroSHIRESAKI_NAME.Clear();
            this.txtSIRAINM.Clear();
            this.txtSFAX.Clear();
            this.dateHAT_NYUKABI.Value = DateTime.Today;
            this.dateHAT_NYUKABI.Clear();
            // TeamCd + 担当者記号 + 得意先記号 + 連番3桁 + C(直送)
            this.txtHAT_ORDER_NO.Text = InitialHatOrderNo();

            this.chkHeaderBunrui.Checked = false;
            this.chkHeaderSyouhin.Checked = false;

            this.txtroFooterPageNo.Clear();
            this.txtroFooterPageCount.Clear();

            this.txtroFooterTeika.Clear();
            this.txtroFooterBaigaku.Clear();
            this.txtroFooterShigaku.Clear();
            this.txtroFooterArari.Clear();
            this.txtroFooterRiritsu.Clear();

            this.txtKOUJITEN_CD.Clear();
            this.txtroKOUJITEN_NAME.Clear();
            this.chkKTankaAuto.Checked = false;
            this.txtANSWER_NAME.Clear();
            this.txtRECV_POSTCODE.Clear();
            this.txtRECV_ADD1.Clear();
            this.txtRECV_ADD2.Clear();
            this.txtRECV_ADD3.Clear();
            this.txtRECV_TEL.Clear();
            this.txtRECV_NAME1.Clear();
            this.txtRECV_NAME2.Clear();
            this.txtORDER_MEMO1.Clear();

            IntCheckPtn = 0;
            dtErrorList.Clear();
            dtErrorListDetail.Clear();

            this.txtroNote.Clear();

            InitControlStyles(this);
        }
        private void ClearFormDetail()
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.Enabled = true;

                jH_Main_Detail.txtroRowNo.Clear();
                jH_Main_Detail.txtKoban.Clear();
                jH_Main_Detail.txtSYOBUN_CD.Clear();
                jH_Main_Detail.txtSYOHIN_CD.Clear();
                jH_Main_Detail.cmbSyohinSuggest.Text = string.Empty;
                jH_Main_Detail.cmbSyohinSuggest.Visible = false;
                jH_Main_Detail.cmbURIKUBN.SelectedIndex = -1;
                jH_Main_Detail.cmbURIKUBN.Text = null;
                jH_Main_Detail.txtroZaikoSuu.Clear();
                jH_Main_Detail.numSURYO.Clear();
                jH_Main_Detail.txtTANI.Clear();
                jH_Main_Detail.numBARA.Clear();
                jH_Main_Detail.decTEI_TAN.Clear();
                if (jH_Main_Detail.dateNOUKI.Visible)
                {
                    jH_Main_Detail.dateNOUKI.Value = DateTime.Today;
                    jH_Main_Detail.dateNOUKI.Clear();
                    jH_Main_Detail.dateNOUKI.Refresh();
                }
                else
                {
                    jH_Main_Detail.dateNOUKI.Visible = true;
                    jH_Main_Detail.dateNOUKI.Value = DateTime.Today;
                    jH_Main_Detail.dateNOUKI.Clear();
                    jH_Main_Detail.dateNOUKI.Refresh();
                    jH_Main_Detail.dateNOUKI.Visible = false;
                }
                jH_Main_Detail.decSII_ANSW_TAN.Clear();
                jH_Main_Detail.txtroRiritsu.Clear();
                jH_Main_Detail.txtURI_KIGOU.Clear();
                jH_Main_Detail.decURI_KAKE.Clear();
                jH_Main_Detail.decURI_TAN.Clear();
                jH_Main_Detail.cmbSOKO_CD.SelectedIndex = -1;
                jH_Main_Detail.cmbSOKO_CD.Text = null;
                jH_Main_Detail.txtSHIRESAKI_CD.Clear();
                jH_Main_Detail.txtSII_KIGOU.Clear();
                jH_Main_Detail.decSII_KAKE.Clear();
                jH_Main_Detail.decSII_TAN.Clear();
                jH_Main_Detail.txtroNoTankaWariai.Clear();
                jH_Main_Detail.txtLBIKO.Clear();
                jH_Main_Detail.txtTAX_FLG.Clear();

                jH_Main_Detail.txtSiireBikou.Clear();       // 20240309
                jH_Main_Detail.lblDEL_FLG.Visible = false;  // (HAT-42)20240309

            }
        }

        /// <summary>ヘッダ部分にあるボタンの押下可否を変更する</summary>
        /// <param name="bSts">フォーカスがヘッダ/フッタにある場合true、明細にある場合false</param>
        private void CngButtonStsMain(bool bSts)
        {
            this.btnFnc03.Enabled = bSts;
            this.btnFnc05.Enabled = bSts;
            this.btnFnc06.Enabled = bSts;
            //this.btnFnc07.Enabled = bSts;
            //this.btnFnc08.Enabled = bSts;
            this.btnFnc10.Enabled = bSts;

            // F11とF12は明細行にフォーカスがあっても押せる
            this.btnFnc11.Enabled = txtroHattyuJyoutai.Text != JHOrderState.Ordered;
            this.btnFnc12.Enabled = txtroHattyuJyoutai.Text != JHOrderState.Acos;
        }

        private static void InitControlStyles(System.Windows.Forms.Control hParent)
        {
            foreach (Control cControl in hParent.Controls)
            {
                if (cControl.HasChildren == true)
                {
                    InitControlStyles(cControl);
                }

                if (cControl is TextBoxChar || cControl is TextBoxCharSize1 || cControl is ComboBoxEx || cControl is C1DateInputEx)
                {
                    cControl.Font = new System.Drawing.Font(cControl.Font, FontStyle.Regular);
                    cControl.BackColor = SystemColors.Window;
                    cControl.ForeColor = SystemColors.ControlText;
                }
            }
        }
        private bool BoolIsChkStatusOnInit()
        {
            if (this.txtJYU2.Text.Length > 0) { return true; }
            if (this.txtNYU2.Text.Length > 0) { return true; }
            if (this.txtroORDER_FLAG.Text.Length > 0) { return true; }
            if (this.txtroOPS_ORDER_NO.Text.Length > 0) { return true; }
            if (this.txtroESTIMATE_NO.Text.Length > 0) { return true; }
            if (this.txtroDSEQ.Text.Length > 0) { return true; }
            if (this.cmbDEN_FLG.Text.Length > 0) { return true; }
            if (this.txtDEN_NO.Text.Length > 0) { return true; }
            if (this.txtTEAM_CD.Text.Length > 0) { return true; }
            if (this.txtTOKUI_CD.Text.Length > 0) { return true; }
            if (this.txtKMAN_CD.Text.Length > 0) { return true; }
            if (this.txtroKMAN_NAME.Text.Length > 0) { return true; }
            if (this.chkOKURI_FLAG.Checked) { return true; }
            if (this.txtroTOKUI_NAME.Text.Length > 0) { return true; }
            if (this.chkTokuisakiHihyouji.Checked) { return true; };
            if (this.chkTEL_RENRAKU_FLG.Checked) { return true; }
            if (this.cmbSOKO_CD.Text.Length > 0) { return true; }
            if (this.txtGENBA_CD.Text.Length > 0) { return true; }
            if (this.cmbBINCD.Text.Length > 0) { return true; }
            if (this.cmbUNCHIN.Text.Length > 0) { return true; }
            if (this.cmbNOHIN.Text.Length > 0) { return true; }
            if (this.chkKESSAI.Checked) { return true; }
            if (this.dateNOUKI.Text.Length > 0) { return true; }
            if (this.txtCUST_ORDERNO.Text.Length > 0) { return true; }
            if (this.txtNOTE_HOUSE.Text.Length > 0) { return true; }
            if (this.cmbHKBN.Text.Length > 0) { return true; };
            if (this.txtSHIRESAKI_CD.Text.Length > 0) { return true; }
            if (this.txtroSHIRESAKI_NAME.Text.Length > 0) { return true; }
            if (this.txtSIRAINM.Text.Length > 0) { return true; }
            if (this.txtSFAX.Text.Length > 0) { return true; }
            if (this.dateHAT_NYUKABI.Text.Length > 0) { return true; }

            if (this.txtKOUJITEN_CD.Text.Length > 0) { return true; }
            if (this.txtroKOUJITEN_NAME.Text.Length > 0) { return true; }
            if (this.chkKTankaAuto.Checked) { return true; }
            if (this.txtANSWER_NAME.Text.Length > 0) { return true; }
            if (this.txtRECV_POSTCODE.Text.Length > 0) { return true; }
            if (this.txtRECV_ADD1.Text.Length > 0) { return true; }
            if (this.txtRECV_ADD2.Text.Length > 0) { return true; }
            if (this.txtRECV_ADD3.Text.Length > 0) { return true; }
            if (this.txtRECV_TEL.Text.Length > 0) { return true; }
            if (this.txtRECV_NAME1.Text.Length > 0) { return true; }
            if (this.txtRECV_NAME2.Text.Length > 0) { return true; }
            if (this.txtORDER_MEMO1.Text.Length > 0) { return true; }

            return false;
        }
        #endregion

        #region << 入力項目　編集状態変更 >>
        /// <summary>発注状態の変更による各コントロールのEnable制御</summary>
        /// <param name="jhOrderState">発注状態</param>
        private void CngEditModeHeader(JHOrderState jhOrderState)
        {
            // 伝票区分が[15:取次]または[21:直送]＝外部に発注
            string denFlg = cmbDEN_FLG.GetSelectedCode();
            var isNeedOrder = new[] { "15", "21" }.Contains(denFlg);
            // 出荷指示書が印刷されるまでは編集が可能。直送の場合は自社から出荷しないのでfalse
            var printed = (denFlg != "21") && (DenShippingPrinted == true);
            var hasCompletedDetail = GetCurDetails()
                .Any(x => GetDTNullableValue<short>(x, "GCompleteFlg") == 1);

            this.txtJYU2.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtNYU2.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.cmbDEN_FLG.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtTEAM_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtTOKUI_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtKMAN_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.cmbSOKO_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && cmbDEN_FLG.GetSelectedCode() != "21";
            this.txtGENBA_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.cmbBINCD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.cmbUNCHIN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.cmbNOHIN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.chkKESSAI.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.dateNOUKI.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtCUST_ORDERNO.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtNOTE_HOUSE.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);

            this.chkHeaderBunrui.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);

            this.txtANSWER_NAME.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_POSTCODE.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_ADD1.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_ADD2.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_ADD3.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_TEL.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_NAME1.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.txtRECV_NAME2.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);

            this.btnClearAddress.Visible = jhOrderState.IsValid && !(printed || hasCompletedDetail);

            this.txtDEN_NO.Enabled = jhOrderState == JHOrderState.PreOrder;
            this.chkOKURI_FLAG.Enabled = jhOrderState == JHOrderState.PreOrder;
            this.chkTokuisakiHihyouji.Enabled = jhOrderState == JHOrderState.PreOrder;
            this.chkTEL_RENRAKU_FLG.Enabled = jhOrderState == JHOrderState.PreOrder;

            this.txtroKOUJITEN_NAME.Enabled = true;
            this.chkKTankaAuto.Enabled = true;
        }

        /// <summary>発注状態による、明細部分のEnable制御</summary>
        /// <param name="jhOrderState">発注状態</param>
        private void CngEditModeDetail(JHOrderState jhOrderState)
        {
            // 伝票区分が[15:取次]または[21:直送]＝外部に発注
            string denFlg = cmbDEN_FLG.GetSelectedCode();
            var isNeedOrder = new[] { "15", "21" }.Contains(denFlg);
            // 出荷指示書が印刷されるまでは編集が可能。直送の場合は自社から出荷しないのでfalse
            var printed = (denFlg != "21") && (DenShippingPrinted == true);
            var hasCompletedDetail = GetCurDetails()
                .Any(x => GetDTNullableValue<short>(x, "GCompleteFlg") == 1);

            foreach (var row in _detailRows)
            {
                row.txtKoban.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtSYOHIN_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.btnSearch.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.cmbURIKUBN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.numSURYO.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtTANI.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decTEI_TAN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.dateNOUKI.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decSII_ANSW_TAN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtURI_KIGOU.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decURI_KAKE.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decURI_TAN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtSHIRESAKI_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtSII_KIGOU.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decSII_KAKE.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.decSII_TAN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtLBIKO.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.txtTAX_FLG.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);

                row.numBARA.Enabled = (jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder && row.SyobunCd.Length > 0);

                row.btnSiireBikou.Enabled = (row.SyobunCd.Length > 0 || row.SyohinCode.Length > 0);
            }
        }

        private void CngEditModeHeaderDenFlg(JHOrderState jhOrderState)
        {
            // 伝票区分が[15:取次]または[21:直送]＝外部に発注
            string denFlg = cmbDEN_FLG.GetSelectedCode();
            var isNeedOrder = new[] { "15", "21" }.Contains(denFlg);
            // 出荷指示書が印刷されるまでは編集が可能。直送の場合は自社から出荷しないのでfalse
            var printed = (denFlg != "21") && (DenShippingPrinted == true);
            var hasCompletedDetail = GetCurDetails()
                .Any(x => GetDTNullableValue<short>(x, "GCompleteFlg") == 1);

            this.cmbHKBN.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
            this.txtSHIRESAKI_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
            this.txtSIRAINM.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
            this.dateHAT_NYUKABI.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
            this.txtORDER_MEMO1.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
            this.txtHAT_ORDER_NO.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
            this.btnNewHatOrderNo.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);

            this.chkHeaderBunrui.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
        }

        /// <summary>発注状態と伝区による、明細部分のEnable制御</summary>
        /// <param name="jhOrderState">発注状態</param>
        private void CngEditModeDetailDenFlg(JHOrderState jhOrderState)
        {
            // 伝票区分が[15:取次]または[21:直送]＝外部に発注
            string denFlg = cmbDEN_FLG.GetSelectedCode();
            var isNeedOrder = new[] { "15", "21" }.Contains(denFlg);
            // 出荷指示書が印刷されるまでは編集が可能。直送の場合は自社から出荷しないのでfalse
            var printed = (denFlg != "21") && (DenShippingPrinted == true);
            var hasCompletedDetail = GetCurDetails()
                .Any(x => GetDTNullableValue<short>(x, "GCompleteFlg") == 1);

            foreach(var row in _detailRows)
            {
                row.txtSYOBUN_CD.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail) && isNeedOrder;
                row.txtKoban.Enabled = jhOrderState.IsValid && !(printed || hasCompletedDetail);
                row.cmbSOKO_CD.Enabled = (jhOrderState.IsValid && 
                    !(printed || hasCompletedDetail) && 
                    new[] { "11", "12", "13", "15" }.Contains(denFlg));
            }
        }
        private void ForEditMode_CmbDenFlg_Validated(object sender, EventArgs e)
        {
            string strDEN_FLG = (this.cmbDEN_FLG.GetSelectedCode() == null) ? @"" : this.cmbDEN_FLG.GetSelectedCode();
            if (strDEN_FLG.Length > 0)
            {
                // 伝区が21:直送なら
                if (strDEN_FLG == "21")
                {
                    // 倉庫を07:メーカー
                    cmbSOKO_CD.SetSelectedCode("07");
                    // 運賃を1:元払い
                    cmbUNCHIN.SetSelectedCode("1");
                    // 区分を0:その他
                    cmbNOHIN.SetSelectedCode("0");
                }
                CngEditModeHeader(txtroHattyuJyoutai.Text);
                CngEditModeHeaderDenFlg(txtroHattyuJyoutai.Text);
                CngEditModeDetailDenFlg(txtroHattyuJyoutai.Text);
            }

        }
        private void CngEditModeHeaderHKBN()
        {
            string strHKBN = (this.cmbHKBN.GetSelectedCode() == null) ? @"" : this.cmbHKBN.GetSelectedCode();
            this.txtSFAX.Enabled = strHKBN == "1";
        }
        private void ForEditMode_CmbHKBN_Validated(object sender, EventArgs e)
        {
            string strHKBN = (this.cmbHKBN.GetSelectedCode() == null) ? @"" : this.cmbHKBN.GetSelectedCode();
            if (strHKBN.Length > 0)
            {
                CngEditModeHeaderHKBN();
            }

        }
        private void SetEditModeCng()
        {
            this.cmbDEN_FLG.Validated += new EventHandler(ForEditMode_CmbDenFlg_Validated);
            this.cmbHKBN.Validated += new EventHandler(ForEditMode_CmbHKBN_Validated);
        }
        #endregion

        #region << ヘッダ部制御 >>
        private void SetDataHeaderInit()
        {
            dtHeader_jhMain.Columns.Add("SaveKey", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderNo", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderState", typeof(string));      // 《ラベルなし》「発注前」(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("DenNo", typeof(string));           // 伝No(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("DenSort", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("DenState", typeof(string));        // 《ラベルなし》「発注前」(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("DenSortInt", typeof(int));         //《画面対応なし》整列用に追加
            dtHeader_jhMain.Columns.Add("DenFlg", typeof(string));          // 伝区(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("EstimateNo", typeof(string));      // 見積番号(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("EstCoNo", typeof(string));         // 見積番号(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TeamCd", typeof(string));          // 販課(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TantoCd", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TantoName", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Jyu2", typeof(string));            // 受発注者(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Jyu2Cd", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Jyu2Id", typeof(int));             // 20240228 ADD
            dtHeader_jhMain.Columns.Add("Nyu2", typeof(string));            // 入力者(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Nyu2Cd", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Nyu2Id", typeof(int));             // 20240228 ADD
            dtHeader_jhMain.Columns.Add("HatOrderNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("CustOrderno", typeof(string));     // 客注(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TokuiCd", typeof(string));         // 得意先(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TokuiName", typeof(string));       // 20240228 ADD
            dtHeader_jhMain.Columns.Add("KmanCd", typeof(string));          // 担(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("KmanName", typeof(string));        // 20240228 ADD
            dtHeader_jhMain.Columns.Add("GenbaCd", typeof(string));         // 現場(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Nouki", typeof(DateTime));         // 納日(FosJyuchuH)DateTime
            dtHeader_jhMain.Columns.Add("HatNyukabi", typeof(DateTime));    //《画面対応なし》入荷日(FosJyuchuH)DateTime
            dtHeader_jhMain.Columns.Add("BukkenExp", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Sale1Flag", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Kessai", typeof(string));          // 決済(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Raikan", typeof(string));          // 来勘(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("KoujitenCd", typeof(string));      // 工事店(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("KoujitenName", typeof(string));    // 20240228 ADD
            dtHeader_jhMain.Columns.Add("SokoCd", typeof(string));          // 倉庫(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("SokoName", typeof(string));        // 20240228 ADD
            dtHeader_jhMain.Columns.Add("NoteHouse", typeof(string));       // 社内備考(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderFlag", typeof(string));       // 受区(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("RecYmd", typeof(DateTime));        //《画面対応なし》(FosJyuchuH)DateTime
            dtHeader_jhMain.Columns.Add("ShiresakiCd", typeof(string));     // 仕入(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("ShiresakiName", typeof(string));   // 20240228 ADD
            dtHeader_jhMain.Columns.Add("Hkbn", typeof(string));            // 発注(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Sirainm", typeof(string));         // 依頼(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Sfax", typeof(string));            // FAX(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("SmailAdd", typeof(string));        //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderMemo1", typeof(string));      // 発注時メモ(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderMemo2", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderMemo3", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Nohin", typeof(string));           // 区分(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Unchin", typeof(string));          // 運賃(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Bincd", typeof(string));           // 扱便(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Binname", typeof(string));         // 扱便(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OkuriFlag", typeof(string));       // 送元(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("ShireKa", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("Dseq", typeof(string));            // 内部No.(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("OrderDenNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("MakerDenNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("IpAdd", typeof(string));           //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("InpDate", typeof(DateTime));       //《画面対応なし》(FosJyuchuH)DateTime
            dtHeader_jhMain.Columns.Add("UpdDate", typeof(DateTime));       //《画面対応なし》(FosJyuchuH)DateTime
            dtHeader_jhMain.Columns.Add("AnswerName", typeof(string));      // 回答者(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("DelFlg", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("AnswerConfirmFlg", typeof(string));//《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("SansyoDseq", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("ReqBiko", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("TelRenrakuFlg", typeof(string));   // 電話連絡済(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("UkeshoFlg", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("EpukoKanriNo", typeof(string));    //《画面対応なし》(FosJyuchuH)string
            dtHeader_jhMain.Columns.Add("SupplierType", typeof(decimal));    // 20240228 ADD

            dtHeader_jhMain.Columns.Add("RecvGenbaCd", typeof(string));     // 《画面対応なし》(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvName1", typeof(string));       // 宛先1(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvName2", typeof(string));       // 宛先2(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvTel", typeof(string));         // TEL(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvPostcode", typeof(string));    // 〒(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvAdd1", typeof(string));        // 住所1(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvAdd2", typeof(string));        // 住所2(FosJyuchuHRecv)string
            dtHeader_jhMain.Columns.Add("RecvAdd3", typeof(string));        // 住所3(FosJyuchuHRecv)string

            dtHeader_jhMain.Columns.Add("OpsOrderNo", typeof(string));      // OPSNo.(FosJyuchuHOp)string
            dtHeader_jhMain.Columns.Add("OpsRecYmd", typeof(DateTime));     // 《画面対応なし》(FosJyuchuHOp)DateTime
            dtHeader_jhMain.Columns.Add("OpsHachuAdr", typeof(string));     // 《画面対応なし》(FosJyuchuHOp)string
            dtHeader_jhMain.Columns.Add("OpsBin", typeof(string));          // 《画面対応なし》(FosJyuchuHOp)string
            dtHeader_jhMain.Columns.Add("OpsHachuName", typeof(string));    // 《画面対応なし》(FosJyuchuHOp)string

            dtHeader_jhMain.Columns.Add("GOrderNo", typeof(string));        /// 《GLASS.受注データ.受注番号》
            dtHeader_jhMain.Columns.Add("GOrderDate", typeof(DateTime));   /// 《GLASS.受注データ.受注日;
            dtHeader_jhMain.Columns.Add("GStartDate", typeof(DateTime));    /// 《GLASS.受注データ.部門開始日》
            dtHeader_jhMain.Columns.Add("GCustCode", typeof(string));       /// 《GLASS.受注データ.顧客コード》
            dtHeader_jhMain.Columns.Add("GCustSubNo", typeof(string));      /// 《GLASS.受注データ.顧客枝番》
            dtHeader_jhMain.Columns.Add("GOrderAmnt", typeof(long));        /// 《GLASS.受注データ.受注金額合計》
            dtHeader_jhMain.Columns.Add("GCmpTax", typeof(long));           /// 《GLASS.受注データ.消費税金額》
            dtHeader_jhMain.Columns.Add("CreateDate", typeof(DateTime));    /// 《GLASS.受注データ.作成日時》
            dtHeader_jhMain.Columns.Add("Creator", typeof(int));            /// 《GLASS.受注データ.作成者》
            dtHeader_jhMain.Columns.Add("UpdateDate", typeof(DateTime));    /// 《GLASS.受注データ.更新日時》
            dtHeader_jhMain.Columns.Add("Updater", typeof(int));            /// 《GLASS.受注データ.更新者》
            dtHeader_jhMain.Columns.Add(nameof(FosJyuchuH.DenShippingPrinted), typeof(bool)); // 出荷指示書印刷済み
        }
        private async Task ShowDataHeaderAsync()
        {
            if (dtHeader_jhMain.Rows.Count == 0) { return; }

            // 20240309
            IntCheckPtn = 0;
            dtErrorList.Clear();
            dtErrorListDetail.Clear();
            InitControlStyles(this);

            int j = (int.Parse(this.txtroFooterPageNo.Text) - 1);

            strSaveKey = dtHeader_jhMain.Rows[j]["SaveKey"].ToString();                         //PK1
            strDenSort = dtHeader_jhMain.Rows[j]["DenSort"].ToString();                         //PK2
            SearchRepo repo = SearchRepo.GetInstance();

            this.txtJYU2.Text = dtHeader_jhMain.Rows[j]["Jyu2"].ToString();                     //受発注者
            this.txtNYU2.Text = dtHeader_jhMain.Rows[j]["Nyu2"].ToString();                     //入力者
            this.txtroORDER_FLAG.Text = dtHeader_jhMain.Rows[j]["OrderFlag"].ToString();        //受注区分
            this.txtroOPS_ORDER_NO.Text = dtHeader_jhMain.Rows[j]["OpsOrderNo"].ToString();     //OPSNo.
            this.txtroESTIMATE_NO.Text = dtHeader_jhMain.Rows[j]["EstimateNo"].ToString();      //見積番号
            this.txtroDSEQ.Text = dtHeader_jhMain.Rows[j]["Dseq"].ToString();                   //内部番号

            SetOrderState(GetCurrentHattyuJotai());

            this.cmbDEN_FLG.SetSelectedCode(dtHeader_jhMain.Rows[j]["DenFlg"].ToString());      //伝票区分
            this.txtDEN_NO.Text = dtHeader_jhMain.Rows[j]["DenNo"].ToString();                  //伝票番号
            this.txtTEAM_CD.Text = dtHeader_jhMain.Rows[j]["TeamCd"].ToString();                //販課
            this.txtTOKUI_CD.Text = dtHeader_jhMain.Rows[j]["TokuiCd"].ToString();              //得意先ＣＤ
            this.txtKMAN_CD.Text = dtHeader_jhMain.Rows[j]["KmanCd"].ToString();                //担

            this.txtroKMAN_NAME.Clear();
            if (this.txtTEAM_CD.Text.Length > 0 && this.txtTOKUI_CD.Text.Length > 0 && this.txtKMAN_CD.Text.Length > 0)
            {
                var teamCode = txtTEAM_CD.Text;
                var tokuiCode = txtTOKUI_CD.Text;
                var keymanCode = txtKMAN_CD.Text;
                var keyman = await ApiHelper.FetchAsync(this, () =>
                {
                    return repo.searchKeyman(teamCode, tokuiCode, keymanCode, 1);
                });
                if (keyman.Failed)
                {
                    return;
                }
                if (keyman != null && keyman.Value.Any()) { this.txtroKMAN_NAME.Text = keyman.Value[0].KmanNm1; }             //担名称
            }

            ShowCheckBoxData(this.chkOKURI_FLAG, dtHeader_jhMain.Rows[j]["OkuriFlag"].ToString());//送元

            this.txtroKMAN_NAME.Clear();
            if (this.txtTOKUI_CD.Text.Length > 0)
            {
                var tokuiCode = txtTOKUI_CD.Text;
                var teamCode = txtTEAM_CD.Text;
                var torihikis = await ApiHelper.FetchAsync(this, () =>
                {
                    return repo.searchTorihiki(tokuiCode, string.Empty, string.Empty, teamCode, 1);
                });
                if (torihikis.Failed)
                {
                    return;
                }
                if (torihikis != null && torihikis.Value.Any()) { this.txtroTOKUI_NAME.Text = torihikis.Value[0].TokuZ; }        //得意先名
            }

            // 倉庫
            cmbSOKO_CD.SetSelectedCode(dtHeader_jhMain.Rows[j]["SokoCd"].ToString());
            this.txtGENBA_CD.Text = dtHeader_jhMain.Rows[j]["GenbaCd"].ToString();              //現場
            // 扱便
            this.cmbBINCD.SetSelectedCode(dtHeader_jhMain.Rows[j]["Bincd"].ToString());
            // 運賃
            this.cmbUNCHIN.SetSelectedCode(dtHeader_jhMain.Rows[j]["Unchin"].ToString());
            // 区分
            this.cmbNOHIN.SetSelectedCode(dtHeader_jhMain.Rows[j]["Nohin"].ToString());

            this.dateNOUKI.Value = dtHeader_jhMain.Rows[j]["Nouki"];                            //納日
            this.dateNOUKI.Text = HatFComParts.DoFormatYYMMDD(this.dateNOUKI.Value);

            this.txtCUST_ORDERNO.Text = dtHeader_jhMain.Rows[j]["CustOrderno"].ToString();      //客注
            this.txtNOTE_HOUSE.Text = dtHeader_jhMain.Rows[j]["NoteHouse"].ToString();          //社内備考
            // 発注区分
            this.cmbHKBN.SetSelectedCode(dtHeader_jhMain.Rows[j]["Hkbn"].ToString());

            this.txtSHIRESAKI_CD.Text = dtHeader_jhMain.Rows[j]["ShiresakiCd"].ToString();      //仕入先ＣＤ
            this.txtroSHIRESAKI_NAME.Clear();
            if (this.txtSHIRESAKI_CD.Text.Length > 0)
            {
                var shiresakiCode = txtSHIRESAKI_CD.Text;
                var teamCode = txtTEAM_CD.Text;
                var suppliers = await ApiHelper.FetchAsync(this, () =>
                {
                    return repo.searchSupplier(shiresakiCode, string.Empty, string.Empty, teamCode, 1);
                });
                if (suppliers.Failed)
                {
                    return;
                }
                if (suppliers != null && suppliers.Value.Any()) { this.txtroSHIRESAKI_NAME.Text = suppliers.Value[0].SupplierName; } //得意先名
            }

            this.txtSIRAINM.Text = dtHeader_jhMain.Rows[j]["Sirainm"].ToString();               //依頼
            this.txtSFAX.Text = dtHeader_jhMain.Rows[j]["Sfax"].ToString();                     //ＦＡＸ

            DateTime dt = DateTime.Parse("1900/01/01");
            this.dateHAT_NYUKABI.Value = dtHeader_jhMain.Rows[j]["HatNyukabi"];                 //入荷日
            //this.dateHAT_NYUKABI.Value = (dtHeader_jhMain.Rows[j]["HatNyukabi"].Equals(dt)) ? null : dtHeader_jhMain.Rows[j]["HatNyukabi"];//入荷日
            this.dateHAT_NYUKABI.Text = HatFComParts.DoFormatYYMMDD(this.dateHAT_NYUKABI.Value);


            this.txtHAT_ORDER_NO.Text = dtHeader_jhMain.Rows[j]["HatOrderNo"].ToString();       //ＨＡＴ注番
            //this.chkTokuisakiHihyouji.Checked = false; = dtHeader_jhMain.Rows[j][""].ToString();
            ShowCheckBoxData(this.chkTEL_RENRAKU_FLG, dtHeader_jhMain.Rows[j]["TelRenrakuFlg"].ToString());//電話連絡済
            ShowCheckBoxData(this.chkKESSAI, dtHeader_jhMain.Rows[j]["Kessai"].ToString());     //決済
            this.txtKOUJITEN_CD.Text = dtHeader_jhMain.Rows[j]["KoujitenCd"].ToString();        //工事店ＣＤ

            this.txtroKOUJITEN_NAME.Clear();
            if (this.txtKOUJITEN_CD.Text.Length > 0)
            {
                var koujitenCode = txtKOUJITEN_CD.Text;
                var tokuiCode = txtTOKUI_CD.Text;
                var koujiten = await ApiHelper.FetchAsync(this, () =>
                {
                    return repo.searchKoujiten(koujitenCode, string.Empty, tokuiCode, 1);
                });
                if (koujiten.Failed)
                {
                    return;
                }
                if (koujiten != null && koujiten.Value.Any()) { this.txtroKOUJITEN_NAME.Text = koujiten.Value[0].KojiNnm; } //工事店名
            }

            //this.chkKTankaAuto.Checked = false; = dtHeader_jhMain.Rows[j][""].ToString();
            this.txtANSWER_NAME.Text = dtHeader_jhMain.Rows[j]["AnswerName"].ToString();        //回答者
            this.txtRECV_POSTCODE.Text = dtHeader_jhMain.Rows[j]["RecvPostcode"].ToString();    //〒
            this.txtRECV_ADD1.Text = dtHeader_jhMain.Rows[j]["RecvAdd1"].ToString();            //住所1
            this.txtRECV_ADD2.Text = dtHeader_jhMain.Rows[j]["RecvAdd2"].ToString();            //住所2
            this.txtRECV_ADD3.Text = dtHeader_jhMain.Rows[j]["RecvAdd3"].ToString();            //住所3
            this.txtRECV_TEL.Text = dtHeader_jhMain.Rows[j]["RecvTel"].ToString();              //TEL
            this.txtRECV_NAME1.Text = dtHeader_jhMain.Rows[j]["RecvName1"].ToString();          //宛先1
            this.txtRECV_NAME2.Text = dtHeader_jhMain.Rows[j]["RecvName2"].ToString();          //宛先2
            this.txtORDER_MEMO1.Text = dtHeader_jhMain.Rows[j]["OrderMemo1"].ToString();        //発注時メモ
        }

        /// <summary>現在のページの発注状態を取得する</summary>
        /// <returns>発注状態</returns>
        private JHOrderState GetCurrentHattyuJotai()
        {
            var currentPage = HatFComParts.DoParseInt(this.txtroFooterPageNo.Text).Value - 1;
            var delFlg = dtHeader_jhMain.Rows[currentPage]["DelFlg"].ToString();
            var ukeshoFlg = dtHeader_jhMain.Rows[currentPage]["UkeshoFlg"].ToString();
            var denState = dtHeader_jhMain.Rows[currentPage]["DenState"].ToString();
            return HatFComParts.GetHattyuJyoutai(delFlg, ukeshoFlg, denState);
        }

        private void SetDataHeader()
        {
            if (dtHeader_jhMain.Rows.Count == 0) { return; }
            int j = (int.Parse(this.txtroFooterPageNo.Text) - 1);

            dtHeader_jhMain.Rows[j]["Jyu2"] = this.txtJYU2.Text;                    //受発注者
            dtHeader_jhMain.Rows[j]["Nyu2"] = this.txtNYU2.Text;                    //入力者
            dtHeader_jhMain.Rows[j]["OrderFlag"] = this.txtroORDER_FLAG.Text;       //受注区分
            dtHeader_jhMain.Rows[j]["OpsOrderNo"] = this.txtroOPS_ORDER_NO.Text;    //OPSNo.
            dtHeader_jhMain.Rows[j]["EstimateNo"] = this.txtroESTIMATE_NO.Text;     //見積番号
            dtHeader_jhMain.Rows[j]["Dseq"] = this.txtroDSEQ.Text;                  //内部番号
            //dtHeader_jhMain.Rows[j]["DelFlg"];       //状態
            dtHeader_jhMain.Rows[j]["DenFlg"] = this.cmbDEN_FLG.GetSelectedCode();  //伝票区分
            dtHeader_jhMain.Rows[j]["DenNo"] = this.txtDEN_NO.Text;                 //伝票番号
            dtHeader_jhMain.Rows[j]["TeamCd"] = this.txtTEAM_CD.Text;               //販課
            dtHeader_jhMain.Rows[j]["TokuiCd"] = this.txtTOKUI_CD.Text;             //得意先ＣＤ
            dtHeader_jhMain.Rows[j]["KmanCd"] = this.txtKMAN_CD.Text;               //担
            dtHeader_jhMain.Rows[j]["OkuriFlag"] = GetCheckBoxValue(chkOKURI_FLAG); //送元
            dtHeader_jhMain.Rows[j]["SokoCd"] = this.cmbSOKO_CD.GetSelectedCode();  //倉庫
            dtHeader_jhMain.Rows[j]["GenbaCd"] = this.txtGENBA_CD.Text;             //現場
            dtHeader_jhMain.Rows[j]["Bincd"] = this.cmbBINCD.GetSelectedCode();     //扱便
            dtHeader_jhMain.Rows[j]["Unchin"] = this.cmbUNCHIN.GetSelectedCode();   //運賃
            dtHeader_jhMain.Rows[j]["Nohin"] = this.cmbNOHIN.GetSelectedCode();     //区分
            dtHeader_jhMain.Rows[j]["Nouki"] = this.dateNOUKI.Value;                //納日
            dtHeader_jhMain.Rows[j]["CustOrderno"] = this.txtCUST_ORDERNO.Text;     //客注
            dtHeader_jhMain.Rows[j]["NoteHouse"] = this.txtNOTE_HOUSE.Text;         //社内備考
            dtHeader_jhMain.Rows[j]["Hkbn"] = this.cmbHKBN.GetSelectedCode();       //発注区分
            dtHeader_jhMain.Rows[j]["ShiresakiCd"] = this.txtSHIRESAKI_CD.Text;     //仕入先ＣＤ
            dtHeader_jhMain.Rows[j]["Sirainm"] = this.txtSIRAINM.Text;              //依頼
            dtHeader_jhMain.Rows[j]["Sfax"] = this.txtSFAX.Text;                    //ＦＡＸ
            dtHeader_jhMain.Rows[j]["HatNyukabi"] = this.dateHAT_NYUKABI.Value;     //入荷日
            dtHeader_jhMain.Rows[j]["HatOrderNo"] = this.txtHAT_ORDER_NO.Text;      //ＨＡＴ注番
                                                                                    //this.chkTokuisakiHihyouji.Checked = false; = dtHeader_jhMain.Rows[j][""].ToString();
            dtHeader_jhMain.Rows[j]["TelRenrakuFlg"] = GetCheckBoxValue(chkTEL_RENRAKU_FLG);//電話連絡済
            dtHeader_jhMain.Rows[j]["Kessai"] = GetCheckBoxValue(chkKESSAI);        //決済
            dtHeader_jhMain.Rows[j]["KoujitenCd"] = this.txtKOUJITEN_CD.Text;       //工事店ＣＤ
                                                                                    //this.chkKTankaAuto.Checked = false; = dtHeader_jhMain.Rows[j][""].ToString();
            dtHeader_jhMain.Rows[j]["AnswerName"] = this.txtANSWER_NAME.Text;       //回答者
            dtHeader_jhMain.Rows[j]["RecvPostcode"] = this.txtRECV_POSTCODE.Text;   //〒
            dtHeader_jhMain.Rows[j]["RecvAdd1"] = this.txtRECV_ADD1.Text;           //住所1
            dtHeader_jhMain.Rows[j]["RecvAdd2"] = this.txtRECV_ADD2.Text;           //住所2
            dtHeader_jhMain.Rows[j]["RecvAdd3"] = this.txtRECV_ADD3.Text;           //住所3
            dtHeader_jhMain.Rows[j]["RecvTel"] = this.txtRECV_TEL.Text;             //TEL
            dtHeader_jhMain.Rows[j]["RecvName1"] = this.txtRECV_NAME1.Text;         //宛先1
            dtHeader_jhMain.Rows[j]["RecvName2"] = this.txtRECV_NAME2.Text;         //宛先2
            dtHeader_jhMain.Rows[j]["OrderMemo1"] = this.txtORDER_MEMO1.Text;       //発注時メモ
        }
        private int GetPageNoFromDenSort(int intDenSort)
        {
            int intPage = 1;
            if (dtHeader_jhMain.Rows.Count > 0)
            {
                for (int i = 0; i < dtHeader_jhMain.Rows.Count; i++)
                {
                    if (int.Parse(dtHeader_jhMain.Rows[i]["DenSort"].ToString()).Equals(intDenSort))
                    {
                        intPage = i + 1;
                        break;
                    }
                }
            }
            return intPage;
        }
        private void ChkHeaderSyouhin_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.UseSuggest = chkHeaderSyouhin.Checked;
            }
        }

        /// <summary>発注状態のラベルを更新する</summary>
        /// <param name="jHOrderState">発注状態</param>
        private void SetOrderState(JHOrderState jHOrderState)
        {
            var label = _orderStateRepo.Entities.Find(opt => opt.Key == jHOrderState);
            // 伝票区分が[15:取次]または[21:直送]＝外部に発注
            string denFlg = cmbDEN_FLG.GetSelectedCode();
            var isNeedOrder = new[] { "15", "21" }.Contains(denFlg);
            // F12:受注照合済みの場合は伝区に応じて追加情報を表示
            if (jHOrderState.IsCompleted)
            {
                var hasCompletedDetail = GetCurDetails()
                    .Any(x => GetDTNullableValue<short>(x, "GCompleteFlg") == 1);
                var printedLabel = DenShippingPrinted == true ? "（出荷指示書印刷済）" : "（出荷指示書未印刷）";

                var displayLabel = hasCompletedDetail ? "（売上確定済）" : (denFlg == "21" ? string.Empty : printedLabel);
                this.txtroHattyuJyoutaiName.Text = $"{label?.Name}{displayLabel}";
            }
            else
            {
                this.txtroHattyuJyoutaiName.Text = label?.Name;
            }
            this.txtroHattyuJyoutai.Text = jHOrderState;
        }

        /// <summary>HAT/HAT以外のラジオボタンが選択された</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void RadioHAT_CheckedChanged(object sender, EventArgs e)
        {
            InitWarehouseComboBox(this.cmbSOKO_CD);
            foreach(var row in _detailRows)
            {
                InitWarehouseComboBox(row.cmbSOKO_CD);
            }
        }

        /// <summary>倉庫用のコンボボックスを初期化する</summary>
        /// <param name="comboBox">コンボボックス</param>
        private void InitWarehouseComboBox(ComboBoxEx comboBox)
        {
            var wareHouses = this.clientRepo.Options.DivSokos
                // 倉庫コード07だけは共通
                .Where(x => x.IsHatWarehouse == radioHAT.Checked || x.WhCode == "07")
                .Select(x => $"{x.WhCode}:{x.WhName}").ToList();
            comboBox.Text = string.Empty;
            _sokoCode.Value = string.Empty;
            comboBox.SetItems(wareHouses);
        }

        /// <summary>扱い便のコンボボックスを初期化する</summary>
        private void InitBinComboBox()
        {
            var bin = this.clientRepo.Options.DivBins
                .Where(x => x.WhCd == this.cmbSOKO_CD.GetSelectedCode())
                .Select(x => $"{x.BinCd}:{x.BinName}").ToList();
            this.cmbBINCD.Text = string.Empty;
            this.cmbBINCD.SetItems(bin);
        }

        /// <summary>倉庫コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void CmbSOKO_CD_Validated(object sender, EventArgs e)
        {
            _sokoCode.Value = cmbSOKO_CD.GetSelectedCode();
        }
        #endregion

        #region << 明細部制御 >>
        private void SetDataDetailInit()
        {
            dtDetail_jhMain.Columns.Add("SaveKey", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OrderNo", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OrderNoLine", typeof(string));     // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OrderState", typeof(string));      // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("DenNo", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("DenSort", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("DenSortInt", typeof(int));         //　画面対応なし》整列用に追加
            dtDetail_jhMain.Columns.Add("DenNoLine", typeof(string));       // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("EstimateNo", typeof(string));      // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("EstCoNo", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("LineNo", typeof(int));             // 《画面対応なし》(FosJyuchuD)int
            dtDetail_jhMain.Columns.Add("Koban", typeof(int));              // 子番
            dtDetail_jhMain.Columns.Add("SyobunCd", typeof(string));        // 分類(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("SyohinCd", typeof(string));        // 商品コード・名称(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("SyohinName", typeof(string));      // 商品コード・名称(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Code5", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Kikaku", typeof(string));          // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Suryo", typeof(int));              // 数量(FosJyuchuD)int
            dtDetail_jhMain.Columns.Add("Tani", typeof(string));            // 単位(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Bara", typeof(int));               // バラ数(FosJyuchuD)int
            dtDetail_jhMain.Columns.Add("TeiKigou", typeof(string));        // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("TeiKake", typeof(decimal));        // 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("TeiTan", typeof(decimal));         // 定価単価(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("TeiKin", typeof(decimal));         // 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("Uauto", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("UriKigou", typeof(string));        // 売上記号(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("UriKake", typeof(decimal));        // 掛率(売上)(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("UriTan", typeof(decimal));         // 売上単価(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("UriKin", typeof(decimal));         // 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("UriType", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Gauto", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("SiiKigou", typeof(string));        // 仕入記号(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("SiiKake", typeof(decimal));        // 掛率(仕入)(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("SiiTan", typeof(decimal));         // 仕入単価(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("SiiAnswTan", typeof(decimal));     // 回答単価(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("SiiKin", typeof(decimal));         // 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Columns.Add("Nouki", typeof(DateTime));         // 納日(FosJyuchuD)DateTime
            dtDetail_jhMain.Columns.Add("TaxFlg", typeof(string));          // 消費税(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Dencd", typeof(int));              // 《画面対応なし》(FosJyuchuD)int
            dtDetail_jhMain.Columns.Add("Urikubn", typeof(string));         // 売区(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("SokoCd", typeof(string));          // 倉庫(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Chuban", typeof(string));          // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("ReqNouki", typeof(DateTime));      // 《画面対応なし》(FosJyuchuD)DateTime
            dtDetail_jhMain.Columns.Add("ShiresakiCd", typeof(string));     // 仕入(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Sbiko", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Lbiko", typeof(string));           // 行備考(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Locat", typeof(string));           // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OrderDenNo", typeof(string));      // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OrderDenLineNo", typeof(string));  // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("DelFlg", typeof(string));          // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("MoveFlg", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("InpDate", typeof(DateTime));       // 《画面対応なし》(FosJyuchuD)DateTime
            dtDetail_jhMain.Columns.Add("Dseq", typeof(string));            // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Hinban", typeof(string));          // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("AddDetailFlg", typeof(string));    // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OyahinFlg", typeof(string));       // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("Oyahinb", typeof(string));         // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("HopeOrderNo", typeof(string));     // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("HopeMeisaiNo", typeof(string));    // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("EcoFlg", typeof(string));          // 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Columns.Add("OpsOrderNo", typeof(string));      // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsRecYmd", typeof(DateTime));     // 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Columns.Add("OpsLineno", typeof(string));       // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsSokocd", typeof(string));       // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsShukkadt", typeof(DateTime));   // 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Columns.Add("OpsNyukabi", typeof(DateTime));    // 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Columns.Add("OpsKonpo", typeof(int));           // 《画面対応なし》(FosJyuchuDOp)int
            dtDetail_jhMain.Columns.Add("OpsTani", typeof(string));         // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsBara", typeof(int));            // 《画面対応なし》(FosJyuchuDOp)int
            dtDetail_jhMain.Columns.Add("OpsUtanka", typeof(decimal));      // 《画面対応なし》(FosJyuchuDOp)decimal
            dtDetail_jhMain.Columns.Add("OpsUauto", typeof(string));        // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsUkigo", typeof(string));        // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsUritu", typeof(decimal));       // 《画面対応なし》(FosJyuchuDOp)decimal
            dtDetail_jhMain.Columns.Add("OpsSyohinCd", typeof(string));     // 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Columns.Add("OpsKikaku", typeof(string));       // 《画面対応なし》(FosJyuchuDOp)string
            // 20240228 ADD
            dtDetail_jhMain.Columns.Add("GOrderNo", typeof(string));       /// 《GLASS.受注データ.受注番号》
            dtDetail_jhMain.Columns.Add("GReserveQty", typeof(short));      /// 《GLASS.受注データ.引当数量》
            dtDetail_jhMain.Columns.Add("GDeliveryOrderQty", typeof(short));/// 《GLASS.受注データ.出荷指示数量》
            dtDetail_jhMain.Columns.Add("GDeliveredQty", typeof(short));    /// 《GLASS.受注データ.出荷済数量》
            dtDetail_jhMain.Columns.Add("GCompleteFlg", typeof(short));     /// 《GLASS.受注データ.完了フラグ》
            dtDetail_jhMain.Columns.Add("GDiscount", typeof(short));        /// 《GLASS.受注データ.値引金額》
            dtDetail_jhMain.Columns.Add("CreateDate", typeof(DateTime));    /// 《GLASS.受注データ.作成日時》
            dtDetail_jhMain.Columns.Add("Creator", typeof(int));            /// 《GLASS.受注データ.作成者》
            dtDetail_jhMain.Columns.Add("UpdateDate", typeof(DateTime));    /// 《GLASS.受注データ.更新日時》
            dtDetail_jhMain.Columns.Add("Updater", typeof(int));            /// 《GLASS.受注データ.更新者》


            /*
    /// <summary>
    /// 《GLASS.受注データ.作成者》
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新日時》
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新者》
    /// </summary>
    public int? Updater { get; set; }
             */

            //dtDetail_jhMain.Columns.Add("SiireBikou", typeof(string));      // 仕入備考
        }
        private void RowUpdateBegin()
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                GuiUtil.BeginUpdate(jH_Main_Detail);
            }
        }
        private void RowUpdateEnd()
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                GuiUtil.EndUpdate(jH_Main_Detail);
            }
        }
        private void SetDataDetailToCrt(JH_Main_Detail jH_Main_Detail, int i)
        {
            if (dtDetail_jhMain.Rows.Count == 0) { return; }
            jH_Main_Detail.RowNo = HatFComParts.DoParseInt(dtDetail_jhMain.Rows[i]["DenNoLine"]);
            jH_Main_Detail.Koban = HatFComParts.DoParseShort(dtDetail_jhMain.Rows[i]["Koban"]);
            jH_Main_Detail.txtSYOBUN_CD.Text = dtDetail_jhMain.Rows[i]["SyobunCd"].ToString();
            jH_Main_Detail.SyobunCd = dtDetail_jhMain.Rows[i]["SyobunCd"].ToString();
            jH_Main_Detail.SyohinCode = dtDetail_jhMain.Rows[i]["SyohinCd"].ToString();
            jH_Main_Detail.cmbURIKUBN.SetSelectedCode(dtDetail_jhMain.Rows[i]["Urikubn"].ToString());
            jH_Main_Detail.numSURYO.Text = dtDetail_jhMain.Rows[i]["Suryo"].ToString();
            jH_Main_Detail.Suuryo = HatFComParts.DoParseInt(dtDetail_jhMain.Rows[i]["Suryo"]);
            jH_Main_Detail.txtTANI.Text = dtDetail_jhMain.Rows[i]["Tani"].ToString();
            jH_Main_Detail.Tani = dtDetail_jhMain.Rows[i]["Tani"].ToString();
            jH_Main_Detail.Bara = HatFComParts.DoParseInt(dtDetail_jhMain.Rows[i]["Bara"]);
            jH_Main_Detail.TeikaTanka = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["TeiTan"]);

            DateTime dt = DateTime.Parse("1900/01/01");
            DateTime dtNow = DateTime.Now;
            jH_Main_Detail.dateNOUKI.Value = dtDetail_jhMain.Rows[i]["Nouki"];
            if (jH_Main_Detail.dateNOUKI.Visible)
            {
                jH_Main_Detail.dateNOUKI.Text = HatFComParts.DoFormatYYMMDD(jH_Main_Detail.dateNOUKI.Value);
            }
            else
            {
                jH_Main_Detail.dateNOUKI.Visible = true;
                jH_Main_Detail.dateNOUKI.Text = HatFComParts.DoFormatYYMMDD(jH_Main_Detail.dateNOUKI.Value);
                jH_Main_Detail.dateNOUKI.Visible = false;
            }

            jH_Main_Detail.ShiireKaitouTanka = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["SiiAnswTan"]);

            jH_Main_Detail.UriKigou = dtDetail_jhMain.Rows[i]["UriKigou"].ToString();
            jH_Main_Detail.Urikake = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["UriKake"]);
            jH_Main_Detail.UriTan = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["UriTan"]);
            jH_Main_Detail.cmbSOKO_CD.SetSelectedCode(dtDetail_jhMain.Rows[i]["SokoCd"].ToString());
            jH_Main_Detail.ShiresakiCode = dtDetail_jhMain.Rows[i]["ShiresakiCd"].ToString();
            jH_Main_Detail.SiiKigou = dtDetail_jhMain.Rows[i]["SiiKigou"].ToString();
            jH_Main_Detail.SiiKake = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["SiiKake"]);
            jH_Main_Detail.SiiTan = HatFComParts.DoParseDecimal(dtDetail_jhMain.Rows[i]["SiiTan"]);
            jH_Main_Detail.LBiko = dtDetail_jhMain.Rows[i]["Lbiko"].ToString();
            jH_Main_Detail.TaxFlg = dtDetail_jhMain.Rows[i]["TaxFlg"].ToString();

            jH_Main_Detail.CalcSiireDivTeika();
            jH_Main_Detail.CalcProfit();
            jH_Main_Detail.SiireBiko = dtDetail_jhMain.Rows[i]["Sbiko"].ToString();
            jH_Main_Detail.lblDEL_FLG.Visible = dtDetail_jhMain.Rows[i]["DelFlg"].ToString() == "1";
        }
        private void SetDataDetailToTbl(JH_Main_Detail jH_Main_Detail, int i)
        {
            if (dtDetail_jhMain.Rows.Count == 0) { return; }
            dtDetail_jhMain.Rows[i]["DenNoLine"] = jH_Main_Detail.txtroRowNo.Text;
            dtDetail_jhMain.Rows[i]["Koban"] = (object)HatFComParts.DoParseInt(jH_Main_Detail.txtKoban.Text) ?? DBNull.Value;
            dtDetail_jhMain.Rows[i]["SyobunCd"] = jH_Main_Detail.txtSYOBUN_CD.Text;
            dtDetail_jhMain.Rows[i]["SyohinCd"] = jH_Main_Detail.txtSYOHIN_CD.Text;

            dtDetail_jhMain.Rows[i]["Urikubn"] = jH_Main_Detail.cmbURIKUBN.GetSelectedCode();

            //jH_Main_Detail.txtroZaikoSuu.text;

            var suryo = HatFComParts.DoParseInt(jH_Main_Detail.numSURYO.Text);
            dtDetail_jhMain.Rows[i]["Suryo"] = (suryo == null) ? DBNull.Value : suryo;

            dtDetail_jhMain.Rows[i]["Tani"] = jH_Main_Detail.txtTANI.Text;

            var bara = HatFComParts.DoParseInt(jH_Main_Detail.numBARA.Text);
            dtDetail_jhMain.Rows[i]["Bara"] = (bara == null) ? DBNull.Value : bara;

            var teitan = HatFComParts.DoParseDecimal(jH_Main_Detail.decTEI_TAN.Text);
            dtDetail_jhMain.Rows[i]["TeiTan"] = (teitan == null) ? DBNull.Value : teitan;

            dtDetail_jhMain.Rows[i]["Nouki"] = jH_Main_Detail.dateNOUKI.Value;

            var siianswtan = HatFComParts.DoParseDecimal(jH_Main_Detail.decSII_ANSW_TAN.Text);
            dtDetail_jhMain.Rows[i]["SiiAnswTan"] = (siianswtan == null) ? DBNull.Value : siianswtan;

            dtDetail_jhMain.Rows[i]["UriKigou"] = jH_Main_Detail.txtURI_KIGOU.Text;

            var urikake = HatFComParts.DoParseDecimal(jH_Main_Detail.decURI_KAKE.Text);
            dtDetail_jhMain.Rows[i]["UriKake"] = (urikake == null) ? DBNull.Value : urikake;

            var uritan = HatFComParts.DoParseDecimal(jH_Main_Detail.decURI_TAN.Text);
            dtDetail_jhMain.Rows[i]["UriTan"] = (uritan == null) ? DBNull.Value : uritan;

            dtDetail_jhMain.Rows[i]["SokoCd"] = jH_Main_Detail.cmbSOKO_CD.GetSelectedCode();
            dtDetail_jhMain.Rows[i]["ShiresakiCd"] = jH_Main_Detail.txtSHIRESAKI_CD.Text;
            dtDetail_jhMain.Rows[i]["SiiKigou"] = jH_Main_Detail.txtSII_KIGOU.Text;

            var siikake = HatFComParts.DoParseDecimal(jH_Main_Detail.decSII_KAKE.Text);
            dtDetail_jhMain.Rows[i]["SiiKake"] = (siikake == null) ? DBNull.Value : siikake;

            var siitan = HatFComParts.DoParseDecimal(jH_Main_Detail.decSII_TAN.Text);
            dtDetail_jhMain.Rows[i]["SiiTan"] = (siitan == null) ? DBNull.Value : siitan;

            //jH_Main_Detail.txtroNoTankaWariai.text;

            dtDetail_jhMain.Rows[i]["Lbiko"] = jH_Main_Detail.txtLBIKO.Text;
            dtDetail_jhMain.Rows[i]["TaxFlg"] = jH_Main_Detail.txtTAX_FLG.Text;

            dtDetail_jhMain.Rows[i]["Sbiko"] = jH_Main_Detail.txtSiireBikou.Text;   // 20240309

            dtDetail_jhMain.AcceptChanges();
        }
        private void CalcFooterSummaryArea()
        {
            var rows = Enumerable.Range(1, 6).Select(i => GetUcName(i)).ToList();
            //フッター.定価 Σ([行.定価単価]×[行.バラ数]）現在の伝票ページの定価合計額を表示する
            decimal decTeiSum = rows.Sum(r => (r.TeikaTanka ?? 0) * (r.Bara ?? 0));
            //フッター.売額 Σ([行.売上単価]×[行.バラ数]）現在の伝票ページの売上合計額を表示する
            decimal decUriSum = rows.Sum(r => (r.UriTan ?? 0) * (r.Bara ?? 0));
            //フッター.仕額 Σ([行.仕入単価]×[行.バラ数]）現在の伝票ページの仕入合計額を表示する
            decimal decSiiSum = rows.Sum(r => (r.SiiTan ?? 0) * (r.Bara ?? 0));
            //フッター.粗利 [フッター.売額]-[フッター.仕額]現在の伝票ページの粗利を表示する
            decimal decArari = decUriSum - decSiiSum;
            //フッター.利率 {[フッター.粗利]÷[フッター.売額]×100}※小数点第2位を四捨五入 現在の伝票ページの利率を表示する
            decimal decRiritu = 0;
            if (decUriSum != 0)
            {
                decRiritu = decArari / decUriSum * 100;
            }
            this.txtroFooterTeika.Text = HatFComParts.DoFormatN0(decTeiSum);
            this.txtroFooterBaigaku.Text = HatFComParts.DoFormatN0(decUriSum);
            this.txtroFooterShigaku.Text = HatFComParts.DoFormatN0(decSiiSum);
            this.txtroFooterArari.Text = HatFComParts.DoFormatN0(decArari);
            this.txtroFooterRiritsu.Text = HatFComParts.DoFormatN1(decRiritu);
        }
        private void CalcFooterSummaryArea_Validated(object sender, EventArgs e)
        {
            CalcFooterSummaryArea();
        }
        private void SetCalcFooterSummaryAreaValidated()
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.numSURYO.Validated += new EventHandler(this.CalcFooterSummaryArea_Validated);
                jH_Main_Detail.numBARA.Validated += new EventHandler(this.CalcFooterSummaryArea_Validated);
                jH_Main_Detail.decTEI_TAN.Validated += new EventHandler(this.CalcFooterSummaryArea_Validated);
                jH_Main_Detail.decURI_TAN.Validated += new EventHandler(this.CalcFooterSummaryArea_Validated);
                jH_Main_Detail.decSII_TAN.Validated += new EventHandler(this.CalcFooterSummaryArea_Validated);
            }

        }
        private void SetFocusLastRow()
        {
            for (int i = 6; i >= 1; i--)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled) { jH_Main_Detail.Focus(); return; }
            }
        }
        private void SetDataDetailPaste()
        {
            int i = -1;
            int j = -1;
            for (int k = 0; k < dtDetail_jhMain.Rows.Count; k++)
            {
                if (dtDetail_jhMain.Rows[k]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[k]["DenNoLine"].ToString().Equals(strRowNo))
                {
                    i = k;
                }
            }
            for (int k = 0; k < dtDetail_jhMain.Rows.Count; k++)
            {
                if (dtDetail_jhMain.Rows[k]["DenSort"].ToString().Equals(strDenSortCopy) && dtDetail_jhMain.Rows[k]["DenNoLine"].ToString().Equals(strRowNoCopy))
                {
                    j = k;
                }
            }
            if (i == -1 || j == -1) { return; }
            dtDetail_jhMain.Rows[i]["OrderNo"] = dtDetail_jhMain.Rows[j]["OrderNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OrderNoLine"] = dtDetail_jhMain.Rows[j]["OrderNoLine"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OrderState"] = dtDetail_jhMain.Rows[j]["OrderState"];// 《画面対応なし》(FosJyuchuD)string
                                                                                          //dtDetail_jhMain.Rows[i]["DenNo"] = dtDetail_jhMain.Rows[j]["DenNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["EstimateNo"] = dtDetail_jhMain.Rows[j]["EstimateNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["EstCoNo"] = dtDetail_jhMain.Rows[j]["EstCoNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["LineNo"] = dtDetail_jhMain.Rows[j]["LineNo"];// 《画面対応なし》(FosJyuchuD)int
            dtDetail_jhMain.Rows[i]["SyobunCd"] = dtDetail_jhMain.Rows[j]["SyobunCd"];// 分類(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["SyohinCd"] = dtDetail_jhMain.Rows[j]["SyohinCd"];// 商品コード・名称(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["SyohinName"] = dtDetail_jhMain.Rows[j]["SyohinName"];// 商品コード・名称(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Code5"] = dtDetail_jhMain.Rows[j]["Code5"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Kikaku"] = dtDetail_jhMain.Rows[j]["Kikaku"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Suryo"] = dtDetail_jhMain.Rows[j]["Suryo"];// 数量(FosJyuchuD)int
            dtDetail_jhMain.Rows[i]["Tani"] = dtDetail_jhMain.Rows[j]["Tani"];// 単位(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Bara"] = dtDetail_jhMain.Rows[j]["Bara"];// バラ数(FosJyuchuD)int
            dtDetail_jhMain.Rows[i]["TeiKigou"] = dtDetail_jhMain.Rows[j]["TeiKigou"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["TeiKake"] = dtDetail_jhMain.Rows[j]["TeiKake"];// 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["TeiTan"] = dtDetail_jhMain.Rows[j]["TeiTan"];// 定価単価(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["TeiKin"] = dtDetail_jhMain.Rows[j]["TeiKin"];// 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["Uauto"] = dtDetail_jhMain.Rows[j]["Uauto"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["UriKigou"] = dtDetail_jhMain.Rows[j]["UriKigou"];// 売上記号(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["UriKake"] = dtDetail_jhMain.Rows[j]["UriKake"];// 掛率(売上)(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["UriTan"] = dtDetail_jhMain.Rows[j]["UriTan"];// 売上単価(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["UriKin"] = dtDetail_jhMain.Rows[j]["UriKin"];// 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["UriType"] = dtDetail_jhMain.Rows[j]["UriType"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Gauto"] = dtDetail_jhMain.Rows[j]["Gauto"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["SiiKigou"] = dtDetail_jhMain.Rows[j]["SiiKigou"];// 仕入記号(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["SiiKake"] = dtDetail_jhMain.Rows[j]["SiiKake"];// 掛率(仕入)(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["SiiTan"] = dtDetail_jhMain.Rows[j]["SiiTan"];// 仕入単価(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["SiiAnswTan"] = dtDetail_jhMain.Rows[j]["SiiAnswTan"];// 回答単価(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["SiiKin"] = dtDetail_jhMain.Rows[j]["SiiKin"];// 《画面対応なし》(FosJyuchuD)decimal
            dtDetail_jhMain.Rows[i]["Nouki"] = dtDetail_jhMain.Rows[j]["Nouki"];// 納日(FosJyuchuD)DateTime
            dtDetail_jhMain.Rows[i]["TaxFlg"] = dtDetail_jhMain.Rows[j]["TaxFlg"];// 消費税(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Dencd"] = dtDetail_jhMain.Rows[j]["Dencd"];// 《画面対応なし》(FosJyuchuD)int
            dtDetail_jhMain.Rows[i]["Urikubn"] = dtDetail_jhMain.Rows[j]["Urikubn"];// 売区(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["SokoCd"] = dtDetail_jhMain.Rows[j]["SokoCd"];// 倉庫(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Chuban"] = dtDetail_jhMain.Rows[j]["Chuban"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["ReqNouki"] = dtDetail_jhMain.Rows[j]["ReqNouki"];// 《画面対応なし》(FosJyuchuD)DateTime
            dtDetail_jhMain.Rows[i]["ShiresakiCd"] = dtDetail_jhMain.Rows[j]["ShiresakiCd"];// 仕入(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Sbiko"] = dtDetail_jhMain.Rows[j]["Sbiko"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Lbiko"] = dtDetail_jhMain.Rows[j]["Lbiko"];// 行備考(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Locat"] = dtDetail_jhMain.Rows[j]["Locat"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OrderDenNo"] = dtDetail_jhMain.Rows[j]["OrderDenNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OrderDenLineNo"] = dtDetail_jhMain.Rows[j]["OrderDenLineNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["DelFlg"] = dtDetail_jhMain.Rows[j]["DelFlg"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["MoveFlg"] = dtDetail_jhMain.Rows[j]["MoveFlg"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["InpDate"] = dtDetail_jhMain.Rows[j]["InpDate"];// 《画面対応なし》(FosJyuchuD)DateTime
            dtDetail_jhMain.Rows[i]["Dseq"] = dtDetail_jhMain.Rows[j]["Dseq"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Hinban"] = dtDetail_jhMain.Rows[j]["Hinban"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["AddDetailFlg"] = dtDetail_jhMain.Rows[j]["AddDetailFlg"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OyahinFlg"] = dtDetail_jhMain.Rows[j]["OyahinFlg"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["Oyahinb"] = dtDetail_jhMain.Rows[j]["Oyahinb"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["HopeOrderNo"] = dtDetail_jhMain.Rows[j]["HopeOrderNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["HopeMeisaiNo"] = dtDetail_jhMain.Rows[j]["HopeMeisaiNo"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["EcoFlg"] = dtDetail_jhMain.Rows[j]["EcoFlg"];// 《画面対応なし》(FosJyuchuD)string
            dtDetail_jhMain.Rows[i]["OpsOrderNo"] = dtDetail_jhMain.Rows[j]["OpsOrderNo"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsRecYmd"] = dtDetail_jhMain.Rows[j]["OpsRecYmd"];// 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Rows[i]["OpsLineno"] = dtDetail_jhMain.Rows[j]["OpsLineno"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsSokocd"] = dtDetail_jhMain.Rows[j]["OpsSokocd"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsShukkadt"] = dtDetail_jhMain.Rows[j]["OpsShukkadt"];// 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Rows[i]["OpsNyukabi"] = dtDetail_jhMain.Rows[j]["OpsNyukabi"];// 《画面対応なし》(FosJyuchuDOp)DateTime
            dtDetail_jhMain.Rows[i]["OpsKonpo"] = dtDetail_jhMain.Rows[j]["OpsKonpo"];// 《画面対応なし》(FosJyuchuDOp)int
            dtDetail_jhMain.Rows[i]["OpsTani"] = dtDetail_jhMain.Rows[j]["OpsTani"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsBara"] = dtDetail_jhMain.Rows[j]["OpsBara"];// 《画面対応なし》(FosJyuchuDOp)int
            dtDetail_jhMain.Rows[i]["OpsUtanka"] = dtDetail_jhMain.Rows[j]["OpsUtanka"];// 《画面対応なし》(FosJyuchuDOp)decimal
            dtDetail_jhMain.Rows[i]["OpsUauto"] = dtDetail_jhMain.Rows[j]["OpsUauto"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsUkigo"] = dtDetail_jhMain.Rows[j]["OpsUkigo"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsUritu"] = dtDetail_jhMain.Rows[j]["OpsUritu"];// 《画面対応なし》(FosJyuchuDOp)decimal
            dtDetail_jhMain.Rows[i]["OpsSyohinCd"] = dtDetail_jhMain.Rows[j]["OpsSyohinCd"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["OpsKikaku"] = dtDetail_jhMain.Rows[j]["OpsKikaku"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["GOrderNo"] = dtDetail_jhMain.Rows[j]["GOrderNo"];// 《画面対応なし》(FosJyuchuDOp)string

            dtDetail_jhMain.Rows[i]["GReserveQty"] = dtDetail_jhMain.Rows[j]["GReserveQty"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["GDeliveryOrderQty"] = dtDetail_jhMain.Rows[j]["GDeliveryOrderQty"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["GDeliveredQty"] = dtDetail_jhMain.Rows[j]["GDeliveredQty"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["GCompleteFlg"] = dtDetail_jhMain.Rows[j]["GCompleteFlg"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["GDiscount"] = dtDetail_jhMain.Rows[j]["GDiscount"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["CreateDate"] = dtDetail_jhMain.Rows[j]["CreateDate"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["Creator"] = dtDetail_jhMain.Rows[j]["Creator"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["UpdateDate"] = dtDetail_jhMain.Rows[j]["UpdateDate"];// 《画面対応なし》(FosJyuchuDOp)string
            dtDetail_jhMain.Rows[i]["Updater"] = dtDetail_jhMain.Rows[j]["Updater"];// 《画面対応なし》(FosJyuchuDOp)string

            dtDetail_jhMain.AcceptChanges();
        }
        private void DataPageAdd()
        {

            string strNewDenSort = (HatFComParts.DoParseInt(strDenSort) + 1).ToString();
            strDenSort = strNewDenSort;
            strDenSortAddPage = strNewDenSort;

            var dtHeader = dtHeader_jhMain.NewRow();
            dtHeader["SaveKey"] = strSaveKey;
            dtHeader["DenSort"] = strNewDenSort;
            dtHeader["DenSortInt"] = int.Parse(strNewDenSort);
            dtHeader_jhMain.Rows.Add(dtHeader);
            dtHeader_jhMain.AcceptChanges();

            var dtDetail = dtDetail_jhMain.NewRow();
            dtDetail["SaveKey"] = strSaveKey;
            dtDetail["DenSort"] = strNewDenSort;
            dtDetail["DenSortInt"] = int.Parse(strNewDenSort);
            dtDetail["DenNoLine"] = @"1";
            dtDetail_jhMain.Rows.Add(dtDetail);
            dtDetail_jhMain.AcceptChanges();

            dtHeader_jhMain.DefaultView.Sort = "SaveKey,DenSortInt";
            dtHeader_jhMain = dtHeader_jhMain.DefaultView.ToTable(true);
            dtDetail_jhMain.DefaultView.Sort = "SaveKey,DenSortInt,DenNoLine";
            dtDetail_jhMain = dtDetail_jhMain.DefaultView.ToTable(true);
        }
        private void DataDetailAdd()
        {
            int j = 0;
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort))
                {
                    j++;
                }
            }
            if (dtDetail_jhMain.Rows.Count.Equals(j + 1))
            {
                //最終行
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["SaveKey"] = strSaveKey;
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = (j + 1).ToString();
                dtDetail_jhMain.Rows.Add(dtDetail);
                dtDetail_jhMain.AcceptChanges();
            }
            else
            {
                //途中行
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["SaveKey"] = strSaveKey;
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = (j + 1).ToString();
                dtDetail_jhMain.Rows.InsertAt(dtDetail, j + 1);
                dtDetail_jhMain.AcceptChanges();
            }
            dtDetail_jhMain.DefaultView.Sort = "SaveKey,DenSortInt,DenNoLine";
            dtDetail_jhMain = dtDetail_jhMain.DefaultView.ToTable(true);
        }
        private void DataDetailDel()
        {
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[i]["DenNoLine"].ToString().Equals(strRowNo))
                {
                    dtDetail_jhMain.Rows[i].Delete();
                    dtDetail_jhMain.AcceptChanges();
                    break;
                }
            }
            if (dtDetail_jhMain.Rows.Count == 0)
            {
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = @"1";
                dtDetail_jhMain.Rows.Add(dtDetail);
                dtDetail_jhMain.AcceptChanges();
                return;
            }
        }
        private void ShowDetailPageData()
        {
            strRowNo = @"1";
            bool bExistFlg = false;
            if (dtHeader_jhMain == null || dtDetail_jhMain == null)
            {
                return;
            }
            if (dtHeader_jhMain.Rows.Count == 0 || dtDetail_jhMain.Rows.Count == 0)
            {
                for (int i = 2; i <= 6; i++)
                {
                    JH_Main_Detail jH_Main_Detail = GetUcName(i);
                    jH_Main_Detail.Enabled = false;
                }
            }

            int k = 1;
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort))
                {
                    bExistFlg = true;
                    JH_Main_Detail jH_Main_Detail = GetUcName(k);
                    jH_Main_Detail.Enabled = true;
                    jH_Main_Detail.txtroRowNo.Text = (k).ToString();
                    SetDataDetailToCrt(jH_Main_Detail, i);
                    k++;
                }
            }
            if (!bExistFlg)
            {
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["SaveKey"] = strSaveKey;
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = @"1";
                dtDetail_jhMain.Rows.Add(dtDetail);
                dtDetail_jhMain.AcceptChanges();
                dtDetail_jhMain.DefaultView.Sort = "SaveKey,DenSortInt,DenNoLine";
                dtDetail_jhMain = dtDetail_jhMain.DefaultView.ToTable(true);

                ucRow1.txtroRowNo.Text = @"1";
            }


            if (k.Equals(1)) { k = 2; }
            for (int j = k; j <= 6; j++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(j);
                jH_Main_Detail.Enabled = false;
            }

            CalcFooterSummaryArea();

            // 20240309
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.UseSuggest = chkHeaderSyouhin.Checked;
            }
        }
        private void SetDetailData()
        {
            if (strRowNo.Length.Equals(0)) { return; }
            JH_Main_Detail jH_Main_Detail = GetUcName(int.Parse(strRowNo));
            SetDataDetailToTbl(jH_Main_Detail, GetDetailArrayNo());
        }
        private int GetDetailArrayNo()
        {
            int intTmp = 0;
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[i]["DenNoLine"].ToString().Equals(strRowNo))
                {
                    intTmp = i;
                    break;
                }
            }

            return intTmp;
        }
        private JH_Main_Detail GetUcName(int intRowNo)
        {
            JH_Main_Detail jH_Main_Detail = new JH_Main_Detail();
            switch (intRowNo)
            {
                case 1:
                    jH_Main_Detail = this.ucRow1;
                    break;
                case 2:
                    jH_Main_Detail = this.ucRow2;
                    break;
                case 3:
                    jH_Main_Detail = this.ucRow3;
                    break;
                case 4:
                    jH_Main_Detail = this.ucRow4;
                    break;
                case 5:
                    jH_Main_Detail = this.ucRow5;
                    break;
                case 6:
                    jH_Main_Detail = this.ucRow6;
                    break;
            }
            return jH_Main_Detail;

        }
        private void CngButtonStsDetail(bool bSts)
        {
            this.btnFooterF2.Enabled = bSts && txtroHattyuJyoutai.Text == JHOrderState.Ordered;
            this.btnFooterF3.Enabled = false;
            this.btnFooterF4.Enabled = false;
            this.btnFooterF5.Enabled = false;
            this.btnFooterF6.Enabled = false;
            this.btnFooterF7.Enabled = false;
            this.btnFooterF8.Enabled = false;
            if (bSts && ((JHOrderState)txtroHattyuJyoutai.Text).IsBeforeComplete)
            {
                this.btnFooterF3.Enabled = true;
                this.btnFooterF4.Enabled = true;
                this.btnFooterF5.Enabled = true;
                this.btnFooterF6.Enabled = true;
                this.btnFooterF7.Enabled = true;
                this.btnFooterF8.Enabled = true;
            }
            this.btnFooterF10.Enabled = bSts;
        }
        private void PnlDetail_Enter(object sender, EventArgs e)
        {
            CngButtonStsMain(false);
            CngButtonStsDetail(true);
        }
        private void PnlDetail_Leave(object sender, EventArgs e)
        {
            CngButtonStsMain(true);
            CngButtonStsDetail(false);
        }
        private void PnlDetailRow1_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow1.txtroRowNo.Text;
            SetDetailData();
        }
        private void PnlDetailRow2_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow2.txtroRowNo.Text;
            SetDetailData();
        }
        private void PnlDetailRow3_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow3.txtroRowNo.Text;
            SetDetailData();
        }
        private void PnlDetailRow4_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow4.txtroRowNo.Text;
            SetDetailData();
        }
        private void PnlDetailRow5_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow5.txtroRowNo.Text;
            SetDetailData();
        }
        private void PnlDetailRow6_Leave(object sender, EventArgs e)
        {
            strRowNo = ucRow6.txtroRowNo.Text;
            SetDetailData();
        }
        private void CngShowDetailParts(bool bSts)
        {
            // 伝票分割非表示対応 ここから
            //this.lblHeaderSOKO_CD.Visible = bSts;
            //this.lblHeaderSHIRESAKI_CD.Visible = bSts;
            //this.lblHeader08.Visible = bSts;
            // 伝票分割非表示対応 ここまで
            this.lblHeader09.Visible = bSts;
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                // 伝票分割非表示対応 ここから
                //jH_Main_Detail.cmbSOKO_CD.Visible = bSts;
                //jH_Main_Detail.txtSHIRESAKI_CD.Visible = bSts;
                // 伝票分割非表示対応 ここまで
                jH_Main_Detail.dateNOUKI.Visible = bSts;
                jH_Main_Detail.decSII_ANSW_TAN.Visible = bSts;
            }
        }

        /// <summary>明細部分に１つでも入力があるか確認する</summary>
        /// <returns>１つでも入力がある場合true、入力がない場合false</returns>
        private bool BoolChkDetailDataInput()
        {
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled)
                {
                    if (jH_Main_Detail.txtKoban.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtSYOBUN_CD.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtSYOHIN_CD.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.cmbURIKUBN.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.numSURYO.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtTANI.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.numBARA.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.decTEI_TAN.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.dateNOUKI.Visible && jH_Main_Detail.dateNOUKI.Text.Length > 0 && !jH_Main_Detail.dateNOUKI.Text.Equals(@"__/__/__")) { return true; }
                    if (jH_Main_Detail.decSII_ANSW_TAN.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtURI_KIGOU.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.decURI_KAKE.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.decURI_TAN.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.cmbSOKO_CD.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtSHIRESAKI_CD.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtSII_KIGOU.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.decSII_KAKE.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.decSII_TAN.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtLBIKO.Text.Length > 0) { return true; }
                    if (jH_Main_Detail.txtTAX_FLG.Text.Length > 0) { return true; }
                    //if (jH_Main_Detail.txtSiireBikou.Text.Length > 0) { return true; }
                }
            }
            return false;
        }
        private bool BoolChkDetailDataInputRow(JH_Main_Detail jH_Main_Detail)
        {
            if (jH_Main_Detail.txtKoban.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtSYOBUN_CD.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtSYOHIN_CD.Text.Length > 0) { return true; }
            if (jH_Main_Detail.cmbURIKUBN.Text.Length > 0) { return true; }
            if (jH_Main_Detail.numSURYO.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtTANI.Text.Length > 0) { return true; }
            if (jH_Main_Detail.numBARA.Text.Length > 0) { return true; }
            if (jH_Main_Detail.decTEI_TAN.Text.Length > 0) { return true; }
            if (jH_Main_Detail.dateNOUKI.Visible && jH_Main_Detail.dateNOUKI.Text.Length > 0 && !jH_Main_Detail.dateNOUKI.Text.Equals(@"__/__/__")) { return true; }
            if (jH_Main_Detail.decSII_ANSW_TAN.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtURI_KIGOU.Text.Length > 0) { return true; }
            if (jH_Main_Detail.decURI_KAKE.Text.Length > 0) { return true; }
            if (jH_Main_Detail.decURI_TAN.Text.Length > 0) { return true; }
            if (jH_Main_Detail.cmbSOKO_CD.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtSHIRESAKI_CD.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtSII_KIGOU.Text.Length > 0) { return true; }
            if (jH_Main_Detail.decSII_KAKE.Text.Length > 0) { return true; }
            if (jH_Main_Detail.decSII_TAN.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtLBIKO.Text.Length > 0) { return true; }
            if (jH_Main_Detail.txtTAX_FLG.Text.Length > 0) { return true; }
            //if (jH_Main_Detail.txtSiireBikou.Text.Length > 0) { return true; }
            return false;
        }
        private void SetNextRowActivate()
        {
            for (int i = 1; i <= 5; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.txtKoban.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtSYOBUN_CD.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtSYOHIN_CD.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.cmbURIKUBN.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.numSURYO.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtTANI.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.numBARA.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decTEI_TAN.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.dateNOUKI.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decSII_ANSW_TAN.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtURI_KIGOU.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decURI_KAKE.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decURI_TAN.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.cmbSOKO_CD.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtSHIRESAKI_CD.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtSII_KIGOU.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decSII_KAKE.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.decSII_TAN.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtLBIKO.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                jH_Main_Detail.txtTAX_FLG.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
                //jH_Main_Detail.txtSiireBikou.TextChanged += new EventHandler(ForNextRowActivate_TextChanged);
            }
        }
        private void NextRowActivate()
        {
            if (txtroHattyuJyoutai.Text != JHOrderState.PreOrder) { return; }

            switch (this.ActiveControl.Name)
            {
                case nameof(ucRow1):
                    if (ucRow2.Enabled) { return; }
                    if (!BoolChkDetailDataInputRow(ucRow1)) { return; }
                    ucRow2.Enabled = true;
                    ucRow2.txtroRowNo.Text = @"2";
                    break;
                case nameof(ucRow2):
                    if (ucRow3.Enabled) { return; }
                    if (!BoolChkDetailDataInputRow(ucRow2)) { return; }
                    ucRow3.Enabled = true;
                    ucRow3.txtroRowNo.Text = @"3";
                    break;
                case nameof(ucRow3):
                    if (ucRow4.Enabled) { return; }
                    if (!BoolChkDetailDataInputRow(ucRow3)) { return; }
                    ucRow4.Enabled = true;
                    ucRow4.txtroRowNo.Text = @"4";
                    break;
                case nameof(ucRow4):
                    if (ucRow5.Enabled) { return; }
                    if (!BoolChkDetailDataInputRow(ucRow4)) { return; }
                    ucRow5.Enabled = true;
                    ucRow5.txtroRowNo.Text = @"5";
                    break;
                case nameof(ucRow5):
                    if (ucRow6.Enabled) { return; }
                    if (!BoolChkDetailDataInputRow(ucRow5)) { return; }
                    ucRow6.Enabled = true;
                    ucRow6.txtroRowNo.Text = @"6";
                    break;
                default:
                    return;
            }
            DataDetailAdd();
        }
        private void ForNextRowActivate_TextChanged(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl.Text.Length > 0)
            {
                NextRowActivate();
            }
        }
        private void SetNextRowActivateOnPageShow()
        {
            if (txtroHattyuJyoutai.Text != JHOrderState.PreOrder) { return; }

            //・活性化している最下行の取得
            int intRowNo = 0;
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled) { intRowNo = i; };
            }
            if (intRowNo.Equals(6)) { return; }
            if (!BoolChkDetailDataInputRow(GetUcName(intRowNo))) { GetUcName(intRowNo).Focus(); return; }

            //・データテーブルの添字取得
            int intSoe = -1;
            for (int i = 0; i < dtDetail_jhMain.Rows.Count; i++)
            {
                if (dtDetail_jhMain.Rows[i]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[i]["DenNoLine"].ToString().Equals(intRowNo.ToString()))
                {
                    intSoe = i;
                    break;
                }
            }
            if (intSoe.Equals(-1)) { return; }

            //・データテーブルに空レコードの追加、挿入
            if (dtDetail_jhMain.Rows.Count.Equals(intSoe + 1))
            {
                //最終行
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["SaveKey"] = strSaveKey;
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = (intRowNo + 1).ToString();
                dtDetail_jhMain.Rows.Add(dtDetail);
                dtDetail_jhMain.AcceptChanges();
            }
            else
            {
                //途中行
                var dtDetail = dtDetail_jhMain.NewRow();
                dtDetail["SaveKey"] = strSaveKey;
                dtDetail["DenSort"] = strDenSort;
                dtDetail["DenSortInt"] = int.Parse(strDenSort);
                dtDetail["DenNoLine"] = (intRowNo + 1).ToString();
                dtDetail_jhMain.Rows.InsertAt(dtDetail, intSoe + 1);
                dtDetail_jhMain.AcceptChanges();
            }

            //・次の行の活性化
            if (intRowNo < 6)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(intRowNo + 1);
                jH_Main_Detail.Enabled = true;
                jH_Main_Detail.txtroRowNo.Text = (intRowNo + 1).ToString();
                jH_Main_Detail.Focus();
            }

        }
        private void DelEmptyDataTableRecord()
        {
            //・空行の取得
            //・対応するデータテーブルのレコード削除
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled && !BoolChkDetailDataInputRow(jH_Main_Detail))
                {
                    for (int j = 0; j < dtDetail_jhMain.Rows.Count; j++)
                    {
                        if (dtDetail_jhMain.Rows[j]["DenSort"].ToString().Equals(strDenSort) && dtDetail_jhMain.Rows[j]["DenNoLine"].ToString().Equals(jH_Main_Detail.txtroRowNo.Text))
                        {
                            dtDetail_jhMain.Rows[j].Delete();
                            dtDetail_jhMain.AcceptChanges();
                        }
                    }
                }
            }
            //・DenNoLineの再設定
            int intRowNo = 1;
            for (int j = 0; j < dtDetail_jhMain.Rows.Count; j++)
            {
                if (dtDetail_jhMain.Rows[j]["DenSort"].ToString().Equals(strDenSort))
                {
                    dtDetail_jhMain.Rows[j]["DenNoLine"] = intRowNo.ToString();
                    dtDetail_jhMain.AcceptChanges();
                    intRowNo++;
                }
            }

        }

        /// <summary>明細部分のフォーカス位置に応じてラベルの色を変更するよう設定する</summary>
        private void SetDetailLabelAutoColor()
        {
            var settings = new Dictionary<Control, Func<JH_Main_Detail, Control>>()
            {
                {lblHeader01, row => row.txtroRowNo},
                {label1, row => row.txtKoban},
                {chkHeaderBunrui, row => row.txtSYOBUN_CD},
                {chkHeaderSyouhin, row => row.txtSYOHIN_CD},
                {lblHeader02, row => row.cmbURIKUBN},
                {lblHeader03, row => row.txtroZaikoSuu},
                {lblHeader04, row => row.numSURYO},
                {lblHeader05, row => row.txtTANI},
                {lblHeader06, row => row.numBARA},
                {lblHeader07, row => row.decTEI_TAN},
                {lblHeader08, row => row.dateNOUKI},
                {lblHeader09, row => row.decSII_ANSW_TAN},
                {lblHeader10, row => row.txtroRiritsu},
                {lblHeader11, row => row.txtURI_KIGOU},
                {lblHeader12, row => row.decURI_KAKE},
                {lblHeader13, row => row.decURI_TAN},
                {lblHeaderSOKO_CD, row => row.cmbSOKO_CD},
                {lblHeaderSHIRESAKI_CD, row => row.txtSHIRESAKI_CD},
                {lblHeader14, row => row.txtSII_KIGOU},
                {lblHeader15, row => row.decSII_KAKE},
                {lblHeader16, row => row.decSII_TAN},
                {lblHeader17, row => row.txtroNoTankaWariai},
                {lblHeader18, row => row.txtLBIKO},
                {lblHeader20, row => row.txtTAX_FLG},
            };

            // 各ラベルの元のスタイルを覚えておく
            var beforeColors = new Dictionary<Control, Color>();
            var beforeFonts = new Dictionary<Control, Font>();
            foreach (var kvp in settings)
            {
                beforeColors.Add(kvp.Key, kvp.Key.ForeColor);
                beforeFonts.Add(kvp.Key, kvp.Key.Font);
            }

            // 自動変更の設定
            foreach (var kvp in settings)
            {
                foreach (var row in Enumerable.Range(1, 6).Select(i => GetUcName(i)))
                {
                    var control = kvp.Value(row);
                    // コントロールにフォーカス移動時、対応するラベルの書式を変更する
                    control.Enter += (_, _) =>
                    {
                        kvp.Key.ForeColor = Color.Blue;
                        kvp.Key.Font = new Font(kvp.Key.Font, FontStyle.Bold);
                    };
                    // コントロールからフォーカス移動時、対応するラベルの書式を戻す
                    control.Leave += (_, _) =>
                    {
                        kvp.Key.ForeColor = beforeColors[kvp.Key];
                        kvp.Key.Font = beforeFonts[kvp.Key];
                    };
                }
            }
        }
        #endregion

        #region フッタ部制御
        /// <summary>特異点コードの変更</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void TxtTOKUI_CD_Validated(object sender, EventArgs e)
        {
            // 4270 = 住友林業
            txtKOUJITEN_CD.Enabled = txtTOKUI_CD.Text.StartsWith("4270");
        }
        #endregion

        #region << フル桁時タブ遷移 >>
        private void SetMoveNextControl()
        {
            this.txtJYU2.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtNYU2.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtDEN_NO.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtTEAM_CD.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtTOKUI_CD.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtKMAN_CD.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtGENBA_CD.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtSHIRESAKI_CD.TextChanged += new EventHandler(ForTabOrder_TextChanged);
            this.txtRECV_POSTCODE.TextChanged += new EventHandler(ForTabOrder_TextChanged);

            this.txtDEN_NO.Validated += new EventHandler(ForTabOrder_TxtDEN_Validated);
        }
        private void ForTabOrder_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (tb.MaxLength == tb.Text.Length)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }

        }
        private void ForTabOrder_TxtDEN_Validated(object sender, EventArgs e)
        {
            if (txtDEN_NO.Text.Length > 0 && txtDEN_NO.Text.Length < 6)
            {
                txtDEN_NO.Focus();
                txtDEN_NO.Text = HatFComParts.DoZeroFill(txtDEN_NO.Text, 6);
            }

        }
        #endregion

        #region << ショートカット制御 >>
        private void JH_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp & e.Control == true)
            {
                if (btnFooterPreviousPage.Enabled)
                {
                    btnFooterPreviousPage.PerformClick();
                    return;
                }
            }
            if (e.KeyCode == Keys.PageDown && e.Control == true)
            {
                if (btnFooterNextPage.Enabled)
                {
                    btnFooterNextPage.PerformClick();
                    return;
                }
            }
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (btnFnc01.Enabled)
                        btnFnc01.PerformClick();
                    break;
                case Keys.F3:
                    if (btnFnc03.Enabled)
                        btnFnc03.PerformClick();
                    else if (btnFooterF3.Enabled)
                        btnFooterF3.PerformClick();
                    break;
                case Keys.F4:
                    if (btnFooterF4.Enabled)
                    {
                        // F4によりドロップダウンが展開されるので無効化する
                        if (ActiveControl is ComboBox)
                        {
                            e.Handled = true;
                        }
                        btnFooterF4.PerformClick();
                    }
                    break;
                case Keys.F5:
                    if (btnFnc05.Enabled)
                        btnFnc05.PerformClick();
                    else if (btnFooterF5.Enabled)
                        btnFooterF5.PerformClick();
                    break;
                case Keys.F6:
                    if (btnFnc06.Enabled)
                        btnFnc06.PerformClick();
                    else if (btnFooterF6.Enabled)
                        btnFooterF6.PerformClick();
                    break;
                case Keys.F7:
                    if (btnFnc07.Enabled)
                        btnFnc07.PerformClick();
                    else if (btnFooterF7.Enabled)
                        btnFooterF7.PerformClick();
                    break;
                case Keys.F8:
                    if (btnFnc08.Enabled)
                        btnFnc08.PerformClick();
                    else if (btnFooterF8.Enabled)
                        btnFooterF8.PerformClick();
                    break;
                case Keys.F9:
                    if (btnFnc09.Enabled)
                        ShowSerchWindow();
                    //btnFnc09.PerformClick();
                    break;
                case Keys.F10:
                    if (btnFnc10.Enabled)
                        btnFnc10.PerformClick();
                    else if (btnFooterF10.Enabled)
                        btnFooterF10.PerformClick();
                    break;
                case Keys.F11:
                    if (btnFnc11.Enabled)
                        btnFnc11.PerformClick();
                    break;
                case Keys.F12:
                    if (btnFnc12.Enabled)
                        btnFnc12.PerformClick();
                    break;
                case Keys.PageDown:
                    if (btnKakunin.Enabled)
                    {
                        // PageDownによりドロップダウンの下の方が選択されるため無効化する
                        if (ActiveControl is ComboBox)
                        {
                            e.Handled = true;
                        }

                        btnKakunin.PerformClick();
                    }
                    break;
                case Keys.PageUp:
                    // コンボボックスのPageDownを無効化しているので、対になるPageUpも同じ条件で無効化する
                    if (btnKakunin.Enabled && ActiveControl is ComboBox)
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }
        #endregion

        #region << コンボボックス、チエックボックス処理 >>
        private void SetCombo()
        {
            // 伝区
            this.cmbDEN_FLG.SetItems(this.clientRepo.Options.DivDenpyo.Select(o => o.Code + ":" + o.Name).ToList());
            // 倉庫
            InitWarehouseComboBox(this.cmbSOKO_CD);
            _sokoCode.ValueChanged += (_, _) => InitBinComboBox();
            // 扱便
            InitBinComboBox();
            // 運賃
            this.cmbUNCHIN.SetItems(this.clientRepo.Options.DivUnchins.Select(o => o.Code + ":" + o.Name).ToList());
            // 区分
            this.cmbNOHIN.SetItems(this.clientRepo.Options.DivNohins.Select(o => o.Code + ":" + o.Name).ToList());
            // 発注
            this.cmbHKBN.SetItems(this.clientRepo.Options.DivHachus.Select(o => o.Code + ":" + o.Name).ToList());

            // 明細行のコンボボックス設定
            foreach(var row in _detailRows)
            {
                // 売区
                this.ucRow1.cmbURIKUBN.SetItems(this.clientRepo.Options.DivUriages.Select(o => o.Code + ":" + o.Name).ToList());
                // 倉庫
                InitWarehouseComboBox(this.ucRow1.cmbSOKO_CD);
            }
        }

        private void ShowCheckBoxData(CheckBoxEx checkBoxEx, string tmpData)
        {
            checkBoxEx.Checked = false;
            if (tmpData != null && tmpData.Equals(@"1"))
            {
                checkBoxEx.Checked = true;
            }
        }
        private string GetCheckBoxValue(CheckBoxEx checkBoxEx)
        {
            if (checkBoxEx.Checked)
            {
                return @"1";
            }
            else
            {
                return @"0";
            }
        }
        #endregion

        #region << 検索画面 F9 >>
        private void SetF9Activate()
        {
            this.txtTOKUI_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtTOKUI_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
            this.txtKMAN_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtKMAN_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
            this.txtSHIRESAKI_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtSHIRESAKI_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
            this.txtGENBA_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtGENBA_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
            this.txtKOUJITEN_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtKOUJITEN_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
            this.txtRECV_POSTCODE.Enter += new EventHandler(this.ForbtnFnc09_Enter);
            this.txtRECV_POSTCODE.Leave += new EventHandler(this.ForbtnFnc09_Leave);

            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);

                jH_Main_Detail.txtSHIRESAKI_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
                jH_Main_Detail.txtSHIRESAKI_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
                jH_Main_Detail.txtSYOHIN_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
                jH_Main_Detail.txtSYOHIN_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);
                jH_Main_Detail.txtSYOBUN_CD.Enter += new EventHandler(this.ForbtnFnc09_Enter);
                jH_Main_Detail.txtSYOBUN_CD.Leave += new EventHandler(this.ForbtnFnc09_Leave);

            }
        }
        private void ShowSerchWindow()
        {
            switch (this.ActiveControl.Name)
            {
                case nameof(ucRow1):
                case nameof(ucRow2):
                case nameof(ucRow3):
                case nameof(ucRow4):
                case nameof(ucRow5):
                case nameof(ucRow6):
                    JH_Main_Detail row = null;
                    switch (this.ActiveControl.Name)
                    {
                        case nameof(ucRow1):
                            row = ucRow1;
                            break;
                        case nameof(ucRow2):
                            row = ucRow2;
                            break;
                        case nameof(ucRow3):
                            row = ucRow3;
                            break;
                        case nameof(ucRow4):
                            row = ucRow4;
                            break;
                        case nameof(ucRow5):
                            row = ucRow5;
                            break;
                        case nameof(ucRow6):
                            row = ucRow6;
                            break;
                    }

                    if (GetForcusedControl(this.ActiveControl).Name == row.txtSHIRESAKI_CD.Name)
                    {
                        using (Views.MasterSearch.MS_Shiresaki dlg = new())
                        {
                            dlg.TxtTEAM_CD = this.txtTEAM_CD.Text;
                            dlg.TxtSHIRESAKI_CD = row.txtSHIRESAKI_CD.Text;
                            switch (dlg.ShowDialog())
                            {
                                case DialogResult.OK:
                                    this.ActiveControl.Controls[row.txtSHIRESAKI_CD.Name].Text = dlg.StrMsShiresakiCode;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (GetForcusedControl(this.ActiveControl).Name.Equals(row.txtSYOHIN_CD.Name))
                    {
                        using (Views.MasterSearch.MS_Syohin dlg = new())
                        {
                            dlg.TxtSHIRESAKI_CD = this.txtSHIRESAKI_CD.Text;
                            dlg.TxtSYOHIN_CD = row.SyohinCode;
                            switch (dlg.ShowDialog())
                            {
                                case DialogResult.OK:
                                    row.SyohinCode = dlg.StrMsSyohinName;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (GetForcusedControl(this.ActiveControl).Name.Equals(row.txtSYOBUN_CD.Name))
                    {
                        using (Views.MasterSearch.MS_ShiresakiBunrui dlg = new())
                        {
                            dlg.TxtTEAM_CD = this.txtTEAM_CD.Text;
                            dlg.TxtSHIRESAKI_CD = ((row.txtSHIRESAKI_CD.Text.Length == 6) ? row.txtSHIRESAKI_CD : txtSHIRESAKI_CD).Text;
                            switch (dlg.ShowDialog())
                            {
                                case DialogResult.OK:
                                    //1．[明細]>[行.仕入先コード]が入力可能状態
                                    //　　1－1．[検索結果]>[仕入先CD] == [明細]>[行.仕入先コード] の場合
                                    //　　　　　[明細]>[行.仕入先コード]を空とする。
                                    //　　1－2．[検索結果]>[仕入先CD] != [明細]>[行.仕入先コード] の場合
                                    //　　　　　[検索結果]>[仕入先コード]⇒[明細]>[行.仕入先コード]
                                    if (row.txtSHIRESAKI_CD.Enabled)
                                    {
                                        if (this.ActiveControl.Controls[row.txtSHIRESAKI_CD.Name].Text.Equals(dlg.StrMsShiresakiCode))
                                        {
                                            this.ActiveControl.Controls[row.txtSHIRESAKI_CD.Name].Text = string.Empty;
                                        }
                                        else
                                        {
                                            this.ActiveControl.Controls[row.txtSHIRESAKI_CD.Name].Text = dlg.StrMsShiresakiCode;
                                        }
                                    }
                                    //　2．[明細]>[行.分類]が入力可能状態
                                    //　　2－1． [検索結果]>[5桁コード]の文字列長<2桁の場合
                                    //　　　　　[明細]>[行.分類]を空とする
                                    //　　2－2．[検索結果]>[5桁コード]の文字列長>=2桁の場合
                                    //　　　　　[検索結果]>[5桁コード]の3文字目以降⇒[明細]>[行.分類]
                                    //　　2－3．[明細]>[行.商品コード・名称]を空とする
                                    if (row.txtSYOBUN_CD.Enabled)
                                    {
                                        if (dlg.StrMsShiresakiBunruiCode.Length < 2)
                                        {
                                            this.ActiveControl.Controls[row.txtSYOBUN_CD.Name].Text = string.Empty;
                                        }
                                        else
                                        {
                                            string strCd = dlg.StrMsShiresakiBunruiCode.Remove(0, 2);
                                            this.ActiveControl.Controls[row.txtSYOBUN_CD.Name].Text = strCd;
                                        }
                                    }
                                    //　3．[明細]>[行.商品コード・名称]が入力可能状態 かつ [明細]>[行.商品コード・名称]が空 の場合
                                    //　　[検索結果]>[商品分類名]⇒[明細]>[行.商品コード・名称]
                                    if (row.txtSYOHIN_CD.Enabled && row.txtSYOHIN_CD.Text.Length == 0)
                                    {
                                        row.SyohinCode = dlg.StrMsShiresakiBunruiName;
                                    }
                                    this.txtSHIRESAKI_CD.Text = dlg.StrMsShiresakiCode;
                                    this.txtroSHIRESAKI_NAME.Text = dlg.StrMsShiresakiName;
                                    row.txtSYOBUN_CD.Focus();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case nameof(txtKMAN_CD):
                    using (Views.MasterSearch.MS_KeyMan dlg = new())
                    {
                        if (this.txtTEAM_CD.Text.Length != 2 || this.txtTOKUI_CD.Text.Length != 6)
                        {
                            HatFComParts.ShowMessageAreaError(txtroNote, HatFComParts.GetErrMsgButton(hatfErrorMessageButtonRepo, @"BT034"));
                            return;
                        }
                        dlg.TxtTEAM_CD = this.txtTEAM_CD.Text;
                        dlg.TxtTOKUI_CD = this.txtTOKUI_CD.Text;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                this.txtKMAN_CD.Text = dlg.StrMsKeyManCode;
                                this.txtroKMAN_NAME.Text = dlg.StrMsKeyManName;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case nameof(txtSHIRESAKI_CD):
                    using (Views.MasterSearch.MS_Shiresaki dlg = new())
                    {
                        dlg.TxtTEAM_CD = this.txtTEAM_CD.Text;
                        dlg.TxtSHIRESAKI_CD = this.txtSHIRESAKI_CD.Text;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                this.txtSHIRESAKI_CD.Text = dlg.StrMsShiresakiCode;
                                this.txtroSHIRESAKI_NAME.Text = dlg.StrMsShiresakiName;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case nameof(txtTOKUI_CD):
                    using (var form = new MS_Tokui2())
                    {
                        form.CustCode = this.txtTOKUI_CD.Text.Trim();
                        if (DialogHelper.IsPositiveResult(form.ShowDialog(this)))
                        {
                            this.txtTOKUI_CD.Text = form.CustCode;
                            this.txtroTOKUI_NAME.Text = form.CustName;
                        }
                    }
                    break;
                case nameof(txtGENBA_CD):
                    using (MasterSearch.MS_Genba dlg = new())
                    {
                        dlg.CustomerCode = this.txtTOKUI_CD.Text;
                        dlg.KeymanCode = this.txtKMAN_CD.Text;
                        dlg.GenbaCode = this.txtGENBA_CD.Text;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                this.txtRECV_ADD1.Text = dlg.Destination.Address1;
                                this.txtRECV_ADD2.Text = dlg.Destination.Address2;
                                this.txtRECV_ADD3.Text = dlg.Destination.Address3;
                                this.txtRECV_POSTCODE.Text = HatFComParts.ToPostCode(dlg.Destination.ZipCode);
                                this.txtRECV_TEL.Text = dlg.Destination.DestTel;
                                this.txtRECV_NAME1.Text = dlg.Destination.DistName1;
                                this.txtRECV_NAME2.Text = dlg.Destination.DistName2;
                                this.txtGENBA_CD.Focus();
                                this.txtGENBA_CD.Text = dlg.Destination.GenbaCode;
                                this.txtTOKUI_CD.Text = dlg.Customer.ArCode;
                                this.txtKMAN_CD.Text = dlg.KeymanCode;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case nameof(txtKOUJITEN_CD):
                    using (Views.MasterSearch.MS_Koujiten dlg = new())
                    {
                        dlg.TxtKOUJITEN_CD = this.txtKOUJITEN_CD.Text;
                        dlg.TxtKOUJITEN_NAME = this.txtroKOUJITEN_NAME.Text;
                        dlg.TxtTOKUI_CD = this.txtTOKUI_CD.Text;
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                this.txtKOUJITEN_CD.Text = dlg.StrMsKoujitenCode;
                                this.txtroKOUJITEN_NAME.Text = dlg.StrMsKoujitenName;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case nameof(txtRECV_POSTCODE):
                    using (Views.MasterSearch.MS_Yuban dlg = new())
                    {
                        dlg.TxtPOSTCODE = this.txtRECV_POSTCODE.Text;
                        switch (dlg.ShowDialog(this))
                        {
                            case DialogResult.OK:
                                this.txtRECV_POSTCODE.Text = dlg.StrMsPostCode;
                                this.txtRECV_ADD1.Text = dlg.StrMsAddName1;
                                this.txtRECV_ADD2.Text = dlg.StrMsAddName2;
                                this.txtRECV_ADD3.Text = dlg.StrMsAddName3;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

        }
        private static Control GetForcusedControl(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c.Focused)
                {
                    return c;
                }
                if (c.ContainsFocus)
                {
                    Control fc = GetForcusedControl(c);
                    if (fc != null)
                    {
                        return fc;
                    }
                }
            }
            return null;
        }
        #endregion

        #region << 時計 >>
        // ↓このメソッド内でブレークポイントを効かせたい場合は一時的にDebuggerStepThrough属性を無効にしてください
        [System.Diagnostics.DebuggerStepThrough]
        private void TimerDSEQTime_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            txtroDSEQTime.Text = datetime.ToLongTimeString();
        }
        #endregion

        #region << 入力文字制限 >>
        private void SetTextBoxCharType()
        {
            this.txtTEAM_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);
            this.txtTOKUI_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);
            this.txtSHIRESAKI_CD.KeyPress += new KeyPressEventHandler(TextBoxCharType_KeyPress);

            this.txtSFAX.KeyPress += new KeyPressEventHandler(TextBoxHyphen_KeyPress);
            this.txtRECV_POSTCODE.KeyPress += new KeyPressEventHandler(TextBoxHyphen_KeyPress);
            this.txtRECV_TEL.KeyPress += new KeyPressEventHandler(TextBoxHyphen_KeyPress);

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
        private void TextBoxHyphen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumHyphen(e.KeyChar);
        }
        private void TextBoxNumOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                return;
            }
            e.Handled = HatFComParts.BoolChkCharOnKeyPressNumOnly(e.KeyChar);
        }
        #endregion

        #region << 入力チェック（フォーカスアウト） >>
        private static bool BoolIsNyukabiRange90(object objDat)
        {
            bool boolFlg = true;
            DateTime dateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(objDat.ToString()))
            {
                DateTime dateDat = DateTime.Parse(objDat.ToString());
                TimeSpan span = dateDat - dateTime;
                if (span.Days > 90 || span.Days < -90)
                {
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
        private static bool BoolIsNyukabiOverNouki(object objNyukabi, object objNouki)
        {
            bool boolFlg = true;
            if (!string.IsNullOrEmpty(objNyukabi.ToString()) && !string.IsNullOrEmpty(objNouki.ToString()))
            {
                DateTime dateNyukabi = DateTime.Parse(objNyukabi.ToString());
                DateTime dateNouki = DateTime.Parse(objNouki.ToString());
                if (dateNouki < dateNyukabi)
                {
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
        private void IsInputCheckFocusOut_Validated(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)sender).Font = new Font(((System.Windows.Forms.TextBox)sender).Font, FontStyle.Regular);
            HatFComParts.InitMessageArea(txtroNote);
            if (((System.Windows.Forms.TextBox)sender).Text.Length == 0) { return; }
            if (!((System.Windows.Forms.TextBox)sender).Enabled) { return; }

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
                case nameof(txtSFAX):
                case nameof(txtRECV_TEL):
                    strId = @"FO003";
                    break;
                case nameof(txtKMAN_CD):
                case nameof(txtGENBA_CD):
                case @"txtSYOBUN_CD":
                    strId = @"FO004";
                    break;
                //case nameof(dateHAT_NYUKABI):
                //    strId = @"FO008";
                //    break;
                case nameof(txtRECV_POSTCODE):
                    strId = @"FO009";
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
                    case @"FO003":
                        boolChk = HatFComParts.BoolIsHyphenNumByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        break;
                    case @"FO004":
                        boolChk = HatFComParts.BoolIsHalfByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        break;
                    case @"FO006":
                        boolChk = HatFComParts.BoolIsHalfByRegex(((System.Windows.Forms.TextBox)sender).Text);
                        if (boolChk) { boolChk = HatFComParts.BoolIsChar4Or7(((System.Windows.Forms.TextBox)sender).Text); } else { strId = @"FO004"; }
                        break;
                    //case @"FO008":
                    //    boolChk = BoolIsNyukabiRange90(((C1DateInputEx)sender).Value);
                    //    if (boolChk) { boolChk = BoolIsNyukabiOverNouki(((C1DateInputEx)sender).Value, this.dateNOUKI.Value); } else { strId = @"FO007"; }
                    //    break;
                    case @"FO009":
                        boolChk = HatFComParts.IsZipCode(((System.Windows.Forms.TextBox)sender).Text);
                        break;
                }
            }
            if (!boolChk)
            {
                HatFComParts.ShowMessageAreaError(txtroNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId));
                ((System.Windows.Forms.TextBox)sender).Focus();
                HatFComParts.SetColorOnErrorControl((System.Windows.Forms.TextBox)sender);
            }
            else
            {
                HatFComParts.InitMessageArea(txtroNote);
            }
        }
        private void IsTxtSYOBUN_CDCheckFocusOut_Validated(object sender, EventArgs e)
        {
            var detailRow = (sender as TextBox).Parent as JH_Main_Detail;

            detailRow.txtSYOBUN_CD.Font = new Font(detailRow.txtSYOBUN_CD.Font, FontStyle.Regular);
            HatFComParts.InitMessageArea(txtroNote);

            if (string.IsNullOrEmpty(detailRow.SyobunCd))
            {
                return;
            }

            string strId = @"FO004";
            bool boolChk = HatFComParts.BoolIsHalfByRegex(detailRow.SyobunCd);
            if (!boolChk)
            {
                HatFComParts.ShowMessageAreaError(txtroNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId));
                ((TextBox)sender).Focus();
                HatFComParts.SetColorOnErrorControl(detailRow.txtSYOBUN_CD);
            }
            else
            {
                HatFComParts.InitMessageArea(txtroNote);
            }
        }

        private void IsComboCheckFocusOut_Validated(object sender, EventArgs e)
        {
            ((ComboBoxEx)sender).Font = new Font(((ComboBoxEx)sender).Font, FontStyle.Regular);
            HatFComParts.InitMessageArea(txtroNote);

            if (((ComboBoxEx)sender).Text.Length == 0) { return; }

            string strId = @"FO005";

            if (!HatFComParts.BoolIsComboboxValidation((ComboBoxEx)sender))
            {
                HatFComParts.ShowMessageAreaError(txtroNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId));
                ((ComboBoxEx)sender).Focus();
                HatFComParts.SetColorOnErrorControl((ComboBoxEx)sender);
            }
            else
            {
                HatFComParts.InitMessageArea(txtroNote);
            }
        }
        // (HAT-12)追加 20240305
        private void IsNyukabiCheckFocusOut_Validated(object sender, EventArgs e)
        {
            HatFComParts.InitMessageArea(txtroNote);
            bool boolChk = true;
            string strId = @"FO008";
            if (((C1DateInputEx)sender).Text.Length != 0 && !((C1DateInputEx)sender).Text.Equals(@"__/__/__"))
            {
                boolChk = BoolIsNyukabiRange90(((C1DateInputEx)sender).Value);
                if (boolChk) { boolChk = BoolIsNyukabiOverNouki(((C1DateInputEx)sender).Value, this.dateNOUKI.Value); } else { strId = @"FO007"; }
            }

            if (!boolChk)
            {
                HatFComParts.ShowMessageAreaError(txtroNote, HatFComParts.GetErrMsgFocusOut(hatfErrorMessageFocusOutRepo, strId));
                ((System.Windows.Forms.TextBox)sender).Focus();
                HatFComParts.SetColorOnErrorControl((System.Windows.Forms.TextBox)sender);
            }
            else
            {
                ((C1DateInputEx)sender).Font = new Font(((C1DateInputEx)sender).Font, FontStyle.Regular);
                HatFComParts.InitMessageArea(txtroNote);
                if (!IntCheckPtn.Equals(0))
                {
                    if (IsChkInputDataButton(IntCheckPtn, 2))
                    {
                        HatFComParts.InitMessageArea(txtroNote);
                    }
                }
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
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.txtSHIRESAKI_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            }
            // 半角ハイフン数字チェック
            this.txtSFAX.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtRECV_POSTCODE.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtRECV_TEL.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            // 半角チェック
            this.txtKMAN_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            this.txtGENBA_CD.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            // 納日
            // (HAT-12)修正 20240305 不要につき削除
            //this.dateNOUKI.Validated += new EventHandler(this.IsInputCheckFocusOut_Validated);
            // 入荷日
            // (HAT-12)修正 20240305
            this.dateHAT_NYUKABI.Validated += new EventHandler(this.IsNyukabiCheckFocusOut_Validated);
            // コンボボックス
            this.cmbDEN_FLG.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            this.cmbSOKO_CD.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            this.cmbBINCD.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            this.cmbUNCHIN.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            this.cmbNOHIN.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            this.cmbHKBN.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                jH_Main_Detail.txtSYOBUN_CD.Validated += new EventHandler(this.IsTxtSYOBUN_CDCheckFocusOut_Validated);
                jH_Main_Detail.cmbURIKUBN.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
                jH_Main_Detail.cmbSOKO_CD.Validated += new EventHandler(this.IsComboCheckFocusOut_Validated);
            }
        }
        #endregion

        #region << 入力チェック（ボタン） >>
        private bool IsChkInputDataButton(int intPtn, int intType)
        {
            bool boolRet = false;
            System.Windows.Forms.Control focusBox = new();
            int intIdx = 999;
            string strId;
            string strMessage = "";
            InitControlStyles(this);// (HAT-12)追加 20240305
            dtErrorList.Clear();
            dtErrorListDetail.Clear();
            HatFComParts.InitMessageArea(txtroNote);

            JHOrderState jhOrderState = txtroHattyuJyoutai.Text;
            string strDEN_FLG = (this.cmbDEN_FLG.GetSelectedCode() == null) ? @"" : this.cmbDEN_FLG.GetSelectedCode();  // 伝票区分
            string strHKBN = (this.cmbHKBN.GetSelectedCode() == null) ? @"" : this.cmbHKBN.GetSelectedCode();           // 発注方法
            string strSOKO_CD = (this.cmbSOKO_CD.GetSelectedCode() == null) ? @"" : this.cmbSOKO_CD.GetSelectedCode();  // 倉庫
            string strBINCD = (this.cmbBINCD.GetSelectedCode() == null) ? @"" : this.cmbBINCD.GetSelectedCode();        // 扱便

            // 必須 ----------------------------------------------------------------------------------------------------------
            strId = @"BT001";
            // [販課（チームCD）]空
            if (this.txtTEAM_CD.Text.Length == 0) { WorkOnError(txtTEAM_CD, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [受発注者]空
            if (this.txtJYU2.Text.Length == 0) { WorkOnError(txtJYU2, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [入力者]空
            if (this.txtNYU2.Text.Length == 0) { WorkOnError(txtNYU2, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [伝票区分]空
            if (this.cmbDEN_FLG.Text.Length == 0) { WorkOnError(cmbDEN_FLG, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [伝票番号]空
            if (this.txtDEN_NO.Text.Length == 0) { WorkOnError(txtDEN_NO, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [得意先CD]空
            if (this.txtTOKUI_CD.Text.Length == 0) { WorkOnError(txtTOKUI_CD, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [倉庫CD]空
            if (this.cmbSOKO_CD.Text.Length == 0) { WorkOnError(cmbSOKO_CD, ref focusBox, ref intIdx, strId, ref strMessage); }

            // [仕入先CD]空 + [伝票区分]「15：取次」「21：直送」
            if (this.txtSHIRESAKI_CD.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
            { WorkOnError(txtSHIRESAKI_CD, ref focusBox, ref intIdx, strId, ref strMessage); }
            // [発注方法]空 + [伝票区分]が「15：取次」「21：直送」
            if (this.cmbHKBN.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
            { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }

            // [FAX]空 + [発注方法]が「1：FAX」
            if (this.txtSFAX.Text.Length == 0 && strHKBN.Equals(@"1"))
            { WorkOnError(txtSFAX, ref focusBox, ref intIdx, strId, ref strMessage); }

            // [HAT入荷日]空 + [発注方法]「4：オンライン」+ [発注状態]が「発注前」
            // (HAT-12)修正 20240305
            if ((this.dateHAT_NYUKABI.Text.Length == 0 || this.dateHAT_NYUKABI.Text.Equals(@"__/__/__")) && strHKBN.Equals(@"4") && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder)
            { WorkOnError(dateHAT_NYUKABI, ref focusBox, ref intIdx, strId, ref strMessage); }

            // [納期]空 + ([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]「5:請書取込」OR [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」)
            // (HAT-12)修正 20240305
            if (intPtn.Equals(3))
            {
                if (this.dateNOUKI.Text.Length == 0 || this.dateNOUKI.Text.Equals(@"__/__/__"))
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if ((this.dateNOUKI.Text.Length == 0 || this.dateNOUKI.Text.Equals(@"__/__/__")) && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5") || (strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13"))))
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            // [HAT入荷日]空 + [伝票区分]「15：取次」+ ([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]が「5:請書取込」)
            // (HAT-12)修正 20240305
            if (intPtn.Equals(3))
            {
                if ((this.dateHAT_NYUKABI.Text.Length == 0 || this.dateHAT_NYUKABI.Text.Equals(@"__/__/__")) && strDEN_FLG.Equals(@"15"))
                { WorkOnError(dateHAT_NYUKABI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if ((this.dateHAT_NYUKABI.Text.Length == 0 || this.dateHAT_NYUKABI.Text.Equals(@"__/__/__")) && strDEN_FLG.Equals(@"15") && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")))
                { WorkOnError(dateHAT_NYUKABI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            // [扱便]空 + (([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]「5:請書取込」) OR [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」)
            if (intPtn.Equals(3))
            {
                if (this.cmbBINCD.Text.Length == 0)
                { WorkOnError(cmbBINCD, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if (this.cmbBINCD.Text.Length == 0 && ((strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")) || (strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13"))))
                { WorkOnError(cmbBINCD, ref focusBox, ref intIdx, strId, ref strMessage); }
            }

            // [運賃]空 + ([伝票区分]「15：取次」OR「21：直送」) + ([F12:発注照合] OR [発注方法]が「０:計上のみ」OR [受注区分]が「5:請書取込」)
            // (HAT-12)修正 20240305
            if (intPtn.Equals(3))
            {
                if (this.cmbUNCHIN.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
                { WorkOnError(cmbUNCHIN, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if (this.cmbUNCHIN.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")) && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")))
                { WorkOnError(cmbUNCHIN, ref focusBox, ref intIdx, strId, ref strMessage); }
            }

            // [納品区分]空 + [伝票区分]「15：取次」「21：直送」+ ([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]「5:請書取込」)
            if (intPtn.Equals(3))
            {
                if (this.cmbNOHIN.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
                { WorkOnError(cmbNOHIN, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if (this.cmbNOHIN.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")) && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")))
                { WorkOnError(cmbNOHIN, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            // [行.子番]空 + [伝票区分]が「15：取次」「21：直送」
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled && BoolChkDetailDataInputRow(jH_Main_Detail))
                {
                    if (jH_Main_Detail.txtKoban.Text.Length == 0 && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
                    { WorkOnErrorDetail(jH_Main_Detail.Name, jH_Main_Detail.txtKoban, ref focusBox, ref intIdx, strId, ref strMessage); }
                }
            }

            // 0、1、2、3、4、5以外は入力不可です。---------------------------------------------------------------------------------
            strId = @"BT011";
            // [発注方法]「6：TETORA連携」+ [受注区分]!=「3：OPS」「4：HOPE」「7：新OPS」+ [発注状態]「発注前」
            if (strHKBN.Equals(@"6") && (this.txtroORDER_FLAG.Text != @"3" && this.txtroORDER_FLAG.Text != @"4" && this.txtroORDER_FLAG.Text != @"7") && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder)
            { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 1、2、3、4、5、6、7、8、9以外エラーです。----------------------------------------------------------------------------
            strId = @"BT015";
            // [行.子番]!=空 + [行.子番]!=「1」「2」「3」「4」「5」「6」「7」「8」「9」
            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);
                if (jH_Main_Detail.Enabled && BoolChkDetailDataInputRow(jH_Main_Detail))
                {
                    if (jH_Main_Detail.txtKoban.Text.Length != 0 && !HatFComParts.BoolIsNum1to9ByRegex(jH_Main_Detail.txtKoban.Text))
                    { WorkOnErrorDetail(jH_Main_Detail.Name, jH_Main_Detail.txtKoban, ref focusBox, ref intIdx, strId, ref strMessage); }
                }
            }

            //  1、2、3、4、5、6以外は入力不可です。----------------------------------------------------------------------------
            strId = @"BT012";
            // [発注方法]「0：計上のみ」+ ([受注区分]「3：OPS」「4：HOPE」「7：新OPS」OR [発注状態]!=「発注前」)
            if (strHKBN.Equals(@"0") && ((this.txtroORDER_FLAG.Text.Equals(@"3") || this.txtroORDER_FLAG.Text.Equals(@"4") || this.txtroORDER_FLAG.Text.Equals(@"7")) || this.txtroHattyuJyoutai.Text != JHOrderState.PreOrder))
            { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 90日以上未来を入力できません。------------------------------------------------------------------------------------
            strId = @"BT007";
            // [納期]が現在日付＋90日より未来 + [F11：受注確定] + [発注状態]「発注前」
            if (intPtn.Equals(2))
            {
                if (!HatFComParts.BoolIsInputDateOver(dateNOUKI.Value, 90) && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder)
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }

            // 同じ注文番号の伝票番号は重複できません。--------------------------------------------------------------------------
            strId = @"BT004";
            // [伝票番号]!=空 + [受注区分]!=「7：新OPS」+ [発注状態]「発注前」+ [伝票番号]と同一の[伝票番号]が別ページに存在
            if (this.txtDEN_NO.Text.Length != 0 && this.txtroORDER_FLAG.Text != @"7" && this.txtroHattyuJyoutai.Text.Equals(@"1") && !IsExistSameNoDenNo())
            { WorkOnError(txtDEN_NO, ref focusBox, ref intIdx, strId, ref strMessage); }

            // オンラインでは過去の納期は指定できません。------------------------------------------------------------------------
            strId = @"BT014";
            // [発注方法]「4：オンライン」+ [発注状態]「発注前」+ [HAT入荷日]!=空 + [HAT入荷日]が現在日付より過去
            if (strHKBN.Equals(@"4") && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder && !HatFComParts.BoolIsInputDateUnder(this.dateHAT_NYUKABI.Value, 1))
            { WorkOnError(dateHAT_NYUKABI, ref focusBox, ref intIdx, strId, ref strMessage); }

            // すべて数字を入力してください。------------------------------------------------------------------------------------
            strId = @"BT003";
            // [伝票番号]!=空 + [伝票番号]に半角数字のみではない
            if (this.txtDEN_NO.Text.Length != 0 && !HatFComParts.BoolIsNumOnlyByRegex(this.txtDEN_NO.Text))
            { WorkOnError(txtDEN_NO, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 伝票区分が倉庫の時、メーカー倉庫は入力不可です。------------------------------------------------------------------
            strId = @"BT010";
            // [倉庫CD]!=空 + [倉庫CD]「07」+ [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」「15：取次」
            // (HAT-12)修正 20240305
            if (strSOKO_CD.Equals(@"07") && (strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13") || strDEN_FLG.Equals(@"15")))
            { WorkOnError(cmbSOKO_CD, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 計上のみを指定してください。--------------------------------------------------------------------------------------
            strId = @"BT013";
            // [発注方法]!=「0：計上のみ」+[受注区分]「5：請書取込」
            if (strHKBN != @"0" && this.txtroORDER_FLAG.Text.Equals(@"5"))
            { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 在庫の場合に、扱便コード「000」は入力不可です。-------------------------------------------------------------------
            strId = @"BT032";
            // [扱便]「000」+ [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」
            if (strBINCD.Equals(@"000") && (strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13")))
            { WorkOnError(cmbBINCD, ref focusBox, ref intIdx, strId, ref strMessage); }

            // 在庫または取次の時、過去日付を入力できません。--------------------------------------------------------------------
            strId = @"BT005";
            // [伝票区分]「15：取次」+ [納期]現在日付より過去 + ([F12:発注照合]ボタン処理 OR [発注方法]「０:計上のみ」OR [受注区分]「5:請書取込」
            if (intPtn.Equals(3))
            {
                if (strDEN_FLG.Equals(@"15") && !HatFComParts.BoolIsInputDateUnder(this.dateNOUKI.Value, 1))
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            else
            {
                if (strDEN_FLG.Equals(@"15") && !HatFComParts.BoolIsInputDateUnder(this.dateNOUKI.Value, 1) && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")))
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            // [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」+ [納期]が現在日付より過去 + != ([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]「5:請書取込」)
            if (!intPtn.Equals(3))
            {
                if ((strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13")) && !HatFComParts.BoolIsInputDateUnder(this.dateNOUKI.Value, 1) && (!strHKBN.Equals(@"0") || !this.txtroORDER_FLAG.Text.Equals(@"5")))
                { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
            }

            // 取次の照合時、1年以上未来日付を入力できません。----------------------------------------------------------------------
            strId = @"BT008";
            // [納期]が現在日付＋1年より未来 + [伝票区分]「15：取次」+ [F12:発注照合]
            // (HAT-12)修正 20240305
            if (intPtn.Equals(3))
            {
                DateTime? dateTime = HatFComParts.GetDateAddYear(DateTime.Now, 1);
                if (this.dateNOUKI.Text.Length != 0 && !this.dateNOUKI.Text.Equals(@"__/__/__"))
                {
                    DateTime? dateNouki = DateTime.Parse(this.dateNOUKI.Value.ToString());
                    if (dateTime.HasValue && dateNouki > dateTime && strDEN_FLG.Equals(@"15"))
                    {
                        WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage);
                    }
                }
            }

            // 取次の照合時、60日以上未来日付を入力できません。---------------------------------------------------------------------
            strId = @"BT009";
            // [納期]が現在日付＋60日より未来 + [伝票区分]「15：取次」+ [F12:発注照合]
            // (HAT-12)修正 20240305
            if (intPtn.Equals(3))
            {
                DateTime? dateTime = HatFComParts.GetDateAddDay(DateTime.Now, 60);
                if (this.dateNOUKI.Text.Length != 0 && !this.dateNOUKI.Text.Equals(@"__/__/__"))
                {
                    DateTime? dateNouki = DateTime.Parse(this.dateNOUKI.Value.ToString());
                    if (dateTime.HasValue && dateNouki > dateTime && strDEN_FLG.Equals(@"15"))
                    {
                        WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage);
                    }
                }
            }

            // 直送の時、半年以上過去の日付を入力できません。-----------------------------------------------------------------------
            strId = @"BT033";
            // [伝票区分]「21：直送」+ [納期]現在日付より過去 + [納期]現在日付-6か月より過去 + ([F12:発注照合] OR [発注方法]「０:計上のみ」OR [受注区分]が「5:請書取込」)
            // (HAT-12)修正 20240305
            if (this.dateNOUKI.Text.Length != 0 && !this.dateNOUKI.Text.Equals(@"__/__/__"))
            {
                DateTime? dateTime = HatFComParts.GetDateAddMonth(DateTime.Now, -6);
                DateTime? dateNouki = DateTime.Parse(this.dateNOUKI.Value.ToString());
                if (dateTime.HasValue && dateNouki < dateTime)
                {
                    if (intPtn.Equals(3))
                    {
                        if (strDEN_FLG.Equals(@"21"))
                        { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
                    }
                    else
                    {
                        if (strDEN_FLG.Equals(@"21") && (strHKBN.Equals(@"0") || this.txtroORDER_FLAG.Text.Equals(@"5")))
                        { WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
                    }

                }
            }

#if false
            // 伝票分割をしてください。---------------------------------------------------------------------------------------------
            strId = @"BT002";
            // ヘッダの[倉庫CD]!=空 + 同一ページ内明細で入力値のある[倉庫CD]が異なる
            if (this.cmbSOKO_CD.Text.Length > 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    JH_Main_Detail jH_Main_Detail = GetUcName(i);
                    if (jH_Main_Detail.Enabled && jH_Main_Detail.cmbSOKO_CD.Visible)
                    {
                        string strSOKO_CD_D = (jH_Main_Detail.cmbSOKO_CD.GetSelectedCode() == null) ? @"" : jH_Main_Detail.cmbSOKO_CD.GetSelectedCode();
                        if (jH_Main_Detail.cmbSOKO_CD.Text.Length != 0 && strSOKO_CD_D != strSOKO_CD)
                        { WorkOnError(cmbSOKO_CD, ref focusBox, ref intIdx, strId, ref strMessage); break; }
                    }
                }
            }
#endif
            // ヘッダの[HAT入荷日]!=空 + 同一ページ内明細で入力値のある[行.納期（回答）]が異なる
            // (HAT-12)修正 20240305
            if (this.dateHAT_NYUKABI.Text.Length > 0 && !this.dateHAT_NYUKABI.Text.Equals(@"__/__/__"))
            {
                for (int i = 1; i <= 6; i++)
                {
                    JH_Main_Detail jH_Main_Detail = GetUcName(i);
                    if (jH_Main_Detail.Enabled)
                    {
                        if (jH_Main_Detail.dateNOUKI.Text.Length != 0 && jH_Main_Detail.dateNOUKI.Text != this.dateHAT_NYUKABI.Text)
                        { WorkOnErrorDetail(jH_Main_Detail.Name, jH_Main_Detail.dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }
                    }
                }
            }
            // ヘッダの[仕入先CD]!=空 + 同一ページ内明細で入力値のある[行.仕入先CD]が異なる
            if (this.txtSHIRESAKI_CD.Text.Length > 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    JH_Main_Detail jH_Main_Detail = GetUcName(i);
                    if (jH_Main_Detail.Enabled && jH_Main_Detail.txtSHIRESAKI_CD.Visible)
                    {
                        if (jH_Main_Detail.txtSHIRESAKI_CD.Text.Length != 0 && jH_Main_Detail.txtSHIRESAKI_CD.Text != this.txtSHIRESAKI_CD.Text)
                        { WorkOnErrorDetail(jH_Main_Detail.Name, jH_Main_Detail.txtSHIRESAKI_CD, ref focusBox, ref intIdx, strId, ref strMessage); }
                    }
                }
            }

            // 同一伝票内に同じ商品を入力できません。--------------------------------------------------------------------------------
            strId = @"BT016";
            // [伝票区分]「11：倉庫」「12：倉庫」「13：倉庫」+ [行.商品CD]!=空の場合[行.商品CD][行.倉庫CD][行.仕入先CD]がすべて合致する他の明細行が存在する
            // (HAT-12)修正 20240305
            if (strDEN_FLG.Equals(@"11") || strDEN_FLG.Equals(@"12") || strDEN_FLG.Equals(@"13"))
            {
                for (int j = 1; j <= 5; j++)
                {
                    string tmpSyohinCd = @"";
                    string tmpSokoCd = @"";
                    string tmpSiireCd = @"";
                    for (int i = j; i <= 6; i++)
                    {
                        JH_Main_Detail jH_Main_Detail = GetUcName(i);
                        if (jH_Main_Detail.txtSYOHIN_CD.Text.Length != 0)
                        {
                            if (i == j)
                            {
                                tmpSyohinCd = jH_Main_Detail.txtSYOHIN_CD.Text;
                                tmpSokoCd = jH_Main_Detail.cmbSOKO_CD.Text;
                                tmpSiireCd = jH_Main_Detail.txtSHIRESAKI_CD.Text;
                            }
                            else
                            {
                                if (jH_Main_Detail.txtSYOHIN_CD.Text.Equals(tmpSyohinCd) && jH_Main_Detail.cmbSOKO_CD.Text.Equals(tmpSokoCd) && jH_Main_Detail.txtSHIRESAKI_CD.Text.Equals(tmpSiireCd))
                                {
                                    WorkOnErrorDetail(jH_Main_Detail.Name, jH_Main_Detail.txtSYOHIN_CD, ref focusBox, ref intIdx, strId, ref strMessage);
                                }
                            }
                        }
                    }
                }
            }

            // 発注方法を3：手動に変更して下さい。------------------------------------------------------------------------------------
            strId = @"BT006";
            // [納期]現在日付より過去 + [受注区分]!=「5:請書取込」+[伝票区分]!=(「11：倉庫」「12：倉庫」「13：倉庫」) + [確認PgDn]ボタン + [発注状態]「発注前」+ [発注方法]「1：FAX」
            if (intPtn.Equals(1))
            {
                if (strDEN_FLG != @"15" && !HatFComParts.BoolIsInputDateUnder(this.dateNOUKI.Value, 1) && (strDEN_FLG != @"11" && strDEN_FLG != @"12" && strDEN_FLG != @"13") && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder && strHKBN.Equals(@"1"))
                { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }
            }
            // [HAT入荷日]!=空 +[HAT入荷日]現在日付より過去 + [発注状態]「発注前」+ [F11：受注確定」ボタン + [伝票区分]「15：取次」+[発注方法]「1：FAX」+ ([納期]!=空 + [納期]現在日付＋90日より未来 ではない)
            // (HAT-12)修正 20240305
            if (intPtn.Equals(2))
            {
                if (this.dateHAT_NYUKABI.Text.Length != 0 && !this.dateHAT_NYUKABI.Text.Equals(@"__/__/__") && this.dateNOUKI.Text.Length != 0 && !this.dateNOUKI.Text.Equals(@"__/__/__"))
                {
                    if (strDEN_FLG.Equals(@"15") && !HatFComParts.BoolIsInputDateUnder(this.dateHAT_NYUKABI.Value, 1) && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder && strHKBN.Equals(@"1") && HatFComParts.BoolIsInputDateOver(dateNOUKI.Value, 91))
                    { WorkOnError(cmbHKBN, ref focusBox, ref intIdx, strId, ref strMessage); }
                }
            }

            // H注番を発番してください。
            if (string.IsNullOrEmpty(txtHAT_ORDER_NO.Text.Trim()))
            {
                WorkOnError(txtHAT_ORDER_NO, ref focusBox, ref intIdx, "BT035", ref strMessage);
            }

            // 在庫数チェック
            foreach (var row in Enumerable.Range(1, 6).Select(i => GetUcName(i)))
            {
                if (row.Bara.HasValue && row.Zaikosuu.HasValue && row.Zaikosuu < row.Bara)
                {
                    WorkOnErrorDetail(row.Name, row.numBARA, ref focusBox, ref intIdx, "BT036", ref strMessage);
                }
            }

            if (intType.Equals(1))
            {
                if (focusBox.Name != @"")
                {
                    HatFComParts.ShowMessageAreaError(txtroNote, strMessage);
                    focusBox.Focus();
                    HatFComParts.SetColorOnErrorControl(focusBox);
                }
                else
                {
                    boolRet = true;
                }
            }
            else
            {
                if (focusBox.Name.Length.Equals(0))
                {
                    boolRet = true;
                }
            }
            return boolRet;
        }
        private void WorkOnError(System.Windows.Forms.Control inputBox, ref System.Windows.Forms.Control focusBox, ref int intIdx, string strId, ref string strMessage)
        {
            HatFComParts.SetColorOnErrorControl(inputBox);
            if (inputBox.TabIndex < intIdx)
            {
                strMessage = HatFComParts.GetErrMsgButton(hatfErrorMessageButtonRepo, strId);
                intIdx = inputBox.TabIndex;
                focusBox = inputBox;
            }
            DataRow dr = dtErrorList.NewRow();
            dr["objName"] = inputBox;
            dr["conName"] = inputBox.Name;
            dr["ErrCd"] = strId;
            dr["ErrMsg"] = HatFComParts.GetErrMsgButton(hatfErrorMessageButtonRepo, strId);
            dtErrorList.Rows.Add(dr);
            dtErrorList.AcceptChanges();
        }
        private void WorkOnErrorDetail(string strPnlName, Control inputBox, ref Control focusBox, ref int intIdx, string strId, ref string strMessage)
        {
            HatFComParts.SetColorOnErrorControl(inputBox);
            if (inputBox.TabIndex < intIdx)
            {
                strMessage = HatFComParts.GetErrMsgButton(hatfErrorMessageButtonRepo, strId);
                intIdx = inputBox.TabIndex;
                focusBox = inputBox;
            }
            DataRow dr = dtErrorListDetail.NewRow();
            dr["pnlName"] = strPnlName;
            dr["objName"] = inputBox;
            dr["conName"] = inputBox.Name;
            dr["ErrCd"] = strId;
            dr["ErrMsg"] = HatFComParts.GetErrMsgButton(hatfErrorMessageButtonRepo, strId);
            dtErrorListDetail.Rows.Add(dr);
            dtErrorListDetail.AcceptChanges();
        }
        private bool IsExistSameNoDenNo()
        {
            bool boolRet = true;
            if (dtHeader_jhMain.Rows.Count < 2) { return boolRet; }
            for (int i = 0; i < dtHeader_jhMain.Rows.Count; i++)
            {
                if (dtHeader_jhMain.Rows[i]["DenSort"].ToString() != strDenSort)
                {
                    if (dtHeader_jhMain.Rows[i]["DenNo"].ToString().Equals(this.txtDEN_NO.Text))
                    {
                        boolRet = false;
                        break;
                    }
                }
            }
            return boolRet;
        }
        private void SetErrorListInit()
        {
            dtErrorList.Columns.Add("objName", typeof(System.Windows.Forms.Control));
            dtErrorList.Columns.Add("conName", typeof(string));
            dtErrorList.Columns.Add("ErrCd", typeof(string));
            dtErrorList.Columns.Add("ErrMsg", typeof(string));

            dtErrorListDetail.Columns.Add("pnlName", typeof(string));
            dtErrorListDetail.Columns.Add("objName", typeof(System.Windows.Forms.Control));
            dtErrorListDetail.Columns.Add("conName", typeof(string));
            dtErrorListDetail.Columns.Add("ErrCd", typeof(string));
            dtErrorListDetail.Columns.Add("ErrMsg", typeof(string));
        }
        private void ShowErrorMessage(System.Windows.Forms.Control inputBox)
        {
            if (dtErrorList == null) { return; }
            foreach (DataRow row in dtErrorList.Rows)
            {
                if (row["conName"].Equals(inputBox.Name))
                {
                    HatFComParts.ShowMessageAreaError(txtroNote, row["ErrMsg"].ToString());
                    HatFComParts.SetColorOnErrorControl(inputBox);
                    break;
                }
            }
        }
        private void ShowErrorMessageDetail(System.Windows.Forms.Control inputBox)
        {
            if (dtErrorListDetail == null) { return; }
            foreach (DataRow row in dtErrorListDetail.Rows)
            {
                if (row["pnlName"].Equals(this.ActiveControl.Name) && row["conName"].Equals(inputBox.Name))
                {
                    HatFComParts.ShowMessageAreaError(txtroNote, row["ErrMsg"].ToString());
                    HatFComParts.SetColorOnErrorControl(inputBox);
                    break;
                }
            }
        }
        private void ForError_Enter(object sender, EventArgs e)
        {
            if (IntCheckPtn.Equals(0)) { return; }
            ShowErrorMessage((System.Windows.Forms.Control)sender);
        }
        private void ForErrorDetail_Enter(object sender, EventArgs e)
        {
            if (IntCheckPtn.Equals(0)) { return; }
            ShowErrorMessageDetail((System.Windows.Forms.Control)sender);
        }
        private void ForError_Leave(object sender, EventArgs e)
        {
            if (IntCheckPtn.Equals(0)) { return; }
            if (IsChkInputDataButton(IntCheckPtn, 2))
            {
                HatFComParts.InitMessageArea(txtroNote);
            }
        }
        private void SetErrorMessageEvent()
        {
            this.txtJYU2.Enter += new EventHandler(this.ForError_Enter);
            this.txtNYU2.Enter += new EventHandler(this.ForError_Enter);
            this.txtTEAM_CD.Enter += new EventHandler(this.ForError_Enter);
            this.txtTOKUI_CD.Enter += new EventHandler(this.ForError_Enter);
            this.txtSHIRESAKI_CD.Enter += new EventHandler(this.ForError_Enter);
            this.txtDEN_NO.Enter += new EventHandler(this.ForError_Enter);
            this.cmbBINCD.Enter += new EventHandler(this.ForError_Enter);
            this.cmbDEN_FLG.Enter += new EventHandler(this.ForError_Enter);
            this.cmbSOKO_CD.Enter += new EventHandler(this.ForError_Enter);
            this.cmbHKBN.Enter += new EventHandler(this.ForError_Enter);
            this.txtSFAX.Enter += new EventHandler(this.ForError_Enter);
            this.dateHAT_NYUKABI.Enter += new EventHandler(this.ForError_Enter);
            this.dateNOUKI.Enter += new EventHandler(this.ForError_Enter);
            this.cmbNOHIN.Enter += new EventHandler(this.ForError_Enter);
            // (HAT-12)修正 20240395
            this.cmbUNCHIN.Enter += new EventHandler(this.ForError_Enter);
            this.txtHAT_ORDER_NO.Enter += new EventHandler(this.ForError_Enter);

            this.txtJYU2.Leave += new EventHandler(this.ForError_Leave);
            this.txtNYU2.Leave += new EventHandler(this.ForError_Leave);
            this.txtTEAM_CD.Leave += new EventHandler(this.ForError_Leave);
            this.txtTOKUI_CD.Leave += new EventHandler(this.ForError_Leave);
            this.txtSHIRESAKI_CD.Leave += new EventHandler(this.ForError_Leave);
            this.txtDEN_NO.Leave += new EventHandler(this.ForError_Leave);
            this.cmbBINCD.Leave += new EventHandler(this.ForError_Leave);
            this.cmbDEN_FLG.Leave += new EventHandler(this.ForError_Leave);
            this.cmbSOKO_CD.Leave += new EventHandler(this.ForError_Leave);
            this.cmbHKBN.Leave += new EventHandler(this.ForError_Leave);
            this.txtSFAX.Leave += new EventHandler(this.ForError_Leave);
            this.dateHAT_NYUKABI.Leave += new EventHandler(this.ForError_Leave);
            this.dateNOUKI.Leave += new EventHandler(this.ForError_Leave);
            this.cmbNOHIN.Leave += new EventHandler(this.ForError_Leave);
            // (HAT-12)修正 20240395
            this.cmbUNCHIN.Leave += new EventHandler(this.ForError_Leave);
            this.txtHAT_ORDER_NO.Leave += new EventHandler(this.ForError_Leave);

            for (int i = 1; i <= 6; i++)
            {
                JH_Main_Detail jH_Main_Detail = GetUcName(i);

                jH_Main_Detail.txtKoban.Enter += new EventHandler(this.ForErrorDetail_Enter);
                jH_Main_Detail.dateNOUKI.Enter += new EventHandler(this.ForErrorDetail_Enter);
                jH_Main_Detail.txtSHIRESAKI_CD.Enter += new EventHandler(this.ForErrorDetail_Enter);
                jH_Main_Detail.txtSYOHIN_CD.Enter += new EventHandler(this.ForErrorDetail_Enter);
                jH_Main_Detail.numBARA.Enter += new EventHandler(this.ForErrorDetail_Enter);

                jH_Main_Detail.txtKoban.Leave += new EventHandler(this.ForError_Leave);
                jH_Main_Detail.dateNOUKI.Leave += new EventHandler(this.ForError_Leave);
                jH_Main_Detail.txtSHIRESAKI_CD.Leave += new EventHandler(this.ForError_Leave);
                jH_Main_Detail.txtSYOHIN_CD.Leave += new EventHandler(this.ForError_Leave);
                jH_Main_Detail.numBARA.Leave += new EventHandler(this.ForError_Leave);
            }
        }

        /// <summary>利率異常チェック</summary>
        /// <param name="canContinue">利率異常時、継続可能</param>
        /// <returns>成否</returns>
        private bool ValidateInterestRate(bool canContinue)
        {
            var rateOverCondition = HatFComParts.DoParseDecimal(Properties.Settings.Default.interestrate_rate_over);
            var rateUnderCondition = HatFComParts.DoParseDecimal(Properties.Settings.Default.interestrate_rate_under);
            var suryoCondition = HatFComParts.DoParseInt(Properties.Settings.Default.interestrate_suryo_over);
            var uriKinCondition = HatFComParts.DoParseInt(Properties.Settings.Default.interestrate_uri_kin_over);
            var uriTanZeroCondition = Properties.Settings.Default.interestrate_uri_tan_zero;
            var rowCount = 1;

            // 利率
            var rows = dtDetail_jhMain.Rows.OfType<DataRow>().Where(r => !string.IsNullOrEmpty(r["SyohinCd"].ToString()));
            foreach (var row in rows)
            {
                // 利率
                var uriTan = HatFComParts.DoParseDecimal(row["UriTan"].ToString());
                var siiTan = HatFComParts.DoParseDecimal(row["SiiTan"].ToString());
                var interestRate = (uriTan.HasValue && siiTan.HasValue && uriTan != 0) ? (uriTan - siiTan) / uriTan * 100 : null;
                if (!interestRate.HasValue &&
                    !ContinueWarning($"{rowCount}行目の利率に異常があります。", canContinue))
                {
                    return false;
                }
                if (rateOverCondition.HasValue && rateOverCondition <= interestRate &&
                    !ContinueWarning($"{rowCount}行目の利率が{rateOverCondition}%以上です。", canContinue))
                {
                    return false;
                }
                if (rateUnderCondition.HasValue && interestRate <= rateUnderCondition &&
                    !ContinueWarning($"{rowCount}行目の利率が{rateUnderCondition}%以下です。", canContinue))
                {
                    return false;
                }
                // 単価がゼロ
                if (uriTanZeroCondition && uriTan == 0 &&
                    !ContinueWarning($"{rowCount}行目の単価が0円です。", canContinue))
                {
                    return false;
                }
                // 数量
                var suryo = HatFComParts.DoParseInt(row["Suryo"].ToString());
                if (suryoCondition.HasValue && suryoCondition <= suryo &&
                    !ContinueWarning($"{rowCount}行目の数量が{suryoCondition}以上です。", canContinue))
                {
                    return false;
                }
                // 金額
                var uriKin = HatFComParts.DoParseInt(row["UriKin"].ToString());
                if (uriKinCondition.HasValue && uriKinCondition <= uriKin &&
                    !ContinueWarning($"{rowCount}行目の売上額が{uriKinCondition:#,##0}円以上です。", canContinue))
                {
                    return false;
                }
                rowCount++;
            }

            return true;
        }

        /// <summary>警告メッセージを表示する。<paramref name="canContinue"/>がtrueならOK/Cancelとなり続行可能</summary>
        /// <param name="prompt">メッセージ</param>
        /// <param name="canContinue">続行が可能</param>
        /// <returns>続行の有無</returns>
        private bool ContinueWarning(string prompt, bool canContinue)
        {
            if (canContinue)
            {
                var builder = new StringBuilder();
                builder.AppendLine(prompt);
                builder.AppendLine("[OK]続行");
                builder.AppendLine("[ｷｬﾝｾﾙ]中断");
                return DialogHelper.OkCancelWarning(this, builder.ToString());
            }
            else
            {
                DialogHelper.WarningMessage(this, prompt);
                return false;
            }
        }
        #endregion

        #region << 更新用ページ作成 >>
        public List<FosJyuchuPage> getFosJyuchPages()
        {
            if (dtHeader_jhMain.Rows.Count == 0) { return null; }

            // 新規
            var isNew = string.IsNullOrEmpty(dtHeader_jhMain.Rows[0]["SaveKey"].ToString());
            if (isNew)
            {
                //SetNewSaveKey(StrGetNewSaveKey());
                var jyu2Cd = loginRepo.CurrentUser.EmployeeCode;
                var jyu2 = txtJYU2.Text;
                SetNewSaveKey(FosJyuchuRepo.GetInstance().CreateNewSaveKey(jyu2Cd, jyu2));
            }

            List<FosJyuchuPage> pages = new List<FosJyuchuPage>();

            foreach (DataRow row in dtHeader_jhMain.Rows)
            {
                FosJyuchuPage page = new FosJyuchuPage(isNew)
                {
                    FosJyuchuH = new FosJyuchuH(),
                    FosJyuchuDs = new List<FosJyuchuD>(),
                };
                // SET KEY
                string saveKey = row["SaveKey"].ToString();
                string denSort = row["DenSort"].ToString();

                page.FosJyuchuH.SaveKey = saveKey;
                page.FosJyuchuH.DenSort = denSort;

                // SET HEADER DATA
                page.FosJyuchuH.OrderNo = row["OrderNo"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.OrderState = row["OrderState"].ToString();                              // 《ラベルなし》「発注前」(FosJyuchuH)string
                page.FosJyuchuH.DenNo = row["DenNo"].ToString();                                        // 伝No(FosJyuchuH)string
                page.FosJyuchuH.DenSort = row["DenSort"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.DenState = row["DenState"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.DenFlg = row["DenFlg"].ToString();                                      // 伝区(FosJyuchuH)string
                page.FosJyuchuH.EstimateNo = row["EstimateNo"].ToString();                              // 見積番号(FosJyuchuH)string
                page.FosJyuchuH.EstCoNo = row["EstCoNo"].ToString();                                    // 見積番号(FosJyuchuH)string
                page.FosJyuchuH.TeamCd = row["TeamCd"].ToString();                                      // 販課(FosJyuchuH)string
                page.FosJyuchuH.TantoCd = row["TantoCd"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.TantoName = row["TantoName"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Jyu2 = row["Jyu2"].ToString();                                          // 受発注者(FosJyuchuH)string
                page.FosJyuchuH.Jyu2Cd = row["Jyu2Cd"].ToString();                                      //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Jyu2Id = HatFComParts.DoParseInt(row["Jyu2Id"]);
                page.FosJyuchuH.Nyu2 = row["Nyu2"].ToString();                                          // 入力者(FosJyuchuH)string
                page.FosJyuchuH.Nyu2Cd = row["Nyu2Cd"].ToString();                                      //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Nyu2Id = HatFComParts.DoParseInt(row["Nyu2Id"]);
                page.FosJyuchuH.HatOrderNo = row["HatOrderNo"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.CustOrderno = row["CustOrderno"].ToString();                            // 客注(FosJyuchuH)string
                page.FosJyuchuH.TokuiCd = row["TokuiCd"].ToString();                                    // 得意先(FosJyuchuH)string
                page.FosJyuchuH.TokuiName = row["TokuiName"].ToString();                                    // 得意先(FosJyuchuH)string
                page.FosJyuchuH.KmanCd = row["KmanCd"].ToString();                                      // 担(FosJyuchuH)string
                page.FosJyuchuH.KmanName = row["KmanName"].ToString();                                      // 担(FosJyuchuH)string
                page.FosJyuchuH.GenbaCd = row["GenbaCd"].ToString();                                    // 現場(FosJyuchuH)string
                page.FosJyuchuH.Nouki = HatFComParts.DoParseDateTime(row["Nouki"]);                     // 納日(FosJyuchuH)DateTime
                page.FosJyuchuH.HatNyukabi = HatFComParts.DoParseDateTime(row["HatNyukabi"]);           //《画面対応なし》入荷日(FosJyuchuH)DateTime
                page.FosJyuchuH.BukkenExp = row["BukkenExp"].ToString();                                //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Sale1Flag = row["Sale1Flag"].ToString();                                //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Kessai = row["Kessai"].ToString();                                      // 決済(FosJyuchuH)string
                page.FosJyuchuH.Raikan = row["Raikan"].ToString();                                      // 来勘(FosJyuchuH)string
                page.FosJyuchuH.KoujitenCd = row["KoujitenCd"].ToString();                              // 工事店(FosJyuchuH)string
                page.FosJyuchuH.KoujitenName = row["KoujitenName"].ToString();                              // 工事店(FosJyuchuH)string
                page.FosJyuchuH.SokoCd = row["SokoCd"].ToString();                                      // 倉庫(FosJyuchuH)string
                page.FosJyuchuH.SokoName = row["SokoName"].ToString();                                      // 倉庫(FosJyuchuH)string
                page.FosJyuchuH.NoteHouse = row["NoteHouse"].ToString();                                // 社内備考(FosJyuchuH)string
                page.FosJyuchuH.OrderFlag = row["OrderFlag"].ToString();                                // 受区(FosJyuchuH)string
                page.FosJyuchuH.RecYmd = HatFComParts.DoParseDateTime(row["RecYmd"]);                   //《画面対応なし》(FosJyuchuH)DateTime
                page.FosJyuchuH.ShiresakiCd = row["ShiresakiCd"].ToString();                            // 仕入(FosJyuchuH)string
                page.FosJyuchuH.ShiresakiName = row["ShiresakiName"].ToString();                            // 仕入(FosJyuchuH)string
                page.FosJyuchuH.Hkbn = row["Hkbn"].ToString();                                          // 発注(FosJyuchuH)string
                page.FosJyuchuH.Sirainm = row["Sirainm"].ToString();                                    // 依頼(FosJyuchuH)string
                page.FosJyuchuH.Sfax = row["Sfax"].ToString();                                          // FAX(FosJyuchuH)string
                page.FosJyuchuH.SmailAdd = row["SmailAdd"].ToString();                                  //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.OrderMemo1 = row["OrderMemo1"].ToString();                              // 発注時メモ(FosJyuchuH)string
                page.FosJyuchuH.OrderMemo2 = row["OrderMemo2"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.OrderMemo3 = row["OrderMemo3"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Nohin = row["Nohin"].ToString();                                        // 区分(FosJyuchuH)string
                page.FosJyuchuH.Unchin = row["Unchin"].ToString();                                      // 運賃(FosJyuchuH)string
                page.FosJyuchuH.Bincd = row["Bincd"].ToString();                                        // 扱便(FosJyuchuH)string
                page.FosJyuchuH.Binname = row["Binname"].ToString();                                    // 扱便(FosJyuchuH)string
                page.FosJyuchuH.OkuriFlag = row["OkuriFlag"].ToString();                                // 送元(FosJyuchuH)string
                page.FosJyuchuH.ShireKa = row["ShireKa"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.Dseq = row["Dseq"].ToString();                                          // 内部No.(FosJyuchuH)string
                page.FosJyuchuH.OrderDenNo = row["OrderDenNo"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.MakerDenNo = row["MakerDenNo"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.IpAdd = row["IpAdd"].ToString();                                        //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.InpDate = HatFComParts.DoParseDateTime(row["InpDate"]);                 //《画面対応なし》(FosJyuchuH)DateTime
                page.FosJyuchuH.UpdDate = HatFComParts.DoParseDateTime(row["UpdDate"]);                 //《画面対応なし》(FosJyuchuH)DateTime
                page.FosJyuchuH.AnswerName = row["AnswerName"].ToString();                              // 回答者(FosJyuchuH)string
                page.FosJyuchuH.DelFlg = row["DelFlg"].ToString();                                      //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.AnswerConfirmFlg = row["AnswerConfirmFlg"].ToString();                  //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.SansyoDseq = row["SansyoDseq"].ToString();                              //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.ReqBiko = row["ReqBiko"].ToString();                                    //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.TelRenrakuFlg = row["TelRenrakuFlg"].ToString();                        // 電話連絡済(FosJyuchuH)string
                page.FosJyuchuH.UkeshoFlg = row["UkeshoFlg"].ToString();                                //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.EpukoKanriNo = row["EpukoKanriNo"].ToString();                          //《画面対応なし》(FosJyuchuH)string
                page.FosJyuchuH.SupplierType = HatFComParts.DoParseShort(row["SupplierType"]);

                page.FosJyuchuH.RecvGenbaCd = row["RecvGenbaCd"].ToString();                        // 《画面対応なし》(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvName1 = row["RecvName1"].ToString();                            // 宛先1(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvName2 = row["RecvName2"].ToString();                            // 宛先2(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvTel = row["RecvTel"].ToString();                                // TEL(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvPostcode = row["RecvPostcode"].ToString();                      // 〒(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvAdd1 = row["RecvAdd1"].ToString();                              // 住所1(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvAdd2 = row["RecvAdd2"].ToString();                              // 住所2(FosJyuchuHRecv)string
                page.FosJyuchuH.RecvAdd3 = row["RecvAdd3"].ToString();                              // 住所3(FosJyuchuHRecv)string

                page.FosJyuchuH.OpsOrderNo = row["OpsOrderNo"].ToString();                            // OPSNo.(FosJyuchuHOp)string
                page.FosJyuchuH.OpsRecYmd = HatFComParts.DoParseDateTime(row["OpsRecYmd"]);           // 《画面対応なし》(FosJyuchuHOp)DateTime
                page.FosJyuchuH.OpsHachuAdr = row["OpsHachuAdr"].ToString();                          // 《画面対応なし》(FosJyuchuHOp)string
                page.FosJyuchuH.OpsBin = row["OpsBin"].ToString();                                    // 《画面対応なし》(FosJyuchuHOp)string
                page.FosJyuchuH.OpsHachuName = row["OpsHachuName"].ToString();                        // 《画面対応なし》(FosJyuchuHOp)string

                page.FosJyuchuH.GOrderNo = row["GOrderNo"].ToString();    /// 《GLASS.受注データ.受注番号》
                page.FosJyuchuH.GOrderDate = HatFComParts.DoParseDateTime(row["GOrderDate"]);/// 《GLASS.受注データ.受注日》
                page.FosJyuchuH.GStartDate = HatFComParts.DoParseDateTime(row["GStartDate"]);/// 《GLASS.受注データ.部門開始日》
                page.FosJyuchuH.GCustCode = row["GCustCode"].ToString();/// 《GLASS.受注データ.顧客コード》
                page.FosJyuchuH.GCustSubNo = row["GCustSubNo"].ToString();/// 《GLASS.受注データ.顧客枝番》
                page.FosJyuchuH.GOrderAmnt = HatFComParts.DoParseLong(row["GOrderAmnt"]);/// 《GLASS.受注データ.受注金額合計》
                page.FosJyuchuH.GCmpTax = HatFComParts.DoParseLong(row["GCmpTax"]);/// 《GLASS.受注データ.消費税金額》
                page.FosJyuchuH.CreateDate = HatFComParts.DoParseDateTime2(row["CreateDate"]);/// 《GLASS.受注データ.部門開始日》
                page.FosJyuchuH.Creator = HatFComParts.DoParseInt(row["Creator"]);/// 《GLASS.受注データ.作成者》
                page.FosJyuchuH.UpdateDate = DateTime.Now;/// 《GLASS.受注データ.更新日時》
                page.FosJyuchuH.Updater = HatFComParts.DoParseInt(row["Updater"]);/// 《GLASS.受注データ.更新者》

                var detailRows = dtDetail_jhMain.Select($"SaveKey='{saveKey}' AND DenSort='{denSort}' ", "DenNoLine ASC");
                foreach (DataRow dr in detailRows)
                {
                    FosJyuchuD fosJyuchuD = new FosJyuchuD();

                    // SET KEY
                    fosJyuchuD.SaveKey = saveKey;
                    fosJyuchuD.DenSort = denSort;
                    fosJyuchuD.DenNoLine = dr["DenNoLine"].ToString();


                    // SET DETAIL DATA
                    fosJyuchuD.Koban = HatFComParts.DoParseShort(dr["Koban"]);
                    fosJyuchuD.OrderNo = dr["OrderNo"].ToString();                          // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.OrderNoLine = dr["OrderNoLine"].ToString();                  // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.OrderState = dr["OrderState"].ToString();                    // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.DenNo = dr["DenNo"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.EstimateNo = dr["EstimateNo"].ToString();                    // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.EstCoNo = dr["EstCoNo"].ToString();                          // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.LineNo = HatFComParts.DoParseInt(dr["LineNo"]);              // 《画面対応なし》(FosJyuchuD)int
                    fosJyuchuD.SyobunCd = dr["SyobunCd"].ToString();                        // 分類(FosJyuchuD)string
                    fosJyuchuD.SyohinCd = dr["SyohinCd"].ToString();                        // 商品コード・名称(FosJyuchuD)string
                    fosJyuchuD.SyohinName = dr["SyohinName"].ToString();                    // 商品コード・名称(FosJyuchuD)string
                    fosJyuchuD.Code5 = dr["Code5"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Kikaku = dr["Kikaku"].ToString();                            // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Suryo = HatFComParts.DoParseInt(dr["Suryo"]);                // 数量(FosJyuchuD)int
                    fosJyuchuD.Tani = dr["Tani"].ToString();                                // 単位(FosJyuchuD)string
                    fosJyuchuD.Bara = HatFComParts.DoParseInt(dr["Bara"]);                  // バラ数(FosJyuchuD)int
                    fosJyuchuD.TeiKigou = dr["TeiKigou"].ToString();                        // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.TeiKake = HatFComParts.DoParseDecimal(dr["TeiKake"]);        // 《画面対応なし》(FosJyuchuD)decimal
                    fosJyuchuD.TeiTan = HatFComParts.DoParseDecimal(dr["TeiTan"]);          // 定価単価(FosJyuchuD)decimal
                    fosJyuchuD.TeiKin = HatFComParts.DoParseDecimal(dr["TeiKin"]);          // 《画面対応なし》(FosJyuchuD)decimal
                    fosJyuchuD.Uauto = dr["Uauto"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.UriKigou = dr["UriKigou"].ToString();                        // 売上記号(FosJyuchuD)string
                    fosJyuchuD.UriKake = HatFComParts.DoParseDecimal(dr["UriKake"]);        // 掛率(売上)(FosJyuchuD)decimal
                    fosJyuchuD.UriTan = HatFComParts.DoParseDecimal(dr["UriTan"]);          // 売上単価(FosJyuchuD)decimal
                    fosJyuchuD.UriKin = HatFComParts.DoParseDecimal(dr["UriKin"]);          // 《画面対応なし》(FosJyuchuD)decimal
                    fosJyuchuD.UriType = dr["UriType"].ToString();                          // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Gauto = dr["Gauto"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.SiiKigou = dr["SiiKigou"].ToString();                        // 仕入記号(FosJyuchuD)string
                    fosJyuchuD.SiiKake = HatFComParts.DoParseDecimal(dr["SiiKake"]);        // 掛率(仕入)(FosJyuchuD)decimal
                    fosJyuchuD.SiiTan = HatFComParts.DoParseDecimal(dr["SiiTan"]);          // 仕入単価(FosJyuchuD)decimal
                    fosJyuchuD.SiiAnswTan = HatFComParts.DoParseDecimal(dr["SiiAnswTan"]);  // 回答単価(FosJyuchuD)decimal
                    fosJyuchuD.SiiKin = HatFComParts.DoParseDecimal(dr["SiiKin"]);          // 《画面対応なし》(FosJyuchuD)decimal
                    fosJyuchuD.Nouki = HatFComParts.DoParseDateTime(dr["Nouki"]);           // 納日(FosJyuchuD)DateTime
                    fosJyuchuD.TaxFlg = dr["TaxFlg"].ToString();                            // 消費税(FosJyuchuD)string
                    fosJyuchuD.Dencd = HatFComParts.DoParseInt(dr["Dencd"]);                // 《画面対応なし》(FosJyuchuD)int
                    fosJyuchuD.Urikubn = dr["Urikubn"].ToString();                          // 売区(FosJyuchuD)string
                    fosJyuchuD.SokoCd = dr["SokoCd"].ToString();                            // 倉庫(FosJyuchuD)string
                    fosJyuchuD.Chuban = dr["Chuban"].ToString();                            // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.ReqNouki = HatFComParts.DoParseDateTime(dr["ReqNouki"]);     // 《画面対応なし》(FosJyuchuD)DateTime
                    fosJyuchuD.ShiresakiCd = dr["ShiresakiCd"].ToString();                  // 仕入(FosJyuchuD)string
                    fosJyuchuD.Sbiko = dr["Sbiko"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Lbiko = dr["Lbiko"].ToString();                              // 行備考(FosJyuchuD)string
                    fosJyuchuD.Locat = dr["Locat"].ToString();                              // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.OrderDenNo = dr["OrderDenNo"].ToString();                    // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.OrderDenLineNo = dr["OrderDenLineNo"].ToString();            // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.DelFlg = dr["DelFlg"].ToString();                            // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.MoveFlg = dr["MoveFlg"].ToString();                          // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.InpDate = HatFComParts.DoParseDateTime(dr["InpDate"]);       // 《画面対応なし》(FosJyuchuD)DateTime
                    fosJyuchuD.Dseq = dr["Dseq"].ToString();                                // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Hinban = dr["Hinban"].ToString();                            // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.AddDetailFlg = dr["AddDetailFlg"].ToString();                // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.OyahinFlg = dr["OyahinFlg"].ToString();                      // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.Oyahinb = dr["Oyahinb"].ToString();                          // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.HopeOrderNo = dr["HopeOrderNo"].ToString();                  // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.HopeMeisaiNo = dr["HopeMeisaiNo"].ToString();                // 《画面対応なし》(FosJyuchuD)string
                    fosJyuchuD.EcoFlg = dr["EcoFlg"].ToString();                            // 《画面対応なし》(FosJyuchuD)string

                    fosJyuchuD.OpsOrderNo = dr["OpsOrderNo"].ToString();                  // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsRecYmd = HatFComParts.DoParseDateTime(dr["OpsRecYmd"]); // 《画面対応なし》(FosJyuchuDOp)DateTime
                    fosJyuchuD.OpsLineno = dr["OpsLineno"].ToString();                    // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsSokocd = dr["OpsSokocd"].ToString();                    // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsShukkadt = HatFComParts.DoParseDateTime(dr["OpsShukkadt"]);// 《画面対応なし》(FosJyuchuDOp)DateTime
                    fosJyuchuD.OpsNyukabi = HatFComParts.DoParseDateTime(dr["OpsNyukabi"]);// 《画面対応なし》(FosJyuchuDOp)DateTime
                    fosJyuchuD.OpsKonpo = HatFComParts.DoParseInt(dr["OpsKonpo"]);        // 《画面対応なし》(FosJyuchuDOp)int
                    fosJyuchuD.OpsTani = dr["OpsTani"].ToString();                        // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsBara = HatFComParts.DoParseInt(dr["OpsBara"]);          // 《画面対応なし》(FosJyuchuDOp)int
                    fosJyuchuD.OpsUtanka = HatFComParts.DoParseDecimal(dr["OpsUtanka"]);  // 《画面対応なし》(FosJyuchuDOp)decimal
                    fosJyuchuD.OpsUauto = dr["OpsUauto"].ToString();                      // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsUkigo = dr["OpsUkigo"].ToString();                      // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsUritu = HatFComParts.DoParseDecimal(dr["OpsUritu"]);    // 《画面対応なし》(FosJyuchuDOp)decimal
                    fosJyuchuD.OpsSyohinCd = dr["OpsSyohinCd"].ToString();                // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.OpsKikaku = dr["OpsKikaku"].ToString();                    // 《画面対応なし》(FosJyuchuDOp)string
                    fosJyuchuD.GOrderNo = dr["GOrderNo"].ToString();/// 《GLASS.受注データ.受注番号》
                    fosJyuchuD.GReserveQty = HatFComParts.DoParseShort(dr["GReserveQty"]);/// 《GLASS.受注データ.引当数量》
                    fosJyuchuD.GDeliveryOrderQty = HatFComParts.DoParseShort(dr["GDeliveryOrderQty"]);/// 《GLASS.受注データ.出荷指示数量》
                    fosJyuchuD.GDeliveredQty = HatFComParts.DoParseShort(dr["GDeliveredQty"]);/// 《GLASS.受注データ.出荷済数量》
                    fosJyuchuD.GCompleteFlg = HatFComParts.DoParseShort(dr["GCompleteFlg"]);/// 《GLASS.受注データ.完了フラグ》
                    fosJyuchuD.GDiscount = HatFComParts.DoParseShort(dr["GDiscount"]);/// 《GLASS.受注データ.値引金額》
                    fosJyuchuD.CreateDate = HatFComParts.DoParseDateTime2(dr["CreateDate"]);/// 《GLASS.受注データ.部門開始日》
                    fosJyuchuD.Creator = HatFComParts.DoParseInt(dr["Creator"]);/// 《GLASS.受注データ.作成者》
                    fosJyuchuD.UpdateDate = DateTime.Now;/// 《GLASS.受注データ.更新日時》
                    fosJyuchuD.Updater = HatFComParts.DoParseInt(dr["Updater"]);/// 《GLASS.受注データ.更新者》
                    // ADD TO PAGE
                    page.FosJyuchuDs.Add(fosJyuchuD);
                }

                // ADD TO PAGE LIST
                pages.Add(page);
            }
            return pages;
        }
        private void SetNewSaveKey(string strKey)
        {
            strSaveKey = strKey;
            dtHeader_jhMain.AsEnumerable().Select(r => r["SaveKey"] = strKey).ToList();
            dtDetail_jhMain.AsEnumerable().Select(r => r["SaveKey"] = strKey).ToList();
        }

        /// <summary>確定させるためにページ情報を変更する</summary>
        /// <param name="pages">ページ情報</param>
        /// <returns>ページ情報</returns>
        public async Task<ApiResponse<List<FosJyuchuPage>>> CommitPagesAsync(List<FosJyuchuPage> pages)
        {
            FosJyuchuPages fosJyuchuPages = new FosJyuchuPages
            {
                TargetPage = int.Parse(this.txtroFooterPageNo.Text) - 1,
                Pages = pages,
            };

            return await fosJyuchuRepo.putOrderCommit(fosJyuchuPages);
        }

        /// <summary>発注照合させるためにページ情報を変更する</summary>
        /// <param name="pages">ページ情報</param>
        /// <returns>ページ情報</returns>
        public async Task<ApiResponse<List<FosJyuchuPage>>> CollationPagesAsync(List<FosJyuchuPage> pages)
        {

            FosJyuchuPages fosJyuchuPages = new FosJyuchuPages
            {
                TargetPage = int.Parse(this.txtroFooterPageNo.Text) - 1,
                Pages = pages,
            };

            return await fosJyuchuRepo.putOrderlCollation(fosJyuchuPages);
        }
        #endregion

        #region << Model <=> DataTable Mapping >>
        public void SetData(List<FosJyuchuPage> pages)
        {
            SetDataToMainHeader(pages);
            SetDataToMainDetail(pages);
        }
        public void SetDataSelectedPage(List<FosJyuchuPage> pages, int intDenSort)
        {
            IntDenSort = intDenSort;
            SetDataToMainHeader(pages);
            SetDataToMainDetail(pages);
        }
        private void SetDataToMainHeader(List<FosJyuchuPage> pages)
        {
            DataTable dtHeader = dtHeader_jhMain;

            dtHeader.Clear();

            if (pages == null || pages.Count == 0) { return; } // NO DATA!!

            Debug.WriteLine($"pageCount: {pages.Count}");
            for (int i = 0; i < pages.Count; i++)
            {
                FosJyuchuH data = pages[i].FosJyuchuH;

                DataRow dr;
                dr = dtHeader.NewRow();

                dr["SaveKey"] = data.SaveKey;//《画面対応なし》(FosJyuchuH)string
                dr["OrderNo"] = data.OrderNo;//《画面対応なし》(FosJyuchuH)string
                dr["OrderState"] = data.OrderState;// 《ラベルなし》「発注前」(FosJyuchuH)string
                dr["DenNo"] = data.DenNo;// 伝No(FosJyuchuH)string
                dr["DenSort"] = data.DenSort;//《画面対応なし》(FosJyuchuH)string
                dr["DenSortInt"] = HatFComParts.DoParseInt(data.DenSort);//《画面対応なし》整列用
                dr["DenState"] = data.DenState;// 《ラベルなし》「発注前」(FosJyuchuH)string
                dr["DenFlg"] = data.DenFlg;// 伝区(FosJyuchuH)string
                dr["EstimateNo"] = data.EstimateNo;// 見積番号(FosJyuchuH)string
                dr["EstCoNo"] = data.EstCoNo;// 見積番号(FosJyuchuH)string
                dr["TeamCd"] = data.TeamCd;// 販課(FosJyuchuH)string
                dr["TantoCd"] = data.TantoCd;//《画面対応なし》(FosJyuchuH)string
                dr["TantoName"] = data.TantoName;//《画面対応なし》(FosJyuchuH)string
                dr["Jyu2"] = data.Jyu2;// 受発注者(FosJyuchuH)string
                dr["Jyu2Cd"] = data.Jyu2Cd;//《画面対応なし》(FosJyuchuH)string
                dr["Jyu2Id"] = (data.Jyu2Id == null) ? DBNull.Value : data.Jyu2Id;//《画面対応なし》(FosJyuchuH)string
                dr["Nyu2"] = data.Nyu2;// 入力者(FosJyuchuH)string
                dr["Nyu2Cd"] = data.Nyu2Cd;//《画面対応なし》(FosJyuchuH)string
                dr["Nyu2Id"] = (data.Nyu2Id == null) ? DBNull.Value : data.Nyu2Id;//《画面対応なし》(FosJyuchuH)string
                dr["HatOrderNo"] = data.HatOrderNo;//《画面対応なし》(FosJyuchuH)string
                dr["CustOrderno"] = data.CustOrderno;// 客注(FosJyuchuH)string
                dr["TokuiCd"] = data.TokuiCd;// 得意先(FosJyuchuH)string
                dr["TokuiName"] = data.TokuiName;// 得意先(FosJyuchuH)string
                dr["KmanCd"] = data.KmanCd;// 担(FosJyuchuH)string
                dr["KmanName"] = data.KmanName;// 担(FosJyuchuH)string
                dr["GenbaCd"] = data.GenbaCd;// 現場(FosJyuchuH)string
                dr["Nouki"] = (data.Nouki == null) ? DBNull.Value : data.Nouki;// 納日(FosJyuchuH)DateTime
                dr["HatNyukabi"] = (data.HatNyukabi == null) ? DBNull.Value : data.HatNyukabi;//《画面対応なし》入荷日(FosJyuchuH)DateTime
                dr["BukkenExp"] = data.BukkenExp;//《画面対応なし》(FosJyuchuH)string
                dr["Sale1Flag"] = data.Sale1Flag;//《画面対応なし》(FosJyuchuH)string
                dr["Kessai"] = data.Kessai;// 決済(FosJyuchuH)string
                dr["Raikan"] = data.Raikan;// 来勘(FosJyuchuH)string
                dr["KoujitenCd"] = data.KoujitenCd;// 工事店(FosJyuchuH)string
                dr["KoujitenName"] = data.KoujitenName;// 工事店(FosJyuchuH)string
                dr["SokoCd"] = data.SokoCd;// 倉庫(FosJyuchuH)string
                dr["SokoName"] = data.SokoName;// 倉庫(FosJyuchuH)string
                dr["NoteHouse"] = data.NoteHouse;// 社内備考(FosJyuchuH)string
                dr["OrderFlag"] = data.OrderFlag;// 受区(FosJyuchuH)string
                dr["RecYmd"] = (data.RecYmd == null) ? DBNull.Value : data.RecYmd;//《画面対応なし》(FosJyuchuH)DateTime
                dr["ShiresakiCd"] = data.ShiresakiCd;// 仕入(FosJyuchuH)string
                dr["ShiresakiName"] = data.ShiresakiName;// 仕入(FosJyuchuH)string
                dr["Hkbn"] = data.Hkbn;// 発注(FosJyuchuH)string
                dr["Sirainm"] = data.Sirainm;// 依頼(FosJyuchuH)string
                dr["Sfax"] = data.Sfax;// FAX(FosJyuchuH)string
                dr["SmailAdd"] = data.SmailAdd;//《画面対応なし》(FosJyuchuH)string
                dr["OrderMemo1"] = data.OrderMemo1;// 発注時メモ(FosJyuchuH)string
                dr["OrderMemo2"] = data.OrderMemo2;//《画面対応なし》(FosJyuchuH)string
                dr["OrderMemo3"] = data.OrderMemo3;//《画面対応なし》(FosJyuchuH)string
                dr["Nohin"] = data.Nohin;// 区分(FosJyuchuH)string
                dr["Unchin"] = data.Unchin;// 運賃(FosJyuchuH)string
                dr["Bincd"] = data.Bincd;// 扱便(FosJyuchuH)string
                dr["Binname"] = data.Binname;// 扱便(FosJyuchuH)string
                dr["OkuriFlag"] = data.OkuriFlag;// 送元(FosJyuchuH)string
                dr["ShireKa"] = data.ShireKa;//《画面対応なし》(FosJyuchuH)string
                dr["Dseq"] = data.Dseq;// 内部No.(FosJyuchuH)string
                dr["OrderDenNo"] = data.OrderDenNo;//《画面対応なし》(FosJyuchuH)string
                dr["MakerDenNo"] = data.MakerDenNo;//《画面対応なし》(FosJyuchuH)string
                dr["IpAdd"] = data.IpAdd;//《画面対応なし》(FosJyuchuH)string
                dr["InpDate"] = (data.InpDate == null) ? DBNull.Value : data.InpDate;//《画面対応なし》(FosJyuchuH)DateTime
                dr["UpdDate"] = (data.UpdDate == null) ? DBNull.Value : data.UpdDate;//《画面対応なし》(FosJyuchuH)DateTime
                dr["AnswerName"] = data.AnswerName;// 回答者(FosJyuchuH)string
                dr["DelFlg"] = data.DelFlg;//《画面対応なし》(FosJyuchuH)string
                dr["AnswerConfirmFlg"] = data.AnswerConfirmFlg;//《画面対応なし》(FosJyuchuH)string
                dr["SansyoDseq"] = data.SansyoDseq;//《画面対応なし》(FosJyuchuH)string
                dr["ReqBiko"] = data.ReqBiko;//《画面対応なし》(FosJyuchuH)string
                dr["TelRenrakuFlg"] = data.TelRenrakuFlg;// 電話連絡済(FosJyuchuH)string
                dr["UkeshoFlg"] = data.UkeshoFlg;//《画面対応なし》(FosJyuchuH)string
                dr["EpukoKanriNo"] = data.EpukoKanriNo;//《画面対応なし》(FosJyuchuH)string
                dr["SupplierType"] = (data.SupplierType == null) ? DBNull.Value : data.SupplierType;

                dr["RecvGenbaCd"] = data.RecvGenbaCd;// 《画面対応なし》(FosJyuchuHRecv)string
                dr["RecvName1"] = data.RecvName1;// 宛先1(FosJyuchuHRecv)string
                dr["RecvName2"] = data.RecvName2;// 宛先2(FosJyuchuHRecv)string
                dr["RecvTel"] = data.RecvTel;// TEL(FosJyuchuHRecv)string
                dr["RecvPostcode"] = data.RecvPostcode;// 〒(FosJyuchuHRecv)string
                dr["RecvAdd1"] = data.RecvAdd1;// 住所1(FosJyuchuHRecv)string
                dr["RecvAdd2"] = data.RecvAdd2;// 住所2(FosJyuchuHRecv)string
                dr["RecvAdd3"] = data.RecvAdd3;// 住所3(FosJyuchuHRecv)string

                dr["OpsOrderNo"] = data.OpsOrderNo;// OPSNo.(FosJyuchuHOp)string
                dr["OpsRecYmd"] = (data.OpsRecYmd == null) ? DBNull.Value : data.OpsRecYmd;// 《画面対応なし》(FosJyuchuHOp)DateTime
                dr["OpsHachuAdr"] = data.OpsHachuAdr;// 《画面対応なし》(FosJyuchuHOp)string
                dr["OpsBin"] = data.OpsBin;// 《画面対応なし》(FosJyuchuHOp)string
                dr["OpsHachuName"] = data.OpsHachuName;// 《画面対応なし》(FosJyuchuHOp)string
                dr["GOrderNo"] = data.GOrderNo;/// 《GLASS.受注データ.受番号》
                dr["GOrderDate"] = (data.GOrderDate == null) ? DBNull.Value : data.GOrderDate;/// 《GLASS.受注データ.受注日》
                dr["GStartDate"] = (data.GStartDate == null) ? DBNull.Value : data.GStartDate;/// 《GLASS.受注データ.部門開始日》
                dr["GCustCode"] = data.GCustCode;/// 《GLASS.受注データ.顧客コード》
                dr["GCustSubNo"] = data.GCustSubNo;/// 《GLASS.受注データ.顧客枝番》
                dr["GOrderAmnt"] = (data.GOrderAmnt == null) ? DBNull.Value : data.GOrderAmnt;/// 《GLASS.受注データ.受注金額合計》
                dr["GCmpTax"] = (data.GCmpTax == null) ? DBNull.Value : data.GCmpTax;/// 《GLASS.受注データ.消費税金額》
                dr["CreateDate"] = (data.CreateDate == null) ? DBNull.Value : data.CreateDate;/// 《GLASS.受注データ.作成日時》
                dr["Creator"] = (data.Creator == null) ? DBNull.Value : data.Creator;/// 《GLASS.受注データ.作成者》
                dr["UpdateDate"] = (data.UpdateDate == null) ? DBNull.Value : data.UpdateDate;/// 《GLASS.受注データ.更新日時》
                dr["Updater"] = (data.Updater == null) ? DBNull.Value : data.Updater;/// 《GLASS.受注データ.更新者》
                //dr["IchuFlg"] = data.IchuFlg;// 《画面対応なし》(FosJyuchuHIchu)string ICHU_FLG
                dr[nameof(FosJyuchuH.DenShippingPrinted)] = (object)data.DenShippingPrinted ?? DBNull.Value;

                dtHeader.Rows.Add(dr);
            }
        }
        private void SetDataToMainDetail(List<FosJyuchuPage> pages)
        {
            if (pages == null || pages.Count == 0) { return; } // NO DATA!!

            DataTable dtDetail = dtDetail_jhMain;

            dtDetail.Clear();

            for (int i = 0; i < pages.Count; i++)
            {
                List<FosJyuchuD> fosJyuchuDs = pages[i].FosJyuchuDs;
                if (fosJyuchuDs == null || fosJyuchuDs.Count == 0) { return; } // NO DATA!!
                foreach (FosJyuchuD data in fosJyuchuDs)
                {
                    DataRow dr;
                    dr = dtDetail.NewRow();
                    dr["SaveKey"] = data.SaveKey;// 《画面対応なし》(FosJyuchuD)string
                    dr["OrderNo"] = data.OrderNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["OrderNoLine"] = data.OrderNoLine;// 《画面対応なし》(FosJyuchuD)string
                    dr["OrderState"] = data.OrderState;// 《画面対応なし》(FosJyuchuD)string
                    dr["DenNo"] = data.DenNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["DenSort"] = data.DenSort;// 《画面対応なし》(FosJyuchuD)string
                    dr["DenSortInt"] = HatFComParts.DoParseInt(data.DenSort);//《画面対応なし》整列用
                    dr["DenNoLine"] = data.DenNoLine;// 《画面対応なし》(FosJyuchuD)string
                    dr["Koban"] = (object)data.Koban ?? DBNull.Value;
                    dr["EstimateNo"] = data.EstimateNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["EstCoNo"] = data.EstCoNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["LineNo"] = (data.LineNo == null) ? DBNull.Value : data.LineNo;// 《画面対応なし》(FosJyuchuD)int
                    dr["SyobunCd"] = data.SyobunCd;// 分類(FosJyuchuD)string
                    dr["SyohinCd"] = data.SyohinCd;// 商品コード・名称(FosJyuchuD)string
                    dr["SyohinName"] = data.SyohinName;// 商品コード・名称(FosJyuchuD)string
                    dr["Code5"] = data.Code5;// 《画面対応なし》(FosJyuchuD)string
                    dr["Kikaku"] = data.Kikaku;// 《画面対応なし》(FosJyuchuD)string
                    dr["Suryo"] = (data.Suryo == null) ? DBNull.Value : data.Suryo;// 数量(FosJyuchuD)int
                    dr["Tani"] = data.Tani;// 単位(FosJyuchuD)string
                    dr["Bara"] = (data.Bara == null) ? DBNull.Value : data.Bara;// バラ数(FosJyuchuD)int
                    dr["TeiKigou"] = data.TeiKigou;// 《画面対応なし》(FosJyuchuD)string
                    dr["TeiKake"] = (data.TeiKake == null) ? DBNull.Value : data.TeiKake;// 《画面対応なし》(FosJyuchuD)decimal
                    dr["TeiTan"] = (data.TeiTan == null) ? DBNull.Value : data.TeiTan;// 定価単価(FosJyuchuD)decimal
                    dr["TeiKin"] = (data.TeiKin == null) ? DBNull.Value : data.TeiKin;// 《画面対応なし》(FosJyuchuD)decimal
                    dr["Uauto"] = data.Uauto;// 《画面対応なし》(FosJyuchuD)string
                    dr["UriKigou"] = data.UriKigou;// 売上記号(FosJyuchuD)string
                    dr["UriKake"] = (data.UriKake == null) ? DBNull.Value : data.UriKake;// 掛率(売上)(FosJyuchuD)decimal
                    dr["UriTan"] = (data.UriTan == null) ? DBNull.Value : data.UriTan;// 売上単価(FosJyuchuD)decimal
                    dr["UriKin"] = (data.UriKin == null) ? DBNull.Value : data.UriKin;// 《画面対応なし》(FosJyuchuD)decimal
                    dr["UriType"] = data.UriType;// 《画面対応なし》(FosJyuchuD)string
                    dr["Gauto"] = data.Gauto;// 《画面対応なし》(FosJyuchuD)string
                    dr["SiiKigou"] = data.SiiKigou;// 仕入記号(FosJyuchuD)string
                    dr["SiiKake"] = (data.SiiKake == null) ? DBNull.Value : data.SiiKake;// 掛率(仕入)(FosJyuchuD)decimal
                    dr["SiiTan"] = (data.SiiTan == null) ? DBNull.Value : data.SiiTan;// 仕入単価(FosJyuchuD)decimal
                    dr["SiiAnswTan"] = (data.SiiAnswTan == null) ? DBNull.Value : data.SiiAnswTan;// 回答単価(FosJyuchuD)decimal
                    dr["SiiKin"] = (data.SiiKin == null) ? DBNull.Value : data.SiiKin;// 《画面対応なし》(FosJyuchuD)decimal
                    dr["Nouki"] = (data.Nouki == null) ? DBNull.Value : data.Nouki;// 納日(FosJyuchuD)DateTime
                    dr["TaxFlg"] = data.TaxFlg;// 消費税(FosJyuchuD)string
                    dr["Dencd"] = (data.Dencd == null) ? DBNull.Value : data.Dencd;// 《画面対応なし》(FosJyuchuD)int
                    dr["Urikubn"] = data.Urikubn;// 売区(FosJyuchuD)string
                    dr["SokoCd"] = data.SokoCd;// 倉庫(FosJyuchuD)string
                    dr["Chuban"] = data.Chuban;// 《画面対応なし》(FosJyuchuD)string
                    dr["ReqNouki"] = (data.ReqNouki == null) ? DBNull.Value : data.ReqNouki;// 《画面対応なし》(FosJyuchuD)DateTime
                    dr["ShiresakiCd"] = data.ShiresakiCd;// 仕入(FosJyuchuD)string
                    dr["Sbiko"] = data.Sbiko;// 《画面対応なし》(FosJyuchuD)string
                    dr["Lbiko"] = data.Lbiko;// 行備考(FosJyuchuD)string
                    dr["Locat"] = data.Locat;// 《画面対応なし》(FosJyuchuD)string
                    dr["OrderDenNo"] = data.OrderDenNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["OrderDenLineNo"] = data.OrderDenLineNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["DelFlg"] = data.DelFlg;// 《画面対応なし》(FosJyuchuD)string
                    dr["MoveFlg"] = data.MoveFlg;// 《画面対応なし》(FosJyuchuD)string
                    dr["InpDate"] = (data.InpDate == null) ? DBNull.Value : data.InpDate;// 《画面対応なし》(FosJyuchuD)DateTime
                    dr["Dseq"] = data.Dseq;// 《画面対応なし》(FosJyuchuD)string
                    dr["Hinban"] = data.Hinban;// 《画面対応なし》(FosJyuchuD)string
                    dr["AddDetailFlg"] = data.AddDetailFlg;// 《画面対応なし》(FosJyuchuD)string
                    dr["OyahinFlg"] = data.OyahinFlg;// 《画面対応なし》(FosJyuchuD)string
                    dr["Oyahinb"] = data.Oyahinb;// 《画面対応なし》(FosJyuchuD)string
                    dr["HopeOrderNo"] = data.HopeOrderNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["HopeMeisaiNo"] = data.HopeMeisaiNo;// 《画面対応なし》(FosJyuchuD)string
                    dr["EcoFlg"] = data.EcoFlg;// 《画面対応なし》(FosJyuchuD)string

                    dr["OpsOrderNo"] = data.OpsOrderNo;// 《画面対応なし》(FosJyuchuDOp)strdrng
                    dr["OpsRecYmd"] = (data.OpsRecYmd == null) ? DBNull.Value : data.OpsRecYmd;// 《画面対応なし》(FosJyuchuDOp)DateTdrme
                    dr["OpsLineno"] = data.OpsLineno;// 《画面対応なし》(FosJyuchuDOp)string
                    dr["OpsSokocd"] = data.OpsSokocd;// 《画面対応なし》(FosJyuchuDOp)strdrng
                    dr["OpsShukkadt"] = (data.OpsShukkadt == null) ? DBNull.Value : data.OpsShukkadt;// 《画面対応なし》(FosJyuchuDOp)DateTdrme
                    dr["OpsNyukabi"] = (data.OpsNyukabi == null) ? DBNull.Value : data.OpsNyukabi;// 《画面対応なし》(FosJyuchuDOp)DateTime
                    dr["OpsKonpo"] = (data.OpsKonpo == null) ? DBNull.Value : data.OpsKonpo;// 《画面対応なし》(FosJyuchuDOp)drnt
                    dr["OpsTani"] = data.OpsTani;// 《画面対応なし》(FosJyuchuDOp)string
                    dr["OpsBara"] = (data.OpsBara == null) ? DBNull.Value : data.OpsBara;// 《画面対応なし》(FosJyuchuDOp)drnt
                    dr["OpsUtanka"] = (data.OpsUtanka == null) ? DBNull.Value : data.OpsUtanka;// 《画面対応なし》(FosJyuchuDOp)decdrmal
                    dr["OpsUauto"] = data.OpsUauto;// 《画面対応なし》(FosJyuchuDOp)strdrng
                    dr["OpsUkigo"] = data.OpsUkigo;// 《画面対応なし》(FosJyuchuDOp)string
                    dr["OpsUritu"] = (data.OpsUritu == null) ? DBNull.Value : data.OpsUritu;// 《画面対応なし》(FosJyuchuDOp)decimal
                    dr["OpsSyohinCd"] = data.OpsSyohinCd;// 《画面対応なし》(FosJyuchuDOp)string
                    dr["OpsKikaku"] = data.OpsKikaku;// 《画面対応なし》(FosJyuchuDOp)string

                    dr["GOrderNo"] = data.GOrderNo;    /// 《GLASS.受注データ.受注番号》
                    dr["GReserveQty"] = (data.GReserveQty == null) ? DBNull.Value : data.GReserveQty;/// 《GLASS.受注データ.引当数量》
                    dr["GDeliveryOrderQty"] = (data.GDeliveryOrderQty == null) ? DBNull.Value : data.GDeliveryOrderQty;/// 《GLASS.受注データ.出荷指示数量》
                    dr["GDeliveredQty"] = (data.GDeliveredQty == null) ? DBNull.Value : data.GDeliveredQty;/// 《GLASS.受注データ.出荷済数量》
                    dr["GCompleteFlg"] = (data.GCompleteFlg == null) ? DBNull.Value : data.GCompleteFlg;/// 《GLASS.受注データ.完了フラグ》
                    dr["GDiscount"] = (data.GDiscount == null) ? DBNull.Value : data.GDiscount;/// 《GLASS.受注データ.値引金額》
                    dr["CreateDate"] = (data.CreateDate == null) ? DBNull.Value : data.CreateDate;/// 《GLASS.受注データ.作成日時》
                    dr["Creator"] = (data.Creator == null) ? DBNull.Value : data.Creator;/// 《GLASS.受注データ.作成者》
                    dr["UpdateDate"] = (data.UpdateDate == null) ? DBNull.Value : data.UpdateDate;/// 《GLASS.受注データ.更新日時》
                    dr["Updater"] = (data.Updater == null) ? DBNull.Value : data.Updater;/// 《GLASS.受注データ.更新者》

                    dtDetail.Rows.Add(dr);
                }

            }
        }
        #endregion

        #region << 発注書 >>
        private void MakePurchaseOrder(string outputFileName)
        {
            // テンプレートファイル
            string templateFileName = ExcelReportUtil.AddTemplatePathToFileName("発注書マッピング20240302.xlsx");

            using (Stream stream = new FileStream(templateFileName, FileMode.Open, FileAccess.Read))
            using (XLWorkbook wb = new(stream))
            {
                // FOS_JYUCHU_H
                string strSheetName = @"FOS_JYUCHU_H";
                IXLWorksheet ixlFOS_JYUCHU_H = wb.Worksheet(strSheetName);
                IXLWorksheet ixlSheet = wb.Worksheets.First(_ => _.TabActive).Worksheet;
                if (ixlSheet != ixlFOS_JYUCHU_H)
                {
                    wb.Worksheet(strSheetName).SetTabActive();
                    ixlSheet.TabSelected = false;
                }

                SetExcelFOS_JYUCHU_H(ixlFOS_JYUCHU_H, getCurHeader());

                // FOS_JYUCHU_D
                strSheetName = @"FOS_JYUCHU_D";
                IXLWorksheet ixlFOS_JYUCHU_D = wb.Worksheet(strSheetName);
                ixlSheet = wb.Worksheets.First(_ => _.TabActive).Worksheet;
                if (ixlSheet != ixlFOS_JYUCHU_D)
                {
                    wb.Worksheet(strSheetName).SetTabActive();
                    ixlSheet.TabSelected = false;
                }

                var detailRow = dtDetail_jhMain.Select($"SaveKey='{strSaveKey}' AND DenSort='{strDenSort}' ", "DenNoLine ASC");
                int intColNo = 2;
                foreach (DataRow dt in detailRow)
                {
                    SetExcelFOS_JYUCHU_D(ixlFOS_JYUCHU_D, dt, intColNo);
                    intColNo++;
                }

                // DEPT_MST
                // EMPLOYEE

                // 発注書
                strSheetName = @" 注　文　書（兼　回　答　書） ";
                ixlSheet = wb.Worksheets.First(_ => _.TabActive).Worksheet;
                if (ixlSheet != wb.Worksheet(strSheetName))
                {
                    wb.Worksheet(strSheetName).SetTabActive();
                    ixlSheet.TabSelected = false;
                }

                wb.SaveAs(outputFileName);
            }
        }
        private void SetExcelFOS_JYUCHU_H(IXLWorksheet ixlFOS_JYUCHU_H, DataRow row)
        {
            ixlFOS_JYUCHU_H.Cell(1, 2).Value = row["SaveKey"].ToString();           //SAVE_KEY
            ixlFOS_JYUCHU_H.Cell(2, 2).Value = row["OrderNo"].ToString();           //ORDER_NO
            ixlFOS_JYUCHU_H.Cell(3, 2).Value = row["OrderState"].ToString();        //ORDER_STATE
            ixlFOS_JYUCHU_H.Cell(4, 2).Value = row["DenNo"].ToString();             //DEN_NO
            ixlFOS_JYUCHU_H.Cell(5, 2).Value = row["DenSort"].ToString();           //DEN_SORT
            ixlFOS_JYUCHU_H.Cell(6, 2).Value = row["DenState"].ToString();          //DEN_STATE
            ixlFOS_JYUCHU_H.Cell(7, 2).Value = row["DenFlg"].ToString();            //DEN_FLG
            ixlFOS_JYUCHU_H.Cell(8, 2).Value = row["EstimateNo"].ToString();        //ESTIMATE_NO
            ixlFOS_JYUCHU_H.Cell(9, 2).Value = row["EstCoNo"].ToString();           //EST_CO_NO
            ixlFOS_JYUCHU_H.Cell(11, 2).Value = row["TeamCd"].ToString();           //TEAM_CD
            ixlFOS_JYUCHU_H.Cell(12, 2).Value = row["TantoCd"].ToString();          //TANTO_CD
            ixlFOS_JYUCHU_H.Cell(13, 2).Value = row["TantoName"].ToString();        //TANTO_NAME
            ixlFOS_JYUCHU_H.Cell(14, 2).Value = row["Jyu2"].ToString();             //JYU2
            ixlFOS_JYUCHU_H.Cell(15, 2).Value = row["Jyu2Cd"].ToString();           //JYU2_CD
            ixlFOS_JYUCHU_H.Cell(16, 2).Value = row["Jyu2Id"].ToString();           //JYU2_ID
            ixlFOS_JYUCHU_H.Cell(17, 2).Value = row["Nyu2"].ToString();             //NYU2
            ixlFOS_JYUCHU_H.Cell(18, 2).Value = row["Nyu2Cd"].ToString();           //NYU2_CD
            ixlFOS_JYUCHU_H.Cell(19, 2).Value = row["Nyu2Id"].ToString();           //NYU2_ID
            ixlFOS_JYUCHU_H.Cell(20, 2).Value = row["HatOrderNo"].ToString();       //HAT_ORDER_NO
            ixlFOS_JYUCHU_H.Cell(21, 2).Value = row["CustOrderno"].ToString();      //CUST_ORDERNO
            ixlFOS_JYUCHU_H.Cell(22, 2).Value = row["TokuiCd"].ToString();          //TOKUI_CD
            ixlFOS_JYUCHU_H.Cell(23, 2).Value = row["TokuiName"].ToString();        //TOKUI_NAME
            ixlFOS_JYUCHU_H.Cell(24, 2).Value = row["KmanCd"].ToString();           //KMAN_CD
            ixlFOS_JYUCHU_H.Cell(25, 2).Value = row["KmanName"].ToString();         //KMAN_NAME
            ixlFOS_JYUCHU_H.Cell(26, 2).Value = row["GenbaCd"].ToString();          //GENBA_CD
            ixlFOS_JYUCHU_H.Cell(27, 2).Value = row["Nouki"].ToString();            //NOUKI
            ixlFOS_JYUCHU_H.Cell(28, 2).Value = row["HatNyukabi"].ToString();       //HAT_NYUKABI
            ixlFOS_JYUCHU_H.Cell(29, 2).Value = row["BukkenExp"].ToString();        //BUKKEN_EXP
            ixlFOS_JYUCHU_H.Cell(30, 2).Value = row["Sale1Flag"].ToString();        //SALE1_FLAG
            ixlFOS_JYUCHU_H.Cell(31, 2).Value = row["Kessai"].ToString();           //KESSAI
            ixlFOS_JYUCHU_H.Cell(32, 2).Value = row["Raikan"].ToString();           //RAIKAN
            ixlFOS_JYUCHU_H.Cell(33, 2).Value = row["KoujitenCd"].ToString();       //KOUJITEN_CD
            ixlFOS_JYUCHU_H.Cell(34, 2).Value = row["KoujitenName"].ToString();     //KOUJITEN_NAME
            ixlFOS_JYUCHU_H.Cell(35, 2).Value = row["SokoCd"].ToString();           //SOKO_CD
            ixlFOS_JYUCHU_H.Cell(36, 2).Value = row["SokoName"].ToString();         //SOKO_NAME
            ixlFOS_JYUCHU_H.Cell(37, 2).Value = row["NoteHouse"].ToString();        //NOTE_HOUSE
            ixlFOS_JYUCHU_H.Cell(38, 2).Value = row["OrderFlag"].ToString();        //ORDER_FLAG
            ixlFOS_JYUCHU_H.Cell(39, 2).Value = row["RecYmd"].ToString();           //REC_YMD
            ixlFOS_JYUCHU_H.Cell(40, 2).Value = row["ShiresakiCd"].ToString();      //SHIRESAKI_CD
            ixlFOS_JYUCHU_H.Cell(41, 2).Value = row["ShiresakiName"].ToString();    //SHIRESAKI_NAME
            ixlFOS_JYUCHU_H.Cell(42, 2).Value = row["Hkbn"].ToString();             //HKBN
            ixlFOS_JYUCHU_H.Cell(43, 2).Value = row["Sirainm"].ToString();          //SIRAINM
            ixlFOS_JYUCHU_H.Cell(44, 2).Value = row["Sfax"].ToString();             //SFAX
            ixlFOS_JYUCHU_H.Cell(45, 2).Value = row["SmailAdd"].ToString();         //SMAIL_ADD
            ixlFOS_JYUCHU_H.Cell(46, 2).Value = row["OrderMemo1"].ToString();       //ORDER_MEMO1
            ixlFOS_JYUCHU_H.Cell(47, 2).Value = row["OrderMemo2"].ToString();       //ORDER_MEMO2
            ixlFOS_JYUCHU_H.Cell(48, 2).Value = row["OrderMemo3"].ToString();       //ORDER_MEMO3
            ixlFOS_JYUCHU_H.Cell(49, 2).Value = row["Nohin"].ToString();            //NOHIN
            ixlFOS_JYUCHU_H.Cell(50, 2).Value = row["Unchin"].ToString();           //UNCHIN
            ixlFOS_JYUCHU_H.Cell(51, 2).Value = row["Bincd"].ToString();            //BINCD
            ixlFOS_JYUCHU_H.Cell(52, 2).Value = row["Binname"].ToString();          //BINNAME
            ixlFOS_JYUCHU_H.Cell(53, 2).Value = row["OkuriFlag"].ToString();        //OKURI_FLAG
            ixlFOS_JYUCHU_H.Cell(54, 2).Value = row["ShireKa"].ToString();          //SHIRE_KA
            ixlFOS_JYUCHU_H.Cell(55, 2).Value = row["Dseq"].ToString();             //DSEQ
            ixlFOS_JYUCHU_H.Cell(56, 2).Value = row["OrderDenNo"].ToString();       //ORDER_DEN_NO
            ixlFOS_JYUCHU_H.Cell(57, 2).Value = row["MakerDenNo"].ToString();       //MAKER_DEN_NO
            ixlFOS_JYUCHU_H.Cell(58, 2).Value = row["IpAdd"].ToString();            //IP_ADD
            ixlFOS_JYUCHU_H.Cell(59, 2).Value = row["InpDate"].ToString();          //INP_DATE
            ixlFOS_JYUCHU_H.Cell(60, 2).Value = row["UpdDate"].ToString();          //UPD_DATE
            ixlFOS_JYUCHU_H.Cell(61, 2).Value = row["AnswerName"].ToString();       //ANSWER_NAME
            ixlFOS_JYUCHU_H.Cell(62, 2).Value = row["DelFlg"].ToString();           //DEL_FLG
            ixlFOS_JYUCHU_H.Cell(63, 2).Value = row["AnswerConfirmFlg"].ToString(); //ANSWER_CONFIRM_FLG
            ixlFOS_JYUCHU_H.Cell(64, 2).Value = row["SansyoDseq"].ToString();       //SANSYO_DSEQ
            ixlFOS_JYUCHU_H.Cell(65, 2).Value = row["ReqBiko"].ToString();          //REQ_BIKO
            ixlFOS_JYUCHU_H.Cell(66, 2).Value = row["TelRenrakuFlg"].ToString();    //TEL_RENRAKU_FLG
            ixlFOS_JYUCHU_H.Cell(67, 2).Value = row["UkeshoFlg"].ToString();        //UKESHO_FLG
            ixlFOS_JYUCHU_H.Cell(68, 2).Value = row["EpukoKanriNo"].ToString();     //EPUKO_KANRI_NO
            ixlFOS_JYUCHU_H.Cell(69, 2).Value = row["SupplierType"].ToString();     //SUPPLIER_TYPE
            ixlFOS_JYUCHU_H.Cell(70, 2).Value = row["RecvGenbaCd"].ToString();      //RECV_GENBA_CD
            ixlFOS_JYUCHU_H.Cell(71, 2).Value = row["RecvName1"].ToString();        //RECV_NAME1
            ixlFOS_JYUCHU_H.Cell(72, 2).Value = row["RecvName2"].ToString();        //RECV_NAME2
            ixlFOS_JYUCHU_H.Cell(73, 2).Value = row["RecvTel"].ToString();          //RECV_TEL
            ixlFOS_JYUCHU_H.Cell(74, 2).Value = row["RecvPostcode"].ToString();     //RECV_POSTCODE
            ixlFOS_JYUCHU_H.Cell(75, 2).Value = row["RecvAdd1"].ToString();         //RECV_ADD1
            ixlFOS_JYUCHU_H.Cell(76, 2).Value = row["RecvAdd2"].ToString();         //RECV_ADD2
            ixlFOS_JYUCHU_H.Cell(77, 2).Value = row["RecvAdd3"].ToString();         //RECV_ADD3
            ixlFOS_JYUCHU_H.Cell(78, 2).Value = row["OpsOrderNo"].ToString();       //OPS_ORDER_NO
            ixlFOS_JYUCHU_H.Cell(79, 2).Value = row["OpsRecYmd"].ToString();        //OPS_REC_YMD
            ixlFOS_JYUCHU_H.Cell(80, 2).Value = row["OpsHachuAdr"].ToString();      //OPS_HACHU_ADR
            ixlFOS_JYUCHU_H.Cell(81, 2).Value = row["OpsBin"].ToString();           //OPS_BIN
            ixlFOS_JYUCHU_H.Cell(82, 2).Value = row["OpsHachuName"].ToString();     //OPS_HACHU_NAME
            ixlFOS_JYUCHU_H.Cell(83, 2).Value = row["GOrderNo"].ToString();         //G_ORDER_NO
            ixlFOS_JYUCHU_H.Cell(84, 2).Value = row["GOrderDate"].ToString();       //G_ORDER_DATE
            ixlFOS_JYUCHU_H.Cell(85, 2).Value = row["GStartDate"].ToString();       //G_START_DATE
            ixlFOS_JYUCHU_H.Cell(86, 2).Value = row["GCustCode"].ToString();        //G_CUST_CODE
            ixlFOS_JYUCHU_H.Cell(87, 2).Value = row["GCustSubNo"].ToString();       //G_CUST_SUB_NO
            ixlFOS_JYUCHU_H.Cell(88, 2).Value = row["GOrderAmnt"].ToString();       //G_ORDER_AMNT
            ixlFOS_JYUCHU_H.Cell(89, 2).Value = row["GCmpTax"].ToString();          //G_CMP_TAX
            ixlFOS_JYUCHU_H.Cell(90, 2).Value = row["CreateDate"].ToString();       //CREATE_DATE
            ixlFOS_JYUCHU_H.Cell(91, 2).Value = row["Creator"].ToString();          //CREATOR
            ixlFOS_JYUCHU_H.Cell(92, 2).Value = row["UpdateDate"].ToString();       //UPDATE_DATE
            ixlFOS_JYUCHU_H.Cell(93, 2).Value = row["Updater"].ToString();          //UPDATER
        }
        private void SetExcelFOS_JYUCHU_D(IXLWorksheet ixlFOS_JYUCHU_D, DataRow dt, int intColNo)
        {
            ixlFOS_JYUCHU_D.Cell(1, intColNo).Value = dt["SaveKey"].ToString();//SAVE_KEY
            ixlFOS_JYUCHU_D.Cell(2, intColNo).Value = dt["OrderNo"].ToString();//ORDER_NO
            ixlFOS_JYUCHU_D.Cell(3, intColNo).Value = dt["OrderNoLine"].ToString();//ORDER_NO_LINE
            ixlFOS_JYUCHU_D.Cell(4, intColNo).Value = dt["OrderState"].ToString();//ORDER_STATE
            ixlFOS_JYUCHU_D.Cell(5, intColNo).Value = dt["DenNo"].ToString();//DEN_NO
            ixlFOS_JYUCHU_D.Cell(6, intColNo).Value = dt["DenSort"].ToString();//DEN_SORT
            ixlFOS_JYUCHU_D.Cell(7, intColNo).Value = dt["DenNoLine"].ToString();//DEN_NO_LINE
            ixlFOS_JYUCHU_D.Cell(8, intColNo).Value = dt["EstimateNo"].ToString();//ESTIMATE_NO
            ixlFOS_JYUCHU_D.Cell(9, intColNo).Value = dt["EstCoNo"].ToString();//EST_CO_NO
            ixlFOS_JYUCHU_D.Cell(10, intColNo).Value = dt["LineNo"].ToString();//LINE_NO
            ixlFOS_JYUCHU_D.Cell(11, intColNo).Value = dt["SyobunCd"].ToString();//SYOBUN_CD
            ixlFOS_JYUCHU_D.Cell(12, intColNo).Value = dt["SyohinCd"].ToString();//SYOHIN_CD
            ixlFOS_JYUCHU_D.Cell(13, intColNo).Value = dt["SyohinName"].ToString();//SYOHIN_NAME
            ixlFOS_JYUCHU_D.Cell(14, intColNo).Value = dt["Code5"].ToString();//CODE5
            ixlFOS_JYUCHU_D.Cell(15, intColNo).Value = dt["Kikaku"].ToString();//KIKAKU
            ixlFOS_JYUCHU_D.Cell(16, intColNo).Value = dt["Suryo"].ToString();//SURYO
            ixlFOS_JYUCHU_D.Cell(17, intColNo).Value = dt["Tani"].ToString();//TANI
            ixlFOS_JYUCHU_D.Cell(18, intColNo).Value = dt["Bara"].ToString();//BARA
            ixlFOS_JYUCHU_D.Cell(19, intColNo).Value = dt["TeiKigou"].ToString();//TEI_KIGOU
            ixlFOS_JYUCHU_D.Cell(20, intColNo).Value = dt["TeiKake"].ToString();//TEI_KAKE
            ixlFOS_JYUCHU_D.Cell(21, intColNo).Value = dt["TeiTan"].ToString();//TEI_TAN
            ixlFOS_JYUCHU_D.Cell(22, intColNo).Value = dt["TeiKin"].ToString();//TEI_KIN
            ixlFOS_JYUCHU_D.Cell(23, intColNo).Value = dt["Uauto"].ToString();//UAUTO
            ixlFOS_JYUCHU_D.Cell(24, intColNo).Value = dt["UriKigou"].ToString();//URI_KIGOU
            ixlFOS_JYUCHU_D.Cell(25, intColNo).Value = dt["UriKake"].ToString();//URI_KAKE
            ixlFOS_JYUCHU_D.Cell(26, intColNo).Value = dt["UriTan"].ToString();//URI_TAN
            ixlFOS_JYUCHU_D.Cell(27, intColNo).Value = dt["UriKin"].ToString();//URI_KIN
            ixlFOS_JYUCHU_D.Cell(28, intColNo).Value = dt["UriType"].ToString();//URI_TYPE
            ixlFOS_JYUCHU_D.Cell(29, intColNo).Value = dt["Gauto"].ToString();//GAUTO
            ixlFOS_JYUCHU_D.Cell(30, intColNo).Value = dt["SiiKigou"].ToString();//SII_KIGOU
            ixlFOS_JYUCHU_D.Cell(31, intColNo).Value = dt["SiiKake"].ToString();//SII_KAKE
            ixlFOS_JYUCHU_D.Cell(32, intColNo).Value = dt["SiiTan"].ToString();//SII_TAN
            ixlFOS_JYUCHU_D.Cell(33, intColNo).Value = dt["SiiAnswTan"].ToString();//SII_ANSW_TAN
            ixlFOS_JYUCHU_D.Cell(34, intColNo).Value = dt["SiiKin"].ToString();//SII_KIN
            ixlFOS_JYUCHU_D.Cell(35, intColNo).Value = dt["Nouki"].ToString();//NOUKI
            ixlFOS_JYUCHU_D.Cell(36, intColNo).Value = dt["TaxFlg"].ToString();//TAX_FLG
            ixlFOS_JYUCHU_D.Cell(37, intColNo).Value = dt["Dencd"].ToString();//DENCD
            ixlFOS_JYUCHU_D.Cell(38, intColNo).Value = dt["Urikubn"].ToString();//URIKUBN
            ixlFOS_JYUCHU_D.Cell(39, intColNo).Value = dt["SokoCd"].ToString();//SOKO_CD
            ixlFOS_JYUCHU_D.Cell(40, intColNo).Value = dt["Chuban"].ToString();//CHUBAN
            ixlFOS_JYUCHU_D.Cell(41, intColNo).Value = dt["ReqNouki"].ToString();//REQ_NOUKI
            ixlFOS_JYUCHU_D.Cell(42, intColNo).Value = dt["ShiresakiCd"].ToString();//SHIRESAKI_CD
            ixlFOS_JYUCHU_D.Cell(43, intColNo).Value = dt["Sbiko"].ToString();//SBIKO
            ixlFOS_JYUCHU_D.Cell(44, intColNo).Value = dt["Lbiko"].ToString();//LBIKO
            ixlFOS_JYUCHU_D.Cell(45, intColNo).Value = dt["Locat"].ToString();//LOCAT
            ixlFOS_JYUCHU_D.Cell(46, intColNo).Value = dt["OrderDenNo"].ToString();//ORDER_DEN_NO
            ixlFOS_JYUCHU_D.Cell(47, intColNo).Value = dt["OrderDenLineNo"].ToString();//ORDER_DEN_LINE_NO
            ixlFOS_JYUCHU_D.Cell(48, intColNo).Value = dt["DelFlg"].ToString();//DEL_FLG
            ixlFOS_JYUCHU_D.Cell(49, intColNo).Value = dt["MoveFlg"].ToString();//MOVE_FLG
            ixlFOS_JYUCHU_D.Cell(50, intColNo).Value = dt["InpDate"].ToString();//INP_DATE
            ixlFOS_JYUCHU_D.Cell(51, intColNo).Value = dt["Dseq"].ToString();//DSEQ
            ixlFOS_JYUCHU_D.Cell(52, intColNo).Value = dt["Hinban"].ToString();//HINBAN
            ixlFOS_JYUCHU_D.Cell(53, intColNo).Value = dt["AddDetailFlg"].ToString();//ADD_DETAIL_FLG
            ixlFOS_JYUCHU_D.Cell(54, intColNo).Value = dt["OyahinFlg"].ToString();//OYAHIN_FLG
            ixlFOS_JYUCHU_D.Cell(55, intColNo).Value = dt["Oyahinb"].ToString();//OYAHINB
            ixlFOS_JYUCHU_D.Cell(56, intColNo).Value = dt["HopeOrderNo"].ToString();//HOPE_ORDER_NO
            ixlFOS_JYUCHU_D.Cell(57, intColNo).Value = dt["HopeMeisaiNo"].ToString();//HOPE_MEISAI_NO
            ixlFOS_JYUCHU_D.Cell(58, intColNo).Value = dt["EcoFlg"].ToString();//ECO_FLG
            ixlFOS_JYUCHU_D.Cell(59, intColNo).Value = dt["OpsOrderNo"].ToString();//OPS_ORDER_NO
            ixlFOS_JYUCHU_D.Cell(60, intColNo).Value = dt["OpsRecYmd"].ToString();//OPS_REC_YMD
            ixlFOS_JYUCHU_D.Cell(61, intColNo).Value = dt["OpsLineno"].ToString();//OPS_LINENO
            ixlFOS_JYUCHU_D.Cell(62, intColNo).Value = dt["OpsSokocd"].ToString();//OPS_SOKOCD
            ixlFOS_JYUCHU_D.Cell(63, intColNo).Value = dt["OpsShukkadt"].ToString();//OPS_SHUKKADT
            ixlFOS_JYUCHU_D.Cell(64, intColNo).Value = dt["OpsNyukabi"].ToString();//OPS_NYUKABI
            ixlFOS_JYUCHU_D.Cell(65, intColNo).Value = dt["OpsKonpo"].ToString();//OPS_KONPO
            ixlFOS_JYUCHU_D.Cell(66, intColNo).Value = dt["OpsTani"].ToString();//OPS_TANI
            ixlFOS_JYUCHU_D.Cell(67, intColNo).Value = dt["OpsBara"].ToString();//OPS_BARA
            ixlFOS_JYUCHU_D.Cell(68, intColNo).Value = dt["OpsUtanka"].ToString();//OPS_UTANKA
            ixlFOS_JYUCHU_D.Cell(69, intColNo).Value = dt["OpsUauto"].ToString();//OPS_UAUTO
            ixlFOS_JYUCHU_D.Cell(70, intColNo).Value = dt["OpsUkigo"].ToString();//OPS_UKIGO
            ixlFOS_JYUCHU_D.Cell(71, intColNo).Value = dt["OpsUritu"].ToString();//OPS_URITU
            ixlFOS_JYUCHU_D.Cell(72, intColNo).Value = dt["OpsSyohinCd"].ToString();//OPS_SYOHIN_CD
            ixlFOS_JYUCHU_D.Cell(73, intColNo).Value = dt["OpsKikaku"].ToString();//OPS_KIKAKU
            ixlFOS_JYUCHU_D.Cell(74, intColNo).Value = dt["GOrderNo"].ToString();//G_ORDER_NO
            ixlFOS_JYUCHU_D.Cell(75, intColNo).Value = dt["GReserveQty"].ToString();//G_RESERVE_QTY
            ixlFOS_JYUCHU_D.Cell(76, intColNo).Value = dt["GDeliveryOrderQty"].ToString();//G_DELIVERY_ORDER_QTY
            ixlFOS_JYUCHU_D.Cell(77, intColNo).Value = dt["GDeliveredQty"].ToString();//G_DELIVERED_QTY
            ixlFOS_JYUCHU_D.Cell(78, intColNo).Value = dt["GCompleteFlg"].ToString();//G_COMPLETE_FLG
            ixlFOS_JYUCHU_D.Cell(79, intColNo).Value = dt["GDiscount"].ToString();//G_DISCOUNT
            ixlFOS_JYUCHU_D.Cell(80, intColNo).Value = dt["CreateDate"].ToString();//CREATE_DATE
            ixlFOS_JYUCHU_D.Cell(81, intColNo).Value = dt["Creator"].ToString();//CREATOR
            ixlFOS_JYUCHU_D.Cell(82, intColNo).Value = dt["UpdateDate"].ToString();//UPDATE_DATE
            ixlFOS_JYUCHU_D.Cell(83, intColNo).Value = dt["Updater"].ToString();//UPDATER
        }
        #endregion

        #region HAT-80確認PgDn処理の補完処理

        /// <summary>受注情報の補完とバリデーション</summary>
        /// <param name="pattern">1:確認PgDn 2:受注確定 3:発注照合</param>
        /// <returns>成否</returns>
        private async Task<bool> CompleteAndValidateAsync(int pattern)
        {
            var headerRequest = createCompleteHeaderRequest();
            var detailsRequest = CreateCompleteDetailsRequest();

            // ヘッダの補完
            var headerResponse = await ApiHelper.FetchAsync(this, async () =>
            {
                return await fosJyuchuRepo.GetCompleteHeader(headerRequest);
            });
            if (headerResponse.Failed)
            {
                return false;
            }
            // 明細の補完
            var detailsResponse = await ApiHelper.FetchAsync(this, async () =>
            {
                return await fosJyuchuRepo.GetCompleteDetails(detailsRequest);
            });
            if (detailsResponse.Failed)
            {
                return false;
            }
            // ヘッダの補完と明細の補完、両方が完了してから画面に反映する
            syncCompleteHeader(headerResponse.Value);
            SyncCompleteDetails(detailsRequest, detailsResponse.Value);

            SetDataHeader();
            SetDetailData();
            IntCheckPtn = pattern;
            if (!IsChkInputDataButton(IntCheckPtn, 1))
            {
                return false;
            }
            if (!ValidateInterestRate(pattern != 1))
            {
                return false;
            }
            if (!await ValidateByServerAsync())
            {
                return false;
            }
            HatFComParts.InitMessageArea(txtroNote);
            return true;
        }

        /// <summary>サーバ通信が必要なバリデーション</summary>
        /// <returns>成否</returns>
        private async Task<bool> ValidateByServerAsync()
        {
            var validateResult = true;
            // 仕入先コード
            if (txtSHIRESAKI_CD.Enabled)
            {
                var companys = await ApiHelper.FetchAsync(this, async () =>
                {
                    return await SearchRepo.GetInstance().searchSupplier(txtSHIRESAKI_CD.Text, null, null, null);
                });
                if (companys.Failed)
                {
                    // APIエラーは即時失敗
                    return false;
                }
                if (!companys.Value.Any())
                {
                    HatFComParts.ShowMessageAreaError(txtroNote, "仕入先情報が存在しません。");
                    HatFComParts.SetColorOnErrorControl(txtSHIRESAKI_CD);
                    validateResult = false;
                }
            }

            // 与信額
            {
                // 子番が9(運賃)以外の項目について売上価格×バラ数の合計を取得
                var totalAmount = Enumerable.Range(1, 6).Select(i => GetUcName(i))
                    .Where(r => r.Enabled && r.Koban != 9)
                    .Sum(r => (r.Bara ?? r.Suuryo) * r.UriTan);
                var url = string.Format(ApiResources.HatF.Client.CheckCredit, txtTOKUI_CD.Text);
                var result = await ApiHelper.FetchAsync(this, async () =>
                {
                    return await Program.HatFApiClient.GetAsync<CheckCreditResult>(url, new { Amount = totalAmount });
                });
                if (result.Failed)
                {
                    // APIエラーは即時失敗
                    return false;
                }
                switch (result.Value)
                {
                    case CheckCreditResult.NoCompany:
                        DialogHelper.WarningMessage(this, "得意先コードが存在しません。");
                        validateResult = false;
                        break;
                    case CheckCreditResult.CreditOver:
                        DialogHelper.WarningMessage(this, "与信限度額を超過しています。");
                        validateResult = false;
                        break;
                }
            }
            return validateResult;
        }

        /// <summary>
        /// 現ページのヘッダDataRowを取得
        /// </summary>
        /// <returns>ヘッダDataRow</returns>
        private DataRow getCurHeader()
        {
            return dtHeader_jhMain.Rows[int.Parse(this.txtroFooterPageNo.Text) - 1];
        }
        /// <summary>
        /// 現ページの明細行DataRowを取得 
        /// </summary>
        /// <param name="denNoLine">DenNoLine</param>
        /// <returns>明細行DataRow</returns>
        private DataRow GetCurDetail(string denNoLine)
        {
            var curHeader = getCurHeader();
            var saveKey = curHeader["SaveKey"]?.ToString();
            var denSort = curHeader["DenSort"]?.ToString();
            return GetDetailRow(saveKey, denSort, denNoLine);
        }

        /// <summary>現ページの明細行DataRowをすべて取得 </summary>
        /// <returns>明細行DataRow</returns>
        private List<DataRow> GetCurDetails()
        {
            var curHeader = getCurHeader();
            var saveKey = curHeader["SaveKey"]?.ToString();
            var denSort = curHeader["DenSort"]?.ToString();
            return dtDetail_jhMain.AsEnumerable()
                .Where(x => x["SaveKey"]?.ToString() == saveKey)
                .Where(x => x["DenSort"]?.ToString() == denSort)
                .ToList();
        }

        /// <summary>
        /// 明細行のDataRowを取得
        /// </summary>
        /// <param name="saveKey">SaveKey</param>
        /// <param name="denSort">DenSort</param>
        /// <param name="denNoLine">DenNoLine</param>
        /// <returns>明細行DataRow</returns>
        private DataRow GetDetailRow(string saveKey, string denSort, string denNoLine)
        {
            return dtDetail_jhMain.AsEnumerable().FirstOrDefault(
                x => x["SaveKey"]?.ToString() == saveKey
                        && x["DenSort"]?.ToString() == denSort
                        && x["DenNoLine"]?.ToString() == denNoLine
            );
        }
        /// <summary>
        /// ヘッダ補完リクエストの作成
        /// </summary>
        /// <returns>ヘッダ補完リクエスト</returns>
        private CompleteHeaderRequest createCompleteHeaderRequest()
        {
            var curHeader = getCurHeader();
            return new CompleteHeaderRequest()
            {
                TeamCd = this.txtTEAM_CD.Text,
                TokuiCd = this.txtTOKUI_CD.Text,
                GenbaCd = this.txtGENBA_CD.Text,
                TantoCd = curHeader["TantoCD"]?.ToString(),
                KeymanCd = this.txtKMAN_CD.Text,
                KoujitenCd = this.txtKOUJITEN_CD.Text,
                JyuchuInitial = this.txtJYU2.Text,
                SokoCd = this.cmbSOKO_CD.GetSelectedCode(),
                ShiresakiCd = this.txtSHIRESAKI_CD.Text,
                Hkbn = this.cmbHKBN.GetSelectedCode(),
                Sirainm = this.txtSIRAINM.Text,
                BinCd = this.cmbBINCD.GetSelectedCode(),
                DenNo = this.txtDEN_NO.Text,
            };
        }
        /// <summary>
        /// ヘッダ補完処理
        /// </summary>
        /// <param name="result">ヘッダ補完結果</param>
        private void syncCompleteHeader(CompleteHeaderResult result)
        {
            var curHeader = getCurHeader();

            // 【画面項目をAPI戻り値で上書きする項目】
            // TantoName:担当者名
            curHeader["TantoName"] = result.TantoName;
            // JyuchuCd:受注者（起票者）Cd
            curHeader["Jyu2Cd"] = result.JyuchuCd;
            // TokuiName:得意先名
            this.txtroTOKUI_NAME.Text = result.TokuiName;
            // KeymanName:キーマン名
            this.txtroKMAN_NAME.Text = result.KeymanName;
            // KoujitenName:工事店名
            this.txtroKOUJITEN_NAME.Text = result.KoujitenName;
            // SokoName:倉庫名
            curHeader["SokoName"] = result.SokoName;
            // ShiresakiNameH:仕入先名
            this.txtroSHIRESAKI_NAME.Text = result.ShiresakiName;
            // BinName:扱便名
            curHeader["Binname"] = result.BinName;
            // ShireKa:仕入課
            curHeader["ShireKa"] = result.ShireKa;
            // 伝No
            txtDEN_NO.Text = result.DenNo;

            // Hkbn:注文書（発注方法）
            // (HAT-12)修正 20240305
            /// 受発注画面を初期表示⇒[確認PgDn]とすると、[発注方法]に「5：プリントアウト」が自動選択されるの対応
            string strDEN_FLG = (this.cmbDEN_FLG.GetSelectedCode() == null) ? @"" : this.cmbDEN_FLG.GetSelectedCode();
            var jhOrderState = (JHOrderState)txtroHattyuJyoutai.Text;
            if (jhOrderState.IsBeforeComplete && (strDEN_FLG.Equals(@"15") || strDEN_FLG.Equals(@"21")))
            {
                if (cmbHKBN.Text == "")
                {
                    var kbn = clientRepo.Options.DivHachus.FirstOrDefault(n => n.Code == result.Hkbn);
                    if (kbn != null)
                    {
                        var szKubun = $"{kbn.Code}:{kbn.Name}";
                        cmbHKBN.Text = szKubun;
                    }
                }
            }
            // MailAddress:送信時用メールアドレス
            if (curHeader["SmailAdd"] == DBNull.Value)
            {
                curHeader["SmailAdd"] = result.MailAddress;

            }
            // Fax:FAX
            if (txtSFAX.Text == "")
            {
                txtSFAX.Text = result.Fax;
            }

            //【RecvName1～2、RecvAddress1～3、RecvTelのすべての画面項目が空白の場合にAPI戻り値を設定する項目】
            if (
                txtRECV_NAME1.Text == ""
                && txtRECV_NAME2.Text == ""
                && txtRECV_POSTCODE.Text == ""
                && txtRECV_ADD1.Text == ""
                && txtRECV_ADD2.Text == ""
                && txtRECV_ADD3.Text == ""
                && txtRECV_TEL.Text == ""
            )
            {
                txtRECV_NAME1.Text = result.RecvName1;
                txtRECV_NAME2.Text = result.RecvName2;
                txtRECV_POSTCODE.Text = result.RecvPostcode;
                txtRECV_ADD1.Text = result.RecvAddress1;
                txtRECV_ADD2.Text = result.RecvAddress2;
                txtRECV_ADD3.Text = result.RecvAddress3;
                txtRECV_TEL.Text = result.RecvTel;
            }
        }
        /// <summary>
        /// 明細補完リクエストの作成
        /// </summary>
        /// <returns>明細補完リクエスト</returns>
        private CompleteDetailsRequest CreateCompleteDetailsRequest()
        {
            var curHeader = getCurHeader();
            int Hkbn = -1;
            int.TryParse(cmbHKBN.GetSelectedCode(), out Hkbn);

            var TeikaKingakuSum = HatFComParts.DoParseDecimal(txtroFooterTeika.Text);
            var UriageKingakuSum = HatFComParts.DoParseDecimal(txtroFooterBaigaku.Text);
            var ShiireKingakuSum = HatFComParts.DoParseDecimal(txtroFooterShigaku.Text);
            var Profit = HatFComParts.DoParseDecimal(txtroFooterArari.Text);
            var ProfitRate = HatFComParts.DoParseDecimal(txtroFooterRiritsu.Text);

            var ucRows = new List<JH_Main_Detail>
                { ucRow1, ucRow2, ucRow3, ucRow4, ucRow5, ucRow6 };

            var details = ucRows
                .Select(GetDetailReqRow)
                .Where(d => d != null)
                .ToArray();

            return new CompleteDetailsRequest()
            {
                SyobunCdChk = chkHeaderBunrui.Checked,
                Hkbn = Hkbn,
                OrderFlag = txtroORDER_FLAG.Text,
                DenFlag = cmbDEN_FLG.GetSelectedCode(),
                OrderState = curHeader["OrderState"]?.ToString(),
                DenCd = txtDEN_NO.Text,
                SokoCd = cmbSOKO_CD.GetSelectedCode(),
                Nouki = HatFComParts.DoParseDateTime(dateNOUKI.Value),
                TokuiCd = txtTOKUI_CD.Text,
                TeamCd = txtTEAM_CD.Text,
                TeikaKingakuSum = (decimal)(TeikaKingakuSum.HasValue ? TeikaKingakuSum : 0),
                UriageKingakuSum = (decimal)(UriageKingakuSum.HasValue ? UriageKingakuSum : 0),
                ShiireKingakuSum = (decimal)(ShiireKingakuSum.HasValue ? ShiireKingakuSum : 0),
                Profit = (decimal)(Profit.HasValue ? Profit : 0),
                ProfitRate = (decimal)(ProfitRate.HasValue ? ProfitRate : 0),
                Details = details,
            };
        }
        /// <summary>
        /// 明細行の補完リクエスト作成
        /// </summary>
        /// <param name="row">明細行コントロール</param>
        /// <returns>明細行補完リクエスト</returns>
        private CompleteDetailsRequestDetail GetDetailReqRow(JH_Main_Detail row)
        {
            if (row.txtroRowNo.Text == "" || row.SyohinCode == "")
            {
                return null;
            }
            return new CompleteDetailsRequestDetail
            {
                Koban = HatFComParts.DoParseShort(row.txtKoban.Text),
                SyohinCd = row.SyohinCode,
                SokoCd = row.SokoCd,
                UriKubun = row.UriKubun,
                UriageKigou = row.UriKigou,
                UriageKakeritsu = row.Urikake,
                UriageTanka = row.UriTan,
                ShiireKigou = row.SiiKigou,
                ShiireKakeritsu = row.SiiKake,
                ShiireTanka = row.SiiTan,
                ShiireKaitouTanka = row.ShiireKaitouTanka,
                TeikaTanka = row.TeikaTanka,
                Suuryo = row.Suuryo,
                Tani = row.Tani,
                BaraCount = row.Bara,
            };
        }
        /// <summary>
        /// 明細補完処理
        /// </summary>
        /// <param name="request">要求</param>
        /// <param name="result">明細補完結果</param>
        private void SyncCompleteDetails(CompleteDetailsRequest request, CompleteDetailsResult result)
        {
            txtroFooterTeika.Text = HatFComParts.FormatOrDefault(HatFComParts.DoFormatN0(result.TeikaKingakuSum), x => x, txtroFooterTeika.Text);
            txtroFooterBaigaku.Text = HatFComParts.FormatOrDefault(HatFComParts.DoFormatN0(result.UriageKingakuSum), x => x, txtroFooterBaigaku.Text);
            txtroFooterShigaku.Text = HatFComParts.FormatOrDefault(HatFComParts.DoFormatN0(result.ShiireKingakuSum), x => x, txtroFooterShigaku.Text);
            txtroFooterArari.Text = HatFComParts.FormatOrDefault(HatFComParts.DoFormatN0(result.Profit), x => x, txtroFooterArari.Text);
            txtroFooterRiritsu.Text = HatFComParts.FormatOrDefault(HatFComParts.DoFormatN1(result.ProfitRate), x => x, txtroFooterRiritsu.Text);

            var ucRows = new List<JH_Main_Detail>
                { ucRow1, ucRow2, ucRow3, ucRow4, ucRow5, ucRow6 };

            result.Details?
                .Select((d, i) => (d, i))
                .ToList()
                .ForEach(x => SyncCompleteDetailsRow(ucRows[x.i], request.Details[x.i], x.d));
        }
        /// <summary>
        /// 明細行補完処理
        /// </summary>
        /// <param name="row">明細行コントロール</param>
        /// <param name="request">要求</param>
        /// <param name="result">明細行補完結果</param>
        private void SyncCompleteDetailsRow(JH_Main_Detail row, CompleteDetailsRequestDetail request, CompleteDetailsResultDetail result)
        {
            var detailRow = GetCurDetail(row.txtroRowNo.Text);

            row.Koban = result.Koban;
            row.SyohinCode = result.SyohinCd;
            row.Zaikosuu = result.Stock;
            // バラ数が入力できない状況なら入力済みのバラ数を無視する
            row.Bara = row.numBARA.Enabled ? row.Bara ?? result.Bara : result.Bara;
            // 売上単価、掛率
            if (!row.Urikake.HasValue && !row.UriTan.HasValue)
            {
                row.Urikake = result.Urikake ?? row.Urikake;
                row.UriTan = result.UriageTanka ?? row.UriTan;
                row.UriTanIsKTanka = result.HasUriageKTanka;
            }
            if (detailRow != null)
            {
                detailRow["UriKin"] = (object)result.UriageKingaku ?? DBNull.Value;
            }

            // 仕入単価、掛率
            if (!row.SiiKake.HasValue && !row.SiiTan.HasValue)
            {
                row.SiiKake = result.SiireKake ?? row.SiiKake;
                // 以下の条件を満たす場合、仕入回答単価の値を仕入単価として採用する
                // ・手配中・回答待ち状態
                // ・回答単価入力あり
                // ・仕入単価入力なし
                if (GetCurrentHattyuJotai() == JHOrderState.Ordered &&
                    result.ShiireKaitouTanka.HasValue &&
                    !request.ShiireTanka.HasValue)
                {
                    row.SiiTan = result.ShiireKaitouTanka;
                }
                else
                {
                    row.SiiTan = result.SiireTanka;
                }
            }
            // SiireKingaku:仕入金額
            if (detailRow != null)
            {
                detailRow["SiiKin"] = (object)result.SiireKingaku ?? DBNull.Value;
            }
            // ShiireKaitouTanka:仕入回答単価
            if (result.ShiireKaitouTanka.HasValue)
            {
                row.ShiireKaitouTanka = result.ShiireKaitouTanka;
            }
            // TeikaTanka:定価単価
            if (result.TeikaTanka.HasValue)
            {
                row.TeikaTanka = result.TeikaTanka;
            }
            // TeikaKingaku:定価金額
            if (detailRow != null)
            {
                detailRow["TeiKin"] = (object)result.TeikaKingaku ?? DBNull.Value;
            }

        }
        #endregion


        private volatile bool _windowUpdating = false;

        /// <summary>
        /// ウィンドウ枠のドラッグ開始
        /// </summary>
        private void JH_Main_ResizeBegin(object sender, EventArgs e)
        {
            // 複数回処理防止
            if (!_windowUpdating)
            {
                _windowUpdating = true;
                //SuspendLayoutAll();
                this.SuspendLayout();
            }
        }

        /// <summary>
        /// ウィンドウ枠のドラッグ終了（ウィンドウサイズ確定）
        /// </summary>
        private async void JH_Main_ResizeEnd(object sender, EventArgs e)
        {
            // このイベントは resizeKit1_ResizeEnd より先に発生するが、
            // 先に resizeKit1_ResizeEnd を処理させたいので少し待つ
            Application.DoEvents();
            await Task.Delay(100);

            // 複数回処理防止
            if (_windowUpdating)
            {
                // 念のため、少し待っても resizeKit1_ResizeEnd が動作していないようなら実施
                _windowUpdating = false;
                //ResumeLayoutAll();
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// ResizeKit がコントロールの再配置を完了
        /// </summary>
        private void resizeKit1_ResizeEnd(object sender, Newtone.ResizeKit.Win.ResizeKit.ResizeEventArgs e)
        {
            // このイベント自体は JH_Main_ResizeEnd より後で発生するが
            // JH_Main_ResizeEnd側でディレイをかけているので先に処理される

            // 複数回処理防止
            if (_windowUpdating)
            {
                _windowUpdating = false;
                //ResumeLayoutAll();
                this.ResumeLayout();
            }
        }

        private List<Control> GetAllControls(Control parentControl)
        {
            var controls = new List<Control>();
            controls.Add(parentControl);
            foreach (Control childControl in parentControl.Controls)
            {
                controls.AddRange(GetAllControls(childControl));
            }
            return controls;
        }

        private void SuspendLayoutAll()
        {
            var controls = GetAllControls(this);
            foreach (var ctrl in controls)
            {
                if (ctrl.HasChildren)
                {
                    ctrl.SuspendLayout();
                }
            }
        }

        private void ResumeLayoutAll()
        {
            var controls = GetAllControls(this);
            foreach (var ctrl in controls)
            {
                if (ctrl.HasChildren)
                {
                    ctrl.ResumeLayout();
                }
            }

            // 全体再描画
            this.Invalidate();
        }
    }
}

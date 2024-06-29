using C1.Win.C1FlexGrid;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls.BlobStrage;
using HatFClient.Repository;
using HatFClient.ViewModels;
using HatFClient.Views.MasterSearch;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HatFClient.Views.ConstructionProject
{
    /// <summary>物件詳細画面</summary>
    public partial class ConstructionDetail : Form
    {
        private const string FILTER_CSV = "csv file|*.csv";
        private const string FILTER_ACCDB = "accdb file|*.accdb";
        private readonly List<string> EnableControlList;
        private readonly List<string> DisableControlList;

        /// <summary>物件情報</summary>
        private ViewConstructionDetail ConstructionData { get; set; }
        private string constructionCode { get; set; }
        private string lockUserName;
        private string lockEditStartDateTime;

        private ScreenMode currentMode = ScreenMode.NewEntry;

        public enum ScreenMode
        {
            /// <summary>新規登録</summary>
            NewEntry,
            /// <summary>編集</summary>
            Edit,
            /// <summary>閲覧</summary>
            ReadOnly
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="constructionData">物件詳細情報。新規作成時はnullとする。</param>
        public ConstructionDetail()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
            }
            cmbORDER_CONFIDENCE.Items.AddRange(new[] { "A", "B", "C","受注","違注" });
            cmbORDER_STATE.Items.AddRange(new[] { "引合", "見積作成", "見積提出", "受注済", "完了" });
            EnableControlList = GetEnabledControlList();
            DisableControlList = GetDisableControlList();
        }

        public void LoadDataAndShowAsync(ScreenMode currentMode, ViewConstructionDetail ConstructionData = null)
        {
            //フォーム位置固定
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);

            this.currentMode = currentMode;
            if (currentMode == ScreenMode.NewEntry)
            {
                lblLockInfo.Visible = false;
                btnUnlock.Visible = false;
                blobStrageForm1.Enabled = false;
            }

            if (currentMode == ScreenMode.Edit)
            {
                this.constructionCode = ConstructionData.物件コード;
                this.ConstructionData = ConstructionData;

                ConstructionLockAndSetting();

                ObjectToScreen();
                ClearC1FlexGridArea();
                blobStrageForm1.Init("constructioninfo_" + constructionCode);
                //TETRAのダミーデータセット
                LoadTotoTetra();
            }

            this.Show();
            this.Activate();
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void ConstructionDetail_Load(object sender, EventArgs e)
        {
            // LoadDataAndShowAsyncで処理を行っているので、現状は処理なし
        }

        /// <summary>物件情報を画面に表示する</summary>
        private void ObjectToScreen()
        {
            // 物件情報
            txtCONSTRUCTTON_CODE.Text = ConstructionData.物件コード;
            txtSUBSCRIPTNUM.Text = ConstructionData.検索キー;

            short? orderState = ConstructionData.受注状態;
            if (orderState.HasValue && orderState.Value >= 0 && orderState.Value <= 4)
            {
                cmbORDER_STATE.SelectedIndex = orderState.Value;
            }

            txtCONSTRUCTION_NAME.Text = ConstructionData.物件名;
            txtCONSTRUCTION_KANA.Text = ConstructionData.物件名フリガナ;
            dateINQUIRY_DATE.Value = ConstructionData.引合日;
            dateESTIMATE_SEND_DATE.Value = ConstructionData.見積送付日;
            dateORDER_RCEIPT_DATE.Value = ConstructionData.注文書受領日;
            dateORDER_CONTRACT_RCEIPT_DATE.Value = ConstructionData.注文請書受領日;
            dateORDER_CONTRACT_SEND_DATE.Value = ConstructionData.注文請書送付日;
            dateORDER_COMPELTED_DATE.Value = ConstructionData.受注対応完了日;
            chkRECOMMENDED.Checked = ConstructionData.推薦物件 == true;
            chkUNCONTRACTED.Checked = ConstructionData.未契約物件 == true;
            chkSALES_EVENT.Checked = ConstructionData.セール == true;
            chkBALANCE_SHEET.Checked = ConstructionData.Bs == true;
            chkTHAILAND.Checked = ConstructionData.タイ == true;
            txtRECOMMEND_COMMENT.Text = ConstructionData.物件備考;

            // 得意先情報
            txtTOKUI_CD.Text = ConstructionData.得意先コード;
            txtTOKUI_NAME.Text = ConstructionData.得意先名;
            txtTOKUI_ADD.Text = ConstructionData.得意先住所;

            // 現場情報
            txtRECV_POSTCODE.Text = ConstructionData.現場郵便番号;
            txtRECV_ADD1.Text = ConstructionData.現場住所1;
            txtRECV_ADD2.Text = ConstructionData.現場住所2;
            txtRECV_ADD3.Text = ConstructionData.現場住所3;
            // TODO ★宛先1、宛先2がCONSTRUCTIONテーブルに見当たらない
            //txtRECV_NAME1.Text = ConstructionData.現場宛先1;
            //txtRECV_NAME2.Text = ConstructionData.現場宛先2;
            txtRECV_TEL.Text = ConstructionData.現場tel;
            txtRECV_FAX.Text = ConstructionData.現場fax;

            // 建設会社情報
            txtCONSTRUCTOR_NAME.Text = ConstructionData.建設会社名;
            txtCONSTRUCTOR_REP_NAME.Text = ConstructionData.建設会社代表者名;
            txtCONSTRUCTOR_TEL.Text = ConstructionData.建設会社tel;
            txtCONSTRUCTOR_FAX.Text = ConstructionData.建設会社fax;

            // コメント入力欄
            txtCOMMENT.Text = ConstructionData.コメント;
        }

        /// <summary>画面から物件情報を取得する</summary>
        private Construction ScreenToObject()
        {
            var result = new Construction();

            result.ConstructionCode = txtCONSTRUCTTON_CODE.Text;
            result.SearchKey = txtSUBSCRIPTNUM.Text;

            int stateIndex = -1;
            for (int i = 0; i < cmbORDER_STATE.Items.Count; i++)
            {
                if (cmbORDER_STATE.Items[i].ToString() == cmbORDER_STATE.Text)
                {
                    stateIndex = i;
                    break;
                }
            }
            if (stateIndex != -1)
            {
                // インデックスが見つかった場合の処理
                result.OrderState = (short?)stateIndex;
            }

            result.ConstructionName = txtCONSTRUCTION_NAME.Text;
            result.ConstructionKana = txtCONSTRUCTION_KANA.Text;

            /*
            for (int i = 0; i < cmbORDER_CONFIDENCE.Items.Count; i++)
            {
                if (cmbORDER_CONFIDENCE.Items[i].ToString() == cmbORDER_CONFIDENCE.Text)
                {
                    result.OrderConfidence = cmbORDER_CONFIDENCE.Items[i].ToString();
                    break;
                }
            }
            */

            // Valueプロパティがobject型を返すため、null許容のDateTimeに安全にキャスト
            result.InquiryDate = dateINQUIRY_DATE.Value as DateTime?;
            result.EstimateSendDate = dateESTIMATE_SEND_DATE.Value as DateTime?;
            result.OrderRceiptDate = dateORDER_RCEIPT_DATE.Value as DateTime?;
            result.OrderContractRceiptDate = dateORDER_CONTRACT_RCEIPT_DATE.Value as DateTime?;
            result.OrderContractSendDate = dateORDER_CONTRACT_SEND_DATE.Value as DateTime?;
            result.OrderCompeltedDate = dateORDER_COMPELTED_DATE.Value as DateTime?;


            result.Recommended = chkRECOMMENDED.Checked;
            result.Uncontracted = chkUNCONTRACTED.Checked;
            result.SalesEvent = chkSALES_EVENT.Checked;
            result.BalanceSheet = chkBALANCE_SHEET.Checked;
            result.Thailand = chkTHAILAND.Checked;
            //result.ConstructtonNotes = txtRECOMMEND_COMMENT.Text;

            // 得意先情報
            result.TokuiCd = txtTOKUI_CD.Text;
            //result.得意先名 = txtTOKUI_NAME.Text;
            //result.得意先住所 = txtTOKUI_ADD.Text;

            // 現場情報
            result.RecvPostcode = txtRECV_POSTCODE.Text;
            result.RecvAdd1 = txtRECV_ADD1.Text;
            result.RecvAdd2 = txtRECV_ADD2.Text;
            result.RecvAdd3 = txtRECV_ADD3.Text;
            // TODO ★宛先1、宛先2がCONSTRUCTIONテーブルに見当たらない
            //result.現場宛先1 = txtRECV_NAME1.Text;
            //result.現場宛先2 = txtRECV_NAME2.Text;
            result.RecvTel = txtRECV_TEL.Text;
            result.RecvFax = txtRECV_FAX.Text;

            // 建設会社情報
            result.ConstructorName = txtCONSTRUCTOR_NAME.Text;
            result.ConstructorRepName = txtCONSTRUCTOR_REP_NAME.Text;
            result.ConstructorTel = txtCONSTRUCTOR_TEL.Text;
            result.ConstructorFax = txtCONSTRUCTOR_FAX.Text;
            result.Comment = txtCOMMENT.Text;

            return result;
        }

        #region << ショートカット制御 >>
        private void ConstructionDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11: //閉じる
                    if (btnCancel.Enabled == true)
                        btnCancel.PerformClick();
                    break;
                case Keys.F12:　//登録・更新
                    if (btnUpdateConstructio.Enabled == true)
                        btnUpdateConstructio.PerformClick();
                    break;
            }
        }
        #endregion

        /// <summary>得意先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void BtnTOKUI_SEARCH_Click(object sender, EventArgs e)
        {
            using var form = new MS_Tokui();

            // TODO ★内部の検索処理でTEAM_CDを使用しているため設定する必要がある。
            // ログインユーザのチームコードがLoginRepo.CurrentUserにあればよかったが含まれていない
            // ログインAPIを修正する？？？
            form.TxtTEAM_CD = "";

            form.TxtTOKUI_CD = txtTOKUI_CD.Text;
            if (form.ShowDialog() == DialogResult.OK)
            {
                txtTOKUI_CD.Text = form.StrMsTokuiCode;
                txtTOKUI_NAME.Text = form.StrMsTokuiName;
                // TODO ★受発注画面の得意先検索画面では住所情報を扱っていない！
                txtTOKUI_ADD.Text = form.StrMsTokuiName;
            }
        }

        /// <summary>必須項目の入力チェック</summary>
        public bool CheckInputs()
        {
            if (string.IsNullOrEmpty(txtCONSTRUCTTON_CODE.Text))
            {
                DialogHelper.WarningMessage(this, "物件コードが入力されていません。");
                return false;
            }

            if (string.IsNullOrEmpty(txtCONSTRUCTION_NAME.Text))
            {
                DialogHelper.WarningMessage(this, "物件名が入力されていません。");
                return false;
            }

            if (string.IsNullOrEmpty(txtTOKUI_CD.Text))
            {
                DialogHelper.WarningMessage(this, "得意先コードが入力されていません。");
                return false;
            }
            return true;
        }

        private async void btnUpdateConstructio_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {
                Construction constructionDetail = ScreenToObject();

                if (currentMode == ScreenMode.Edit)
                {
                    var detail = await ApiHelper.UpdateAsync(this, () =>
                        Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.UpdateConstructionDetail, constructionDetail)
                    );
                }
                else if (currentMode == ScreenMode.NewEntry)
                {
                    if (await CheckDuplicateConstructionCode(txtCONSTRUCTTON_CODE.Text))
                    {
                        await ApiHelper.UpdateAsync(this, () =>
                            Program.HatFApiClient.PutAsync<string>(ApiResources.HatF.Client.AddConstructionDetail, constructionDetail)
                        );
                    }
                    else
                    {
                        DialogHelper.WarningMessage(this, "物件コードが重複しています。");
                    }
                    constructionCode = constructionDetail.ConstructionCode;
                    ConstructionLockAndSetting();
                }
            }
        }

        /// <summary>
        /// 編集設定
        /// </summary>
        private void EditSetting()
        {
            lblScreenMode.ForeColor = System.Drawing.SystemColors.WindowText;
            lblLockInfo.Visible = false;
            btnUnlock.Visible = false;
            lblScreenMode.Text = "物件編集";
            currentMode = ScreenMode.Edit;
            SetFlexGridAllowEditing(true);
            EnableControls(this);
        }

        /// <summary>
        /// 読み取り設定
        /// </summary>
        private void ReadSetting()
        {
            lblScreenMode.ForeColor = System.Drawing.Color.Red;
            lblScreenMode.Text = "読み取り専用";
            lblLockInfo.Text = "編集者：" + lockUserName
                + "\r\n"
                +"編集開始日時："+ lockEditStartDateTime;
            this.currentMode = ScreenMode.ReadOnly;
            DisableControls(this);
            SetFlexGridAllowEditing(false);
        }

        private void SetFlexGridAllowEditing(bool value) 
        {
            grdListToto.AllowEditing = value;
            grdListSinryo.AllowEditing = value;
            grdListSekisui.AllowEditing = value;
            grdListNacss.AllowEditing = value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearC1FlexGridArea()
        {
            //TODO  gridリセット
            txtTOTO_PATH.Clear();
            txtSINRYO_PATH.Clear();
            txtSEKISUI_PATH.Clear();
            txtNACSS_PATH.Clear();
            grdListToto.Clear(ClearFlags.Content);
            grdListSinryo.Clear(ClearFlags.Content);
            grdListSekisui.Clear(ClearFlags.Content);
            grdListNacss.Clear(ClearFlags.Content);
            grdListToto.DataSource = null;
            grdListSinryo.DataSource = null;
            grdListSekisui.DataSource = null;
            grdListNacss.DataSource = null;
            grdListToto.Refresh();
            grdListSinryo.Refresh();
            grdListSekisui.Refresh();
            grdListNacss.Refresh();
        }

        public void LoadTotoTetra()
        {
            // 初期設定（列数、行数、フォント、列幅、ヘッダ設定など）
            grdListToto.Cols.Count = 11;
            grdListToto.Rows.Count = 52; // 必要な行数に合わせて設定
            grdListToto.Font = new System.Drawing.Font("メイリオ", 9);
            grdListToto.Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

            // 列幅の設定
            var colWidths = new int[] { 50, 80, 80, 80, 60, 100, 250, 80, 120, 120, 120 };
            for (int i = 0; i < colWidths.Length; i++)
            {
                grdListToto.Cols[i].Width = colWidths[i];
            }

            // ヘッダ情報の設定
            var headers = new string[] { "No", "種別", "注番", "ステータス", "5桁", "商品コード", "商品名", "数量", "単価", "金額", "納期" };
            for (int i = 0; i < headers.Length; i++)
            {
                grdListToto[0, i] = headers[i];
            }

            // データの準備
            object[,] rowData = new object[,]
            {
                {1, "単品", "32AG60C1", "計上済", "FK511", "TT TKS05303J", "TOTO TKS05303J", "11", "24,540", "269,940", "2023/11/02"},
                {2, "単品", "32AG60C2", "計上済", "FK511", "TT T28KUNH13", "TOTO T 28KUNH13", "3", "6,600", "19,800", "2023/11/02"},
                {3, "単品", "32AG60C3", "計上済", "FK511", "TT T28KUNH13", "TOTO T 28KUNH13", "10", "6,600", "66,000", "2023/11/02"},
                {4, "単品", "32AG60C4", "計上済", "FQ532", "532**********", "TOTO REKB25A2 ", "13", "215,400", "2,800,200", "2023/11/02"},
                {5, "単品", "32AG60C5", "計上済", "FQ532", "532**********", "TOTO RHE708R", "13", "1,740", "22,620", "2023/11/02"},
                {6, "単品", "32AG60C6", "計上済", "FQ532", "TT RHE22H-50N", "TOTO RHE22H-50N", "13", "10,260", "133,380", "2023/11/02"},
                {7, "セット品", "32AJ01C1", "未計上", "FD503", "TT TCF9530#NW1", "TOTO  TCF9530    #NW1", "1", "136,800", "136,800", "2023/09/07"},
                {8, "セット1", "32AJ01C2", "未計上", "FD503", "TT CS921B#NW1", "TOTO  CS921B     #NW1", "1", "93,000", "93,000", "2023/09/07"},
                {9, "セット2", "32AJ01C3", "未計上", "FA523", "TT CS370B#NW1", "TOTO  CS370B     #NW1", "5", "22,080", "110,400", "2023/09/07"},
                {10,"単品", "32AJ01C4", "未計上", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "5", "26,280", "131,400", "2023/09/07"},
                {11,"単品", "32AJ01C5", "未計上", "FA523", "TT CS370BP#NW1", "TOTO  CS370BP    #NW1", "1", "20,640", "20,640", "2023/09/07"},
                {12,"単品", "32AJ01C6", "未計上", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "1", "26,280", "26,280", "2023/09/07"},
                {13,"単品", "32AJ01C9", "計上済", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "9,720", "9,720", "00/00/00"},
                {14,"セット品", "32AJ02C1", "計上済", "FA526", "TT Y9206", "TOTO  Y9206", "1", "3,180", "3,180", "2023/09/07"},
                {15,"セット1", "32AJ02C2", "計上済", "FB502", "TT TCF2223#NW1", "TOTO  TCF2223    #NW1", "6", "51,900", "311,400", "2023/09/07"},
                {16,"単品", "32AJ38C1", "計上済", "FK511", "TT TW11GR", "TOTO  TW 11GR", "7", "8,820", "61,740", "2023/09/09"},
                {17,"単品", "32AJ38C9", "計上済", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "1,400", "1,400", "00/00/00"},
                {18, "セット品", "32AJ01C1", "未計上", "FD503", "TT TCF9530#NW1", "TOTO  TCF9530    #NW1", "1", "136,800", "136,800", "2023/09/07"},
                {19, "セット1", "32AJ01C2", "未計上", "FD503", "TT CS921B#NW1", "TOTO  CS921B     #NW1", "1", "93,000", "93,000", "2023/09/07"},
                {20, "セット2", "32AJ01C3", "未計上", "FA523", "TT CS370B#NW1", "TOTO  CS370B     #NW1", "5", "22,080", "110,400", "2023/09/07"},
                {21,"単品", "32AJ02C2", "未計上", "FB502", "TT TCF2223#NW1", "TOTO  TCF2223    #NW1", "6", "51,900", "311,400", "2023/09/07"},
                {22,"単品", "32AJ38C1", "未計上", "FK511", "TT TW11GR", "TOTO  TW 11GR", "7", "8,820", "61,740", "2023/09/09"},
                {23,"単品", "32AJ38C9", "未計上", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "1,400", "1,400", "00/00/00"},
                {24,"セット品", "32AJ01C1", "計上済", "FD503", "TT TCF9530#NW1", "TOTO  TCF9530    #NW1", "1", "136,800", "136,800", "2023/09/07"},
                {25,"セット1", "32AJ01C2", "計上済", "FD503", "TT CS921B#NW1", "TOTO  CS921B     #NW1", "1", "93,000", "93,000", "2023/09/07"},
                {26,"セット2", "32AJ01C3", "計上済", "FA523", "TT CS370B#NW1", "TOTO  CS370B     #NW1", "5", "22,080", "110,400", "2023/09/07"},
                {27,"セット3", "32AJ01C4", "計上済", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "5", "26,280", "131,400", "2023/09/07"},
                {28,"セット4", "32AJ01C5", "計上済", "FA523", "TT CS370BP#NW1", "TOTO  CS370BP    #NW1", "1", "20,640", "20,640", "2023/09/07"},
                {29,"単品", "32AJ01C6", "計上済", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "1", "26,280", "26,280", "2023/09/07"},
                {30,"単品", "32AJ01C9", "計上済", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "9,720", "9,720", "00/00/00"},
                {31,"単品", "32AJ02C1", "計上済", "FA526", "TT Y9206", "TOTO  Y9206", "1", "3,180", "3,180", "2023/09/07"},
                {32,"単品", "32AJ02C2", "計上済", "FB502", "TT TCF2223#NW1", "TOTO  TCF2223    #NW1", "6", "51,900", "311,400", "2023/09/07"},
                {33,"単品", "32AJ38C1", "計上済", "FK511", "TT TW11GR", "TOTO  TW 11GR", "7", "8,820", "61,740", "2023/09/09"},
                {34,"単品", "32AJ38C9", "計上済", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "1,400", "1,400", "00/00/00"},
                {35,"単品", "32AG60C1", "未計上", "FK511", "TT TKS05303J", "TOTO TKS05303J", "11", "24,540", "269,940", "2023/11/02"},
                {36,"単品", "32AG60C2", "未計上", "FK511", "TT T28KUNH13", "TOTO T 28KUNH13", "3", "6,600", "19,800", "2023/11/02"},
                {37,"単品", "32AG60C3", "未計上", "FK511", "TT T28KUNH13", "TOTO T 28KUNH13", "10", "6,600", "66,000", "2023/11/02"},
                {38,"単品", "32AG60C4", "未計上", "FQ532", "532**********", "TOTO REKB25A2 ", "13", "215,400", "2,800,200", "2023/11/02"},
                {39,"単品", "32AG60C5", "未計上", "FQ532", "532**********", "TOTO RHE708R", "13", "1,740", "22,620", "2023/11/02"},
                {40,"単品", "32AG60C6", "未計上", "FQ532", "TT RHE22H-50N", "TOTO RHE22H-50N", "13", "10,260", "133,380", "2023/11/02"},
                {41,"単品", "32AJ01C1", "未計上", "FD503", "TT TCF9530#NW1", "TOTO  TCF9530    #NW1", "1", "136,800", "136,800", "2023/09/07"},
                {42,"単品", "32AJ01C2", "未計上", "FD503", "TT CS921B#NW1", "TOTO  CS921B     #NW1", "1", "93,000", "93,000", "2023/09/07"},
                {43,"単品", "32AJ01C3", "未計上", "FA523", "TT CS370B#NW1", "TOTO  CS370B     #NW1", "5", "22,080", "110,400", "2023/09/07"},
                {44,"単品", "32AJ01C4", "未計上", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "5", "26,280", "131,400", "2023/09/07"},
                {45,"単品", "32AJ01C5", "未計上", "FA523", "TT CS370BP#NW1", "TOTO  CS370BP    #NW1", "1", "20,640", "20,640", "2023/09/07"},
                {46,"単品", "32AJ01C6", "未計上", "FA523", "TT SH370BA#NW1", "TOTO  SH370BA    #NW1", "1", "26,280", "26,280", "2023/09/07"},
                {47,"単品", "32AJ01C9", "未計上", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "9,720", "9,720", "00/00/00"},
                {48,"単品", "32AJ02C1", "未計上", "FA526", "TT Y9206", "TOTO  Y9206", "1", "3,180", "3,180", "2023/09/07"},
                {49,"単品", "32AJ02C2", "未計上", "FB502", "TT TCF2223#NW1", "TOTO  TCF2223    #NW1", "6", "51,900", "311,400", "2023/09/07"},
                {50,"単品", "32AJ38C1", "未計上", "FK511", "TT TW11GR", "TOTO  TW 11GR", "7", "8,820", "61,740", "2023/09/09"},
                {51,"単品", "32AJ38C9", "未計上", "FT595", "595**********", "TOTO  +B0B1Bﾊｲｿ-", "1", "1,400", "1,400", "00/00/00"},
            };

            // C1FlexGridにデータを設定
            for (int row = 0; row < rowData.GetLength(0); row++)
            {
                int gridRow = grdListToto.Rows.Count - 1;

                for (int col = 0; col < rowData.GetLength(1); col++)
                {
                    // 行番号は自動で挿入されるため、行番号の列はスキップ
                    if (col == 0)
                    {
                        grdListToto[row + 1, 0] = rowData[row, col];
                    }
                    else
                    {
                        // 実際のデータ列に+1をして調整（行番号列を考慮）
                        grdListToto[row + 1, col] = rowData[row, col];
                    }
                }
            }

            // 金額表示にする
            grdListToto.Cols[6].Format = "N0";
            grdListToto.Cols[7].Format = "N0";

            // 行の高さ設定
            for (int i = 0; i < grdListToto.Rows.Count; i++)
            {
                grdListToto.Rows[i].Height = 25;
            }

            // 左寄せにする
            for (int columnIndex = 4; columnIndex < 6; columnIndex++)
            {
                var col = grdListToto.Cols[columnIndex];
                if (col.Style == null)
                {
                    col.Style = grdListToto.Styles.Add("CustomStyle" + columnIndex);

                }
                col.Style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            }

            //先頭列にチェックボックスを追加
            CreateCheckBox(grdListToto, rowData.GetLength(0));

            grdListToto.Tree.Column = 2;
            grdListToto.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;

            int lastParentIndex = -1;

            for (int row = 1; row < grdListToto.Rows.Count; row++)
            {
                string itemName = grdListToto[row, 2]?.ToString() ?? "";

                if (itemName.Contains("セット品"))
                {
                    grdListToto.Rows[row].IsNode = true;
                    grdListToto.Rows[row].Node.Level = 0;
                    lastParentIndex = row;
                }
                else if (lastParentIndex != -1 && itemName.StartsWith("セット"))
                {
                    grdListToto.Rows[row].IsNode = true;
                    grdListToto.Rows[row].Node.Level = 1;
                }
                else
                {
                    grdListToto.Rows[row].IsNode = true;
                    grdListToto.Rows[row].Node.Level = 0;
                }
            }
        }
        private void btnTOTO_DIALOG_Click(object sender, EventArgs e)
        {
            txtTOTO_PATH.Text = OpenFileDialog(FILTER_CSV);
        }
        private void btnINVOICE_CSV_IMPORT_Click(object sender, EventArgs e)
        {
            if (FileCheck(txtTOTO_PATH.Text, ".csv"))
            {
                DataTable dt = CreateTotoImportColumns();
                dt = ImportCsv(txtTOTO_PATH.Text, dt, ",");

                var datas = dt.AsEnumerable()
                    .Select(x => new
                    {
                        col1 = x["総合受番"],
                        col5 = x["正規品番"],
                        col39 = x["品名"],
                        col6 = x["個数"],
                        col52 = x["単価"],
                        col51 = x["値引後単価"],
                        col11 = x["希望納期識別"],
                    });

                DataTable dt2 = CreateTotoColumns();
                DataRow dr;
                foreach (var data in datas)
                {
                    dr = dt2.NewRow();
                    dr["注番"] = data.col1;
                    dr["商品コード"] = data.col5;
                    dr["商品名"] = data.col39;
                    dr["数量"] = data.col6;
                    dr["値引率"] = CalcDiscountRate(Convert.ToDouble(data.col51), Convert.ToDouble(data.col52));
                    dr["単価"] = data.col52;
                    dr["値引後単価"] = data.col51;
                    dr["金額"] = Convert.ToInt32(data.col6) * Convert.ToInt32(data.col52);
                    dr["値引後金額"] = Convert.ToInt32(data.col6) * Convert.ToInt32(data.col51);
                    dr["納期"] = data.col11;

                    dt2.Rows.Add(dr);
                }

                grdListToto.AutoResize = true;
                grdListToto.DataSource = dt2;
                SetRowNo(grdListToto);
                SetColumnHeaderSize(grdListToto);
                CreateCheckBox(grdListToto, dt.Rows.Count);
            }
        }
        private string CalcDiscountRate(double discountPrice, double price)
        {
            double discountrate = (1 - (discountPrice / price)) * 100;
            //小数点第２位で四捨五入
            return Math.Round(discountrate, 1, MidpointRounding.AwayFromZero).ToString("F1") + "%";
        }

        private void btnSINRYO_DIALOG_Click(object sender, EventArgs e)
        {
            txtSINRYO_PATH.Text = OpenFileDialog(FILTER_CSV);
        }
        private void btnSINRYO_CSV_IMPORT_Click(object sender, EventArgs e)
        {
            if (FileCheck(txtSINRYO_PATH.Text, ".csv"))
            {
                DataTable dt = CreateSinryoColumns();
                grdListSinryo.AutoResize = true;
                grdListSinryo.DataSource = ImportCsv(txtSINRYO_PATH.Text, dt, ",");
                SetRowNo(grdListSinryo);
                SetColumnHeaderSize(grdListSinryo);
                CreateCheckBox(grdListSinryo, dt.Rows.Count);
            }
        }
        private void btnSEKISUI_DIALOG_Click(object sender, EventArgs e)
        {
            txtSEKISUI_PATH.Text = OpenFileDialog(FILTER_ACCDB);
        }

        private async void btnSEKISUI_CSV_IMPORT_Click(object sender, EventArgs e)
        {
            if (FileCheck(txtSEKISUI_PATH.Text, ".accdb"))
            {
                DataTable dt = new DataTable();
                grdListSekisui.AutoResize = true;
                using (var progressForm = new SimpleProgressForm())
                {
                    // この using ブロック内には、画面にアクセス(参照/設定)するコードを記述しないでください
                    progressForm.Start(this);
                    dt = await Task.Run(() => ImportAccdb(txtSEKISUI_PATH.Text, "T_雨どい拾い数量合算"));
                }

                SetRowNo(grdListSekisui);
                if (dt.Rows.Count != 0)
                {
                    grdListSekisui.DataSource = dt;
                    SetSekisuiFormat();
                    SetRowNo(grdListSekisui);
                    SetColumnHeaderSize(grdListSekisui);
                    CreateCheckBox(grdListSekisui, dt.Rows.Count);
                }
                else
                {
                    DialogHelper.WarningMessage(this, "アクセスに失敗しました。");
                }
            }
        }

        private void btnNACSS_DIALOG_Click(object sender, EventArgs e)
        {
            txtNACSS_PATH.Text = OpenFileDialog(FILTER_CSV);
        }

        private void btnNACSS_CSV_IMPORT_Click(object sender, EventArgs e)
        {
            if (FileCheck(txtNACSS_PATH.Text, ".csv"))
            {
                DataTable dt = CreateNacssColumns();
                grdListNacss.AutoResize = true;
                grdListNacss.DataSource = ImportCsv(txtNACSS_PATH.Text, dt, "\t");
                SetRowNo(grdListNacss);
                SetColumnHeaderSize(grdListNacss);
                CreateCheckBox(grdListNacss, dt.Rows.Count);
            }
        }

        /// <summary>
        /// 行番号を行ヘッダーに追加
        /// </summary>
        private void SetRowNo(C1FlexGrid grid)
        {
            for (int i = grid.Rows.Fixed; i < grid.Rows.Count; i++)
            {
                grid[i, 0] = i.ToString();
            }
            grid.Cols[0].Width = 30;

        }
        private void SetColumnHeaderSize(C1FlexGrid grid)
        {
            grid.Rows[0].Height = 20;
        }

        /// <summary>
        /// ファイルチェック
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="extension">拡張子</param>
        /// <returns></returns>
        private Boolean FileCheck(string path, string extension)
        {
            string msg = string.Empty;
            if (path == string.Empty)
            {
                DialogHelper.WarningMessage(this, "ファイルを選択してください。");
                return false;
            }
            if (Path.GetExtension(path) != extension)
            {
                DialogHelper.WarningMessage(this, "拡張子が正しくありません。");
                return false;
            }
            if (!System.IO.File.Exists(path))
            {
                DialogHelper.WarningMessage(this, "ファイルが存在しません。");
                return false;
            }
            return true;
        }
        private string OpenFileDialog(string filter)
        {
            string result = string.Empty;
            using (OpenFileDialog ofd = new())
            {
                ofd.Title = "ファイルダイアログ";
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK) { result = ofd.FileName; }
            }
            return result;
        }
        /// <summary>
        /// ダミーデータ 削除予定
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTotoImportColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("総合受番");
            dt.Columns.Add("個別受番");
            dt.Columns.Add("行番号");
            dt.Columns.Add("行付加番号");
            dt.Columns.Add("正規品番");
            dt.Columns.Add("個数");
            dt.Columns.Add("拠点指定");
            dt.Columns.Add("出荷拠点");
            dt.Columns.Add("希望出荷日");
            dt.Columns.Add("希望着日");
            dt.Columns.Add("希望納期識別");
            dt.Columns.Add("在庫グループ");
            dt.Columns.Add("手配番号");
            dt.Columns.Add("出荷日");
            dt.Columns.Add("荷姿");
            dt.Columns.Add("便名指定フラグ");
            dt.Columns.Add("便名");
            dt.Columns.Add("業者");
            dt.Columns.Add("未定");
            dt.Columns.Add("現場搬入");
            dt.Columns.Add("送り先（店番）");
            dt.Columns.Add("送り先（コード）");
            dt.Columns.Add("送り先名");
            dt.Columns.Add("送り先名（現場）");
            dt.Columns.Add("特販店コード");
            dt.Columns.Add("ターミナル");
            dt.Columns.Add("運賃");
            dt.Columns.Add("朝夕");
            dt.Columns.Add("納品番号");
            dt.Columns.Add("伝票番号");
            dt.Columns.Add("発店ＴＥＬ");
            dt.Columns.Add("着店ＴＥＬ");
            dt.Columns.Add("ガス");
            dt.Columns.Add("ＢＬ（ラベル）");
            dt.Columns.Add("新旧指定");
            dt.Columns.Add("単価指定");
            dt.Columns.Add("メーカー希望小売価格");
            dt.Columns.Add("色名");
            dt.Columns.Add("品名");
            dt.Columns.Add("入力品番");
            dt.Columns.Add("入力個数");
            dt.Columns.Add("切替前品番");
            dt.Columns.Add("貴注文番号");
            dt.Columns.Add("適要");
            dt.Columns.Add("商品識別区分");
            dt.Columns.Add("回答出荷日");
            dt.Columns.Add("回答着日");
            dt.Columns.Add("希望着日2");
            dt.Columns.Add("営業製品");
            dt.Columns.Add("値引製品");
            dt.Columns.Add("値引後単価");
            dt.Columns.Add("単価");
            dt.Columns.Add("現場名(カナ)");
            dt.Columns.Add("折衝番号");
            dt.Columns.Add("得意先コード");
            dt.Columns.Add("重点販売店コード");
            dt.Columns.Add("重点お得意先コード");
            dt.Columns.Add("送り先郵便番号");
            dt.Columns.Add("送り先電話番号");
            dt.Columns.Add("送り住所（カナ）");
            dt.Columns.Add("送り住所（漢字）");
            dt.Columns.Add("現場名(漢字)");
            dt.Columns.Add("お客様注文番号");
            dt.Columns.Add("統計製品CD");
            dt.Columns.Add("セット品番");
            dt.Columns.Add("注文主（店番）");
            dt.Columns.Add("注文主（コード）");
            dt.Columns.Add("現場No");
            dt.Columns.Add("発注日");
            dt.Columns.Add("column70");

            return dt;
        }
        private DataTable CreateTotoColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("注番");
            dt.Columns.Add("商品コード");
            dt.Columns.Add("商品名");
            dt.Columns.Add("数量");
            dt.Columns.Add("値引率");
            dt.Columns.Add("単価");
            dt.Columns.Add("値引後単価");
            dt.Columns.Add("金額");
            dt.Columns.Add("値引後金額");
            dt.Columns.Add("納期");

            return dt;
        }
        private DataTable CreateSinryoColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("記号");
            dt.Columns.Add("分類");
            dt.Columns.Add("HAT商品コード");
            dt.Columns.Add("メーカー商品");
            dt.Columns.Add("商品名称");
            dt.Columns.Add("数量");
            dt.Columns.Add("単位");
            dt.Columns.Add("バラ数");
            dt.Columns.Add("売記");
            dt.Columns.Add("売率");
            dt.Columns.Add("売上単価");
            dt.Columns.Add("売上金額");
            dt.Columns.Add("売/定");
            dt.Columns.Add("仕記");
            dt.Columns.Add("仕率");
            dt.Columns.Add("仕入単価");
            dt.Columns.Add("仕入金額");
            dt.Columns.Add("仕/定");
            dt.Columns.Add("仕入先CD");
            dt.Columns.Add("粗利益");
            dt.Columns.Add("粗利率");
            dt.Columns.Add("参考仕入単価");
            dt.Columns.Add("定価単価");
            dt.Columns.Add("定価金額");
            dt.Columns.Add("備考");

            return dt;
        }
        private DataTable CreateNacssColumns()
        {
            DataTable dt = new DataTable();
            //TODO カラム名が決まっていないため、仮設定
            for (int i = 1; i <= 500; i++)
            {
                dt.Columns.Add("test" + i.ToString());
            }
            return dt;
        }

        private DataTable ImportCsv(string inputpath, DataTable dt, string delimiter)
        {
            var parser = new TextFieldParser(inputpath, Encoding.GetEncoding("Shift-JIS"))
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new string[] { delimiter }
            };
            while (!parser.EndOfData)
            {
                dt.Rows.Add(parser.ReadFields());
            }
            return dt;
        }
        private DataTable ImportAccdb(string inputpath, string tablename)
        {
            DataTable dt = new DataTable();

            OleDbConnection connection = new OleDbConnection();
            OleDbCommand command = new OleDbCommand();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + inputpath;
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM " + tablename;
                command.Connection = connection;
                OleDbDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch
            {
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return dt;
        }
        private void CreateCheckBox(C1FlexGrid grid, int count)
        {
            // 新しい列を先頭に挿入
            grid.Cols.Insert(1);
            grid.Cols[1].Width = 35;

            // 挿入した列の設定
            grid.Cols[1].Name = "Select";
            grid.Cols[1].Caption = "選択";
            grid.Cols[1].AllowEditing = true;

            for (int row = 1; row < count + 1; row++)
            {
                grid.SetCellCheck(row, 1, CheckEnum.Unchecked);
            }
            grid.Cols["Select"].ImageAlign = ImageAlignEnum.CenterCenter;

        }

        /// <summary>
        /// gridからチェック済みのデータを取得する
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private DataTable GetCheckedData(C1FlexGrid grid)
        {
            DataTable dt = new DataTable();
            for (int col = 2; col < grid.Cols.Count; col++)
            {
                dt.Columns.Add(grid.Cols[col].Caption);
            }
            for (int row = 1; row < grid.Rows.Count; row++)
            {
                if (grid.GetCellCheck(row, 1) == CheckEnum.Checked)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 2; col < grid.Cols.Count; col++)
                    {
                        dr[grid.Cols[col].Caption] = grid[row, col];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private void SetSekisuiFormat()
        {
            grdListSekisui.Cols["無償扱い単価"].Format = "C";
            grdListSekisui.Cols["有償扱い単価"].Format = "C";
        }

        /// <summary>
        /// 有効にするコントロールのリスト
        /// </summary>
        /// <returns></returns>
        private List<string> GetEnabledControlList()
        {
            List<string> list = new List<string>();
            //閉じるボタン
            list.Add(btnCancel.Name);
            //タブ
            list.Add(tabIMPORT.Name);
            list.Add(tabCOMPANY_INFO.Name);
            list.Add(tabTOTO.Name);
            list.Add(tabSHINRYO.Name);
            list.Add(tabSEKISUI.Name);
            list.Add(tabNACSS.Name);
            //C1FLEXGRID
            list.Add(grdListToto.Name);
            list.Add(grdListSinryo.Name);
            list.Add(grdListSekisui.Name);
            list.Add(grdListNacss.Name);

            //色変更用
            list.Add(lblScreenMode.Name);

            list.Add(btnUnlock.Name);

            return list;
        }

        /// <summary>
        /// 無効にするコントロールのリスト
        /// </summary>
        /// <returns></returns>
        private List<string> GetDisableControlList()
        {
            List<string> list = new List<string>();
            //BlobStrageForm用
            list.Add("btnDownload");
            list.Add("btnDelete");

            list.Add(txtCONSTRUCTTON_CODE.Name);

            return list;
        }

        /// <summary>
        /// 物件詳細ロック機能
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string,string>> ConstructionLock()
        {
            var result = await Program.HatFApiClient.PostAsync<Dictionary<string, string>>
                (ApiResources.HatF.Client.ConstructionLock, new Dictionary<string, object>()
                {
                            { "constructionCode", constructionCode},
                            { "empid",LoginRepo.GetInstance().CurrentUser.EmployeeCode }
                });
            return result.Data;
        }
        /// <summary>
        /// 物件詳細アンロック機能
        /// </summary>
        private async Task<bool> ConstructionUnlock()
        {
            var result = await Program.HatFApiClient.PostAsync<bool>(ApiResources.HatF.Client.ConstructionUnLock, new Dictionary<string, object>()
                {
                            { "constructionCode", constructionCode}
                });
            return result.Data;
        }
        /// <summary>
        /// 閲覧時にコントロールを非活性に変更（EnableControlListのコントールは含まない）
        /// </summary>
        /// <param name="parent"></param>
        private void DisableControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {

                if (!EnableControlList.Contains(control.Name))
                {
                    control.Enabled = false;
                }
                DisableControls(control);
            }
        }

        /// <summary>
        /// 編集時にコントロールを活性に変更（DisableControlListのコントロールは含まない）
        /// </summary>
        /// <param name="parent"></param>
        private void EnableControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (!DisableControlList.Contains(control.Name))
                {
                    control.Enabled = true;
                }
                EnableControls(control);
            }
        }
        /// <summary>
        /// Closing処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConstructionDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentMode == ScreenMode.Edit)
            {
                await ConstructionUnlock();
            }
        }

        private async void btnUnlock_Click(object sender, EventArgs e)
        {
            if (DialogHelper.YesNoQuestion(this, "読み取り専用を解除しますか？"))
            {
                await ConstructionUnlock();
                ConstructionLockAndSetting();
            }
        }
        private async void ConstructionLockAndSetting()
        {
            txtCONSTRUCTTON_CODE.Enabled = false;
            btnDuplicate.Visible = false;
            var result = await ConstructionLock();
            if (result.Count == 0)
            {
                EditSetting();
            }
            else
            {
                lockUserName = result["emp_name"];
                lockEditStartDateTime = result["edit_start_datetime"];
                ReadSetting();
            }
        }

        /// <summary>重複チェックボタン</summary>
        private async void btnDuplicate_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtCONSTRUCTTON_CODE.Text))
            {
                var result = await CheckDuplicateConstructionCode(txtCONSTRUCTTON_CODE.Text);
                if (result)
                {
                    DialogHelper.InformationMessage(this, "重複している物件コードはありません。");
                }
                else
                {
                    DialogHelper.WarningMessage(this, "物件コードが重複しています。");
                } 
            }
            else
            {
                DialogHelper.InformationMessage(this, "物件コードが入力されていません。");
            }
        }

        /// <summary>物件コード重複チェック</summary>
        /// <param name="constructioncode">物件コード</param>
        /// <returns>重複結果</returns>
        private async Task<bool> CheckDuplicateConstructionCode(string constructioncode)
        {
            var result =  await Program.HatFApiClient.PostAsync<bool>
                (ApiResources.HatF.Client.CheckDuplicateConstructionCode, new Dictionary<string, object>()
                {
                            { "constructionCode", constructioncode},
                });
            return result.Data;
        }

        /// <summary>
        /// 物件名コントロールがアクティブでなくなった際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCONSTRUCTION_NAME_Leave(object sender, EventArgs e)
        {
            //物件名テキストボックスの内容をカタカナに変換し、物件名フリガナテキストボックスに表示
            txtCONSTRUCTION_KANA.Text = ConversionKana(txtCONSTRUCTION_NAME.Text);
        }
        /// <summary>
        /// カタカナに変換
        /// </summary>
        /// <param name="str">入力文字</param>
        /// <returns>カタカナ</returns>
        private string ConversionKana(string str)
        {
            string result = string.Empty;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                result = excel.GetPhonetic(str);
            }
            finally
            {
                //エラー時にExcelのプロセスが残らないために必要
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
            return result;
        }
    }
}
using C1.Win.C1FlexGrid;
using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.CustomControls;
using HatFClient.Repository;
using HatFClient.Views.MasterSearch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.ConstructionProject
{
    public partial class NewConstructionDetail : Form
    {
        private const string FILTER_CSV = "csv file|*.csv";
        private const string FILTER_ACCDB = "accdb file|*.accdb";
        private readonly List<string> EnableControlList;
        private readonly List<string> DisableControlList;
        private ClientRepo clientRepo;
        private LoginRepo loginRepo;
        private FosJyuchuRepo fosJyuchuRepo;

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

        public NewConstructionDetail()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                FormStyleHelper.SetWorkWindowStyle(this);
            }
            cmbORDER_STATE.Items.AddRange(new[] { "引合", "見積作成", "見積提出", "受注済", "完了" });

            // 種別の設定
            cmbCONSTRUCTION_TYPE.Items.AddRange(new[]
            {
                "戸建",
                "HM・HB",
                "マンション",
                "アパート",
                "工場",
                "事務所",
                "宿泊施設",
                "店舗",
                "学校",
                "病院",
                "土木",
                "RF住宅",
                "RF非住宅",
                "高齢者施設",
                "太陽光",
                "その他"
            });

            // 業種の設定
            cmbCONSTRUCTION_INDUSTRY.Items.AddRange(new[]
            {
                "水工店",
                "燃料店",
                "リノベーション",
                "大手RF",
                "DV",
                "大手サブコン",
                "地場サブコン",
                "工務店",
                "中小RF",
                "プラント",
                "ゼネコン",
                "HM(PB)",
                "その他"
            });
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
                grd_D.Visible = false;
                btnADD_ROW.Visible = false;
                btnDELETE_ROW.Visible = false;
                btnAccounting.Visible = false;
                lblUpdate2.Visible = false;
                btnCopy.Visible = false;
                btnTransfer.Visible = false;
                groupBox3.Visible = false;
            }

            if (currentMode == ScreenMode.Edit)
            {
                this.constructionCode = ConstructionData.物件コード;
                this.ConstructionData = ConstructionData;

                ConstructionLockAndSetting();

                ObjectToScreen();
                //ClearC1FlexGridArea();
                blobStrageForm1.Init("constructioninfo_" + constructionCode);
                //TETRAのダミーデータセット
                //LoadTotoTetra();
            }

            this.Show();
            this.Activate();
        }

        /// <summary>画面初期化</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void NewConstructionDetail_Load(object sender, EventArgs e)
        {
            this.clientRepo = ClientRepo.GetInstance();
            this.loginRepo = LoginRepo.GetInstance();
            this.fosJyuchuRepo = FosJyuchuRepo.GetInstance();

            if (currentMode == ScreenMode.NewEntry)
            {
                txtMANAGER_ID.Text = loginRepo.CurrentUser.EmployeeTag;
            }

            LoadDData();
        }

        /// <summary>物件情報を画面に表示する</summary>
        private void ObjectToScreen()
        {
            // 物件情報
            txtCONSTRUCTTON_CODE.Text = ConstructionData.物件コード;
            txtTEAM_CD.Text = ConstructionData.チームcd;
            txtMANAGER_ID.Text = ConstructionData.担当社員id;

            short? orderState = ConstructionData.受注状態;
            if (orderState.HasValue && orderState.Value >= 0 && orderState.Value <= 4)
            {
                cmbORDER_STATE.SelectedIndex = orderState.Value;
            }

            txtCONSTRUCTION_NAME.Text = ConstructionData.物件名;
            txtCONSTRUCTION_KANA.Text = ConstructionData.物件名フリガナ;
            txtRECV_POSTCODE.Text = ConstructionData.現場郵便番号;
            txtRECV_ADD1.Text = ConstructionData.現場住所1;
            txtRECV_BUILDING.Text = ConstructionData.ビル名等;

            // 得意先情報
            txtTOKUI_CD.Text = ConstructionData.得意先コード;
            txtTOKUI_NAME.Text = ConstructionData.得意先名;
            txtKMAN_CD.Text = ConstructionData.キーマンcd;

            // 工事店情報
            txtCONSTRUCTOR_NAME.Text = ConstructionData.建設会社名;
            dateINQUIRY_DATE.Value = ConstructionData.引合日;

            short? constructionType = ConstructionData.建設種別;
            if (constructionType.HasValue && constructionType.Value >= 0 && constructionType.Value <= 15)
            {
                cmbCONSTRUCTION_TYPE.SelectedIndex = constructionType.Value;
            }

            short? constructionIndustry = ConstructionData.建設業種;
            if (constructionIndustry.HasValue && constructionIndustry.Value >= 0 && constructionIndustry.Value <= 12)
            {
                cmbCONSTRUCTION_INDUSTRY.SelectedIndex = constructionIndustry.Value;
            }

            // コメント入力欄
            txtCOMMENT.Text = ConstructionData.コメント;
        }

        /// <summary>画面から物件情報を取得する</summary>
        private Construction ScreenToObject()
        {
            var result = new Construction();

            result.ConstructionCode = txtCONSTRUCTTON_CODE.Text;
            result.TeamCd = txtTEAM_CD.Text;
            result.EmpId = txtMANAGER_ID.Text;

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

            result.TokuiCd = txtTOKUI_CD.Text;
            result.KmanCd = txtKMAN_CD.Text;

            // 物件情報
            result.ConstructionName = txtCONSTRUCTION_NAME.Text;
            result.ConstructionKana = txtCONSTRUCTION_KANA.Text;
            result.RecvPostcode = txtRECV_POSTCODE.Text;
            result.RecvAdd1 = txtRECV_ADD1.Text;
            result.BuildingNameEtc = txtRECV_BUILDING.Text;

            // 工事店情報
            result.ConstructorName = txtCONSTRUCTOR_NAME.Text;
            // Valueプロパティがobject型を返すため、null許容のDateTimeに安全にキャスト
            result.InquiryDate = dateINQUIRY_DATE.Value as DateTime?;
            int typeIndex = -1;
            for (int i = 0; i < cmbCONSTRUCTION_TYPE.Items.Count; i++)
            {
                if (cmbCONSTRUCTION_TYPE.Items[i].ToString() == cmbCONSTRUCTION_TYPE.Text)
                {
                    typeIndex = i;
                    break;
                }
            }
            if (typeIndex != -1)
            {
                // インデックスが見つかった場合の処理
                result.ConstructorType = (short?)typeIndex;
            }
            int industryIndex = -1;
            for (int i = 0; i < cmbCONSTRUCTION_INDUSTRY.Items.Count; i++)
            {
                if (cmbCONSTRUCTION_INDUSTRY.Items[i].ToString() == cmbCONSTRUCTION_INDUSTRY.Text)
                {
                    industryIndex = i;
                    break;
                }
            }
            if (industryIndex != -1)
            {
                // インデックスが見つかった場合の処理
                result.ConstructorIndustry = (short?)industryIndex;
            }

            //コメント
            result.Comment = txtCOMMENT.Text;

            return result;
        }

        /// <summary>
        /// C1FlexGridからデータを取得する
        /// ステータスはNULLの場合0:未計上を取得する
        /// </summary>
        /// <returns></returns>
        private List<HAT_F_api.Models.ConstructionDetail> GetGridToObject()
        {
            var result = new List<HAT_F_api.Models.ConstructionDetail>();

            for (int rowIndex = grd_D.Rows.Fixed; rowIndex < grd_D.Rows.Count; rowIndex++)
            {
                bool insert = false;
                insert = new string[]
                { "仕入先コード","受注確度","商品名", "数量","単位","バラ数"
                ,"定価単価","納期","売上掛率","売上単価","仕入掛率","仕入単価" }
                .Any(col => grd_D.GetData(rowIndex, col) != null);
                if (insert == false) continue;

                var data = new HAT_F_api.Models.ConstructionDetail();
                data.ConstructionCode = constructionCode;
                data.Koban = rowIndex;
                data.AppropState = (short?)grd_D.GetData(rowIndex, "ステータス") ?? 0; //0:未計上を設定
                data.ShiresakiCd = grd_D.GetData(rowIndex, "仕入先コード")?.ToString();
                data.OrderConfidence = grd_D.GetData(rowIndex, "受注確度")?.ToString();
                data.SyohinName = grd_D.GetData(rowIndex, "商品名")?.ToString();
                data.Suryo = (int?)grd_D.GetData(rowIndex, "数量") ?? null;
                data.Tani = grd_D.GetData(rowIndex, "単位")?.ToString();
                data.Bara = (int?)grd_D.GetData(rowIndex, "バラ数") ?? null;
                if (string.IsNullOrEmpty(grd_D.GetData(rowIndex, "定価単価")?.ToString()))
                {
                    data.TeiTan = null;
                }
                else
                {
                    data.TeiTan = Convert.ToDecimal(grd_D.GetData(rowIndex, "定価単価"));
                }

                data.Nouki = (DateTime?)grd_D.GetData(rowIndex, "納期") ?? null;
                if (string.IsNullOrEmpty(grd_D.GetData(rowIndex, "売上掛率")?.ToString()))
                {
                    data.UriKake = null;
                }
                else
                {
                    data.UriKake = Convert.ToDecimal(grd_D.GetData(rowIndex, "売上掛率"));
                }
                if (string.IsNullOrEmpty(grd_D.GetData(rowIndex, "売上単価")?.ToString()))
                {
                    data.UriTan = null;
                }
                else
                {
                    data.UriTan = Convert.ToDecimal(grd_D.GetData(rowIndex, "売上単価"));
                }
                if (string.IsNullOrEmpty(grd_D.GetData(rowIndex, "仕入掛率")?.ToString()))
                {
                    data.SiiKake = null;
                }
                else
                {
                    data.SiiKake = Convert.ToDecimal(grd_D.GetData(rowIndex, "仕入掛率"));
                }
                if (string.IsNullOrEmpty(grd_D.GetData(rowIndex, "仕入単価")?.ToString()))
                {
                    data.SiiTan = null;
                }
                else
                {
                    data.SiiTan = Convert.ToDecimal(grd_D.GetData(rowIndex, "仕入単価"));
                }
                result.Add(data);
            }

            //1件もない場合物件コードのみ
            if (result.Count == 0)
            {
                var data = new HAT_F_api.Models.ConstructionDetail();
                data.ConstructionCode = constructionCode;
                result.Add(data);
            }

            return result;
        }

        #region << ショートカット制御 >>
        private void ConstructionDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11: //登録・更新
                    if (btnUpdateConstructio.Enabled == true)
                        btnUpdateConstructio.PerformClick();
                    break;
                case Keys.F12: //閉じる
                    if (btnCancel.Enabled == true)
                        btnCancel.PerformClick();
                    break;
            }
        }
        #endregion

        /// <summary>得意先検索ボタン</summary>
        /// <param name="sender">イベント発生元</param>
        /// <param name="e">イベント情報</param>
        private void btnTOKUI_SEARCH_Click(object sender, EventArgs e)
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
                //txtTOKUI_ADD.Text = form.StrMsTokuiName;
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

                    List<HAT_F_api.Models.ConstructionDetail> gridDetailList = GetGridToObject();
                    var grid = await ApiHelper.UpdateAsync(this, () =>
                        Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.DeleteInsertConstructionDetailGrid, gridDetailList)
                    ,true);

                    foreach(var item in gridDetailList)
                    {
                        if (item.AppropState != null)
                        {
                            grd_D[item.Koban, "ステータス"] = item.AppropState;
                            grd_D[item.Koban, "子番"] = item.Koban;
                            //grd_D.SetCellCheck(item.Koban, 1, CheckEnum.Unchecked);
                        }
                    }

                    // チェックボックスの設定とステータス列の色設定を呼び出す
                    CreateCheckBox(grd_D, grd_D.Rows.Count - 1);
                    SetStatusColumnColors(grd_D);

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

            lblUpdate1.ForeColor = System.Drawing.Color.Red;
            lblUpdate2.ForeColor = System.Drawing.Color.Red;
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
                + "編集開始日時：" + lockEditStartDateTime;
            this.currentMode = ScreenMode.ReadOnly;
            DisableControls(this);
            SetFlexGridAllowEditing(false);
        }

        private void SetFlexGridAllowEditing(bool value)
        {
            //grdListToto.AllowEditing = value;
            //grdListSinryo.AllowEditing = value;
            //grdListSekisui.AllowEditing = value;
            //grdListNacss.AllowEditing = value;
            //grd_H.AllowEditing = value;
            grd_D.AllowEditing = value;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private void ClearC1FlexGridArea()
        //{
        //    //TODO  gridリセット
        //    txtTOTO_PATH.Clear();
        //    txtSINRYO_PATH.Clear();
        //    txtSEKISUI_PATH.Clear();
        //    txtNACSS_PATH.Clear();
        //    grdListToto.Clear(ClearFlags.Content);
        //    grdListSinryo.Clear(ClearFlags.Content);
        //    grdListSekisui.Clear(ClearFlags.Content);
        //    grdListNacss.Clear(ClearFlags.Content);
        //    grdListToto.DataSource = null;
        //    grdListSinryo.DataSource = null;
        //    grdListSekisui.DataSource = null;
        //    grdListNacss.DataSource = null;
        //    grdListToto.Refresh();
        //    grdListSinryo.Refresh();
        //    grdListSekisui.Refresh();
        //    grdListNacss.Refresh();
        //}

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
            //list.Add(tabIMPORT.Name);
            //list.Add(tabCOMPANY_INFO.Name);
            //list.Add(tabTOTO.Name);
            //list.Add(tabSHINRYO.Name);
            //list.Add(tabSEKISUI.Name);
            //list.Add(tabNACSS.Name);
            ////C1FLEXGRID
            //list.Add(grdListToto.Name);
            //list.Add(grdListSinryo.Name);
            //list.Add(grdListSekisui.Name);
            //list.Add(grdListNacss.Name);
            //list.Add(grd_H.Name);
            list.Add(grd_D.Name);

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

            // ★対応するまで無効にする
            list.Add(btnAppSheet.Name);
            list.Add(btnTransfer.Name);
            list.Add(btnCopy.Name);

            return list;
        }

        /// <summary>
        /// 物件詳細ロック機能
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> ConstructionLock()
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
            grd_D.Visible = true;
            btnADD_ROW.Visible = true;
            btnDELETE_ROW.Visible = true;

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
            if (!String.IsNullOrEmpty(txtCONSTRUCTTON_CODE.Text))
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
            var result = await Program.HatFApiClient.PostAsync<bool>
                (ApiResources.HatF.Client.CheckDuplicateConstructionCode, new Dictionary<string, object>()
                {
                            { "constructionCode", constructioncode},
                });
            return result.Data;
        }

        /// <summary>
        /// 物件名を物件名フリガナに表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKANA_Click(object sender, EventArgs e)
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

            result = excel.GetPhonetic(str);

            // エラー時にExcelのプロセスが残らないために必要
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

            return result;
        }

        public async void LoadDData()
        {
            grd_D.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid1_CellButtonClick);
            // 初期設定（列数、行数、フォント）
            grd_D.Cols.Count = 17;
            grd_D.Rows.Count = 2; //初期表示用
            grd_D.Select(1, grd_D.Cols["仕入先コード"].Index);
            grd_D.Font = new System.Drawing.Font("メイリオ", 9);
            grd_D.Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

            int row = 1;
            var griddata = await GetGridData();
            if (griddata.Count != 0) grd_D.Rows.Count = griddata.Count + 1;

            foreach (var item in griddata)
            {
                grd_D[row, "ステータス"] = item.AppropState;
                grd_D[row, "仕入先コード"] = item.ShiresakiCd;
                grd_D[row, "仕入先名"] = item.ShiresakiName;
                grd_D[row, "受注確度"] = item.OrderConfidence;
                grd_D[row, "商品名"] = item.SyohinName;
                grd_D[row, "数量"] = item.Suryo;
                grd_D[row, "単位"] = item.Tani;
                grd_D[row, "バラ数"] = item.Bara;
                grd_D[row, "定価単価"] = item.TeiTan;
                grd_D[row, "納期"] = item.Nouki;
                grd_D[row, "売上掛率"] = item.UriKake;
                grd_D[row, "売上単価"] = item.UriTan;
                grd_D[row, "仕入掛率"] = item.SiiKake;
                grd_D[row, "仕入単価"] = item.SiiTan;
                if (item.UriTan != null && item.SiiTan != null)
                {
                    decimal uritan = (decimal)item.UriTan;
                    decimal siitan = (decimal)item.SiiTan;
                    grd_D[row, "利率"] = CalcProfit(uritan, siitan);
                }
                grd_D[row, "子番"] = item.Koban;
                row++;
            }

            int statusColIndex = grd_D.Cols["ステータス"].Index;

            if (grd_D.Cols[statusColIndex] != null)
            {
                grd_D.Cols[statusColIndex].DataMap = new ListDictionary()
                {
                    { (short)short.MinValue, "" },
                    { (short)0, "未計上" },
                    { (short)1, "計上済" }
                };
            }
            grd_D.AutoResize = true;
            //ステータス編集不可
            grd_D.Cols["ステータス"].AllowEditing = false;
            grd_D.Cols["ステータス"].Style.TextAlign = TextAlignEnum.CenterCenter;
            grd_D.Cols["ステータス"].Style.BackColor = Color.Gray;

            // 新しい列を先頭に挿入
            grd_D.Cols.Insert(0);
            grd_D.Cols[1].Width = 35;

            // 挿入した列の設定
            grd_D.Cols[1].Name = "Select";
            grd_D.Cols[1].Caption = "選択";
            grd_D.Cols[1].AllowEditing = true;

            // ステータス列の色を設定
            SetStatusColumnColors(grd_D);

            //チェックボックスを更新
            CreateCheckBox(grd_D, row - 1);

            // 受注確度のコンボボックスを設定
            SetOrderConfidenceComboBox();
        }


        private void SetOrderConfidenceComboBox()
        {
            // 受注確度のコンボボックスに設定する項目
            string[] orderConfidenceOptions = new string[] { "A", "B", "C", "受注", "違注" };

            // コンボリストを作成
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < orderConfidenceOptions.Length; i++)
            {
                sb.Append(orderConfidenceOptions[i]);
                // c1FlexGridのComboListに設定する際に|で値を分割する。
                if (i < orderConfidenceOptions.Length - 1) sb.Append("|");
            }

            // "受注確度"列にコンボボックスを設定
            grd_D.Cols["受注確度"].ComboList = sb.ToString();
        }

        /// <summary>利率を算出して表示する</summary>
        public string CalcProfit(decimal UriTan,decimal SiiTan)
        {
            string result = string.Empty;
            // ([行.売上単価]-[行.仕入単価])÷[行.売上単価]×100 ※小数点第2位を四捨五入
            if (UriTan == 0m)
            {
                result = "エラー"; //TODO後で変更
            }
            else
            {
                result = HatFComParts.DoFormatN1((UriTan - SiiTan) / UriTan * 100);
            }
            return result;
        }

        private void c1FlexGrid1_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            
            switch (grd_D.Cols[e.Col].Caption)
            {
                case "仕入先コード":
                    using (Views.MasterSearch.MS_ShiresakiBunrui dlg = new())
                    {
                        dlg.TxtTEAM_CD = this.txtTEAM_CD.Text;
                        dlg.TxtSHIRESAKI_CD = grd_D.GetDataDisplay(e.Row, e.Col);
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                grd_D.SetData(e.Row, grd_D.Cols["仕入先コード"].Index,dlg.StrMsShiresakiCode);
                                grd_D.SetData(e.Row, grd_D.Cols["仕入先名"].Index, dlg.StrMsShiresakiName);
                                break;
                            default:
                                break;
                        }
                    }
                break;

                case "商品名":
                    using (Views.MasterSearch.MS_Syohin dlg = new())
                    {
                        //物件詳細は仕入先なし
                        dlg.TxtSHIRESAKI_CD = grd_D.GetDataDisplay(e.Row, "仕入先コード");
                        dlg.TxtSYOHIN_CD = grd_D.GetDataDisplay(e.Row, e.Col);
                        switch (dlg.ShowDialog())
                        {
                            case DialogResult.OK:
                                grd_D.SetData(e.Row, grd_D.Cols["商品名"].Index, dlg.StrMsSyohinName);
                                break;
                            default:
                                break;
                        }
                    }
                break;
            }
        }

        private void CreateCheckBox(C1FlexGrid grid, int count)
        {
            for (int row = 1; row < count + 1; row++)
            {
                // "ステータス"列の値を取得
                var statusValue = grid[row, "ステータス"]?.ToString();

                // "ステータス"列の値が"計上済"でない場合にのみチェックボックスを設定
                if (statusValue != "1") // 1 が "計上済" に対応
                {
                    grid.SetCellCheck(row, 1, CheckEnum.Unchecked);
                }
                else
                {
                    grid.SetCellCheck(row, 1, CheckEnum.None);
                    grid.Rows[row].AllowEditing = false;
                }
            }
            grid.Cols["Select"].ImageAlign = ImageAlignEnum.CenterCenter;
        }

        private void SetStatusColumnColors(C1FlexGrid grid)
        {
            // ステータス列のインデックスを取得
            int statusColIndex = grid.Cols["ステータス"].Index;

            for (int row = 1; row < grid.Rows.Count; row++)
            {
                var statusValue = grid[row, statusColIndex]?.ToString();

                if (statusValue == "1")
                {
                    grid.SetCellStyle(row, statusColIndex, CreateCellStyle(Color.SkyBlue));
                }
                else
                {
                    grid.SetCellStyle(row, statusColIndex, CreateCellStyle(Color.LightYellow));
                }
            }
        }

        private CellStyle CreateCellStyle(Color backColor)
        {
            CellStyle style = grd_D.Styles.Add("CustomStyle" + backColor.ToString());
            style.BackColor = backColor;
            return style;
        }


        private void btnAppSheet_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnADD_ROW_Click(object sender, EventArgs e)
        {
            grd_D.Rows.Add();
        }

        private async Task<List<HAT_F_api.CustomModels.ConstructionDetailEx>> GetGridData()
        {
            var parameters = new Dictionary<string, object>()
            {
                        { "constructionCode",constructionCode }
            };

            var apiResponse = await Program.HatFApiClient.GetAsync<List<HAT_F_api.CustomModels.ConstructionDetailEx>>(
                      ApiResources.HatF.Client.ConstructionDetailDetail, parameters);   // 一覧取得API

            return apiResponse.Data;
                
        }

        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDELETE_ROW_Click(object sender, EventArgs e)
        {
            //行削除時に行がずれないように後ろから削除する。
            for (int row = grd_D.Rows.Count - 1; row >= 1; row--)
            {
                if (grd_D.GetCellCheck(row, 1) == CheckEnum.Checked) // 先頭列に変更
                {
                    grd_D.Rows.Remove(row);
                }
            }
        }

        /// <summary>
        /// セルに入力した際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_D_AfterEdit(object sender, RowColEventArgs e)
        {
            //フォーマット
            if (e.Col == grd_D.Cols["定価単価"].Index)
            {
                grd_D[e.Row, "定価単価"] = HatFComParts.DoFormatN2(grd_D.GetDataDisplay(e.Row, "定価単価"));
            }
            if (e.Col == grd_D.Cols["売上単価"].Index)
            {
                grd_D[e.Row, "売上単価"] =  HatFComParts.DoFormatN2(grd_D.GetDataDisplay(e.Row, "売上単価"));
            }
            if (e.Col == grd_D.Cols["仕入単価"].Index)
            {
                grd_D[e.Row, "仕入単価"] = HatFComParts.DoFormatN2(grd_D.GetDataDisplay(e.Row, "仕入単価"));
            }

            //TODO 納期のチェック
            //if (!HatFComParts.BoolIsInputDateOver(dateNOUKI.Value, 90) && this.txtroHattyuJyoutai.Text == JHOrderState.PreOrder)
            //{ WorkOnError(dateNOUKI, ref focusBox, ref intIdx, strId, ref strMessage); }

            //利率表示
            if (e.Col == grd_D.Cols["売上単価"].Index || e.Col == grd_D.Cols["仕入単価"].Index)
            {
                if (!string.IsNullOrEmpty(grd_D.GetDataDisplay(e.Row, "売上単価"))
                    && !string.IsNullOrEmpty(grd_D.GetDataDisplay(e.Row, "仕入単価")))
                {
                    grd_D[e.Row, "利率"] = CalcProfit(Convert.ToDecimal(grd_D.GetDataDisplay(e.Row, "売上単価"))
                        , Convert.ToDecimal(grd_D.GetDataDisplay(e.Row, "仕入単価")));
                }
            }
        }

        /// <summary>
        /// 選択対象を計上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAccounting_ClickAsync(object sender, EventArgs e)
        {
            // チェックされたデータを取得
            var pages = getFosJyuchPages();

            // ページが空の場合、処理を中断
            if (pages.Count == 0)
            {
                return;
            }

            // ページがある場合、CommitPagesAsyncを実行
            var commitPagesResult = await ApiHelper.FetchAsync(this, () =>
            {
                return CommitPagesAsync(pages);
            });

            // ページの情報を更新
            var updatelist = new List<HAT_F_api.Models.ConstructionDetail>();
            foreach (DataRow dr in GetCheckedData(grd_D).Rows)
            {
                var data = new HAT_F_api.Models.ConstructionDetail();
                data.ConstructionCode = constructionCode;
                data.Koban = Convert.ToInt32(dr["子番"]);
                grd_D[data.Koban, "ステータス"] = "計上済";
                updatelist.Add(data);
            }

            // チェックボックスの設定とステータス列の色設定を呼び出す
            CreateCheckBox(grd_D, grd_D.Rows.Count - 1);
            SetStatusColumnColors(grd_D);

            // API呼び出しによる更新
            var update = await ApiHelper.UpdateAsync(this, () =>
                Program.HatFApiClient.PutAsync<int>(ApiResources.HatF.Client.UpdateConstructionDetailGridKoban, updatelist));
        }

        public List<FosJyuchuPage> getFosJyuchPages()
        {
            DataTable dt = GetCheckedData(grd_D);

            // チェックされたデータが0件の場合
            if (dt.Rows.Count == 0)
            {
                DialogHelper.WarningMessage(this, "選択された行がありません。");
                return new List<FosJyuchuPage>(); // 空のリストを返して処理を中断
            }

            var grouped = from row in dt.AsEnumerable()
                          group row by new 
                          { nouki = row.Field<string>("納期"),
                              shiresaki = row.Field<string>("仕入先コード")} into grp
                          select grp;

            List<List<DataRow>> pageList = new List<List<DataRow>>();
            foreach (var group in grouped)
            {
                List<DataRow> currentSubGroup = new List<DataRow>();
                int count = 0;

                foreach (var row in group)
                {
                    currentSubGroup.Add(row);
                    count++;

                    // 6つの要素を満たしたかチェックし、サブグループに追加します
                    if (count == 6)
                    {
                        pageList.Add(currentSubGroup.ToList()); // リストのコピーを追加する
                        currentSubGroup.Clear(); // サブグループをクリアして次のグループへ
                        count = 0; // カウントをリセット
                    }
                }
                // 6つ未満の残りの要素がある場合は、最後のサブグループとして追加します
                if (currentSubGroup.Any())
                {
                    pageList.Add(currentSubGroup.ToList());
                }
            }

            var isNew = true;
            if (isNew)
            {
                //var jyu2Cd = loginRepo.CurrentUser.EmployeeCode;
                //var jyu2 = loginRepo.CurrentUser.EmployeeTag;
                //SetNewSaveKey(FosJyuchuRepo.GetInstance().CreateNewSaveKey(jyu2Cd, jyu2));
            }

            List<FosJyuchuPage> pages = new List<FosJyuchuPage>();
            int DenSort = 1;
            foreach (var subGroup in pageList)
            {
                FosJyuchuPage page = new FosJyuchuPage(false)
                {
                    FosJyuchuH = new FosJyuchuH(),
                    FosJyuchuDs = new List<FosJyuchuD>(),
                };
                page.FosJyuchuH.OrderState = "1"; //固定
                page.FosJyuchuH.DenSort = DenSort.ToString();
                page.FosJyuchuH.DenState = "5"; //固定
                page.FosJyuchuH.DenNo = "00000" + DenSort; //一意の６桁番号　TODO 変更予定
                page.FosJyuchuH.ConstructionCode = constructionCode; //物件コード
                page.FosJyuchuH.DenFlg = "11"; //TODO 後で変更

                page.FosJyuchuH.Jyu2 = txtMANAGER_ID.Text;
                page.FosJyuchuH.Nyu2 = txtMANAGER_ID.Text;
                page.FosJyuchuH.TeamCd = txtTEAM_CD.Text;

                //GRIDからFosJyuchuHに入れる
                DataRow hdr = subGroup[0];
                page.FosJyuchuH.ShiresakiCd = hdr["仕入先コード"].ToString();
                page.FosJyuchuH.ShiresakiName = hdr["仕入先名"].ToString();
                page.FosJyuchuH.Nouki = HatFComParts.DoParseDateTime(hdr["納期"]);

                int DenNoCount = 1;
                foreach (var dr in subGroup)
                {
                    FosJyuchuD fosJyuchuD = new FosJyuchuD();
                    fosJyuchuD.DenSort = DenSort.ToString();
                    fosJyuchuD.DenNoLine = DenNoCount.ToString();
                    fosJyuchuD.SyohinCd = dr["商品名"].ToString();
                    fosJyuchuD.Suryo = HatFComParts.DoParseInt(dr["数量"]);
                    fosJyuchuD.Tani = dr["単位"].ToString();
                    fosJyuchuD.Bara = HatFComParts.DoParseInt(dr["バラ数"]);
                    fosJyuchuD.TeiTan = HatFComParts.DoParseDecimal(dr["定価単価"]);
                    fosJyuchuD.UriKake = HatFComParts.DoParseDecimal(dr["売上掛率"]);
                    fosJyuchuD.UriTan = HatFComParts.DoParseDecimal(dr["売上単価"]);
                    fosJyuchuD.SiiKake = HatFComParts.DoParseDecimal(dr["仕入掛率"]);
                    fosJyuchuD.SiiTan = HatFComParts.DoParseDecimal(dr["仕入単価"]);
                    fosJyuchuD.OrderState = "1";
                    page.FosJyuchuDs.Add(fosJyuchuD);
                    DenNoCount++;
                }
                pages.Add(page);
                DenSort++;
            }
            return pages;
        }

        /// <summary>確定させるためにページ情報を変更する</summary>
        /// <param name="pages">ページ情報</param>
        /// <returns>ページ情報</returns>
        public async Task<ApiResponse<List<FosJyuchuPage>>> CommitPagesAsync(List<FosJyuchuPage> pages)
        {
            FosJyuchuPages fosJyuchuPages = new FosJyuchuPages
            {
                TargetPage = pages.Count -1,
                Pages = pages,
            };

            return await fosJyuchuRepo.putOrderCommit(fosJyuchuPages);
        }

        /// <summary>
        /// gridからチェック済みのデータを取得する
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private DataTable GetCheckedData(C1FlexGrid grid)
        {
            DataTable dt = new DataTable();
            for (int col = 1; col < grid.Cols.Count; col++)
            {
                dt.Columns.Add(grid.Cols[col].Caption);
            }
            for (int row = 1; row < grid.Rows.Count; row++)
            {
                if (grid.GetCellCheck(row, 1) == CheckEnum.Checked)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col < grid.Cols.Count; col++)
                    {
                        dr[grid.Cols[col].Caption] = grid[row, col];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
    public class ConstructionAppSheetData
    {
        public string Property { get; set; }
    }
}

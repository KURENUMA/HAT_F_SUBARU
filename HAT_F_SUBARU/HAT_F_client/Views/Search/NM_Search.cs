using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Search
{
    public partial class NM_Search : Form
    {
        List<string> ListLayOut = new List<string>();   //リストレイアウト
        public DataTable DtLayOut = new DataTable();    //データテーブルレイアウト
        DataTable DtList = new DataTable();             //FlexGrid用データテーブル

        const int IntShowMaxConunt = 200;               // 表示最大件数
        const string StrNote = @"※入荷日が当月末以前のデータのみ抽出しています。";

        public NM_Search()
        {
            InitializeComponent();
        }

        private void M_Search_Load(object sender, EventArgs e)
        {

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0); this.KeyPreview = true;

            ClearForm();
            SetLayOutData();            // FelxGrid 構成情報セット
            InitDrLayOutForGridCols();  // FlexGrid 表示項目情報セット
            SetGridDataName();          // FlexGrid DataTable 紐づけ
            SetGridCols();              // FlexGrid グリッド項目セット

            this.lblMaxCount.Text = @"最大 " + IntShowMaxConunt.ToString() + @" 件表示";    // 表示最大件数
            this.lblNote.Text = StrNote;

        }

        #region << ボタンイベント >>
        private void BtnFnc09_Click(object sender, System.EventArgs e)
        {
            this.btnFnc09.Focus();
            if (MessageBox.Show("検索しますか？", "検索", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DtList.Clear();
            testData();
        }
        private void BtnFnc10_Click(object sender, System.EventArgs e)
        {
            this.btnFnc10.Focus();
            if (MessageBox.Show("条件をクリアしますか？", "クリア", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            ClearForm();
            DtList.Clear();
            DataView dv = new DataView(DtList);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);
            txtTEAM_CD.Focus();
        }
        private void BtnFnc08_Click(object sender, System.EventArgs e)
        {
            this.btnFnc08.Focus();
            MessageBox.Show(this.btnFnc08.Text);
        }
        private void BtnFnc11_Click(object sender, System.EventArgs e)
        {
            this.btnFnc11.Focus();
            MessageBox.Show(this.btnFnc11.Text);
        }
        private void BtnFnc12_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("本画面を閉じますか？", "閉じる", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.Close();
        }
        #endregion

        #region << ショートカット制御 >>
        private void M_Search_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    if (btnFnc08.Enabled == true)
                        btnFnc08.PerformClick();
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
        #endregion

        private void ClearForm()
        {
            this.txtTEAM_CD.Clear();
            this.txtTOKUI_CD.Clear();
            this.txtSHIRESAKI_CD.Clear();
            this.txtNYU2_CD.Clear();
            this.txtJYU2_CD.Clear();
            this.txtTANTO_CD.Clear();
            this.txtDEN_NO.Clear();
            this.txtHAT_ORDER_NO.Clear();
            this.txtCUST_ORDERNO.Clear();
            this.dateHAT_NYUKABI.Clear();
        }

        private void SetLayOutData()
        {
            // ID,Caption,DataName,Width,Format,TextAlign,Flg,Sort
            ListLayOut.Add("1,チーム\r\nＣＤ,TeamCd,70,,L,1,1");
            ListLayOut.Add("2,得意先\r\nＣＤ,TokuiCd,70,,M,1,1");
            ListLayOut.Add("3,仕入先\r\nＣＤ,ShiresakiCd,70,,M,1,1");
            ListLayOut.Add("4,入力者,Nyu2,120,,L,1,1");
            ListLayOut.Add("5,受注者,Jyu2,120,,L,1,1");
            ListLayOut.Add("6,担当者,Tanto,120,,L,1,1");
            ListLayOut.Add("7,伝票No,DenNo,70,,M,1,1");
            ListLayOut.Add("8,受注番号,OrderNo,70,,M,1,1");
            ListLayOut.Add("10,HAT注番,HatOrderNo,80,,M,1,1");
            ListLayOut.Add("11,客先注番,CustOrderNo,260,,L,1,1");
            ListLayOut.Add("12,入荷日,HatNyukabi,90,d,M,1,1");
        }
        private void InitDrLayOutForGridCols()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error " + MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetGridDataName()
        {
            try
            {
                DataView dv = new DataView(DtLayOut);
                dv.Sort = "SortNull,Sort";
                DataTable result = dv.ToTable();
                for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
                {
                    DtList.Columns.Add(result.Rows[rowIndex]["DataName"].ToString(), typeof(string));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error " + MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetGridCols()
        {
            try
            {
                DataView dv = new DataView(DtLayOut);
                dv.Sort = "SortNull,Sort";
                DataTable result = dv.ToTable();

                grdList.Cols.Fixed = 0;
                grdList.Cols.Count = result.Rows.Count;
                grdList.Rows[0].Height = 50;
                grdList.AllowFiltering = true;      //フィルタ設定
                grdList.AllowEditing = false;        //編集不可

                for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
                {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error " + MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void testData()
        {

            DataRow dr;

            for (int i = 0; i < 50; i++)
            {
                dr = DtList.NewRow();
                dr["TeamCd"] = "X2";
                dr["TokuiCd"] = "123456";
                dr["ShiresakiCd"] = "123456";
                dr["Nyu2"] = "ＮＮＮ　ＮＮＮ";
                dr["Jyu2"] = "ＮＮＮ　ＮＮＮ";
                dr["Tanto"] = "ＮＮＮ　ＮＮＮ";
                dr["DenNo"] = "123456";
                dr["OrderNo"] = "123456";
                dr["HatOrderNo"] = "1234567";
                dr["CustOrderno"] = "26004486-Dｺﾞｳﾄﾞｳﾁｮｳｼ";
                dr["HatNyukabi"] = "24/03/31";
                DtList.Rows.Add(dr);
            }

            DataView dv = new DataView(DtList);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);
        }
    }
}

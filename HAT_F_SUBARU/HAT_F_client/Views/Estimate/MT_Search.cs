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

namespace HatFClient.Views.Estimate
{
    public partial class MT_Search : Form
    {
        List<string> ListLayOut = new List<string>();   //リストレイアウト
        public DataTable DtLayOut = new DataTable();    //データテーブルレイアウト
        DataTable DtList = new DataTable();             //FlexGrid用データテーブル

        const int IntShowMaxConunt = 200;               // 表示最大件数

        public MT_Search()
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
            this.txtBUKKEN.Clear();
            this.txtESTIMATE_NO.Clear();
            this.dateINP_DATEFrom.Clear();
            this.dateINP_DATETo.Clear();
            this.dateNOUKIFrom.Clear();
            this.dateNOUKITo.Clear();
        }

        private void SetLayOutData()
        {
            // ID,Caption,DataName,Width,Format,TextAlign,Flg,Sort
            ListLayOut.Add("1,見積日,InpDate,90,d,M,1,1");
            ListLayOut.Add("2,見積番号,EstimateNo,70,,L,1,1");
            ListLayOut.Add("3,得意先CD,TokuiCd,70,,M,1,1");
            ListLayOut.Add("4,得意先名,Tokui,300,,L,1,1");
            ListLayOut.Add("5,物件名,Bukken,300,,L,1,1");
            ListLayOut.Add("6,納期,Nouki,90,d,M,1,1");
            ListLayOut.Add("7,担当者,Tanto,100,,L,1,1");
            ListLayOut.Add("8,受注区分,OrderFlg,120,,L,1,1");
            ListLayOut.Add("9,見積子番,Koban,80,,M,1,1");
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
                dr["InpDate"] = "23/01/01";
                dr["EstimateNo"] = "123456";
                dr["TokuiCd"] = "123456";
                dr["Tokui"] = "ＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮ";
                dr["Nouki"] = "24/03/31";
                dr["Tanto"] = "ＮＮＮ　ＮＮＮ";
                dr["OrderFlg"] = "通常受注";
                dr["Koban"] = "99";
                DtList.Rows.Add(dr);
            }

            DataView dv = new DataView(DtList);
            DataTable result = dv.ToTable();
            grdList.SetDataBinding(result, "", true);
        }
    }
}

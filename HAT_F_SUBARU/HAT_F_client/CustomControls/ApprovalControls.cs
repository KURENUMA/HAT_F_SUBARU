using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    public partial class ApprovalControls : UserControl
    {
        public event System.EventHandler Cancel;

        public ApprovalControls()
        {
            InitializeComponent();
        }

        private void ApprovalControls_Load(object sender, EventArgs e)
        {
            c1FlexGrid1.Clear();
            c1FlexGrid1.Rows.Count = 1;
            c1FlexGrid1.Cols.Count = 5;
            c1FlexGrid1.Cols[0].Caption = "";
            c1FlexGrid1.Cols[0].Width = 30;

            c1FlexGrid1.Cols[1].Caption = "申請者/承認者";
            c1FlexGrid1.Cols[1].Width = 100;

            c1FlexGrid1.Cols[2].Caption = "状態";
            c1FlexGrid1.Cols[2].Width = 100;

            c1FlexGrid1.Cols[3].Caption = "コメント";
            c1FlexGrid1.Cols[3].Width = 150;

            c1FlexGrid1.Cols[4].Caption = "登録日時";
            c1FlexGrid1.Cols[4].Width = 150;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel.Invoke(this, e);
        }
    }
}

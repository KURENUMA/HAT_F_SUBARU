using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HatFClient.Common;
using HAT_F_api.CustomModels;
using HatFClient.Constants;

namespace HatFClient.Views.MasterEdit
{
    public partial class ME_Menu : Form
    {
        private List<MasterTable> _masterTables;

        public ME_Menu()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
            }
        }

        private async void ME_Menu_Load(object sender, EventArgs e)
        {
            var apiResult = await ApiHelper.FetchAsync<List<MasterTable>>(this, async () => {
                var response = await Program.HatFApiClient.GetAsync<List<MasterTable>>(ApiResources.HatF.MasterEditor.MasterTableList);
                return response;
            });

            if (apiResult.Failed)
            {
                this.Close();
                return;
            }

            _masterTables = apiResult.Value;

            //this.listBox1.DataSource = MasterEditRepo.GetInstance().Entities;
            this.listBox1.DataSource = _masterTables;
            this.listBox1.DisplayMember = "LogicalName";
            this.listBox1.ValueMember = "Name";

            if (0 < listBox1.Items.Count)
            {
                this.listBox1.SelectedIndex = 0;
            }
        }


        private void openEditor() 
        {
            using (var editor = new ME_Editor())
            {
                //var master = MasterEditRepo.GetInstance().Entities.Find(m => m.Name == (string)this.listBox1.SelectedValue);
                var master = _masterTables.Find(m => m.Name == (string)this.listBox1.SelectedValue);
                editor.MasterEditEntity = master;
                //editor.Parent = this.Parent;
                editor.ShowDialog();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e) 
        {
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openEditor();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openEditor();
        }

        private void ME_Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                btnSelect.PerformClick();
            }
            else if (e.KeyCode == Keys.F12)
            {
                btnClose.PerformClick();
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            using (var form = new ME_Employee()) 
            { 
                form.ShowDialog();
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            using (var form = new ME_Supplier())
            {
                form.ShowDialog();
            }
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CompanysMst())
            {
                form.ShowDialog();
            }
        }

        private void btnCustomerMst_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CustomersMst())
            {
                form.ShowDialog();
            }
        }

        private void btnDestinationsMst_Click(object sender, EventArgs e)
        {
            using (var form = new ME_DestinaitonsMst())
            {
                form.ShowDialog();
            }
        }

        private void btnKeyman_Click(object sender, EventArgs e)
        {
            using (var form = new ME_CustomersUserMst())
            {
                form.ShowDialog();
            }
        }
    }
}

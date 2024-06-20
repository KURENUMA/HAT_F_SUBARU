using HAT_F_api.Models;
using HatFClient.Common;
using HatFClient.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.MasterSearch
{
    public partial class MS_Employee : Form
    {
        public Employee Employee { get; private set; }

        private const int SearchMaxRows = 200;

        public MS_Employee()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetResizableDialogStyle(this);
            }
        }

        private void MS_Employee_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            lblNote.Visible = false;
            lblMaxCount.Text = string.Format(lblMaxCount.Text, SearchMaxRows);

            c1gEmployee.AutoGenerateColumns = false;
            c1gEmployee.DataSource = new List<Employee>();
        }

        private void MS_Employee_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    if (btnSearch.Enabled)
                    {
                        btnSearch.PerformClick();
                    }
                    break;
                case Keys.F11:
                    if (btnOK.Enabled)
                    {
                        btnOK.PerformClick();
                    }
                    break;
                case Keys.F12:
                    if (btnCancel.Enabled)
                    {
                        btnCancel.PerformClick();
                    }
                    break;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string empCode = txtEmpCode.Text;
            string empName = txtEmpName.Text;
            string empKana = txtEmpNameKana.Text;

            // 社員
            var apiResult = await ApiHelper.FetchAsync<List<Employee>>(this, async () => {
                string url = ApiResources.HatF.MasterEditor.Emloyee;
                var conditions = new { empCode = empCode, empName = empName, empKana = empKana, includeDeleted = true, rows = SearchMaxRows };

                var apiResponse = await Program.HatFApiClient.GetAsync<List<Employee>>(
                    url,   // 一覧取得API
                    conditions);   // 検索条件

                return apiResponse;
            });

            if (apiResult.Failed)
            {
                return;
            }

            var employees = apiResult.Value;

            c1gEmployee.AutoGenerateColumns = false;
            c1gEmployee.DataSource = employees;

            if (employees.Count > 0) 
            {
                btnOK.Enabled = true;
                lblNote.Visible = false;
            }
            else
            {
                btnOK.Enabled = (employees.Count > 0);
                lblNote.Visible = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Employee = (Employee)c1gEmployee.Rows[c1gEmployee.Row].DataSource;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void c1gEmployee_DoubleClick(object sender, EventArgs e)
        {
            if (btnOK.Enabled != true)
            {
                return;
            }

            btnOK.PerformClick();
        }
    }
}

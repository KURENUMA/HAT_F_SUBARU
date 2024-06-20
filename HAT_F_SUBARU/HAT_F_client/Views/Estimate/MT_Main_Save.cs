using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.Views.Estimate
{
    public partial class MT_Main_Save : Form
    {
        public MT_Main_Save()
        {
            InitializeComponent();
        }
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

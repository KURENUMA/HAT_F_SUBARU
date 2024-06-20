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
    public partial class CheckBoxEx : CheckBox
    {
        public CheckBoxEx()
        {
            SetCheckBoxEx();
        }
        private void SetCheckBoxEx()
        {
            this.Enter += new EventHandler(CheckBoxEx_Enter);
            this.Leave += new EventHandler(CheckBoxEx_Leave);
        }
        private void CheckBoxEx_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
        }
        private void CheckBoxEx_Leave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.AppWorkspace;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAT_F_client.Views.BlobStorage
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }
        public void SetProgress(string msg, int progress)
        {
            this.labelMessage.Text = msg;
            this.progressBar1.Value = progress;
        }
    }

}

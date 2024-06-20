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
    public partial class TextBoxChar : TextBox
    {
        public TextBoxChar()
        {
            SetTextBoxChar();
        }

        private void SetTextBoxChar()
        {
            this.Enter += new EventHandler(TextBoxChar_Enter);
            this.Leave += new EventHandler(TextBoxChar_Leave);
        }
        private void TextBoxChar_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.ForeColor = SystemColors.HotTrack;
            this.SelectAll();
        }
        private void TextBoxChar_Leave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Window;
            this.Font = new Font(this.Font, FontStyle.Regular);
            this.ForeColor = SystemColors.WindowText;
        }
    }
}

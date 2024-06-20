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
    public partial class TextBoxCharSize1 : TextBox
    {
        public TextBoxCharSize1()
        {
            SetTextBoxCharSize1();
        }
        private void SetTextBoxCharSize1()
        {
            this.Enter += new EventHandler(TextBoxCharSize1_Enter);
            this.Leave += new EventHandler(TextBoxCharSize1_Leave);
            this.KeyPress += new KeyPressEventHandler(TextBoxCharSize1_KeyPress);
            this.TextAlign = HorizontalAlignment.Center;
            this.MaxLength = 1;
            this.ImeMode = ImeMode.Disable;
            this.CharacterCasing = CharacterCasing.Upper;
        }
        private void TextBoxCharSize1_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.ForeColor = SystemColors.HotTrack;
            this.SelectAll();
        }
        private void TextBoxCharSize1_Leave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Window;
            this.Font = new Font(this.Font, FontStyle.Regular);
            this.ForeColor = SystemColors.WindowText;
        }
        private void TextBoxCharSize1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && (e.KeyChar < 'a' || 'z' < e.KeyChar) && (e.KeyChar < 'A' || 'Z' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}

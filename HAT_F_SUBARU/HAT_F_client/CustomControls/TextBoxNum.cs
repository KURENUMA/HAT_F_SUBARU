using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    public partial class TextBoxNum : TextBox
    {
        public TextBoxNum()
        {
            SetTextBoxNum();
        }
        private void SetTextBoxNum()
        {
            this.TextAlign = HorizontalAlignment.Right;
            this.ImeMode = ImeMode.Off;
            this.Enter += new EventHandler(TextBoxNum_Enter);
            this.Leave += new EventHandler(TextBoxNum_Leave);
            this.KeyPress += new KeyPressEventHandler(TextBoxNum_KeyPress);
        }
        private void TextBoxNum_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.ForeColor = SystemColors.HotTrack;
            this.Text = Regex.Replace(this.Text, @"[^0-9\-]", "");
            this.SelectAll();
        }
        private void TextBoxNum_Leave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Window;
            this.Font = new Font(this.Font, FontStyle.Regular);
            this.ForeColor = SystemColors.WindowText;
            if (long.TryParse(this.Text, out long n))
            {
                this.Text = string.Format("{0:#,0}", long.Parse(this.Text));
            }
        }
        private void TextBoxNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                if (this.Text.IndexOf("-") >= 0)
                {
                    e.Handled = true;
                }
            }
            else if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                //押されたキーが 0～9,-,BackSpacesでない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }
    }
}

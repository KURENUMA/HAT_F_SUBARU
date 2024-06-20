using HatFClient.Common;
using HatFClient.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.CustomControls
{
    public partial class ComboBoxPayDate : ComboBox
    {
        /// <summary>
        /// 選択された日付
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? Value
        {
            get => (SelectedItem as CodeName<DateTime?>)?.Code;
            set
            {
                var month = value.HasValue ? new DateTime(value.Value.Year, value.Value.Month, 1) : (DateTime?)null;
                var index = Items.OfType<CodeName<DateTime?>>().ToList().FindIndex(x => x.Code == month);
                if(index >= 0)
                {
                    SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// 選択された月初日付
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? FirstOfMonthValue => Value != null ? DateTimeUtil.GetFirstOfMonth(Value.Value) : null;

        /// <summary>
        /// 選択された月末日付
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? EndOfMonthValue
        {
            get
            {
                if (Value == null)
                {
                    return null;
                }
                var endDate = DateTimeUtil.GetEndOfMonth(Value.Value);
                return new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            }
        }

        public ComboBoxPayDate()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                SetComboBoxPayDate();
            }
        }

        public ComboBoxPayDate(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            if (!this.DesignMode)
            {
                SetComboBoxPayDate();
            }
        }

        private void SetComboBoxPayDate()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ImeMode = ImeMode.Off;
        }

        /// <summary>コンボボックスの選択肢を登録する</summary>
        public void InitializeItems()
        {
            this.DisplayMember = nameof(CodeName<DateTime?>.Name);
            var baseDate = new DateTime(2023, 1, 1);
            var now = DateTimeUtil.GetFirstOfMonth();
            var d = now.AddMonths(3);
            this.Items.Add(new CodeName<DateTime?>()
            {
                Code = null,
                Name = string.Empty
            });
            while (d.CompareTo(baseDate) >= 0)
            {
                var item = new CodeName<DateTime?>
                {
                    Code = d,
                    Name = d.ToString("yy/MM")
                };
                this.Items.Add(item);
                d = d.AddMonths(-1);
            }
        }
    }
}

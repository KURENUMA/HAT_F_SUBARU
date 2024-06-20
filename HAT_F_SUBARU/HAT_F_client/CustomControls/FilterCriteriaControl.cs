using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using HatFClient.Common;
using HatFClient.Extensions;
using HatFClient.ViewModels;
using HatFClient.Views.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HatFClient.CustomControls
{
    public partial class FilterCriteriaControl : UserControl
    {
        public event System.EventHandler RemoveButtonClick;
        private FilterCriteria _criteria;
        public FilterCriteria Criteria => _criteria;

        private List<SearchDropDownInfo> _searchDropDownInfos;

        /// <summary>
        /// GUIデザイナー用のコンストラクタ
        /// </summary>
        public FilterCriteriaControl()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                System.Diagnostics.Debugger.Break();
                throw new InvalidOperationException("引数付きコンストラクタを使用してください。");
            }
        }

        //public FilterCriteriaControl(bool isFirst = false, FilterCriteria criteria = null)
        public FilterCriteriaControl(bool isFirst, FilterCriteria criteria, List<SearchDropDownInfo> searchDropDownInfos)
        {
            InitializeComponent();

            _criteria = criteria;
            _searchDropDownInfos = searchDropDownInfos;
            
            if (!this.DesignMode)
            {
                if (searchDropDownInfos != null)
                {
                    foreach (SearchDropDownInfo dropDownInfo in searchDropDownInfos)
                    {
                        var item = criteria.Configs.Where(x => x.FieldName == dropDownInfo.FieldName).FirstOrDefault();
                        if (item != null)
                        {
                            item.IsDropDownItem = true;
                        }
                    }
                }

                InitColumnItems();

                label1.Visible = isFirst;
                combinationTypecomboBox.Visible = !isFirst;
                button1.Visible = !isFirst;

                InitState();

                _criteria.OnColumnSelected += setInputType;
                _criteria.OnColumnSelected += setStatus;
                _criteria.OnOperatorsChange += setOperators;
                _criteria.OnOperatorSelected += setInputType;
                _criteria.OnOperatorSelected += setOperator;

                columnComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnSelectedIndexChanged);
                operatorComboBox.SelectedIndexChanged += new System.EventHandler(this.operatorComboBox_SelectedIndexChanged);
            }
        }

        private void InitState()
        {
            if (_criteria != null)
            {
                numericUpDown1.Value = _criteria.NumValue;
                textBox1.Text = _criteria.TextValue;

                //cmbDropDown.Text = _criteria.TextValue;
                InitialDropDownItems();
                for (int i = 0; i < cmbDropDown.Items.Count; i++)
                {
                    var kv = (KeyValuePair<string, string>)cmbDropDown.Items[i];
                    if (kv.Value == _criteria.TextValue)
                    {
                        cmbDropDown.SelectedIndex = i;
                        break;
                    }
                }

                dateTimePicker1.Value = _criteria.DateRangeValue.Item1;
                dateTimePicker2.Value = _criteria.DateRangeValue.Item2;
                dateTimePicker3.Value = _criteria.DateValue;

                dateTimePicker1.Value = dateTimePicker1.Value.Date;
                dateTimePicker2.Value = dateTimePicker2.Value.Date;
                dateTimePicker3.Value = dateTimePicker3.Value.Date;
                dateTimePicker1.CustomFormat = FormTextFormatting.DefaultDateFormat;
                dateTimePicker2.CustomFormat = FormTextFormatting.DefaultDateFormat;
                dateTimePicker3.CustomFormat = FormTextFormatting.DefaultDateFormat;

                columnComboBox.SelectedIndex = _criteria.ColumnIndex;
                operatorComboBox.SelectedIndex = _criteria.OperatorIndex;
                combinationTypecomboBox.SelectedIndex = (int)_criteria.CombinationType;
                setInputType(this, EventArgs.Empty);
            }
        }

        private void InitialDropDownItems()
        {
            cmbDropDown.Items.Clear();

            string name = _criteria.SelectedColumn.FieldName;
            var dropDownInfo = _searchDropDownInfos?.Where(x => x.FieldName == name).SingleOrDefault();
            if (dropDownInfo != null)
            {
                cmbDropDown.DisplayMember = "Key";
                cmbDropDown.ValueMember = "Value";

                cmbDropDown.SuspendLayout();
                foreach (var item in dropDownInfo.DropDownItems)
                {
                    cmbDropDown.Items.Add(item);
                }
                cmbDropDown.ResumeLayout();
            }
        }

        private void setStatus(object sender, EventArgs e)
        {
            if (_criteria.SelectedColumn.IsDropDownItem)
            {
                InitialDropDownItems();
            }
        }

        private void setOperator(object sender, EventArgs e)
        {
            operatorComboBox.SelectedIndex = _criteria.OperatorIndex;
        }

        private void setOperators(object sender, EventArgs e)
        {
            operatorComboBox.Items.Clear();
            operatorComboBox.Items.AddRange(_criteria.Filters.Select(f => f.Value).ToArray());
        }

        private void setInputType(object sender, EventArgs e)
        {
            if (_criteria.SelectedColumn.IsDropDownItem)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                dateTimePicker3.Visible = false;
                textBox1.Visible = false;
                cmbDropDown.Visible = true;
                numericUpDown1.Visible = false;
                return;
            }

            if (_criteria.SelectedColumn.DataType.IsNumeric())
            {
                if (_criteria.SelectedColumn.FieldName == "status_class")
                {
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dateTimePicker3.Visible = false;
                    textBox1.Visible = false;
                    cmbDropDown.Visible = true;
                    numericUpDown1.Visible = false;
                }
                else
                {
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dateTimePicker3.Visible = false;
                    textBox1.Visible = false;
                    cmbDropDown.Visible = false;
                    numericUpDown1.Visible = true;
                }
            }

            if (_criteria.SelectedColumn.DataType.IsDate())
            {
                if (_criteria.Operator == FilterOperators.Between)
                {
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    dateTimePicker3.Visible = false;
                }
                else
                {
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    dateTimePicker3.Visible = true;
                }
                textBox1.Visible = false;
                cmbDropDown.Visible = false;
                numericUpDown1.Visible = false;
            }

            if (_criteria.SelectedColumn.DataType == typeof(string))
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                dateTimePicker3.Visible = false;
                textBox1.Visible = true;
                cmbDropDown.Visible = false;
                numericUpDown1.Visible = false;
            }
        }

        private void InitColumnItems()
        {
            string[] items = _criteria.Configs.Select(c =>
                            c.Caption
                        ).ToArray();
            operatorComboBox.Items.AddRange(_criteria.Filters.Select(f => f.Value).ToArray());
            columnComboBox.Items.AddRange(items);
        }

        private void ColumnSelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected (SelectedIndex is not -1)
            if (columnComboBox.SelectedIndex != -1)
            {
                if (_criteria.SelectedColumn.IsDropDownItem)
                {
                    // ドロップダウンから別種に変更されようとしている
                    // 区分値が表示されても変なので消しておく
                    numericUpDown1.Value = 0;
                    textBox1.Text = "";
                }

                // Get the selected item
                _criteria.SelectColumn(columnComboBox.SelectedIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateRangeValue = (dateTimePicker1.Value, _criteria.DateRangeValue.Item2);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateRangeValue = (_criteria.DateRangeValue.Item1, dateTimePicker2.Value);
        }

        private void operatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria.SelectOperator(operatorComboBox.SelectedIndex);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _criteria.NumValue = numericUpDown1.Value;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _criteria.TextValue = textBox1.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _criteria.CombinationType = combinationTypecomboBox.SelectedItem.ToString() == "AND" ? FilterCombinationTypes.AND : FilterCombinationTypes.OR;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            _criteria.DateValue = dateTimePicker3.Value;
        }

        private void cmbDropDown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbDropDown.SelectedIndex < 0)
            {
                _criteria.TextValue = "";
                _criteria.NumValue = 0;
                return;
            }


            var item = _searchDropDownInfos.Where(x => x.FieldName == _criteria.SelectedColumn.FieldName).Single();
            var selected = (KeyValuePair<string, string>)cmbDropDown.Items[cmbDropDown.SelectedIndex];
            _criteria.TextValue = selected.Value;

            if (decimal.TryParse(selected.Value, out var value))
            {
                _criteria.NumValue= value;
            }
        }
    }

    public class DropDownStatus
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DropDownStatus(byte id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}

using HatFClient.Shared;
using System;
using System.Collections.Generic;
using Dma.DatasourceLoader.Creator;
using Dma.DatasourceLoader.Models;
using HatFClient.Extensions;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;

namespace HatFClient.ViewModels
{
    public class FilterCriteria
    {
        private List<ColumnMappingConfig> _configs;
        public List<ColumnMappingConfig> Configs => _configs;
        private ColumnMappingConfig _selectedColumn;
        private List<FilterOperators> _operators = new List<FilterOperators>();
        private FilterOperators _operator;
        public FilterOperators Operator => _operator;
        public int OperatorIndex => _operators.Count > 0 ? _operators.IndexOf(_operator) : -1;
        public FilterCombinationTypes CombinationType { get; set; } = FilterCombinationTypes.AND;
        public decimal NumValue { get; set; } = 0;
        public (DateTime, DateTime) DateRangeValue { get; set; } = (DateTime.Now, DateTime.Now);
        public DateTime DateValue { get; set; } = DateTime.Now;
        public string TextValue { get; set; } = "";
        public bool Visible { get; private set; } = true;

        public event EventHandler OnColumnSelected;
        public event EventHandler OnOperatorSelected;
        public event EventHandler OnOperatorsChange;
        public ColumnMappingConfig SelectedColumn => _selectedColumn;
        public int ColumnIndex => _configs.IndexOf(_selectedColumn) == -1 ? 0 : _configs.IndexOf(_selectedColumn);

        public FilterCriteria(List<ColumnMappingConfig> configs, bool visible)
            : this(configs)
        {
            Visible = visible;
        }

        public FilterCriteria(List<ColumnMappingConfig> configs)
        {
            _configs = configs;
            SelectColumn(0);
        }

        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, decimal value, bool visible)
            : this(configs, configIndex, filterOperator, value)
        {
            Visible = visible;
        }
        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, decimal value)
        {
            _configs = configs;
            SelectColumn(configIndex);
            _operator = filterOperator;
            NumValue = value;
        }

        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, string value, bool visible)
            : this(configs, configIndex, filterOperator, value)
        {
            Visible = visible;
        }
        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, string value)
        {
            _configs = configs;
            SelectColumn(configIndex);
            _operator = filterOperator;
            TextValue = value;
        }

        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, DateTime value,bool visible)
            :this(configs, configIndex, filterOperator, value)
        {
            Visible = visible;
        }
        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, FilterOperators filterOperator, DateTime value)
        {
            if (filterOperator.Equals(FilterOperators.Between))
            {
                throw new InvalidOperationException();
            }
            _configs = configs;
            SelectColumn(configIndex);
            _operator = filterOperator;
            DateValue = value;
        }

        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, DateTime valueFrom, DateTime valueTo, bool visible)
            : this(configs, configIndex, valueFrom, valueTo)
        {
            Visible = visible;
        }
        public FilterCriteria(List<ColumnMappingConfig> configs, int configIndex, DateTime valueFrom, DateTime valueTo)
        {
            _configs = configs;
            SelectColumn(configIndex);
            _operator = FilterOperators.Between;
            DateRangeValue = (valueFrom, valueTo);
        }

        public List<FilterOperators> Filters { get => _operators; }

        public void SelectOperator(int ind)
        {
            if (ind < 0) return;
            if (ind >= _operators.Count) return;

            _operator = _operators[ind];
            OnOperatorSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SelectColumn(int index)
        {
            var config = _configs[index];
            SelectColumn(config);

        }

        public void SelectColumn(ColumnMappingConfig config)
        {
            _selectedColumn = config;

            if (config.IsDropDownItem)
            {
                _operators = FilterOperators.GetDivFilters();
            }
            else
            {
                if (config.DataType == typeof(string))
                {
                    _operators = FilterOperators.GetStringFilters();
                }

                if (config.DataType.IsNumeric())
                {
                    _operators = FilterOperators.GetNumericFilters();
                }

                if (config.DataType == typeof(DateTime))
                {
                    _operators = FilterOperators.GetDateFilters();
                }
            }

            OnColumnSelected?.Invoke(this, EventArgs.Empty);
            OnOperatorsChange?.Invoke(this, EventArgs.Empty);
            SelectOperator(0);
        }

        // Helper method to raise the ColumnSelected event


        private object GetValue()
        {
            if (_selectedColumn.DataType == typeof(string))
                return TextValue;
            if (_selectedColumn.DataType == typeof(DateTime))
            {
                if (Operator == FilterOperators.Between) return DateRangeValue;
                return DateValue;
            }
            return _selectedColumn.DataType.CastNumber(NumValue);
        }

        public (FilterCombinationTypes, FilterOption) AsFilterOption()
        {
            return (CombinationType, new FilterOption(SelectedColumn.FieldName, Operator, GetValue()));
        }
        public (FilterCombinationTypes, FilterOption, string) AsFilterOptionAndCaption()
        {
            return (CombinationType, new FilterOption(SelectedColumn.FieldName, Operator, GetValue()), Configs[ColumnIndex].Caption);
        }
    }
}

using HatFClient.Common;
using HatFClient.CustomControls;
using HatFClient.Shared;
using HatFClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HatFClient.Views.Search
{
    public partial class FrmAdvancedSearch : Form
    {
        private List<FilterCriteriaControl> filterCriteriaList = new List<FilterCriteriaControl>();
        private List<FilterCriteria> initialCriterias;
        //private readonly string selectedPattern;
        private List<ColumnMappingConfig> _criteriaDefinitions;

        public event System.EventHandler<AdvancedSearchEventArgs> OnSearch;
        public event System.EventHandler<AdvancedSearchEventArgs> OnSearchAndSave;
        public event System.EventHandler<AdvancedSearchEventArgs> OnReset;
        public List<FilterCriteria> FilterCriterias => filterCriteriaList
            .Select(f => f.Criteria)
            .Where(c => c.Visible)
            .Where(c => c.SelectedColumn.DataType != typeof(string) || !string.IsNullOrEmpty(c.TextValue))
            .ToList();

        public List<SearchDropDownInfo> DropDownItems { get; private set; } = new List<SearchDropDownInfo>();

        public void AddDropDownItems(List<SearchDropDownInfo> items)
        {
            DropDownItems = items;
        }

        /// <summary>コンストラクタ</summary>
        public FrmAdvancedSearch()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
            }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="criteriaDefinitions">検索条件としての選択肢を定義したオブジェクト</param>
        public FrmAdvancedSearch(List<ColumnMappingConfig> criteriaDefinitions)
            : this()
        {
            this._criteriaDefinitions = criteriaDefinitions;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="criteriaDefinitions">検索条件としての選択肢を定義したオブジェクト</param>
        /// <param name="initialCriterias">初期検索条件</param>
        public FrmAdvancedSearch(List<ColumnMappingConfig> criteriaDefinitions, List<FilterCriteria> initialCriterias)
            : this(criteriaDefinitions)
        {
            this.initialCriterias = initialCriterias;
        }

        [Obsolete("List<ColumnMappingConfig> criteriaDefinitionsを指定するコンストラクタを使用してください")]
        public FrmAdvancedSearch(GridOrderManager gridManager, List<FilterCriteria> initialCriterias)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                FormStyleHelper.SetFixedSizeDialogStyle(this);
                this.initialCriterias = initialCriterias;
            }
        }

        private void InitializeState(List<FilterCriteria> initialCriterias)
        {
            var i = 0;
            foreach (var c in initialCriterias.Where(x => x.Visible))
            {
                var control = new FilterCriteriaControl(isFirst: i == 0, criteria: c, DropDownItems);
                control.Dock = DockStyle.Bottom;
                addFilterCriteria(control);
                i++;
            }
        }

        private void FrmAdvancedSearch_Load(object sender, EventArgs e)
        {
            if (initialCriterias != null)
            {
                InitializeState(initialCriterias);
            }
            if (filterCriteriaList.Count > 0) return;
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(
                isFirst: true, 
                criteria: new FilterCriteria(_criteriaDefinitions.Where(x => x.Visible).ToList()),
                DropDownItems);
            filterCriteria.Dock = DockStyle.Top;
            addFilterCriteria(filterCriteria);

        }

        private void addFilterCriteria(FilterCriteriaControl filterCriteria)
        {
            filterCriteria.RemoveButtonClick += (s, args) =>
            {
                if (filterCriteriaList.Count == 1)
                {
                    return;
                }
                removeFilterCriteria(filterCriteria);
            };
            filterCriteriaList.Add(filterCriteria);
            filterPanel.Controls.Add(filterCriteria);
        }

        private void removeFilterCriteria(FilterCriteriaControl filterCriteria)
        {
            filterCriteriaList.Remove(filterCriteria);
            filterPanel.Controls.Remove(filterCriteria);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Create a new FilterCriteria control
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(false, criteria: new FilterCriteria(_criteriaDefinitions), DropDownItems);


            // Add the new filter criteria to the list and the panel
            filterCriteria.Dock = DockStyle.Bottom;
            addFilterCriteria(filterCriteria);
        }

        private void btnSearchAndSave_Click(object sender, EventArgs e)
        {
            var eventArgs = new AdvancedSearchEventArgs();
            OnSearchAndSave?.Invoke(this, eventArgs);

            if(!eventArgs.Cancel)
            {
                // 画面を閉じます
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var eventArgs = new AdvancedSearchEventArgs();
            OnSearch?.Invoke(this, eventArgs);

            if(!eventArgs.Cancel)
            {
                // 画面を閉じます
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 画面を閉じます
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (var f in filterCriteriaList)
            {
                filterPanel.Controls.Remove(f);
            }
            filterCriteriaList.Clear();
            FilterCriteriaControl filterCriteria = new FilterCriteriaControl(isFirst: true, criteria: new FilterCriteria(_criteriaDefinitions), DropDownItems);
            filterCriteria.Dock = DockStyle.Top;
            addFilterCriteria(filterCriteria);
            OnReset?.Invoke(this, new AdvancedSearchEventArgs());
        }
    }
}

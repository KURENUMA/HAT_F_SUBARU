using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HatFClient.ViewModels
{
    public interface IGridManager
    {
        DataTable Dt { get; }

        List<FilterCriteria> Filters { get; }

        int Total { get; }

        int TotalPages { get; }

        event System.EventHandler OnDataSourceChange;

        //void Reload();

        //void Reload(List<FilterCriteria> filters);

        Task<bool> Reload();

        Task<bool> Reload(List<FilterCriteria> filters);

        //Task<bool> ReloadAsync();

        //Task<bool> ReloadAsync(List<FilterCriteria> filters);

        void SetCurrentPage(int currentPage);
        int CurrentPage { get; }

        void SetFilters(List<FilterCriteria> filters);

        void SetPageSize(int pageSize);

        string FilterOptionStr { get; }
    }
}

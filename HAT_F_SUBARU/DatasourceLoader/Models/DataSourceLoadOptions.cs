using System.Collections.Generic;

namespace Dma.DatasourceLoader.Models
{
    public class DataSourceLoadOptions
    {
        public List<FilterOption> Filters { get; set; } = new List<FilterOption>();
        public List<OrderOption> Orders { get; set; } = new List<OrderOption>();
        public int Cursor { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}

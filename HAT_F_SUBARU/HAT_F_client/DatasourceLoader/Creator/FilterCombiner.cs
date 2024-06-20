using Dma.DatasourceLoader.Filters;

namespace Dma.DatasourceLoader.Creator
{
    public enum FilterCombinationTypes
    {
        AND, OR
    }
    public class FilterCombiner<T>
    {
        private FilterBaseBase filter;
        public FilterBaseBase Filter { get { return filter; } }

        public FilterCombiner(FilterBaseBase filter)
        {
            this.filter = filter;
        }

        public FilterCombiner<T> AddAnd(FilterBaseBase f)
        {
            var andExpr = new AndFilter<T>(filter, f);
            this.filter = andExpr;
            return this;
        }

        public FilterCombiner<T> AddOr(FilterBaseBase f)
        {
            var orExpr = new OrFilter<T>(filter, f);
            this.filter = orExpr;
            return this;
        }
    }
}

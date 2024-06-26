namespace Dma.DatasourceLoader.Models
{
    public class FilterOption
    {
        public FilterOption(string PropertyName, string Operator, object Value)
        {
            this.PropertyName = PropertyName;
            this.Operator = Operator;
            this.Value = Value;
        }

        public FilterOption(string PropertyName, string Operator, object Value, string GroupKey)
            : this(PropertyName, Operator, Value)
        {
            this.GroupKey = GroupKey;
        }

        public string PropertyName { get; }
        public string Operator { get; }
        public object Value { get; }
        public string GroupKey { get; }
    }
}

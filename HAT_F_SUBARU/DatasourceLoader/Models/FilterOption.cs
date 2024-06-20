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

        public string PropertyName { get; }
        public string Operator { get; }
        public object Value { get; }
    }
}

namespace Dma.DatasourceLoader.Models
{
    public class OrderOption
    {
        public OrderOption(string Selector, string Desc)
        {
            this.Selector = Selector;
            this.Desc = Desc;
        }

        public string Selector { get; }
        public string Desc { get; }
    }
}
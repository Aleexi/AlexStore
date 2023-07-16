using Core.Entities;

namespace Core.OrderAggregation
{
    public class DelieveryMethod : SuperEntity
    {
        public string ShortName { get; set; }
        public string DelieveryTime { get; set; }
        public string Description { get; set; }
        public decimal PriceOfDelievery { get; set; }
    }
}
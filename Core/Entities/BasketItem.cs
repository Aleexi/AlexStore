namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Abilitie { get; set; }
    }
}
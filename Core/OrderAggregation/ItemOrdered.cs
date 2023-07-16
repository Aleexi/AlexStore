namespace Core.OrderAggregation
{
    public class ItemOrdered
    {
        public ItemOrdered()
        {
        }

        public ItemOrdered(int itemId, string itemName, string pictureUrl)
        {
            ItemId = itemId;
            ItemName = itemName;
            PictureUrl = pictureUrl;
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string PictureUrl { get; set; }
    }
}
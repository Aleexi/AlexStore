namespace Core.Entities
{
    public class Product
    {
        // Id primary key of table Product, Id defaults to primary key
        public int Id { get; set; }  

        // Other columns in Product table
        public string Name { get; set; }

        public string Rarity { get; set; }
    } 
}
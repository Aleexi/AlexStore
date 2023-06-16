namespace Core.Entities
{
    public class Product : SuperEntity
    {
        // Columns in Product table
        public string Name { get; set; }

        public string Rarity { get; set; }

        public decimal Price { get; set; }

        public string PictureURL { get; set; }

        // Entity framework creates foreign key connected to the table ProductTypes
        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        // Entity framework creates foreign key connected to the table ProductBrands
        public ProductBrand ProductBrand { get; set; }

        public int ProductBrandId { get; set; }
    } 
}
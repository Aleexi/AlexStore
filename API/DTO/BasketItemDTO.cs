using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        [Range(0.5, double.MaxValue, ErrorMessage = "The Item price canno't be below 0.5")]
        public decimal ItemPrice { get; set; }
        
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The quantity canno't be below 0")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Type { get; set; }
        
        public string Brand { get; set; }

        public string Abilitie { get; set; }
    }
}
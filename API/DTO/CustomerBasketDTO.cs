using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.DTO
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } 
    }
}
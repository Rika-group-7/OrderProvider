using System.ComponentModel.DataAnnotations;

namespace OrderProvider.Models
{
    public class OrderItemDto
    {
        [Required]
        public string ProductId { get; set; } = null!;
        [Required]
        public string ProductName { get; set; } = null!;
        public string? Size { get; set; }
        public string? Color { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

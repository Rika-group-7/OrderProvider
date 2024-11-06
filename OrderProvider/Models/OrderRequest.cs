using System.ComponentModel.DataAnnotations;

namespace OrderProvider.Models;

public class OrderRequest
{
    [Required]
    public string CustomerId { get; set; } = null!;
    [Required]
    public string CustomerEmail { get; set; } = null!;
    [Required]
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}

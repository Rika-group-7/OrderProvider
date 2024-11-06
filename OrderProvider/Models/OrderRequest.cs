namespace OrderProvider.Models;

public class OrderRequest
{
    public string CustomerId { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}

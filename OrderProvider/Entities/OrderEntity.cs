using System.ComponentModel.DataAnnotations;

namespace OrderProvider.Entities;

public class OrderEntity
{
    [Key]
    public string Id { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; } 

    // Navigation property to OrderItemEntity
    public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
}

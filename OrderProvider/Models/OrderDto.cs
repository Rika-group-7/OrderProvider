using OrderProvider.Entities;

namespace OrderProvider.Models
{
    public class OrderDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CustomerId { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;

        // Navigation property to OrderItemEntity with lazy loading
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    }
}

using Microsoft.EntityFrameworkCore;
using OrderProvider.Entities;

namespace OrderProvider.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;


    }
}

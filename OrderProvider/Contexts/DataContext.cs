using Microsoft.EntityFrameworkCore;
using OrderProvider.Entities;

namespace OrderProvider.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between OrderEntity and OrderItemEntity
            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<OrderItemEntity>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            // Additional configurations can be added here
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrderProvider.Contexts;
using OrderProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Repositories_Tests;

public class OrderRepo_Tests
{
    private readonly DataContext _context = new(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options);

    [Fact]
    public async void Create_OneOrder_ShouldCreateOrder_AndOrderItems_AndReturnOrder()
    {
        // Arrange
        var orderEntity = new OrderEntity
        {
            Id = "orderTest123",
            CustomerId = "test123",
            CustomerEmail = "test@example.com",
            OrderNumber = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 100,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest123",
                    ProductId = "test123",
                    ProductName = "test123",
                    Size = "test123",
                    Color = "test123",
                    Quantity = 1,
                    Price = 100
                }
            }
        };

        // Act

        var result = await _context.Orders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();

        // Assert
        var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == "orderTest123");
        Assert.NotNull(order);
        Assert.Equal("test123", order.CustomerId);
        Assert.Equal(1, order.OrderNumber);
        Assert.Equal(100, order.TotalAmount);
        Assert.Single(order.OrderItems);
        Assert.Equal("test123", order.OrderItems.First().ProductId);

    }
}
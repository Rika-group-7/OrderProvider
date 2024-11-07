using OrderProvider.Factories;
using OrderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Factories_Tests;

public class OrderFactory_Tests
{
    [Fact]
    public void Create_OrderRequestAndOrderNumber_ShouldReturnOrderEntity()
    {
        // Arrange
        var orderRequest = new OrderRequest
        {
            CustomerId = "test123",
            CustomerEmail = "test@example.com",
            OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto
                {
                    ProductId = "test123",
                    ProductName = "test123",
                    Size = "test123",
                    Color = "test123",
                    Quantity = 2,
                    Price = 100
                }
            }
        };

        // Act
        var orderEntity = OrderFactory.Create(orderRequest, 1);

        // Assert
        Assert.NotNull(orderEntity);
        Assert.Equal("test123", orderEntity.CustomerId);
        Assert.Equal(1, orderEntity.OrderNumber);
        Assert.Equal(200, orderEntity.TotalAmount);
        Assert.Single(orderEntity.OrderItems);
        Assert.Equal("test123", orderEntity.OrderItems.First().ProductId);

    }
}

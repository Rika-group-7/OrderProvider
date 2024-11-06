using Moq;
using OrderProvider.Entities;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using OrderProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Services_Tests;

public class OrderService_Tests
{
    private readonly Mock<IOrderRepo> _repo;
    private readonly IOrderService _service;

    public OrderService_Tests()
    {
        _repo = new Mock<IOrderRepo>();
        _service = new OrderService(_repo.Object, new Mock<IOrderItemRepo>().Object);
    }

    [Fact]
    public async Task Should_Create_order_AndReturnTrue()
    {
        // Arrange
        var orderRequest = new OrderRequest
        {
            CustomerId = "1",
            CustomerEmail = "test@example.com",
            OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto
                {
                    ProductId = "1",
                    ProductName = "Product 1",
                    Size = "M",
                    Color = "Red",
                    Quantity = 1,
                    Price = 100
                },
                new OrderItemDto
                {
                    ProductId = "2",
                    ProductName = "Product 2",
                    Size = "L",
                    Color = "Blue",
                    Quantity = 2,
                    Price = 200
                }
            }
        };

        _repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<OrderEntity>());
        _repo.Setup(repo => repo.CreateOneAsync(It.IsAny<OrderEntity>())).ReturnsAsync(new OrderEntity());

        OrderEntity capturedOrderEntity = null!;
        _repo.Setup(repo => repo.CreateOneAsync(It.IsAny<OrderEntity>()))
            .Callback<OrderEntity>(order => capturedOrderEntity = order)
            .ReturnsAsync(new OrderEntity());

        // Act
        var result = await _service.CreateOrderAsync(orderRequest);

        // Assert
        Assert.True(result);
        _repo.Verify(repo => repo.GetAllAsync(), Times.Once);
        _repo.Verify(repo => repo.CreateOneAsync(It.IsAny<OrderEntity>()), Times.Once);
        Assert.NotNull(capturedOrderEntity);
        Assert.Equal(1, capturedOrderEntity.OrderNumber);
    }
}

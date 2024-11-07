using Moq;
using OrderProvider.Entities;
using OrderProvider.Factories;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using OrderProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Services_Tests;

public class OrderService_Tests
{
    private readonly Mock<IOrderRepo> _orderRepoMock;
    private readonly Mock<IOrderItemRepo> _orderItemRepoMock;
    private readonly IOrderService _orderService;

    public OrderService_Tests()
    {
        _orderRepoMock = new Mock<IOrderRepo>();
        _orderItemRepoMock = new Mock<IOrderItemRepo>();
        _orderService = new OrderService(_orderRepoMock.Object, _orderItemRepoMock.Object);
    }

    [Fact]
    public async Task CreateOrder_ShouldCreateOrder_AndReturnTrue()
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
                }
            }
        };

        var orderEntity = OrderFactory.Create(orderRequest, 1);
        _orderRepoMock.Setup(repo => repo.CreateOneAsync(It.IsAny<OrderEntity>()))
                      .ReturnsAsync(orderEntity);

        _orderItemRepoMock.Setup(repo => repo.CreateOneAsync(It.IsAny<OrderItemEntity>()))
                          .ReturnsAsync((OrderItemEntity item) => item);

        // Act
        var result = await _orderService.CreateOrderAsync(orderRequest);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public async Task GetAllOrdersAsync_ShouldReturnAllOrders_IfOrdersExist()
    {
        // Arrange
        var orderEntities = new List<OrderEntity>
        {
            new OrderEntity
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
            },
            new OrderEntity
            {
                Id = "orderTest124",
                CustomerId = "test124",
                CustomerEmail = "test2@example.com",
                OrderNumber = 2,
                OrderDate = DateTime.Now,
                TotalAmount = 200,
                OrderItems = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = "orderItemTest124",
                        ProductId = "test124",
                        ProductName = "test124",
                        Size = "test124",
                        Color = "test124",
                        Quantity = 2,
                        Price = 200
                    }
                }
            }
        };

        _orderRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderEntities);

        // Act
        var result = await _orderService.GetAllOrdersAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(1, result.First().OrderNumber);
        Assert.Equal(2, result.Last().OrderNumber);
    }

    [Fact]
    public async Task GetOrderByOrderNumberAsync_ShouldReturnOrder_IfOrderExist()
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

        var orderEntity = OrderFactory.Create(orderRequest, 1);

        _orderRepoMock.Setup(repo => repo.GetOneAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                      .ReturnsAsync(orderEntity);

        // Act
        var result = await _orderService.GetOrderByOrderNumberAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.OrderNumber);
    }

    [Fact]
    public async Task GetOrdersByCustomerAsync_ShouldReturnAllOrdersByCustomer_IfOrdersExist()
    {
        // Arrange
        var orderEntities = new List<OrderEntity>
    {
        new OrderEntity
        {
            Id = "orderTest123",
            CustomerId = "1",
            CustomerEmail = "test@example.com",
            OrderNumber = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 100,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest123",
                    ProductId = "1",
                    ProductName = "Product 1",
                    Size = "M",
                    Color = "Red",
                    Quantity = 1,
                    Price = 100
                }
            }
        },
        new OrderEntity
        {
            Id = "orderTest124",
            CustomerId = "2",
            CustomerEmail = "test2@example.com",
            OrderNumber = 2,
            OrderDate = DateTime.Now,
            TotalAmount = 200,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest124",
                    ProductId = "2",
                    ProductName = "Product 2",
                    Size = "L",
                    Color = "Blue",
                    Quantity = 2,
                    Price = 200
                }
            }
        }
    };

        _orderRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderEntities);


        // Act
        var result = await _orderService.GetOrdersByCustomerIdAsync("1");

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(1, result.First().OrderNumber);
        Assert.Equal("1", result.First().CustomerId);

    }

    [Fact]
    public async Task GetOrdersByCustomerEmailAsync_ShouldReturnAllOrdersByCustomerEmail_IfOrdersExist()
    {
        // Arrange
        var orderEntities = new List<OrderEntity>
        {
            new OrderEntity
            {
                Id = "orderTest123",
                CustomerId = "1",
                CustomerEmail = "test@example.com",
                OrderNumber = 1,
                OrderDate = DateTime.Now,
                TotalAmount = 100,
                OrderItems = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = "orderItemTest123",
                        ProductId = "1",
                        ProductName = "Product 1",
                        Size = "M",
                        Color = "Red",
                        Quantity = 1,
                        Price = 100
                    }
                }
            },
            new OrderEntity
            {
                Id = "orderTest124",
                CustomerId = "2",
                CustomerEmail = "test2@example.com",
                OrderNumber = 2,
                OrderDate = DateTime.Now,
                TotalAmount = 200,
                OrderItems = new List<OrderItemEntity>
                {
                    new OrderItemEntity
                    {
                        Id = "orderItemTest124",
                        ProductId = "2",
                        ProductName = "Product 2",
                        Size = "L",
                        Color = "Blue",
                        Quantity = 2,
                        Price = 200
                    }
                }
            }
        };

        _orderRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderEntities);

        // Act
        var result = await _orderService.GetOrdersByCustomerEmailAsync("test@example.com");

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);

    }

    [Fact]
    public async Task DeleteOrderAsync_ShouldDeleteOrder_AndReturnTrue()
    {
        // Arrange
        var orderEntity = new OrderEntity
        {
            Id = "orderTest123",
            CustomerId = "1",
            CustomerEmail = "test@example.com",
            OrderNumber = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 100,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest123",
                    ProductId = "1",
                    ProductName = "Product 1",
                    Size = "M",
                    Color = "Red",
                    Quantity = 1,
                    Price = 100
                }
            }
        };

        _orderRepoMock.Setup(repo => repo.GetOneAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                      .ReturnsAsync(orderEntity);

        _orderRepoMock.Setup(repo => repo.DeleteOneAsync(It.IsAny<Expression<Func<OrderEntity, bool>>>()))
                      .ReturnsAsync(true);

        // Act
        var result = await _orderService.DeleteOrderAsync(1);

        // Assert
        Assert.True(result);

    }
}


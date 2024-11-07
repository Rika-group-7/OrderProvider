using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using OrderProvider.Contexts;
using OrderProvider.Entities;
using OrderProvider.Models;
using OrderProvider.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Repositories_Tests;

public class OrderRepo_Tests
{
    private readonly DataContext _context;
    private readonly OrderRepo _orderRepo;

    public OrderRepo_Tests()
    {
        _context = new DataContext(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
        _orderRepo = new OrderRepo(_context);
    }


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

        var result = await _orderRepo.CreateOneAsync(orderEntity);


        // Assert
        var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == "orderTest123");
        Assert.NotNull(order);
        Assert.Equal("test123", order.CustomerId);
        Assert.Equal(1, order.OrderNumber);
        Assert.Equal(100, order.TotalAmount);
        Assert.Single(order.OrderItems);
        Assert.Equal("test123", order.OrderItems.First().ProductId);

    }

    [Fact]
    public async void ExistsAsync_WillReturnTrue_IfOrderExists()
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

        await _orderRepo.CreateOneAsync(orderEntity);

        // Act
        var result = await _context.Orders.AnyAsync(x => x.Id == "orderTest123");
    }


    [Fact]
    public async void GetAllAsync_WillReturnAllOrders_IfOrdersExist()
    {
        // Arrange
        var orderEntity = new OrderEntity
        {
            Id = "orderTest1",
            CustomerId = "test123",
            CustomerEmail = "test@example.com",
            OrderNumber = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 100,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest1",
                    ProductId = "test123",
                    ProductName = "test123",
                    Size = "test123",
                    Color = "test123",
                    Quantity = 1,
                    Price = 100
                }
            }
        };
        var orderEntity2 = new OrderEntity
        {
            Id = "orderTest2",
            CustomerId = "test123",
            CustomerEmail = "test@example.com",
            OrderNumber = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 100,
            OrderItems = new List<OrderItemEntity>
            {
                new OrderItemEntity
                {
                    Id = "orderItemTest2",
                    ProductId = "test123",
                    ProductName = "test123",
                    Size = "test123",
                    Color = "test123",
                    Quantity = 1,
                    Price = 100
                }
            }
        };

        await _orderRepo.CreateOneAsync(orderEntity);
        await _orderRepo.CreateOneAsync(orderEntity2);

        // Act
        var result = await _orderRepo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

    }

    [Fact]
    public async void GetOneAsync_WillReturnOrder_IfOrderExist()
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

        await _orderRepo.CreateOneAsync(orderEntity);

        // Act
        var result = await _orderRepo.GetOneAsync(x => x.Id == "orderTest123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test123", result.CustomerId);
        Assert.Equal(1, result.OrderNumber);
    }

    [Fact]
    public async void DeleteOneAsync_WillDeleteOrder_IfOrderExist()
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

        await _orderRepo.CreateOneAsync(orderEntity);

        // Act
        var result = await _orderRepo.DeleteOneAsync(x => x.OrderNumber == 1);

        // Assert
        var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == "orderTest123");
        Assert.Null(order);

    }
}
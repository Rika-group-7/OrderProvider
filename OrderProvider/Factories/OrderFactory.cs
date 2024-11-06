using OrderProvider.Entities;
using OrderProvider.Models;

namespace OrderProvider.Factories;

public static class OrderFactory
{
    public static OrderEntity Create(OrderDto order)
    {
        // creates a new OrderEntity and creates an OrderItemEntity object for each OrderItemDto object in the OrderDto object
        return new OrderEntity
        {
            CustomerId = order.CustomerId,
            CustomerEmail = order.CustomerEmail,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity),
            OrderItems = order.OrderItems.Select(Create).ToList()
        };
    }

    public static OrderDto Create(OrderEntity order)
    {
        // creates a new OrderDto object and creates an OrderItemDto object for each OrderItemEntity object in the OrderEntity object
        return new OrderDto
        {
            CustomerId = order.CustomerId,
            CustomerEmail = order.CustomerEmail,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(Create).ToList()
        };
    }


    // OrderItemEntity


    public static OrderItemEntity Create(OrderItemDto orderItem)
    {
        // creates a new OrderItemEntity object from every single OrderItemDto object in the OrderDto object
        return new OrderItemEntity
        {
            ProductId = orderItem.ProductId,
            ProductName = orderItem.ProductName,
            Size = orderItem.Size,
            Color = orderItem.Color,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }

    public static OrderItemDto Create(OrderItemEntity orderItem)
    {
        // creates a new OrderItemDto object from every single OrderItemEntity object in the OrderEntity object
        return new OrderItemDto
        {
            ProductId = orderItem.ProductId,
            ProductName = orderItem.ProductName,
            Size = orderItem.Size,
            Color = orderItem.Color,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }
}

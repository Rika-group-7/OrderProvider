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

    // creates a new OrderItemEntity object from every single OrderItemDto object in the OrderDto object
    public static OrderItemEntity Create(OrderItemDto orderItem)
    {
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
}

using OrderProvider.Entities;
using OrderProvider.Models;

namespace OrderProvider.Factories;

public static class OrderFactory
{
    public static OrderEntity Create(OrderRequest orderRequest, int orderNumber)
    {
        // creates a new OrderEntity and creates an OrderItemEntity object for each OrderItemDto object in the OrderDto object
        var orderId = Guid.NewGuid().ToString();
        return new OrderEntity
        {
            Id = orderId,
            CustomerId = orderRequest.CustomerId,
            CustomerEmail = orderRequest.CustomerEmail,
            OrderNumber = orderNumber,
            OrderDate = DateTime.UtcNow,
            TotalAmount = orderRequest.OrderItems.Sum(x => x.Price * x.Quantity),
            OrderItems = orderRequest.OrderItems.Select(item => new OrderItemEntity
            {
                Id = Guid.NewGuid().ToString(),
                OrderEntityId = orderId,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Size = item.Size,
                Color = item.Color,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
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
            OrderItems = order.OrderItems.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Size = item.Size,
                Color = item.Color,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };
    }


    // OrderItemEntity


    //public static OrderItemEntity Create(OrderItemDto orderItem)
    //{
    //    // creates a new OrderItemEntity object from every single OrderItemDto object in the OrderDto object
    //    return new OrderItemEntity
    //    {
    //        ProductId = orderItem.ProductId,
    //        ProductName = orderItem.ProductName,
    //        Size = orderItem.Size,
    //        Color = orderItem.Color,
    //        Quantity = orderItem.Quantity,
    //        Price = orderItem.Price
    //    };
    //}

    //public static OrderItemDto Create(OrderItemEntity orderItem)
    //{
    //    // creates a new OrderItemDto object from every single OrderItemEntity object in the OrderEntity object
    //    return new OrderItemDto
    //    {
    //        ProductId = orderItem.ProductId,
    //        ProductName = orderItem.ProductName,
    //        Size = orderItem.Size,
    //        Color = orderItem.Color,
    //        Quantity = orderItem.Quantity,
    //        Price = orderItem.Price
    //    };
    //}
}

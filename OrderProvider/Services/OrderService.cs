using OrderProvider.Entities;
using OrderProvider.Factories;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System.Diagnostics;

namespace OrderProvider.Services;

public class OrderService(IOrderRepo orderRepo, IOrderItemRepo orderItemRepo)
{
    private readonly IOrderRepo _orderRepo = orderRepo;
    private readonly IOrderItemRepo _orderItemRepo = orderItemRepo;


    // create an order and its order items
    public async Task<bool> CreateOrderAsync(OrderDto order)
    {
        try
        {

            var allOrders = await _orderRepo.GetAllAsync();
            var highestOrderNumber = allOrders.OrderByDescending(o => o.OrderNumber).Select(o => o.OrderNumber).FirstOrDefault();
            order.OrderNumber = highestOrderNumber + 1;
            var orderEntity = OrderFactory.Create(order);
            await _orderRepo.CreateOneAsync(orderEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    // fetch an order by its order number
    public async Task<OrderDto> GetOrderAsync(int orderNumber)
    {
        try
        {
            var orderEntity = await _orderRepo.GetOneAsync(o => o.OrderNumber == orderNumber);
            if (orderEntity != null)
            {
                var orderDto = OrderFactory.Create(orderEntity);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;

    }
}

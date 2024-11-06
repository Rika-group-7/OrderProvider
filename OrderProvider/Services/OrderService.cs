using OrderProvider.Entities;
using OrderProvider.Factories;
using OrderProvider.Interfaces;
using OrderProvider.Models;
using System.Diagnostics;

namespace OrderProvider.Services;

public class OrderService(IOrderRepo orderRepo, IOrderItemRepo orderItemRepo) : IOrderService
{
    private readonly IOrderRepo _orderRepo = orderRepo;
    private readonly IOrderItemRepo _orderItemRepo = orderItemRepo;


    #region CREATE

    // create an order and its order items
    public async Task<bool> CreateOrderAsync(OrderRequest orderRequest)
    {
        try
        {
            var allOrders = await _orderRepo.GetAllAsync();
            var highestOrderNumber = allOrders.OrderByDescending(o => o.OrderNumber).Select(o => o.OrderNumber).FirstOrDefault();
            var orderNumber = highestOrderNumber + 1;

            var orderEntity = OrderFactory.Create(orderRequest, orderNumber);
            var saveOrder = await _orderRepo.CreateOneAsync(orderEntity);
            if (saveOrder != null)
            {
                return true;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    #endregion



    #region READ

    // fetch all orders
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        try
        {
            var orderEntities = await _orderRepo.GetAllAsync();
            var orders = orderEntities.Select(OrderFactory.Create).ToList();
            return orders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }


    // fetch an order by its id
    public async Task<OrderDto> GetOrderByIdAsync(string orderId)
    {
        try
        {
            var orderEntity = await _orderRepo.GetOneAsync(o => o.Id == orderId);
            var order = OrderFactory.Create(orderEntity);
            return order;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    // fetch all orders by a customer
    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(string customerId)
    {
        try
        {
            var allOrders = await _orderRepo.GetAllAsync();
            var customerOrders = allOrders.Where(o => o.CustomerId == customerId).ToList();
            var orders = customerOrders.Select(OrderFactory.Create).ToList();
            return orders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    #endregion

    #region DELETE

    public async Task<bool> DeleteOrderAsync(string orderId)
    {
        try
        {
            var orderEntity = await _orderRepo.GetOneAsync(o => o.Id == orderId);
            if (orderEntity != null)
            {
                var deleteOrder = await _orderRepo.DeleteOneAsync(x => x.Id == orderId);
                if (deleteOrder)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    #endregion
}

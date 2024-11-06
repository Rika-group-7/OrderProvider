using OrderProvider.Models;

namespace OrderProvider.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderRequest orderRequest);
        Task<bool> DeleteOrderAsync(string orderId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(string customerId);
    }
}
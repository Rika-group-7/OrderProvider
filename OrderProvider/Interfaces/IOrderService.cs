using OrderProvider.Models;

namespace OrderProvider.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderRequest orderRequest);
        Task<bool> DeleteOrderAsync(int orderNumber);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByOrderNumberAsync(int orderNumber);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string customerEmail);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
using OrderProvider.Interfaces;

namespace OrderProvider.Services;

public class OrderService(IOrderRepo orderRepo, IOrderItemRepo orderItemRepo)
{
    private readonly IOrderRepo _orderRepo = orderRepo;
    private readonly IOrderItemRepo _orderItemRepo = orderItemRepo;



}

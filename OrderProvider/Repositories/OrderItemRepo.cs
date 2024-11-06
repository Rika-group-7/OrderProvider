using OrderProvider.Contexts;
using OrderProvider.Entities;
using OrderProvider.Interfaces;

namespace OrderProvider.Repositories
{
    public class OrderItemRepo(DataContext context) : BaseRepo<OrderItemEntity>(context), IOrderItemRepo
    {
        private readonly DataContext _context = context;
    }
}

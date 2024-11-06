using OrderProvider.Contexts;
using OrderProvider.Entities;
using OrderProvider.Interfaces;

namespace OrderProvider.Repositories
{
    public class OrderRepo(DataContext context) : BaseRepo<OrderEntity>(context) , IOrderRepo
    {
        private readonly DataContext _context = context;
    }
}

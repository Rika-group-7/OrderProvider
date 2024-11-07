using Microsoft.EntityFrameworkCore;
using OrderProvider.Contexts;
using OrderProvider.Entities;
using OrderProvider.Interfaces;
using System.Linq.Expressions;

namespace OrderProvider.Repositories
{
    public class OrderRepo(DataContext context) : BaseRepo<OrderEntity>(context) , IOrderRepo
    {
        private readonly DataContext _context = context;

        public override async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public override async Task<OrderEntity> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate)
        {
            return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(predicate) ?? new OrderEntity();
        }
    }
}

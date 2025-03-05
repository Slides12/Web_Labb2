using Microsoft.EntityFrameworkCore;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly APIDBContext _context;
        public async Task CreateOrderAsync(OrderInfo order)
        {
            await _context.Orders.AddAsync(order);
        }

        public void DeleteOrderAsync(OrderInfo order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<IEnumerable<OrderInfo>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public void UpdateOrderAsync(OrderInfo order)
        {
            _context.Orders.Update(order);
        }
    }
}

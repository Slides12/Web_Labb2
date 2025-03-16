using Microsoft.EntityFrameworkCore;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly APIDBContext _context;

        public OrderRepository(APIDBContext context)
        {
            _context = context;
        }

        public async Task<OrderInfo> CreateOrderAsync(OrderInfo order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public void DeleteOrderAsync(OrderInfo order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<IEnumerable<OrderInfo>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<OrderInfo> GetOrdersByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<IEnumerable<OrderInfo>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders.Where(o => o.CustomerID == customerId).Include(o => o.OrderDetails).ToListAsync();
        }

        public void UpdateOrderAsync(OrderInfo order)
        {
            _context.Orders.Update(order);
        }
    }
}

using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderInfo>> GetAllOrdersAsync();
        public Task<OrderInfo> CreateOrderAsync(OrderInfo order);
        public Task<OrderInfo> GetOrdersByIdAsync(int orderId);
        public void UpdateOrderAsync(OrderInfo order);
        public void DeleteOrderAsync(OrderInfo order);
    }
}

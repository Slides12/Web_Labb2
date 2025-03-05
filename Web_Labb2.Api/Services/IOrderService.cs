using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderInfo>> GetAllOrdersAsync();
        public Task CreateOrderAsync(OrderInfo order);
        public void UpdateOrderAsync(OrderInfo order);
        public void DeleteOrderAsync(OrderInfo order);
    }
}

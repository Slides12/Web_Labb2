using Web_Labb2.Shared.Models;
using Web_Labb2.Shared.DTO_s;

namespace Web_Labb2.Api.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        public Task<OrderDTO> CreateOrderAsync(OrderDTO order);
        public Task<OrderInfo> GetOrderById(int order);
        public Task<OrderDTO> UpdateOrderAsync(OrderDTO order);
        public Task DeleteOrderAsync(int order);
    }
}

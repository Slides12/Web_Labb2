using Web_Labb2.Data;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CreateOrderAsync(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderAsync(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderInfo>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderAsync(OrderInfo order)
        {
            throw new NotImplementedException();
        }
    }
}

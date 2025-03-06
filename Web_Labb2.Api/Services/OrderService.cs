using Web_Labb2.Data;
using Web_Labb2.Shared.Models;
using Web_Labb2.Shared.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace Web_Labb2.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO order)
        {
            if (order.CustomerID == 0 || order.OrderDetails == null)
                return null;

            var newOrder = new OrderInfo
            {
                CustomerID = order.CustomerID,
                Customer = order.Customer,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetail
                {
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };

            var createdOrder = await _unitOfWork.Orders.CreateOrderAsync(newOrder);

            return new OrderDTO
            {
                CustomerID = createdOrder.CustomerID,
                OrderDate = createdOrder.OrderDate,
                TotalAmount = createdOrder.TotalAmount,
                Customer = createdOrder.Customer,
                OrderDetails = createdOrder.OrderDetails
            };
        }


        public async Task DeleteOrderAsync(OrderDTO order)
        {
            var deleteOrder = await GetOrderById(order.OrderID);
            if (deleteOrder != null)
            {
                _unitOfWork.Orders.DeleteOrderAsync(deleteOrder);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return _unitOfWork.Orders.GetAllOrdersAsync();
        }

        public Task<OrderInfo> GetOrderById(int order)
        {
            var getOrder = _unitOfWork.Orders.GetOrdersByIdAsync(order);
            if (getOrder != null)
            {
                return getOrder;
            }
            else
            {
                return null;
            }
        }


        public void UpdateOrderAsync(OrderDTO order)
        {
            if (order.CustomerID != 0 || order.OrderDetails != null)
            {
                var newOrder = new OrderInfo
                {
                    CustomerID = order.CustomerID,
                    Customer = order.Customer,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetail
                    {
                        ProductID = od.ProductID,
                        Quantity = od.Quantity,
                        Price = od.Price
                    }).ToList()
                };

                _unitOfWork.Orders.UpdateOrderAsync(newOrder);
            }
                
        }
    }
}

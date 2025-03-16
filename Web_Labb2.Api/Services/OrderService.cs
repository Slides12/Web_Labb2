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

            
            order.OrderDate = DateTime.UtcNow;

            var newOrder = new OrderInfo
            {
                CustomerID = order.CustomerID,
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
            await _unitOfWork.SaveChangesAsync();

            return new OrderDTO
            {
                CustomerID = createdOrder.CustomerID,
                OrderDate = createdOrder.OrderDate,
                TotalAmount = createdOrder.TotalAmount,
            };
        }



        public async Task DeleteOrderAsync(int order)
        {
            var deleteOrder = await GetOrderById(order);
            if (deleteOrder != null)
            {
                _unitOfWork.Orders.DeleteOrderAsync(deleteOrder);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await  _unitOfWork.Orders.GetAllOrdersAsync();

            return orders.Select(o => new OrderDTO
            {
                OrderID = o.OrderID,
                CustomerID = o.CustomerID,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
            });
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(customerId);

            return orders.Select(o => new OrderDTO
            {
                OrderID = o.OrderID,
                CustomerID = o.CustomerID,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.OrderDetailID,
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            });
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


        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO order)
        {
            if (order.CustomerID == 0 || order.OrderDetails == null || !order.OrderDetails.Any())
            {
                throw new ArgumentException("Invalid order data. CustomerID and OrderDetails are required.");
            }

            var existingOrder = await _unitOfWork.Orders.GetOrdersByIdAsync(order.OrderID);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with ID {order.OrderID} not found.");
            }

            existingOrder.CustomerID = order.CustomerID;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.OrderDetails = order.OrderDetails.Select(od => new OrderDetail
            {
                ProductID = od.ProductID,
                Quantity = od.Quantity,
                Price = od.Price
            }).ToList();

            _unitOfWork.Orders.UpdateOrderAsync(existingOrder);
            await _unitOfWork.SaveChangesAsync();

            return new OrderDTO
            {
                OrderID = existingOrder.OrderID,
                CustomerID = existingOrder.CustomerID,
                OrderDate = existingOrder.OrderDate,
                TotalAmount = existingOrder.TotalAmount,
                OrderDetails = existingOrder.OrderDetails.Select(od => new OrderDetailDTO
                {
                    ProductID = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            };
        }

    }
}

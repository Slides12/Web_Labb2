using Microsoft.AspNetCore.Mvc;
using Web_Labb2.Api.Services;
using Web_Labb2.DTO_s;
using Web_Labb2.Services;
using Web_Labb2.Shared.DTO_s;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Labb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var customers = await _orderService.GetAllOrdersAsync();
            return Ok(customers);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result == null)
            {
                return BadRequest("Order not found");
            }

            return Ok(result);
        }

        [HttpGet("orders/{customerId}")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrdersByCustomerId(int customerId)
        {
            var result = await _orderService.GetOrdersByCustomerId(customerId);
            if (result == null)
            {
                return BadRequest("Orders not found");
            }

            return Ok(result);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderDTO value)
        {
            Console.WriteLine(value.OrderDetails[0].ProductName);
            if (value == null || value.CustomerID == 0 || value.OrderDetails == null || value.OrderDetails.Count == 0)
            {
                return BadRequest("Invalid order data. CustomerID and OrderDetails are required.");
            }

            var result = await _orderService.CreateOrderAsync(value);

            if (result == null)
            {
                return BadRequest("Failed to create order. Please check the provided details.");
            }

            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderID }, result);
        }



        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderByEmail(int id, [FromBody] OrderDTO updatedOrder)
        {
            
            if (updatedOrder.OrderID == null || updatedOrder.CustomerID == null || updatedOrder.OrderDetails == null)
            {
                return BadRequest("You need to add updated information.");
            }

            var result = await _orderService.UpdateOrderAsync(updatedOrder);
            if (result == null)
            {
                return NotFound("Order not found or update failed.");
            }

            return Ok("Order is updated successfully.");
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderByEmail(int id)
        {
            if (id == null)
            {
                return BadRequest("You need to input a correct id.");
            }
            await _orderService.DeleteOrderAsync(id);
           
            return Ok($"Deleted user with email: {id}");
        }
    }
}

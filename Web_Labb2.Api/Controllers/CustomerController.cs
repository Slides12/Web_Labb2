using Microsoft.AspNetCore.Mvc;
using Web_Labb2.Api.Services;
using Web_Labb2.DTO_s;
using Web_Labb2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{email}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerByEmail(string email)
        {
            var result = await _customerService.GetCustomerByEmail(email);
            if (result == null)
            {
                return BadRequest("Customer not fouund. Did you put the correct email?");
            }

            return Ok(result);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult> PostCustomer([FromBody] CustomerDTO value)
        {
            if(value != null)
            { 
                var result = await _customerService.AddCustomerAsync(value);
                if(!result)
                {
                    return BadRequest("You need to add all information of the customer or the email is allready in use. Try again.");
                }
            }
            return Created("Added to the DB",value);

        }

      
        // DELETE api/<CustomerController>/5
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteCustomerByEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return BadRequest("You need to input a correct email.");
            }
            var result = await _customerService.DeleteCustomerByEmail(email);
            if(!result)
            {
                return NotFound("No user with that email exist.");
            }
            return Ok($"Deleted user with email: {email}");
        }


        [HttpPut("{email}")]
        public async Task<ActionResult> UpdateCustomerByEmail(string email, [FromBody] CustomerDTO updatedCustomer)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound("No user with that email exist.");
            }
            else if (string.IsNullOrEmpty(updatedCustomer.FirstName) || string.IsNullOrEmpty(updatedCustomer.LastName) || string.IsNullOrEmpty(updatedCustomer.Email))
            {
                return BadRequest("You need to add updated information.");
            }

            var result = await _customerService.UpdateCustomerByEmail(email, updatedCustomer);
            if (!result)
            {
                return NotFound("Customer not found or update failed.");
            }

            return Ok("Customer is updated successfully.");
        }
    }
}

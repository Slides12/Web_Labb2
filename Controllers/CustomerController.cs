using Microsoft.AspNetCore.Mvc;
using Web_Labb2.DTO_s;
using Web_Labb2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
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

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteCustomerByEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return BadRequest("You need to input a correct email.");
            }
            return Ok(await _customerService.DeleteCustomerByEmail(email));
        }
    }
}

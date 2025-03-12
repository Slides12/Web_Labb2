using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_Labb2.Api.Services;
using Web_Labb2.DTO_s;
using Web_Labb2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ProductDTO>>GetProductById(string id)
        {
            var result = await _productService.GetProductById(id);
            if(result == null)
            {
                return NotFound("There is no product by this number.");
            }
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProductDTO>> GetProductByName(string name)
        {
            var result = await _productService.GetProductByName(name);
            if (result == null)
            {
                return NotFound("There is no product by this name.");
            }
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody] ProductDTO newProduct)
        {
            if (newProduct != null)
            {
                var result = await _productService.AddProductAsync(newProduct);
                if (!result)
                {
                    return BadRequest("The product was null.");
                }

                return CreatedAtAction(nameof(PostProduct), new { id = newProduct.ProductId }, newProduct);
            }

            return BadRequest("Invalid product data");
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"https://localhost:7218/uploads/{fileName}"; // Return full URL

            return Ok(new { imagePath = imageUrl });
        }




        // PUT api/<ProductController>/5
        [HttpPut("update/{id}")]
        public async Task<ActionResult> PutAllInfo(string id, [FromBody] ProductDTO updatedProduct)
        {
            Console.WriteLine($"Received PUT request for product ID: {updatedProduct.ProductId}");

            if (string.IsNullOrEmpty(id)) return BadRequest("You need to enter a Product Id.");
            if (updatedProduct == null) return BadRequest("You need to add data to be updated.");

            var result = await _productService.UpdateProductAsync(id, updatedProduct);
            if (!result)
                return NotFound("Did not find a product by that id.");

            return Ok("Product was updated successfully.");
        }


        // PUT api/<ProductController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("status/{id}")]
        public async Task<ActionResult> PutStatus(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("You need to enter a Product Id.");

            var result = await _productService.UpdateProductStatus(id);
            if (!result)
                return NotFound("Did not find a product by that id.");

            return Ok("Product was updated successfully.");
        }

        // DELETE api/<ProductController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("You need to input a correct product Id.");
            }

            var result = await _productService.DeleteProductById(id);
            if (!result)
            {
                return NotFound("No product with that Id exists.");
            }

            return NoContent();
        }

    }
}

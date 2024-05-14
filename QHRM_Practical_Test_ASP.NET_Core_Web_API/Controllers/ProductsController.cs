using Microsoft.AspNetCore.Mvc;
using QHRM_Practical_Test_App_2.DAL;
using QHRM_Practical_Test_App_2.Models;

namespace QHRM_Practical_Test_App_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDataAccess _productDataAccess;

        public ProductsController(IProductDataAccess productDataAccess)
        {
            _productDataAccess = productDataAccess ?? throw new ArgumentNullException(nameof(productDataAccess));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productDataAccess.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productDataAccess.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product object is null");
                }
                var productId = await _productDataAccess.AddProductAsync(product);
                product.Id = productId;
                return CreatedAtAction(nameof(GetProduct), new { id = productId }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("Product id mismatch");
                }
                await _productDataAccess.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productDataAccess.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productDataAccess.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

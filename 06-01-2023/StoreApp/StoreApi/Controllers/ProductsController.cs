using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Data.Entities;
using StoreApi.Dtos.ProductDtos;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ProductsController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            ProductDetailDto productDto = new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                SalePrice = product.SalePrice,
                DiscountPercent = product.DiscountPercent,
                Category = new CategoryInProductDetailDto
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name
                }
            };

            return Ok(productDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page=1)
        {
            List<Product> products = _context.Products.Skip((page - 1) * 4).Take(4).ToList();

            return Ok(products);
        }
    }
}

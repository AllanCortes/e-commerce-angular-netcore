using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
    {
    public StoreContext Context { get; }
    public ProductsController(StoreContext context)
    {
      Context = context;
    }

    [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
        var products = await Context.Products.ToListAsync();
        return Ok(products);
    }

      [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          return await Context.Products.FindAsync(id);
        }
    }
}
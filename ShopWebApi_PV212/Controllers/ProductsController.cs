using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopWebApi_PV212.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext ctx;

        public ProductsController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }

        // [C]reate [R]ead [U]pdate [D]elete

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(ctx.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Add(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Update(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            ctx.Products.Remove(product);
            ctx.SaveChanges();

            return Ok();
        }
    }
}

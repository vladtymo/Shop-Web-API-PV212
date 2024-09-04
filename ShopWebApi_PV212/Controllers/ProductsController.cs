using AutoMapper;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopWebApi_PV212.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsController(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        // [C]reate [R]ead [U]pdate [D]elete

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var items = mapper.Map<List<ProductDto>>(ctx.Products.ToList());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            // load related table data
            ctx.Entry(product).Reference(x => x.Category).Load();

            return Ok(mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Add(mapper.Map<Product>(model));
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(EditProductDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPatch("archive")]
        public IActionResult Archive(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            product.Archived = true;
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPatch("restore")]
        public IActionResult Restore(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null) return NotFound();

            product.Archived = false;
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

using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
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
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        // [C]reate [R]ead [U]pdate [D]elete

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productsService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await productsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            await productsService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProductDto model)
        {
            await productsService.Edit(model);
            return Ok();
        }

        [HttpPatch("archive")]
        public async Task<IActionResult> Archive(int id)
        {
            await productsService.Archive(id);
            return Ok();
        }

        [HttpPatch("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            await productsService.Restore(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);
            return Ok();
        }
    }
}

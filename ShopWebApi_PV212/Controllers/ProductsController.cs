using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //try
            //{
                return Ok(await productsService.Get(id));
            //}
            //catch (Exception ex)
            //{
            //    return NotFound(new { ex.Message });
            //}
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            await productsService.Create(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProductDto model)
        {
            await productsService.Edit(model);
            return Ok();
        }

        [HttpPatch("archive/{id}")]
        public async Task<IActionResult> Archive(int id)
        {
            await productsService.Archive(id);
            return Ok();
        }

        [HttpPatch("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await productsService.Restore(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);
            return Ok();
        }
    }
}

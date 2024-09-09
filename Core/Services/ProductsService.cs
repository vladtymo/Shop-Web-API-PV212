using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;
        private readonly IValidator<CreateProductDto> validator;

        public ProductsService(
            ShopDbContext ctx, 
            IMapper mapper,
            IValidator<CreateProductDto> validator)
        {
            this.ctx = ctx;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task Archive(int id)
        {
            var product = await ctx.Products.FindAsync(id);
            if (product == null)
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            product.Archived = true;
            await ctx.SaveChangesAsync();
        }

        public async Task Create(CreateProductDto model)
        {
            // TODO: validate model
            //validator.ValidateAndThrow(model);

            ctx.Products.Add(mapper.Map<Product>(model));
            await ctx.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await ctx.Products.FindAsync(id);
            if (product == null)
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            ctx.Products.Remove(product);
            await ctx.SaveChangesAsync();
        }

        public async Task Edit(EditProductDto model)
        {
            // TODO: validate model

            ctx.Products.Update(mapper.Map<Product>(model));
            await ctx.SaveChangesAsync();
        }

        public async Task<ProductDto?> Get(int id)
        {
            var product = await ctx.Products.FindAsync(id);
            if (product == null) 
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            // load related table data
            await ctx.Entry(product).Reference(x => x.Category).LoadAsync();

            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return mapper.Map<List<ProductDto>>(await ctx.Products.ToListAsync());
        }

        public async Task Restore(int id)
        {
            var product = await ctx.Products.FindAsync(id);
            if (product == null) 
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            product.Archived = false;
            await ctx.SaveChangesAsync();
        }
    }
}

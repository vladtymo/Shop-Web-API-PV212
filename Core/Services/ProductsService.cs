﻿using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductsService : IProductsService
    {
        //private readonly ShopDbContext ctx;

        private readonly IMapper mapper;
        private readonly IValidator<CreateProductDto> validator;
        private readonly IRepository<Product> productR;
        private readonly IRepository<Category> categoryR;
        private readonly IFileService fileService;

        public ProductsService(
            IMapper mapper,
            IValidator<CreateProductDto> validator,
            IRepository<Product> productR,
            IRepository<Category> categoryR,
            IFileService fileService)
        {
            this.mapper = mapper;
            this.validator = validator;
            this.productR = productR;
            this.categoryR = categoryR;
            this.fileService = fileService;
        }

        public async Task Archive(int id)
        {
            var product = await productR.GetById(id);
            if (product == null)
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            product.Archived = true;
            await productR.Save();
        }

        public async Task Create(CreateProductDto model)
        {
            // TODO: validate model
            //validator.ValidateAndThrow(model);

            var entity = mapper.Map<Product>(model);
            
            if (model.Image != null)
                entity.ImageUrl = await fileService.SaveProductImage(model.Image);
                        
            await productR.Insert(entity);
            await productR.Save();
        }

        public async Task Delete(int id)
        {
            var product = await productR.GetById(id);
            if (product == null)
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            await productR.Delete(product);
            await productR.Save();
        }

        public async Task Edit(EditProductDto model)
        {
            // TODO: validate model

            await productR.Update(mapper.Map<Product>(model));
            await productR.Save();
        }

        public async Task<ProductDto?> Get(int id)
        {
            var product = await productR.GetById(id);
            if (product == null) 
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            // load related table data
            //await ctx.Entry(product).Reference(x => x.Category).LoadAsync();

            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return mapper.Map<List<ProductDto>>(await productR.Get(includeProperties: "Category"));
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return mapper.Map<IEnumerable<CategoryDto>>(await categoryR.GetAll());
        }

        public async Task Restore(int id)
        {
            var product = await productR.GetById(id);
            if (product == null) 
                throw new HttpException("Product not found!", HttpStatusCode.NotFound);

            product.Archived = false;
            await productR.Save();
        }
    }
}

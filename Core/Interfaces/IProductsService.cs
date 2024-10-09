using Core.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> Get(int id);
        Task Edit(EditProductDto model);
        Task Create(CreateProductDto model);
        Task Delete(int id);
        Task Archive(int id);
        Task Restore(int id);
    }
}

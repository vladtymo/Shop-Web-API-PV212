using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> Get(int id);
        Task Edit(EditProductDto model);
        Task Create(CreateProductDto model);
        Task Delete(int id);
        Task Archive(int id);
        Task Restore(int id);
    }
}

using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> GetProducts(ProductQueryParameters queryParameters);
        Task<IEnumerable<Product>> GetRelatedProducts(int productId);
    }
}
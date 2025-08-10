using API.Models;
using API.Extensions; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;
        
        public ProductService()
        {
            _products = GenerateMockProducts();
        }
        
        public Task<PaginatedList<Product>> GetProducts(ProductQueryParameters parameters)
        {
            var query = _products.AsQueryable();
            
            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(parameters.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }
            
            if (!string.IsNullOrEmpty(parameters.Category))
            {
                query = query.Where(p => p.Category == parameters.Category);
            }
            
            if (parameters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= parameters.MinPrice.Value);
            }
            
            if (parameters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= parameters.MaxPrice.Value);
            }
            
            return Task.FromResult(query.ToPaginatedList(parameters.PageNumber, parameters.PageSize));
        }
        
        public Task<IEnumerable<Product>> GetRelatedProducts(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return Task.FromResult(Enumerable.Empty<Product>());
            
            var related = _products
                .Where(p => p.Category == product.Category && p.Id != productId)
                .Take(4)
                .AsEnumerable();
                
            return Task.FromResult(related);
        }
        
        private List<Product> GenerateMockProducts()
        {
            var categories = new[] { "Electronics", "Clothing", "Home", "Books", "Toys" };
            var products = new List<Product>();
            
            for (int i = 1; i <= 25; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Price = 10 + (i * 5),
                    Category = categories[i % categories.Length],
                    ImageUrl = $"https://example.com/images/product_{i}.jpg",
                    IsInStock = i % 3 != 0
                });
            }
            
            return products;
        }
    }
}
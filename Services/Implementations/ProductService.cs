using Clase7BE.Entities;
using Clase7BE.Models.DTOs.Requests;
using Clase7BE.Models.DTOs.Responses;
using Clase7BE.Repositories.Interfaces;
using Clase7BE.Services.Interfaces;

namespace Clase7BE.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            return _repository.GetAllProducts().Select(ToReadDto).ToList();
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            return product is null ? null : ToReadDto(product);
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            _repository.AddProduct(product);

            return ToReadDto(product);
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            _repository.UpdateProduct(new Product
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price
            });
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product is not null)
            {
                _repository.DeleteProduct(product);
            }
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            return _repository.SearchProductsByName(name).Select(ToReadDto).ToList();
        }

        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();

            if (products.Count == 0)
            {
                return new ProductStatsDto();
            }

            return new ProductStatsDto
            {
                Total = products.Count,
                AveragePrice = products.Average(p => p.Price),
                MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
            };
        }

        public bool ProductNameExists(string name)
        {
            return _repository.GetAllProducts().Any(p =>
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private static ProductForReadDto ToReadDto(Product product)
        {
            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}

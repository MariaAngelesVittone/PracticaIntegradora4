using Clase7BE.Models.DTOs.Requests;
using Clase7BE.Models.DTOs.Responses;

namespace Clase7BE.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);
        List<ProductForReadDto> SearchProductsByName(string name);
        ProductStatsDto GetStats();
        bool ProductNameExists(string name);
    }
}

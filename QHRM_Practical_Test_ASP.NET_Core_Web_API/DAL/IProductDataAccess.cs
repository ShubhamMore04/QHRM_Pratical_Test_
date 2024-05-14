using Microsoft.AspNetCore.Mvc;
using QHRM_Practical_Test_App_2.Models;

namespace QHRM_Practical_Test_App_2.DAL
{
    public interface IProductDataAccess
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
    }
}

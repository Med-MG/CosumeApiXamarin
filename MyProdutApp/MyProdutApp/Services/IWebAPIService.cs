using MyProdutApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProdutApp.Services
{
    public interface IWebAPIService
    {
        Task<List<Product>> RefreshDataAsync();
        Task<Product> GetSingleProduct(string ID);
        Task SaveProductAsync(Product product, bool isNewItem = false);
        Task DeleteProductAsync(Product product);
    }
}

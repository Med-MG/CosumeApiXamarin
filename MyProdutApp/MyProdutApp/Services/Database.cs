using MyProdutApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MyProdutApp.Services
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProducts()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<Product> GetSingleProduct(int ID)
        {
            return _database.Table<Product>().Where(x => x.Id == ID).FirstOrDefaultAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }

        public Task<int> UpdateProductAsync(Product product)
        {
            return _database.UpdateAsync(product);
        }


        public Task<int> DeleteProductAsync(Product product)
        {
            
            return _database.DeleteAsync(product);

        }

        

    }
}

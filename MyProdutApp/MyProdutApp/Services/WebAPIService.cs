using MyProdutApp.Models;
using MyProdutApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProdutApp.Services
{
    public class WebAPIService : IWebAPIService
    {
        HttpClient client;
        string ApiUrl = "https://productapiccc.herokuapp.com/api/product/";
        public WebAPIService()
        {
            client = new HttpClient();
        }

        public async Task<List<Product>> RefreshDataAsync()
        {

            Uri uri = new Uri(string.Format(ApiUrl, string.Empty));


            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var Products = JsonConvert.DeserializeObject<List<Product>>(content);
                    return Products;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return null;

        }

        public async Task<Product> GetSingleProduct(string ID)
        {
            Uri uri = new Uri($"{ApiUrl}{ID}");

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var Products = JsonConvert.DeserializeObject<Product>(content);
                    return Products;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return null;
            
        }

        public async Task SaveProductAsync(Product product, bool ItemExist = false)
        {
            Uri uri = new Uri(string.Format(ApiUrl, string.Empty));
            //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string json = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (!ItemExist)
            {
                response = await client.PostAsync(uri, content);
            }
            if (ItemExist)
            {
                response = await client.PutAsync(uri, content);
            }
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully saved.");
            }

        }

        //public Task<int> UpdateProductAsync(Product product)
        //{
        //    return _database.UpdateAsync(product);
        //}


        public async Task DeleteProductAsync(Product product)
        {
            Uri uri = new Uri($"{ApiUrl}{product.Id}");
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\tTodoItem successfully deleted.");
            }

        }
    }
}

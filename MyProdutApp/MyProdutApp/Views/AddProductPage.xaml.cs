using MyProdutApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProdutApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
       
        public bool _UpdateMode { get; set; }
        public int _ProductToUpdate { get; set; }
        public AddProductPage()
        {
            InitializeComponent();
            _UpdateMode = false;
        }

        public AddProductPage(Product product)
        {
            InitializeComponent();

            if (product != null)
            {
                nameEntry.Text = product.Name;
                priceEntry.Text = product.Price.ToString();
                quantityEntry.Text = product.Quantity.ToString();
                SubmitBtn.Text = "Update Product";
                _ProductToUpdate = product.ID;
                _UpdateMode = true;
            }



        }

        async void AddOrUpdateProductButton(object sender, EventArgs e)
        {
            
            if(_UpdateMode)
            {
                await App.Database.UpdateProductAsync(new Product
                {
                    ID = _ProductToUpdate,
                    Name = nameEntry.Text,
                    Price = int.Parse(priceEntry.Text),
                    Quantity = int.Parse(quantityEntry.Text),
                });

                await Navigation.PushAsync(new ProductPage());
                
            }

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(priceEntry.Text) && !string.IsNullOrWhiteSpace(quantityEntry.Text) && !_UpdateMode)
            {
                await App.Database.SaveProductAsync(new Product
                {
                    Name = nameEntry.Text,
                    Price = int.Parse(priceEntry.Text),
                    Quantity = int.Parse(quantityEntry.Text),
                });

                nameEntry.Text = priceEntry.Text = quantityEntry.Text = string.Empty;
                await Navigation.PushAsync(new ProductPage());
            }
        }
    }
}
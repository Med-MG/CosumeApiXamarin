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
        public AddProductPage()
        {
            InitializeComponent();
        }

        async void AddProductButton(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(priceEntry.Text) && !string.IsNullOrWhiteSpace(quantityEntry.Text))
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
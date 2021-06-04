using MyProdutApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProdutApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
       
        public ProductPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = ChangeImageType(await App.Database.GetProducts());
            
        }

        private async void DeleteProduct(object sender, EventArgs e)
        {
            try
            {
                string ID = (sender as SwipeItem).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(ID))
                {
                    var product = await App.Database.GetSingleProduct(int.Parse(ID));
                    var result = await DisplayAlert("Confirm", "Do you want to delete Product: " + product.Name.ToString() + "?", "Yes", "No");
                    if (result)
                       await App.Database.DeleteProductAsync(product);
                       OnAppearing();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async void  UpdateProduct(object sender, EventArgs e)
        {
            try
            {
                string ID = (sender as SwipeItem).CommandParameter.ToString();
                if (!string.IsNullOrWhiteSpace(ID))
                {
                    var product = await App.Database.GetSingleProduct(int.Parse(ID));
                    //var result = await DisplayAlert("Confirm", "Product: " + product.Name + "?", "Yes", "No");
                    var UpdatePage = new AddProductPage(product);
                    await Navigation.PushAsync(UpdatePage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<ProductReturn> ChangeImageType(List<Product> ListProd)
        {
             List<ProductReturn> ProdToshow = new List<ProductReturn>();

            foreach (var product in ListProd)
            {
                ProdToshow.Add(new ProductReturn(product));
            }

            return ProdToshow;
        }

    }
}
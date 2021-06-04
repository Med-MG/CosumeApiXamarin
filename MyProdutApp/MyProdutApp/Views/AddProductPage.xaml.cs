using Android;
using Android.OS;
using MyProdutApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace MyProdutApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
       
        public bool _UpdateMode { get; set; }
        public int _ProductToUpdate { get; set; }
        public string _ImageFile { get; set; }

        public AddProductPage()
        {
            InitializeComponent();
            _UpdateMode = false;
            //RequestPermissions(permissionGroup, 0);
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

        readonly string[] permissionGroup =
{
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };


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
                    Image = _ImageFile
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
                    Image = _ImageFile
                });

                nameEntry.Text = priceEntry.Text = quantityEntry.Text = string.Empty;
                await Navigation.PushAsync(new ProductPage());
            }
        }

        private async void UploadImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Upload not supported on this device", "Cancel");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 40
            });
            
            _ImageFile = file.Path;

            // Convert file to byre array, to bitmap and set it to our ImageView

            //byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            //thisImageView.SetImageBitmap(bitmap);

        }
    }
}
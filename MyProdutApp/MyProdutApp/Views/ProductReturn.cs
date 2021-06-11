using MyProdutApp.Models;
using Xamarin.Forms;

namespace MyProdutApp.Views
{
    public class ProductReturn
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public ImageSource Image { get; set; }

        public ProductReturn(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Quantity = product.Quantity;
            this.Image = ImageSource.FromFile(product.Image);
        }
    }
}
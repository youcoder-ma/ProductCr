using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProductCrud.Models;
using ProductCrud.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductCrud.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {
        private readonly ProductService _service;
        private readonly bool _inUpdate;
        public int prodId;
        public AddProduct()
        {
            InitializeComponent();
            _service = new ProductService();
        }

        public AddProduct(Product products)
        {
            InitializeComponent();
            _service = new ProductService();
            if (products != null)
            {
                prodId = products.Id;
                name.Text = products.Name;
                description.Text = products.Description;
                quantity.Text = products.Quantity.ToString();
                price.Text = products.Price.ToString();
                disImage.Source = ImageSource.FromFile(products.Photo.Remove(0,6));
                _inUpdate = true;
            }
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(name.Text) && string.IsNullOrWhiteSpace(price.Text) && string.IsNullOrWhiteSpace(quantity.Text) && string.IsNullOrWhiteSpace(description.Text) && disImage.Source == null))
            {
                var data = new Product
                {
                    
                    Name = name.Text,
                    Price = Convert.ToDecimal(price.Text),
                    Description = description.Text,
                    Quantity = Convert.ToDecimal(quantity.Text),
                    Photo = disImage.Source.ToString().Remove(0, 6)

                };
                if (_inUpdate)
                {
                    data.Id = prodId;
                    await _service.UpdateProduct(data);
                    await DisplayAlert("Success", "Product updated Successfully", "OK");

                }
                else
                {
                    await _service.AddProduct(data);
                    await DisplayAlert("Success", "Product added Successfully", "OK");
                }
                //await this.Navigation.PopAsync();
                await this.Navigation.PushAsync(new Products());
            }
            else
            {
                await DisplayAlert("Warning", "Fields required", "OK");
                return;
            }
            
        }
        
        private async void Image_OnClicked(object sender, EventArgs e)
        {
            var cross = CrossMedia.Current;

            if (cross.IsPickPhotoSupported && cross.IsTakePhotoSupported)
            {
                var file = await cross.PickPhotoAsync();
                if (file == null) return;
                await DisplayAlert("Message", $"{file.Path}", "OK");
                disImage.Source = ImageSource.FromFile(file.Path);
                

            }
            else
            {
                await DisplayAlert("Alert", "There is no camera or is damaged", "OK");
                return;
            }
            

        }
        
        private async void TakeImage_OnClicked(object sender, EventArgs e)
        {
            var cross = CrossMedia.Current;
            if (cross.IsTakePhotoSupported && cross.IsCameraAvailable)
            {
                var file = await cross.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    Name = $"{DateTime.Today.ToShortDateString()}.png",
                    CompressionQuality = 75,
                    PhotoSize = PhotoSize.Medium,
                    SaveToAlbum = true,
                    AllowCropping = true,
                    Directory = "ProductCrud"
                });

                if (file != null)
                {
                    disImage.Source = ImageSource.FromFile(file.Path);
                }
                else
                {
                    await DisplayAlert("Alert", "There is no camera or is damaged", "OK");
                    return;
                }
            }
        }
    }
}
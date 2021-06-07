using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCrud.Models;
using ProductCrud.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductCrud.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
        ProductService _service;
        public Products()
        {
            InitializeComponent();
            _service = new ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetAllProducts();
        }

        public void GetAllProducts()
        {
            var prod = _service.GetProducts().Result;
            if (prod != null && prod.Count > 0)
            {
                lblInfo.Text = $"Total {prod.Count.ToString()} record(s) found";
            }
            else
            {
                lblInfo.Text = "No product records found. Please add one";
            }
            lstProduct.ItemsSource = prod;
        }


        private void BtnAddProduct_OnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddProduct());
        }

        private async void LstProduct_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Product products = (Product)e.SelectedItem;
                var action = await DisplayActionSheet("Operation", "Cancel", null, "Update");

                if (action == "Update")
                {
                   await this.Navigation.PushAsync(new AddProduct(products));
                }
                //else if (action == "Delete")
                //{
                //    await _service.DeleteProduct(products);
                //}

                //lstProduct.SelectedItem = null;
            }
        }

        private async void BtnDeleteProduct_OnClicked(object sender, EventArgs e)
        {
            var Id = (sender as Button).CommandParameter.ToString();
            int id = Convert.ToInt32(Id);
            //var prod = _service.GetttProducts().Where(x => x.Id.ToString() == Id);
            var pro = _service.GetProducts().Result.Where(x => x.Id.ToString() == Id);
            // var pros = _service.GetProductId(id);

            //var product = _service.GetProductId(id);
            var confirm = await DisplayAlert("Confirm",
                $"Do you want to delete the product {pro.FirstOrDefault().Name}?", "Yes", "No");
            if (confirm)
            {
                _service.DeleteProduct((Product)pro.FirstOrDefault());
                await DisplayAlert("Success", "Product Deleted Successfully", "OK");
                await this.Navigation.PushAsync(new Products());
            }
                

        }
    }
}
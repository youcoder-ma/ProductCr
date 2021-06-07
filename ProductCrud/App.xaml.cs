using System;
using ProductCrud.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductCrud
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Products());
        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}


using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    public partial class App : Application
    {
        public static string DtabaseLocation = string.Empty;
        public static MobileServiceClient MobileService = new MobileServiceClient("https://avsinventario.azurewebsites.net");
        public App()
        {
            InitializeComponent();
           // NavigationPage navPage = new NavigationPage(new MainPage());
            var navPage = new NavigationPage(new MainPage());
            MainPage = navPage;
        }

        public App(string filepath)
        {
            InitializeComponent();
            var navPage = new NavigationPage(new MainPage());
            MainPage = navPage;
            DtabaseLocation = filepath;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Escanear3 : ZXingScannerPage
    {
        NuevoProducto p;
        public Escanear3(NuevoProducto n)
        {
            InitializeComponent();
            p = n;
        }

        public void ScanPage(ZXing.Result result)
        {
            Boolean x;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                p.stringphoto = result.Text;
                await Navigation.PopAsync();
                
                //await DisplayAlert("","","oooo");
            });
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsScanning = false;
        }
    }
}
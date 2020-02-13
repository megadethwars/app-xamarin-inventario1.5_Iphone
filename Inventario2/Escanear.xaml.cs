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
    public partial class Escanear : ZXingScannerPage
    {
        Inventario s;
        public Escanear(Inventario i)
        {
            InitializeComponent();
            s = i;
        }

        public void ScanPage(ZXing.Result result)
        {
            Boolean x;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                s.stringcode = result.Text;
                s.cont = 1;
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
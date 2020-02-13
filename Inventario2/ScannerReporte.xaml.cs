using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerReporte : ZXingScannerPage
    {
        LevantarReporte reporte;
        public bool IsScanning = false;
        public ScannerReporte(LevantarReporte reporte)
        {
            InitializeComponent();
            IsScanning = false;
            this.reporte = reporte;
        }

        public void ScanPage(ZXing.Result result)
        {
            Boolean x;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                reporte.scanText = result.Text;
                reporte.isScanning = true;
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
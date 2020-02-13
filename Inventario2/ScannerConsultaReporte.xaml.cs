using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerConsultaReporte : ZXingScannerPage
    {
        public bool IsScanning = false;
        private ConsultarReporte reporte;
        public ScannerConsultaReporte(ConsultarReporte reporte)
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
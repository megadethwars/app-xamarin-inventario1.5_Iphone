using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Services;
using Inventario2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Escanear2 : ZXingScannerPage
    {
        List<InventDB> users1;
        RetirarProducto s;
        Plugin.Media.Abstractions.MediaFile f = null;
        public Escanear2(RetirarProducto r)
        {
            InitializeComponent();
            s = r;
        }

        public void ScanPage(ZXing.Result result)
        {
            Boolean boo = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                if (s.mv.Count > 0)
                {
                    for (int x = 0; x < s.mv.Count; x++)
                    {
                        if (!(s.mv[x].codigo == result.Text))
                        {
                            boo = true;
                        }
                        else
                            boo = false;
                    }
                    if (boo)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(result.Text);

                        buscar(result.Text);
                    }

                }
                else
                {


                    buscar(result.Text);
                }

                //await DisplayAlert("","","oooo");
            });
        }
        public async void buscar(string qr)
        {
            var devices = await DeviceService.getdevicebycode(qr);
            if (devices == null)
            {

                await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                return;
            }

            if (devices[0].statuscode == 500)
            {
                await DisplayAlert("Buscando", "error interno del servidor", "OK");
                return;
            }

            if (devices[0].statuscode == 404)
            {
                await DisplayAlert("Buscando", "producto no encontrado", "OK");
                return;
            }

            if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
            {
                Movimientos moves = new Movimientos
                {
                };
                s.Llenar(devices[0]);
                DependencyService.Get<IMessage>().ShortAlert(qr);
            }

            else
                DependencyService.Get<IMessage>().ShortAlert("Producto no Encontrado");
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
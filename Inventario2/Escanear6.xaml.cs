using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Escanear6 : ZXingScannerPage
    {
        IngresarProducto h;
        List<InventDB> users1;
        Plugin.Media.Abstractions.MediaFile f = null;
        public Escanear6(IngresarProducto t)
        {
            InitializeComponent();
            h = t;
        }
        public  void ScanPage(ZXing.Result result)
        {
            Boolean boo = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                if (h.movimientos.Count > 0)
                {
                    for (int x = 0; x < h.movimientos.Count; x++)
                    {
                        if (!(h.movimientos[x].codigo == result.Text))
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
            //users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == qr).ToListAsync();

            var devices = await DeviceService.getdevicebycode(qr);

            if (devices == null)
            {
                return;
            }

            if (devices[0].statuscode == 500)
            {
                return;
            }

            if (devices[0].statuscode == 404)
            {
                return;
            }

            if (devices[0].statuscode == 200)
            {
                ModelMovements mv1 = new ModelMovements
                {
                    ID = "",
                    observacionesMov = "Ninguna",
                    producto = devices[0].producto,
                    marca = devices[0].marca,
                    modelo = devices[0].modelo,
                    IDdevice = devices[0].ID,
                    codigo = devices[0].codigo,
                    serie = devices[0].serie,
                    cantidad = "1",
                    fotomov1 = "",
                    IDtipomov = 2,
                    IDlugar = 1
                    //fecha = DateTime.Now.ToString("dd/MM/yyyy")
                };
                h.movimientos.Add(mv1);
                h.f1.Add(f);
                h.f2.Add(f);
                DependencyService.Get<IMessage>().ShortAlert(qr);
            }

            
            
            
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
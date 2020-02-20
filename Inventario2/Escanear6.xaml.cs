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
        public void ScanPage(ZXing.Result result)
        {
            Boolean boo = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                if (h.mv.Count > 0)
                {
                    for (int x = 0; x < h.mv.Count; x++)
                    {
                        if (!(h.mv[x].codigo == result.Text))
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
            users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == qr).ToListAsync();
            if (users1.Count != 0)
            {
                Movimientos mv1 = new Movimientos
                {
                    ID = "",
                    observ = "Ninguna",
                    producto = users1[0].nombre,
                    marca = users1[0].marca,
                    modelo = users1[0].modelo,
                    IdProducto = users1[0].ID,
                    codigo = users1[0].codigo,
                    serie = users1[0].serie,
                    cantidad = "1",
                    foto = "",
                    movimiento = "Retirar",
                    lugar = " ",
                    fecha = DateTime.Now.ToString("dd/MM/yyyy")
                };
                h.mv.Add(mv1);
                h.f1.Add(f);
                h.f2.Add(f);
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
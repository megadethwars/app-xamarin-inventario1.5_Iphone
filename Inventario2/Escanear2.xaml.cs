﻿using System;
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
    public partial class Escanear2 : ZXingScannerPage
    {
        List<InventDB> users1;
        RetirarProducto s;
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
                    for(int x =0; x<s.mv.Count; x++)
                    {
                        if (!(s.mv[x].codigo == result.Text))
                        {
                            boo = true;
                        }
                        else
                            boo = false;
                    }
                    if(boo)
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
                s.mv.Add(mv1);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesCarrito2 : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile f;
        Plugin.Media.Abstractions.MediaFile f2;
        public IngresarProducto ca;
        public Movimientos ma;
        public DetallesCarrito2(Movimientos m, IngresarProducto r)
        {
            InitializeComponent();
            nameProd.Text = m.producto;
            marcatxt.Text = m.marca;
            modeltxt.Text = m.modelo;
            cantidadtxt.Text = m.cantidad;
            observtxt.Text = m.observ;
            ma = m;
            ca = r;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!(observ.Text == null))
            {
                ma.observ = observ.Text;
                observtxt.Text = observ.Text;
            }
            if (!(cantidad.Text == null))
            {
                ma.cantidad = cantidad.Text;
                cantidadtxt.Text = cantidad.Text;
            }
            if (observ.Text != null || cantidad.Text != null)
                DisplayAlert("Aceptar", "Producto Actualizado Correctamente", "Aceptar");
            if (f != null || f2 != null)
            {
                for (int x = 0; x < ca.mv.Count; x++)
                {
                    if (ca.mv[x].codigo == ma.codigo)
                        ca.f1[x] = f;
                    if (f2 != null)
                    {
                        if (ca.mv[x].codigo == ma.codigo)
                            ca.f2[x] = f2;
                    }

                }


                DisplayAlert("OK", "FOTO AGREGADA CORRECTAMENTE", "ACEPTAR");
            }

        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            int aux = 2;
            var lista = Navigation.NavigationStack;
            for (int x = 0; x < lista.Count; x++)
            {
                if (lista.Count > 3)
                {
                    if (x == 2)
                    {
                        x--;
                        Navigation.RemovePage(lista[aux]);
                    }

                }
            }
            Navigation.PopAsync();
        }
        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": No camera available", "OK");
                return;
            }

            f = await CrossMedia.Current.TakePhotoAsync(
              new Plugin.Media.Abstractions.StoreCameraMediaOptions
              {
                  Directory = "Sample",
                  RotateImage = false,

                  Name = "prueba" + ".jpg"
              });
            if (f == null)
                return;
            await DisplayAlert("File Location", f.Path, "OK");
            image1.Source = f.Path;
            image1.RotateTo(90);
            f.GetStream();
        }

        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": No camera available", "OK");
                return;
            }

            f2 = await CrossMedia.Current.TakePhotoAsync(
              new Plugin.Media.Abstractions.StoreCameraMediaOptions
              {
                  Directory = "Sample",
                  RotateImage = false,

                  Name = "prueba" + ".jpg"
              });
            if (f2 == null)
                return;
            await DisplayAlert("File Location", f2.Path, "OK");
            image2.Source = f2.Path;
            image2.RotateTo(90);
            f2.GetStream();
        }
    }
}
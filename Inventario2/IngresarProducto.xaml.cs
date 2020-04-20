using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresarProducto : ContentPage
    {
        public List<Plugin.Media.Abstractions.MediaFile> f1 = new List<Plugin.Media.Abstractions.MediaFile>();
        public List<Plugin.Media.Abstractions.MediaFile> f2 = new List<Plugin.Media.Abstractions.MediaFile>();
        Plugin.Media.Abstractions.MediaFile f = null;
        string p;
        public int cont;
        List<InventDB> users1;
        public List<ModelMovements> movimientos = new List<ModelMovements>();

        public string text;
        public IngresarProducto()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            search.Focus();
            if (movimientos.Count > 0)
            {
                for (int x = 0; x < movimientos.Count; x++)
                {
                    for (int y = x + 1; y < movimientos.Count; y++)
                    {
                        if (movimientos[x].codigo == movimientos[y].codigo)
                        {
                            movimientos.Remove(movimientos[y]);
                        }
                    }
                }
            }
            BotonCarrito.Text = "Carrito " + "(" + movimientos.Count.ToString() + ")";
            p = Guid.NewGuid().ToString("D");
            

        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": (No camera available.", "OK");
                return;
            }

            f = await CrossMedia.Current.TakePhotoAsync(
              new Plugin.Media.Abstractions.StoreCameraMediaOptions
              {
                  Directory = "Sample",

                  Name = "prueba.jpg"
              });
            if (f == null)
                return;
            await DisplayAlert("", "Foto Exitosa", "OK");


        }

        public async void busqueda()
        {
            if (search.Text.Length > 2)
            {
                string cadena = search.Text.Substring(search.Text.Length - 2);
                var isNumeric = long.TryParse(cadena, out long n);



                if (!isNumeric)
                {

                    var devices = await DeviceService.getdevicebyproduct(search.Text);
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
                        nombreTxt.Text = devices[0].producto;
                        modeloTxt.Text = devices[0].marca;
                        serietxt.Text = devices[0].serie;
                        pertenece.Text = devices[0].pertenece;
                        origentxt.Text = devices[0].origen;
                        Llenar(devices[0]);
                        BotonCarrito.Text = "Carrito " + "(" + movimientos.Count.ToString() + ")";
                    }

                    
                }
                else
                {

                    var devices = await DeviceService.getdevicebycode(search.Text);
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
                        //DisplayAlert("Buscando", "encontrado", "OK");
                        nombreTxt.Text = devices[0].producto;
                        modeloTxt.Text = devices[0].modelo;
                        serietxt.Text = devices[0].serie;
                        pertenece.Text = devices[0].pertenece;
                        origentxt.Text = devices[0].origen;
                       
                        if (!(devices[0].foto == ""))
                            foto.Source = devices[0].foto;
                        Llenar(devices[0]);
                        BotonCarrito.Text = "Carrito " + "(" + movimientos.Count.ToString() + ")";
                    }

                    
                }
            }
            else
                await DisplayAlert("Buscando", " no encontrado", "OK");
            search.Text = "";
        }
        private void SearchBar(object sender, EventArgs e)
        {
            busqueda();
        }

        private void Scan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear6(this));
            //Declarada en inventario Principal
        }

        private void Llenar(ModelDevice dev)
        {
            ModelMovements moves = new ModelMovements
            {
                
        };

            moves.IDmovimiento = "";
            moves.IDtipomov = 2;
            moves.IDusuario = Model.CurrentUser.ID;
            moves.IDdevice = dev.ID;
            moves.observacionesMov = "OK";
            moves.producto = dev.producto;
            moves.marca = dev.marca;
            moves.serie = dev.serie;
            moves.modelo = dev.modelo;
            moves.codigo = dev.codigo;
            movimientos.Add(moves);

            f1.Add(f);
            f2.Add(f);
            f = null;
        }




        private void RetiraP(object sender, EventArgs e)
        {


            Navigation.PushAsync(new Carrito2(this));


        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carrito2(this));
        }

        void ToolbarItem_Clicked_1(System.Object sender, System.EventArgs e)
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
    }
}
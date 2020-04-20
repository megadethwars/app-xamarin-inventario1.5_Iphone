using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using Inventario2.Models;
using Inventario2.Services;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RetirarProducto : ContentPage
    {
        public List<Plugin.Media.Abstractions.MediaFile> f1 = new List<Plugin.Media.Abstractions.MediaFile>();
        public List<Plugin.Media.Abstractions.MediaFile> f2 = new List<Plugin.Media.Abstractions.MediaFile>();
        Plugin.Media.Abstractions.MediaFile f = null;
        string p;
        public int cont;
        List<InventDB> users1;
        List<ModelDevice> devices;
        public List<ModelMovements> movimientos = new List<ModelMovements>();

        public List<Movimientos> mv = new List<Movimientos>();
        public Inventario inv;
        public string text;
        public RetirarProducto(Inventario i)
        {
            InitializeComponent();
            inv = i;


        }

        protected override void OnAppearing()
        {
            search.Focus();
            base.OnAppearing();
            if (movimientos.Count>0)
            {
                for(int x =0;x<movimientos.Count;x++)
                {
                    for(int y = x+1; y<movimientos.Count;y++)
                    {
                        if(movimientos[x].codigo == movimientos[y].codigo)
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


                    devices = await DeviceService.getdevicebyproduct(search.Text);
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

                    //users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.nombre == search.Text).ToListAsync();
                    
                    
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
                        modeloTxt.Text = devices[0].marca;
                        serietxt.Text = devices[0].serie;
                        pertenece.Text = devices[0].pertenece;
                        origentxt.Text = devices[0].origen;
                        marcaTxt.Text = devices[0].modelo;
                        obserb.Text = devices[0].observaciones;
                        if (!(devices[0].foto == ""))
                            foto.Source = devices[0].foto;
                        Llenar(devices[0]);
                        BotonCarrito.Text = "Carrito " + "(" + movimientos.Count.ToString() + ")";
                    } 


                    //users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == search.Text).ToListAsync();
                   
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
            Navigation.PushAsync(new Escanear2(this));
            //Declarada en inventario Principal
        }

        private void Llenar(ModelDevice device)
        {
            ModelMovements moves = new ModelMovements {
                
                
                
                //fecha = DateTime.Now.ToString("dd/MM/yyyy")
            };

            moves.IDmovimiento = "";
            moves.IDtipomov = 2;
            moves.IDusuario = Model.CurrentUser.ID;
            moves.IDdevice = device.ID;
            moves.observacionesMov = "OK";
            moves.producto = device.producto;
            moves.marca = device.marca;
            moves.serie = device.serie;
            moves.modelo = device.modelo;
            moves.codigo = device.codigo;
            movimientos.Add(moves);

            
            f2.Add(f);
            f1.Add(f);
            f = null;
        }




        private void RetiraP(object sender, EventArgs e)
        {


            Navigation.PushAsync(new Carrito(this));


        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carrito(this));
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
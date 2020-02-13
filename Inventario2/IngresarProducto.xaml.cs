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
    public partial class IngresarProducto : ContentPage
    {
        public List<Plugin.Media.Abstractions.MediaFile> f1 = new List<Plugin.Media.Abstractions.MediaFile>();
        public List<Plugin.Media.Abstractions.MediaFile> f2 = new List<Plugin.Media.Abstractions.MediaFile>();
        Plugin.Media.Abstractions.MediaFile f = null;
        string p;
        public int cont;
        List<InventDB> users1;
        public List<Movimientos> mv = new List<Movimientos>();

        public string text;
        public IngresarProducto()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (mv.Count > 0)
            {
                for (int x = 0; x < mv.Count; x++)
                {
                    for (int y = x + 1; y < mv.Count; y++)
                    {
                        if (mv[x].codigo == mv[y].codigo)
                        {
                            mv.Remove(mv[y]);
                        }
                    }
                }
            }
            BotonCarrito.Text = "Carrito " + "(" + mv.Count.ToString() + ")";
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
                    //SQLiteConnection conn = new SQLiteConnection(App.DtabaseLocation);
                    //conn.CreateTable<InventDB>();
                    //var users1 = conn.Query<InventDB>("select * from InventDB where Nombre= ?", search.Text);
                    //conn.Close();
                    users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.nombre == search.Text).ToListAsync();
                    if (users1.Count == 1)
                    {
                        //DisplayAlert("Buscando", "encontrado", "OK");
                        nombreTxt.Text = users1[0].nombre;
                        modeloTxt.Text = users1[0].marca;
                        serietxt.Text = users1[0].serie;
                        pertenece.Text = users1[0].pertenece;
                        origentxt.Text = users1[0].origen;
                        Llenar();
                        BotonCarrito.Text = "Carrito " + "(" + mv.Count.ToString() + ")";

                    }
                    else
                    {
                        await DisplayAlert("Buscando", "Producto no encontrado", "Aceptar");

                    }
                }
                else
                {

                    users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == search.Text).ToListAsync();
                    if (users1.Count != 0)
                    {
                        //DisplayAlert("Buscando", "encontrado", "OK");
                        nombreTxt.Text = users1[0].nombre;
                        modeloTxt.Text = users1[0].marca;
                        serietxt.Text = users1[0].serie;
                        pertenece.Text = users1[0].pertenece;
                        origentxt.Text = users1[0].origen;
                        if (!(users1[0].foto == ""))
                            foto.Source = users1[0].foto;
                        Llenar();
                        BotonCarrito.Text = "Carrito " + "(" + mv.Count.ToString() + ")";
                    }
                    else
                    {
                        await DisplayAlert("Buscando", " no encontrado", "OK");

                    }
                }
            }
            else
                await DisplayAlert("Buscando", " no encontrado", "OK");
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

        private void Llenar()
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
                movimiento = "Ingresar",
                lugar = " ",
                fecha = DateTime.Now.ToString("dd/MM/yyyy")
            };
            mv.Add(mv1);
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
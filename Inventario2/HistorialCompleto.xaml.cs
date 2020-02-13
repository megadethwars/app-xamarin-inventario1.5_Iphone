using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialCompleto : ContentPage
    {
        public string scantext;
        FormFieldsHistorial formfieldhistorial;
        ControllerHistorialCompleto controllerhistorial;
        public int contador;

        public HistorialCompleto()
        {
            InitializeComponent();
            /*
            desde.Date = DateTime.Now;
            desde.MinimumDate = new DateTime(2000, 1, 1);
            desde.MaximumDate = desde.Date;
            desde.DateSelected += desde_DateSelected;

            hasta.Date = DateTime.Now;
            hasta.MinimumDate = new DateTime(2000, 1, 1);
            hasta.MaximumDate = hasta.Date;
            hasta.DateSelected += hasta_DateSelected;
            */

            formfieldhistorial = new FormFieldsHistorial();
            formfieldhistorial.OnEventSender += new FormFieldsHistorial.ONFieldEventHandler(OnFieldEventAsync);
            controllerhistorial = new ControllerHistorialCompleto();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchbar.Text = scantext;
            if (contador == 1)
            {
                buscar();
                contador = 0;
            }
        }

        private void desde_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void hasta_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private async void IncludeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            /*
            if (includeSwitch.IsToggled)
            {
                TimeSpan span = hasta.Date - desde.Date;
                DateTime s = hasta.Date;
                
                List<Movimientos> fechas = new List<Movimientos>();
                for (int x = 0; x <= span.TotalDays; x++)
                {
                    var yesterday = s.AddDays(-x);
                    var fecha = await App.MobileService.GetTable<Movimientos>().Where(u => u.fecha == yesterday.ToString("dd/MM/yyyy")).ToListAsync();
                    for(int y =0;y<fecha.Count();y++)
                    {
                        fechas.Add(fecha[y]);
                    }
                    postListView.ItemsSource = fechas;
                }
            }
            */
        }
        public async void buscar()
        {
            if (searchbar.Text.Length > 2)
            {
                string cadena = searchbar.Text.Substring(searchbar.Text.Length - 2);
                var isNumeric = long.TryParse(cadena, out long n);



                if (!isNumeric)
                {
                    //SQLiteConnection conn = new SQLiteConnection(App.DtabaseLocation);
                    //conn.CreateTable<InventDB>();
                    //var users1 = conn.Query<InventDB>("select * from InventDB where Nombre= ?", search.Text);
                    //conn.Close();
                    var users1 = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == searchbar.Text).ToListAsync();
                    // var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == "hsjs").ToListAsync();
                    if (users1.Count != 0)
                    {
                        //DisplayAlert("Buscando", "encontrado", "OK");
                        postListView.ItemsSource = users1;
                    }
                    else
                    {
                        DisplayAlert("Buscando", "Producto no encontrado", "Aceptar");
                        postListView.ItemsSource = users1;
                    }
                }
                else
                {

                    var users1 = await App.MobileService.GetTable<Movimientos>().Where(u => u.codigo == searchbar.Text).ToListAsync();
                    if (users1.Count != 0)
                    {
                        //DisplayAlert("Buscando", "encontrado", "OK");
                        postListView.ItemsSource = users1;
                    }
                    else
                    {
                        DisplayAlert("Buscando", " no encontrado", "OK");
                        postListView.ItemsSource = users1;
                    }
                }
            }

        }

        private async void Search_SearchButtonPressed(object sender, EventArgs e)
        {
            buscar();
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear5(this));
        }

        private void Avanzada_clicked(object sender, EventArgs e)
        {
            Console.WriteLine("testing..");
            //Navigation.PushAsync(new FormFieldsHistorial());
            Navigation.PushAsync(formfieldhistorial);

        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            postListView.SelectedItem = null;
            var selectedPost = postListView.SelectedItem as Movimientos;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesHistorial(selectedPost));
        }

        private async void OnFieldEventAsync(object sender)
        {
            try
            {

                ModelHistorialCompleto modelhistorial = (ModelHistorialCompleto)sender;

                List<Movimientos> lista = await controllerhistorial.Search(modelhistorial);

                //List<Movimientos> lista = await MovementCase2(modelhistorial);  //finciono

                //List<Movimientos> lista = await ControllerHistorialCompleto.searching(modelhistorial);  funciono

                if (lista.Count == 0)
                {
                    await DisplayAlert("Buscando", "Producto no encontrado", "Aceptar");
                }

                postListView.ItemsSource = lista;

                Console.WriteLine("testing queries");
            }
            catch
            {

            }

        }












        private async Task<List<Movimientos>> MovementCase1(ModelHistorialCompleto modelhistorial)
        {

            // searching only by date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase2(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == modelhistorial.modelo).ToListAsync();
            Console.WriteLine("executing");
            return table;
        }

        private async Task<List<Movimientos>> MovementCase3(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == modelhistorial.modelo).
                Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase4(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase5(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase6(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase7(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date, product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase8(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase9(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase10(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase11(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and date, modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase12(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and producto
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase13(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto y fecha
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase14(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto y modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase15(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto  modelo date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.movimiento == modelhistorial.movimiento).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
                Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase16(ModelHistorialCompleto modelhistorial)
        {

            // searching only idproduct
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase17(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase18(ModelHistorialCompleto modelhistorial)
        {


            // searching only by idproduct and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase19(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  model, date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase20(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase21(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase22(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase23(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase24(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase25(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase26(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase27(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase28(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase29(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }


        private async Task<List<Movimientos>> MovementCase30(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase31(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and model  and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase32(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase33(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase34(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase35(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and model date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase36(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase37(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase38(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase39(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase40(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase41(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase42(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase43(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase44(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase45(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase46(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto and model 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase47(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto  model date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.movimiento == modelhistorial.movimiento).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase48(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase49(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.fecha == modelhistorial.fecha.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase50(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.modelo == modelhistorial.modelo).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase51(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase52(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase53(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase54(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase55(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase56(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase57(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase58(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase59(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase60(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase61(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product and Date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase62(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase63(ModelHistorialCompleto modelhistorial)
        {

            // searching by everithing, user,idproduct,movimiento, producto, id producto, date

            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.movimiento == modelhistorial.movimiento).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.fecha == modelhistorial.fecha.ToString()).ToListAsync();

            return table;
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

    }
}
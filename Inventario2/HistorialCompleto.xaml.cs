using Inventario2.Models;
using Inventario2.Services;
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
          
        }


        private async Task<List<ModelMovements>> queryDatabyproduct(string producto)
        {
            try
            {
                //var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.ID == IDsalida).ToListAsync();
                var listamoves = await MovementService.searchmovements(null, 0, 0, 0, producto, null, null, null, null, null);
                if (listamoves == null)
                {
                    await DisplayAlert("buscando", "error de conexion", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 500)
                {
                    await DisplayAlert("buscando", "error interno en el servidor", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 404)
                {
                    await DisplayAlert("buscando", "no encontrado", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 200 || listamoves[0].statuscode == 201)
                {
                    return listamoves;
                }

                return listamoves;

            }
            catch
            {
                return null;
            }
            // searching only idproduct


        }


        private async Task<List<ModelMovements>> queryDatabycode(string code)
        {
            try
            {
                //var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.ID == IDsalida).ToListAsync();
                var listamoves = await MovementService.searchmovements(null, 0, 0, 0, null, null, null, null, code, null);
                if (listamoves == null)
                {
                    await DisplayAlert("buscando", "error de conexion", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 500)
                {
                    await DisplayAlert("buscando", "error interno en el servidor", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 404)
                {
                    await DisplayAlert("buscando", "no encontrado", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 200 || listamoves[0].statuscode == 201)
                {
                    return listamoves;
                }

                return listamoves;

            }
            catch
            {
                return null;
            }
            // searching only idproduct


        }


        private async Task<List<ModelMovements>> search(ModelMovements paramshistorial)
        {
            try
            {
                //var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.ID == IDsalida).ToListAsync();



                var listamoves = await MovementService.searchmovements(paramshistorial.IDmovimiento, paramshistorial.IDtipomov, paramshistorial.IDlugar, paramshistorial.IDusuario,paramshistorial.producto , paramshistorial.fechamovimiento, paramshistorial.modelo, paramshistorial.marca,paramshistorial.codigo,paramshistorial.serie);
                if (listamoves == null)
                {
                    await DisplayAlert("buscando", "error de conexion", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 500)
                {
                    await DisplayAlert("buscando", "error interno en el servidor", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 404)
                {
                    await DisplayAlert("buscando", "no encontrado", "Aceptar");
                    return null;
                }

                if (listamoves[0].statuscode == 200 || listamoves[0].statuscode == 201)
                {
                    return listamoves;
                }

                return listamoves;

            }
            catch
            {
                return null;
            }
            // searching only idproduct


        }

        public async void buscar()
        {
            if (searchbar.Text.Length > 2)
            {
                string cadena = searchbar.Text.Substring(searchbar.Text.Length - 2);
                var isNumeric = long.TryParse(cadena, out long n);


                

                if (!isNumeric)
                {

                    var listamoves = await queryDatabyproduct(searchbar.Text);

                    if (listamoves == null)
                    {
                        postListView.ItemsSource = null;
                        return;
                    }

                    postListView.ItemsSource = listamoves;
              
                }
                else
                {
                    var listamoves = await queryDatabycode(searchbar.Text);


                    if (listamoves == null)
                    {
                        postListView.ItemsSource = null;
                        return;
                    }

                    postListView.ItemsSource = listamoves;
                    //var users1 = await App.MobileService.GetTable<Movimientos>().Where(u => u.codigo == searchbar.Text).ToListAsync();

                }
            }

        }

        private  void Search_SearchButtonPressed(object sender, EventArgs e)
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
            var selectedPost = postListView.SelectedItem as ModelMovements;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesHistorial(selectedPost));
        }

        private async void OnFieldEventAsync(object sender)
        {
            try
            {

                ModelMovements modelhistorial = (ModelMovements)sender;

                List<ModelMovements> lista = await search(modelhistorial);

                //List<Movimientos> lista = await MovementCase2(modelhistorial);  //finciono

                //List<Movimientos> lista = await ControllerHistorialCompleto.searching(modelhistorial);  funciono

                if (lista==null)
                {
                  
                    
                    postListView.ItemsSource = new List<ModelMovements>();

                }

                postListView.ItemsSource = lista;

                Console.WriteLine("testing queries");
            }
            catch
            {

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

    }
}
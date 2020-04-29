using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lugares : ContentPage
    {
        public Lugares()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var lugares = await GetLugares();

            if (lugares == null)
            {
                 return;
            }

            
             postListView.ItemsSource = lugares;
            

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(tbLugar.Text == "" || tbLugar.Text == null)
            {
                await DisplayAlert("error", "campo vacio", "Aceptar");

                return;
            }
            ModelLugares objLugar = new ModelLugares();
            objLugar.Lugar = tbLugar.Text;
            objLugar.message = "null";

            await PostLugar(JsonConvert.SerializeObject(objLugar));
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }


        private async Task<List<ModelLugares>>  GetLugares()
        {
            try
            {
                var tablalugares =await LugaresService.getlugares();
                    
                if(tablalugares==null)
                {
                    await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                    return null;
                }

                if (tablalugares[0].statuscode == 500)
                {
                    await DisplayAlert("error", "error de interno del servidor", "Aceptar");
                    return null;
                }

                if (tablalugares[0].statuscode == 401)
                {
                    await DisplayAlert("error", "error de peticion", "Aceptar");
                    return null;
                }

                if(tablalugares[0].statuscode==200 || tablalugares[0].statuscode ==201)
                {
                    return tablalugares;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<bool> PostLugar(string objeto)
        {
            try
            {
                var status = await LugaresService.postlugar(objeto);

                if (status == null)
                {
                    await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                    return false;
                }

                if (status.statuscode == 500)
                {
                    await DisplayAlert("error", "error de interno del servidor", "Aceptar");
                    return false;
                }

                if (status.statuscode == 401)
                {
                    await DisplayAlert("error", "error de peticion", "Aceptar");
                    return false;
                }

                if (status.statuscode == 409)
                {
                    await DisplayAlert("error", "conflicto, ya existe ese Destino", "Aceptar");
                    return false;
                }

                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    await DisplayAlert("Estado", "Creato correctamente", "Aceptar");
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> UpdateLugar(int id,string objeto)
        {
            try
            {
                var status = await LugaresService.putlugar(id,objeto);

                if (status == null)
                {
                    await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                    return false;
                }

                if (status.statuscode == 500)
                {
                    await DisplayAlert("error", "error de interno del servidor", "Aceptar");
                    return false;
                }

                if (status.statuscode == 401)
                {
                    await DisplayAlert("error", "error de peticion", "Aceptar");
                    return false;
                }

                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    await DisplayAlert("Estado", "Actualizado correctamente", "Aceptar");
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }




    }
}
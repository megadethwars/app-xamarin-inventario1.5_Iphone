using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inventario2.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empleado : ContentPage
    {
        public List<Usuario> users;
        ModelUser us;
        public Empleado(ModelUser u)
        {
            InitializeComponent();
            us = u;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
           // var usuarios = await App.MobileService.GetTable<Usuario>().ToListAsync();
            var usuarios = await UserService.getusers();
            if(usuarios[0].statuscode==200 || usuarios[0].statuscode == 201)
            {
                postListView.ItemsSource = usuarios;
            }
            else
            {
                await DisplayAlert("Buscando", "no encotntrados, error en servicio", "OK");
            }
            
        }
        private async void SearchBarEmp(object sender, EventArgs e)
        {
            var isNumeric = int.TryParse(search.Text,out int n);


            if (!isNumeric)
            {
               
                //var users1 = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == search.Text).ToListAsync();
                var users1 = await UserService.getuserbyname(search.Text);

                if (users1==null)
                {
                    await DisplayAlert("Buscando", "error de conexion con el servidor", "Aceptar");
                }


                if (users1.Count != 0)
                {
                    if (users1[0].statuscode==404)
                    {
                        await DisplayAlert("Buscando", "Usuario no encontrado", "Aceptar");
                        return;
                    }
                    else if(users1[0].statuscode == 500)
                    {
                        await DisplayAlert("Buscando", "error interno del servicio", "Aceptar");
                        return;
                    }

                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    await DisplayAlert("Buscando", "Usuario no encontrado", "Aceptar");
                    
                    var usuarios = await UserService.getusers();
                    if (usuarios[0].statuscode == 200 || usuarios[0].statuscode == 201)
                    {
                        postListView.ItemsSource = usuarios;
                    }
                    else
                    {
                        await DisplayAlert("Buscando", "no encotntrados, error en servicio", "OK");
                        return;
                    }
                 
                }
            }
            else
            {

                var users1 = await UserService.getuser(Int32.Parse(search.Text));

                if (users1 == null)
                {
                    await DisplayAlert("Buscando", "error de conexion con el servidor", "Aceptar");
                }


                if (users1.Count != 0)
                {
                    if (users1[0].statuscode == 404)
                    {
                        await DisplayAlert("Buscando", "Usuario no encontrado", "Aceptar");
                        return;
                    }
                    else if (users1[0].statuscode == 500)
                    {
                        await DisplayAlert("Buscando", "error interno del servicio", "Aceptar");
                        return;
                    }

                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    await DisplayAlert("Buscando", "Usuario no encontrado", "Aceptar");

                    var usuarios = await UserService.getusers();
                    if (usuarios[0].statuscode == 200 || usuarios[0].statuscode == 201)
                    {
                        postListView.ItemsSource = usuarios;
                    }
                    else
                    {
                        await DisplayAlert("Buscando", "no encotntrados, error en servicio", "OK");
                        return;
                    }

                }
            }

         }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as ModelUser;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesEmpleado(selectedPost));
        }

        private void AgregarEmp(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new AgregarEmpleado());

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

        private void search_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            
        }
    }
}
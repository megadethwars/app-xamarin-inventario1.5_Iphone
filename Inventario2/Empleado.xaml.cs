using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inventario2.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empleado : ContentPage
    {
        public List<Usuario> users;
        Usuario us;
        public Empleado(Usuario u)
        {
            InitializeComponent();
            us = u;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            var usuarios = await App.MobileService.GetTable<Usuario>().ToListAsync();
        
            postListView.ItemsSource = usuarios;
        }
        private async void SearchBarEmp(object sender, EventArgs e)
        {
            var isNumeric = int.TryParse(search.Text,out int n);


            if (!isNumeric)
            {
                //SQLiteConnection conn = new SQLiteConnection(App.DtabaseLocation);
                //conn.CreateTable<InventDB>();
                //var users1 = conn.Query<InventDB>("select * from InventDB where Nombre= ?", search.Text);
                //conn.Close();
                var users1 = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == search.Text).ToListAsync();
                if (users1.Count != 0)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    DisplayAlert("Buscando", "Usuario no encontrado", "Aceptar");
                    var usuarios = await App.MobileService.GetTable<Usuario>().ToListAsync();

                    postListView.ItemsSource = usuarios;
                }
            }
            else
            {

                var users1 = await App.MobileService.GetTable<Usuario>().Where(u => u.ID == search.Text).ToListAsync();
                if (users1.Count != 0)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    DisplayAlert("Buscando", " no encontrado", "OK");

                    var usuarios = await App.MobileService.GetTable<Usuario>().ToListAsync();

                    postListView.ItemsSource = usuarios;
                }
            }
            }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Usuario;
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

        async void search_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (search.Text == "")
            {
                var usuario = await App.MobileService.GetTable<Usuario>().ToListAsync();
                postListView.ItemsSource = usuario;
            }
        }
    }
}
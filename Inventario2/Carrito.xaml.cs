using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Inventario2.Model;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrito : ContentPage
    {

        public RetirarProducto re;
        int contador = 0;
        public Carrito(RetirarProducto r)
        {
            InitializeComponent();
            re = r;
            postListView.ItemsSource = re.mv;


        }
        protected override void OnDisappearing()
        {
            re.cont = 0;
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //re.mv.Clear();
            postListView.ItemsSource = null;
            postListView.ItemsSource = re.mv;


        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            postListView.SelectedItem = null;
            var selectedPost = postListView.SelectedItem as Movimientos;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesCarrito(selectedPost, re));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.BindingContext as Movimientos;
            int x = 0;
            for (int y = 0; y < re.mv.Count(); y++)
            {
                if (re.mv[y] == product)
                {
                    x = y;
                    break;
                }
            }
            if(re.f1==null)
            re.f1.Remove(re.f1[x]);
            re.mv.Remove(product);

            postListView.ItemsSource = null;
            postListView.ItemsSource = re.mv;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            re.mv.Clear();
            postListView.ItemsSource = null;
            postListView.ItemsSource = re.mv;
        }

        /* private async void Acept_Clicked(object sender, EventArgs e)
         {
             Boolean v = true;
             Boolean password = false;
             if (userEntry.Text != null && passEntry.Text != null)
             {
                 var usuarios = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == userEntry.Text).ToListAsync();
                 if (usuarios.Count() != 0)
                 {
                     for(int x=0;x<usuarios.Count();x++)
                     {
                         if(usuarios[x].contrasena==passEntry.Text)
                         {
                             password = true;
                             for (int y = 0; y < re.mv.Count(); y++)
                             {
                                 try
                                 {
                                     re.mv[y].usuario = usuarios[x].nombre + usuarios[x].apellido_paterno; 
                                     await App.MobileService.GetTable<Movimientos>().InsertAsync(re.mv[y]);
                                     //UploadFile(f.GetStream());
                                     //DisplayAlert("Agregado", re.mv.Count().ToString(), "Aceptar");
                                     //re.mv.Clear();
                                     //await Navigation.PopAsync();
                                     v = true;
                                     if (re.f1[y] != null)
                                         UploadFile(re.f1[y].GetStream(), re.mv[y].ID);

                                 }
                                 catch (MobileServiceInvalidOperationException ms)
                                 {
                                     var response = await ms.Response.Content.ReadAsStringAsync();
                                     await DisplayAlert("Error", response, "Aceptar");
                                     v = false;
                                     break;
                                 }
                             }
                             if (v)
                             {
                                 re.mv.Clear();
                                 re.f1.Clear();
                                 await DisplayAlert("Agregado", "Carrito Agregado correctamente", "Aceptar");
                                 await Navigation.PopAsync();
                             }
                         }
                     }
                     if(password==false)
                         DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");
                 }
                 else
                 {
                     DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");
                 }

             }
             else
             {
                 DisplayAlert("Error", "Usuario o contraseña no ingresado(s)", "Aceptar");
             }


         }
         */

        private async void UploadFile(Stream stream, string PathFoto)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=kS7YxHQSBtu6kDpa2sG7OVidbxcJq1Dip7+KnNjQA5SHn9N7loT2/Ul9HkdN0R5UPDWeKy0WpWQprGgnjIrbdA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("fotosinventario");
            await container.CreateIfNotExistsAsync();

            var block = container.GetBlockBlobReference($"{PathFoto}.jpg");
            await block.UploadFromStreamAsync(stream);
            string url = block.Uri.OriginalString;
            re.f1.Clear();
        }

        private void acept_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Confirmar(this));
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
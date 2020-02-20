using Inventario2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Inventario2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
           //this.BackgroundImageSource = "image1.jpeg";
            
        }


        private  void OnEnterPressed(object sender, EventArgs e)
        {
             IniciarSesion(null, null);
        }

        private async void IniciarSesion(object sender, EventArgs e)
        {
             Boolean password = false;
             try
             {
                 if (nameEntry.Text != null && passEntry.Text != null)
                 {
                     var usuarios = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == nameEntry.Text).ToListAsync();
                     if (usuarios.Count() != 0)
                     {
                         for (int x = 0; x < usuarios.Count(); x++)
                         {
                             if (usuarios[x].contrasena == passEntry.Text)
                             {
                                 password = true;
                                 await Navigation.PushAsync(new Menu(usuarios[x]));
                                 break;
                             }

                         }
                         if (password == false)
                             await DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");

                     }
                     else
                         await DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");
                 }
                 else
                     await DisplayAlert("Error", "Usuario o contraseña no ingresado(s)", "Aceptar");
             }
             catch
             {
                 await DisplayAlert("Error", "Error de Conexion con el Servidor", "Aceptar");
             }

             
            
        }
    }
}

using Inventario2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Inventario2.Services;
using Inventario2.Models;
using Newtonsoft.Json;

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
            
             try
             {
                 if (nameEntry.Text != null && passEntry.Text != null)
                 {
                    //var usuarios = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == nameEntry.Text).ToListAsync();
                    LoginUser logus = new LoginUser();
                    logus.nombre = nameEntry.Text;
                    logus.password = passEntry.Text;
                    var status = await UserService.loginAsync(JsonConvert.SerializeObject(logus));

                    if (status!=null)
                     {

                        if(status.statuscode == 404)
                        {
                            await DisplayAlert("Error", "Usuario no encontrado", "Aceptar");

                            return;
                        }

                        if(status.statuscode == 401)
                        {
                            await DisplayAlert("Error", "Usuario y/o contraseña incorrecto", "Aceptar");
                            return;
                        }

                        if (status.statuscode == 500)
                        {
                            await DisplayAlert("Error", "error en el servidor(bad request)", "Aceptar");
                            return;

                        }

                        if (status.statuscode==201 || status.statuscode ==200)
                        {
                            //query this user
                            var usuarios = await UserService.getuserbyname(logus.nombre);

                            if (usuarios == null)
                            {
                                await DisplayAlert("Error", "Error de conexion con el servidor", "Aceptar");

                                return;
                            }


                            if (usuarios[0].statuscode == 500)
                            {
                                await DisplayAlert("Error", "Error interno en el servidor", "Aceptar");
                                return;
                            }


                            logus.Dispose();                                              
                            await Navigation.PushAsync(new Menu(usuarios[0]));
                                    
                                                           
                        }
                                           
                     }
                     else
                         await DisplayAlert("Error", "Error de conexion con el servidor", "Aceptar");
                 }
                 else
                     await DisplayAlert("Error", "Usuario o contraseña no ingresado(s)", "Aceptar");
             }
             catch(Exception ex)
             {
                Console.WriteLine(ex.Message);
                 await DisplayAlert("Error", "Error de Conexion con el Servidor", "Aceptar");
             }

             
            
        }
    }
}

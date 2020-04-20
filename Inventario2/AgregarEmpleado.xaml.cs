using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Model;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Media;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Inventario2.Models;
using Inventario2.Services;

using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarEmpleado : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile f;
        public static MobileServiceClient client = new MobileServiceClient("https://inventarioavs.azurewebsites.net");
        public string identi;
        public string PathFoto;
        public int tipousuario;
        Stream stream;
        public  AgregarEmpleado()
        {
            InitializeComponent();
            identi = Guid.NewGuid().ToString();
          
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var roles = await RolesService.getroles();

            if (roles == null)
            {
                await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                return;
            }

            if (roles[0].statuscode==200 || roles[0].statuscode==201)
            {
                pickerUser.ItemsSource = roles;
            }

            

        }

        private void GenerateID(object sender, EventArgs e)
        {//Generar ID usando Data Binding y asignarlo a la variable idEmp

            //var idEmpleado = 04236; 
            //idEmp.Text= idEmpleado.ToString();
        }

        private async void AgregaEmp(object sender, EventArgs e)
        {
            if (nombrEntry.Text != "" && correoEntry.Text != "" && contraEntry.Text != "")
            {


                if (contra2.Text == contraEntry.Text)
                {

                    ModelUser user = new ModelUser
                    {
                        
                        nombre = nombrEntry.Text,
                        password = contraEntry.Text,
                        apellido_paterno = apepEntry.Text,
                        apellido_materno = apemEntry.Text,
                        IDtipoUsuario = tipousuario,
                        telefono = telEntry.Text,
                        correo = correoEntry.Text,
                        
                        //fechaContratacion = DateTime.Now.ToString("dd/MM/yyyy")
                    };
                    try
                    {

                      
                        
                        user.foto = PathFoto;

                        if (PathFoto == null)
                        {
                            user.foto = "foto pendiente";
                        }

                        var status = await UserService.postuser(JsonConvert.SerializeObject(user));
                        if (status.statuscode==400)
                        {
                            await DisplayAlert("error", "bad request", "Aceptar");
                            return;
                        }

                        if (status.statuscode == 409)
                        {
                            await DisplayAlert("error", "el usiario ya existe", "Aceptar");
                            return;
                        }

                        if (status.statuscode == 500)
                        {
                            await DisplayAlert("error", "error en el servicio", "Aceptar");
                            return;
                        }

                        if (status.statuscode == 200 || status.statuscode == 201)
                        {
                            await DisplayAlert("Agregado", "Usuario agregado correctamente", "Aceptar");
                            await Navigation.PopAsync();
                        }

                        


                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {
                        var response = await ms.Response.Content.ReadAsStringAsync();
                        await DisplayAlert("error", response, "Aceptar");
                    }
                }
                else
                    await DisplayAlert("Error", "Contraseña no coincide", "Aceptar");
            }
            else
                await DisplayAlert("Error","Faltan campos por Llenar","Aceptar");
        }

        private void PickerUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var modus = (ModelRoles)pickerUser.SelectedItem;
            tipousuario = modus.ID;
            modus.Dispose();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                stream.Close();
            }
            catch
            {

            }

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": No camera available", "OK");
                return;
            }

            f = await CrossMedia.Current.TakePhotoAsync(
              new Plugin.Media.Abstractions.StoreCameraMediaOptions
              {
                  Directory = "Sample",
                  RotateImage = false,

                  Name = nombrEntry.Text + ".jpg"
              });
            if (f == null)
                return;
            await DisplayAlert("File Location", f.Path, "OK");
            imagen.Source = f.Path;
            await imagen.RotateTo(90);
            stream = f.GetStream();

            converttobase64(stream);

            stream.Close();

        }

        private void converttobase64(Stream stream)
        {
            byte[] ImageData = new byte[stream.Length];
            stream.Read(ImageData, 0, System.Convert.ToInt32(stream.Length));
            string _base64String = Convert.ToBase64String(ImageData);
            PathFoto = _base64String;
        }

        private async void UploadFile(Stream stream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("fotosempleados");
            await container.CreateIfNotExistsAsync();



            var block = container.GetBlockBlobReference($"{identi}.jpg");
            await block.UploadFromStreamAsync(stream);
            string url = block.Uri.OriginalString;
            PathFoto = url;
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
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

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarEmpleado : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile f;
        public static MobileServiceClient client = new MobileServiceClient("https://inventarioavs.azurewebsites.net");
        public string identi;
        public string PathFoto;
        public string tipousuario;
        public AgregarEmpleado()
        {
            InitializeComponent();
            identi = Guid.NewGuid().ToString();


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

                    Usuario user = new Usuario
                    {
                        ID = identi,
                        nombre = nombrEntry.Text,
                        contrasena = contraEntry.Text,
                        apellido_paterno = apepEntry.Text,
                        apellido_materno = apemEntry.Text,
                        tipoUsuario = tipousuario,
                        telefono = telEntry.Text,
                        correo = correoEntry.Text,
                        fechaContratacion = DateTime.Now.ToString("dd/MM/yyyy")
                    };
                    try
                    {
                        await App.MobileService.GetTable<Usuario>().InsertAsync(user);
                        if (!(f == null))
                            UploadFile(f.GetStream());
                        await DisplayAlert("Agregado", "Usuario agregado correctamente", "Aceptar");
                        await Navigation.PopAsync();


                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {
                        var response = await ms.Response.Content.ReadAsStringAsync();
                        await DisplayAlert("error", response, "Aceptar");
                    }
                }
                else
                    DisplayAlert("Error", "Contraseña no coincide", "Aceptar");
            }
            else
                DisplayAlert("Error","Faltan campos por Llenar","Aceptar");
        }

        private void PickerUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipousuario = pickerUser.SelectedItem as string;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
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
            imagen.RotateTo(90);
            f.GetStream();
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Inventario2.Models;
using Inventario2.Services;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoProducto : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile f;
        public string PathFoto;
        public string stringphoto;
        string url = "not url";
        public static MobileServiceClient client = new MobileServiceClient("https://inventarioavs.azurewebsites.net");
        public NuevoProducto()
        {
            InitializeComponent();
            PathFoto = Guid.NewGuid().ToString("N");


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            codigoEntry.Text = stringphoto;

        }
        private async void AgregarP(object sender, EventArgs e)
        {
            
            ModelDevice device = new ModelDevice
            {
                //ID = PathFoto,
                codigo = codigoEntry.Text,
                producto = nameEntry.Text,
                marca = marca.Text,
                modelo = modelo.Text,
                costo = costo.Text,
                compra = compra.Text,
                serie = serie.Text,
                origen = origen.Text,
                descompostura = dec.Text,
                pertenece = pert.Text,
                IDlugar = 1,
                cantidad = cant.Text,
                observaciones = observ.Text,
                proveedor = proveedor.Text,
                foto = PathFoto + ".jpg",
                //Fecha = DateTime.Now.ToString("dd/MM/yyyy")
            };

            try
            {               
                    //await App.MobileService.GetTable<InventDB>().InsertAsync(invent);
                if (!(f == null))
                {
                    await UploadFile(f.GetStream());
                }

                device.foto = url;

                var status = await DeviceService.postdevice(JsonConvert.SerializeObject(device));
                if (status == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");

                    return;
                }

                if (status.statuscode == 500)
                {
                    await DisplayAlert("actualizando", "error interno del servidor", "OK");

                    return;
                }

                if (status.statuscode == 201)
                {
                    await DisplayAlert("Agregado", "Producto agregado correctamente", "Aceptar");
                    await Navigation.PopAsync();
                }

                


            }
            catch (MobileServiceInvalidOperationException ms)
            {
                var response = await ms.Response.Content.ReadAsStringAsync();
                await DisplayAlert("error", response, "Aceptar");
            }


        }
        private void Scan(object sender, EventArgs e)
        {
            //Declarada en Inventario Principal
            Navigation.PushAsync(new Escanear3(this));
        }

        private async void Foto_nuevop_Clicked(object sender, EventArgs e)
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

                  Name = nameEntry.Text + ".jpg"
              });
            if (f == null)
                return;
            await DisplayAlert("File Location", f.Path, "OK");
            imagen.Source = f.Path;
            f.GetStream();



        }

        private async Task<string>  UploadFile(Stream stream)
        {
            try
            {
                var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("fotosinventario");
                await container.CreateIfNotExistsAsync();



                var block = container.GetBlockBlobReference($"{PathFoto}.jpg");
                await block.UploadFromStreamAsync(stream);
                url = block.Uri.OriginalString;
                return url;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "not url";
            }

            
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var lista = Navigation.NavigationStack;
            for (int x = 0; x < lista.Count; x++)
            {
                if (x > 1 && x < lista.Count)
                {
                    Navigation.RemovePage(lista[x]);
                }
            }
            Navigation.PopAsync();
        }
    }
}
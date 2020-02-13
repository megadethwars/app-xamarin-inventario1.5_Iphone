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

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoProducto : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile f;
        public string PathFoto;
        public string stringphoto;
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
            InventDB invent = new InventDB
            {
                ID = PathFoto,
                codigo = codigoEntry.Text,
                nombre = nameEntry.Text,
                marca = marca.Text,
                modelo = modelo.Text,
                costo = costo.Text,
                compra = compra.Text,
                serie = serie.Text,
                origen = origen.Text,
                descompostura = dec.Text,
                pertenece = pert.Text,
                lugar = "Almacen",
                cantidad = cant.Text,
                observaciones = observ.Text,
                proveedor = proveedor.Text,
                foto = PathFoto + ".jpg",
                Fecha = DateTime.Now.ToString("dd/MM/yyyy")
            };

            try
            {
                await App.MobileService.GetTable<InventDB>().InsertAsync(invent);
                if (!(f == null))
                    UploadFile(f.GetStream());

                await DisplayAlert("Agregado", "Producto agregado correctamente", "Aceptar");
                await Navigation.PopAsync();


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

        private async void UploadFile(Stream stream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("fotosinventario");
            await container.CreateIfNotExistsAsync();



            var block = container.GetBlockBlobReference($"{PathFoto}.jpg");
            await block.UploadFromStreamAsync(stream);
            string url = block.Uri.OriginalString;
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
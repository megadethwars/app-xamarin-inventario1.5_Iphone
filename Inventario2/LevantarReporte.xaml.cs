using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevantarReporte : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile camara;
        public bool isScanning = false;
        public string scanText;
        private ModelDevice device;
        private ModelReport reporte;
        private string ID = Guid.NewGuid().ToString();

        private bool isFull = false;
        public string PathFoto;
        public string stringphoto;
        public LevantarReporte(string c)
        {
            reporte = new ModelReport();
            device = new ModelDevice();
            InitializeComponent();
            nombreID.Text = c;


        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            if (isScanning)
            {
                nombreID.Text = "";
                nombreID.Text = scanText;

                //search device
                List<ModelDevice> tabladevice = await QueryDevice(scanText);
                fillDevice(tabladevice);
                isScanning = false;

            }

        }

        private async void fillDevice(List<ModelDevice> tabla)
        {
            try
            {
                device = tabla[0];
                lbNombre.Text = device.producto;
                lbMarca.Text = device.marca;
                lbSerie.Text = device.serie;
                lbModelo.Text = device.modelo;
                lbAccDe.Text = device.pertenece;
                isFull = true;
            }
            catch
            {
                await DisplayAlert("No Product", "producto no encontrado", "OK");
                isFull = false;
            }


        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();


        }

        public  void Button_Clicked(object sender, System.EventArgs e)
        {


        }

        private async Task<bool> TakeFoto(string ID)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": No camera available", "OK");
                return false;
            }

            camara = await CrossMedia.Current.TakePhotoAsync(
              new Plugin.Media.Abstractions.StoreCameraMediaOptions
              {
                  Directory = "Sample",
                  RotateImage = true,
                  

                  Name = ID + ".jpg"
              });
            if (camara == null)
                return true;
            await DisplayAlert("File Location", camara.Path, "OK");
            imagen.Source = camara.Path;
            await imagen.RotateTo(90);
            camara.GetStream();

            return false;
        }

        private async void ScanFotos(object sender, EventArgs e)
        {
            await TakeFoto(Guid.NewGuid().ToString());
        }

        private void Scan(object sender, EventArgs e)
        { //Declarada en inventario principal
            Navigation.PushAsync(new ScannerReporte(this));
        }

        private  void OnEnterPressed(object sender, EventArgs e)
        {
           
        }
        private async void Enviar_Reporte(object sender, EventArgs e)
        {

            if (isFull)
            {
                reporte.foto2 = Guid.NewGuid().ToString();
                string id = reporte.foto2;
                reporte.codigo = device.codigo;
                reporte.marca = device.marca;
                reporte.serie = device.serie;
                reporte.modelo = device.modelo;
                reporte.producto = device.producto;
                reporte.comentario = editor.Text;
                reporte.IDreporte = id;
                PathFoto = id;
                reporte.IDdevice = device.ID;
                reporte.IDusuario = Model.CurrentUser.ID;
                bool res = await PostReport(reporte);

                //enviar foto
                if (camara != null)
                {
                    UploadFile(camara.GetStream());

                }

                editor.Text = "";
                if (res)
                {
                    await DisplayAlert("Mensaje", "Reporte subido correctamente", "OK");
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Mensaje", "No se encontro producto para enviar", "OK");
            }


        }

        private async Task<List<ModelDevice>> QueryDevice(string codigo)
        {

            try
            {
                //var table = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == codigo).ToListAsync();


                var table = await DeviceService.getdevicebycode(codigo);

                if (table == null)
                {
                    return null;
                }

                if (table[0].statuscode == 500)
                {
                    return null;
                }

                if (table[0].statuscode == 404)
                {
                    return null;
                }



                return table;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        private async Task<bool> PostReport(ModelReport reporte)
        {


            try
            {



                //await App.MobileService.GetTable<Model.Reportes>().InsertAsync(reporte);
                //device.observaciones = reporte.comentario;
                //await App.MobileService.GetTable<InventDB>().UpdateAsync(device);
                //return true;

                var status = await ReportService.postreport(JsonConvert.SerializeObject(reporte));
                if (status == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");

                    return false;
                }

                if (status.statuscode == 500)
                {
                    await DisplayAlert("actualizando", "error interno del servidor", "OK");

                    return false;
                }

                if (status.statuscode == 201)
                {
                    await DisplayAlert("Agregado", "Producto agregado correctamente", "Aceptar");
                    await Navigation.PopAsync();
                }

                device.observaciones = reporte.comentario;
                /////////////////////actualizar device
                ///
                status = await DeviceService.putdevice(device.ID,JsonConvert.SerializeObject(device));
                if (status == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");

                    return false;
                }

                if (status.statuscode == 500)
                {
                    await DisplayAlert("actualizando", "error interno del servidor", "OK");

                    return false;
                }

                if (status.statuscode == 201 || status.statuscode==200)
                {
                    await DisplayAlert("Agregado", "Producto actualizado correctamente", "Aceptar");
                    await Navigation.PopAsync();
                }

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await DisplayAlert("Error al subir comentarios", "error de post", "OK");
                return false;
            }

        }

        private async void UploadFile(Stream stream)
        {
            try
            {
                var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("fotosreporte");
                await container.CreateIfNotExistsAsync();



                var block = container.GetBlockBlobReference($"{PathFoto}.jpg");
                await block.UploadFromStreamAsync(stream);
                string url = block.Uri.OriginalString;
            }
            catch
            {
                await DisplayAlert("Error al subir imagen", "error de post", "OK");
            }

        }

        private void Consultar_Reporte(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultarReporte());
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void searchbar_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {
            if (nombreID != null)
            {
                List<ModelDevice> tabladevice = await QueryDevice(nombreID.Text);
                fillDevice(tabladevice);
            }
        }
    }
}
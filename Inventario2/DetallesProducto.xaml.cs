using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class DetallesProducto : ContentPage
    {
        public ModelDevice n;
        public DetallesProducto(ModelDevice db)
        {
            InitializeComponent();
            this.n = db;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            nameProd.Text = n.producto;
            idCodigo.Text = n.codigo;
            idcantidad.Text = n.cantidad;
            idlugar.Text = n.Lugar;
            idobserv.Text = n.observaciones;
            //idProd.Text = n.ID.ToString();
            idmarca.Text = n.marca;
            idcompra.Text = n.compra;
            idcosto.Text = n.costo;
            idorigen.Text = n.origen;
            idserie.Text = n.serie;
            iddesc.Text = n.descompostura;
            idpert.Text = n.pertenece;
            idFecha.Text = n.fecha;

            idmodelo.Text = n.modelo;
            imagen.Source = "https://fotosavs.blob.core.windows.net/fotosinventario/" + n.foto;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("¡Estas a punto de eliminar un Producto!, ¿Deseas continuar?", "Cancelar", null, "Eliminar Producto");
            switch (res)
            {
                case "Eliminar Producto":
                    //Eliminar empleado
                    try
                    {
                        //await App.MobileService.GetTable<InventDB>().DeleteAsync(n);
                        //var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
                       // var client = account.CreateCloudBlobClient();
                       // var container = client.GetContainerReference("fotosinventario");
                       // await container.CreateIfNotExistsAsync();
                       // var block = container.GetBlockBlobReference($"{n.foto}");
                       // await block.DeleteIfExistsAsync();
                       // await DisplayAlert("Hecho", "Producto borrado exitosamente", "Aceptar");
                       // await Navigation.PopAsync();
                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {

                        Console.WriteLine(ms.Message);
                       await  DisplayAlert("Error", "Error al borrar el producto", "Aceptar");

                    }
                        break;
                    
            }
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

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditarProducto(this));
        }
    }
}
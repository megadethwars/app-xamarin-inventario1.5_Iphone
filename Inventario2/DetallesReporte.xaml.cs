using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class DetallesReporte : ContentPage
    {
        private string urlImage = "";

        private Model.Reportes reporte;
        bool isImageOK;
        public DetallesReporte(Model.Reportes Reporte)
        {
            InitializeComponent();
            this.reporte = Reporte;
            nameProd.Text = reporte.producto;
            idcodigo.Text = reporte.codigo;
            idmarca.Text = reporte.marca;
            idmodelo.Text = reporte.modelo;
            idSerie.Text = reporte.serie;
            idobserv.Text = reporte.comentario;
            try
            {
                imagen.Source = "https://fotosavs.blob.core.windows.net/fotosreporte/" + reporte.foto + ".jpg";
                isImageOK = true;
            }
            catch
            {
                isImageOK = false;
            }



        }

        protected async override void OnAppearing()
        {
            if (!isImageOK)
            {
                await DisplayAlert("No image", "error al descargar imagen", "OK");
            }
        }

        private void OnAccept(object sender, EventArgs e)
        {
            Navigation.PopAsync();
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
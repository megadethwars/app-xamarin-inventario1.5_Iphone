using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
using Inventario2.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarReporte : ContentPage
    {
        Model.Reportes reporte;
        public bool isScanning = false;
        public string scanText;
        private bool isInt;
        public ConsultarReporte()
        {
            reporte = new Model.Reportes();
            isInt = false;
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            if (isScanning)
            {
                nombreID.Text = "";
                nombreID.Text = scanText;

                //search device
                // consulta de reportes y llenado de tabla
                List<ModelReport> listareportes = await QueryReport(scanText);



                if (listareportes.Count != 0)
                {
                    postListView.ItemsSource = listareportes;
                }
                else
                {
                    await DisplayAlert("Buscando", "Reportes de codigo encontrados", "Aceptar");
                }

                isScanning = false;

            }

        }


       

        private void Scan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScannerConsultaReporte(this));
        }

        private void PostListView_ItemSelected(object sender, EventArgs e)
        {
            postListView.SelectedItem = null;
            var selectedPost = postListView.SelectedItem as ModelReport;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesReporte(selectedPost));

        }

        private async Task<List<ModelReport>> QueryReport(string codigo)
        {

            try
            {
                //var table = await App.MobileService.GetTable<Model.Reportes>().Where(u => u.codigo == codigo).ToListAsync();
                var table = await ReportService.getreportbycode(codigo);


                if (table == null)
                {
                    await DisplayAlert("Buscando", "error de conexion", "Aceptar");
                    return null;
                }

                if (table[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "Reportes de codigo no encontrados", "Aceptar");
                    return null;
                }


                if (table[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "Reportes de codigo no encontrados", "Aceptar");
                    return null;
                }


                if (table[0].statuscode==201 || table[0].statuscode == 200)
                {
                    return table;
                }

                return table;
            
            
            
            }



            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await DisplayAlert("Buscando", "error de conexion", "Aceptar");
                return null;
            }

        }


        private async Task<List<ModelReport>> QueryReportByName(string nombre)
        {

            try
            {
                //var table = await App.MobileService.GetTable<Model.Reportes>().Where(u => u.codigo == codigo).ToListAsync();
                var table = await ReportService.getreportbyproduct(nombre);


                if (table == null)
                {
                    await DisplayAlert("Buscando", "error de conexion", "Aceptar");
                    return null;
                }

                if (table[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "Reportes de codigo no encontrados", "Aceptar");
                    return null;
                }


                if (table[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "Reportes de codigo no encontrados", "Aceptar");
                    return null;
                }


                if (table[0].statuscode == 201 || table[0].statuscode == 200)
                {
                    return table;
                }

                return table;



            }



            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await DisplayAlert("Buscando", "error de conexion", "Aceptar");
                return null;
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

        private async void nombreID_SearchButtonPressed(object sender, EventArgs e)
        {
            if (nombreID != null)
            {
                //consulta de reportes y llenado en tabla
                int length = nombreID.Text.Length - 2;
                string buscador = nombreID.Text.Substring(2, length);

                //tryparse
                try
                {
                    int testInt = Int32.Parse(buscador);
                    isInt = true;

                }
                catch
                {
                    isInt = false;
                }

                try
                {
                    if (isInt)
                    {
                        List<ModelReport> listareportes = await QueryReport(nombreID.Text);

                        if (listareportes == null)
                        {
                            return;
                        }

                        if (listareportes.Count != 0)
                        {
                            postListView.ItemsSource = listareportes;
                        }
                        else
                        {
                            //await DisplayAlert("Buscando", "Reportes de codigo encontrados", "Aceptar");
                        }
                    }
                    else
                    {
                        List<ModelReport> listareportes = await QueryReportByName(nombreID.Text);

                        if (listareportes == null)
                        {
                            return;
                        }

                        if (listareportes.Count != 0)
                        {
                            postListView.ItemsSource = listareportes;
                        }
                        else
                        {
                            //await DisplayAlert("Buscando", "Reportes de codigo encontrados", "Aceptar");
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.FilePicker;
using Syncfusion.XlsIO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;
using Newtonsoft;
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inventario : ContentPage
    {
        Plugin.FilePicker.Abstractions.FileData f;
        //string codigo;
        public List<InventDB> users;
        public string stringcode;
        public int cont = 0;
        public string tipoBusqueda;
        public Inventario()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            search.Text = stringcode;
            tipoBusqueda = pickerBuscar.SelectedItem as String;
            search.Focus();
            if (cont == 1)
            {
                //var busqueda = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == search.Text).ToListAsync();
                var busqueda = await DeviceService.getdevicebycode(search.Text);

                if (busqueda == null)
                {
                    return;
                }

                if (busqueda[0].statuscode==500)
                {
                    return;
                }

                if (busqueda[0].statuscode == 404)
                {
                    return;
                }

                if (busqueda[0].statuscode == 200)
                {
                    if (busqueda.Count != 0)
                        postListView.ItemsSource = busqueda;
                    else
                        //DependencyService.Get<IMessage>().ShortAlert("Producto no Encontrado");
                        cont = 0;
                }

                
            }
            else
            {
                //var usuario = await App.MobileService.GetTable<InventDB>().ToListAsync();
                var devices = await DeviceService.getdevices();

                if (devices == null)
                {
                    return;
                }

                if (devices[0].statuscode==500)
                {
                    return;
                }

                if (devices[0].statuscode==200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }

                
            }
            
            
        }

        
        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selectedPost = postListView.SelectedItem as ModelDevice;
                if (selectedPost != null)
                    Navigation.PushAsync(new DetallesProducto(selectedPost));
            }
            catch
            {

            }

            
        }

        public async void buscar()
        {
            string cadena = "";
            if (search.Text.Length > 3)
                cadena = search.Text.Substring(search.Text.Length - 3);
            var isNumeric = long.TryParse(cadena, out long n);


            if (tipoBusqueda=="producto")
            {
               
                var devices = await DeviceService.getdevicebyproduct(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode==200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }


                
            }
            if(tipoBusqueda=="QR")
            {

                var devices = await DeviceService.getdevicebycode(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }

            }
            if (tipoBusqueda == "modelo")
            {
                var devices = await DeviceService.getdevicebymodel(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }
            }


            if (tipoBusqueda == "marca")
            {
                var devices = await DeviceService.getdevicebymarca(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }
            }


            if (tipoBusqueda == "proveedor")
            {

                var devices = await DeviceService.getdevicebyprov(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }
            }
            if (tipoBusqueda == "serie")
            {

                var devices = await DeviceService.getdevicebyserie(search.Text);
                if (devices == null)
                {

                    await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 500)
                {
                    await DisplayAlert("Buscando", "error interno del servidor", "OK");
                    return;
                }

                if (devices[0].statuscode == 404)
                {
                    await DisplayAlert("Buscando", "producto no encontrado", "OK");
                    return;
                }

                if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                {
                    postListView.ItemsSource = devices;
                }
            }
        }
        private  void SearchBar(object sender, EventArgs e)
        {
            buscar();
        }

        private void Scan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear(this));
        }
        public async void Checar()
        {
            
            f = await CrossFilePicker.Current.PickFile();

            if (f != null)
            {
                var t = DisplayAlert("Excel",f.FileName,"aceptar");
                
                    ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, IsVisible = true, IsEnabled = true };
                    ExcelEngine excelEngine = new ExcelEngine();
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;


                    IWorkbook workbook = application.Workbooks.Open(f.GetStream());

                    //Access first worksheet from the workbook.
                    IWorksheet worksheet = workbook.Worksheets[1];
                    
                    
                    //Filas.
                    for (int x = 2; x < 1514; x++)
                    {
                        //columnas

                        var strs = worksheet.GetText(x, 2);

                        //list = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == strs).ToListAsync();


                        var devices = await DeviceService.getdevicebyserie(search.Text);
                        if (devices == null)
                        {

                            await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                            return;
                        }

                        if (devices[0].statuscode == 500)
                        {
                            await DisplayAlert("Buscando", "error interno del servidor", "OK");
                            return;
                        }

                        if (devices[0].statuscode == 404)
                        {
                            await DisplayAlert("Buscando", "producto no encontrado", "OK");

                            var id = Guid.NewGuid().ToString();
                            ModelDevice n = new ModelDevice
                            {

                                codigo = strs,
                                serie = worksheet.GetText(x, 1),
                                compra = worksheet.GetText(x, 8),
                                costo = worksheet.GetText(x, 6),
                                descompostura = worksheet.GetText(x, 11),
                                marca = worksheet.GetText(x, 4),
                                modelo = worksheet.GetText(x, 5),
                                producto = worksheet.GetText(x, 3),
                                observaciones = worksheet.GetText(x, 9),
                                origen = worksheet.GetText(x, 7),
                                pertenece = worksheet.GetText(x, 10),
                                //ID = id,
                                IDlugar = 1,
                            };

                            await DeviceService.postdevice(JsonConvert.SerializeObject(n));
                            //await App.MobileService.GetTable<InventDB>().InsertAsync(n);

                        
                        }

                        if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                        {
                            devices[0].compra = worksheet.GetText(x, 8);
                            devices[0].costo = worksheet.GetText(x, 6);
                            devices[0].descompostura = worksheet.GetText(x, 11);
                            devices[0].marca = worksheet.GetText(x, 4);
                            devices[0].modelo = worksheet.GetText(x, 5);
                            devices[0].producto = worksheet.GetText(x, 3);
                            devices[0].observaciones = worksheet.GetText(x, 9);
                            devices[0].origen = worksheet.GetText(x, 7);
                            devices[0].pertenece = worksheet.GetText(x, 10);                          
                            devices[0].serie = worksheet.GetText(x, 1);
                            await DeviceService.putdevice(devices[0].ID,JsonConvert.SerializeObject(devices[0]) );
                            //await App.MobileService.GetTable<InventDB>().UpdateAsync(list[0]);
                        }


                        

                    }
                    activityIndicator.IsRunning = false;
                
            }
            
        }

        private async void MenuOp(object sender, EventArgs e)
        { //Despegar menu de  3 opciones Ingresar, Retirar, Detalles
            string res = await DisplayActionSheet("Opciones", "Cancelar", null, "Agregar Nuevo Producto", "Reingresar Producto", "Salida","Actualizar BD");
            switch (res)
            {
                case "Agregar Nuevo Producto":
                    //Abrir vista/pagina Detalles del Producto
                    await Navigation.PushAsync(new NuevoProducto());
                    
                    break;
                case "Reingresar Producto":
                    //Abrir vista/pagina Ingresar Producto
                    await Navigation.PushAsync(new IngresarProducto(  ));
                    break;
                case "Salida":
                    //Abrir vista/pagina Retirar Producto
                    await Navigation.PushAsync(new RetirarProducto(this));
                    break;
                case "Actualizar BD":
                    await Navigation.PushAsync(new UpdateBD());
                    break;



            }
        }

        private void pickerBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoBusqueda = pickerBuscar.SelectedItem as string;
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void search_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if(search.Text=="")
            {

                //var usuario = await App.MobileService.GetTable<InventDB>().ToListAsync();
                var list = await DeviceService.getdevices();
                postListView.ItemsSource = list;
            }
        }
    }
}
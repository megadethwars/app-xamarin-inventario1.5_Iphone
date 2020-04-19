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
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateBD : ContentPage
    {
        Plugin.FilePicker.Abstractions.FileData f;
        bool isStopped = false;
        public UpdateBD()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            f = await CrossFilePicker.Current.PickFile();
            nombre.Text = f.FileName;
        }


        private void Button_detener(object sender, EventArgs e)
        {
            isStopped = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            isStopped = false;
            if (f != null)
            {



                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;


                IWorkbook workbook = application.Workbooks.Open(f.GetStream());

                //Access first worksheet from the workbook.
                IWorksheet worksheet = workbook.Worksheets[1];



                //Filas.
                int row = 2;

                while (!worksheet.GetValueRowCol(row, 2).Equals("") && worksheet.GetValueRowCol(row, 2) != null)
                {

                    if (isStopped)
                    {
                        porcent.Text = "Detenido";
                        break;
                    }

                    //columnas

                    var strs = worksheet.GetText(row, 1);

                    //list = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == strs).ToListAsync();


                    var devices = await DeviceService.getdevicebycode(strs);
                    if (devices == null)
                    {

                        await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");
                        porcent.Text = "Error de conexion";
                        break;
                    }

                    if (devices[0].statuscode == 500)
                    {
                        await DisplayAlert("Buscando", "error interno del servidor", "OK");
                        porcent.Text = "Error de sincronizacion";
                        break;
                    }

                    if (devices[0].statuscode == 404)
                    {
                        //await DisplayAlert("Buscando", "producto no encontrado", "OK");

                        //var id = Guid.NewGuid().ToString();
                        ModelDevice n = new ModelDevice
                        {

                            codigo = strs,
                            serie = (string)worksheet.GetValueRowCol(row, 2),
                            compra = worksheet.GetText(row, 8),
                            costo = worksheet.GetText(row, 6),
                            descompostura = worksheet.GetText(row, 11),
                            marca = worksheet.GetText(row, 4),
                            modelo = worksheet.GetText(row, 5),
                            producto = worksheet.GetText(row, 3),
                            observaciones = worksheet.GetText(row, 9),
                            origen = worksheet.GetText(row, 7),
                            pertenece = worksheet.GetText(row, 10),
                            //ID = id,
                            IDlugar = 1,

                        };

                        n.serie = n.serie.Replace('\x22', '\0');

                        await DeviceService.postdevice(JsonConvert.SerializeObject(n));
                        //await App.MobileService.GetTable<InventDB>().InsertAsync(n);


                    }

                    if (devices[0].statuscode == 200 || devices[0].statuscode == 201)
                    {
                        devices[0].compra = worksheet.GetText(row, 8);
                        devices[0].costo = worksheet.GetText(row, 6);
                        devices[0].descompostura = worksheet.GetText(row, 11);
                        devices[0].marca = worksheet.GetText(row, 4);
                        devices[0].modelo = worksheet.GetText(row, 5);
                        devices[0].producto = worksheet.GetText(row, 3);
                        devices[0].observaciones = worksheet.GetText(row, 9);
                        devices[0].origen = worksheet.GetText(row, 7);
                        devices[0].pertenece = worksheet.GetText(row, 10);
                        devices[0].serie = (string)worksheet.GetValueRowCol(row, 1);
                        devices[0].IDlugar = 1;
                        devices[0].serie = devices[0].serie.Replace('\x22', '\0');
                        await DeviceService.putdevice(devices[0].ID, JsonConvert.SerializeObject(devices[0]));
                        //await App.MobileService.GetTable<InventDB>().UpdateAsync(list[0]);
                    }



                    porcent.Text = worksheet.GetText(row, 1) + "  " + (row * 100 / 1576).ToString() + "%";
                    row = row + 1;
                }
                row = 2;
                porcent.Text = "terminado";

                /*
                for (int x = 2; x < 1577; x++)
                {
                    //columnas

                    var strs = worksheet.GetText(x, 1);
                    list = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == strs).ToListAsync();
                    if (list.Count != 0)
                    {
                        list[0].compra = worksheet.GetText(x, 8);
                        list[0].costo = worksheet.GetText(x, 6);
                        list[0].descompostura = worksheet.GetText(x, 11);
                        list[0].marca = worksheet.GetText(x, 4);
                        list[0].modelo = worksheet.GetText(x, 5);
                        list[0].nombre = worksheet.GetText(x, 3);
                        list[0].observaciones = worksheet.GetText(x, 9);
                        list[0].origen = worksheet.GetText(x, 7);
                        list[0].pertenece = worksheet.GetText(x, 10);
                        list[0].lugar = "Almacen";
                        list[0].serie = worksheet.GetText(x, 1);
                        await App.MobileService.GetTable<InventDB>().UpdateAsync(list[0]);
                    }
                    else
                    {
                        var id = Guid.NewGuid().ToString();
                        InventDB n = new InventDB
                        {

                            codigo = strs,
                            serie = worksheet.GetText(x, 1),
                            compra = worksheet.GetText(x, 8),
                            costo = worksheet.GetText(x, 6),
                            descompostura = worksheet.GetText(x, 11),
                            marca = worksheet.GetText(x, 4),
                            modelo = worksheet.GetText(x, 5),
                            nombre = worksheet.GetText(x, 3),
                            observaciones = worksheet.GetText(x, 9),
                            origen = worksheet.GetText(x, 7),
                            pertenece = worksheet.GetText(x, 10),
                            ID = id,
                            lugar = "Almacen"
                        };
                        await App.MobileService.GetTable<InventDB>().InsertAsync(n);
                    }
                    porcent.Text = worksheet.GetText(x, 1)+"  "+(x * 100 / 1576).ToString()+"%";

                }*/
                workbook.Close();
            }
        }
    }
}
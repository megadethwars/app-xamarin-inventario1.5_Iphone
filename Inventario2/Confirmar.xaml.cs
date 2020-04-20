using Inventario2.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
using Inventario2.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirmar : ContentPage
    {
        string p;
        public Carrito rp;
        private GeneratePDF pdf;
        //private InventDB CurrentDevice;
        private ModelDevice CurrentDevice;
        private bool isToggled;
        private ModelUser usuariosalida;
        public int idlugar;
        public Confirmar(Carrito x)
        {
            InitializeComponent();
            rp = x;
            p = Guid.NewGuid().ToString("D");
            pdf = new GeneratePDF();
            CurrentDevice = new ModelDevice();
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var lugares = await LugaresService.getlugares();

            if (lugares == null)
            {
                await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                return;
            }

            if (lugares[0].statuscode == 200 || lugares[0].statuscode == 201)
            {
                pickerLugar.ItemsSource = lugares;
            }

        }


        private void PickerLugar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lugarindex = (ModelLugares)pickerLugar.SelectedItem;
                idlugar = lugarindex.ID;
                lugarindex.Dispose();
            }
            catch
            {

            }
            
        }

        private async Task<bool> verifyuser(string user,string password)
        {
            LoginUser logus = new LoginUser();
            logus.nombre = user;
            logus.password = password;

            var status = await UserService.loginAsync(JsonConvert.SerializeObject(logus));

            if (status==null)
            {
                return false;
            }

            if (status.statuscode == 404)
            {
                await DisplayAlert("Error", "Usuario no encontrado", "Aceptar");

                return false;
            }

            if (status.statuscode == 401)
            {
                await DisplayAlert("Error", "Usuario y/o contraseña incorrecto", "Aceptar");
                return false;
            }

            if (status.statuscode == 500)
            {
                await DisplayAlert("Error", "error en el servidor(bad request)", "Aceptar");
                return  false;

            }

            if (status.statuscode == 201 || status.statuscode == 200)
            {
                //query this user
                var users = await UserService.getuserbyname(logus.nombre);

                if (users[0].statuscode == 500)
                {
                    await DisplayAlert("Error", "Error de conexion con el servidor", "Aceptar");
                    return false;
                }

                usuariosalida = users[0];
                logus.Dispose();
                users = null;
                return true;

            }

            return false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!isToggled)
            {
                Boolean v = true;
            
                if (Usuario.Text != null && Contra.Text != null)
                {
                    bool status = await verifyuser(Usuario.Text, Contra.Text);


                    //var usuarios = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == Usuario.Text).ToListAsync();
                    
                    
                    if (status)
                    {
                        isToggled = true;
                       

                        await UpdateLocations(rp.re.movimientos, idlugar);

                        for (int y = 0; y < rp.re.movimientos.Count(); y++)
                        {
                            try
                            {
                                rp.re.movimientos[y].IDmovimiento = p;
                                rp.re.movimientos[y].IDtipomov = 2;
                                rp.re.movimientos[y].IDusuario = usuariosalida.ID;
                                rp.re.movimientos[y].IDlugar = idlugar;
                                rp.re.movimientos[y].fotomov1 = p.Substring(15) + rp.re.movimientos[y].codigo + ".jpg";
                                rp.re.movimientos[y].fotomov2 = p.Substring(10) + rp.re.movimientos[y].codigo + "2.jpg";



                                var statusmove =await  MovementService.postmovement(JsonConvert.SerializeObject(rp.re.movimientos[y]));

                                if (statusmove == null)
                                {
                                    await DisplayAlert("Error", "error de conexion", "Aceptar");
                                    break;
                                }

                                if (statusmove.statuscode == 500)
                                {
                                    await DisplayAlert("Error", "interno del servidor", "Aceptar");
                                    break;
                                }

                                if (statusmove.statuscode != 201) {
                                    break;
                                }

                          
                                v = true;
                                if (rp.re.f1[y] != null)
                                    UploadFile(rp.re.f1[y].GetStream(), rp.re.movimientos[y].fotomov1);
                                if (rp.re.f2[y] != null)
                                    UploadFile(rp.re.f2[y].GetStream(), rp.re.movimientos[y].fotomov2);

                            }
                            catch (MobileServiceInvalidOperationException ms)
                            {
                                var response = await ms.Response.Content.ReadAsStringAsync();
                                await DisplayAlert("Error", response, "Aceptar");
                                v = false;
                                break;
                            }
                        }
                        if (v)
                        {
                            //agregar el pdf
                            rp.re.movimientos.Clear();
                            rp.re.f1.Clear();
                            rp.re.f2.Clear();
                            await DisplayAlert("Agregado", "Carrito Agregado correctamente", "Aceptar");
                            //await Navigation.PushAsync(new PDFMovement(p));
                            await pdf.InitPDFAsync(p);
                            ToolbarItem_Clicked(null, null);
                            //await Navigation.PopAsync();
                        }

                    }
                    else
                    {
                        return;
                    }                  
                  
                }
                else
                {
                    await DisplayAlert("Error", "Usuario o contraseña no ingresado(s)", "Aceptar");
                }
                
            }
            else
            {
                await DisplayAlert("Alerta", "Ya se han actualizado los productos de salida", "Aceptar");
            }

            

        }

        private async void UploadFile(Stream stream, string PathFoto)
        {
            try
            {
                var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=kS7YxHQSBtu6kDpa2sG7OVidbxcJq1Dip7+KnNjQA5SHn9N7loT2/Ul9HkdN0R5UPDWeKy0WpWQprGgnjIrbdA==;EndpointSuffix=core.windows.net");
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("fotossalida");
                await container.CreateIfNotExistsAsync();

                var block = container.GetBlockBlobReference($"{PathFoto}");
                await block.UploadFromStreamAsync(stream);
                string url = block.Uri.OriginalString;
            }
            catch
            {

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
                    if(x==2)
                    {
                        x--;
                        Navigation.RemovePage(lista[aux]);
                    }
                   
                }
            }
            Navigation.PopAsync();
        }

        private async Task UpdateLocations(List<ModelMovements> movimientos, int lugar)
        {
            foreach (ModelMovements movimiento in movimientos)
            {
                try
                {
                    var tabladevice = await DeviceService.getdevicebycode(movimiento.codigo);

                    if (tabladevice==null)
                    {
                        break;
                    }

                    if (tabladevice[0].statuscode==200)
                    {
                        CurrentDevice = tabladevice[0];
                        CurrentDevice.IDlugar = lugar;
                        //update

                        var status = await DeviceService.putdevice(CurrentDevice.ID,JsonConvert.SerializeObject(CurrentDevice));

                        if (status == null)
                        {
                            break;
                        }

                        
                    }


                    //var tablainventario = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == movimiento.codigo).ToListAsync();
                   
                }
                catch
                {

                }
            }
        }

    }
}
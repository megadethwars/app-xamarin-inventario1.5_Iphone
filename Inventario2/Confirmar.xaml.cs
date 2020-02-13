using Inventario2.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirmar : ContentPage
    {
        string p;
        public Carrito rp;
        private GeneratePDF pdf;
        private InventDB CurrentDevice;
        private bool isToggled;
        public Confirmar(Carrito x)
        {
            InitializeComponent();
            rp = x;
            p = Guid.NewGuid().ToString("D");
            pdf = new GeneratePDF();
            CurrentDevice = new InventDB();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!isToggled)
            {
                Boolean v = true;
                Boolean password = false;
                if (Usuario.Text != null && Contra.Text != null)
                {
                    var usuarios = await App.MobileService.GetTable<Usuario>().Where(u => u.nombre == Usuario.Text).ToListAsync();
                    if (usuarios.Count() != 0)
                    {
                        for (int x = 0; x < usuarios.Count(); x++)
                        {
                            if (usuarios[x].contrasena == Contra.Text)
                            {
                                isToggled = true;
                                password = true;
                                await UpdateLocations(rp.re.mv, Destino.Text);

                                for (int y = 0; y < rp.re.mv.Count(); y++)
                                {
                                    try
                                    {
                                        rp.re.mv[y].ID = p;
                                        rp.re.mv[y].usuario = usuarios[x].nombre;
                                        rp.re.mv[y].lugar = Destino.Text;
                                        rp.re.mv[y].foto = p.Substring(15) + rp.re.mv[y].codigo+".jpg";
                                        rp.re.mv[y].foto2 = p.Substring(10) + rp.re.mv[y].codigo + "2.jpg";
                                        await App.MobileService.GetTable<Movimientos>().InsertAsync(rp.re.mv[y]);
                                        //UploadFile(f.GetStream());
                                        //DisplayAlert("Agregado", re.mv.Count().ToString(), "Aceptar");
                                        //re.mv.Clear();
                                        //await Navigation.PopAsync();
                                        v = true;
                                        if (rp.re.f1[y] != null)
                                            UploadFile(rp.re.f1[y].GetStream(), rp.re.mv[y].foto);
                                        if (rp.re.f2[y] != null)
                                            UploadFile(rp.re.f2[y].GetStream(), rp.re.mv[y].foto2);

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
                                    rp.re.mv.Clear();
                                    rp.re.f1.Clear();
                                    rp.re.f2.Clear();
                                    await DisplayAlert("Agregado", "Carrito Agregado correctamente", "Aceptar");
                                    //await Navigation.PushAsync(new PDFMovement(p));
                                    await pdf.InitPDFAsync(p);
                                    ToolbarItem_Clicked(null, null);
                                    //await Navigation.PopAsync();
                                }
                            }
                        }
                        if (password == false)
                            await DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuario o contraseña incorrecto(s)", "Aceptar");
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
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=kS7YxHQSBtu6kDpa2sG7OVidbxcJq1Dip7+KnNjQA5SHn9N7loT2/Ul9HkdN0R5UPDWeKy0WpWQprGgnjIrbdA==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("fotossalida");
            await container.CreateIfNotExistsAsync();

            var block = container.GetBlockBlobReference($"{PathFoto}");
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
                    if(x==2)
                    {
                        x--;
                        Navigation.RemovePage(lista[aux]);
                    }
                   
                }
            }
            Navigation.PopAsync();
        }

        private async Task UpdateLocations(List<Movimientos> movimientos, string lugar)
        {
            foreach (Movimientos movimiento in movimientos)
            {
                try
                {
                    var tablainventario = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == movimiento.codigo).ToListAsync();
                    if (tablainventario.Count != 0)
                    {
                        CurrentDevice = tablainventario[0];
                        CurrentDevice.lugar = lugar;

                        //update
                        await App.MobileService.GetTable<InventDB>().UpdateAsync(CurrentDevice);

                    }
                }
                catch
                {

                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
using Inventario2.Services;
using Newtonsoft.Json;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesEmpleado : ContentPage
    {
        ModelUser usuario;
        public DetallesEmpleado(ModelUser user)
        {
            InitializeComponent();
            this.usuario = user;
            nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno + " " + usuario.apellido_materno;

            fechaCont.Text = usuario.fecha;
            tipoUs.Text = usuario.tipousuario;
            telText.Text = usuario.telefono;
            correotext.Text = usuario.correo;
        }

        private async void EliminaEmp(object sender, EventArgs e)
        {
            
            if(usuario.ID == CurrentUser.ID)
            {
                await DisplayAlert("Error", "No puedes borrar el usuario actual", "Aceptar");

            }

            string res = await DisplayActionSheet("¡Estas a punto de eliminar un Empleado!, ¿Deseas continuar?", "Cancelar", "Eliminar Empleado");
            switch (res)
            {
                case "Eliminar Empleado":
                    //Eliminar empleado
                    try
                    {
                        //await App.MobileService.GetTable<Usuario>().DeleteAsync(usuario);
                        //var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
                        //var client = account.CreateCloudBlobClient();
                        //var container = client.GetContainerReference("fotosempleados");
                        //await container.CreateIfNotExistsAsync();
                        //var foto = usuario.ID + ".jpg";
                        //var block = container.GetBlockBlobReference($"{foto}");
                        //await block.DeleteIfExistsAsync();

                        bool resp = await Deleteuser(usuario.ID);

                        //await DisplayAlert("Hecho", "Usuario borrado exitosamente", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {


                        await DisplayAlert("Error", "Error al borrar el producto", "Aceptar");

                    }
                    break;

            }
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                if (correoChange.Text != null)
                    usuario.correo = correoChange.Text;
                if (changetel.Text != null)
                    usuario.telefono = changetel.Text;
                if (changename.Text != null)
                    usuario.nombre = changename.Text;
                if (correoChange.Text != null || changetel.Text != null || changename.Text != null || changecontra.Text != null)
                {
                    if (changecontra.Text != null && changecontra2.Text != null && changecontra.Text != "" && changecontra2.Text != "")
                    {
                        

                        if (changecontra.Text == changecontra2.Text)
                        {


                            usuario.password = changecontra.Text;
                            bool res = await Updateuser(usuario.ID, JsonConvert.SerializeObject(usuario));

                            if (!res)
                            {
                                return;
                            }

                            res = await UpdatePasword(usuario.ID, JsonConvert.SerializeObject(usuario));

                            if (!res)
                            {
                                return;
                            }

                            //await App.MobileService.GetTable<Usuario>().UpdateAsync(usuario);
                            if (res)
                            {
                                await DisplayAlert("ACTUALIZAR", "DATOS Y CONTRASEÑA ACTUALIZADOS CORRECTAMENTE", "ACEPTAR");

                            }
                            nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno + " " + usuario.apellido_materno;
                            telText.Text = usuario.telefono;
                            correotext.Text = usuario.correo;

                        }
                        else
                            await DisplayAlert("ERROR", "Contraseñas no coinciden", "ACEPTAR");

                    }
                    else
                    {

                        bool res = await Updateuser(usuario.ID, JsonConvert.SerializeObject(usuario));

                        //await App.MobileService.GetTable<Usuario>().UpdateAsync(usuario);

                        if (res)
                        {
                            await DisplayAlert("ACTUALIZAR", "DATOS ACTUALIZADOS CORRECTAMENTE", "ACEPTAR");

                        }

                        telText.Text = usuario.telefono;
                        correotext.Text = usuario.correo;
                        nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno + " " + usuario.apellido_materno;
                    }
                }
                else
                    await DisplayAlert("ERROR", "NO HAY DATOS PARA ACTUALIZAR", "ACEPTAR");
            }
            catch (MobileServiceInvalidOperationException ms)
            {


                await DisplayAlert("Error", "Error al actualizar el producto", "Aceptar");

            }
            
        }

        private async Task<bool> UpdatePasword(int id,string userstr)
        {
            try
            {
                var upduser = await UserService.putpass(id, userstr);

                if (upduser == null)
                {
                    await DisplayAlert("Error", "Error de conexion al servidor", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 500)
                {
                    await DisplayAlert("Error", "Error interno en el servidor", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 404)
                {
                    await DisplayAlert("Error", "No encontrado", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 200)
                {
                    await DisplayAlert("Mensaje", "Actualizado correctamente", "Aceptar");
                    return true;
                }


                //var tablainventario = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == movimiento.codigo).ToListAsync();

            }
            catch
            {
                return false;
            }


            return false;
        }

        
        private async Task<bool> Updateuser(int id,string userstr)
        {
            
            try
            {
                var upduser = await UserService.putuser(id,userstr);

                if (upduser == null)
                {
                    await DisplayAlert("Error", "Error de conexion al servidor", "Aceptar");
                    return false;
                }

                if (upduser.statuscode==500)
                {
                    await DisplayAlert("Error", "Error interno en el servidor", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 404)
                {
                    await DisplayAlert("Error", "No encontrado", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 409)
                {
                    await DisplayAlert("Error", "Usuario ya existente", "Aceptar");
                    return false;
                }

                if (upduser.statuscode == 200)
                {
                    await DisplayAlert("Mensaje", "Actualizado correctamente", "Aceptar");
                    return true;
                }


                //var tablainventario = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == movimiento.codigo).ToListAsync();

            }
            catch
            {
                return false;
            }
            

            return false;
        }

        private async Task<bool> Deleteuser(int id)
        {
            try
            {
                var deluser = await UserService.deleteUser(id);

                if (deluser == null)
                {
                    await DisplayAlert("Error", "Error de conexion al servidor", "Aceptar");
                    return false;
                }

                if (deluser.statuscode == 500)
                {
                    await DisplayAlert("Error", "Error interno en el servidor", "Aceptar");
                    return false;
                }

                if (deluser.statuscode == 404)
                {
                    await DisplayAlert("Error", "No encontrado", "Aceptar");
                    return false;
                }

                if (deluser.statuscode == 409)
                {
                    await DisplayAlert("Error", "Conflicto al borrar Usuario", "Aceptar");
                    return false;
                }

                if (deluser.statuscode == 200 || deluser.statuscode == 201)
                {
                    await DisplayAlert("Mensaje", "Borrado correctamente", "Aceptar");
                    return true;
                }


                //var tablainventario = await App.MobileService.GetTable<InventDB>().Where(u => u.codigo == movimiento.codigo).ToListAsync();

            }
            catch
            {
                return false;
            }


            return false;
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
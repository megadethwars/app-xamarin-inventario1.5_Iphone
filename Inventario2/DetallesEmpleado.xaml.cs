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

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesEmpleado : ContentPage
    {
        Usuario usuario;
        public DetallesEmpleado(Usuario user)
        {
            InitializeComponent();
            this.usuario = user;
            nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno + " " + usuario.apellido_materno;

            fechaCont.Text = usuario.fechaContratacion;
            tipoUs.Text = usuario.tipoUsuario;
            telText.Text = usuario.telefono;
            correotext.Text = usuario.correo;
        }

        private async void EliminaEmp(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("¡Estas a punto de eliminar un Empleado!, ¿Deseas continuar?", "Cancelar", null, "Eliminar Empleado");
            switch (res)
            {
                case "Eliminar Empleado":
                    //Eliminar empleado
                    try
                    {
                        await App.MobileService.GetTable<Usuario>().DeleteAsync(usuario);
                        var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=fotosavs;AccountKey=NLazg0RjiUxSF9UvkeSWvNYicNDSUPn4IoXp4KSKXx0qe+W2bt40BrGFK6M+semkKHHOV5T4Ya2eNKDDQNY57A==;EndpointSuffix=core.windows.net");
                        var client = account.CreateCloudBlobClient();
                        var container = client.GetContainerReference("fotosempleados");
                        await container.CreateIfNotExistsAsync();
                        var foto = usuario.ID + ".jpg";
                        var block = container.GetBlockBlobReference($"{foto}");
                        await block.DeleteIfExistsAsync();
                        

                        await DisplayAlert("Hecho", "Usuario borrado exitosamente", "Aceptar");
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
                    if (changecontra.Text != null && changecontra2.Text != null)
                    {
                        if (changecontra.Text == changecontra2.Text)
                        {
                            usuario.contrasena = changecontra.Text;
                            await App.MobileService.GetTable<Usuario>().UpdateAsync(usuario);
                            await DisplayAlert("ACTUALIZAR", "DATOS Y CONTRASEÑA ACTUALIZADOS CORRECTAMENTE", "ACEPTAR");
                            nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno + " " + usuario.apellido_materno;
                            telText.Text = usuario.telefono;
                            correotext.Text = usuario.correo;

                        }
                        else
                            await DisplayAlert("ERROR", "Contraseñas no coinciden", "ACEPTAR");

                    }
                    else
                    {
                        await App.MobileService.GetTable<Usuario>().UpdateAsync(usuario);
                        await DisplayAlert("ACTUALIZAR", "DATOS ACTUALIZADOS CORRECTAMENTE", "ACEPTAR");
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
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Inventario2.Models;
using Inventario2.Services;
using Newtonsoft.Json;

namespace Inventario2
{
    public partial class EditarProducto : ContentPage
    {
        DetallesProducto pro;
        ModelDevice pr;
        
        public EditarProducto(DetallesProducto nu)
        {
            InitializeComponent();
            pr = nu.n;
            pro = nu;
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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (idProducto.Text != "")
                pr.producto = idProducto.Text;
            if(idMarca.Text != "")
                pr.marca = idMarca.Text;
            if (idModelo.Text != "")
                pr.modelo = idModelo.Text;
            if (idSerie.Text != "")
                pr.serie = idSerie.Text;
            if (idCodigo.Text != "")
                pr.codigo = idCodigo.Text;
            if (idOrigen.Text != "")
                pr.origen = idOrigen.Text;
            if (idCosto.Text != "")
                pr.costo= idCosto.Text;
            if (idProveedor.Text != "")
                pr.proveedor = idProveedor.Text;
            if (idCompra.Text != "")
                pr.compra= idCompra.Text;           
            if (idAcces.Text != "")
                pr.pertenece = idAcces.Text;
            if (idDesc.Text != "")
                pr.descompostura = idDesc.Text;
            if (idProducto.Text != "" || idMarca.Text != "" || idModelo.Text != "" || idSerie.Text != "" || idCodigo.Text != "" || idOrigen.Text != "" || idCosto.Text != "" || idProveedor.Text != "" || idCompra.Text != "" || idAcces.Text != "" || idDesc.Text != "")
            {
                try
                {
                    var status = await DeviceService.putdevice(pr.ID, JsonConvert.SerializeObject(pr));
                    if (status == null)
                    {

                        await DisplayAlert("Buscando", "error de conexion con el servidor", "OK");

                        return ;
                    }

                    if (status.statuscode == 500)
                    {
                        await DisplayAlert("actualizando", "error interno del servidor", "OK");

                        return ;
                    }

                    if (status.statuscode == 201 || status.statuscode == 200)
                    {
                        await DisplayAlert("Agregado", "Producto actualizado correctamente", "Aceptar");
                        
                    }





                    //await App.MobileService.GetTable<InventDB>().UpdateAsync(pr);
                    await DisplayAlert("OK", "PRODUCTO EDITADO CORRECTAMENTE", "ACEPTAR");
                    pro.n = pr;
                    await Navigation.PopAsync();
                }
                catch (MobileServiceInvalidOperationException ms)
                {

                    Console.WriteLine(ms.Message);
                    await DisplayAlert("Error", "Error al actualizar", "Aceptar");

                }
            }
            else
                await DisplayAlert("Error", "Faltan campos por llenar","Aceptar");

        }
    }
}

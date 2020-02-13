using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace Inventario2
{
    public partial class EditarProducto : ContentPage
    {
        DetallesProducto pro;
        InventDB pr;
        
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
                pr.nombre = idProducto.Text;
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
                    await App.MobileService.GetTable<InventDB>().UpdateAsync(pr);
                    DisplayAlert("OK", "PRODUCTO AGREGADO CORRECTAMENTE", "ACEPTAR");
                    pro.n = pr;
                    Navigation.PopAsync();
                }
                catch (MobileServiceInvalidOperationException ms)
                {


                    await DisplayAlert("Error", "Error al actualizar", "Aceptar");

                }
            }
            else
                DisplayAlert("Error", "Faltan campos por llenar","Aceptar");

        }
    }
}

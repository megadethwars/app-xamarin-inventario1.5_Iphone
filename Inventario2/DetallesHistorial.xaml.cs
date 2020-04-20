using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Models;
namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesHistorial : ContentPage
    {
        public DetallesHistorial(ModelMovements mv)
        {
            InitializeComponent();
            nameUser.Text = mv.nombre;
            nameProd.Text = mv.producto;
            idCodigo.Text = mv.codigo;
            idmarca.Text = mv.marca;
            idmodelo.Text = mv.modelo;
            idcantidad.Text = mv.cantidad;
            idobserv.Text = mv.observacionesMov;
            idlugar.Text = mv.Lugar;
            idMove.Text = mv.tipomovimiento;
            idFecha.Text = mv.fechamovimiento;
            try
            {
                imagen.Source = "https://fotosavs.blob.core.windows.net/fotossalida/" + mv.fotomov1;
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
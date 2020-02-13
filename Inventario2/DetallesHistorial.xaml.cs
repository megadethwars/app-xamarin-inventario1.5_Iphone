using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesHistorial : ContentPage
    {
        public DetallesHistorial(Movimientos mv)
        {
            InitializeComponent();
            nameUser.Text = mv.usuario;
            nameProd.Text = mv.producto;
            idCodigo.Text = mv.codigo;
            idmarca.Text = mv.marca;
            idmodelo.Text = mv.modelo;
            idcantidad.Text = mv.cantidad;
            idobserv.Text = mv.observ;
            idlugar.Text = mv.lugar;
            idMove.Text = mv.movimiento;
            idFecha.Text = mv.fecha;
            imagen.Source = "https://fotosavs.blob.core.windows.net/fotossalida/"+mv.foto;

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
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
    public partial class Ajustes : ContentPage
    {
        public Ajustes()
        {
            InitializeComponent();
        }

        private void Copia(object sender, ToggledEventArgs e)
        {
            //Generar copia de seguridad en la nube
        }

        private void Sincronizacion(object sender, ToggledEventArgs e)
        {
            //Almacenar las nuevas operaciones e informaciones en la nube
        }

        private void Informacion(object sender, EventArgs e)

        {
            DisplayAlert("Informacion","Audio Video Solutions © 2020","OK");
            //Informacion de la empresa, producto (app), fechas y version
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
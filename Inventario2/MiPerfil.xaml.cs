using Inventario2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPerfil : ContentPage
    {
        public MiPerfil(ModelUser u)
        {
            InitializeComponent();
            
            idCorreo.Text = u.correo;
            name.Text = u.nombre + " " + u.apellido_paterno + " " + u.apellido_materno;
            idtel.Text = u.telefono;
            idPersonal.Text = u.rol;

        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
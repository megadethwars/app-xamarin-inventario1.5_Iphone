using Inventario2.Model;
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
    public partial class MiPerfil : ContentPage
    {
        public MiPerfil(Usuario u)
        {
            InitializeComponent();
            
            idCorreo.Text = u.correo;
            name.Text = u.nombre + " " + u.apellido_paterno + " " + u.apellido_materno;
            idtel.Text = u.telefono;
            idPersonal.Text = u.tipoUsuario;

        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
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
    public partial class Menu : ContentPage
    {
        public PDFMovement pdfmovement;
        public Usuario user;
        public Menu(Usuario u)
        {
            pdfmovement = new PDFMovement("");
            InitializeComponent();
            user = u;
            User.nombre = user.nombre;
            User.apellido_paterno = user.apellido_paterno;
            User.ID = user.ID;
            User.correo = user.correo;
        }

        private void Ir_Perfil(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MiPerfil(user));
        }

        private void Ir_Inventario(object sender, EventArgs e)
        {
            if (user.tipoUsuario == "Administrador" || user.tipoUsuario == "Almacen")
                Navigation.PushAsync(new Inventario());
        }

        private void Ir_Historial(object sender, EventArgs e)
        { //Hacer validacion y si es administrador o de bodega accede al historial completo, si es usuario accede a su historial propio
          //tipo=
          //if (tipo==usuario)
          //Navigation.PushAsync(new HistorialUsuario());
            Navigation.PushAsync(new HistorialCompleto());
        }

        private void Ir_Empleados(object sender, EventArgs e)
        { //Hacer validacion y si es administrador accede, si no no realiza nada
          //tipo=
          //if (tipo==administrador)
            if (user.tipoUsuario == "Administrador")
                Navigation.PushAsync(new Empleado(user));
            //else
            //DisplayAlert("Advertencia", "No Puedes acceder, no eres administrador", "OK");
        }

        private void Ir_Reporte(object sender, EventArgs e)
        { //Hacer validacion y si es usuario accede, si no no realiza nada
          //tipo=
          //if (tipo==usuario)
            Navigation.PushAsync(new LevantarReporte(""));
        }

        private void Ir_Ajustes(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Ajustes());
        }

        //solo de prueba
        private void testPDF(object sender, EventArgs e)
        {
            Navigation.PushAsync(pdfmovement);
        }

        
    }
}
    

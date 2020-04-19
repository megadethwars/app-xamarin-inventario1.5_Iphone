using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Model
{
    public class CurrentUser
    {
        public static int ID { get; set; }

        public static string nombre { get; set; }

        public static string apellido_materno { get; set; }

        public static string apellido_paterno { get; set; }

        public static string contrasena { get; set; }

        public static string tipoUsuario { get; set; }

        public static string fechaContratacion { get; set; }

        public static string telefono { get; set; }

        public static string correo { get; set; }
    }
}

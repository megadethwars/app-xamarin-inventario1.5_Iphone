using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Model
{
    public class Usuario
    {
        
        public string ID { get; set; }

        public string nombre{ get; set; }

        public string apellido_materno { get; set; }

        public string apellido_paterno { get; set;  }

        public string contrasena { get; set; }

        public string tipoUsuario { get; set; }

        public string fechaContratacion { get; set; }

        public string telefono { get; set; }

        public string correo { get; set; }

        
    }
}

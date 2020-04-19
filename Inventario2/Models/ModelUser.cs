using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class ModelUser : IDisposable
    {
        public int ID { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string password { get; set; }
        public int IDtipoUsuario { get; set; }
        public string tipousuario { get; set; }
        public string fecha { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public int statuscode { get; set; }
        public string message { get; set; }

        public string foto { get; set; }

        public override string ToString()
        {
            return nombre;
        }
        public void Dispose()
        {
           
        }
    }
}

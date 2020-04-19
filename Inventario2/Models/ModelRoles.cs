using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class ModelRoles:IDisposable
    {
        public int ID { get; set; }

        public string rol { get; set; }

        public int statuscode { get; set; }

        public string message { get; set; }
        public override string ToString()
        {
            return rol;
        }
        public void Dispose()
        {
            
        }
    }
}

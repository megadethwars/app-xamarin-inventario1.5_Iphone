using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class LoginUser:IDisposable
    {
        public string nombre { get; set; }

        public string password { get; set; }

        public void Dispose()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class ModelLugares:IDisposable
    {

        public int ID { get; set; }
        public string Lugar { get; set; }

        public int statuscode { get; set; }

        public string message { get; set; }
        public override string ToString()
        {
            return Lugar;
        }
        public void Dispose()
        {

        }

    }
}

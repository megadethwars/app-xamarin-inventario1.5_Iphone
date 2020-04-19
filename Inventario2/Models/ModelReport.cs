using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class ModelReport:IDisposable
    {

        public string ID { get; set; }

        public string IDreporte { get; set; }

      
        public int IDdevice { get; set; }


        public string fechareporte { get; set; }

        public int IDusuario { get; set; }

        public string codigo { get; set; }

        public string producto { get; set; }

        public string serie { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public string comentario { get; set; }

        

        public string nombre { get; set; }

        public int statuscode { get; set; }

        public string message { get; set; }

        public string foto2 { get; set; }

        public void Dispose()
        {
          
        }
    }
}

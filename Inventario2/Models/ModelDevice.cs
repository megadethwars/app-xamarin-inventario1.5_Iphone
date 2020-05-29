using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
    public class ModelDevice
    {

        public int ID { get; set; }

        public string codigo { get; set; }

        public string producto { get; set; }

        public string marca { get; set; }

        public string fecha { get; set; }

        public string modelo { get; set; }

        public string foto { get; set; }

        public string cantidad { get; set; }

        public string origen { get; set; }

        public string pertenece { get; set; }

        public string observaciones { get; set; }

        public string descompostura { get; set; }

        public string costo { get; set; }

        public string compra { get; set; }

        public string serie { get; set; }

        public string proveedor { get; set; }

        public string Lugar { get; set; }

        public int IDlugar { get; set; }

        public string message { get; set; }

        public int statuscode { get; set; }


        public string IDmov { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2.Models
{
	public class ModelMovements
	{

		public string ID { get; set; }

        public string IDmovimiento { get; set; }

        public int IDtipomov { get; set; }

        public string tipomovimiento { get; set; }

        public int IDdevice { get; set; }


        public string fechamovimiento { get; set; }

        public int IDusuario { get; set; }

        public string codigo { get; set; }

        public string producto { get; set; }

        public string serie { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public int IDlugar { get; set; }

        public string nombre { get; set; }

      

        public string Lugar { get; set; }

        public string observacionesMov { get; set; }

        public string fotomov1 { get; set; }

        public string fotomov2 { get; set; }

        public int statuscode { get; set; }

        public string message { get; set; }

        public string cantidad { get; set; }

    }
}

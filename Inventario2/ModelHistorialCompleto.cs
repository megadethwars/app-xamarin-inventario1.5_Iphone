using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario2
{
    public class ModelHistorialCompleto:IDisposable
    {
        [JsonProperty(PropertyName ="ID")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "movimento")]
        public string movimiento { get; set; }

        [JsonProperty(PropertyName = "lugar")]
        public string lugar { get; set; }

        [JsonProperty(PropertyName = "usuario")]
        public string usuario { get; set; }

        [JsonProperty(PropertyName = "observ")]
        public string observ { get; set; }

        [JsonProperty(PropertyName = "producto")]
        public string producto { get; set; }

        [JsonProperty(PropertyName = "cantidad")]
        public string cantidad { get; set; }

        [JsonProperty(PropertyName = "IdProducto")]
        public string IdProducto { get; set; }

        [JsonProperty(PropertyName = "fechaIn")]
        public string fecha { get; set; }

        [JsonProperty(PropertyName = "fechaOut")]
        public string fechaOut { get; set; }

        public string foto { get; set; }

        public string modelo { get; set; }

        public string marca { get; set; }

        public int QueryStatus { get; set; }

        public string serie { get; set; }

        public void Dispose()
        {
            try {
                throw new NotImplementedException();
            }
            catch
            {

            }
            
        }
    }
}

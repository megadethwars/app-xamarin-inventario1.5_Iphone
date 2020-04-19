using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;

namespace Inventario2.Services
{
    public class LugaresService
    {
        public static string url = "http://127.0.0.1:5000/";
       



        public static async Task<List<ModelLugares>> getlugares()
        {
            var status = await HttpMethods.get(Global.url + "lugares");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelLugares>>(status.message);
                foreach (ModelLugares rol in list)
                {
                    rol.statuscode = status.statuscode;
                }

                return list;

            }
            else
            {
                List<ModelLugares> listerror = new List<ModelLugares>();
                listerror.Add(new ModelLugares());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }
        }

        public static async Task<List<ModelLugares>> getlugar(int id)
        {
            var status = await HttpMethods.get(Global.url + "roles/" + $"{id}");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelLugares>>(status.message);
                foreach (ModelLugares usuario in list)
                {
                    usuario.statuscode = status.statuscode;
                }
                return list;
            }
            else
            {
                List<ModelLugares> listerror = new List<ModelLugares>();
                listerror.Add(new ModelLugares());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }

        }




        public static async Task<StatusMessage> putlugar(int id, string objeto)
        {
            var status = await HttpMethods.put(Global.url + "putrole/" + $"{id}", objeto);
            return status;
        }


        public static async Task<StatusMessage> postlugar(string objeto)
        {
            var status = await HttpMethods.Post(Global.url + "postRole", objeto);
            return status;
        }
    }
}

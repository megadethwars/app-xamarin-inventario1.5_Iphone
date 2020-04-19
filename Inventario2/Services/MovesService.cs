using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
using Newtonsoft.Json;

namespace Inventario2.Services
{
    public class MovesService
    {


        public static async Task<List<Modelmoves>> gettypemoves()
        {
            var status = await HttpMethods.get(Global.url + "typemoves");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Modelmoves>>(status.message);
                foreach (Modelmoves rol in list)
                {
                    rol.statuscode = status.statuscode;
                }

                return list;

            }
            else
            {
                List<Modelmoves> listerror = new List<Modelmoves>();
                listerror.Add(new Modelmoves());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }
        }

        public static async Task<List<Modelmoves>> gettypemove(int id)
        {
            var status = await HttpMethods.get(Global.url + "typemoves/" + $"{id}");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Modelmoves>>(status.message);
                foreach (Modelmoves usuario in list)
                {
                    usuario.statuscode = status.statuscode;
                }
                return list;
            }
            else
            {
                List<Modelmoves> listerror = new List<Modelmoves>();
                listerror.Add(new Modelmoves());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }

        }




        public static async Task<StatusMessage> puttypemove(int id, string objeto)
        {
            var status = await HttpMethods.put(Global.url + "puttypemove/" + $"{id}", objeto);
            return status;
        }


        public static async Task<StatusMessage> posttypemove(string objeto)
        {
            var status = await HttpMethods.Post(Global.url + "posttypemove", objeto);
            return status;
        }

    }
}

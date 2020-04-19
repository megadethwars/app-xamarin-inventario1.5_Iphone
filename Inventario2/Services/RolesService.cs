using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;

namespace Inventario2.Services
{
    public class RolesService
    {
        public static string url = "http://127.0.0.1:5000/";
        
       


        public static async Task<List<ModelRoles>> getroles()
        {
            var status = await HttpMethods.get(Global.url + "roles");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelRoles>>(status.message);
                foreach (ModelRoles rol in list)
                {
                    rol.statuscode = status.statuscode;
                }

                return list;

            }
            else
            {
                List<ModelRoles> listerror = new List<ModelRoles>();
                listerror.Add(new ModelRoles());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }
        }

        public static async Task<List<ModelRoles>> getrol(int id)
        {
            var status = await HttpMethods.get(Global.url + "roles/" + $"{id}");
            if (status.statuscode == 200 || status.statuscode == 201)
            {
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelRoles>>(status.message);
                foreach (ModelRoles usuario in list)
                {
                    usuario.statuscode = status.statuscode;
                }
                return list;
            }
            else
            {
                List<ModelRoles> listerror = new List<ModelRoles>();
                listerror.Add(new ModelRoles());

                listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                listerror[0].statuscode = status.statuscode;
                return listerror;
            }

        }

        


        public static async Task<StatusMessage> putrol(int id, string objeto)
        {
            var status = await HttpMethods.put(Global.url + "putrole/" + $"{id}", objeto);
            return status;
        }


        public static async Task<StatusMessage> postRole(string objeto)
        {
            var status = await HttpMethods.Post(Global.url + "postRole", objeto);
            return status;
        }
    }
}

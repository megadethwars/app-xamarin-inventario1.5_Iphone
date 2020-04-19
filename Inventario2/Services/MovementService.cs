using System;
using System.Collections.Generic;
using System.Text;
using Inventario2.Models;
using System.Threading.Tasks;

namespace Inventario2.Services
{
    public class MovementService
    {
        public static async Task<StatusMessage> postmovement(string objeto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "postmove", objeto);
                return status;
            }
            catch
            {
                return null;
            }


        }


        public static async Task<List<ModelMovements>> searchmovements(string idmovimiento,int idtipomov,int idlugar,int idusuario,string producto,string fechamovimiento,string modelo,string marca,string codigo,string serie)
        {
            //generador string
            if (idmovimiento==null || idmovimiento == "")
            {
                idmovimiento = "null"; 
            }

            if (producto == null || producto=="")
            {
                producto = "null";
            }

            if (fechamovimiento == null || fechamovimiento=="")
            {
                fechamovimiento = "null";
            }

            if (modelo == null || modelo=="")
            {
                modelo = "null";
            }

            if (marca == null || marca=="")
            {
                marca = "null";
            }

            if (codigo == null || codigo=="")
            {
                codigo = "null";
            }

            if (serie == null || serie=="")
            {
                serie = "null";
            }

            string urlsearch = "movesearch?" + "IDmovimiento=" + $"{idmovimiento}"+ "&IDtipomov=" + $"{idtipomov}" + "&IDlugar=" + $"{idlugar}" + "&IDusuario=" + $"{idusuario}" + "&producto=" + $"{producto}" + "&fechamovimiento=" + $"{fechamovimiento}" + "&modelo=" + $"{modelo}" + "&marca=" + $"{marca}" + "&codigo=" + $"{codigo}" + "&serie=" + $"{serie}";


            try
            {
                var status = await HttpMethods.get(Global.url + urlsearch);
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelMovements>>(status.message);
                    foreach (ModelMovements usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelMovements> listerror = new List<ModelMovements>();
                    listerror.Add(new ModelMovements());

                    listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                    listerror[0].statuscode = status.statuscode;
                    return listerror;
                }
            }
            catch
            {
                return null;
            }



        }


    }
}

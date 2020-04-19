using Inventario2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventario2.Services
{
    public class ReportService
    {
        public static async Task<StatusMessage> postreport(string objeto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "postreport", objeto);
                return status;
            }
            catch
            {
                return null;
            }


        }



        public static async Task<List<ModelReport>> getreportbyid(int id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "deviceid/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelReport>>(status.message);
                    foreach (ModelReport usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelReport> listerror = new List<ModelReport>();
                    listerror.Add(new ModelReport());

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


        public static async Task<List<ModelReport>> getreportbycode(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "reportcode/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelReport>>(status.message);
                    foreach (ModelReport usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelReport> listerror = new List<ModelReport>();
                    listerror.Add(new ModelReport());

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


        public static async Task<List<ModelReport>> getreportbyproduct(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "reportname?producto=" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelReport>>(status.message);
                    foreach (ModelReport usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelReport> listerror = new List<ModelReport>();
                    listerror.Add(new ModelReport());

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

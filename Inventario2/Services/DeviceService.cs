using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;


namespace Inventario2.Services
{
    public  class DeviceService
    {

        public static string url = "http://127.0.0.1:5000/";




        public static async Task<List<ModelDevice>> getdevices()
        {
            try
            {
                var status = await HttpMethods.get(Global.url + "devices");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice rol in list)
                    {
                        rol.statuscode = status.statuscode;
                    }

                    return list;

                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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

        public static async Task<List<ModelDevice>> getdevicebyid(int id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "deviceid/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


        public static async Task<List<ModelDevice>> getdevicebycode(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "devicecode/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


        public static async Task<List<ModelDevice>> getdevicebymodel(string id)
        {
            try
            {
                var status = await HttpMethods.get(Global.url + "devicemodelo/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


        public static async Task<List<ModelDevice>> getdevicebyprov(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "deviceprov/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


        public static async Task<List<ModelDevice>> getdevicebyserie(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "deviceserie?serie=" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

                    listerror[0].message = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(status.message).message;
                    listerror[0].statuscode = status.statuscode;
                    return listerror;
                }
            }
            catch (Exception ex) 
            
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            

        }


        public static async Task<List<ModelDevice>> getdevicebymarca(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "devicemarca/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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

        public static async Task<List<ModelDevice>> getdevicebyproduct(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "devicename?nombre=" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


        public static async Task<StatusMessage> deleteDevice(int id)
        {
            try
            {
                var status = await HttpMethods.delete(Global.url + "deldevice/" + $"{id}");
                return status;
            }
            catch
            {
                return null;
            }
        }


        public static async Task<StatusMessage> putdevice(int id1, string objeto)
        {
            try
            {
                var status = await HttpMethods.put(Global.url + "putdevice/" + $"{id1}", objeto);
                return status;
            }
            catch
            {
                return null;
            }

            
        }


        public static async Task<StatusMessage> postdevice(string objeto)
        {
            try 
            {
                var status = await HttpMethods.Post(Global.url + "postDevice", objeto);
                return status;
            }
            catch
            {
                return null;
            }

            
        }


        public static async Task<StatusMessage> VerifyDevicesPlaces(string objeto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "validatesamedevices", objeto);
                return status;
            }
            catch
            {
                return null;
            }


        }


        public static async Task<List<ModelDevice>> getmissingdevices(string id)
        {
            try
            {
                var status = await HttpMethods.get(Global.url + "missingdevices/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelDevice>>(status.message);
                    foreach (ModelDevice usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelDevice> listerror = new List<ModelDevice>();
                    listerror.Add(new ModelDevice());

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


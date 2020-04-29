using Inventario2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Inventario2.Services
{
    public class UserService
    {

        public static string url = "http://127.0.0.1:5000/";
        LoginUser loginuser = new LoginUser();
        public static async Task<StatusMessage> loginAsync(string objecto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "loginpre", objecto);
                return status;
            }
            catch {
                return null;
            }
            
            //devolver como tabla
        }

        public static async Task<StatusMessage> loginpreAsync(string objecto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "login", objecto);
                return status;
            }
            catch
            {
                return null;
            }
            
            //devolver como tabla
        }


        public static async Task<List<ModelUser>> getusers()
        {
            try 
            {
                var status = await HttpMethods.get(Global.url + "users");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelUser>>(status.message);
                    foreach (ModelUser usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }

                    return list;

                }
                else
                {
                    List<ModelUser> listerror = new List<ModelUser>();
                    listerror.Add(new ModelUser());

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

        public static async Task<List<ModelUser>> getuser(int id)
        {
            try
            {
                var status = await HttpMethods.get(Global.url + "users/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelUser>>(status.message);
                    foreach (ModelUser usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelUser> listerror = new List<ModelUser>();
                    listerror.Add(new ModelUser());

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

        public static async Task<List<ModelUser>> getuserbyname(string id)
        {

            try
            {
                var status = await HttpMethods.get(Global.url + "userbyname/" + $"{id}");
                if (status.statuscode == 200 || status.statuscode == 201)
                {
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelUser>>(status.message);
                    foreach (ModelUser usuario in list)
                    {
                        usuario.statuscode = status.statuscode;
                    }
                    return list;
                }
                else
                {
                    List<ModelUser> listerror = new List<ModelUser>();
                    listerror.Add(new ModelUser());

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


        public static async Task<StatusMessage> putuser(int id, string objeto)
        {
            try
            {
                var status = await HttpMethods.put(Global.url + "putuser/" + $"{id}", objeto);
                return status;
            }
            catch
            {
                return null;
            }

            
        }

        public static async Task<StatusMessage> putpass(int id, string objeto)
        {
            try
            {
                var status = await HttpMethods.put(Global.url + "putpassword/" + $"{id}", objeto);
                return status;
            }
            catch
            {
                return null;
            }


        }

        public static async Task<StatusMessage> postuser(string objeto)
        {
            try
            {
                var status = await HttpMethods.Post(Global.url + "postUser", objeto);
                return status;
            }
            catch
            {
                return null;
            }

            
        }


        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
        TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;

        }


        public static async Task<StatusMessage> deleteUser(int id)
        {
            try
            {
                var status = await HttpMethods.delete(Global.url + "deluser/" + $"{id}");
                return status;
            }
            catch
            {
                return null;
            }
        }



    }
}

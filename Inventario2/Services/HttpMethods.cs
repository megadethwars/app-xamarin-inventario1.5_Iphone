using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
namespace Inventario2.Services
{
    class HttpMethods
    {
        public static string url = "http://127.0.0.1:5000/";

        //static HttpClient client = new HttpClient();
        public static async Task<StatusMessage> Post(string url, string objeto)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var stringcontent = new StringContent(objeto, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(url, stringcontent).Result;
                    var estado = await result.Content.ReadAsStringAsync();
                    var statusmessage = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(estado);
                    statusmessage.statuscode = (int)result.StatusCode;
                    return statusmessage;
                }
                
            }
            catch (Exception e)
            {
                return null;
            }



        }


        public static async Task<StatusMessage> get(string path)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(path);

                    var stringres = await response.Content.ReadAsStringAsync();

                    StatusMessage mensaje = new StatusMessage();
                    mensaje.message = stringres;
                    mensaje.statuscode = (int)response.StatusCode;


                    return mensaje;
                }
                

            }
            catch (Exception e)
            {
                return null;
            }


            //if (response.IsSuccessStatusCode)
            //{

            //}

        }



        public static async Task<StatusMessage> put(string url, string objeto)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var stringcontent = new StringContent(objeto, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url, stringcontent);
                    var estado = await response.Content.ReadAsStringAsync();
                    var statusmessage = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(estado);
                    statusmessage.statuscode = (int)response.StatusCode;
                    return statusmessage;
                }

                
            }
            catch (Exception ex)
            {
                return null;
            }


        }




        static async Task<StatusMessage> delete(string url)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);
                    var estado = await response.Content.ReadAsStringAsync();
                    var statusmessage = Newtonsoft.Json.JsonConvert.DeserializeObject<StatusMessage>(estado);
                    statusmessage.statuscode = (int)response.StatusCode;
                    return statusmessage;
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
    }
}

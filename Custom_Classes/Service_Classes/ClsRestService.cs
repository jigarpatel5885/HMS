using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;

namespace HMS.Custom_Classes
{
    public class ClsRestService
    {
        public string  globalResponseMessage { get; set; }
        public int globalResponseId { get; set; }
       

        #region "Utilities"


        public Boolean checkAlive(string url)
        {
            var fgCheckAlive = false;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var serviceResponse = response.RequestMessage.ToString();
                    HttpContent Content = response.Content;
                    var myContent = Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(myContent.ToString());
                    globalResponseId = (int)obj["responseCode"];
                   // globalResponseMessage = (string)obj["responseMessage"];

                    if (globalResponseId == 1)
                    {
                        fgCheckAlive = true;
                    }
                }
            }
            catch (Exception)
            {

                fgCheckAlive = false;
            }
            return fgCheckAlive;
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public DataTable GetHttpRequestDataTable(string url)
        {
            var joDataTable = new DataTable();
            try
            {             
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var serviceResponse = response.RequestMessage.ToString();
                    HttpContent Content = response.Content;
                    var myContent = Content.ReadAsStringAsync();
                    JObject joResponse = JObject.Parse(myContent.Result);              
                    joDataTable = JsonConvert.DeserializeObject<DataTable>(joResponse.GetValue("response").ToString());

                }
                else
                {             
                    joDataTable = null;
                }

            }
            catch (Exception ex)
            {
                joDataTable = null;
            }
              return joDataTable;
        } 
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int PostRequest(string Url, Dictionary<string, string> Params)
        {
            globalResponseId = 0;
            globalResponseMessage = string.Empty;
            try
            {

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Params);               
                HttpClient Client = new HttpClient();
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Response =  Client.PostAsync(Url, contentPost).Result;
                if (Response.IsSuccessStatusCode)
                {
                    var responseContent = Response.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(responseContent.ToString());
                    globalResponseId = (int)obj["responseCode"];
                    globalResponseMessage = (string)obj["responseMessage"];

                }
                else
                {
                     globalResponseId = 0 ;
                     globalResponseMessage = Response.StatusCode.ToString();
                }

            }
            catch (Exception ex)
            {

                globalResponseId = 0 ;
                globalResponseMessage = ex.Message.ToString();
            }

            return globalResponseId;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
       
        public int PostRequestMultiDimesionJsonString(string Url, Dictionary<string, object> Params)
        {
            globalResponseId = 0;
            globalResponseMessage = string.Empty;
            try
            {

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Params);
                HttpClient Client = new HttpClient();
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Response = Client.PostAsync(Url, contentPost).Result;
                if (Response.IsSuccessStatusCode)
                {
                    var responseContent = Response.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(responseContent.ToString());
                    globalResponseId = (int)obj["responseCode"];
                    globalResponseMessage = (string)obj["responseMessage"];

                }
                else
                {
                    globalResponseId = 0;
                    globalResponseMessage = Response.StatusCode.ToString();
                }

            }
            catch (Exception ex)
            {

                globalResponseId = 0;
                globalResponseMessage = ex.Message.ToString();
            }

            return globalResponseId;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int PutRequest(string Url, Dictionary<string, string> Params)
        {
            globalResponseId = 0;
            globalResponseMessage = string.Empty;
            try
            {

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Params);
                HttpClient Client = new HttpClient();
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Response = Client.PutAsync(Url, contentPost).Result;
                if (Response.IsSuccessStatusCode)
                {
                    var responseContent = Response.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(responseContent.ToString());
                    globalResponseId = (int)obj["responseCode"];
                    globalResponseMessage = (string)obj["responseMessage"];
                 
                }

            }
            catch (Exception ex)
            {

                globalResponseId = 0;
                globalResponseMessage = ex.Message.ToString();
            }

            return globalResponseId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public int PutRequestMultiDimesionJsonString(string Url, Dictionary<string, object> Params)
        {
            globalResponseId = 0;
            globalResponseMessage = string.Empty;
            try
            {

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Params);
                HttpClient Client = new HttpClient();
                HttpContent contentPost = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage Response = Client.PutAsync(Url, contentPost).Result;
                if (Response.IsSuccessStatusCode)
                {
                    var responseContent = Response.Content.ReadAsStringAsync().Result;
                    JObject obj = JObject.Parse(responseContent.ToString());
                    globalResponseId = (int)obj["responseCode"];
                    globalResponseMessage = (string)obj["responseMessage"];

                }
                else
                {
                    globalResponseId = 0;
                    globalResponseMessage = Response.StatusCode.ToString();
                }

            }
            catch (Exception ex)
            {

                globalResponseId = 0;
                globalResponseMessage = ex.Message.ToString();
            }

            return globalResponseId;

        }


        //public int DeleteRequest(string Url, Dictionary<string, string> Params)
        //{
        //    // this metho is not yet ready 
        //    // very temporary  params should be used from the parameter of the post request method  as pass as list object           

        //    HttpContent Content = new FormUrlEncodedContent(P_List);
        //    using (HttpClient Client = new HttpClient())
        //    {
        //        using (HttpResponseMessage Response =  Client.DeleteAsync("api/person/10"))
        //        {
        //            using (HttpContent Content1 = Response.Content)
        //            {
        //                string MyContent =  Content.ReadAsStringAsync();
        //                HttpContentHeaders headers = Content.Headers;
        //                // print MyContent
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Url"></param>
        ///// <param name="P_List"></param>
        //public async void Delete(string Url, IEnumerable<KeyValuePair<string, string>> P_List)
        //{
        //    // this metho is not yet ready 
        //    // very temporary  params should be used from the parameter of the post request method  as pass as list object           

        //    HttpContent Content = new FormUrlEncodedContent(P_List);
        //    using (HttpClient Client = new HttpClient())
        //    {
        //        using (HttpResponseMessage Response = await Client.DeleteAsync("api/person/10"))
        //        {
        //            using (HttpContent Content1 = Response.Content)
        //            {
        //                string MyContent = await Content.ReadAsStringAsync();
        //                HttpContentHeaders headers = Content.Headers;
        //                // print MyContent
        //            }
        //        }
        //    }
        //}


        public DataTable GetHttpRequestDataTableNoFormat(string url)
        {
            var joDataTable = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var serviceResponse = response.RequestMessage.ToString();
                    HttpContent Content = response.Content;
                    var myContent = Content.ReadAsStringAsync();               
                   joDataTable = JsonConvert.DeserializeObject<DataTable>(myContent.Result);                   
                }
                else
                {
                    joDataTable = null;
                }

            }
            catch (Exception ex)
            {
                joDataTable = null;
            }
            return joDataTable;
        } 

        #endregion

    }



}

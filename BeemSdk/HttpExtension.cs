using BeemSDK.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BeemSDK
{
    public static class HttpExtension
    {
        public static KeyValuePair<string, T> Put<T>(this HttpClient client, string fullUrl, object bodyToPost) where T : class
        {
            var uri = new Uri(fullUrl);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var stringContent = new StringContent(JsonConvert.SerializeObject(bodyToPost), Encoding.UTF8, "application/json");

            var response = client.PutAsync(uri, stringContent).Result;

            var resData = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<T>(resData);

                return new KeyValuePair<string, T>(null, data);
            }
            // log 
            return new KeyValuePair<string, T>("Error processing request", null);
        }

        public static KeyValuePair<string , T> Post<T>(this HttpClient client , string fullUrl , object bodyToPost ) where T:class
        { 
            var uri = new Uri(fullUrl);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
           
            var stringContent = new StringContent(JsonConvert.SerializeObject(bodyToPost), Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri , stringContent).Result;

            var resData = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<T>(resData);

                return new KeyValuePair<string, T>(null , data);
            }
            // log 
            return new KeyValuePair<string, T>("Error processing request",null);
        }
        public static KeyValuePair<string , T> Get<T>(this HttpClient client , string fullUrl  ) where T:class
        { 
            var uri = new Uri(fullUrl);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
           
            var response = client.GetAsync(uri).Result;

            var resData = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<T>(resData);

                return new KeyValuePair<string, T>(null , data);
            }
            return new KeyValuePair<string, T>("Error processing request",null);
        }

    }
}

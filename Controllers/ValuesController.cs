﻿using BeemAfricaSDK.USSD.Builder.v1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace TreeNodeSample.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Beem()
        {
            /*
             * User query param key to protect you can built a customer attribute to handle this
             */ 
            System.IO.StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);

            string requestFromPost = reader.ReadToEnd();
   
            if (string.IsNullOrEmpty(requestFromPost))
            {
                // return error
            }

            JObject responseD = new JObject();
            JObject request = JObject.Parse(requestFromPost);

            responseD["msisdn"] = request.UssdGetJsonObjectStringValue("msisdn");
            responseD["operator"] = request.UssdGetJsonObjectStringValue("operator");
            responseD["session_id"] = request.UssdGetJsonObjectStringValue("session_id");

            var userText = request.UssdGetJsonObject("payload").UssdGetJsonObjectStringValue("response");

            string ussdSessionKey = "ussd_" + responseD["session_id"];


            /*
             * you can use session , cache , any to keep track of the session just using cache as an example but you can use any
             */ 

            USSDEngine uSSDSessionModel = CacheManager.GetCache<USSDEngine>(ussdSessionKey);
            int counter = 0;
            if (null == uSSDSessionModel)
            {
                /**
                 * Bootstrap your ussd application and put stuff your want its only loaded once
                 * 
                 */ 

                uSSDSessionModel = new USSDEngine();
                uSSDSessionModel.UssdMenus = DataForm.GetAppForm();
                uSSDSessionModel.UpdateUssdMenu();
                uSSDSessionModel.SessionId = responseD["session_id"].ToString();
                uSSDSessionModel.MobileNumber = responseD["msisdn"].ToString();
                uSSDSessionModel.Operator = responseD["operator"].ToString();
                uSSDSessionModel.MetaData = new JObject();
                uSSDSessionModel.MetaData["_counter"] = counter;

                CacheManager.AddCache(ussdSessionKey, uSSDSessionModel, DateTime.Now.AddSeconds(30));
            }
            else
            { 
                /*
                 * called when a session exist 
                 */ 
                int.TryParse(uSSDSessionModel.MetaData["_counter"].ToString(), out counter);
                counter = counter + 1;
                uSSDSessionModel.MetaData["_counter"] = counter;
            }

            var data = uSSDSessionModel.ProcessRequest(userText);

            CacheManager.Update(ussdSessionKey, data.Value);

            var command = "continue";
            if (data.Key == USSDState.ENDSESSION)
                command = "Terminate";

            responseD["command"] = command;

            var responsePayLoad = new JObject();
            responsePayLoad["request_id"] = counter;
            responsePayLoad["request"] = data.Value.Response;

            responseD["payload"] = responsePayLoad;
             
            return new HttpResponseMessage()
            {
                Content = new StringContent(responseD.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
         

    }
}

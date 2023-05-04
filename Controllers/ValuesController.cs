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
using USSDFormBuilder.USSD.Builder.v1;

namespace TreeNodeSample.Controllers
{
    public class ValuesController : ApiController
    {
        public static Dictionary<string, USSDEngine> _sesssions;
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

            if (_sesssions == null)
                _sesssions = new Dictionary<string, USSDEngine>();

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

            // the USSDEngine is a routing framework to manage your request and response , know the menu and form you are at
            // USSDEngine will soon add this to the nuget package manage , l have included a ddls for the sdk
            // it then uses events to call your methods 
            // basically you just build the menus , forms and events without need to worry about ussd callback logic 
            // check Data.GetAppForm to see how to build a form
            USSDEngine uSSDSessionModel = null;

            int counter = 0;
            if (!_sesssions.TryGetValue(ussdSessionKey, out uSSDSessionModel))
            {
                /**
                 * Bootstrap your ussd application and put stuff your want its only loaded once
                 * 
                 */ 

                uSSDSessionModel = new USSDEngine();
                uSSDSessionModel.UssdMenus = DataForm.GetAppForm(); // you can build a menu you want open the form to see how the menu and forms are generated
                
                /*
                 * when user is already sign up bootstrap which form to show 
                 * this is just a demo yet to include many utils to the builder to make it more dynamic
                 * 
                 */

                // this only gets called once when initilizing
                uSSDSessionModel.UpdateUssdMenu();
                uSSDSessionModel.SessionId = responseD["session_id"].ToString();
                uSSDSessionModel.MobileNumber = responseD["msisdn"].ToString();
                uSSDSessionModel.Operator = responseD["operator"].ToString();
                uSSDSessionModel.MetaData = new JObject();
                uSSDSessionModel.MetaData["_counter"] = counter;

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

            _sesssions[ussdSessionKey] = data.Value;

            var command = "continue";
            if (data.Key == USSDState.ENDSESSION)
                command = "Terminate";

            responseD["command"] = command;

            var responsePayLoad = new JObject();
            responsePayLoad["request_id"] = counter;
            responsePayLoad["request"] = data.Value.Response;

            responseD["payload"] = responsePayLoad;
             

            if(data.Key == USSDState.ENDSESSION)
            {
                // remove from sesssion
                _sesssions.Remove(ussdSessionKey);
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(responseD.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
         

    }
}

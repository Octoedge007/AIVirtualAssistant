using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Octoedge007.Models;
using Microsoft.CognitiveServices.Speech;
using static System.Net.WebRequestMethods;
using System.Text;
using Newtonsoft.Json;

namespace Octoedge007.Controllers
{
    public class HomeController : Controller
    {
        private readonly VoiceChatbot chatbot = new VoiceChatbot();
        private readonly string subscriptionKey = "YourAzureSubscriptionKey";
        private readonly string serviceRegion = "YourAzureServiceRegion";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMesage(string textmsg)
        {
            try
            {
                string Correctmesage="";
                Reques res= new Reques();

                if (textmsg.ToLower().Trim()=="hi")
                {
                    Correctmesage = "Welcome to IIFL Loans! This is your AI Virtual Assistant, How Can I Help You Today...";
                }

                else
                {
                    Correctmesage = "Welcome to IIFL Loans! This is your AI Virtual Assistant, How Can I Help You Today...";
                }
                Correctmesage = OpenApicall(textmsg);

                res = (Reques)JsonConvert.DeserializeObject(Correctmesage, typeof(Reques));
                Correctmesage = res.answer;

                return Json(Correctmesage, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Kindly contact our Customer Care for further assistance.", JsonRequestBehavior.AllowGet);
            }
        }

        public string OpenApicall(string textmsg)
        {
            try
            {

                Requestmodel obj = new Requestmodel();                
                obj.question = textmsg;
                string Response;
                string uri = "https://team2-brainyfools.azurewebsites.net/api/promptGpt";
                
                System.Web.Script.Serialization.JavaScriptSerializer J = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonObj = J.Serialize(obj);
                Response = GetAPIResponse(uri, jsonObj);

                return Response;
            }
            catch (Exception ex)
            {
                return "Kindly contact our Customer Care for further assistance.";
            }
        }

        public string GetAPIResponse(string uri, string jsonObj)
        {
            UTF8Encoding encoding;
            byte[] bytes;
            try
            {
                Uri geocodeRequest = new Uri(uri);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(geocodeRequest);
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Web.Script.Serialization.JavaScriptSerializer J = new System.Web.Script.Serialization.JavaScriptSerializer();
                request.Method = "POST";
                request.ContentType = "application/json";                
                encoding = new UTF8Encoding();
                encoding = new UTF8Encoding();
                bytes = encoding.GetBytes(jsonObj);
                request.ContentLength = bytes.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the data.
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                StreamReader loResponseStream = new StreamReader(resp.GetResponseStream());
                string Response = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                resp.Close();

                return Response;
            }
            catch (Exception ex)
            {               
                return null;
            }
        }


        public async Task<ActionResult> About()
        {
            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the URL of the API you want to call
                    string apiUrl = "https://team2-brainyfools.azurewebsites.net/api/promptGpt"; // Replace with the actual API URL

                    // Make an HTTP GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        // Process the API response data
                        // You can parse JSON, XML, or other data formats here
                        return Content(apiResponse, "application/json"); // Return the API response as content
                    }
                    else
                    {
                        // Handle the error, e.g., log it or return an error message
                        return Content("API call failed with status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions that may occur during the API call
                    return Content("An error occurred: " + ex.Message);
                }
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
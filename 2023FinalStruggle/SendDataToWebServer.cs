using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfSendHttpRequest
{
    class SendDataToWebServer
    {


        //[Read server URL from setting file]
        //private static string webServer = "http://localhost:62338/Home/GetClientData";
        private static string webServer = "http://localhost:27534/api/values";
        
        
        private static readonly HttpClient client = new HttpClient();
        //Webサーバーに送信する
        //送信内容を一つのdictionaryに入れる必要がある。
        public static async Task<string> sendDataWithPost(Dictionary<string, string> clientMsgDict)
        {
            


            try
            {
                //Reference:https://copyprogramming.com/howto/csharp-send-json-in-post-request-c-httpclient#send-json-data-in-http-post-request-c

                string jsonPreliminaryString = JsonConvert.SerializeObject(clientMsgDict);
                string jsonString = JsonConvert.SerializeObject(jsonPreliminaryString);
                //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                //string jsonString = JsonConvert.SerializeObject(clientMsgDict, Formatting.None, jsonSerializerSettings);
                
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(webServer, content);
                string serverMsg = "";
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    //If the content returned is in json
                   
                    try
                    {
                        
                        //Get the response as an object to get the value directly 
                        //if the response contains JSON information 
                        var responseObj = JsonConvert.DeserializeObject<ServerResponse>(responseString);

                        //Extract values from the server response
                        var serverStatus = responseObj.serverStatus;
                        var sendError = responseObj.sendError;
                        serverMsg = "Server response format: JSON\n";
                        serverMsg += $"{serverStatus}+{sendError}";
                       
                    }

                    //If the response from the server is not in JSON format, 
                    //read it as a string
                    catch (Exception)
                    {
                        serverMsg = responseString;
                    }

                    //For Debug
                    //Write the response from server to log for debug
                    //string errorType = "WebServerに送信成功記録";
                    //ReportErrorMsg.outputErrorMsg(serverMsg, errorType);
                    
                }

                return serverMsg;
            }
            catch(Exception e)
            {
                //Output and accumulate the error message to a file 
                //string errorType = "WebServerに送信エラー";
                //ReportErrorMsg.outputErrorMsg(e.StackTrace, errorType, needAccumulate: true);
                return e.ToString();
            }


        }


        // The JSON data received from the server
        public class ServerResponse
        {
            public string sendError { get; set; }
            public string serverStatus { get; set; }
        }
    }
}

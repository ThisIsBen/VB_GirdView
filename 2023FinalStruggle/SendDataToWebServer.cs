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
        private static string webServer = "http://localhost:62338/Home/GetClientData";

        
        private static readonly HttpClient client = new HttpClient();
        //Webサーバーに送信する
        //送信内容を一つのdictionaryに入れる必要がある。
        public async void sendData(Dictionary<string, string> clientMsgDict)
        {
            //var Example_clientMsgDict = new Dictionary<string, string>
            //  {
                 
            //      { "key1", "10.jpg" },
            //      { "key2", "NG" }
            //  };



            try
            {
                //Reference:https://copyprogramming.com/howto/csharp-send-json-in-post-request-c-httpclient#send-json-data-in-http-post-request-c
                string jsonString = JsonConvert.SerializeObject(clientMsgDict);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(webServer, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    //If the content returned is in json
                    string serverMsg="";
                    try
                    {
                        
                        //Get the response as an object to get the value directly 
                        //if the response contains JSON information 
                        var responseObj = (ServerResponse)JsonConvert.DeserializeObject(responseString);

                        //Extract values from the server response
                        var age = responseObj.serverStatus;
                        var name = responseObj.sendError;
                        serverMsg = "Server response format: JSON\n";
                        serverMsg += $"{age}{name}";
                       
                    }

                    //If the response from the server is not in JSON format, 
                    //read it as a string
                    catch (Exception)
                    {
                        serverMsg = responseString;
                    }

                    //For Debug
                    //Write the response from server to log for debug
                    string errorType = "WebServerに送信成功記録";
                    ReportErrorMsg.outputErrorMsg(serverMsg, errorType);

                }
            }
            catch(Exception e)
            {
                //Output and accumulate the error message to a file 
                string errorType = "WebServerに送信エラー";
                ReportErrorMsg.outputErrorMsg(e.StackTrace, errorType, needAccumulate: true);

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

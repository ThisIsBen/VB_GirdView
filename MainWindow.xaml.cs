using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

namespace WpfSendHttpRequest
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            
        }

        private static readonly HttpClient client = new HttpClient();
        private static string webServer = "https://localhost:44399/ReceiveHttpRequest.aspx";


        private static int postBtnClickCount = 0;
        private static int getBtnClickCount = 0;

        private async void postBtn_Click(object sender, RoutedEventArgs e)
        {
            

            var values = new Dictionary<string, string>
              {
                  { "key1", "10"+postBtnClickCount+".jpg" },
                  { "key2", "NG" }
              };
            postBtnClickCount += 1;
            var content = new FormUrlEncodedContent(values);



       
            //If you don't need to receive message from the server, use the following.
            //client.PostAsync(webServer, content);

            //If the server side doesn't use Response.Write(msgFromServer);Response.End()
            //the webpage's html content will be received by responseString.
            var response = await client.PostAsync(webServer, content);
            var responseString = await response.Content.ReadAsStringAsync();

            lbWebServerResponse.Content += "\n"+responseString;
        }

        private async void getBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string paraStr = "?key1=50"+ getBtnClickCount + ".jpg&key2=良品";
            getBtnClickCount += 1;
            string httpRequest = webServer + paraStr;

            //If you don't need to receive message from the server, use the following.
            //client.GetAsync(httpRequest);

            //If the server side doesn't use Response.Write(msgFromServer);Response.End()
            //the webpage's html content will be received by responseString.
            var responseString = await client.GetStringAsync(httpRequest);
            lbWebServerResponse.Content += "\n" + responseString;

            


        }

        

    }
}

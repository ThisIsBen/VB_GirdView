using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace WpfSendHttpRequest
{
    class WebServerSettings
    {
        //usage
        //readWebServerSettings(GlobalConstants.WebServer_SettingFile);
       
        //Webサーバー送信に必要な設定ファイルを読み込む
        public void readWebServerSettings(string filePath)
        {
           
            bool IsFirstLine=true; 
            foreach(string line in FileIO.readAllLinesOfAFile(filePath)) 
            { 
               //Headerを記録
               if(IsFirstLine)
               {
                    IsFirstLine=false;
                    foreach(string header in line.Split(','))
                    {
                        GlobalConstants.WebServer_SettingDict[header]=new List<string>();
                        GlobalConstants.WebServer_SettingOrder.Add(header);
                    }
                    

               }
               //設定値を記録
               else
               {
                   string[] settingArray = line.Split(',');
                   for(int i=0;i<settingArray.Length;i++)
                   {
                      GlobalConstants.WebServer_SettingDict[GlobalConstants.WebServer_SettingOrder[i]].Add(settingArray[i]);
                   }

               }
            } 
            


        }

        //検索対象に該当するWebサーバーの設定値を探し出して返す
        public string getSettings(string indexType, string indexValue, string searchType)
        {
            List<string> values=GlobalConstants.WebServer_SettingDict[indexType];
            //検索対象の設定ファイル内のRow番号の取得
            int rowNo;
            for(int i=0;i<values.Length;i++)
            {
                if(values[i]==indexValue)
                {
                    rowNo=i;
                    break;
                }
            }

            return GlobalConstants.WebServer_SettingDict[searchType][rowNo]
            
                  


        }


        //usage
        //WebServerSetting settingObj=new WebServerSetting();
        public class WebServerSetting
        {
            public string stationNo { get; set; }
            public string machineNo { get; set; }
            public string IP { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace [TBD]
{
    public class GlobalConstants
    {
      


        #region 処理時間測定記録の出力
        public static string OveralTimeFilePath = @"C:\AutoCompressorWindowsService\ユーザー操作\圧縮済みフォルダー記録.json";
        public static string DetailedTimeFilePath = @"C:\AutoCompressorWindowsService\ユーザー操作\圧縮済みフォルダー記録.json";

        #endregion



        #region エラーメッセージの出力
        public static string errorMessageTxtFolderPath = @"C:\AutoCompressorWindowsService\ユーザー操作\圧縮済みフォルダー記録.json";

        #endregion

        #region エラー時のリトライ
            //Retry times when error occurs during compression.
            public static int retryTimesLimit = 3;

            //The time interval between 2 retries when error occurs    
            public static int retryTimeInterval = 1000;
        #endregion


        #region 文字コード
            public static Encoding AppEncoding = Encoding.GetEncoding("shift-jis");
        #endregion

        #region Webサーバーに送信
            public static string WebServer_SettingFile = @"C:\AutoCompressorWindowsService\ユーザー操作\圧縮済みフォルダー記録.json";
            
            public static Dictionary<string, string> WebServer_SettingDict = new Dictionary<string, string>();
            //登録順が保証されているListで設定の順番を記録する
            public static List<sting> WebServer_SettingOrder = new List<string>();   
        #endregion



       




    }
}

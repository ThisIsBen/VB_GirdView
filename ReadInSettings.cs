using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PI製膜予兆事後監視カネカアプリ
{
    class ReadInSettings
    {

        //設定ファイルから設定を読み込んで適用する。
        public static void ApplySettingsFromTxtFile(string settingFilePath)
        {

            try
            {
                Queue<string> txtFileContentQueue = new Queue<string>();
                String line = null;
                // Create an instance of StreamReader to read from a file.
                // The using statement automatically closes the StreamReader.
                using (StreamReader settingFileReader = new StreamReader(settingFilePath))
                {
                    do
                    {
                        line = settingFileReader.ReadLine();

                        //when the reader reaches the end of the file
                        if (line == null)
                        {
                            break;
                        }

                        //ignore white space
                        else if (line == String.Empty || Regex.Replace(line, @"\s", "") == "")
                        {
                            continue;
                        }


                        //ignore lines that start with 全角半角[ or ( or * or / or \
                        else if (line[0] == '[' || line[0] == '［' || line[0] == '(' || line[0] == '（' || line[0] == '*' || line[0] == '＊' || line[0] == '/' || line[0] == '・' || line[0] == '\\' || line[0] == '￥')
                        {
                            continue;
                        }


                        //ignore each title 
                        else if (!line.Contains('='))
                        {
                            continue;
                        }



                        //trim the white space in the front and in the back of a non-empty line
                        txtFileContentQueue.Enqueue(line.Trim().Split('=')[1].Trim());


                    } while (true);
                }

                //assign the settings read from 設定パラメーターTxtファイル to system variables
                assignSettingsToSystemVar(txtFileContentQueue);
            }
            catch(Exception e)
            {
                Console.WriteLine("設定ファイルの読み込み・パラメーター初期化に失敗しました。\n");                
                Console.WriteLine("エラーメッセージ："+e.Message+"\n");
                Console.WriteLine("Enterキーを押して終了してください。");
                //処理をここに止める。
                Console.Read();
                //stop this カネカアプリ 
                Environment.Exit(Environment.ExitCode);
            }
            



            



        }

        //Assign the settings read from 設定ファイル to system variables
        //!!Notice: "変数の値の設定順番は設定ファイルと同じ順番にする必要がある。"
        private static void assignSettingsToSystemVar(Queue<string>txtFileContentQueue)
        {
            #region 連動ソフトの変数　（設定パラメータTxtファイルから読み込んだパラメータ）
            GlobalConstants.cameraPicFolderPath = txtFileContentQueue.Dequeue();
            GlobalConstants.imageType = txtFileContentQueue.Dequeue();
            //The TCP socket connection settings with データ管理カネカアプリKanekaApp7
            GlobalConstants.KanekaApp7TCPSocketServerName= txtFileContentQueue.Dequeue();
            GlobalConstants.KanekaApp7TCPSocketServerIP = txtFileContentQueue.Dequeue();
            GlobalConstants.KanekaApp7TCPSocketServerPort = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);




            //The TCP socket connection settings with AnotherPCデータ管理カネカアプリKanekaApp7

            //(予兆や事後を検知したら、AnotherPCのカネカアプリ7に連絡するかどうかを決める)
            //Yes:予兆や事後を検知したら、AnotherPCのカネカアプリ7に連絡し、警報前後の画像を保存してもらう。
            //No :予兆や事後を検知したら、AnotherPCのカネカアプリ7に連絡せず、予兆や事後を検知したPCだけ前後の画像を保存する。
            string wantToConnectToAnotherPCKanekaApp7 = txtFileContentQueue.Dequeue().ToUpper();
            if (wantToConnectToAnotherPCKanekaApp7 == "YES")
            {
                GlobalConstants.wantToConnectToAnotherPCKanekaApp7 = true;
            }
            else
            {
                GlobalConstants.wantToConnectToAnotherPCKanekaApp7 = false;
            }
            GlobalConstants.AnotherPCKanekaApp7TCPSocketServerName = txtFileContentQueue.Dequeue();
            GlobalConstants.AnotherPCKanekaApp7TCPSocketServerIP = txtFileContentQueue.Dequeue();
            GlobalConstants.AnotherPCKanekaApp7TCPSocketServerPort = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);


            //The TCP socket connection settings with データ管理カネカアプリKanekaApp8
            GlobalConstants.KanekaApp8TCPSocketServerName = txtFileContentQueue.Dequeue();
            GlobalConstants.KanekaApp8TCPSocketServerIP = txtFileContentQueue.Dequeue();
            GlobalConstants.KanekaApp8TCPSocketServerPort = Convert.ToInt32(txtFileContentQueue.Dequeue());

            string wantToConnectToKanekaApp8 = txtFileContentQueue.Dequeue().ToUpper();
            if (wantToConnectToKanekaApp8 == "YES")
            {
                GlobalConstants.wantToConnectToKanekaApp8 = true;
            }
            else
            {
                GlobalConstants.wantToConnectToKanekaApp8 = false;
            }



            //データ管理アプリ（KanekaApp7、KanekaApp8、AnotherPCのKanekaApp7）に送信してからどのくらい返信が来なかったら、エラーメッセージを表示するかを決める
            GlobalConstants.TCPSocketServerResponseTimeout = Convert.ToInt32(txtFileContentQueue.Dequeue(),CultureInfo.InvariantCulture) *1000;




            //警報ファイルの出力先
            GlobalConstants.alertFileFolderPath = txtFileContentQueue.Dequeue();

            //警報履歴ファイルを生成する機能
            //警報履歴ファイルを生成するかどうかを決める
            string saveAlertFileHistoryLogCSV = txtFileContentQueue.Dequeue().ToUpper();
            if (saveAlertFileHistoryLogCSV == "YES")
            {
                GlobalConstants.saveAlertFileHistoryLogCSV = true;
            }
            else
            {
                GlobalConstants.saveAlertFileHistoryLogCSV = false;
            }            
            GlobalConstants.alertFileHistoryLogFolderPath = txtFileContentQueue.Dequeue();
            GlobalConstants.saveAlertFileHistoryLogCSVTimeInterval = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);

            //終了ファイル監視
            GlobalConstants.endFileFolderPath = txtFileContentQueue.Dequeue();

            //エラー処理
            GlobalConstants.retryTimesLimit = Convert.ToByte(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
            GlobalConstants.errorMessageTxtFolderPath = txtFileContentQueue.Dequeue();
            GlobalConstants.ERR_PicFolderPath = txtFileContentQueue.Dequeue();
            GlobalConstants.retryTimeInterval = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 1000);
            GlobalConstants.retryConnectToTCPSocketServerTimeInterval = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 1000);

            //定期的に生存確認ファイル更新機能
            GlobalConstants.checkIsDataManagementAppAliveTimeInterval = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 1000);
            GlobalConstants.aliveConfirmRootFolderPath = txtFileContentQueue.Dequeue();
            GlobalConstants.updateAliveConfirmFileTimeInterval = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 60 * 1000);
            GlobalConstants.waitForAliveConfirmFolderCreationTimeInterval = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 1000);
            GlobalConstants.getLatestAliveConfirmFolder_TimeLimit = Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture));


            // Decide whether to show the window of the program
            const int SW_MINIMIZE = 6;
            const int SW_HIDE = 0;
            string windowDisplay = txtFileContentQueue.Dequeue().ToUpper();
            if (windowDisplay == "YES")
            {
                GlobalConstants.windowDisplay = SW_MINIMIZE;
            }
            else
            {
                GlobalConstants.windowDisplay = SW_HIDE;
            }
            GlobalConstants.runOnWhichCPU = txtFileContentQueue.Dequeue();

            #endregion


            #region 画像処理結果連続N回予兆、許容事後、異物侵入だったら、本当の予兆、事後、異物侵入と判断する
            //Halcon画像処理結果連続N回許容事後だったら、本当の事後と判断する
            //警報ファイルを出す、警報履歴ファイルに記録する、アラームを鳴らす、画像をNASに保存する。
            //Set ”How many times 許容事後  occurs do we regard it as a real 事後 ”
            GlobalConstants.tolerableJIGOThreshold = Convert.ToByte(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);


            //Halcon画像処理結果連続N回許容事後だったら、本当の事後と判断する
            //警報ファイルを出す、警報履歴ファイルに記録する、アラームを鳴らす、画像をNASに保存する。
            //Set ”How many times 許容事後  occurs do we regard it as a real 事後 ”
            GlobalConstants.tolerableYOCYOThreshold = Convert.ToByte(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);

            #endregion




            #region 使い勝手の向上対策

                #region プログラムを編集せずに事象名を調整する仕組み
                //事象名参照ファイルのパス 
                GlobalConstants.categoryName_Path = txtFileContentQueue.Dequeue();

               
                #endregion


                #region トレンドデータの出力

                        #region 1時間ごと/1秒ごとのトレンドデータの共通設定
                        //トレンドデータを出力するかどうかが選べる。
                        string wantTrendData = txtFileContentQueue.Dequeue().ToUpper();
                        if (wantTrendData == "YES")
                        {
                            GlobalConstants.wantTrendData = true;
                        }
                        else
                        {
                            GlobalConstants.wantTrendData = false;
                        }



                        //どのトレンドデータを出力かを指定
                        string whichtTrendDataToOutput = txtFileContentQueue.Dequeue().ToUpper();
                        if (whichtTrendDataToOutput == "A")
                        {
                            GlobalConstants.whichtTrendDataToOutput = "数値データのみ出力";
                        }
                        else if (whichtTrendDataToOutput == "B")
                        {
                            GlobalConstants.whichtTrendDataToOutput = "カテゴリーデータのみ出力";
                        }
                        else
                        {       
                            GlobalConstants.whichtTrendDataToOutput = "両方出力";
                        }



                        //トレンドデータの出力周期（単位：分）
                        //(制限：60の因数に設定してください。)
                        GlobalConstants.outputTrendDataInterval = Convert.ToByte(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);


                        //[対応表.csvのパス]
                        //[数値データカメラ]の場合:
                        GlobalConstants.numericRelatedTable_Path = txtFileContentQueue.Dequeue();
                        //[カテゴリーデータカメラ]の場合:
                        GlobalConstants.categoryRelatedTable_Path = txtFileContentQueue.Dequeue();



                        //[カテゴリー番号ファイルのパス]
                        GlobalConstants.categoryNo_Path = txtFileContentQueue.Dequeue();

                        #endregion



                        #region 1時間ごとの要約版トレンドデータのみの設定
                        //[1時間ごとの要約版トレンドデータのみの設定]
                        //[数値データ1時間ごとの要約版トレンドデータ出力先]
                        GlobalConstants.numericSummaryTrendDataOutputPath = txtFileContentQueue.Dequeue();

                        //[カテゴリーデータ1時間ごとの要約版トレンドデータ出力先]
                        GlobalConstants.categorySummaryTrendDataOutputPath = txtFileContentQueue.Dequeue();
                        #endregion

                        #region 1秒ごとのトレンドデータのみの設定
                        //[1秒ごとのトレンドデータのみの設定]
                        //[数値データ1秒ごとのトレンドデータ出力先]
                        GlobalConstants.numericRawTrendDataOutputPath = txtFileContentQueue.Dequeue();
                        //[カテゴリーデータ1秒ごとのトレンドデータ出力先]
                        GlobalConstants.categoryRawTrendDataOutputPath = txtFileContentQueue.Dequeue();
                        #endregion

                #endregion
            

                #region 現場確認用切り替わり画像を保存する

                    #region オリジナル画像の保存
                    //現場確認用オリジナル画像の保存が必要かどうかが選べる
                    string wantOnSiteCheckOriginalPic= txtFileContentQueue.Dequeue().ToUpper();
                    if (wantOnSiteCheckOriginalPic == "YES")
                    {
                        GlobalConstants.wantOnSiteCheckOriginalPic = true;
                    }
                    else
                    {
                        GlobalConstants.wantOnSiteCheckOriginalPic = false;
                    }
                    //オリジナル画像の保存先
                    GlobalConstants.onSiteCheckOriginalPicPath = txtFileContentQueue.Dequeue();
                    //保存したい予兆/事後前後のオリジナル画像の枚数
                    GlobalConstants.beforeYocyo_SavePicNumber = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
                    GlobalConstants.afterYocyo_SavePicNumber = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
                    GlobalConstants.beforeJigo_SavePicNumber = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
                    //1枚後の画像の処理結果が出るまでの待ち時間
                    GlobalConstants.waitForNextPicInspectionResult_Interval=Convert.ToInt32(float.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture) * 1000);
                    #endregion


                    #region 処理結果画像の保存
                    //現場確認用処理結果画像の保存が必要かどうかが選べる
                    string wantOnSiteCheckHALCONResultPic = txtFileContentQueue.Dequeue().ToUpper();
                    if (wantOnSiteCheckHALCONResultPic == "YES")
                    {
                        GlobalConstants.wantOnSiteCheckHALCONResultPic = true;
                    }
                    else
                    {
                        GlobalConstants.wantOnSiteCheckHALCONResultPic = false;
                    }
                    //処理結果画像の保存先
                    GlobalConstants.onSiteCheckHALCONResultPicPath = txtFileContentQueue.Dequeue();
                    #endregion


                    

                #endregion
            #endregion





            #region 品種選択ソフトで選択した品種の記録

            //Local PCにある現時点適用中の品種を記録しているファイルのパス
            GlobalConstants.Local_GradeSelectedFilePath = txtFileContentQueue.Dequeue();
            #endregion



            #region オフライン検証用画像の保存枚数設定
            GlobalConstants.saveInitialPicNumber = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
            GlobalConstants.savePastValidationPicNumber = Convert.ToInt32(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);
            #endregion



            #region Halconルールベース画像処理

            //ルールベース画像処理を使う：Yes/ルールベース画像処理を使わない：No
            //To indicate whether we want to use ルールベース画像処理 to do the image inspection 
            string useRuleBasedImgProcessing = txtFileContentQueue.Dequeue().ToUpper();
            if (useRuleBasedImgProcessing == "YES")
            {
                GlobalConstants.useRuleBasedImgProcessing = true;
            }
            else
            {
                GlobalConstants.useRuleBasedImgProcessing = false;
            }
           

            //Halcon画像処理プログラムのパス
            GlobalConstants.HalconHDevPath = txtFileContentQueue.Dequeue();


            //将来画像処理を改良するための"画像を処理できない"、"ROI設置用ターゲットを認識できない"などの問題画像収集機能
            //問題画像の保存枚数を決める。
            GlobalConstants.collectProblemImgNumLimit = byte.Parse(txtFileContentQueue.Dequeue(), CultureInfo.InvariantCulture);



            //HALCON特徴量出力ファイルを生成する機能:
            //Halcon画像処理で各画像から取得した特徴量をCSVファイルに出力するかどうかを設定する
            string saveHalconCSV = txtFileContentQueue.Dequeue().ToUpper();
            if (saveHalconCSV == "YES")
            {
                GlobalConstants.saveHalconCSV = true;
            }
            else
            {
                GlobalConstants.saveHalconCSV = false;
            }


            //Halcon画像処理で各画像から取得した特徴量のCSVファイルの出力先
            GlobalConstants.HalconCSVOutputPath = txtFileContentQueue.Dequeue();

            #endregion


            #region AI画像処理
            //To indicate whether we want to use AI to do the image inspection 
            string useAIImgProcessings = txtFileContentQueue.Dequeue().ToUpper();
            if (useAIImgProcessings == "YES")
            {
                GlobalConstants.useAIImgProcessing = true;
            }
            else
            {
                GlobalConstants.useAIImgProcessing = false;
            }
            

            //Specify the file path of the AI model class label file 
            GlobalConstants.AIClassLabelFilePath= txtFileContentQueue.Dequeue();

            //Specify the file path of the AI model 
            GlobalConstants.AIModelFilePath=txtFileContentQueue.Dequeue();

            //Specify the name of the input layer of the AI model 
            GlobalConstants.AIModelInputLayerName=txtFileContentQueue.Dequeue();

            //Specify the name of the output layer of the AI model 
            GlobalConstants.AIModelOutputLayerName=txtFileContentQueue.Dequeue();


            //GPU Memory Limit of the AI model
            double GPUMemoryLimit = 0;
            if (double.TryParse(txtFileContentQueue.Dequeue(), out GPUMemoryLimit))
            {
                GlobalConstants.GPUMemoryLimit = GPUMemoryLimit;
            
            }
            //設定ファイルの値が数値ではない場合、
            //GPU Memory Limit なしとみなす。
            else
            {
                GlobalConstants.GPUMemoryLimit = 0;
            }
            

            


            //AI画像処理の前処理の切り出しROI画像を生成するためのHALCONプログラム保存先
            GlobalConstants.AI_Preprocessing_HalconHDevPath = txtFileContentQueue.Dequeue();

            #endregion

            #region どの画像処理の結果を警報に使うかを設定する
            //AI画像処理の結果のみを使うか
            //AI+ルールベース画像処理結果の組み合わせを使うか
            //ルールベース画像処理の結果のみを使うか
            //を決める
            string whichResultToUse = txtFileContentQueue.Dequeue().ToUpper();
            if (whichResultToUse == "A")
            {
                GlobalConstants.whichResultToUse = "UseAI";
            }
            else if (whichResultToUse == "B")
            {
                GlobalConstants.whichResultToUse = "UseAI+RuleBase";
            }
            else
            {
                GlobalConstants.whichResultToUse = "UseRuleBase";
            }
            

            #endregion

        /*Randomly output 切り替わり message for testing every 30 seconds
        #region Fake Image Inspction
        //Read in the outputAlertFileTimePeriod　of the fake image processing
        if (txtFileContentQueue.Count > 38)
        {
            GlobalConstants.outputAlertFileTimePeriod = 30;
        }
        #endregion

        
        */


            #region Don't need to read from 設定パラメーター.txt

                #region 効率的に情報ファイルと警報ファイルの履歴をbufferに貯めるためのbuffer buffer設定
                //効率的に情報ファイルと警報ファイルの履歴をbufferに貯めるための 
                //buffer buffer設定

                /*Comment out 情報ファイル履歴ファイルの出力
                GlobalConstants.infoFileHistoryLog_Length = GlobalConstants.infoFileFormat.Length;
                */

                GlobalConstants.alertFileHistoryLog_Length = GlobalConstants.alertFileHistoryRecordFormat.Length;
            
                /*Comment out 情報ファイル履歴ファイルの出力
                GlobalConstants.infoFileHistoryLogWriter_Capacity = ((GlobalConstants.saveAlertFileHistoryLogCSVTimeInterval * 3600) + GlobalConstants.capacityJustInCaseBuffer) * GlobalConstants.infoFileHistoryLog_Length;
                */

                GlobalConstants.alertFileHistoryLogWriter_Capacity = ((GlobalConstants.saveAlertFileHistoryLogCSVTimeInterval * 3600) + GlobalConstants.capacityJustInCaseBuffer) * GlobalConstants.alertFileHistoryLog_Length;
            #endregion



            #region 現場確認用切り替わり画像を保存する

                #region オリジナル画像の保存

                //最大限今の画像からkeepThisNumOfPicResult枚前の
                //画像処理結果記録Dictionary内の処理結果を保つ。
                //keepThisNumOfPicResult=初期値+Max(予兆前の保存枚数,予兆後の保存枚数,事後前の保存枚数)
                //現場確認用オリジナル画像の保存スピードが落ちる場合、必要な古い処理結果を削除させないように
                //初期値を足して余裕を作る。
                GlobalConstants.keepThisNumOfPicResult = GlobalConstants.keepThisNumOfPicResult + Math.Max(Math.Max(GlobalConstants.beforeYocyo_SavePicNumber, GlobalConstants.afterYocyo_SavePicNumber), GlobalConstants.beforeJigo_SavePicNumber);
                #endregion
            #endregion

            #endregion

        }


        //Read in 品種選択ソフトで選択した品種。
        public  static void getSelectedGrade()
        {
            //品種選択ソフトが適用されている場合のみ、
            //品種選択ソフトで選択した品種を読み込む。
            if (File.Exists(GlobalConstants.Local_GradeSelectedFilePath))
            {
                string fileType = "Grade.Selected";
                string errorMessage = GlobalConstants.Local_GradeSelectedFilePath + " の読み込みエラーが発生したが、\n1時間ごとのトレンドデータの備考欄に\n品種選択ソフトで選択した品種が記録できないだけです。\n\n\n他の機能に影響がないため、対応は不要です。\nこのメッセージを閉じていいです。";
                IEnumerable<string> lines = CreateReadFiles.readAllLinesOfAFile(GlobalConstants.Local_GradeSelectedFilePath, errorMessage, fileType);
                if(lines.First()!=null)
                {
                    GlobalConstants.selectedGradeName = lines.First();
                }
                
            }
            
        }




    }
}

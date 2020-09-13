using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ShopBridge.Helper
{
    public class AppLog
    {
        public static void WriteLog(string message, string methodName)
        {
            try
            {
                string LogLocation = ConfigurationManager.AppSettings["LogLocation"].ToString();
                string logFile = LogLocation + "\\ShopBridgeAppLog" + DateTime.Now.ToString("ddMMMyyyy").Trim() + ".txt";

                if (!Directory.Exists(LogLocation))
                {
                    Directory.CreateDirectory(LogLocation);
                }
                if (!File.Exists(logFile))
                {
                    File.Create(logFile).Close();
                }
                using (StreamWriter streamWriter = new StreamWriter(logFile, true))
                {
                    streamWriter.WriteLineAsync("Datetime : " + DateTime.Now + ", Method Name : " + methodName + ", Exception : " + message);
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
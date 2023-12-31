using System;
using System.IO;
using System.Reflection;

namespace Api.ValidToken.Log
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(path + "Logger\\" + "log.txt"))
                {
                    w.WriteLine("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), logMessage);
                }
            }
            catch
            {
            }
        }
    }
}

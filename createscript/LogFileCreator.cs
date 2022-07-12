using System;
using System.IO;
using System.Windows.Forms;

namespace createscript
{
    public class LogFileCreator
    {
        string logFilePath;
        public LogFileCreator()
        {
            var PathEXEfileRaw = System.Reflection.Assembly.GetEntryAssembly().Location;
            logFilePath = System.IO.Path.GetDirectoryName(PathEXEfileRaw)+"\\" + "log.txt";    
            CreateLogFile();
        }

        private void CreateLogFile()
        {
            try
            {
                if(!File.Exists(logFilePath))
                {
                    File.Create(logFilePath).Close();
                }
            }
            catch
            {
                MessageBox.Show($"There is no access to create the log file.");
            }
        }

        public DateTime GetCurrentDateAndTime()
        {
            DateTime now = DateTime.Now;
            return now;
        }

        public void WriteText(string inputText)
        {
            string text = $"{GetCurrentDateAndTime().ToString()}     {inputText}";

            using (StreamWriter w = File.AppendText(logFilePath))
            {
                w.WriteLine(text);
                w.Close();
            }
        }  
    }
}

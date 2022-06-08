using System;
using System.Diagnostics;
using System.Windows.Forms;
using createscript;

namespace myspace
{   
    class Myclass
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string model = args[0];
            int timeToWaitForOpening = Convert.ToInt32(args[1]);
            timeToWaitForOpening = timeToWaitForOpening*60000;

            LogFileCreator myLogFile = new LogFileCreator();

            TeklaModelChanger myModel = new TeklaModelChanger(args);

            myLogFile.WriteText($"Opening {model};");
            try 
            {
                myModel.OpenModel(); 
            }
            catch
            {
                myLogFile.WriteText($"FAILURE of Opening {model};");
            }

            myLogFile.WriteText($"Waiting for {model} is open;");
            System.Threading.Thread.Sleep(timeToWaitForOpening);

            try
            {
                myModel.GetMacroPath();
            }
            catch(Exception ex)
            {
                myLogFile.WriteText($"ERROR for {model}: {ex.Message};\n");
            }

            myLogFile.WriteText($"Read in for {model} is started");
            try
            {
                myModel.ReadInMacro();
                myLogFile.WriteText($"Waiting for Read In of {model} is finished;");
                while(!myModel.IsReadInFinished())
                {
                    System.Threading.Thread.Sleep(60000);//60 sec
                }
                myLogFile.WriteText($"Read In of {model} is finished;");
            }
            catch
            {
                myLogFile.WriteText($"FAILURE of Read In of {model};");
            }
            
            
            myLogFile.WriteText($"Saving {model};");
            try
            {
                myModel.SaveModelMacro();
            }
            catch
            {
                myLogFile.WriteText($"FAILURE of Saving {model};");
            }
            

            myLogFile.WriteText($"Closing {model};");
            try
            {
                myLogFile.WriteText($"Closing {model};");
                myModel.CloseTeklaModel();
            }
            catch
            {
                myLogFile.WriteText($"FAILURE of Closing {model};");
            }

            CloseTekla(myLogFile, model);
            System.Threading.Thread.Sleep(10000);
            System.Windows.Forms.Application.Exit();
        }         

        public static void CloseTekla(LogFileCreator myLogFile, string model)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("TeklaStructures");
                foreach(var process in processes)
                {
                    process.Kill();
                }
            }
            catch
            {
                myLogFile.WriteText($"Closing of {model} was failed;");
            }
        } 
    }
}
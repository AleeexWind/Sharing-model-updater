using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;

namespace createscript
{
    public class TeklaModelChanger
    {
        private string FilePath;
        static StreamWriter file;
        private static string macroPath;

        string target1;
        string target2;
        string target3;

        public TeklaModelChanger(string[] args)
        {
            SetTargets(args);
            GetModelHistoryFilePath();
        }

        public void OpenModel()
        {
            var proc = new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = @"C:\Windows\System32",
                FileName = @"C:\Windows\System32\cmd.exe",

                Arguments = $"/c {target1} " + $"-I \"{target2}\" " + $" \"{target3}\" ",
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(proc);
            
        }

        private void SetTargets(string[] args)
        {
            string[] rdiAndModel = args[0].Split(',');

            target1 = args[2];
            target2= args[3];
            target3= rdiAndModel[1];
          
        }

        public void GetModelHistoryFilePath()
        {
            string modelDBfilePath = Path.Combine(target3, "history.db");

            FilePath = modelDBfilePath;
        }

        public void CloseTeklaModel()
        {
            ModelHandler myModel = new ModelHandler();
            myModel.Close();
        }


        public DateTime? GetFileTime(string path)
        {
            DateTime? dt;
            try
            {
                dt = File.GetLastWriteTime(path);
                return dt;
            }
            catch
            {
                MessageBox.Show("Error. There is no access to the file.");
                dt = null;
                return dt;          
            }
        }

        public bool IsReadInFinished()
        {
            bool isFinished = false;
            DateTime? firstCheckDate;
            DateTime? secondCheckDate;
                
            firstCheckDate = GetFileTime(FilePath);
            System.Threading.Thread.Sleep(120000); //120sec
            secondCheckDate = GetFileTime(FilePath);

            if(firstCheckDate!=null&&secondCheckDate!=null)
            {
                if(firstCheckDate==secondCheckDate)
                {
                    isFinished = true;
                }
            }           
            return isFinished;
        }

        public void ReadInMacro()
        {   
            file = new StreamWriter(macroPath + "+readIn.cs");   
            file.WriteLine("#pragma warning disable 1633 // Unrecognized #pragma directive");
            file.WriteLine("#pragma reference \"Tekla.Macros.Wpf.Runtime\"");
            file.WriteLine("#pragma reference \"Tekla.Macros.Akit\"");
            file.WriteLine("#pragma reference \"Tekla.Macros.Runtime\"");
            file.WriteLine("#pragma warning restore 1633 // Unrecognized #pragma directive");
            file.WriteLine("namespace UserMacros {");
            file.WriteLine("    public sealed class Macro {");
            file.WriteLine("        [Tekla.Macros.Runtime.MacroEntryPointAttribute()]");
            file.WriteLine("        public static void Run(Tekla.Macros.Runtime.IMacroRuntime runtime) {");
            file.WriteLine("            Tekla.Macros.Akit.IAkitScriptHost akit = runtime.Get<Tekla.Macros.Akit.IAkitScriptHost>();");
            file.WriteLine("            Tekla.Macros.Wpf.Runtime.IWpfMacroHost wpf = runtime.Get<Tekla.Macros.Wpf.Runtime.IWpfMacroHost>();");
            file.WriteLine("            wpf.InvokeCommand(\"CommandRepository\", \"Sharing.ReadIn\");");
            file.WriteLine("            wpf.Dialog().As.Window.Close();");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");
            file.Close();

            Operation.RunMacro("+readIn.cs");
            System.Threading.Thread.Sleep(30000);//30 sec
        }

        public void SaveModelMacro()
        {   
            file = new StreamWriter(macroPath + "+saveModel.cs");    
            file.WriteLine("#pragma warning disable 1633 // Unrecognized #pragma directive");
            file.WriteLine("#pragma reference \"Tekla.Macros.Wpf.Runtime\"");
            file.WriteLine("#pragma reference \"Tekla.Macros.Akit\"");
            file.WriteLine("#pragma reference \"Tekla.Macros.Runtime\"");
            file.WriteLine("#pragma warning restore 1633 // Unrecognized #pragma directive");
            file.WriteLine("namespace UserMacros {");
            file.WriteLine("    public sealed class Macro {");
            file.WriteLine("        [Tekla.Macros.Runtime.MacroEntryPointAttribute()]");
            file.WriteLine("        public static void Run(Tekla.Macros.Runtime.IMacroRuntime runtime) {");
            file.WriteLine("            Tekla.Macros.Akit.IAkitScriptHost akit = runtime.Get<Tekla.Macros.Akit.IAkitScriptHost>();");
            file.WriteLine("            Tekla.Macros.Wpf.Runtime.IWpfMacroHost wpf = runtime.Get<Tekla.Macros.Wpf.Runtime.IWpfMacroHost>();");
            file.WriteLine("            wpf.InvokeCommand(\"CommandRepository\", \"Common.Save\");");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");
            file.Close();

            Operation.RunMacro("+saveModel.cs");
            System.Threading.Thread.Sleep(30000);//30 sec
        }

        public void GetMacroPath()
        {
            Tekla.Structures.TeklaStructuresSettings.GetAdvancedOption("XS_MACRO_DIRECTORY", ref macroPath);
            if(macroPath.Length != 0)
            {
                macroPath = macroPath.Split(';').First();
                if(macroPath[macroPath.Length-1] == '\\')
                {
                    macroPath = macroPath + "modeling\\";
                }
                else
                {
                    macroPath = macroPath + "\\modeling\\";
                }              
            }
            else
            {
                throw new Exception("The Advanced option \"XS_MACRO_DIRECTORY\" is empty in Tekla. Please fill it with a path for macro usage.");
            }
        }
    }
}

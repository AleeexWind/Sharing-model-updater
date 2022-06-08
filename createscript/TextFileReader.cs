using System.IO;
using System.Collections.Generic;
using System;

namespace createscript
{
    public interface ITextFileReadable
    {
        void ReadFile();
        List<string> GetListFromFile();
    }

    public class TextFileReader : ITextFileReadable
    {
        private List<string> list = new List<string>();
        private string fileName;

        public TextFileReader(string fileName)
        {   
            this.fileName = fileName;
        }
        private string GetTextFilePath()
        {
            var exeFilePathRaw = System.Reflection.Assembly.GetEntryAssembly().Location;
            return System.IO.Path.GetDirectoryName(exeFilePathRaw) + "\\" + fileName;
        }
        public void ReadFile()
        {
            string textFilePath = GetTextFilePath();
            try
            {
                using (StreamReader reader = new StreamReader(textFilePath))
                {  
                    var lines = File.ReadAllLines(textFilePath);
                    foreach(string line in lines)
                    { 
                        list.Add(line);
                    }
                }
            }
            catch
            {
                throw new Exception($"Reading of the file {textFilePath} was failed");
            }
        }
        public List<string> GetListFromFile()
        {
            return list;
        }

        public static string[] CleanBlankSpacesFromStartAndEnd(string[] settings)
        {   
            string[] cleanedSettings = new string[settings.Length];
            string temp = "";
            int index = 0;
            foreach(var item in settings)
            {   
                temp = item;
                while(temp[0].Equals(' '))
                {
                    temp = CleanBlankSpasesFromStart(temp);
                }
                while(temp[temp.Length-1].Equals(' '))
                {
                    temp = CleanBlankSpasesFromEnd(temp);
                }
                cleanedSettings[index] = temp;
                index++;
            }  
            return cleanedSettings;
        }
        public static string CleanBlankSpasesFromStart(string value)
        {
            value = value.Remove(0,1);
            return value;
        }
        public static string CleanBlankSpasesFromEnd(string value)
        {
            int index = value.Length-1;
            value = value.Remove(index,1);
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace createscript
{
    interface ISettingReadable
    {
        Dictionary<string, string> ReadSettings(List<string> settingsListFromReader);
    }

    class TeklaEXEsettingReader : ISettingReadable
    {
        public Dictionary<string, string> ReadSettings(List<string> settingsListFromReader)
        {       
            string exceptionMessege = "The setting with the TeklaStructures.exe path has not been read.\nPlease check settings.ini file and reopen the application.";
            string settingName = @"XS_TEKLA(....)_PATH";
            Dictionary<string, string> teklaEXEsettingDictionary = new Dictionary<string, string>(SettingsHandler.GetSpecificSettingsDictionary(settingsListFromReader, exceptionMessege, settingName));
            if(teklaEXEsettingDictionary.Count == 0)
            {
                throw new Exception(exceptionMessege);
            }
            return SettingsHandler.GetSpecificSettingsDictionary(settingsListFromReader, exceptionMessege, settingName);
        }
    }
    class BypassSettingReader : ISettingReadable
    {
        public Dictionary<string, string> ReadSettings(List<string> settingsListFromReader)
        {
            string exceptionMessege = "The setting with the Bypass.ini path has not been read.\nPlease check settings.ini file and reopen the application.";
            string settingName = @"XS_TEKLA(....)_BYPASS_PATH";
            Dictionary<string, string> BypassSettingDictionary = new Dictionary<string, string>(SettingsHandler.GetSpecificSettingsDictionary(settingsListFromReader, exceptionMessege, settingName));
            if(BypassSettingDictionary.Count == 0)
            {
                throw new Exception(exceptionMessege);
            }
            return SettingsHandler.GetSpecificSettingsDictionary(settingsListFromReader, exceptionMessege, settingName);
        }
    }

    public static class SettingsHandler
    {
        private static Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
        public static void ReadSettings(List<string> settingsListFromReader)
        {
            ISettingReadable teklaEXEsettingsReader = new TeklaEXEsettingReader();
            Dictionary<string, string> teklaEXEsettings = new Dictionary<string, string>(teklaEXEsettingsReader.ReadSettings(settingsListFromReader));

            ISettingReadable bypassSettingsReader = new BypassSettingReader();
            Dictionary<string, string> bypassSettings = new Dictionary<string, string>(bypassSettingsReader.ReadSettings(settingsListFromReader));

            teklaEXEsettings.ToList().ForEach(x => settingsDictionary.Add(x.Key, x.Value));
            bypassSettings.ToList().ForEach(x => settingsDictionary.Add(x.Key, x.Value));
        }

        public static Dictionary<string, string> GetSpecificSettingsDictionary(List<string> settingsListFromReader, string exceptionMessege, string settingName)
        {
            Dictionary<string, string> settingDic = new Dictionary<string, string>();

            if(settingsListFromReader.Count == 0)
            {
                throw new Exception(exceptionMessege);
            }

            List<string> settingsList = settingsListFromReader;

            Regex regex = new Regex(settingName);
            bool failed = false;
            foreach(var setting in settingsList)
            {
                if(failed){break;}
                if(regex.IsMatch(setting))
                {
                    string[] stringItems = setting.Split('=');     
                    try
                    {
                        stringItems = TextFileReader.CleanBlankSpacesFromStartAndEnd(stringItems);
                        settingDic.Add(stringItems[0], stringItems[1]);
                    }
                    catch
                    {   
                        failed = true;
                        throw new Exception(exceptionMessege);     
                    } 
                }
            }
            return settingDic;
        }

        public static string GetTeklaEXEPath(string scope)
        {
            bool found = false;
            foreach(var settingName in settingsDictionary.Keys)
            {
                if(settingName.Contains($"TEKLA{scope}_PATH"))
                {
                    return settingsDictionary[settingName];
                } 
            }
            if(!found)
            {
                throw new Exception("The settings.ini file does not contain a setting which supports the TeklaStructures.exe version listed in \"List of model pathes.txt\"");
            }

            return "";
        }
        public static string GetBypassPath(string scope)
        {
            bool found = false;
            foreach(var settingName in settingsDictionary.Keys)
            {
                if(settingName.Contains($"XS_TEKLA{scope}_BYPASS_PATH"))
                {
                    found = true;
                    return settingsDictionary[settingName];
                }
            }
            if(!found)
            {
                throw new Exception("The settings.ini file does not contain a setting which supports the Bypass.ini version listed in \"List of model pathes.txt\"");
            }
            return "";
        }
    }
}

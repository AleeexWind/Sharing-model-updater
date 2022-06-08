using System;
using System.Collections.Generic;
using System.Windows.Forms;
using createscript;

namespace myspace
{   
    class Myclass
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ITextFileReadable settingsListReader = new TextFileReader("settings.ini");

            try
            {   
                settingsListReader.ReadFile();

                SettingsHandler.ReadSettings(settingsListReader.GetListFromFile());
                Application.Run(new Form1());
            }
            catch(Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}\n");
            }
        }
    }
}
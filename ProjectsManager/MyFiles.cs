using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ProjectsManager
{
    static class MyFiles
    {
        public static string Settings = MyDirectories.Main + "\\" + "Settings.xml";
        public static string ExceptionFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\exceptionFile.txt";
    }
}

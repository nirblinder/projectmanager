using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ProjectsManager
{
    static class MyDirectories
    {
        public static string Main = Path.GetDirectoryName(Application.ExecutablePath);
        //public static string Export;
    }
}

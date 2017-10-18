using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ProjectsManager
{
    static class MySettings
    {
        public static void CreateFile()
        {
            String xmlFile = MyFiles.Settings;
            if (!File.Exists(xmlFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement xmlRoot = xmlDoc.CreateElement("Settings");
                xmlDoc.AppendChild(xmlRoot);

                XmlElement DBConnection = xmlDoc.CreateElement("DBConnection");
                DBConnection.SetAttribute("Server", "localhost");
                DBConnection.SetAttribute("Database", "projectmanager");
                DBConnection.SetAttribute("Username", "root");
                DBConnection.SetAttribute("Password", "bnr");
                xmlRoot.AppendChild(DBConnection);

                xmlDoc.Save(xmlFile);
            }
        }
        public static String getServer()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Server");
            }
            catch { return "false"; }
        }
        public static void setServer(string server)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Server", server);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
        public static String getDatabase()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Database");
            }
            catch { return "false"; }
        }
        public static void setDatabase(string db)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Database", db);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
        public static String getUsername()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Username");
            }
            catch { return "false"; }
        }
        public static void setUsername(string username)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Username", username);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
        public static String getPassword()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Password");
            }
            catch { return "false"; }
        }
        public static void setPassword(String password)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Password", password);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
        public static String getHeight()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Height");
            }
            catch { return "false"; }
        }
        public static String getWidth()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Width");
            }
            catch { return "false"; }
        }
        public static void setSize(String height, String width)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Height", height);
                elmNode.SetAttribute("Width", width);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
        public static String getY()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("Y");
            }
            catch { return "false"; }
        }
        public static String getX()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                return elmNode.GetAttribute("X");
            }
            catch { return "false"; }
        }
        public static void setLocation(String y, String x)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(MyFiles.Settings);
                XmlNodeList nodeList = doc.GetElementsByTagName("DBConnection");
                XmlElement elmNode = (XmlElement)nodeList[0];
                elmNode.SetAttribute("Y", y);
                elmNode.SetAttribute("X", x);
                doc.Save(MyFiles.Settings);
            }
            catch { }
        }
    }
}

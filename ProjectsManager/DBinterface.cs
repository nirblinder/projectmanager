using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ProjectsManager
{
    public class DBinterface
    {
        private MySqlConnection mysqlCon;
        public connectionSettings conSettings;
        public string exportLibrary;
        public string importLibrary;
        public struct connectionSettings
        {
            public string server;
            public string database;
            public string username;
            public string password;
        }
        public void LoadSettings()
        {
            conSettings.server = MySettings.getServer();
            conSettings.database = MySettings.getDatabase();
            conSettings.username = MySettings.getUsername();
            conSettings.password = MySettings.getPassword();
            exportLibrary = this.loadExportLibrary();
            importLibrary = this.loadImportLibrary();
        }
        public void SaveSettings()
        {
            MySettings.setServer(conSettings.server);
            MySettings.setDatabase(conSettings.database);
            MySettings.setUsername(conSettings.username);
            MySettings.setPassword(conSettings.password);
        }
        private bool ConnectToDB()
        {
            string strProvider = "Data Source=" + conSettings.server + ";Database=" + conSettings.database + ";User ID=" + conSettings.username +";Password=" + conSettings.password;
            try
            {
                mysqlCon = new MySqlConnection(strProvider);
                mysqlCon.Open();
                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                return false;
            }
        }
        private bool DisconnectFromDB()
        {
            try
            {
                mysqlCon.Close();
                return true;
            }
            catch { return false; }
        }
        public bool ConnecivityCheck(string server, string database, string username, string password)
        {
            MySqlConnection mysqlConTemp;
            string strProvider = "Data Source=" + server + ";Database=" + database + ";User ID=" + username + ";Password=" + password;
            try
            {
                mysqlConTemp = new MySqlConnection(strProvider);
                mysqlConTemp.Open();
                return true;
            }
            catch { return false; }
        }
        public bool Insert(string table, string columns, string values) //comma seperated columns and values
        {
            string insertSQL = "INSERT INTO " + table + " (" + columns + ") VALUES ('" + values + "')";
            if (ConnectToDB())
            {
                MySqlCommand cmd = new MySqlCommand(insertSQL, mysqlCon);
                try
                {
                    int executed = cmd.ExecuteNonQuery();
                    if (executed <= 0)
                    {
                        DisconnectFromDB();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                    return false;
                }
                DisconnectFromDB();
                return true;
            }
            else
            {
                DisconnectFromDB();
                return false;
            }
        }
        public bool InsertSql(string query) //comma seperated columns and values
        {
            if (ConnectToDB())
            {
                MySqlCommand cmd = new MySqlCommand(query, mysqlCon);
                try
                {
                    int executed = cmd.ExecuteNonQuery();
                    if (executed <= 0)
                    {
                        DisconnectFromDB();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                    return false;
                }
                DisconnectFromDB();
                return true;
            }
            else
            {
                DisconnectFromDB();
                return false;
            }
        }
        public bool Update(string query)
        {
            string updateSQL = query;
            if (ConnectToDB())
            {
                MySqlCommand cmd = new MySqlCommand(updateSQL, mysqlCon);
                try
                {
                    int executed = cmd.ExecuteNonQuery();
                    if (executed <= 0)
                    {
                        DisconnectFromDB();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                }
                DisconnectFromDB();
                return true;
            }
            else
            {
                DisconnectFromDB();
                return false;
            }
        }
        public DataSet Select(string query)
        {
            DataSet ds = new DataSet();
            string insertSQL = query;
            if (ConnectToDB())
            {
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(query, mysqlCon);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                }

                DisconnectFromDB();
                return ds;
            }
            else
            {
                DisconnectFromDB();
                return new DataSet();
            }
        }
        public bool Delete(string table, string column, string value)
        {
            string deleteSQL = "DELETE FROM " + table + " WHERE " + column +" = '" + value +"'";
            if (ConnectToDB())
            {
                MySqlCommand cmd = new MySqlCommand(deleteSQL, mysqlCon);
                try
                {
                    int executed = cmd.ExecuteNonQuery();
                    if (executed <= 0)
                    {
                        DisconnectFromDB();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
                }
                DisconnectFromDB();
                return true;
            }
            else
            {
                DisconnectFromDB();
                return false;
            }
        }
        private string loadExportLibrary()
        {
            try
            {
                return this.Select("SELECT library FROM export").Tables[0].Rows[0][0].ToString().Replace("//", "\\");
            }
            catch
            {
                return "";
            }
        }
        public bool saveExportLibrary(string text)
        {
            this.exportLibrary = text;
            text = text.Replace("\\", "//");
            return this.Update("UPDATE export SET library='" + text + "' WHERE id=1");
        }
        private string loadImportLibrary()
        {
            try
            {
                return this.Select("SELECT library FROM export").Tables[0].Rows[1][0].ToString().Replace("//", "\\");
            }
            catch
            {
                return "";
            }
        }
        public bool saveImportLibrary(string text)
        {
            this.importLibrary = text;
            text = text.Replace("\\", "//");
            return this.Update("UPDATE export SET library='" + text + "' WHERE id=2");
        }
    }
}

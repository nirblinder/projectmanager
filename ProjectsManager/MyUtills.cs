using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ProjectsManager
{
    static class MyUtills
    {
        public static String dateToSQL(DateTime date)
        {
            return date.Year.ToString().PadLeft(4, '0') + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Day.ToString().PadLeft(2, '0');
        }
        public static DateTime dateFromSQL(String date)
        {
            return Convert.ToDateTime(date);
            //return new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(5, 2)), int.Parse(date.Substring(8, 2)));
        }
        public static string ConvertToMd5(string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}

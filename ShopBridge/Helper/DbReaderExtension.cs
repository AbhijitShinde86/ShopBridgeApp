using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBridge.Helper
{
    public static class DbReaderExtension
    {
        public static string GetSafeString(this SqlDataReader reader, int colIndex)
        {
            return !reader.IsDBNull(colIndex) ? reader.GetString(colIndex) : string.Empty;
        }
        public static string ToEmptyString(this string str)
        {
            return str == null ? string.Empty : str;
        }

        public static object TonullToDBNull(this object obj)
        {
            return obj == null ? DBNull.Value : obj;
        }

        public static object TonullToDBNullInt(this object obj)
        {
            return obj != null ? Convert.ToInt32(obj) <= 0 ? DBNull.Value : obj : obj;
        }

        public static bool GetSafeBool(this SqlDataReader reader, int colIndex)
        {
            return !reader.IsDBNull(colIndex) && reader.GetBoolean(colIndex);
        }

        public static DateTime? GetSafeNullableDateTime(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDateTime(colIndex);
            }
            return null;
        }

        public static int GetSafeInt(this SqlDataReader reader, int colIndex)
        {
            return !reader.IsDBNull(colIndex) ? reader.GetInt32(colIndex) : -1;
        }

        public static double GetSafeDouble(this SqlDataReader reader, int colIndex)
        {
            return !reader.IsDBNull(colIndex) ? reader.GetDouble(colIndex) : -1;
        }
    }
}
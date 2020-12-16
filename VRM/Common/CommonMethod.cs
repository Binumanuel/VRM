using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRM.Common
{
    public static class CommonMethod
    {
        /// <summary>
        /// This method is used to handle the null integer value.
        /// </summary>
        /// <param name="objString">integer value parameter</param>
        /// <param name="defaultValue">result</param>
        /// <returns></returns>
        public static string ToStr(this object objString, string defaultValue = "")
        {
            if (objString == null) return defaultValue;
            return Convert.ToString(objString);
        }

        /// <summary>
        /// This method is used to formated the date using substring.
        /// </summary>
        /// <param name="objDate">substring value</param>
        /// <param name="defaultValue">formatted date</param>
        /// <returns></returns>
        public static string ToFormatedDateSubStr(this object objDate, string defaultValue = "")
        {
            if (objDate == null) return defaultValue;
            string dateValue = objDate.ToStr();
            if (dateValue.Length == 8)
            {
                string year = dateValue.Substring(0, 4);
                string mm = dateValue.Substring(4, 2);
                string dd = dateValue.Substring(6, 2);
                return defaultValue = mm + "/" + dd + "/" + year;
            }
            return defaultValue;
        }

        public static int ToInt(this object objInt, int defaultValue = 0)
        {
            if (objInt == null) return defaultValue;
            int.TryParse(objInt.ToStr(), out defaultValue);
            return defaultValue;
        }

        public static string ToFormatedDateStr(this object objDate, string defaultValue = "")
        {
            if (objDate == null) return defaultValue;
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(objDate.ToStr(), out dt))
            {
                return dt.ToString("MM/dd/yyyy");
            }
            return defaultValue;
        }
    }
}
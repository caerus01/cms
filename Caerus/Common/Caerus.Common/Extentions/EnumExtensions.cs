using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Extentions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static string GetDescription(Type enumType, object val)
        {
            var memInfo = enumType.GetMember(val.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return val.ToString();
        }

        /// <summary>
        /// Converts an enum to int [be ware: this conversion does not work in EF queries, rather use (int)myEnum ]
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int AsInt(this Enum e)
        {
            return Convert.ToInt32(e);
        }

        /// <returns>The <see cref="System.String"/> based decription.</returns>
        public static string ToEnumerationDescription(this Enum value)
        {
            return value.ToString().ExpandCamelCaseWord();
        }


        /// <summary>
        /// Get a List of the items in an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>a list</returns>
        public static List<T> GetMeAlistOf<T>()
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>();
            return list.ToList();
        }
    }
}

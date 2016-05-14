using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Extentions
{
    public static class StringExtensions
    { /// <returns></returns>
        public static int AsInt(this string str)
        {
            int result;
            return int.TryParse(str, out result)
                ? result
                : 0;
        }

        public static long AsLong(this string str)
        {
            long result;
            return long.TryParse(str, out result)
                ? result
                : 0;
        }

        public static decimal AsDecimal(this string str)
        {
            decimal result;
            return decimal.TryParse(str, out result)
                ? result
                : 0;
        }

        public static DateTime AsDate(this string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result)
                ? result
                : DateTime.MinValue;
        }

        public static string TrimLeft(this string str, int lengthValue)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            if (str.Trim().Length > lengthValue)
            {
                return str.Trim().Substring(0, lengthValue);
            }

            return str.Trim();
        }

        /// <summary>
        /// Expands the camel case word.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The expanded string.</returns>
        public static string ExpandCamelCaseWord(this string target)
        {
            if (!string.IsNullOrEmpty(target))
            {
                var sb = new StringBuilder(target[0].ToString());
                for (var i = 1; i < target.Length; i++)
                {
                    if (char.IsUpper(target[i]) && target[i - 1] != ' ' && !char.IsUpper(target[i - 1]))
                    {
                        sb.Append(" ");
                    }

                    sb.Append(target[i]);
                }

                return sb.ToString();
            }

            return target;
        }

        /// <summary>
        /// Corrects the casing to normal.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string CorrectCasingToNormal(this string target)
        {
            if (!string.IsNullOrEmpty(target))
            {
                var sb = new StringBuilder(Char.ToUpper(target[0]).ToString(CultureInfo.InvariantCulture));
                for (var i = 1; i < target.Length; i++)
                {
                    if (Char.IsLetter(target[i]) && target[i - 1] != ' ') sb.Append(char.ToLower(target[i]).ToString(CultureInfo.InvariantCulture));
                    else sb.Append(char.ToUpper(target[i]).ToString(CultureInfo.InvariantCulture));
                }
                return sb.ToString();
            }
            return target;
        }

        /// <summary>
        /// Extracts the surname and initials.
        /// </summary>
        /// <param name="surname">The surname.</param>
        /// <param name="middlenames">The middlenames.</param>
        /// <param name="firstName">The first name.</param>
        /// <returns>The correctly formatted result.</returns>
        public static string ExtractSurnameAndInitials(
            this string surname,
            string middlenames,
            string firstName)
        {
            var stringBuilder = new StringBuilder(CorrectCasingToNormal(surname.Trim()));
            stringBuilder.Append(" ");

            if (!string.IsNullOrEmpty(firstName.Trim()))
            {
                stringBuilder.Append(Char.ToUpper(firstName[0]));
            }

            if (!string.IsNullOrEmpty(middlenames))
            {
                if (!string.IsNullOrEmpty(middlenames.Trim()))
                {
                    var names = middlenames.Split(new char[] { ' ' });
                    foreach (var targetName in names.Where(targetName => !string.IsNullOrEmpty(targetName)))
                    {
                        stringBuilder.Append(Char.ToUpper(targetName[0]));
                    }
                }


            }
            return stringBuilder.ToString();
        }

        public static string ExtractInitials(this string firstname, string middleNames = null)
        {
            var initials = string.Empty;
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                initials = firstname.Substring(0, 1);
            }
            if (!string.IsNullOrWhiteSpace(middleNames))
            {
                if (middleNames.Contains(' '))
                {
                    var names = middleNames.Split(' ');
                    foreach (var name in names)
                    {
                        initials += name.Substring(0, 1).ToUpper();
                    }
                }
                else if (middleNames.Contains(','))
                {
                    var names = middleNames.Split(',');
                    foreach (var name in names)
                    {
                        initials += name.Substring(0, 1).ToUpper();
                    }
                }
                else
                {
                    initials = !string.IsNullOrEmpty(firstname) ? firstname.Substring(0, 1) : string.Empty;
                }
            }
            return initials;
        }


        /// <summary>
        /// Replaces the commas with ASCII.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ReplaceCommasWithAscii(this string source)
        {
            return source.Replace(",", "&054;");
        }



        public static string ConvertToUtf8(this string original)
        {
            var bytes = Encoding.Default.GetBytes(original);
            original = Encoding.UTF8.GetString(bytes);
            return original;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }


        public static string[] Wrap(this string text, int max)
        {
            var charCount = 0;
            var lines = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return lines.GroupBy(w => (charCount += (((charCount % max) + w.Length + 1 >= max)
                            ? max - (charCount % max) : 0) + w.Length + 1) / max)
                        .Select(g => string.Join(" ", g.ToArray()))
                        .ToArray();
        }

        public static bool ToBoolean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            bool result;
            return bool.TryParse(str, out result) && result;
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <param name="ignorecase">if set to <c>true</c> it will [ignore the case] of the value..</param>
        /// <returns>
        /// The Enum corresponding to the extended type.
        /// </returns>
        /// <example>
        /// TestEnum foo = "Test".EnumParse<TestEnum>();
        /// </example>
        public static T EnumParse<T>(this string value, bool ignorecase)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (value.Length == 0)
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", "value");
            }

            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "T");
            }
            return (T)Enum.Parse(t, value, ignorecase);
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <returns>The Enum corresponding to the extended type.</returns>
        /// <example>
        /// TestEnum foo = "Test".EnumParse<TestEnum>();
        /// </example>
        public static T EnumParse<T>(this string value)
        {
            return value.EnumParse<T>(false);
        }

       
    }
}

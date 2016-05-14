using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Extentions
{
    public static class IntegerExtensions
    {        /// <summary>
        /// Parses a integer into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">Integer value to parse</param>
        /// <returns>The Enum corresponding to the extended type.</returns>
        /// <example>
        /// TestEnum foo = "Test".EnumParse<TestEnum>();
        /// </example>
        public static T EnumParse<T>(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture).EnumParse<T>(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Extentions
{
   public static class BooleanExtensions
    {
        /// <summary>
        /// Converts a boolean value to an integer (as used in SQL)
        /// </summary>
        /// <remarks>in SQL: false=0, true = 1</remarks>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int AsInt(this Boolean b)
        {
            return b ? 1 : 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Enums
{
    public enum JsonContractResolver
    {
        Default = 0,
        CamelCase = 1,
        SnakeCase = 2,
        LowerCase = 3,
        Indented
    }
}

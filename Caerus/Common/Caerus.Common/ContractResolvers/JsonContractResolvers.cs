using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Caerus.Common.ContractResolvers
{
    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            var startUnderscores = System.Text.RegularExpressions.Regex.Match(propertyName, @"^_+");
            return startUnderscores + System.Text.RegularExpressions.Regex
              .Replace(propertyName, @"([A-Z0-9])", "_$1").ToLower().TrimStart('_');
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}

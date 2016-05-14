using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Tools
{
    public static class HttpTools
    {
        public static StringBuilder CreateParameterString(Dictionary<string, string> paramterList, string startValue = "", string endvalue = "", string queryStringPrefix = @"?", string pairPrefix = @"=", string pairSeperator = @"&")
        {
            var requestParameters = new StringBuilder(startValue); //  = /
            var firstDone = false;
            if (paramterList.Count > 0)
            {
                requestParameters.Append(queryStringPrefix); //  = ?

                foreach (var item in paramterList)
                {
                    if (firstDone)
                    {
                        requestParameters.Append(pairSeperator); // = &
                    }
                    else
                    {
                        firstDone = true;
                    }
                    requestParameters.Append(item.Key);
                    requestParameters.Append(pairPrefix); // = =
                    requestParameters.Append(item.Value);
                }
            }

            requestParameters.Append(endvalue); //empty

            return requestParameters;
        }

        public static string SerializeFormToString(NameValueCollection formKeys)
        {
            var builder = new StringBuilder();

            if (formKeys.HasKeys() && formKeys.Count > 0)
                for (int i = 0; i < formKeys.Count; i++)
                {
                    builder.AppendLine(string.Format(@"{0}:{1}|", formKeys.AllKeys[i], formKeys[i]));
                }

            return builder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Interfaces;
using Caerus.Common.ViewModels;
using RestSharp;
using RestSharp.Authenticators;

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

        public class RestService : IDisposable
        {
            private RestClient _webClient;
            private RestRequest _request;
            public RestService(string serviceUrl, Method method, DataFormat format = DataFormat.Json, int timeout = 120000, IAuthenticator authenticator = null)
            {
                _webClient = new RestClient(serviceUrl);
                if (authenticator != null)
                    _webClient.Authenticator = authenticator;

                _request = new RestRequest(method) { RequestFormat = format, Timeout = timeout };
            }

            public void AddCertificate(X509Certificate certificate)
            {
                _webClient.ClientCertificates.Add(certificate);
            }

            public RestRequest CurrentRequest
            {
                get { return _request; }
            }

            public T Execute<T>(string action) where T : new()
            {
                _request.Resource = action;

                var response = _webClient.Execute<T>(_request);
                if (response.ErrorException == null) return response.Data;

                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, response.ErrorException);
                throw ex;
            }


            public IRestResponse Execute(string action)
            {
                _request.Resource = action;

                var response = _webClient.Execute(_request);
                if (response.ErrorException == null) return response;

                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, response.ErrorException);
                throw ex;
            }

            public void Dispose()
            {
                _request = null;
                _webClient = null;
            }
        }
    }


}

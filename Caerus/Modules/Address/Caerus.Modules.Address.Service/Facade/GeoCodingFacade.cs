using System;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Client.ViewModels;
using Caerus.Common.Modules.Geocode.ValueObjects;
using Caerus.Common.Modules.Session.Interfaces;

namespace Caerus.Modules.GeoCode.Service.Facade
{
    public class GeoCodingFacade
    {
        private ICaerusSession _session;
        private string googleApiKey;
        private string googleApiUrl;
        public GeoCodingFacade(ICaerusSession session)
        {
            _session = session;
        }

        public AddressViewModel GeoCodeAddress(AddressViewModel address)
        {
            try
            {

                if (string.IsNullOrEmpty(googleApiKey) || string.IsNullOrEmpty(googleApiUrl))
                {
                    address.ReplyMessage = "Failed to initialize GeoFacade";
                    address.ReplyStatus = ReplyStatus.Error;
                    return address;
                } var url = googleApiUrl + "?address=";

                if (!string.IsNullOrEmpty(address.AddressLine))
                    url += address.AddressLine.Replace(" ", "+").Replace("\n", "").Replace("\r", "");
                if (!string.IsNullOrEmpty(address.Suburb))
                    url += address.Suburb.Replace(" ", "+").Replace("\n", "").Replace("\r", "");
                if (!string.IsNullOrEmpty(address.City))
                    url += "," + address.City.Replace(" ", "+");
                if (!string.IsNullOrEmpty(address.ProvinceName))
                    url += "," + address.ProvinceName.Replace(" ", "+");
                if (!string.IsNullOrEmpty(address.Zip))
                    url += "," + address.Zip.Replace(" ", "+");

                url += "&key=" + googleApiKey;

                var client = new HttpClient();
                client.BaseAddress = new Uri(googleApiUrl);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseObject = new GeocodeResponse();
                    XmlSerializer serializer = new XmlSerializer(typeof(GeocodeResponse));
                    responseObject = (GeocodeResponse)serializer.Deserialize(response.Content.ReadAsStreamAsync().Result);
                    if (responseObject != null && responseObject.status == "OK")
                    {
                        var geo = responseObject.result.FirstOrDefault();
                        if (geo != null)
                        {
                            var loc = geo.geometry.FirstOrDefault();
                            if (loc != null)
                            {
                                var coords = loc.location.FirstOrDefault();
                                if (coords != null)
                                {
                                    address.Latitude = coords.lat;
                                    address.Longitude = coords.lng;
                                    return address;
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(address.Longitude) || string.IsNullOrEmpty(address.Latitude))
                    {
                        address.ReplyMessage = "Failed to decode address";
                        address.ReplyStatus = ReplyStatus.Error;
                        _session.Logger.LogWarning("Failed to decode address");
                        return address;
                    }
                }
                else
                {
                    address.ReplyMessage = "Failed to request geocodeded address";
                    address.ReplyStatus = ReplyStatus.Error;
                    _session.Logger.LogWarning("Failed to request geocodeded address");
                    return address;
                }
                return address;

            }
            catch (Exception ex)
            {
                var exResult = _session.Logger.WrapException(ex, new dynamic[] { address.OwningRefId, address.AddresType });
                exResult.CopyProperties(address);
            }
            return address;
        }
    }
}

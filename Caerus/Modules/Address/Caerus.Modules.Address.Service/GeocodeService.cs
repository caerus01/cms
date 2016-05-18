using Caerus.Common.Modules.Client.ViewModels;
using Caerus.Common.Modules.Geocode.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Modules.GeoCode.Service.Facade;

namespace Caerus.Modules.GeoCode.Service
{
    public class GeocodeService : IGeocodeService
    {
        private ICaerusSession _session;
        public GeocodeService(ICaerusSession session)
        {
            _session = session;
        }

        #region Addresses
        public AddressViewModel GeoCodeAddress(AddressViewModel address)
        {
            return new GeoCodingFacade(_session).GeoCodeAddress(address);
        }
        #endregion

        #region Phones

        #endregion

        #region On-line

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Address.Interfaces;
using Caerus.Common.Modules.Address.ViewModels;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Modules.Address.Service.Facade;
using Caerus.Modules.Address.Service.Repository;

namespace Caerus.Modules.Address.Service
{
    public class AddressService : IAddressService
    {
        private ICaerusSession _session;
        private IAddressRepository _repository;
        public AddressService(ICaerusSession session, IAddressRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new AddressRepository();
        }
        public AddressViewModel GeoCodeAddress(AddressViewModel address)
        {
            return new GeoCodingFacade(_session).GeoCodeAddress(address);
        }
    }
}

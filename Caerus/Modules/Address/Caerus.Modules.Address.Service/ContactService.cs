using Caerus.Common.Modules.Address.ViewModels;
using Caerus.Common.Modules.Contact.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Modules.Contact.Service.Facade;
using Caerus.Modules.Contact.Service.Repository;

namespace Caerus.Modules.Contact.Service
{
    public class ContactService : IContactService
    {
        private ICaerusSession _session;
        private IContactRepository _repository;
        public ContactService(ICaerusSession session, IContactRepository repository = null)
        {
            _session = session;
            _repository = repository ?? new ContactRepository();
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

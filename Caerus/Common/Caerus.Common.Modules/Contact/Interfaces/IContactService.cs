using Caerus.Common.Modules.Address.ViewModels;

namespace Caerus.Common.Modules.Contact.Interfaces
{
    public interface IContactService
    {
        AddressViewModel GeoCodeAddress(AddressViewModel address);
    }
}

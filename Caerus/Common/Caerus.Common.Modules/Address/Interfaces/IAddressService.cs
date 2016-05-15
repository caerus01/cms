using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Address.ViewModels;

namespace Caerus.Common.Modules.Address.Interfaces
{
    public interface IAddressService
    {
        AddressViewModel GeoCodeAddress(AddressViewModel address);
    }
}

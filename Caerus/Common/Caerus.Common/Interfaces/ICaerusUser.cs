using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caerus.Common.Interfaces
{
    public interface ICaerusUser
    {
        string Id { get; set; }
        string UserName { get; set; }
        string EmailAddress { get; set; }
        string CellNumber { get; set; }
    }
}

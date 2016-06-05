using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Enums;

namespace Caerus.Common.Modules.Authentication.ViewModels
{
   public class RegisterUserViewModel
    {
       public string EmailAddress { get; set; }
       public string PhoneNumber { get; set; }
       public string Password { get; set; }
       public UserTypes UserType { get; set; }
    }
}

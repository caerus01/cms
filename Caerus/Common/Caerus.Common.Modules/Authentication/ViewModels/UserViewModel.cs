using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Enums;
using Caerus.Common.ViewModels;

namespace Caerus.Common.Modules.Authentication.ViewModels
{
    public class UserViewModel : ReplyObject
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public UserTypes UserType { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime LastLogin { get; set; }
    }
}

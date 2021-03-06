﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Caerus.Common.Modules.Authentication.Entities
{
    [Table("AspNetUsers")]
    public class CaerusUser : IdentityUser
    {
        public UserTypes UserType { get; set; }
    }
}

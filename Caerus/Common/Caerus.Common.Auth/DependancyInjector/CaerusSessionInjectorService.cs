using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Stub;

namespace Caerus.Common.Auth.DependancyInjector
{
    public static class CaerusSessionInjectorService
    {
        public static T GetService<T>(ICaerusSession session, ModuleTypes module)
        {
            switch (module)
            {
                case ModuleTypes.Configuration:
                {
                    return (T) (object) new StubConfigurationService();
                }
                case ModuleTypes.Authentication:
                {
                    return (T) (object) new StubAuthenticationService();
                }
            }
            throw new Exception("Service not defined of type " + module.ToEnumerationDescription());
        }
    }
}

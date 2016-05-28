using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Enums;
using Caerus.Common.Extentions;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Stub;
using Caerus.Common.Stub.Services;
using Caerus.Modules.Client.Service;
using Caerus.Modules.FieldMapping.Service;
using Caerus.Modules.Lookup.Service;

namespace Caerus.Common.Auth.DependancyInjector
{
    public static class CaerusSessionInjectorService
    {
        public static T GetService<T>(ICaerusSession session, ModuleTypes module, bool authenticated = true)
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
                case ModuleTypes.FieldMapping:
                {
                    return (T)(object)new FieldMappingService(session);
                }
                case ModuleTypes.Client:
                {
                    return (T)(object)new ClientService(session);
                }
                case ModuleTypes.Notification:
                {
                    return (T)(object)new StubNotificationService();
                }
                case ModuleTypes.Audit:
                {
                    return (T)(object)new StubAuditService();
                }
                case ModuleTypes.Lookup:
                {
                    return (T)(object)new LookupService(session);
                }
            }
            throw new Exception("Service not defined of type " + module.ToEnumerationDescription());
        }
    }
}

using Caerus.Common.Interfaces;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.Configuration.Interfaces;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.Notification.Interfaces;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Common.Stub.Services;

namespace Caerus.Common.Stub.Sessions
{
    public class StubCaerusSession : ICaerusSession
    {
        public bool IsAuthenticated
        {
            get { return false; }
        }

        public string CurrentUserRef
        {
            get { return "";  }
        }

        public string Email
        {
            get { return ""; }
        }

        public string CellNumber
        {
            get { return ""; }
        }
        #region Services
        private IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService
        {
            get
            {
                return _authenticationService ?? (_authenticationService = new StubAuthenticationService());
            }
            set { _authenticationService = value; }
        }
        private IConfigurationService _configurationService;
        public IConfigurationService ConfigurationService
        {
            get { return _configurationService ?? (_configurationService = new StubConfigurationService()); }
            set { _configurationService = value; }
        }
        public IClientService ClientService { get; set; }
        public IFieldMappingService FieldMappingService { get; set; }
        public INotificationService NotificationService { get; set; }


        #endregion

        #region Logger

        private ICaerusLogger _logger;
        public ICaerusLogger Logger
        {
            get { return _logger ?? (_logger = new Logger()); }
            set { _logger = value; }
        }
        #endregion
    }
}

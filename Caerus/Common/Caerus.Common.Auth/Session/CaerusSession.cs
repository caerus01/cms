using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Auth.DependancyInjector;
using Caerus.Common.Enums;
using Caerus.Common.Interfaces;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.Configuration.Interfaces;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.Notification.Interfaces;
using Caerus.Common.Modules.Session;
using Caerus.Common.Modules.Session.Interfaces;
using Caerus.Modules.Authentication.Service;
using Caerus.Modules.Configuration.Service;

namespace Caerus.Common.Auth.Session
{

    public class CaerusSession : ICaerusSession {

        private string _currentUser;
        private string _emailAddress;
        private string _cellNumber;
        private bool _isAuthenticated;

        private IAuthenticationService _startUpAuth;
        private IConfigurationService _startUpConfig;

        public CaerusSession()
        {
            InitSession();
        }

        public CaerusSession(string userName)
        {
            InitSession(userName);
        }

        private void InitSession(string userName = "")
        {
            _startUpAuth = new AuthenticationService();
            _startUpConfig = new ConfigurationService();
        }

        #region Properties
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }
        public string CurrentUserRef
        {
            get { return _currentUser; }
        }
        public string Email
        {
            get { return _emailAddress; }
        }
        public string CellNumber
        {
            get { return _cellNumber; }
        }
        #endregion

        #region Services
        private IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService
        {
            get
            {
                return _authenticationService ?? (_authenticationService = CaerusSessionInjectorService.GetService<IAuthenticationService>(this, ModuleTypes.Authentication, IsAuthenticated));
            }
            set { _authenticationService = value; }
        }
        private IConfigurationService _configurationService;
        public IConfigurationService ConfigurationService
        {
            get { return _configurationService ?? (_configurationService = CaerusSessionInjectorService.GetService<IConfigurationService>(this, ModuleTypes.Configuration, IsAuthenticated)); }
            set { _configurationService = value; }
        }

        private IClientService _clientService;
        public IClientService ClientService
        {
            get { return _clientService ?? (_clientService = CaerusSessionInjectorService.GetService<IClientService>(this, ModuleTypes.Client, IsAuthenticated)); }
            set { _clientService = value; }
        }

        private IFieldMappingService _fieldMappingService;
        public IFieldMappingService FieldMappingService
        {
            get { return _fieldMappingService ?? (_fieldMappingService = CaerusSessionInjectorService.GetService<IFieldMappingService>(this, ModuleTypes.FieldMapping, IsAuthenticated)); }
            set { _fieldMappingService = value; }
        }

        private INotificationService _notificationService;
        public INotificationService NotificationService
        {
            get { return _notificationService ?? (_notificationService = CaerusSessionInjectorService.GetService<INotificationService>(this, ModuleTypes.Notification, IsAuthenticated)); }
            set { _notificationService = value; }
        }
       

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

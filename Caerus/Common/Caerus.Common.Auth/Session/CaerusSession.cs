using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caerus.Common.Modules.Authentication.Interfaces;
using Caerus.Common.Modules.Client.Interfaces;
using Caerus.Common.Modules.Configuration.Interfaces;
using Caerus.Common.Modules.FieldMapping.Interfaces;
using Caerus.Common.Modules.Session;
using Caerus.Common.Modules.Session.Interfaces;

namespace Caerus.Common.Auth.Session
{
    public class CaerusSession : ICaerusSession
    {

        private long _currentUser;
        private string _emailAddress;
        private string _cellNumber;
        private bool _isAuthenticated;
        public CaerusSession()
        {

        }

        public CaerusSession(string userName)
        {

        }

        #region Properties
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }
        public long CurrentUserRef
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
        public IAuthenticationService AuthenticationService { get; set; }
        public IConfigurationService ConfigurationService { get; set; }
        public IClientService ClientService { get; set; }
        public IFieldMappingService FieldMappingService { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CaerusSession(string userName, string password)
        {

        }

        #region Properties
        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
        }
        #endregion

        #region Services

        #endregion
    }
}

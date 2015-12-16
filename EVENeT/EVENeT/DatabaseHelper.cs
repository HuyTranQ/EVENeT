using EVENeT.EVENeTServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVENeT
{
    static class DatabaseHelper
    {
        private static ServiceClient _Client = null;
        private static string _CurrentUser = null;
        private static int _CurrentUserType = -1;


        public static ServiceClient Client
        {
            get { return _Client; }
            private set { _Client = value; }
        }

        public static string CurrentUser
        {
            get { return _CurrentUser; }
            set { _CurrentUser = value; }
        }

        public static int CurrentUserType
        {
            get { return _CurrentUserType; }
            set { _CurrentUserType = value; }
        }

        public static void Initialize()
        {
            if (_Client == null)
                _Client = new ServiceClient();
        }
    }
}

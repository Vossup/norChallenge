using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Helpers
{
    public static class UserHelper
    {
        public static int CompanyId = 1;
        public static int GetLoggedOnUserCompanyId()
        {
            return CompanyId;
        }
    }
}

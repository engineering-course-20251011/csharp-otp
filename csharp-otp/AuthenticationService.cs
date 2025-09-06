using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class AuthenticationService
    {
        private readonly ProfileDao profileDao;
        private readonly RsaToken rsaToken;

        public AuthenticationService(ProfileDao profileDao, RsaToken rsaToken)
        {
            this.rsaToken = rsaToken;
            this.profileDao = profileDao;
        }

        public bool IsValid(string userName, string password)
        {
            string passwordFromDao = profileDao.GetPassword(userName);
            string randomCode = rsaToken.GetRandom(userName);
            string validPassword = passwordFromDao + randomCode;

            bool isValid = password == validPassword;

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

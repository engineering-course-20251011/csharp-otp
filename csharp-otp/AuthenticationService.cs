using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class AuthenticationService
    {

        public bool IsValid(string userName, string password)
        {
            ProfileDao profileDao = new ProfileDao();
            string passwordFromDao = profileDao.GetPassword(userName);
            RsaToken rsaToken = new RsaToken();
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

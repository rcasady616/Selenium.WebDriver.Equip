using System;

namespace SeleniumExtension.SauceLabs
{
    public class SauceDriverKeys
    {
        public static string SAUCELABS_USERNAME
        {
            get 
            {
                var userName = Environment.GetEnvironmentVariable("SAUCELABS_USERNAME");
                if(string.IsNullOrEmpty(userName))
                    throw new Exception("Missing environment variable, name: SAUCELABS_USERNAME");
                return userName;
            }
        }

        public static string SAUCELABS_ACCESSKEY
        {
            get
            {
                var userKey = Environment.GetEnvironmentVariable("SAUCELABS_ACCESSKEY");
                if (string.IsNullOrEmpty(userKey))
                    throw new Exception("Missing environment variable, name: SAUCELABS_ACCESSKEY");
                return userKey; 
            }
        }
    }
}

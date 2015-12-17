using System;

namespace SeleniumExtension.SauceLabs
{
    public class SauceDriverKeys
    {
        public static string SAUCELABS_USERNAME
        {
            get 
            {
                var userName = Environment.GetEnvironmentVariable("SAUCELABS_USERNAME", EnvironmentVariableTarget.User);
                if(string.IsNullOrEmpty(userName))
                    throw new Exception("Missing environment variable, name: SAUCELABS_USERNAME");
                return userName;
            }
        }

        public static string SAUCELABS_ACCESSKEY
        {
            get
            {
                var userName = Environment.GetEnvironmentVariable("SAUCELABS_ACCESSKEY", EnvironmentVariableTarget.User);
                if (string.IsNullOrEmpty(userName))
                    throw new Exception("Missing environment variable, name: SAUCELABS_ACCESSKEY");
                return userName; 
            }
        }
    }
}

using System;

namespace Selenium.WebDriver.Equip.SauceLabs
{
    /// <summary>
    /// Sauce Driver Username and access key
    /// </summary>
    public class SauceDriverKeys
    {
        /// <summary>
        /// The Sauce Labs username, should be in save as enviorment variable 
        /// </summary>
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

        /// <summary>
        /// The Sauce Labs access key, should be in save as enviorment variable 
        /// </summary>
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

using Selenium.WebDriver.Equip.Settings;

namespace Selenium.WebDriver.Equip
{
    public static class DriversConfiguration
    {
        #region Gecko
        public static string GeockoDriverFileName = "geckodriver.exe";
        public static string GeockoDriverURL64 = "https://github.com/mozilla/geckodriver/releases/download/v0.23.0/geckodriver-v0.23.0-win64.zip";
        public static string GeockoDriverURL32 = "https://github.com/mozilla/geckodriver/releases/download/v0.23.0/geckodriver-v0.23.0-win32.zip";

        public static string FireFoxBinaryPath = new SeleniumSettings().FireFoxBinaryPath;
        
        #endregion
    }
}

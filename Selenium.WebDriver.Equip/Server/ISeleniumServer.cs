using WebDriverProxy.DTO;

namespace Selenium.WebDriver.Equip.Server
{
    public interface ISeleniumServer
    {
        void Start(string configurationArgs = "");
        bool Stop();
        bool WaitUntilStopped();
        bool WaitUntilRunning();
        bool IsSeleniumServerRunning();
    }
}

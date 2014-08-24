using WebDriverProxy.DTO;

namespace SeleniumExtension.Server
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

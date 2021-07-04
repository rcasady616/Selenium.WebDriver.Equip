namespace Selenium.WebDriver.Equip.DriverManager
{
    public interface IDriverBinary
    {
        //new
        string Name { get; }
        string GetUrl { get; }
        string DownloadUrl { get; }
        string DownloadUrlLatest { get; }
        //old
        string BrowserExePath { get; }
        string DownloadString { get; }
        string ExeVersion { get; }
        string FileName { get; }
        string MajorVersion { get; }
    }
}
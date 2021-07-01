using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;

namespace Selenium.WebDriver.Equip.WebDriver
{
    public class DriverManager
    {
        public DriverManager()
        {
            // ChromeDriver = new DriverBinary() { BrowserUrl = "https://chromedriver.storage.googleapis.com/", };
        }

        public void GetAndUnpack(IDriverBinary driver, string pathToExtractTo = "", bool deleteZip = true, bool getFileOnlyIfNewer = true)
        {
            if (string.IsNullOrEmpty(pathToExtractTo))
                pathToExtractTo = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string driverVersion = new WebClient().DownloadString(driver.DownloadString);
            string localDriverFilePath = $@"{pathToExtractTo}\{driver.FileName}";
            if (File.Exists(localDriverFilePath))
                if (FileVersionInfo.GetVersionInfo(localDriverFilePath).FileVersion != driver.ExeVersion)
                    File.Delete(localDriverFilePath);

            if (!File.Exists(localDriverFilePath))
            {
                //string searchFileName = $@"{driverVersion}/chromedriver_win32.zip";
                //var jsonstr = new WebClient().DownloadString("https://www.googleapis.com/storage/v1/b/chromedriver/o");

                string zipfileName = @$"{pathToExtractTo}\chromedriver_win32.zip";
                string chromeSource = @$"https://chromedriver.storage.googleapis.com/{driverVersion}/chromedriver_win32.zip";
                new WebClient().DownloadFile(chromeSource, zipfileName);

                ZipFile.ExtractToDirectory(zipfileName, pathToExtractTo);
                if (deleteZip)
                    File.Delete(zipfileName);
            }
        }
    }
}

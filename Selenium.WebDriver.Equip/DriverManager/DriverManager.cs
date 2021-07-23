using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;

namespace Selenium.WebDriver.Equip.DriverManager
{
    public class DriverManager
    {
        public DriverManager()
        {
        }

        public void GetAndUnpack(IDriverBinary driver, string pathToExtractTo = "", bool deleteZip = true, bool getFileOnlyIfNewer = true)
        {
            if (string.IsNullOrEmpty(pathToExtractTo))
                pathToExtractTo = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string localDriverFilePath = $@"{pathToExtractTo}\{driver.FileName}";
            if (File.Exists(localDriverFilePath))
                if (FileVersionInfo.GetVersionInfo(localDriverFilePath).FileVersion != driver.ExeVersion)
                    File.Delete(localDriverFilePath);

            if (!File.Exists(localDriverFilePath))
            {
                string zipfileName = @$"{pathToExtractTo}\{driver.GetType()}.zip";
                new WebClient().DownloadFile(driver.DownloadString, zipfileName);
                ZipFile.ExtractToDirectory(zipfileName, pathToExtractTo);
                if (deleteZip)
                    File.Delete(zipfileName);
            }
        }
    }
}
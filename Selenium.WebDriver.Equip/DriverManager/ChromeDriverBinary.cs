using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Selenium.WebDriver.Equip.WebDriver
{
    public class ChromeDriverBinary : IDriverBinary
    {
        public string Name => "Chrome";
        public string DownloadUrl => "https://chromedriver.storage.googleapis.com/";
        public string DownloadUrlLatest => "https://chromedriver.storage.googleapis.com/";

        public string GetUrl
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return $"{DownloadUrl}chromedriver_mac64.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return $"{DownloadUrl}chromedriver_linux64.zip";

                return $"{DownloadUrl}chromedriver_win32.zip";
            }
        }
        public string FileName => "chromedriver.exe";

        public string BrowserExePath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", null).ToString();

                throw new NotImplementedException("");
            }
        }
        private string exeVersion;
        public string ExeVersion
        {
            get
            {
                if (!string.IsNullOrEmpty(exeVersion))
                    return exeVersion;
                exeVersion = FileVersionInfo.GetVersionInfo(BrowserExePath).FileVersion;
                return exeVersion;
            }
        }
        private string majorVersion;

        public string MajorVersion
        {
            get
            {
                if (!string.IsNullOrEmpty(majorVersion))
                    return majorVersion;
                majorVersion = ExeVersion.Substring(0, ExeVersion.LastIndexOf('.') + 1 - 1);
                return majorVersion;
            }
        }
        public string DownloadString
        {
            get
            {
                return "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_" + MajorVersion;
            }
        }
    }
}

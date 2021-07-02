using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Selenium.WebDriver.Equip.DriverManager
{
    public class FirefoxDriverBinary : IDriverBinary
    {
        public string FileName => "geckodriver.exe";
        public string BrowserExePath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\firefox.exe", "", null).ToString();

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

        string IDriverBinary.Name => throw new NotImplementedException();

        string IDriverBinary.GetUrl => throw new NotImplementedException();

        string IDriverBinary.DownloadUrl => throw new NotImplementedException();

        string IDriverBinary.DownloadUrlLatest => throw new NotImplementedException();

        string IDriverBinary.DownloadString => @"https://github.com/mozilla/geckodriver/releases/download/v0.29.1/geckodriver-v0.29.1-win64.zip";// $@"https://github.com/mozilla/geckodriver/releases/download/v0.29.1/geckodriver-v0.29.1-win32.zip";

        string IDriverBinary.FileName => "geckodriver.exe";

    }

}

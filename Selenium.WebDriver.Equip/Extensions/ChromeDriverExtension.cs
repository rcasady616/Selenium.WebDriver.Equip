using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium.Chrome
{
    public static class ChromeDriverExtension
    {
        public static string GetBrowserExePath(this ChromeDriver chromeDriver)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", null).ToString();

            throw new NotImplementedException("");
        }
    }
}

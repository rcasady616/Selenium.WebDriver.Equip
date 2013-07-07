using System;
using System.Diagnostics;
using System.IO;

namespace SeleniumExtention
{
    public class SeleniumServer
    {
        public static void Start(string seleniumServerFilePath)
        {
            if (!File.Exists(seleniumServerFilePath))
                throw new Exception(string.Format("Could not find selenium-server, file name: {0}", seleniumServerFilePath));
            Kill();
            Process.Start("Java.exe", string.Format("-jar \"{0}\" -timeout 90 -browserTimeout 600", seleniumServerFilePath));
        }

        public static void Kill()
        {
            Process[] procs = Process.GetProcessesByName("Java");
            foreach (Process proc in procs)
            {
                proc.Kill();
            }
        }
    }
}

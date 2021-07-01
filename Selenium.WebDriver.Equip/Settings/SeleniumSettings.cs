using Selenium.WebDriver.Equip.WebDriver;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Selenium.WebDriver.Equip.Settings
{
    public class SeleniumSettings
    {
        private string fileName = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\SeleniumSettings.config";

        private string driverType;
        private string browserName;
        private string remoteBrowserName;
        private string remoteBrowserVersion;
        private string remoteOsPlatform;
        private string fireFoxBinaryPath;
        private string environment = "local";

        public SeleniumSettings()
        {
            environment = Environment.GetEnvironmentVariable("EQUIP_ENVIRONMENT");
        }

        /// <summary>
        /// example DriverType: HtmlUnit, IE, Firefox, Safari, Chrome, Opera, Remote, IPhone, Android, WindowsPhone, PhantomJS, SauceLabs 
        /// </summary>
        public DriverType DriverType
        {
            get { return (DriverType)Enum.Parse(typeof(DriverType), driverType); }
            set { driverType = value.GetDescription(); }
        }

        /// <summary>
        /// example DriverType: HtmlUnit, IE, Firefox, Safari, Chrome, Opera, IPhone, Android, WindowsPhone, PhantomJS
        /// </summary>
        public BrowserName BrowserName
        {
            get { return (BrowserName)Enum.Parse(typeof(BrowserName), browserName); }
            set { browserName = value.GetDescription(); }
        }

        /// <summary>
        /// example RemoteBrowserName: firefox, internet explorer, safari
        /// </summary>
        public string RemoteBrowserName
        {
            get { return remoteBrowserName; }
            set { remoteBrowserName = value; }
        }

        public string RemoteBrowserVersion
        {
            get { return remoteBrowserVersion; }
            set { remoteBrowserVersion = value; }
        }

        public string RemoteOsPlatform
        {
            get { return remoteOsPlatform; }
            set { remoteOsPlatform = value; }
        }

        public string FireFoxBinaryPath
        {
            get { return fireFoxBinaryPath; }
            set { fireFoxBinaryPath = value; }
        }

        private SeleniumSettings GetDefault()
        {
            var settings = new SeleniumSettings()
            {
                DriverType = DriverType.Chrome,
                BrowserName = BrowserName.Chrome,
                RemoteBrowserName = "Chrome",
                RemoteBrowserVersion = "71.0",
                RemoteOsPlatform = "Windows 10",
                FireFoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe"
            };
            return settings;
        }

        private SeleniumSettings GetBuildServer()
        {
            var settings = new SeleniumSettings()
            {
                DriverType = DriverType.SauceLabs,
                BrowserName = BrowserName.Chrome,
                RemoteBrowserName = "Chrome",
                RemoteBrowserVersion = "91.0",
                RemoteOsPlatform = "Windows 10",
                FireFoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe"
            };
            return settings;
        }

        public SeleniumSettings Deserialize()
        {
            var xmlDoc = new XmlDocument();
            if (!File.Exists(fileName)) // create a default object if the file dosent exist
                if (environment == "ci")
                    GetBuildServer().Serialize();
                else
                {
                    GetDefault().Serialize();
                }



            xmlDoc.Load(fileName);
            return Deserialize(xmlDoc);
        }

        private static SeleniumSettings Deserialize(XmlDocument doc)
        {
            var reader = new XmlNodeReader(doc.DocumentElement);
            var ser = new XmlSerializer(Type.GetType("Selenium.WebDriver.Equip.Settings.SeleniumSettings"));
            var obj = (SeleniumSettings)ser.Deserialize(reader);
            return obj;
        }

        public void Serialize()
        {
            var ser = new XmlSerializer(this.GetType());
            using (XmlWriter writer = XmlWriter.Create(fileName, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                ser.Serialize(writer, this);
            }
        }
    }
}

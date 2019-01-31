using System;
using System.IO;
using System.Reflection;
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

        public SeleniumSettings()
        {
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

        public SeleniumSettings Deserialize()
        {
            var xmlDoc = new XmlDocument();
            if (!File.Exists(fileName)) // create a default object if the file dosent exist
                GetDefault().Serialize();

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

        public string Serialize()
        {
            var ser = new XmlSerializer(this.GetType());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            ser.Serialize(writer, this);
            var doc = new XmlDocument();
            return sb.ToString();
        }

        public void CreateXMl()
        {
            using (XmlWriter xw = XmlWriter.Create(fileName))
            {
                var doc = new XmlDocument();
                doc.LoadXml(this.Serialize());
                doc.WriteTo(xw);
                xw.Flush();
            }
        }
    }
}

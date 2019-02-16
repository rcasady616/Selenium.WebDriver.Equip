using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Selenium.WebDriver.Equip.Settings
{
    public class SeleniumDriverExeSettings //: BaseSettings
    {
        private string fileName = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\SeleniumDriverExeSettings.config";

        public string NugetChromeDriverVersion { get; set; }
        public string NugetIEDriverVersion { get; set; }
        public string NugetGeckoDriverVersion { get; set; }

        public SeleniumDriverExeSettings()
        {
        }

        public SeleniumDriverExeSettings GetDefault() 
        {
            var settings = new SeleniumDriverExeSettings()
            {
                NugetChromeDriverVersion = "2.45.0",
                NugetIEDriverVersion = "3.141.0",
                NugetGeckoDriverVersion = "0.23.0.3"
            };
            return settings;
        }

        public SeleniumDriverExeSettings Deserialize()
        {
            var xmlDoc = new XmlDocument();
            if (!File.Exists(fileName)) // create a default object if the file dosent exist
                GetDefault().Serialize();

            xmlDoc.Load(fileName);
            return Deserialize(xmlDoc);
        }

        private static SeleniumDriverExeSettings Deserialize(XmlDocument doc)
        {
            var reader = new XmlNodeReader(doc.DocumentElement);
            var ser = new XmlSerializer(Type.GetType("Selenium.WebDriver.Equip.Settings.SeleniumDriverExeSettings"));
            var obj = (SeleniumDriverExeSettings)ser.Deserialize(reader);
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

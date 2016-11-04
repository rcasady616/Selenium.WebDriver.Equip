using System.Xml.Serialization;

namespace Selenium.WebDriver.Equip.Settings
{
    
    public class SeleniumServerSettings
    {
        [XmlAttribute]
        public string HostName { get; set; }

        [XmlAttribute]
        public string Port { get; set; }

        [XmlAnyAttribute]
        public string StandAlonePath { get; set; }
    }
}

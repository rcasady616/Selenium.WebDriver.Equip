using System.Runtime.Serialization;

namespace Selenium.WebDriver.Equip.Server
{
    [DataContract]
    public class CapabilitiesContract
    {
        [DataMember]
        internal string browserName = "firefox";
        [DataMember]
        internal int maxInstances = 2;
        [DataMember]
        internal string seleniumProtocol = "WebDriver";
    }
}

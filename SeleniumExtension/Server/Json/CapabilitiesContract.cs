using System.Runtime.Serialization;

namespace SeleniumExtension.Server
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

using System.Runtime.Serialization;

namespace Selenium.WebDriver.Equip.Server
{
    [DataContract]
    public class NodeContract
    {
        [DataMember]
        internal CapabilitiesContract[] capabilities = { new CapabilitiesContract() };
        [DataMember]
        internal ConfigurationContract configuration = new ConfigurationContract();
    }
}

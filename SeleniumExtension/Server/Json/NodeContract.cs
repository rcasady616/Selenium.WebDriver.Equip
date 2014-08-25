using System.Runtime.Serialization;

namespace SeleniumExtension.Server
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

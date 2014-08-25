using System.Runtime.Serialization;

namespace SeleniumExtension.Server
{
    [DataContract]
    public class ConfigurationContract
    {
        [DataMember]
        internal string proxy = "org.openqa.grid.selenium.proxy.DefaultRemoteProxy";
        [DataMember]
        internal int maxSession = 5;
        [DataMember]
        internal int port = 5555;
        [DataMember]
        internal string host = "ip";
        [DataMember]
        internal bool register = true;
        [DataMember]
        internal int registerCycle = 5000;
        [DataMember]
        internal int hubPort = 4444;
        [DataMember]
        internal string hubHost = "ip";
    }
}

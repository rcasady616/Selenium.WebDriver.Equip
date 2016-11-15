using Microsoft.Win32;

namespace Equip.Java
{
    public class Java
    {
        {
            string jreRegistryPath = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
            return Registry.LocalMachine.GetRegistryKey(jreRegistryPath).GetKeyValue("CurrentVersion");
        }
    }
}

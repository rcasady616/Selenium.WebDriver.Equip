using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Selenium.WebDriver.Equip.Server.Json
{
    public class JsonSerializer
    {
        public static void Serialize(NodeContract cap, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            var ser = new DataContractJsonSerializer(typeof(NodeContract));
            var stream1 = new MemoryStream();
            ser.WriteObject(stream1, cap);
           

            stream1.Position = 0;
            using (var sr = new StreamReader(stream1))
            {
                using (var sw = new StreamWriter(fileName))
                {
                    sw.Write(sr.ReadToEnd());
                }
            }
        }
    }
}

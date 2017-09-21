using System.IO;
using System.Xml.Serialization;

namespace AutomationScript
{
    public static class LoadSettings
    {
        internal static T LoadXmlAbsolute<T>(string filename)
        {
            using (var reader = File.OpenRead(filename))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}

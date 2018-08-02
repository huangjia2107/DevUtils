using System.IO; 
using System.Text; 
using System.Xml.Serialization;

namespace Utils.IO
{
    public static class FileHelper
    {
        public static bool SaveToXmlFile<T>(string filePath, T instance)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        new XmlSerializer(typeof(T)).Serialize(sw, instance);
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool LoadFromXmlFile<T>(string filePath, ref T instance)
        {
            try
            {
                if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                    return false;

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    instance = (T)(new XmlSerializer(typeof(T))).Deserialize(fs);

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

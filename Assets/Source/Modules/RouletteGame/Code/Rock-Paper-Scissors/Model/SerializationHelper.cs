using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RockPaperScissors
{
    public static class SerializationHelper
    {
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static object Deserialize(byte[] data)
        {
            if (data == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(data))
            {
                return bf.Deserialize(ms);
            }
        }
    }
}

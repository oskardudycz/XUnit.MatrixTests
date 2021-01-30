using System.Text.Json;

namespace Weasel.Serialization
{
    public class SystemTextJsonSerializer : ISerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
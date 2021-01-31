using System.Collections.Generic;
using Weasel.Serialization;

namespace Weasel
{
    public class DocumentSession
    {
        private readonly ISerializer serializer;

        private Dictionary<string, string> Database = new()
        {
            {"1", "{\"Id\": \"1\", \"Name\": \"test\"}"}
        };

        public DocumentSession()
        {
            serializer = SerializerFactory.New();
        }

        public T Load<T>(string id)
        {
            var docJson = Database[id];

            return serializer.Deserialize<T>(docJson);
        }
        
        public void Store<T>(string id, T doc)
        {
            var docJson = serializer.Serialize(doc);
            if(!Database.TryAdd(id, docJson))
            {
                Database[id] = docJson;
            }
        }
    }
}
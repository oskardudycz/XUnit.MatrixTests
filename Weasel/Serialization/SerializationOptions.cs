using System;

namespace Weasel.Serialization
{
    public class SerializationOptions
    {
        public static Func<ISerializer> DefaultSerializerFactory = () => new NewtonsoftSerializer();
    }
}
using System;

namespace Weasel.Serialization
{
    public static class SerializerFactory
    {
        public static SerializerType DefaultSerializerType { get; set; } = SerializerType.Newtonsoft;

        public static ISerializer New(SerializerType? serializerType = null)
        {
            serializerType ??= DefaultSerializerType;

            return serializerType switch
            {
                SerializerType.Newtonsoft => new NewtonsoftSerializer(),
                SerializerType.SystemTextJson => new SystemTextJsonSerializer(),
                _ => throw new ArgumentOutOfRangeException(nameof(serializerType), serializerType, null)
            };
        }
    }
}
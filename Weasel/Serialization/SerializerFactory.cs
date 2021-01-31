using System;

namespace Weasel.Serialization
{
    public static class SerializerFactory
    {
        public static SerializerType DefaultSerializerType { get; set; } = SerializerType.NewtonsoftJsonNet;

        public static ISerializer New(SerializerType? serializerType = null)
        {
            serializerType ??= DefaultSerializerType;

            return serializerType switch
            {
                SerializerType.NewtonsoftJsonNet => new NewtonsoftSerializer(),
                SerializerType.SystemTextJson => new SystemTextJsonSerializer(),
                _ => throw new ArgumentOutOfRangeException(nameof(serializerType), serializerType, null)
            };
        }
    }
}
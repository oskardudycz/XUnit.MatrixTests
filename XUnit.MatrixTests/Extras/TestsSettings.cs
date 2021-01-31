using System;
using Weasel.Serialization;

namespace XUnit.MatrixTests
{
    public static class TestsSettings
    {
        private static SerializerType? serializerType;

        public static SerializerType SerializerType
        {
            get
            {
                if (serializerType.HasValue) 
                    return serializerType.Value;
                
                var defaultSerializerEnv = Environment.GetEnvironmentVariable("DEFAULT_SERIALIZER");

                serializerType = Enum.TryParse(defaultSerializerEnv, out SerializerType parsedSerializerType)
                    ? parsedSerializerType
                    : SerializerType.NewtonsoftJsonNet;
                
                return serializerType.Value;
            }
        }
    }
}
using System;
using Weasel.Serialization;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework("XUnit.MatrixTests.TestSetup", "XUnit.MatrixTests")]

namespace XUnit.MatrixTests
{
    public class TestSetup : XunitTestFramework
    {
        public TestSetup(IMessageSink messageSink)
            :base(messageSink)
        {
            if (TestsSettings.SerializerType == SerializerType.SystemTextJson)
            {
                SerializationOptions.DefaultSerializerFactory = () => new SystemTextJsonSerializer();
            }
        }

        public new void Dispose()
        {
            // Place tear down code here
            base.Dispose();
        }
    }
}
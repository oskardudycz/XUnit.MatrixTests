using System;
using System.Collections.Generic;
using Weasel.Serialization;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnit.MatrixTests.Extras
{
    /// <summary>
    /// Allows targeting test at specified minimum and/or maximum version of PG
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer("XUnit.MatrixTests.Extras.SerializerTargetedFactDiscoverer", "XUnit.MatrixTests")]
    public sealed class SerializerTypeTargetedFact: FactAttribute
    {
        public SerializerType RunFor { get; set; }
    }

    public sealed class SerializerTargetedFactDiscoverer: FactDiscoverer
    {
        private readonly SerializerType serializerType;

        static SerializerTargetedFactDiscoverer()
        {
        }

        public SerializerTargetedFactDiscoverer(IMessageSink diagnosticMessageSink): base(diagnosticMessageSink)
        {
            serializerType = TestsSettings.SerializerType;
        }

        public override IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
            IAttributeInfo factAttribute)
        {
            var runForSerializer = factAttribute.GetNamedArgument<SerializerType?>(nameof(SerializerTypeTargetedFact.RunFor));
            
            if (runForSerializer != null && runForSerializer != serializerType)
            {
                yield return new TestCaseSkippedDueToSerializerSupport($"Test skipped as it cannot be run for {serializerType} ", DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);
            }
            
            yield return base.CreateTestCase(discoveryOptions, testMethod, factAttribute);
        }

        internal sealed class TestCaseSkippedDueToSerializerSupport: XunitTestCase
        {
            [Obsolete("Called by the de-serializer", true)]
            public TestCaseSkippedDueToSerializerSupport()
            {
            }

            public TestCaseSkippedDueToSerializerSupport(string skipReason, IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, object[] testMethodArguments = null) : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
            {
                SkipReason = skipReason;
            }
        }
    }
}

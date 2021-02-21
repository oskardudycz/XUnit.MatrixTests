﻿using System;
using System.Collections.Generic;
using Weasel.Serialization;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnit.MatrixTests.Extras
{
    /// <summary>
    /// Allows targeting test at specified serializer type
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer("XUnit.MatrixTests.Extras.SerializerTargetedTheoryDiscoverer", "XUnit.MatrixTests")]
    public sealed class SerializerTypeTargetedTheory: TheoryAttribute
    {
        public SerializerType RunFor { get; set; }
    }

    public sealed class SerializerTargetedTheoryDiscoverer: TheoryDiscoverer
    {
        private readonly SerializerType serializerType;

        static SerializerTargetedTheoryDiscoverer()
        {
        }

        public SerializerTargetedTheoryDiscoverer(IMessageSink diagnosticMessageSink): base(diagnosticMessageSink)
        {
            serializerType = TestsSettings.SerializerType;
        }

        public override IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
            IAttributeInfo theoryAttribute)
        {
            var runForSerializer = theoryAttribute.GetNamedArgument<SerializerType?>(nameof(SerializerTypeTargetedTheory.RunFor));

            if (runForSerializer != null && runForSerializer != serializerType)
            {
                yield return new TestCaseSkippedDueToSerializerSupport($"Test skipped as it cannot be run for {serializerType} ", DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);
                yield break;
            }

            foreach (var xunitTestCase in CreateTestCasesForTheory(discoveryOptions, testMethod, theoryAttribute))
            {
                yield return xunitTestCase;
            }
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

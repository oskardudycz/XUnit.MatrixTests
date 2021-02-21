using System.Collections.Generic;
using Weasel;
using Weasel.Serialization;
using Xunit;
using XUnit.MatrixTests.Extras;

namespace XUnit.MatrixTests
{
    public class DynamicTests
    {
        [SerializerTypeTargetedFact(RunFor = SerializerType.NewtonsoftJsonNet)]
        public void TestWithCustomFact()
        {
            var session = new DocumentSession();

            var doc = session.Load<dynamic>("1");
            
            Assert.Equal("1", (string)doc.Id);
            Assert.Equal("test", (string)doc.Name);
        }
        
        
        [SerializerTypeTargetedTheory(RunFor = SerializerType.NewtonsoftJsonNet)]
        [MemberData(nameof(GetIds))]
        public void TestWithCustomTheory(string id)
        {
            var session = new DocumentSession();

            var doc = session.Load<dynamic>("1");
            
            Assert.Equal("1", (string)doc.Id);
            Assert.Equal("test", (string)doc.Name);
        }
        
        public static IEnumerable<object[]> GetIds()
        {
            yield return new object[]{"1"};
        }
    }
}
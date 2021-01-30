using Weasel;
using Xunit;
using XUnit.MatrixTests.Extras;

namespace XUnit.MatrixTests
{
    public class DynamicTests
    {
        [SerializerTargetedFact(RunFor = SerializerType.Newtonsoft)]
        public void DeserializeDynamic()
        {
            var session = new DocumentSession();

            var doc = session.Load<dynamic>("1");
            
            Assert.Equal("1", (string)doc.Id);
            Assert.Equal("test", (string)doc.Name);
        }
    }
}
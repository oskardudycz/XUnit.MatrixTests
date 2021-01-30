using Weasel;
using Xunit;

namespace XUnit.MatrixTests
{
    public class TestClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    
    public class RegularTests
    {
        [Fact]
        public void DeserializeRegularClass()
        {
            var session = new DocumentSession();

            var doc = session.Load<TestClass>("1");
            
            Assert.Equal("1", doc.Id);
            Assert.Equal("test", doc.Name);
        }
    }
}
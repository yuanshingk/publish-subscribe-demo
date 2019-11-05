using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Publisher.Test
{
    [TestClass]
    public class MessageConstructorTest
    {
        [TestMethod]
        public void Construct_NullAsInput_ReturnNull()
        {
            var sut = new MessageConstructor();
            var result = sut.Construct(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Construct_ValidInput_ReturnValuePropertyAsInput()
        {
            var sut = new MessageConstructor();
            var result = sut.Construct("test");

            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Value);
        }

        [TestMethod]
        public void Construct_ValidInput_ReturnCorrectSHA256Property()
        {
            var inputString = "test123";
            
            // expected SHA256 value is precomputed from external trusted tools https://emn178.github.io/online-tools/sha256.html
            var expectedSHA256Output = "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae"; 

            var sut = new MessageConstructor();
            var result = sut.Construct(inputString);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSHA256Output, result.SHA256);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NServiceBus.Testing;
using PublishSubscribeContract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Publisher.Test
{
    [TestClass]
    public class BootstrapperTest
    {
        private TestableEndpointInstance _endpointInstance;
        private Mock<IMessageConstructor> _messageConstructorMock;

        [TestInitialize]
        public void Setup()
        {
            _endpointInstance = new TestableEndpointInstance();
            _messageConstructorMock = new Mock<IMessageConstructor>();
        }

        [TestMethod]
        public async Task Start_EnterTextExit_ExitTaskLoopWithCorrectConsoleMessage()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader("exit"))
                {
                    var expectedString = string.Format(
                        "Enter \"exit\" to exit...{0}Enter string to publish:{0}",
                        Environment.NewLine);

                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    var sut = new Bootstrapper(_endpointInstance, _messageConstructorMock.Object);
                    await sut.Start();

                    var sentMessages = _endpointInstance.PublishedMessages;
                    Assert.AreEqual(expectedString, sw.ToString());
                    Assert.AreEqual(0, sentMessages.Length);
                }
            }
        }

        [TestMethod]
        public async Task Start_EnterStringValue_PublishMessage()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader("Test123\nexit"))
                {
                    var message = new Message { Value = "Test123", SHA256 = "dummy SHA Value" };
                    _messageConstructorMock.Setup(mc => mc.Construct("Test123")).Returns(message);
                    Console.SetIn(sr);

                    var sut = new Bootstrapper(_endpointInstance, _messageConstructorMock.Object);
                    await sut.Start();

                    var sentMessages = _endpointInstance.PublishedMessages;
                    Assert.AreEqual(1, sentMessages.Length);
                    Assert.AreEqual(message, (Message)sentMessages[0].Message);
                }
            }
        }

        [TestMethod]
        public async Task Start_EnterEmptyStringValue_NoMessagePublished()
        {
            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader("  \n  \nexit"))
                {
                    Console.SetIn(sr);

                    var sut = new Bootstrapper(_endpointInstance, _messageConstructorMock.Object);
                    await sut.Start();

                    var sentMessages = _endpointInstance.PublishedMessages;
                    Assert.AreEqual(0, sentMessages.Length);
                    _messageConstructorMock.Verify(mc => mc.Construct(It.IsAny<string>()), Times.Never);
                }
            }
        }
    }
}

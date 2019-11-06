using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus.Testing;
using PublishSubscribeContract;

namespace Subscriber.Test
{
    [TestClass]
    public class SHA256DisplayHandlerTest
    {
        [TestMethod]
        public async Task Handle_ValidMessageReceived_PrintSHA256PropertyToConsole()
        {
            using (var sw = new StringWriter())
            {
                var message = new Message { Value = "in-coming message", SHA256 = "dummy sha" };
                var expectedString = string.Format(
                    "Incoming message - SHA256DisplayHandler: {0}{1}{0}",
                    Environment.NewLine,
                    message.SHA256);
                var sut = new SHA256DisplayHandler();
                var context = new TestableMessageHandlerContext();

                Console.SetOut(sw);
                await sut.Handle(message, context);

                Assert.AreEqual<string>(expectedString, sw.ToString());
            }
        }
    }
}

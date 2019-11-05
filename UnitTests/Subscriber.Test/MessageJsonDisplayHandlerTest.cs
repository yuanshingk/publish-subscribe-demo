using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NServiceBus.Testing;
using PublishSubscribeContract;

namespace Subscriber.Test
{
    [TestClass]
    public class MessageJsonDisplayHandlerTest
    {
        [TestMethod]
        public async Task Handle_ValidMessageRecieved_PrintOutputAsJsonStringToConsole()
        {
            using (StringWriter sw = new StringWriter())
            {
                var message = new Message { Value = "in-coming message", SHA256 = "dummy sha" };
                var expectedString = string.Format(
                    "Incoming message - MessageJsonDisplayHandler: {0}{1}{0}", 
                    Environment.NewLine, 
                    JsonConvert.SerializeObject(message, Formatting.Indented));
                var sut = new MessageJsonDisplayHandler();
                var context = new TestableMessageHandlerContext();

                Console.SetOut(sw);
                await sut.Handle(message, context);

                Assert.AreEqual<string>(expectedString, sw.ToString());
            }
        }
    }
}

using Newtonsoft.Json;
using NServiceBus;
using PublishSubscribeContract;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class MessageJsonDisplayHandler : IHandleMessages<Message>
    {
        public Task Handle(Message message, IMessageHandlerContext context)
        {
            if (message != null)
            {
                Console.WriteLine("Incoming message - MessageJsonDisplayHandler: ");
                Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
            }
           
            return Task.CompletedTask;
        }
    }
}

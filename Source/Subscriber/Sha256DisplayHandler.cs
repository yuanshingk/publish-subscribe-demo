using NServiceBus;
using PublishSubscribeContract;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class SHA256DisplayHandler : IHandleMessages<Message>
    {
        public Task Handle(Message message, IMessageHandlerContext context)
        {
            if (message != null)
            {
                Console.WriteLine("Incoming message - SHA256DisplayHandler: ");
                Console.WriteLine(message.SHA256);
            }
            
            return Task.CompletedTask;
        }
    }
}

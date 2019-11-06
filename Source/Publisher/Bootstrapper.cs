using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    public class Bootstrapper
    {
        private readonly IEndpointInstance _endpointInstance;
        private readonly IMessageConstructor _messageConstructor;

        public Bootstrapper(IEndpointInstance endpointInstance, IMessageConstructor messageConstructor)
        {
            _endpointInstance = endpointInstance;
            _messageConstructor = messageConstructor;
        }

        public async Task Start()
        {
            Console.WriteLine("Enter \"exit\" to exit...");

            string input;
            while (true)
            {
                Console.WriteLine("Enter string to publish:");
                input = Console.ReadLine();

                if (input == "exit")
                {
                    return;
                }

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var message = _messageConstructor.Construct(input);
                    await _endpointInstance.Publish(message);
                }
            }
        }
    }
}

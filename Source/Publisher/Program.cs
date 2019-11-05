using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.Title = "Publisher";
            
            var endpointConfiguration = new EndpointConfiguration("Publisher");
            endpointConfiguration.UseTransport<LearningTransport>();

            var messageConstructor = new MessageConstructor();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Enter \"exit\" to exit...");

            string input;
            do
            {
                Console.WriteLine("Enter string to publish: ");
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var message = messageConstructor.Construct(input);
                    await endpointInstance.Publish(message);
                }
            } while (input != "exit");

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}

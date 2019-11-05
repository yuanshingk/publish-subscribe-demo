using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.Title = "Subscriber";
            var endpointConfiguration = new EndpointConfiguration("Subscriber");
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}

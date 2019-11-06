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

            var boostrapper = new Bootstrapper(endpointInstance, messageConstructor);
            await boostrapper.Start();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}

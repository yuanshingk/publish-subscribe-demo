using NServiceBus;

namespace PublishSubscribeContract
{
    public class Message : IEvent
    {
        public string Value { get; set; }
        public string SHA256 { get; set; }
    }
}

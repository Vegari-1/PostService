using BusService;
using BusService.Routing;
using Polly;
using PostService.Service.Interface.Sync;

namespace PostService.Messaging
{
    public class ConnectionMessageBusService : MessageBusHostedService
    {
        public ConnectionMessageBusService(IMessageBusService serviceBus, IServiceScopeFactory serviceScopeFactory) : base(serviceBus, serviceScopeFactory)
        {
        }

        protected override void ConfigureSubscribers()
        {
            var policy = BuildPolicy();
            Subscribers.Add(new MessageBusSubscriber(policy, SubjectBuilder.Build(Topics.Connection), typeof(IConnectionSyncService)));
        }

        private Policy BuildPolicy()
        {
            return Policy
                    .Handle<Exception>()
                    .WaitAndRetry(5, _ => TimeSpan.FromSeconds(5), (exception, _, _, _) =>
                    {});
        }
    }
}
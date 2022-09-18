using BusService;
using BusService.Contracts;
using BusService.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PostService.Model;
using PostService.Service.Interface;
using PostService.Service.Interface.Sync;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Service.Sync
{
    public class PostSyncService : ConsumerBase<Post, PostContract>, IPostSyncService
    {
        private readonly IMessageBusService _messageBusService;

        public PostSyncService(IMessageBusService messageBusService, ILogger<PostSyncService> logger) : base(logger)
        {
            _messageBusService = messageBusService;
        }

        public override Task PublishAsync(Post entity, string action)
        {
            PostContract contract = new(entity.AuthorId);

            var serialized = JsonConvert.SerializeObject(contract);
            var bData = Encoding.UTF8.GetBytes(serialized);
            _messageBusService.PublishEvent(SubjectBuilder.Build(Topics.Post, action), bData);
            return Task.CompletedTask;
        }

        public override Task SynchronizeAsync(PostContract entity, string action)
        {
            throw new NotImplementedException();
        }
    }
}

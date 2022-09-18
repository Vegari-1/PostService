using BusService;
using BusService.Contracts;
using PostService.Model;

namespace PostService.Service.Interface.Sync
{
    public interface IPostSyncService: ISyncService<Post, PostContract>
    {
    }
}

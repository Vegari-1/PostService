using BusService;
using BusService.Contracts;

namespace PostService.Service.Interface.Sync
{
    public interface IEventSyncService : ISyncService<EventContract, EventContract>
    {
    }
}

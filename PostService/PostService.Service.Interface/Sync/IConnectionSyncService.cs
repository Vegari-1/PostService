using BusService;
using BusService.Contracts;
using PostService.Model.Sync;

namespace PostService.Service.Interface.Sync
{
    public interface IConnectionSyncService : ISyncService<Connection, ConnectionContract>
    {
    }
}

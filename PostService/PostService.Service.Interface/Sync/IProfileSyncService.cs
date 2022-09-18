using BusService;
using BusService.Contracts;
using PostService.Model.Sync;
using System;
using System.Threading.Tasks;

namespace PostService.Service.Interface.Sync
{
    public interface IProfileSyncService : ISyncService<Profile, ProfileContract>
    {
        Profile GetById(Guid id);
        Task<Profile> GetByIdAsync(Guid id);
    }
}

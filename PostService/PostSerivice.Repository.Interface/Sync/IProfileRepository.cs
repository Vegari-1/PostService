using PostService.Model.Sync;
using System;
using System.Threading.Tasks;

namespace PostService.Repository.Interface.Sync
{
	public interface IProfileRepository : IRepository<Profile>
	{
		Profile GetById(Guid id);
		Task<Profile> GetByIdAsync(Guid id);
    }
}

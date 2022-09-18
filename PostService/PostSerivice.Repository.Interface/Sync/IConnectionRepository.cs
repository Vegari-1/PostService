using PostService.Model.Sync;
using System;
using System.Threading.Tasks;

namespace PostService.Repository.Interface.Sync
{
	public interface IConnectionRepository : IRepository<Connection>
	{
		Connection GetById(Guid id);
		Task<Connection> GetByIdAsync(Guid id);
    }
}

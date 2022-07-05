using PostService.Model;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface IReactionRepository : IRepository<Reaction>
    {
        Task<Reaction> Save(Reaction reaction);
    }
}

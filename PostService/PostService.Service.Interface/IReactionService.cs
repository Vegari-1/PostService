using PostService.Model;
using PostService.Repository.Interface.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Service.Interface
{
    public interface IReactionService
    {
        Task<Reaction> Save(Guid id, Guid profileId, Reaction reaction);
    }
}

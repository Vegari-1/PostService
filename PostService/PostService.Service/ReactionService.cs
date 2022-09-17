using PostService.Model;
using PostService.Repository.Interface;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace PostService.Service.Interface
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;

        public ReactionService(IReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        public Task<Reaction> Save(Guid id, Guid profileId, Reaction reaction)
        {
            return _reactionRepository.Save(id, profileId, reaction);
        }
    }
}

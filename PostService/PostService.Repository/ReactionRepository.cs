using PostService.Model;
using PostService.Repository.Interface;
using PostService.Repository.Interface.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class ReactionRepository : Repository<Reaction>, IReactionRepository
    {
        public ReactionRepository(AppDbContext context) : base(context) { }

        public async Task<Reaction> Save(Reaction reaction)
        {
            Reaction savedReaction = (from r in _context.Reactions
                                     where r.PostId == reaction.PostId && r.AuthorId == reaction.AuthorId
                                     select r).FirstOrDefault();
            Post resPost = (from post in _context.Posts
                            where post.Id == reaction.PostId
                            select post).FirstOrDefault();
            if (savedReaction == null)
            {
                await _context.Reactions.AddAsync(reaction);
                if (reaction.Positive)
                {
                    resPost.Likes++;
                }
                else
                {
                    resPost.Dislikes++;
                }
            } else
            {
                if (savedReaction.Positive && !reaction.Positive)
                {
                    resPost.Dislikes++;
                    resPost.Likes--;
                }
                else if (!savedReaction.Positive && reaction.Positive)
                {
                    resPost.Likes++;
                    resPost.Dislikes--;
                }
                savedReaction.Positive = reaction.Positive;
            }
            
            _context.SaveChanges();

            return reaction;
        }
    }
}

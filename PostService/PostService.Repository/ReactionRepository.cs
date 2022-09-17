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

        public async Task<Reaction> Save(Guid postId, string username, Reaction reaction)
        {
            var authorId = _context.Profiles
                           .Where(x => x.Username == username)
                           .Select(x => x.Id)
                           .FirstOrDefault();

            Reaction savedReaction = (from r in _context.Reactions
                                     where r.PostId == postId && r.AuthorId == authorId
                                     select r).FirstOrDefault();

            Post resPost = (from post in _context.Posts
                            where post.Id == postId
                            select post).FirstOrDefault();

            if (savedReaction == null)
            {
                reaction.AuthorId = authorId;
                reaction.PostId = postId;
                await _context.Reactions.AddAsync(reaction);
                if (reaction.Positive)
                {
                    resPost.LikesNumber++;
                    resPost.Likes.Add(reaction.AuthorId);
                }
                else
                {
                    resPost.DislikesNumber++;
                    resPost.Dislikes.Add(reaction.AuthorId);
                }
            } else
            {
                if (savedReaction.Positive && !reaction.Positive)
                {
                    resPost.DislikesNumber++;
                    resPost.LikesNumber--;
                    resPost.Dislikes.Add(reaction.AuthorId);
                    resPost.Likes.Remove(reaction.AuthorId);
                }
                else if (!savedReaction.Positive && reaction.Positive)
                {

                    resPost.LikesNumber++;
                    resPost.DislikesNumber--;
                    resPost.Likes.Add(reaction.AuthorId);
                    resPost.Dislikes.Remove(reaction.AuthorId);
                }
                savedReaction.Positive = reaction.Positive;
            }
            
            _context.SaveChanges();

            return reaction;
        }
    }
}

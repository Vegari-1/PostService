﻿using PostService.Model;
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
            await _context.Reactions.AddAsync(reaction);
            Post resPost = (from post in _context.Posts
                       where post.Id == reaction.PostId
                       select post).FirstOrDefault();
            if(reaction.Positive)
            {
                resPost.Likes++;
            } else
            {
                resPost.Dislikes++;
            }
            
            _context.SaveChanges();

            return reaction;
        }
    }
}

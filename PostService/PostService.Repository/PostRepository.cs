using PostService.Model;
using PostService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public Task<Post> GetPost()
        {
            throw new NotImplementedException();
        }

        public async Task<Post> Save(Post post)
        {
            await _context.Posts.AddAsync(post);
            _context.SaveChanges();

            return post;
        }
    }
}

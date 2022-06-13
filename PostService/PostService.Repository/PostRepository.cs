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

        public async Task<PagedList<Post>> FindAll(PaginationParams paginationParams)
        {
            return PagedList<Post>.ToPagedList(_context.Posts,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams)
        {
            var res = from post in _context.Posts
                      join profile in _context.Profiles on post.AuthorId equals profile.Id
                      where profile.Public == true
                      select post;

            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize); ;
        }
    }
}

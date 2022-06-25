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



        public async Task<PagedList<Post>> FindAllPublic(PaginationParams paginationParams)
        {
            var publicPosts = from post in _context.Posts
                              join profile in _context.Profiles on post.AuthorId equals profile.Id
                              where profile.Public == true
                              select post;

            return PagedList<Post>.ToPagedList(publicPosts,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllFollowed(PaginationParams paginationParams, Guid profileId)
        {
            var connections = from c in _context.Connections
                              select c;

            var profile1 = from post in _context.Posts
                           join connection in connections on post.AuthorId equals connection.Profile1
                           where post.AuthorId == profileId
                           select connection.Profile2;

            var profile2 = from post in _context.Posts
                           join connection in connections on post.AuthorId equals connection.Profile2
                           where post.AuthorId == profileId
                           select connection.Profile1;

            var connectedProfiles = profile1.Concat(profile2);
            var res = from profile in connectedProfiles
                      join post in _context.Posts on profile equals post.AuthorId
                      select post;

            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams, Guid profileId)
        {
            var connections = from c in _context.Connections
                              select c;

            var profile1 = from post in _context.Posts
                           join connection in connections on post.AuthorId equals connection.Profile1
                           where post.AuthorId == profileId
                           select connection.Profile2;

            var profile2 = from post in _context.Posts
                           join connection in connections on post.AuthorId equals connection.Profile2
                           where post.AuthorId == profileId
                           select connection.Profile1;

            var connectedProfiles = profile1.Concat(profile2);
            var res = from profile in connectedProfiles
                      join post in _context.Posts on profile equals post.AuthorId
                      select post;
            var publicPosts = from post in _context.Posts
                              join profile in _context.Profiles on post.AuthorId equals profile.Id
                              where profile.Public == true
                              select post;
            res = res.Concat(publicPosts).Distinct();

            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }
    }
}

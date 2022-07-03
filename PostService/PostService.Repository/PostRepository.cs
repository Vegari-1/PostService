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
            var connections = (from c in _context.Connections
                               where c.Profile1 == profileId || c.Profile2 == profileId
                               select c).Distinct();

            var profile1 = from profile in _context.Profiles
                           join connection in connections on profile.Id equals connection.Profile1
                           select connection.Profile2;

            var profile2 = from profile in _context.Profiles
                           join connection in connections on profile.Id equals connection.Profile2
                           select connection.Profile2;

            var connectedProfiles = profile1.Concat(profile2).Distinct();
            var res = (from profile in connectedProfiles
                      join post in _context.Posts on profile equals post.AuthorId
                      select post).Distinct();

            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams, Guid profileId)
        {
            var followed = await FindAllFollowed(paginationParams, profileId);
            var publicPosts = await FindAllPublic(paginationParams);
            var res = followed.ToList().Concat(publicPosts.ToList()).AsQueryable().Distinct();

            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }
    }
}

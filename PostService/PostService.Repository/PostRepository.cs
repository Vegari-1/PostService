using Microsoft.EntityFrameworkCore;
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

        public async Task<PagedList<Post>> FindAll(PaginationParams paginationParams)
        {
            var posts = (from post in _context.Posts
                         select new Post()
                        {
                            Id = post.Id,
                            AuthorId = post.AuthorId,
                            Content = post.Content,
                            Likes = post.Likes,
                            Dislikes = post.Dislikes,
                            TimeStamp = post.TimeStamp,
                            Images = (from image in _context.Images
                                      where image.PostId == post.Id
                                      select image).ToList(),
                            Comments = (from comment in _context.Comments
                                        join profile in _context.Profiles on comment.AuthorId equals profile.Id
                                        select new Comment()
                                        {
                                            Id = comment.Id,
                                            Content = comment.Content,
                                            AuthorId = comment.AuthorId,
                                            PostId = comment.PostId,
                                            TimeStamp = comment.TimeStamp,
                                            Name = profile.Name,
                                            Surname = profile.Surname,
                                            Username = profile.Username,
                                            Avatar = profile.Avatar
                                        }).ToList()

                        });


            posts = posts.Where(x => x.Id.ToString() == "908a9615-8700-49a3-80c2-a66e1a972a11");

            return PagedList<Post>.ToPagedList(posts,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllProfilePosts(PaginationParams paginationParams, Guid profileId)
        {
            var posts = (from post in _context.Posts
                         where post.AuthorId == profileId
                         orderby post.TimeStamp descending
                         select new Post()
                         {
                             Id = post.Id,
                             AuthorId = post.AuthorId,
                             Content = post.Content,
                             Likes = post.Likes,
                             LikesNumber = post.LikesNumber,
                             Dislikes = post.Dislikes,
                             DislikesNumber = post.DislikesNumber,
                             TimeStamp = post.TimeStamp,
                             Images = (from image in _context.Images
                                       where image.PostId == post.Id
                                       select image).ToList(),
                             Comments = (from comment in _context.Comments
                                         join profile in _context.Profiles on comment.AuthorId equals profile.Id
                                         where comment.PostId == post.Id
                                         select new Comment()
                                         {
                                             Id = comment.Id,
                                             Content = comment.Content,
                                             AuthorId = comment.AuthorId,
                                             PostId = comment.PostId,
                                             TimeStamp = comment.TimeStamp,
                                             Name = profile.Name,
                                             Surname = profile.Surname,
                                             Username = profile.Username,
                                             Avatar = profile.Avatar
                                         }).ToList()

                         });
            return PagedList<Post>.ToPagedList(posts,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        public async Task<PagedList<Post>> FindAllFollowedByUsername(PaginationParams paginationParams, Guid profileId)
        {
            var connectedProfiles = await GetFollowedProfilesIds(profileId);

            var res = (from post in _context.Posts
                       where connectedProfiles.Contains(post.AuthorId)
                       orderby post.TimeStamp descending
                       select new Post()
                       {
                           Id = post.Id,
                           AuthorId = post.AuthorId,
                           Content = post.Content,
                           Likes = post.Likes,
                           LikesNumber = post.LikesNumber,
                           Dislikes = post.Dislikes,
                           DislikesNumber = post.DislikesNumber,
                            TimeStamp = post.TimeStamp,
                           Images = (from image in _context.Images
                                     where image.PostId == post.Id
                                     select image).ToList(),
                           Comments = (from comment in _context.Comments
                                       join commentProfile in _context.Profiles on comment.AuthorId equals commentProfile.Id
                                       where comment.PostId == post.Id
                                       select new Comment()
                                       {
                                           Id = comment.Id,
                                           Content = comment.Content,
                                           AuthorId = comment.AuthorId,
                                           PostId = comment.PostId,
                                           TimeStamp = comment.TimeStamp,
                                           Name = commentProfile.Name,
                                           Surname = commentProfile.Surname,
                                           Username = commentProfile.Username,
                                           Avatar = commentProfile.Avatar
                                       }).ToList()

                       });


            return PagedList<Post>.ToPagedList(res,
                                               paginationParams.PageNumber,
                                               paginationParams.PageSize);
        }

        private async Task<IReadOnlyList<Guid>> GetFollowedProfilesIds(Guid profileId)
        {
            var connections = (from c in _context.Connections
                               where c.Profile1 == profileId || c.Profile2 == profileId
                               select c).Distinct();

            var profile1 = from profile in _context.Profiles
                           join connection in connections on profile.Id equals connection.Profile1
                           select connection.Profile2;

            var profile2 = from profile in _context.Profiles
                           join connection in connections on profile.Id equals connection.Profile2
                           select connection.Profile1;

            var connectedProfiles = profile1.Concat(profile2).Distinct();

            return connectedProfiles.ToList();

        }

        public async Task<IReadOnlyList<Post>> SearchPostByContent(Guid profileId, string query)
        {

            var connectedProfiles = await GetFollowedProfilesIds(profileId);

            var posts =  (from post in _context.Posts
                         where connectedProfiles.Contains(post.AuthorId) && 
                               post.Content.ToLower().Contains(query.ToLower())
                         orderby post.TimeStamp descending
                         select new Post()
                         {
                             Id = post.Id,
                             AuthorId = post.AuthorId,
                             Content = post.Content,
                             Likes = post.Likes,
                             LikesNumber = post.LikesNumber,
                             Dislikes = post.Dislikes,
                             DislikesNumber = post.DislikesNumber,
                             TimeStamp = post.TimeStamp,
                             Images = (from image in _context.Images
                                       where image.PostId == post.Id
                                       select image).ToList(),
                             Comments = (from comment in _context.Comments
                                         join profile in _context.Profiles on comment.AuthorId equals profile.Id
                                         select new Comment()
                                         {
                                             Id = comment.Id,
                                             Content = comment.Content,
                                             AuthorId = comment.AuthorId,
                                             PostId = comment.PostId,
                                             TimeStamp = comment.TimeStamp,
                                             Name = profile.Name,
                                             Surname = profile.Surname,
                                             Username = profile.Username,
                                             Avatar = profile.Avatar
                                         }).ToList()

                         });
            return posts.ToList();
        }
    }
}

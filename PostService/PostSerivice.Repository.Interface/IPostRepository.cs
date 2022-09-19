using PostService.Model;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PostService.Repository.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IReadOnlyList<Post>> SearchPostByContent(Guid id, string query);
        Task<PagedList<Post>> FindAll(PaginationParams paginationParams);
        Task<PagedList<Post>> FindAllProfilePosts(PaginationParams paginationParams, Guid profileId);
        Task<PagedList<Post>> FindAllFollowedByUsername(PaginationParams paginationParams, Guid profileId);
        Task<Post> GetPost();
    }
}


using PostService.Model;
using PostService.Repository.Interface.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Service.Interface
{
    public interface IPostService
    {
        Task<Post> Save(Post post);

        Task<PagedList<Post>> FindAll(PaginationParams paginationParams);

        Task<IReadOnlyList<Post>> SearchPostByContent(Guid id, string query);

        Task<PagedList<Post>> FindAllProfilePosts(PaginationParams paginationParams, Guid profileId);

        Task<PagedList<Post>> FindAllFollowed(PaginationParams paginationParams, Guid profileId);
    }
}

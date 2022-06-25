
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

        Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams, Guid profileId);

        Task<PagedList<Post>> FindAllPublic(PaginationParams paginationParams);

        Task<PagedList<Post>> FindAllFollowed(PaginationParams paginationParams, Guid profileId);
    }
}

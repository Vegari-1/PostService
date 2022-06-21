using PostService.Model;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> Save(Post post);
        Task<PagedList<Post>> FindAll(PaginationParams paginationParams);
        Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams);
        Task<Post> GetPost();
    }
}

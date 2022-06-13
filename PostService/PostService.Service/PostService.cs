using PostService.Model;
using PostService.Repository.Interface;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PostService.Service.Interface
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostsService(IPostRepository IPostRepository)
        {
            _postRepository = IPostRepository;
        }

        public Task<Post> Save(Post post)
        {
            return _postRepository.Save(post);
        }

        public Task<PagedList<Post>> FindAll(PaginationParams paginationParams)
        {
            return _postRepository.FindAll(paginationParams);
        }

        public Task<PagedList<Post>> FindAllPublicAndFollowed(PaginationParams paginationParams)
        {
            return _postRepository.FindAllPublicAndFollowed(paginationParams);
        }
    }
}

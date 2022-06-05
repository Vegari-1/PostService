using PostService.Model;
using PostService.Repository.Interface;
using System.Threading.Tasks;

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
    }
}

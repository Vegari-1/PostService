using PostService.Model;
using System.Threading.Tasks;

namespace PostService.Repository.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> Save(Post post);

        Task<Post> GetPost();
    }
}

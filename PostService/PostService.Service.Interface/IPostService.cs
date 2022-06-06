
using PostService.Model;
using System.Threading.Tasks;

namespace PostService.Service.Interface
{
    public interface IPostService
    {
        Task<Post> Save(Post post);
    }
}

using Microsoft.EntityFrameworkCore;
using PostService.Model;

namespace PostService.Repository
{
    public interface IAppDbContext
    {
        DbSet<Post> Posts { get; set; }
    }
}

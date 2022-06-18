using Microsoft.EntityFrameworkCore;
using PostService.Model;

namespace PostService.Repository
{
    public class AppDbContext : DbContext, IAppDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Connection> Connections { get; set; }

    }
}

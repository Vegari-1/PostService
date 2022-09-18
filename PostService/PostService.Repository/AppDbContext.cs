using Microsoft.EntityFrameworkCore;
using PostService.Model;
using PostService.Model.Sync;
using System;

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

        public DbSet<Reaction> Reactions { get; set; }
    
        public DbSet<Comment> Comments { get; set;  }

        public DbSet<Image> Images { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Post>()
        //    .HasMany(p => p.Images);

        //}
    }
}

using lab2.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab2
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);
                u.ToTable("Users");
            });

            modelBuilder.Entity<Image>(i =>
            {
                i.HasKey(i => i.Id);
                i.HasOne(i => i.User).WithMany(u => u.Images).OnDelete(DeleteBehavior.NoAction);
                i.ToTable("Images");
            });

            modelBuilder.Entity<Like>(l =>
            {
                l.HasKey(l => l.Id);
                l.HasOne(l => l.User).WithMany(u => u.Likes).OnDelete(DeleteBehavior.NoAction);
                l.HasOne(l => l.Image).WithMany(i => i.Likes).OnDelete(DeleteBehavior.NoAction);
                l.ToTable("Likes");
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
    }
}

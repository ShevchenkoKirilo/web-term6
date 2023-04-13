using Microsoft.EntityFrameworkCore;

namespace Module1
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(s =>
            {
                s.HasKey(s => s.Nick);
                s.ToTable("Students");
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Student> Students { get; set; }
    }
}

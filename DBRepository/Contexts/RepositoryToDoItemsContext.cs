using Models;
using Microsoft.EntityFrameworkCore;

namespace DBRepository.Contexts
{
    public class RepositoryToDoItemsContext : DbContext
    {
        public RepositoryToDoItemsContext(DbContextOptions<RepositoryToDoItemsContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Items", "ToDo");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Date);
            });
        }
    }
}

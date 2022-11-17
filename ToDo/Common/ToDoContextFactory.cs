using DBRepository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDo.Common
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<RepositoryToDoItemsContext>
    {
        public RepositoryToDoItemsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryToDoItemsContext>();

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ToDo;User Id=MAStepuchev;Password=Maks7755991;";
            optionsBuilder.UseSqlServer(connectionString);
            return new RepositoryToDoItemsContext(optionsBuilder.Options);
        }
    }
}

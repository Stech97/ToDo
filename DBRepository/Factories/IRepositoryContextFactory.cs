using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DBRepository.Factories
{
    public interface IRepositoryContextFactory
    {
        public T CreateDbContextMSSqlEFCore<T>(string connectionString) where T : DbContext;

        public IDbConnection CreateDbContextMsSqlDapper(string connectionString);
    }
}

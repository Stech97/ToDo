using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DBRepository.Factories
{
	public class RepositoryContextFactory : IRepositoryContextFactory
	{

		public T CreateDbContextMSSqlEFCore<T>(string connectionString) where T : DbContext
		{
			var optionsBuilder = new DbContextOptionsBuilder<T>();
			optionsBuilder.UseSqlServer(connectionString);
			optionsBuilder.EnableSensitiveDataLogging();

			return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
		}

		public IDbConnection CreateDbContextMsSqlDapper(string connectionString)
		{
			var connect = new SqlConnection(connectionString);
			connect.Open();
			return connect;
		}
	}
}

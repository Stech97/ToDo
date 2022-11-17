using ToDoBL.Interface;
using ToDoBL.Implementation;
using DBRepository.Factories;
using ToDoRepositories.Interfaces;
using ToDoRepositories.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider ServiceProvider;
        public IConfiguration Configuration { get; private set; }
        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging();
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IItemsRepository, ItemsRepository>(provider => new ItemsRepository(
                Configuration.GetConnectionString("DefaultConnection"),
                provider.GetRequiredService<IRepositoryContextFactory>(),
                provider.GetRequiredService<ILogger<ItemsRepository>>()));

            services.AddScoped<IItemsService, ItemsService>(provider => new ItemsService(
                provider.GetRequiredService<IItemsRepository>(),
                provider.GetRequiredService<ILogger<ItemsService>>()));

            services.AddSingleton<MainWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}

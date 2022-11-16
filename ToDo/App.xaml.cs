using ToDoBL.Interface;
using ToDoBL.Implementation;
using DBRepository.Factories;
using ToDoRepositories.Interfaces;
using ToDoRepositories.Repositories;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging();
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IItemsRepository, ItemsRepository>(provider => new ItemsRepository(
                "Server=(localdb)\\MSSQLLocalDB;Database=ToDo;User Id=MAStepuchev;Password=Maks7755991;",
                provider.GetRequiredService<IRepositoryContextFactory>(),
                provider.GetRequiredService<ILogger<ItemsRepository>>()));

            services.AddScoped<IItemsService, ItemsService>(provider => new ItemsService(
                provider.GetRequiredService<IItemsRepository>(),
                provider.GetRequiredService<ILogger<ItemsService>>()));

            services.AddSingleton<MainWindow> (/*provider => new MainWindow(provider.GetRequiredService<IItemsService>())*/);
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}

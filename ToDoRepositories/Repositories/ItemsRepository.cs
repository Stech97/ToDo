using DBRepository;
using DBRepository.Contexts;
using DBRepository.Factories;
using ToDoRepositories.Interfaces;
using Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ToDoRepositories.Repositories
{
    public class ItemsRepository : BaseRepository, IItemsRepository
    {
        private readonly ILogger<ItemsRepository> _logger;

        public ItemsRepository(string connectionString, IRepositoryContextFactory contextFactory, ILogger<ItemsRepository> logger)
            : base(connectionString, contextFactory) => _logger = logger;

        public async Task<IEnumerable<Item>> GetItemsByDateOrDefaultAsync(DateTime Date)
        {
            _logger.LogInformation($"Получаю задачи из БД на дату: {Date:D}");
            try
            {
                using var context = ContextFactory.CreateDbContextMSSqlEFCore<RepositoryToDoItemsContext>(ConnectionString);
                return await context.Items.Where(x => x.Date.Date == Date.Date).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Не удалось получить задачи на дату: {Date:D}. Ошибка: {e.Message} Стек:{e.StackTrace}");
                throw;
            }
        }

        public async Task<int> GetTotalIntemsOnDateAsync(DateTime Date)
        {
            _logger.LogInformation($"Получаю количество задач из БД на дату: {Date:D}");
            try 
            {
                using var context = ContextFactory.CreateDbContextMSSqlEFCore<RepositoryToDoItemsContext>(ConnectionString);
                return await context.Items.Where(x => x.Date.Date == Date.Date).CountAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Не удалось получить количество задач на дату: {Date:D}. Ошибка: {e.Message} Стек:{e.StackTrace}");
                throw;
            }
        }
        public async Task<int> GetLeftIntemsOnDateAsync(DateTime Date)
        {
            _logger.LogInformation($"Получаю количество не выполненых задач из БД на дату: {Date:D}");
            try
            {
                using var context = ContextFactory.CreateDbContextMSSqlEFCore<RepositoryToDoItemsContext>(ConnectionString);
                return await context.Items.Where(x => x.Date.Date == Date.Date && !x.IsDone).CountAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Не удалось получить количество не выполненых задач на дату: {Date:D}. Ошибка: {e.Message} Стек:{e.StackTrace}");
                throw;
            }
        }

        public async Task<bool> AddItemAsync(Item Item)
        {
            _logger.LogInformation($"Добавляю новую задачу в БД");
            try
            {
                using var context = ContextFactory.CreateDbContextMSSqlEFCore<RepositoryToDoItemsContext>(ConnectionString);

                var ItemDTO = context.Items.FirstOrDefault(x => x.Title == Item.Title && x.Date == Item.Date && x.TimeStart == Item.TimeStart && x.TimeEnd == Item.TimeEnd);
                if (ItemDTO is null)
                {
                    await context.Items.AddAsync(Item);
                    await context.SaveChangesAsync();
                    _logger.LogInformation("Задача добавлена в БД");
                    return true;
                }
                else
                {
                    _logger.LogWarning("Данная запись уже существует в БД");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Не удалось добавить задачу в БД. Ошибка: {e.Message} Стек:{e.StackTrace}");
                throw;
            }
        }
    }
}
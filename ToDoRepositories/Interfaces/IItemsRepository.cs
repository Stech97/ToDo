using Models;

namespace ToDoRepositories.Interfaces
{
    public interface IItemsRepository
    {
        public Task<IEnumerable<Item>> GetItemsByDateOrDefaultAsync(DateTime Date);
        public Task<int> GetTotalIntemsOnDateAsync(DateTime Date);
        public Task<int> GetLeftIntemsOnDateAsync(DateTime Date);
        public Task<bool> AddItemAsync(Item Item);
    }
}

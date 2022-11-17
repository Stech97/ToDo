using Models;

namespace ToDoRepositories.Interfaces
{
    public interface IItemsRepository
    {
        public Task<IEnumerable<Item>> GetItemsByDateOrDefaultAsync(DateTime Date);
        public Task<int> GetTotalIntemsOnDateAsync(DateTime Date);
        public Task<int> GetLeftIntemsOnDateAsync(DateTime Date);
        public Task<Item> GetItemByParametrsAsync(Item item);
        public Task<bool> AddItemAsync(Item Item);
        public Task UpdateItemAsync(Item Item);
        public Task DeleteItemAsync(Item Item);
    }
}

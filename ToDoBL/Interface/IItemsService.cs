using Models;

namespace ToDoBL.Interface
{
    public interface IItemsService
    {
        public Task<IEnumerable<ItemVM>> GetItemsByDateAsync(DateTime date);

        public Task<string> GetTotalItemsOnDateAsync(DateTime dateTime);
        public Task<string> GetLeftItemsOnDateAsync(DateTime dateTime);
        public Task<bool> AddItemAsync(string Title, string Time, DateTime Date);
        public Task<bool> UpdateItemAsync(ItemVM itemVM);
        public Task<bool> UpdateNotifyAsync(ItemVM itemVM);
        public Task<bool> DeleteItemAsync(ItemVM itemVM);
    }
}

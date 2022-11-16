using ToDoRepositories.Interfaces;
using Models;
using Microsoft.Extensions.Logging;
using ToDoBL.Interface;

namespace ToDoBL.Implementation
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly ILogger<ItemsService> _logger;

        public ItemsService(IItemsRepository itemsRepository, ILogger<ItemsService> logger)
        {
            _itemsRepository = itemsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemVM>> GetItemsByDateAsync(DateTime date)
        {
            _logger.LogInformation("Получаю задачи");
            var ItemsDTO = await _itemsRepository.GetItemsByDateOrDefaultAsync(date);
            List<ItemVM> ItemsVM = new();
            foreach (var ItemDTO in ItemsDTO)
            {
                ItemVM itemVM = new()
                {
                    Title = ItemDTO.Title,
                    Time = ItemDTO.TimeStart.ToString("t") + " - " + ItemDTO.TimeEnd.ToString("t")
                };

                if (ItemDTO.IsDone)
                    itemVM.Icon = "CheckCircle";
                else
                    itemVM.Icon = "CircleThin";

                if (ItemDTO.IsNotify)
                    itemVM.IconBell = "Bell";
                else
                    itemVM.IconBell = "BellSlash";

                if (!ItemDTO.IsDone && ItemDTO.TimeStart < DateTime.Now)
                    itemVM.Color = "#EBA5BB";
                else
                    itemVM.Color = "#F1F1F1";

                ItemsVM.Add(itemVM);
            }

            _logger.LogInformation("Задачи получены");
            return ItemsVM;
        }

        public async Task<string> GetTotalItemsOnDateAsync(DateTime dateTime)
        {
            _logger.LogInformation("Получаю общее количество задач");
            var count = await _itemsRepository.GetTotalIntemsOnDateAsync(dateTime);
            return "На сегодня запланировано " + count + " задач";
        }

        public async Task<string> GetLeftItemsOnDateAsync(DateTime dateTime)
        {
            _logger.LogInformation("Получаю общее количество задач");
            var count = await _itemsRepository.GetLeftIntemsOnDateAsync(dateTime);
            return "Остолось выполнить " + count + " задач";
        }


        public async Task<bool> AddItemAsync(string Title, string Time, DateTime Date)
        {
            Item itemDTO = new()
            {
                Title = Title,
                Date = Date.Date,
                IsDone = false,
                IsNotify = true
            };
            try
            {
                var TimeStart = Time.Split("-")[0].Trim();
                var TimeEnd = Time.Split("-")[1].Trim();

                if (TimeStart is not null && TimeEnd is not null)
                {
                    itemDTO.TimeStart = DateTime.Parse(TimeStart);
                    itemDTO.TimeEnd = DateTime.Parse(TimeEnd);
                    itemDTO.TimeStart = itemDTO.TimeStart.AddDays((Date.Date - itemDTO.TimeStart.Date).Days);
                    itemDTO.TimeEnd = itemDTO.TimeEnd.AddDays((Date.Date - itemDTO.TimeEnd.Date).Days);
                }
                else
                    return false;

                return await _itemsRepository.AddItemAsync(itemDTO);
            }
            catch
            {
                return false;
            }
        }


    }
}

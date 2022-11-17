using System.Windows;
using ToDoBL.Interface;

namespace ToDo.Common
{
    public class MenuButtonViewModel
    {
        private readonly IItemsService _itemsService;
        private RelayCommand doneCommand;
        private RelayCommand deleteCommand;
        private RelayCommand changeComand;

        public MenuButtonViewModel(IItemsService itemsService) => _itemsService = itemsService;

        public RelayCommand DoneCommand
        {
            get
            {
                return doneCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    var ItemUC = selectedItem as UserControls.Item;
                    var ItemVMs = ItemUC.ItemVM;

                    var IsUpdate = await _itemsService.UpdateItemAsync(ItemVMs);
                    if (IsUpdate)
                    {
                        var oldMain = Application.Current.MainWindow as MainWindow;
                        oldMain.GetItems(ItemVMs.Date);
                    }
                    else
                        MessageBox.Show("При обновлении заметки возникли ошибки", "Ошибка при обновлении заметки", MessageBoxButton.OK);
                });
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    var ItemUC = selectedItem as UserControls.Item;
                    var ItemVMs = ItemUC.ItemVM;

                    var IsDelete = await _itemsService.DeleteItemAsync(ItemVMs);
                    if (IsDelete)
                    {
                        var oldMain = Application.Current.MainWindow as MainWindow;
                        oldMain.GetItems(ItemVMs.Date);
                    }
                    else
                        MessageBox.Show("При удалении заметки возникли ошибки", "Ошибка при удалении заметки", MessageBoxButton.OK);
                });
            }
        }

        public RelayCommand ChangeCommand
        {
            get
            {
                return changeComand ??= new RelayCommand(async (selectedItem) =>
                {
                    var ItemUC = selectedItem as UserControls.Item;
                    var ItemVMs = ItemUC.ItemVM;

                    var IsUpdate = await _itemsService.UpdateNotifyAsync(ItemVMs);
                    if (IsUpdate)
                    {
                        var oldMain = Application.Current.MainWindow as MainWindow;
                        oldMain.GetItems(ItemVMs.Date);
                    }
                    else
                        MessageBox.Show("При обновлении заметки возникли ошибки", "Ошибка при обновлении заметки", MessageBoxButton.OK);
                });
            }
        }
    }

}

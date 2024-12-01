using ShopWorld.Shared;
using ShopWorld.Shared.Models;

namespace ShopWorld.BusinessLogic
{
    public interface IItemLogic
    {
        IEnumerable<ItemModel> GetAllItems();
        ItemModel AddItem(ItemInputModel Item);
        ItemModel GetItem(int ItemId);
        bool UpdateItem(ItemInputModel Item);
        bool DeleteItem(int ItemId);
        Task<string> GetBase64ImageForImageName(string ImageName);
    }
}

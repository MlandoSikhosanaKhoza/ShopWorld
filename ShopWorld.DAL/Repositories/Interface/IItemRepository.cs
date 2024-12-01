namespace ShopWorld.DAL
{
    public interface IItemRepository
    {
        List<Item> GetAllItems();
        Item AddItem(Item ItemToAdd);
        Item GetItem(int ItemId);
        bool UpdateItem(Item ItemToUpdate);
        bool DeleteItem(int ItemId);
    }
}

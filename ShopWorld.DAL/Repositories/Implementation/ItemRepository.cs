

namespace ShopWorld.DAL
{
    public class ItemRepository:IItemRepository
    {
        private readonly IGenericRepository<Item> _itemRepository;
        private IUnitOfWork _unitOfWork;
        public ItemRepository(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _itemRepository = UnitOfWork.GetRepository<Item>();
        }
        public List<Item> GetAllItems()
        {
            return _itemRepository.Get(e => !e.IsDeleted).ToList();
        }


        public Item AddItem(Item ItemToAdd)
        {
            Item itemAdded = _itemRepository.Insert(ItemToAdd);
            _unitOfWork.SaveChanges();
            return itemAdded;
        }


        public Item GetItem(int ItemId)
        {
            return _itemRepository.GetById(ItemId);
        }

        public bool UpdateItem(Item ItemToUpdate)
        {
            _itemRepository.Update(ItemToUpdate);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteItem(int ItemId)
        {
            Item item = _itemRepository.GetById(ItemId);
            item.IsDeleted = true;
            _itemRepository.Update(item);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

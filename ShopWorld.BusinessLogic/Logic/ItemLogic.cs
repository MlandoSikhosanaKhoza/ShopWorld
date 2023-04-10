using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopWorld.BusinessLogic
{
    public class ItemLogic:IItemLogic
    {
        private GenericRepository<Item> ItemRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public ItemLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            ItemRepository = UnitOfWork.GetRepository<Item>();
        }

        public List<Item> GetAllItems()
        {
            return ItemRepository.Get(e => !e.IsDeleted).ToList();
        }

        public Item AddItem(Item Item)
        {
            Item itemAdded=ItemRepository.Insert(Item);
            _unitOfWork.SaveChanges();
            return itemAdded;
        }

        public Item GetItem(int ItemId)
        {
            return ItemRepository.GetById(ItemId);
        }

        public bool UpdateItem(Item Item)
        {
            ItemRepository.Update(Item);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteItem(int ItemId)
        {
            Item item = GetItem(ItemId);
            item.IsDeleted = true;
            UpdateItem(item);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

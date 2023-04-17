using ShopWorld.Shared;
using ShopWorld.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IItemLogic
    {
        List<Item> GetAllItems();
        Item AddItem(ItemInputModel Item);
        Item GetItem(int ItemId);
        bool UpdateItem(ItemInputModel Item);
        bool DeleteItem(int ItemId);
    }
}

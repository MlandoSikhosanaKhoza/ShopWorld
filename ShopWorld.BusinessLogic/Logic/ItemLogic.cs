using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace ShopWorld.BusinessLogic
{
    public class ItemLogic:IItemLogic
    {
        private GenericRepository<Item> ItemRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ItemLogic(IUnitOfWork UnitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = UnitOfWork;
            ItemRepository = UnitOfWork.GetRepository<Item>();
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Item> GetAllItems()
        {
            return ItemRepository.Get(e => !e.IsDeleted).ToList();
        }


        public Item AddItem(ItemInputModel ItemToAdd)
        {
            string path = StoreByteArrayFromBase64.Execute(ItemToAdd.Base64, $"{_webHostEnvironment.WebRootPath}/Images/", Path.GetExtension(ItemToAdd.ImageName));
            Item itemAdded = ItemRepository.Insert(new Item { ImageName=Path.GetFileName(path), Description=ItemToAdd.Description, Price=ItemToAdd.Price,IsDeleted=false});
            _unitOfWork.SaveChanges();
            return itemAdded;
        }

        public async Task<string> GetBase64ImageForImageName(string ImageName)
        {
            string path = System.IO.Path.Combine(_webHostEnvironment.WebRootPath,"Images", ImageName);
            byte[] bytes;
            string base64;
            if (!string.IsNullOrEmpty(ImageName))
            {
                if (System.IO.File.Exists(path))
                {
                    bytes = await System.IO.File.ReadAllBytesAsync(path);
                    base64 = Convert.ToBase64String(bytes);
                    return base64;
                }
            }
            return "";    
        }

        public Item GetItem(int ItemId)
        {
            return ItemRepository.GetById(ItemId);
        }

        public bool UpdateItem(ItemInputModel ItemToUpdate)
        {
            Item item = GetItem(ItemToUpdate.ItemId);
            string path,imageName;
            
            item.Description= ItemToUpdate.Description;
            item.Price = ItemToUpdate.Price;
            
            if (!string.IsNullOrEmpty(ItemToUpdate.Base64))
            {
                DeleteExistingFile.Execute($"{_webHostEnvironment.WebRootPath}/Images/{item.ImageName}");
                path = StoreByteArrayFromBase64.Execute(ItemToUpdate.Base64, $"{_webHostEnvironment.WebRootPath}/Images/{item.ImageName}", Path.GetExtension(ItemToUpdate.Base64));
                imageName=Path.GetFileName(path);
                item.ImageName = imageName;
            }

            ItemRepository.Update(item);
            
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteItem(int ItemId)
        {
            Item item = ItemRepository.GetById(ItemId);
            item.IsDeleted = true;
            ItemRepository.Update(item);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}

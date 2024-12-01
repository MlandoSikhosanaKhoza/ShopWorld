
using ShopWorld.Shared;
using ShopWorld.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using ShopWorld.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace ShopWorld.BusinessLogic
{
    public class ItemLogic : IItemLogic
    {
        private readonly IItemRepository _itemRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ItemLogic(IItemRepository itemRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _itemRepository     = itemRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper             = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemModel> GetAllItems()
        {
            return _itemRepository.GetAllItems().Select(_mapper.Map<ItemModel>);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemToAdd"></param>
        /// <returns></returns>
        public ItemModel AddItem(ItemInputModel ItemToAdd)
        {
            string path    = StoreByteArrayFromBase64.Execute(ItemToAdd.Base64, $"{_webHostEnvironment.WebRootPath}/Images/", Path.GetExtension(ItemToAdd.ImageName));
            Item itemAdded = _itemRepository.AddItem(
                new Item { 
                    ImageName   = Path.GetFileName(path), 
                    Description = ItemToAdd.Description, 
                    Price       = ItemToAdd.Price,
                    IsDeleted   = false
                });
            return _mapper.Map<ItemModel>(itemAdded);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImageName"></param>
        /// <returns></returns>
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

        public ItemModel GetItem(int ItemId)
        {
            Item item = _itemRepository.GetItem(ItemId);
            return _mapper.Map<ItemModel>(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemToUpdate"></param>
        /// <returns></returns>
        public bool UpdateItem(ItemInputModel ItemToUpdate)
        {
            Item item = _itemRepository.GetItem(ItemToUpdate.ItemId);
            string path, imageName;
            
            //Check if there is an image for the model
            if (!string.IsNullOrEmpty(ItemToUpdate.Base64))
            {
                //Delete existing image
                DeleteExistingFile.Execute($"{_webHostEnvironment.WebRootPath}/Images/{item.ImageName}");
                //Determine path and store image
                path           = StoreByteArrayFromBase64.Execute(ItemToUpdate.Base64, $"{_webHostEnvironment.WebRootPath}/Images/{item.ImageName}", Path.GetExtension(ItemToUpdate.Base64));
                imageName      = Path.GetFileName(path);
                item.ImageName = imageName;
            }

            _mapper.Map(ItemToUpdate, item);

            _itemRepository.UpdateItem(item);
            return true;
        }

        public bool DeleteItem(int ItemId)
        {
            return _itemRepository.DeleteItem(ItemId);
        }
    }
}

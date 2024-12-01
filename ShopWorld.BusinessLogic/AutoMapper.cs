using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopWorld.DAL;
using ShopWorld.Shared;
using ShopWorld.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.BusinessLogic
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();

            CreateMap<Item, ItemModel>().ReverseMap();
            CreateMap<Logistic,LogisticsModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderItem,OrderItemModel>().ReverseMap();
            CreateMap<StockItem, StockItemModel>().ReverseMap();
            CreateMap<Supplier, SupplierModel>().ReverseMap();
            CreateMap<SupplierLocation,SupplierLocationModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();

            CreateMap<ItemInputModel, Item>()
                .ForMember(m => m.ImageName, n => n.Ignore());
        }
    }
}

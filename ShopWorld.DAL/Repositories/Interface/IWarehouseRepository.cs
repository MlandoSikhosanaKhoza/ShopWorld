
namespace ShopWorld.DAL
{
    public interface IWarehouseRepository
    {
        List<Warehouse> GetWarehouses();
        bool DeleteWarehouse(Warehouse WarehouseObject);
        bool UpdateWarehouse(Warehouse WarehouseObject);
        Warehouse AddWarehouse(Warehouse WarehouseObject);
    }
}

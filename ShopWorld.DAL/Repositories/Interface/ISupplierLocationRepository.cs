

namespace ShopWorld.DAL
{
    public interface ISupplierLocationRepository
    {
        List<SupplierLocation> GetSupplierLocations();
        bool DeleteSupplierLocation(SupplierLocation SupplierLocationObject);
        bool UpdateSupplierLocation(SupplierLocation SupplierLocationObject);
        SupplierLocation AddSupplierLocation(SupplierLocation SupplierLocationObject);
    }
}


namespace ShopWorld.DAL
{
    public interface ISupplierRepository
    {
        List<Supplier> GetSuppliers();
        Supplier GetSupplierById(int SupplierId);
        List<Supplier> SearchSuppliers(string Name);
        bool DeleteSupplier(Supplier SupplierObject);
        bool UpdateSupplier(Supplier SupplierObject);
        Supplier AddSupplier(Supplier SupplierObject);
    }
}

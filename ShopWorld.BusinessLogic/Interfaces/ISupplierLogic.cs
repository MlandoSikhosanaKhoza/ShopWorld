using ShopWorld.Shared;
using ShopWorld.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.BusinessLogic
{
    public interface ISupplierLogic
    {
        IEnumerable<SupplierModel> GetSuppliers();
        SupplierModel GetSupplierById(int SupplierId);
        IEnumerable<SupplierModel> SearchSuppliers(string Name);
        SupplierModel AddSupplier(SupplierModel SupplierObj);
        bool UpdateSupplier(SupplierModel SupplierObj);
    }
}

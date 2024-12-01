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
    public class SupplierLogic : ISupplierLogic
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierLogic(ISupplierRepository supplierRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _supplierRepository = supplierRepository;
            _mapper             = mapper;
        }

        public IEnumerable<SupplierModel> GetSuppliers()
        {
            return _supplierRepository.GetSuppliers().Select(_mapper.Map<SupplierModel>);
        }

        public SupplierModel GetSupplierById(int SupplierId)
        {
            Supplier supplier = _supplierRepository.GetSupplierById(SupplierId);
            return _mapper.Map<SupplierModel>(supplier);
        }

        public IEnumerable<SupplierModel> SearchSuppliers(string Name)
        {
            return _supplierRepository.SearchSuppliers(Name).Select(_mapper.Map<SupplierModel>);
        }

        public SupplierModel AddSupplier(SupplierModel SupplierModel)
        {
            Supplier supplier = _mapper.Map<Supplier>(SupplierModel);
            supplier          = _supplierRepository.AddSupplier(supplier);

            return _mapper.Map<SupplierModel>(supplier);
        }

        public bool UpdateSupplier(SupplierModel SupplierModel) {
            Supplier supplier = _supplierRepository.GetSupplierById(SupplierModel.SupplierId);

            return _supplierRepository.UpdateSupplier(supplier);
        }
    }
}

namespace ShopWorld.DAL
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IGenericRepository<Warehouse> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WarehouseRepository(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Warehouse>();
        }
        
        public List<Warehouse> GetWarehouses()
        {
            return _repository.Get().ToList();
        }

        public bool DeleteWarehouse(Warehouse WarehouseObject) { 
            _repository.Delete(WarehouseObject);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateWarehouse(Warehouse WarehouseObject)
        {
            _repository.Update(WarehouseObject);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Warehouse AddWarehouse(Warehouse WarehouseObject)
        {
            Warehouse WarehouseAdded = _repository.Insert(WarehouseObject);
            _unitOfWork.SaveChanges();
            return WarehouseAdded;
        }


    }
}

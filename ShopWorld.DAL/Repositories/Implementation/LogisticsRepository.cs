namespace ShopWorld.DAL
{
    public class LogisticsRepository:ILogisticsRepository
    {
        private readonly IGenericRepository<Logistic> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LogisticsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Logistic>();
        }

        public List<Logistic> GetLogisticsList()
        {
            return _repository.Get().ToList();
        }

        public bool DeleteLogistics(Logistic LogisticsObject)
        {
            _repository.Delete(LogisticsObject);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteLogisticsById(int LogisticsId)
        {
            _repository.DeleteById(LogisticsId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateLogistics(Logistic LogisticsObject)
        {
            _repository.Update(LogisticsObject);
            _unitOfWork.SaveChanges();
            return true;
        }

        public Logistic AddLogistics(Logistic LogisticsObject)
        {
            Logistic LogisticsAdded = _repository.Insert(LogisticsObject);
            _unitOfWork.SaveChanges();
            return LogisticsAdded;
        }
    }
}

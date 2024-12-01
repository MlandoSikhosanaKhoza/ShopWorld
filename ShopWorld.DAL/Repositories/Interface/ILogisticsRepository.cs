namespace ShopWorld.DAL
{
    public interface ILogisticsRepository
    {
        List<Logistic> GetLogisticsList();
        bool DeleteLogistics(Logistic LogisticsObject);
        bool UpdateLogistics(Logistic LogisticsObject);
        Logistic AddLogistics(Logistic LogisticsObject);
        bool DeleteLogisticsById(int LogisticsId);
    }
}

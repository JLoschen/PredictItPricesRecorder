using System.Data;

namespace PredictItPriceRecorder.DataAccess
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

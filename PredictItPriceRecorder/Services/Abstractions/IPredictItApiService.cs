using System.Threading.Tasks;

namespace PredictItPriceRecorder.Services.Abstractions
{
    public interface IPredictItApiService
    {
        Task<MarketModel> GetMarket(int Id);
        void RunTest();
    }
}

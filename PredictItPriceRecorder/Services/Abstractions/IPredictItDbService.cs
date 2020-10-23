using PredictItPriceRecorder.Domain.Model;

namespace PredictItPriceRecorder.Services.Abstractions
{
    public interface IPredictItDbService
    {
        void RunTest();
        bool MarketExists(int Id);
        bool ContractExists(int Id);
        bool AddMarket(market market);
        bool AddContract(contract contract);
        bool AddPrice(contract_price price);
    }
}

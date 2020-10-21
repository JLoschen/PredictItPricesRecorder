using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Model;
using PredictItPriceRecorder.Services.Abstractions;

namespace PredictItPriceRecorder.Factory.Abstractions
{
    public interface IPredictItFactory
    {
        market GetMarketEntity(MarketModel model);
        contract GetContract(ContractModel contract, int marketId);
        contract_price GetContractPrice(ContractModel contract);
    }
}

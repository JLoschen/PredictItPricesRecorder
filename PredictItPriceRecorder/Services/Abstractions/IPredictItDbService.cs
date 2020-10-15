using PredictItPriceRecorder.Model;
using System.Collections.Generic;

namespace PredictItPriceRecorder.Services.Abstractions
{
    public interface IPredictItDbService
    {
        bool InsertMarket(MarketDbModel market);
        IEnumerable<MarketDbModel> GetMarkets();
    }
}

using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Model;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Collections.Generic;

namespace PredictItPriceRecorder.Factory
{
    public class PredictItFactory : IPredictItFactory
    {
        public market GetMarketEntity(MarketModel model)
        {
            return new market
            {
                market_id = model.Id,
                name = model.Name,
                url = model.Url,
                short_name = model.ShortName,
                contracts = GetContracts(model.Contracts)
            };
        }

        private ICollection<contract> GetContracts(List<ContractModel> contracts)
        {
            return new List<contract>();
        }
    }
}
using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Model;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

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
                contracts = GetContracts(model.Contracts ?? new List<ContractModel>(), model.Id)
            };
        }

        private ICollection<contract> GetContracts(List<ContractModel> contracts, int marketId)
            => contracts.Select(c => GetContract(c, marketId)).ToList();

        public contract GetContract(ContractModel contract, int marketId)
        {  
            var myContract = new contract
            {
                contract_id = contract.Id,
                market_id = marketId,
                date_end = contract.DateEnd,
                name = contract.Name,
                short_name = contract.ShortName,
            };

            myContract.contract_prices = new List<contract_price>() { GetContractPrice(contract) };

            return myContract;
        }

        public contract_price GetContractPrice(ContractModel contract)
        {
            var price = new contract_price
            {
                contract_id = contract.Id,
                time_stamp = DateTime.Now,
                last_trade_price = contract.LastTradePrice ?? -1,
                best_buy_yes_cost = contract.BestBuyYesCost ?? -1,
                best_buy_no_cost = contract.BestBuyNoCost ?? -1,
                best_sell_yes_cost = contract.BestSellYesCost ?? -1,
                best_sell_no_cost = contract.BestSellNoCost ?? -1,
                last_close_price = contract.LastClosePrice ?? -1,
                display_order = contract.DisplayOrder,
            };
            return price;
        }
            
    }
}
using Dapper;
using Microsoft.EntityFrameworkCore;
using PredictItPriceRecorder.DataAccess;
using PredictItPriceRecorder.Domain;
using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Model;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PredictItPriceRecorder.Services
{
    public class PredictItDbService : IPredictItDbService
    {
        private readonly PredictItContext _predictItContext;
        public PredictItDbService(PredictItContext predictItContext)
        {
            _predictItContext = predictItContext;
        }

        public void RunTest()
        {
            //var markets = _predictItContext.markets
            //                               .Include(m => m.contracts)
            //                               .ThenInclude(c => c.contract_prices).ToList();

            //foreach (var market in markets)
            //{
            //    Debug.WriteLine($"market:{market.name}");
            //    foreach (var contract in market.contracts)
            //    {
            //        Debug.WriteLine($"contract:{contract.name}");
            //        foreach (var price in contract.contract_prices)
            //        {
            //            Debug.WriteLine($"Price:{price.time_stamp} - {price.best_sell_yes_cost}");
            //        }
            //    }
            //}

            var myMarket = new market()
            {
                market_id = 123,
                name = "Who will win the presidency",
                url = "www.yourmom.com",
                short_name = "Who!?!",
                create_date = DateTime.Now,
                contracts = new List<contract>
                {
                    new contract()
                    {
                        market_id = 123,
                        contract_id = 234,
                        name = "Joe Biden",
                        short_name = "Joe",
                        create_date = DateTime.Now,
                        contract_prices = new List<contract_price>()
                        {
                            new contract_price()
                            {
                                contract_id = 234,
                                time_stamp = DateTime.Now,
                                last_trade_price = 0.4M,
                                best_buy_no_cost = 0.3M,
                            },
                        }
                    }
                },
            };

            _predictItContext.markets.Add(myMarket);
            var x = _predictItContext.SaveChanges();

        }

        public bool AddMarket(market market)
        {
            try
            {
                _predictItContext.markets.Add(market);
                return _predictItContext.SaveChanges() > 0;

            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool MarketExists(int id) => _predictItContext.markets.Any(m => m.market_id == id);

        public bool ContractExists(int id)
            => _predictItContext.contracts.Any(c => c.contract_id == id);
    }
}
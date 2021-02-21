using PredictItPriceRecorder.Domain;
using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Services.Abstractions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictItPriceRecorder.Services
{
    public class PredictItDbService : IPredictItDbService
    {
        private readonly PredictItContext _predictItContext;
        private readonly ILogger _logger;
        public PredictItDbService(PredictItContext predictItContext, ILogger logger)
        {
            _predictItContext = predictItContext;
            _logger = logger;
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
                _logger.Information($"Adding new market:{market.name}-{market.market_id}");
                _predictItContext.markets.Add(market);
                return _predictItContext.SaveChanges() > 0;
            }
            catch(Exception e)
            {
                _logger.Error(e, $"Error Adding new market:{market?.name}-{market?.market_id}");
                return false;
            }
        }

        public bool AddContract(contract contract)
        {
            try
            {
                _logger.Information($"Adding new contract:{contract?.name}-{contract?.contract_id}");
                _predictItContext.contracts.Add(contract);
                return _predictItContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error Adding contract:{contract?.name}-{contract?.contract_id} - market: {contract?.market?.name}");
                return false;
            }
        }

        public bool AddPrice(contract_price price)
        {
            try
            {
                _predictItContext.contract_prices.Add(price);
                return _predictItContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error setting price for contract:{price?.contract?.name}-{price?.contract?.contract_id}");
                return false;
            }
        }

        public bool MarketExists(int id) => _predictItContext.markets.Any(m => m.market_id == id);

        public bool ContractExists(int id)
            => _predictItContext.contracts.Any(c => c.contract_id == id);
    }
}
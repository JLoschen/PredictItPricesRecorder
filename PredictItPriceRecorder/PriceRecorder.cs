using Serilog;
using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using PredictItPriceRecorder.Model;
using System.Threading.Tasks;

namespace PredictItPriceRecorder
{
    public class PriceRecorder
    {
        private IPredictItApiService _api;
        private IPredictItDbService _db;
        private IPredictItFactory _factory;
        private readonly ILogger _logger;

        public PriceRecorder(IPredictItApiService predictItApi,
                              IPredictItDbService predictItDb,
                              IPredictItFactory factory,
                             ILogger logger)
        {
            _api = predictItApi;
            _db = predictItDb;
            _factory = factory;
            _logger = logger;
        }

        public async Task QueryPredictItApi()
        {
            //_api.RunTest();
            foreach (var marketId in MarketsToRecord)
            {
                var market = await _api.GetMarket(marketId);
                if (market == null)
                {
                    //Debug.WriteLine($"Failure querying API for market:{marketId}");
                    _logger.Error($"Failure querying API for market:{marketId}");
                    continue;
                }

                _logger.Information($"Got market {market.Name}");

                if (!_db.MarketExists(market.Id))
                {
                    AddMarket(market);
                }
                else
                {
                    _logger.Information($"Market {market.Name} already exists, updating prices");
                    foreach (var contract in market.Contracts)
                    {
                        if (!_db.ContractExists(contract.Id))
                        {
                            AddContract(contract, market.Id);
                        }

                        AddPrice(contract);
                    }
                }
            }
        }

        private void AddMarket(MarketModel market)
        {
            _logger.Information($"New market created, adding for the first time: {market.Name}");

            var marketEntity = _factory.GetMarketEntity(market);

            marketEntity.create_date = DateTime.Now;

            foreach (var contract in marketEntity.contracts)
            {
                contract.create_date = DateTime.Now;
            }

            var success = _db.AddMarket(marketEntity);
            var log = success ? "Successfully added market" : "Failure to add market";
            _logger.Information(log);
        }

        private void AddContract(ContractModel contract, int marketId)
        {
            var entity = _factory.GetContract(contract, marketId);
            entity.create_date = DateTime.Now;
            var success = _db.AddContract(entity);
            var log = success ? $"Successfully added contract {contract.ShortName}" : "Failure to add contract";
            _logger.Information(log);
        }

        private void AddPrice(ContractModel contract)
        {
            var price = _factory.GetContractPrice(contract);
            var success = _db.AddPrice(price);
            if (!success)
            {
                _logger.Error($"Failure to add contract price to Contract:{contract?.ShortName}");
            }
        }

        private int[] MarketsToRecord { get; } =
            { 
                //3633, //Dem Nom-closed
                //2721,//Which Party will win Presidency
                //5542,//Wisconsin
                //5597,//Minnesota
                //6874,//2022 Senate
                //2721,//2020 Presidental election
                //3698,//Who will win 2020 presidential market
                //6199,//Which member of Trumps cabinet will leave next
                //5717,//Next European leader out
                //6234,//Will Nasa find 2020's global average temp highest
                7053//2024 Republican nominee
            };
    }
}

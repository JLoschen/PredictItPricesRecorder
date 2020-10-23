using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Model;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace PredictItPriceRecorder
{
    public class Runner
    {
        private readonly Timer _timer;
        private const int _queryInterval = 10000;
        private IPredictItApiService _api;
        private IPredictItDbService _db;
        private IPredictItFactory _factory;

        public Runner(IPredictItApiService predictItApi, 
                      IPredictItDbService predictItDb,
                      IPredictItFactory factory)
        {
            _api = predictItApi;
            _db = predictItDb;
            _factory = factory;

            _timer = new Timer(_queryInterval) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            await QueryPredictItApi();
        }

        public async Task QueryPredictItApi()
        {
            foreach (var marketId in MarketsToRecord)
            {
                var market = await _api.GetMarket(marketId);
                if(market == null)
                {
                    Debug.WriteLine($"Failure querying API for market:{marketId}");
                    continue;
                }

                Debug.WriteLine($"Got market {market.Name}");

                if (!_db.MarketExists(market.Id))
                {
                    AddMarket(market);
                }
                else
                {
                    Debug.WriteLine($"Market {market.Name} already exists, updating prices");
                    foreach(var contract in market.Contracts)
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
            Debug.WriteLine($"New market created, adding for the first time: {market.Name}");

            var marketEntity = _factory.GetMarketEntity(market);

            marketEntity.create_date = DateTime.Now;

            foreach (var contract in marketEntity.contracts)
            {
                contract.create_date = DateTime.Now;
            }

            var success = _db.AddMarket(marketEntity);
            var log = success ? "Successfully added market" : "Failure to add market";
            Debug.WriteLine(log);
        }

        private void AddContract(ContractModel contract, int marketId)
        {
            var entity = _factory.GetContract(contract, marketId);
            entity.create_date = DateTime.Now;
            var success = _db.AddContract(entity);
            var log = success ? $"Successfully added contract {contract.ShortName}" : "Failure to add contract";
            Debug.WriteLine(log);
        }

        private void AddPrice(ContractModel contract)
        {
            var price = _factory.GetContractPrice(contract);
            var success = _db.AddPrice(price);
            if (!success)
            {
                Debug.WriteLine($"Failure to add contract price to Contract:{contract?.ShortName}");
            }
        }

        //api's to try:
        //https://predictit-f497e.firebaseio.com/contractOrderBook.json
        //https://ww.predictit.org/api/Trade/16010/OrderBook
        //https://ww.predictit.org/api/Account/Token
        //https://ww.predictit.org/api/Market/<MarketId>/Contracts/Stats
        //'You have to use ur loging credentials
        //attach that to a cookie to get the request
        //the cookies expire every 12 hours
        //or so 
        //so just manually update the cookie every 9 hours and by manually i mean using your bot obviously (it's bad you're not using javascript, I coulda give you my code)
        private int[] MarketsToRecord { get; } =
            { 
                //3633, //Dem Nom-closed
                2721,//Which Party will win Presidency
                //5542,//Wisconsin
                //5597,//Minnesota
                //6874,//2022 Senate
                //2721,//2020 Presidental election
                //3698,//Who will win 2020 presidential market
                //6199,//Which member of Trumps cabinet will leave next
                //5717,//Next European leader out
                //6234,//Will Nasa find 2020's global average temp highest
            };

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace PredictItPriceRecorder
{
    public class Heartbeat
    {
        private readonly Timer _timer;
        private const int _queryInterval = 10000;
        private IPredictItApiService _predictItApiService;
        private IPredictItDbService _predictItDbService;

        public Heartbeat(IPredictItApiService predictItApiService, IPredictItDbService predictItDbService)
        {
            //_timer = new Timer(1000) { AutoReset = true };
            _predictItApiService = predictItApiService;
            _predictItDbService = predictItDbService;

            _timer = new Timer(_queryInterval) { AutoReset = true };
            //_timer.Elapsed += (o,e) => QueryPredictItApi();
            _timer.Elapsed += TimerElapsed;
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            await QueryPredictItApi();
        }

        //public void QueryPredictItApi(/*object sender, ElapsedEventArgs e*/)
        //{
        //    //string[] lines = new string[] { DateTime.Now.ToString() };
        //    //File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

        //    //TODO fire off query!
        //    foreach (var marketId in MarketsToRecord)
        //    {
        //        //var market = _predictItApiService.GetMarket(marketId).ConfigureAwait(false).GetAwaiter().GetResult();
        //        var market = _predictItApiService.GetMarket(marketId).Result;
        //        if (market == null)
        //        {
        //            Debug.WriteLine("I'm a failure :(");
        //        }
        //        else
        //        {
        //            Debug.WriteLine("Success");
        //        }
        //    }
        //    //_predictItApiService.RunTest();


        //    //test DB
        //    //var markets = _predictItDbService.GetMarkets();
        //    //foreach (var market in markets)
        //    //{
        //    //    Debug.WriteLine($"{market.Id} - {market.Name} - {market.Url}");
        //    //}
        //}

        public async Task QueryPredictItApi()
        {
            //string[] lines = new string[] { DateTime.Now.ToString() };
            //File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            //TODO fire off query!
            foreach (var marketId in MarketsToRecord)
            {
                //var market = _predictItApiService.GetMarket(marketId).ConfigureAwait(false).GetAwaiter().GetResult();
                var market = await _predictItApiService.GetMarket(marketId);
                if (market == null)
                {
                    Debug.WriteLine("I'm a failure :(");
                }
                else
                {
                    
                }
            }
            //_predictItApiService.RunTest();


            //test DB
            //var markets = _predictItDbService.GetMarkets();
            //foreach (var market in markets)
            //{
            //    Debug.WriteLine($"{market.Id} - {market.Name} - {market.Url}");
            //}
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
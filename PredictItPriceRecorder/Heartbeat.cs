using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace PredictItPriceRecorder
{
    public class Heartbeat
    {
        private readonly Timer _timer;
        private const int _queryInterval = 20000;
        private IPredictItApiService _predictItApiService;
        private IPredictItDbService _predictItDbService;

        public Heartbeat(IPredictItApiService predictItApiService, IPredictItDbService predictItDbService)
        {
            //_timer = new Timer(1000) { AutoReset = true };
            _predictItApiService = predictItApiService;
            _predictItDbService = predictItDbService;

            _timer = new Timer(_queryInterval) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //string[] lines = new string[] { DateTime.Now.ToString() };
            //File.AppendAllLines(@"C:\Temp\Demos\Heartbeat.txt", lines);

            //TODO fire off query!
            //foreach (var marketId in MarketsToRecord)
            //{
            //    var market =  _predictItApiService.GetMarket(marketId).ConfigureAwait(false).GetAwaiter().GetResult(); 
            //    if (market == null)
            //    {
            //        Debug.WriteLine("I'm a failure :(");
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Success");
            //    }
            //}
            //_predictItApiService.RunTest();


            //test DB
            //var markets = _predictItDbService.GetMarkets();
            //foreach (var market in markets)
            //{
            //    Debug.WriteLine($"{market.Id} - {market.Name} - {market.Url}");
            //}
        }

        private int[] MarketsToRecord { get; } =
            { 
                //3633, //Dem Nom-closed
                2721,//Which Party will win Presidency
                5542,//Wisconsin
                5597,//Minnesota
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
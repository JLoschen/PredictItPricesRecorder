using Ninject;
using PredictItPriceRecorder.Ninject;
using Serilog;
using System.Timers;

namespace PredictItPriceRecorder
{
    public class Runner
    {
        private readonly Timer _timer;
        private const int _queryInterval = 200000;
        private readonly ILogger _logger;

        public Runner(ILogger logger)
        {
            _logger = logger;

            _timer = new Timer(_queryInterval) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            using (var kernel = new StandardKernel(new PriceRecorderNinjectModule()))
            {
                var heart = kernel.Get<PriceRecorder>();
                heart.QueryPredictItApi().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        public void Start()
        {
            _logger.Information($"Starting the timer");
            _timer.Start();
        }

        public void Stop()
        {
            _logger.Information($"Stopping the timer");
            _timer.Stop();
        }

        public void BeforeStart()
        {
            _logger.Information($"Before Start()");
        }

        public void BeforeStop()
        {
            _logger.Information($"BeforeStop()");
        }
    }
}
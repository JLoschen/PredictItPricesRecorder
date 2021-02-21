using Ninject;
using PredictItPriceRecorder.Ninject;
using System;
using Topshelf;

namespace PredictItPriceRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            RunService();
            //RunTest();
        }

        private static void RunTest()
        {
            //var heart = GetHeartbeat();
            //heart.QueryPredictItApi().ConfigureAwait(false).GetAwaiter().GetResult();

            using (var kernel = new StandardKernel(new PriceRecorderNinjectModule()))
            {
                //var recorder = kernel.Get<Recorder>();
                //recorder.Run().ConfigureAwait(false).GetAwaiter().GetResult();
                var heart = kernel.Get</*Runner*/PriceRecorder>();
                heart.QueryPredictItApi().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        private static void RunService()
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Runner>(s =>
                {
                    //s.ConstructUsing(heartBeat => new Heartbeat());
                    //s.BeforeStartingService(s => s./*runner => runner.BeforeStart()*/);
                    s.ConstructUsing(runner => GetHeartbeat());
                    s.WhenStarted(runner => runner.Start());
                    s.WhenStopped(runner => runner.Stop());
                    
                });

                x.RunAsLocalSystem();

                x.SetServiceName("PredictItPriceService");
                x.SetDisplayName("PredictIt Price Service");
                x.SetDescription("Calls PredictIt API and records their prices in a local DB");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }

        private static Runner GetHeartbeat()
        {
            using (var kernel = new StandardKernel(new PriceRecorderNinjectModule()))
            {
                //var recorder = kernel.Get<Recorder>();
                //recorder.Run().ConfigureAwait(false).GetAwaiter().GetResult();
                return kernel.Get<Runner>();
            }
        }
    }
}

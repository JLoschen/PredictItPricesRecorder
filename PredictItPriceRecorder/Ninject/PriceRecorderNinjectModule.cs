using Ninject.Modules;
using PredictItPriceRecorder.DataAccess;
using PredictItPriceRecorder.Factory;
using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Services;
using PredictItPriceRecorder.Services.Abstractions;
using Serilog;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PredictItPriceRecorder.Ninject
{
    public class PriceRecorderNinjectModule : NinjectModule
    {
        private HttpClient _client;
        private readonly ILogger _logger;

        public PriceRecorderNinjectModule()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.predictit.org/api/marketdata/markets/")
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); /*;charset=utf-8*/
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _logger = new LoggerConfiguration()
                          .MinimumLevel.Debug()
                          //.WriteTo.File(@"C:\Users\Josh\Documents\PredictIt\PredictProfitCalculator\Logs\log.txt", rollingInterval: RollingInterval.Day)
                          .WriteTo.File(@"C:\Services\Logs\Log.txt", rollingInterval: RollingInterval.Day)
                          .CreateLogger();
        }

        public override void Load()
        {
            Kernel.Bind<IPredictItApiService>().To<PredictItApiService>();
            Kernel.Bind<HttpClient>().ToConstant(_client);
            Kernel.Bind<ILogger>().ToConstant(_logger);
            Kernel.Bind<IPredictItDbService>().To<PredictItDbService>();
            Kernel.Bind<IPredictItFactory>().To<PredictItFactory>();
            Bind<IDbConnectionFactory>()
                    .To<DbConnectionFactory>()
                    .WithConstructorArgument("connectionString",
                            ConfigurationManager.ConnectionStrings["PredictItDb"].ConnectionString);
        }
    }
}

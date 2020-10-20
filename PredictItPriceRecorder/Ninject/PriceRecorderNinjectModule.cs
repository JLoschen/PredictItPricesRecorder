using Ninject.Modules;
using PredictItPriceRecorder.DataAccess;
using PredictItPriceRecorder.Factory;
using PredictItPriceRecorder.Factory.Abstractions;
using PredictItPriceRecorder.Services;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PredictItPriceRecorder.Ninject
{
    public class PriceRecorderNinjectModule : NinjectModule
    {
        private HttpClient _client;

        public PriceRecorderNinjectModule()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.predictit.org/api/marketdata/markets/")
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); /*;charset=utf-8*/
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        }

        public override void Load()
        {
            Kernel.Bind<IPredictItApiService>().To<PredictItApiService>();
            Kernel.Bind<HttpClient>().ToConstant(_client);
            Kernel.Bind<IPredictItDbService>().To<PredictItDbService>();
            Kernel.Bind<IPredictItFactory>().To<PredictItFactory>();
            Bind<IDbConnectionFactory>()
                    .To<DbConnectionFactory>()
                    .WithConstructorArgument("connectionString",
                            ConfigurationManager.ConnectionStrings["PredictItDb"].ConnectionString);
        }
    }
}

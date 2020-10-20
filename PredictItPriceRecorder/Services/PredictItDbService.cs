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
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly PredictItContext _predictItContext;
        public PredictItDbService(IDbConnectionFactory dbConnectionFactory, PredictItContext predictItContext)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _predictItContext = predictItContext;
        }

        public bool InsertMarket(MarketDbModel market)
        {
            //select * from Market;
            var sql = @"INSERT INTO Market (Name, url) values (@Name, @Url);";
            using (var con = _dbConnectionFactory.CreateConnection())
            {
                try
                {
                    con.Execute(sql, market);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public IEnumerable<MarketDbModel> GetMarkets()
        {
            using (var con = _dbConnectionFactory.CreateConnection())
            {
                try
                {
                    var sql = "select * from Market;";
                    return con.Query<MarketDbModel>(sql).ToList();
                }
                catch (Exception e)
                {
                    return new List<MarketDbModel>();
                }
            }
        }

        public void RunTest()
        {
            var markets = _predictItContext.markets
                                           .Include(m => m.contracts)
                                           .ThenInclude(c => c.contract_prices).ToList();

            foreach (var market in markets)
            {
                Debug.WriteLine($"market:{market.name}");
                foreach (var contract in market.contracts)
                {
                    Debug.WriteLine($"contract:{contract.name}");
                    foreach (var price in contract.contract_prices)
                    {
                        Debug.WriteLine($"Price:{price.time_stamp} - {price.best_sell_yes_cost}");
                    }
                }
            }

            var market = new market()
            {

            }

        }

        public bool AddMarket(market market)
        {
            return false;
        }
    }
}
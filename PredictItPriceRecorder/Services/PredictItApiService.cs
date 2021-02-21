using Newtonsoft.Json;
using PredictItPriceRecorder.Services.Abstractions;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PredictItPriceRecorder.Services
{
    public class PredictItApiService : IPredictItApiService
    {
        private readonly HttpClient _predictClient;
        private readonly ILogger _logger;

        public PredictItApiService(HttpClient predictClient, ILogger logger)
        {
            _predictClient = predictClient;
            _logger = logger;
        }

        public async Task<MarketModel> GetMarket(int id)
        {
            try
            {
                _logger.Information($"Querying PredictIt API for marketid:{id}");
                using (HttpResponseMessage response = await _predictClient.GetAsync($"{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var cleanedJson = CleanJson(json);
                        return JsonConvert.DeserializeObject<MarketModel>(cleanedJson);
                    }
                    else
                    {
                        _logger.Error($"PredictIt API call unsuccessful, response code:{response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, $"exeption querying PredictIt API");
            }
            return null;
        }

        private string CleanJson(string json)
        {
            var cleanedJson = json.Replace("N/A", string.Empty);
            cleanedJson = cleanedJson.Replace("\"NA\"", "\"\"");
            return cleanedJson;
        }

        public void RunTest()
        {
            try
            {
                var market = JsonConvert.DeserializeObject<MarketModel>(CleanJson(marketJson));
            }
            catch (Exception e)
            {
                Console.WriteLine("What is you doing?");
            }
        }

        private string marketJson = "{\r\n  \"id\": 7053,\r\n  \"name\": \"Who will win the 2024 Republican presidential nomination?\",\r\n  \"shortName\": \"GOP 2024 presidential nominee?\",\r\n  \"image\": \"https://az620379.vo.msecnd.net/images/Markets/b2def0a6-92a1-4247-bc77-ec76ebea642f.jpg\",\r\n  \"url\": \"https://www.predictit.org/markets/detail/7053/Who-will-win-the-2024-Republican-presidential-nomination\",\r\n  \"contracts\": [\r\n    {\r\n      \"id\": 24787,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_36231f71-30ba-47c1-8f5d-c7481e014c11.jpg\",\r\n      \"name\": \"Donald Trump\",\r\n      \"shortName\": \"Trump\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.20,\r\n      \"bestBuyYesCost\": 0.21,\r\n      \"bestBuyNoCost\": 0.80,\r\n      \"bestSellYesCost\": 0.20,\r\n      \"bestSellNoCost\": 0.79,\r\n      \"lastClosePrice\": 0.20,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24782,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_20003de6-fa6b-4fb5-82be-aba2bc8c02f9.jpg\",\r\n      \"name\": \"Nikki Haley\",\r\n      \"shortName\": \"Haley\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.14,\r\n      \"bestBuyYesCost\": 0.15,\r\n      \"bestBuyNoCost\": 0.86,\r\n      \"bestSellYesCost\": 0.14,\r\n      \"bestSellNoCost\": 0.85,\r\n      \"lastClosePrice\": 0.14,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24814,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_1d8cb077-b0ce-456e-aadc-57fc4d88dcd7.png\",\r\n      \"name\": \"Ron DeSantis\",\r\n      \"shortName\": \"DeSantis\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.14,\r\n      \"bestBuyYesCost\": 0.15,\r\n      \"bestBuyNoCost\": 0.86,\r\n      \"bestSellYesCost\": 0.14,\r\n      \"bestSellNoCost\": 0.85,\r\n      \"lastClosePrice\": 0.15,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24789,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_3074a2e1-9027-4c1c-a50f-18db9c0a9bd4.jpg\",\r\n      \"name\": \"Mike Pence\",\r\n      \"shortName\": \"Pence\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.08,\r\n      \"bestBuyYesCost\": 0.09,\r\n      \"bestBuyNoCost\": 0.92,\r\n      \"bestSellYesCost\": 0.08,\r\n      \"bestSellNoCost\": 0.91,\r\n      \"lastClosePrice\": 0.08,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24815,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_4b2b5732-b84d-4ac6-b7da-9d3790c691e0.png\",\r\n      \"name\": \"Kristi Noem\",\r\n      \"shortName\": \"Noem\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.08,\r\n      \"bestBuyYesCost\": 0.09,\r\n      \"bestBuyNoCost\": 0.92,\r\n      \"bestSellYesCost\": 0.08,\r\n      \"bestSellNoCost\": 0.91,\r\n      \"lastClosePrice\": 0.08,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24785,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_405ebd48-c0d6-45ba-896d-d5cc56b464b9.jpg\",\r\n      \"name\": \"Ted Cruz\",\r\n      \"shortName\": \"Cruz\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.07,\r\n      \"bestBuyYesCost\": 0.08,\r\n      \"bestBuyNoCost\": 0.93,\r\n      \"bestSellYesCost\": 0.07,\r\n      \"bestSellNoCost\": 0.92,\r\n      \"lastClosePrice\": 0.07,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24788,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_9fbc3103-49b8-4f93-a1ce-79ff5fe31957.jpg\",\r\n      \"name\": \"Tom Cotton\",\r\n      \"shortName\": \"Cotton\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.05,\r\n      \"bestBuyYesCost\": 0.06,\r\n      \"bestBuyNoCost\": 0.95,\r\n      \"bestSellYesCost\": 0.05,\r\n      \"bestSellNoCost\": 0.94,\r\n      \"lastClosePrice\": 0.05,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24791,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_e3e5d1e1-db5f-4379-bf5b-af47e874e943.jpg\",\r\n      \"name\": \"Marco Rubio\",\r\n      \"shortName\": \"Rubio\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.05,\r\n      \"bestBuyYesCost\": 0.06,\r\n      \"bestBuyNoCost\": 0.95,\r\n      \"bestSellYesCost\": 0.05,\r\n      \"bestSellNoCost\": 0.94,\r\n      \"lastClosePrice\": 0.05,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24783,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_58b5f881-b587-4333-880b-024bc39922e8.jpg\",\r\n      \"name\": \"Mike Pompeo\",\r\n      \"shortName\": \"Pompeo\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24784,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_bbfdd2ed-95d5-43a3-bf5d-2403503705b0.jpg\",\r\n      \"name\": \"Mitt Romney\",\r\n      \"shortName\": \"Romney\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24786,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_851a6d14-5083-4680-91f6-419ec2d02a14.jpg\",\r\n      \"name\": \"Tucker Carlson\",\r\n      \"shortName\": \"Carlson\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24790,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_2badfab5-24b6-4e6d-8988-19c135639222.jpg\",\r\n      \"name\": \"Donald Trump Jr.\",\r\n      \"shortName\": \"Trump Jr.\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24792,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_683be142-e62f-4597-bc39-00fb2e2cb373.jpg\",\r\n      \"name\": \"Josh Hawley\",\r\n      \"shortName\": \"Hawley\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24793,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_958885c8-c04c-4ad3-b64e-fe33e9af83cc.jpg\",\r\n      \"name\": \"Tim Scott\",\r\n      \"shortName\": \"T. Scott\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.04,\r\n      \"bestBuyYesCost\": 0.05,\r\n      \"bestBuyNoCost\": 0.96,\r\n      \"bestSellYesCost\": 0.04,\r\n      \"bestSellNoCost\": 0.95,\r\n      \"lastClosePrice\": 0.04,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24794,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_e3ff1357-c8f6-4498-a5d6-12580eca75b7.jpg\",\r\n      \"name\": \"Rick Scott\",\r\n      \"shortName\": \"R. Scott\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.03,\r\n      \"bestBuyYesCost\": 0.04,\r\n      \"bestBuyNoCost\": 0.97,\r\n      \"bestSellYesCost\": 0.03,\r\n      \"bestSellNoCost\": 0.96,\r\n      \"lastClosePrice\": 0.03,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 24813,\r\n      \"dateEnd\": \"NA\",\r\n      \"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_8d271559-d751-448d-bacb-1a209e714bd4.png\",\r\n      \"name\": \"Larry Hogan\",\r\n      \"shortName\": \"Hogan\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.02,\r\n      \"bestBuyYesCost\": 0.03,\r\n      \"bestBuyNoCost\": 0.98,\r\n      \"bestSellYesCost\": 0.02,\r\n      \"bestSellNoCost\": 0.97,\r\n      \"lastClosePrice\": 0.02,\r\n      \"displayOrder\": 0\r\n    }\r\n  ],\r\n  \"timeStamp\": \"2021-02-20T12:27:22.3400423\",\r\n  \"status\": \"Open\"\r\n}";
    }
}

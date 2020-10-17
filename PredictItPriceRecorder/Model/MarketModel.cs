using Newtonsoft.Json;
using PredictItPriceRecorder.Model;
using System.Collections.Generic;

namespace PredictItPriceRecorder.Services.Abstractions
{
    public class MarketModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("contracts")]
        public List<ContractModel> Contracts { get; set; }
    }
}
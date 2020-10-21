using Newtonsoft.Json;
using System;

namespace PredictItPriceRecorder.Model
{
    public class ContractModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dateEnd")]
        public DateTime? DateEnd { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("status")]
        public ContractStatus Status { get; set; }

        [JsonProperty("lastTradePrice")]
        public decimal? LastTradePrice { get; set; }

        [JsonProperty("bestBuyYesCost")]
        public decimal? BestBuyYesCost { get; set; }

        [JsonProperty("bestBuyNoCost")]
        public decimal? BestBuyNoCost { get; set; }

        [JsonProperty("bestSellYesCost")]
        public decimal? BestSellYesCost { get; set; }

        [JsonProperty("bestSellNoCost")]
        public decimal? BestSellNoCost { get; set; }

        [JsonProperty("lastClosePrice")]
        public decimal? LastClosePrice { get; set; }

        [JsonProperty("displayOrder")]
        public byte DisplayOrder { get; set; }
    }
}

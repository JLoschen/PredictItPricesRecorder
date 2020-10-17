using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictItPriceRecorder.Domain.Model
{
    [Table("contract")]
    public class contract
    {
        [Key]
        public int contract_id { get; set; }


        public int market_id { get; set; }
        public DateTime? date_end { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public DateTime? create_date { get; set; }

        [ForeignKey(nameof(market_id))]
        public virtual market market { get; set; }
    }
}
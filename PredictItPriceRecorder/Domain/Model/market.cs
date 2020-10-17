using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictItPriceRecorder.Domain.Model
{
    [Table("market")] /*Schema = "PredictIt"*/
    public class market
    {
        [Key]
        public int market_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string short_name { get; set; }
        public DateTime? create_date { get; set; }
        public virtual ICollection<contract> contracts { get; set; }
    }
}
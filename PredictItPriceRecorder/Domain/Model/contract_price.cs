﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PredictItPriceRecorder.Domain.Model
{
    [Table("contract_price")]
    public class contract_price
    {
        public DateTime date_time { get; set; }
        public decimal last_trade_price { get; set; }
        public decimal best_buy_yes_cost { get; set; }
        public decimal best_buy_no_cost { get; set; }
        public decimal best_sell_yes_cost { get; set; }
        public decimal best_sell_no_cost { get; set; }
        public decimal last_close_price { get; set; }
    }
}
using PredictItPriceRecorder.Domain.Model;
using PredictItPriceRecorder.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictItPriceRecorder.Factory.Abstractions
{
    public interface IPredictItFactory
    {
        market GetMarketEntity(MarketModel model);
    }
}

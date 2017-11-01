using System.Collections.Generic;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public interface IDistributor
    {
        DailyRecord PriceBuilder(DayRate dayRate, IEnumerable<Price> prices, bool hasFreeConsultant);
    }
}

using System.Collections.Generic;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public interface IDistributor
    {
        SalesRecord PriceBuilder(DayRate dayRate, IEnumerable<Price> prices);
    }
}

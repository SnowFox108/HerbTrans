using System.Collections.Generic;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public interface IRemainingPicker
    {
        void Build(SalesRecord salesRecord, IEnumerable<Price> prices, ProductCategory category, int batchId);
    }
}

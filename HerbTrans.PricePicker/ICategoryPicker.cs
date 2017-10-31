using System.Collections.Generic;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public interface ICategoryPicker
    {
        ProductCategory Category { get; }
        void Build(SalesRecord salesRecord, IEnumerable<Price> prices, decimal subTotal, int batchId);
    }
}

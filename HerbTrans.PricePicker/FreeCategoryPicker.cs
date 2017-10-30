using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class FreeCategoryPicker : ICategoryPicker

    {
        public ProductCategory Category { get; }
        public decimal Build(SalesRecord salesRecord, IEnumerable<Price> prices, decimal subTotal, int batchId)
        {
            return 0;
        }
    }
}

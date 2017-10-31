using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class HerbCategoryPicker : ICategoryPicker
    {
        public ProductCategory Category => ProductCategory.Herb;
        private readonly ILogger _logger;

        public HerbCategoryPicker(ILogger logger)
        {
            _logger = logger;
        }

        public void Build(SalesRecord salesRecord, IEnumerable<Price> prices, decimal subTotal, int batchId)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var remaining = subTotal;
            var subPrices = prices.Where(p => p.Category == Category).ToArray();

            var totalChance = subPrices.Sum(p => p.ShowRate);

            while (remaining > 0)
            {
                var picker = rand.Next(0, totalChance);
                var pointer = 0;
                for (int i = 0; i < subPrices.Length; i++)
                {
                    pointer += subPrices[i].ShowRate;
                    if (picker < pointer)
                    {
                        var item = subPrices[i];
                        if (item.UnitPrice > remaining)
                            return;
                        salesRecord.AddPrice(new OutputPrice()
                        {
                            BatchId = batchId,
                            ProductId = item.Id,
                            Service = item.Service,
                            UnitPrice = item.UnitPrice,
                            Category = item.Category,
                            Quantity = 1,
                            Discount = 0
                        });
                        remaining -= item.UnitPrice;
                        _logger.Info(
                            $"picker: {picker} Picked item: {item.Service} price: {item.UnitPrice} remaining: {remaining}");
                        break;
                    }
                }
            }

            return;
        }
    }
}

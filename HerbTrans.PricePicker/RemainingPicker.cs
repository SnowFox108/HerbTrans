using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class RemainingPicker : IRemainingPicker
    {
        private readonly ILogger _logger;
        private readonly ICategoryPicker[] _categoryPickers;

        public RemainingPicker(ILogger logger, 
            ICategoryPicker[] categoryPickers)
        {
            _logger = logger;
            _categoryPickers = categoryPickers;
        }

        public void Build(SalesRecord salesRecord, IEnumerable<Price> prices, ProductCategory category, int batchId)
        {
            _logger.Info($"Processing Ramaining: {salesRecord.Remaining}");
            while (true)
            {
                var item = prices.OrderBy(p => p.UnitPrice).FirstOrDefault(p => p.Category == category && p.UnitPrice > salesRecord.Remaining);
                if (item == null)
                {
                    _logger.Info($"Couldn't find best price item, now try to pick again.");

                    var picker = _categoryPickers.Single(p => p.Category == category);

                    picker.Build(salesRecord, prices, salesRecord.Remaining, batchId);
                }
                else
                {
                    var pickedItem = new OutputPrice()
                    {
                        BatchId = batchId,
                        Service = item.Service,
                        UnitPrice = salesRecord.Remaining,
                        Category = item.Category,
                        Quantity = 1,
                        Discount = item.UnitPrice - salesRecord.Remaining
                    };

                    salesRecord.AddPrice(pickedItem);
                    _logger.Info(
                        $"Remaining picker picked item: {pickedItem.Service} price: {pickedItem.UnitPrice} " +
                        $"after discount {pickedItem.Discount} remaining: {salesRecord.Remaining}");
                    return;
                }
            }
        }
    }
}

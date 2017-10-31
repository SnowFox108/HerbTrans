using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class FreeCategoryPicker : ICategoryPicker

    {
        public ProductCategory Category => ProductCategory.FreeConsultant;
        private readonly ILogger _logger;
        public FreeCategoryPicker(ILogger logger)
        {
            _logger = logger;
        }

        public void Build(SalesRecord salesRecord, IEnumerable<Price> prices, decimal subTotal, int batchId)
        {
            var rand = new Random(DateTime.Now.Millisecond);

            var subPrices = prices.Where(p => p.Category == Category).ToArray();
            var total = salesRecord.Prices.Count();

            if (subPrices.Length == 0)
                return;

            foreach (var item in subPrices)
            {
                for (int i = 0; i < total; i++)
                {
                    var picker = rand.Next(0, 100);
                    if (picker < item.ShowRate)
                    {
                        salesRecord.AddPrice(new OutputPrice()
                        {
                            BatchId = batchId,
                            ProductId = item.Id,
                            Service = item.Service,
                            UnitPrice = 0,
                            Category = item.Category,
                            Quantity = 1,
                            Discount = item.UnitPrice
                        });
                        _logger.Info(
                            $"picker: {picker} Picked item: {item.Service} price: {item.UnitPrice}");
                    }
                }
            }
        }
    }
}

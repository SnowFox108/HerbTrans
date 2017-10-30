using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class Distributor: IDistributor
    {
        private readonly ILogger _logger;
        private readonly ICategoryPicker[] _categoryPickers;

        public Distributor(
            ILogger logger, 
            ICategoryPicker[] categoryPickers)
        {
            _logger = logger;
            _categoryPickers = categoryPickers;
        }

        public SalesRecord PriceBuilder(DayRate dayRate, IEnumerable<Price> prices)
        {
            var salesRecord = new SalesRecord(dayRate.DailyTotal, dayRate.Date);

            _logger.Info($"Processing DayRate: {dayRate.Id} on {dayRate.Date.ToShortDateString()}, Total: {dayRate.DailyTotal}");
            foreach (var categoryRate in dayRate.Rates)
            {
                var subTotal = 0m;
                var picker = _categoryPickers.Single(p => p.Category == categoryRate.Category);
                if (categoryRate.Rate > 0)
                {
                    subTotal = dayRate.DailyTotal * categoryRate.Rate / 100;
                    _logger.Info($"{categoryRate.Category.ToString()} with {subTotal} Portion: {categoryRate.Rate}");

                    picker.Build(salesRecord, prices, subTotal, dayRate.Id);
                }
            }

            return salesRecord;
        }
    }
}

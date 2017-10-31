using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.PricePicker
{
    public class Distributor: IDistributor
    {
        private readonly ILogger _logger;
        private readonly ICategoryPicker[] _categoryPickers;
        private readonly IRemainingPicker _remainingPicker;

        public Distributor(
            ILogger logger, 
            ICategoryPicker[] categoryPickers, 
            IRemainingPicker remainingPicker)
        {
            _logger = logger;
            _categoryPickers = categoryPickers;
            _remainingPicker = remainingPicker;
        }

        public SalesRecord PriceBuilder(DayRate dayRate, IEnumerable<Price> prices, bool hasFreeConsultant)
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

            if (salesRecord.Remaining > 0)
                _remainingPicker.Build(salesRecord, prices, dayRate.Rates.OrderByDescending(r => r.Rate).First().Category, dayRate.Id);

            if (hasFreeConsultant)
            {
                var picker = _categoryPickers.Single(p => p.Category == ProductCategory.FreeConsultant);
                picker.Build(salesRecord, prices, 0m, dayRate.Id);
            }

            return salesRecord;
        }
    }
}

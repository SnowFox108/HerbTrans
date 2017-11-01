using System;
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

        public DailyRecord PriceBuilder(DayRate dayRate, IEnumerable<Price> prices, bool hasFreeConsultant)
        {
            var dailyRecord = new DailyRecord();
            _logger.Info($"Processing DayRate: {dayRate.Id} on {dayRate.Date.ToShortDateString()}, " +
                         $"TotalCard: {dayRate.CardDailyTotal} TotalCash: {dayRate.CashDailyTotal}");

            _logger.Info($"Processing Card DayRate {dayRate.Date.ToShortDateString()} ");
            dailyRecord.Card = GetSalesRecord(dayRate.Id, dayRate.CardDailyTotal, dayRate.Date, dayRate.CardRates,
                prices, hasFreeConsultant);
            _logger.Info($"Processing Cash DayRate {dayRate.Date.ToShortDateString()} ");
            dailyRecord.Cash = GetSalesRecord(dayRate.Id, dayRate.CashDailyTotal, dayRate.Date, dayRate.CashRates,
                prices, hasFreeConsultant);


            return dailyRecord;
        }

        private SalesRecord GetSalesRecord(
            int batchId,
            decimal total, 
            DateTime today,
            IEnumerable<CategoryRate> rates,
            IEnumerable<Price> prices, bool hasFreeConsultant)
        {
            var salesRecord = new SalesRecord(total, today);

            foreach (var categoryRate in rates)
            {
                var subTotal = 0m;
                var picker = _categoryPickers.Single(p => p.Category == categoryRate.Category);
                if (categoryRate.Rate > 0)
                {
                    subTotal = total * categoryRate.Rate / 100;
                    _logger.Info($"{categoryRate.Category.ToString()} with {subTotal} Portion: {categoryRate.Rate}");

                    picker.Build(salesRecord, prices, subTotal, batchId);
                }
            }

            if (salesRecord.Remaining > 0)
                _remainingPicker.Build(salesRecord, prices, rates.OrderByDescending(r => r.Rate).First().Category, batchId);

            if (hasFreeConsultant)
            {
                var picker = _categoryPickers.Single(p => p.Category == ProductCategory.FreeConsultant);
                picker.Build(salesRecord, prices, 0m, batchId);
            }

            return salesRecord;
        }
    }
}

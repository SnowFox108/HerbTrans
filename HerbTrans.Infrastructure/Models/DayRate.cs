using System;
using System.Collections.Generic;

namespace HerbTrans.Infrastructure.Models
{
    public class DayRate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal CardDailyTotal { get; set; }
        public IEnumerable<CategoryRate> CardRates { get; set; }
        public decimal CashDailyTotal { get; set; }
        public IEnumerable<CategoryRate> CashRates { get; set; }

    }
}

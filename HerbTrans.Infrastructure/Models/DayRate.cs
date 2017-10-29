using System;
using System.Collections.Generic;

namespace HerbTrans.Infrastructure.Models
{
    public class DayRate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal DailyTotal { get; set; }
        public IEnumerable<CategoryRate> Rates { get; set; }
    }
}

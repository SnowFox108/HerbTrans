using System;
using System.Collections.Generic;

namespace HerbTrans.Infrastructure.Models
{
    public class SalesRecord
    {
        public decimal Total { get; }
        public decimal Remaining { get; private set; }
        public int Count { get; private set; }
        public DateTime Date { get; }
        public List<OutputPrice> Prices { get; }

        public SalesRecord(decimal total, DateTime date)
        {
            Total = total;
            Remaining = total;
            Date = date;
            Prices = new List<OutputPrice>();
        }

        public void AddPrice(OutputPrice outputPrice)
        {
            Count++;
            outputPrice.Id = Count;
            Remaining -= outputPrice.UnitPrice;
            Prices.Add(outputPrice);
        }

    }
}

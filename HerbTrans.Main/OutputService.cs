using System;
using System.Collections.Generic;
using System.Text;
using HerbTrans.Infrastructure.Files;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Main
{
    public class OutputService : IOutputService
    {
        private readonly IFileHelper _fileHelper;

        public OutputService(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public void WriteToFile(IEnumerable<DailyRecord> dailyRecords, string file)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var dailyRecord in dailyRecords)
            {
                var totalChance = dailyRecord.Card.Prices.Count + dailyRecord.Cash.Prices.Count;
                Queue<OutputPrice> card = new Queue<OutputPrice>(dailyRecord.Card.Prices);
                Queue<OutputPrice> cash = new Queue<OutputPrice>(dailyRecord.Cash.Prices);

                while ((card.Count + cash.Count) > 0)
                {
                    var picker = rand.Next(0, totalChance);
                    if (picker < dailyRecord.Card.Prices.Count && card.Count > 0)
                    {
                        stringBuilder.AppendLine(GetOutputString(dailyRecord.Card.Date, card.Dequeue(), "Card"));
                    }
                    else
                    {
                        if (cash.Count > 0)
                            stringBuilder.AppendLine(GetOutputString(dailyRecord.Cash.Date, cash.Dequeue(), "Cash"));
                    }
                }
            }
            _fileHelper.WriteTextToFile(file, stringBuilder.ToString());
        }

        private string GetOutputString(DateTime date, OutputPrice outputPrice, string payment)
        {
            return $"{date.ToShortDateString()},{payment},{outputPrice.Service},{outputPrice.UnitPrice}";
        }
    }
}

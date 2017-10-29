using System;
using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class DayRateParser: IParser<DayRate>
    {
        public DayRate Parser(string data)
        {
            var items = data.Split(',');
            var dayRate = new DayRate()
            {
                Id = int.Parse(items[0]),
                DailyTotal = decimal.Parse(items[1]),
                Date = DateTime.Parse(items[2]),
                Rates = new[]
                {
                    new CategoryRate()
                    {
                        Category = ProductCategory.Medicine,
                        Rate = int.Parse(items[3])
                    },
                    new CategoryRate()
                    {
                        Category = ProductCategory.Beauty,
                        Rate = int.Parse(items[4])
                    },
                    new CategoryRate()
                    {
                        Category = ProductCategory.Herb,
                        Rate = int.Parse(items[5])
                    },
                }
            };
            return dayRate;
        }
    }
}

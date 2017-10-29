using HerbTrans.Infrastructure.Enums;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class PriceParser : IParser<Price>
    {
        public Price Parser(string data)
        {
            var items = data.Split(',');
            var price = new Price()
            {
                Service = items[0],
                UnitPrice = decimal.Parse(items[1]),
                Category = (ProductCategory)int.Parse(items[2]),
                ShowRate = int.Parse(items[3])
            };
            return price;
        }
    }
}

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
                Id = int.Parse(items[0]),
                Service = items[1],
                UnitPrice = decimal.Parse(items[2]),
                Category = (ProductCategory)int.Parse(items[3]),
                ShowRate = int.Parse(items[4])
            };
            return price;
        }
    }
}

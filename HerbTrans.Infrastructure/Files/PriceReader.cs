using System.Collections.Generic;
using System.IO;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class PriceReader : ICsvReader<Price>
    {
        private readonly IParser<Price> _parser;

        public PriceReader(IParser<Price> parser)
        {
            _parser = parser;
        }

        public IEnumerable<Price> FileReader(string file)
        {
            var prices = new List<Price>();

            using (var reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    prices.Add(_parser.Parser(line));
                }
            }

            return prices;
        }
    }
}

using System.Collections.Generic;
using System.IO;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class DayRateReader : ICsvReader<DayRate>
    {
        private readonly IParser<DayRate> _parser;

        public DayRateReader(IParser<DayRate> parser)
        {
            _parser = parser;
        }

        public IEnumerable<DayRate> FileReader(string file)
        {
            var dayRates = new List<DayRate>();

            using (var reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    dayRates.Add(_parser.Parser(line));
                }
            }

            return dayRates;
        }
    }
}

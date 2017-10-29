using System.Collections.Generic;
using System.IO;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class HerbProcessReader : ICsvReader<FileProcess>
    {
        private readonly IParser<FileProcess> _parser;

        public HerbProcessReader(IParser<FileProcess> parser)
        {
            _parser = parser;
        }

        public IEnumerable<FileProcess> FileReader(string file)
        {
            var processes = new List<FileProcess>();

            using (var reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    processes.Add(_parser.Parser(line));
                }
            }

            return processes;

        }
    }
}

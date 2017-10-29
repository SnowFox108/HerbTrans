using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerbTrans.Infrastructure.Files;

namespace HerbTrans.Client
{
    public class FileReadTest
    {
        public FileReadTest()
        {
            var priceReader = new PriceReader(new PriceParser());
            var result = priceReader.FileReader(@"Docs\8Price2016.csv");
        }
    }
}

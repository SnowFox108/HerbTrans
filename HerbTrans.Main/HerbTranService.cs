using System.Collections.Generic;
using Castle.Core.Logging;
using HerbTrans.Infrastructure.Files;
using HerbTrans.Infrastructure.Models;
using HerbTrans.PricePicker;

namespace HerbTrans.Main
{
    public class HerbTranService: IHerbTranService
    {
        private readonly string _path;
        private readonly ILogger _logger;
        private readonly ICsvReader<FileProcess> _fileReader;
        private readonly ICsvReader<Price> _priceReader;
        private readonly ICsvReader<DayRate> _dayRateReader;
        private readonly IDistributor _distributor;

        public HerbTranService(
            string path,
            ILogger logger, 
            ICsvReader<FileProcess> fileReader, 
            ICsvReader<Price> priceReader, 
            ICsvReader<DayRate> dayRateReader, 
            IDistributor distributor)
        {
            _path = path;
            _logger = logger;
            _fileReader = fileReader;
            _priceReader = priceReader;
            _dayRateReader = dayRateReader;
            _distributor = distributor;
        }


        public void Execute()
        {
            var processFiles = _fileReader.FileReader(_path);
            foreach (var file in processFiles)
            {
                _logger.InfoFormat($"Processing DayRate: {file.DayRateFile} with Price: {file.PriceFile}.");
                var prices = _priceReader.FileReader(file.PriceFile);
                var dayRates = _dayRateReader.FileReader(file.DayRateFile);

                var salesRecords = new List<SalesRecord>();
                foreach (var dayRate in dayRates)
                {
                    var salesRecord = _distributor.PriceBuilder(dayRate, prices);
                    salesRecords.Add(salesRecord);
                }
            }
        }
    }
}

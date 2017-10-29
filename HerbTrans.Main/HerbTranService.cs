using Castle.Core.Logging;
using HerbTrans.Infrastructure.Files;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Main
{
    public class HerbTranService: IHerbTranService
    {
        private readonly string _path;
        private readonly ILogger _logger;
        private readonly ICsvReader<FileProcess> _fileReader;
        private readonly ICsvReader<Price> _priceReader;
        private readonly ICsvReader<DayRate> _dayRateReader;

        public HerbTranService(
            string path,
            ILogger logger, 
            ICsvReader<FileProcess> fileReader, 
            ICsvReader<Price> priceReader, 
            ICsvReader<DayRate> dayRateReader)
        {
            _path = path;
            _logger = logger;
            _fileReader = fileReader;
            _priceReader = priceReader;
            _dayRateReader = dayRateReader;
        }


        public void Execute()
        {
            var processFiles = _fileReader.FileReader(_path);
            foreach (var file in processFiles)
            {
                _logger.InfoFormat($"Processing DayRate: {file.DayRateFile} with Price: {file.PriceFile}.");
                var prices = _priceReader.FileReader(file.PriceFile);
                var dayRates = _dayRateReader.FileReader(file.DayRateFile);
            }
        }
    }
}

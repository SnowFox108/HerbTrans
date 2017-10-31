using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Infrastructure.Files
{
    public class HerbProcessParser : IParser<FileProcess>
    {
        public FileProcess Parser(string data)
        {
            var items = data.Split(',');
            var process = new FileProcess()
            {
                PriceFile = items[0],
                DayRateFile = items[1],
                OutputFile = items[2],
                HasFreeConsultant = bool.Parse(items[3])
            };
            return process;
        }
    }
}

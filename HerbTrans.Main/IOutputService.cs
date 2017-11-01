using System.Collections.Generic;
using HerbTrans.Infrastructure.Models;

namespace HerbTrans.Main
{
    public interface IOutputService
    {
        void WriteToFile(IEnumerable<DailyRecord> dailyRecords, string file);
    }
}

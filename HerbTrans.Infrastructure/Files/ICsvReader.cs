using System.Collections.Generic;

namespace HerbTrans.Infrastructure.Files
{
    public interface ICsvReader<out T>
    {
        IEnumerable<T> FileReader(string file);
    }
}

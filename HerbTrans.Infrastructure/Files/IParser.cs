namespace HerbTrans.Infrastructure.Files
{
    public interface IParser<out T>
    {
        T Parser(string data);
    }
}

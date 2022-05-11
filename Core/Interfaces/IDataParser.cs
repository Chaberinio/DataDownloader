namespace Core.Interfaces
{
    public interface IDataParser
    {
        string AccountsToString(string json);

        string TransactionsToString(string json);
    }
}
namespace Core.Interfaces
{
    public interface IDataParser
    {
        string AccountsToString(string json);

        string AccountIdToString(string json);

        string TransactionsToString(string json);

        string GetIbanToString(string json);
    }
}
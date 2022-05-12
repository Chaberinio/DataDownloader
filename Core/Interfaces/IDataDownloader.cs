using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataDownloader
    {
        Task<string> GetAccounts();

        Task<string> GetTransactions();

        Task<string> GetIban();

        void SetToken(string value);
    }
}
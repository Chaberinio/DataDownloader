using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenRefresher
    {
        Task<string> GetToken();
    }
}
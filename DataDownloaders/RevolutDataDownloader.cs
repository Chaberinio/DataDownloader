using Core.Exceptions;
using Core.Interfaces;
using DataParsers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataDownloaders
{
    public class RevolutDataDownloader : IDataDownloader
    {
        private string baseUrl = "https://sandbox-b2b.revolut.com/api/1.0/";
        private string bearerToken = "";

        private RevolutDataParser parser = new RevolutDataParser();

        public void SetToken(string value)
        {
            bearerToken = value;
        }

        public async Task<string> GetAccounts()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var url = baseUrl + "accounts";

            var response = await client.GetAsync(url);

            var responseCode = response.StatusCode.ToString();
            if (responseCode == "Unauthorized")
                throw new AuthenticationException();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> GetTransactions()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var url = baseUrl + "transactions";

            var response = await client.GetAsync(url);

            var responseCode = response.StatusCode.ToString();
            if (responseCode == "Unauthorized")
                throw new AuthenticationException();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> GetIban()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var id = parser.AccountIdToString(await GetAccounts());
            var url = baseUrl + "accounts/" + id + "/bank-details";

            var response = await client.GetAsync(url);

            var responseCode = response.StatusCode.ToString();
            if (responseCode == "Unauthorized")
                throw new AuthenticationException();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
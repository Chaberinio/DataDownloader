using Core.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace DataDownloaders
{
    public class RevolutDataDownloader : IDataDownloader
    {
        string baseUrl = "https://sandbox-b2b.revolut.com/api/1.0/";
        string bearerToken = "";



        public async Task<string> GetAccounts()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var url = baseUrl + "transactions";

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task GenerateTokenAsync(string grantType, string refreshToken, string ClientID, string ClientAssertionType, string ClientAssertion)
        {
            //using var client = new Windows.Web.Http.HttpClient();
            //string url = baseUrl + "auth/token";
            //var uriRequest = new Uri(url);


            ////HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), url);

            ////requestMessage.Content = JsonContent.Create(new
            ////{
            ////    grant_type = grantType,
            ////    refresh_token = refreshToken,
            ////    client_id = ClientID,
            ////    client_assertion_type = ClientAssertionType,
            ////    client_assertion = ClientAssertion
            ////});

            //var content = new Windows.Web.Http.HttpFormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("grant_type", grantType),
            //    new KeyValuePair<string, string>("refresh_token", refreshToken),
            //    new KeyValuePair<string, string>("client_id", ClientID),
            //    new KeyValuePair<string, string>("client_assertion_type", ClientAssertionType),
            //    new KeyValuePair<string, string>("client_assertion", ClientAssertion)
            //});


            //Windows.Web.Http.HttpResponseMessage response = await client.PostAsync(uriRequest, content);

            //var responseString = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(responseString);

        }

        public async Task<string> GetTransactions()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var url = baseUrl + "transactions";

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(result);

            return result;
        }

        public void SetParameter(string value)
        {
            bearerToken = value;
        }


    }





}

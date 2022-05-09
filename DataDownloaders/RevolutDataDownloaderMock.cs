using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloaders
{
    public class RevolutDataDownloaderMock : IDataDownloader
    {
        //string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\accounts.json");


        string parameter = string.Empty;
        string token = "oa_sand_p1R6DbI5DB2A0tixpahINpj1zAWOAWHL88PbvRdQSWU";

        string result = string.Empty;

        public async Task GenerateTokenAsync()
        {
            string ClientID = string.Empty;
            string ClientAssertionType = string.Empty;
            string ClientAssertion = string.Empty;

            string token = string.Empty;
            string tokenType = string.Empty;

            var url = "https://sandbox-b2b.revolut.com/api/1.0/auth/token";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fd6307d837ff85177c69fa0e74a51304512f9627417658830487cf0470db6890");

            var response = await client.PostAsync(url, );
            var result = await response.Content.ReadAsStringAsync();




        }

        public string GetAccounts()
        {
            string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\accounts.json");

            if (parameter!=token)            
              throw new AuthenticationException();
            
            for(int i = 0; i < acc.Length; i ++)
                result += acc[i] + "\n";

            return result;
        }
        
        public string GetTransactions()
        {
            string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\trans.json");

            if (parameter != token)
                throw new AuthenticationException();
            
            for (int i = 0; i < acc.Length; i++)
                result += acc[i] + "\n";

            return result;
        }

        public void SetParameter(string value)
        {
            parameter = value;
        }

        void IDataDownloader.GenerateTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}

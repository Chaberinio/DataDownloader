using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataDownloaders
{
    public class RevolutDataDownloader : IDataDownloader
    {
        public string GetAccounts()
        {
            //using var client = new HttpClient();
            //var url = "https://sandbox-b2b.revolut.com/api/1.0/accounts";

            //var token = "oa_sand_p1R6DbI5DB2A0tixpahINpj1zAWOAWHL88PbvRdQSWU";
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var result = await client.GetStringAsync(url);

            return "idk";



            //var json = JsonConvert.SerializeObject();
            //var data = new StringContent(json, Encoding.UTF8, "application/json");
        }

        public string GetTransactions()
        {
            throw new NotImplementedException();
        }

        public void SetParameter(string value)
        {
            throw new NotImplementedException();
        }
    }
}

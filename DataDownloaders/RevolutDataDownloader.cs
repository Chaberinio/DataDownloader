using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DataDownloaders
{
    public class RevolutDataDownloader : IDataDownloader
    {
        //string url = "https://sandbox-b2b.revolut.com/api/1.0/";




        //public string GetAccounts()
        //{
        //    using var client = new HttpClient();
        //    var getUrl = url + "accounts";




        //    var result = await client.GetStringAsync(url);

        //    return "idk";



        //var json = JsonConvert.SerializeObject();
        //var data = new StringContent(json, Encoding.UTF8, "application/json");
        public void GenerateTokenAsync()
        {
            throw new NotImplementedException();

        }

        public string GetAccounts()
        {
            throw new NotImplementedException();
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
}

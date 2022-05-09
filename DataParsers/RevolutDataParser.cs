using Core.Interfaces;
using Core.Model;
using DataDownloaders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataParsers
{
    public class RevolutDataParser : IDataParser
    {
        IDataDownloader dataDownloader = new RevolutDataDownloader();



        //public class Account
        //{
        //    public string id { get; set; }
        //    public string name { get; set; }
        //    public float balance { get; set; }
        //    public string currency { get; set; }
        //    public string state { get; set; }
        //    public bool _public { get; set; }
        //    public DateTime created_at { get; set; }
        //    public DateTime updated_at { get; set; }
        //}

        public async Task<string> AccountsToString()
        {
            var json = await dataDownloader.GetAccounts();

            RootobjectAccJson accountsList = JsonConvert.DeserializeObject<RootobjectAccJson>(json);

            string accountsString = "";

            foreach(var acc in accountsList.data)
            {
                accountsString +=
                    "{\n ID: " + acc.id + " | Name: " + acc.name +
                    "\n Balance: " + acc.balance + " " + acc.currency + "\n}";

            }

            return accountsString;
        }

        public async Task<string> TransactionsToString()
        {
            throw new System.NotImplementedException();
        }
    }
}

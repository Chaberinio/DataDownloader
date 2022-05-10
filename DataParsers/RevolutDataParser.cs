using Core.Interfaces;
using Core.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataParsers
{
    public class RevolutDataParser : IDataParser
    {



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

        public string AccountsToString(string json)
        {
            List<AccJson> accountsList = null;
            accountsList = JsonConvert.DeserializeObject<List<AccJson>>(json);

            string accountsString = "";

            foreach (var acc in accountsList)
            {
                accountsString +=
                    "{\n\tID: " + acc.id + " | Name: " + acc.name +
                    "\n\tBalance: " + acc.balance + " " + acc.currency +
                    "\n\tState: " + acc.state +
                    "\n\tPublic: " + acc._public +
                    "\n\tCreated at " + acc.created_at + " | Updated at: " + acc.updated_at +
                    "\n}\n";

            }

            return accountsString;
        }

        public  string TransactionsToString(string json)
        {
            List<TransJson> transactionList = null;
            transactionList = JsonConvert.DeserializeObject<List<TransJson>>(json);

            string transactionString = "";

            foreach (var trans in transactionList)
            {
                transactionString +=
                    "{\n\tID: " + trans?.id +
                    "\n\tType: " + trans?.type +
                    "\n\tState: " + trans?.state +
                    "\n\tRequest ID: " + trans?.request_id +
                    "\n\tCreated at: " + trans?.created_at +
                    "\n\tUpdated at: " + trans?.updated_at +
                    "\n\tCompleted at: " + trans?.created_at;

                if(trans.merchant != null)
                    transactionString +=
                        "\n\tMerchant: {" +
                        "\n\t\tName: " + trans?.merchant?.name +
                        "\n\t\tCity: " + trans?.merchant?.city +
                        "\n\t\tCategory code: " + trans?.merchant?.category_code +
                        "\n\t\tCountry: " + trans?.merchant?.country +
                        "\n\t}\n"; 

                transactionString +=
                     "\n\tLegs: {";

                    foreach(var leg in trans?.legs)
                        transactionString +=
                        "\n\t\t[" +
                        "\n\t\tLeg ID: " + leg?.leg_id +
                        "\n\t\tAccount ID: " + leg?.account_id +
                        "\n\t\tAmound: " + leg?.amount +
                        "\n\t\tCurrency: " + leg?.currency +
                        "\n\t\tDescryption: " + leg?.description +
                        "\n\t\tBalanc: " + leg?.balance + 
                        "\n\t\t],";

                transactionString +=
                    "\n\t}\n";

                if (trans.card != null)
                    transactionString +=
                        "\n\tCard: {" + 
                        "\n\t\tCard number: " + trans?.card?.card_number + 
                        "\n\t\tName: " + trans?.card?.first_name + " " + trans?.card?.last_name +
                        "\n\t\tPhone: " + trans?.card?.phone +
                        "\n\t}\n" +
                        "\n}\n";

            }

            return transactionString;
        }

        //public class Leg
        //{
        //    public string leg_id { get; set; }
        //    public string account_id { get; set; }
        //    public float amount { get; set; }
        //    public string currency { get; set; }
        //    public string description { get; set; }
        //    public float balance { get; set; }
        //    public Counterparty counterparty { get; set; }
        //}

        /*    {
              "id": "624301a0-7ce0-ab4c-928a-dce88e39c5d2",
              "type": "card_payment",
              "state": "completed",
              "request_id": "d893d3e6-d351-49fe-947c-7e096f55d220",
              "created_at": "2022-03-29T12:54:56.757955Z",
              "updated_at": "2022-03-29T12:54:56.757955Z",
              "completed_at": "2022-03-29T12:54:56.758268Z",
              "merchant": {
                  "name": "Amazon",
                  "city": "London",
                  "category_code": "5969",
                  "country": "GBR"
               },
              "legs": [
                  {
                      "leg_id": "098093e4-8d6f-4cea-8c5e-d0a2b8172d50",
                      "account_id": "3337a6f8-67ed-451d-90bf-f5eb927508d8",
                      "amount": -11,
                      "currency": "GBP",
                      "description": "Amazon",
                      "balance": 28556.01
                  }
              ],
              "card": {
                   "card_number": "805719******2246"
              }
            },*/

    }
}

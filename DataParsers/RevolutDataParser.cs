using Core.Interfaces;
using Core.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataParsers
{
    public class RevolutDataParser : IDataParser
    {
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
                    "\n},\n";

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
    }
}

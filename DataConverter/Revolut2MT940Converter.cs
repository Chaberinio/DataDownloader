using Core.Interfaces;
using Core.Model;
using MT940;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConverter
{
    public class Revolut2MT940Converter : IRevolut2MT940Converter
    {
        public StatementMT940 Convert(AccJson account, List<TransJson> trans, string iban)
        {
            StatementMT940 output = new StatementMT940();

            output.rawAccId = iban;

            output.openBal = CreateBalance(trans[0].created_at, trans[0].legs[0].currency, (decimal)trans[0].legs[0].balance);

            output.closeBal = CreateBalance(trans[trans.Count - 1].completed_at, trans[trans.Count - 1].legs[0].currency, (decimal)trans[trans.Count - 1].legs[0].balance);

            return output;
        }

        private Balance CreateBalance(DateTime bookDate, string currency, decimal amount)
        {
            Balance balance = new Balance();

            balance.bookDate = bookDate;
            balance.currency = currency;
            balance.amount = amount;

            return balance;
        }
    }
}

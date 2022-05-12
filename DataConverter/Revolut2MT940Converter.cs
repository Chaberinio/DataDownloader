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
        public StatementMT940 Convert(AccJson account, List<TransJson> TransList, string iban)
        {
            StatementMT940 output = new StatementMT940();

            output.transRefNum = CreateTransRefNum(TransList[TransList.Count - 1].completed_at);
            output.rawAccId = iban;


            foreach (var trans in TransList)
                output.transactions.Add(CreateTransaction(trans));

            output.openBal = CreateBalance(TransList[0].created_at, TransList[0].legs[0].currency, (decimal)TransList[0].legs[0].balance);
            output.closeBal = CreateBalance(TransList[TransList.Count - 1].completed_at, TransList[TransList.Count - 1].legs[0].currency, (decimal)TransList[TransList.Count - 1].legs[0].balance);

            

            return output;
        }

        private string CreateTransRefNum(DateTime date)
        {
            string dayOfYear = DateTime.Now.DayOfYear.ToString().PadLeft(3,'0');
            string hh = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string mm = DateTime.Now.Minute.ToString().PadLeft(2, '0');

            return dayOfYear + hh + mm;
        }

        private Balance CreateBalance(DateTime bookDate, string currency, decimal amount)
        {
            Balance balance = new Balance();

            balance.bookDate = bookDate;
            balance.currency = currency;
            balance.amount = amount;

            return balance;
        }

        private Transaction CreateTransaction(TransJson trans)
        {
            Transaction transaction = new Transaction();
            transaction.transLine = CreateTransLine(trans); // :61


            return transaction;
        }

        private TransLine CreateTransLine(TransJson trans) // :61
        {
            TransLine transLine = new TransLine();

            //public DateTime valueDate;
            //public DateTime? bookingDate;
            //public string debitCreditMark;
            //public decimal amount;  // zawsze liczba dodatnia
            //public string bookingCode;
            //public string cRef;       ???????????????????????????????????
            //public string bRef;       ???????????????????????????????????
            //public string furtherInfo;
            //public string rawData;

            transLine.valueDate = trans.created_at;
            transLine.bookingDate = trans.completed_at;
            transLine.debitCreditMark = trans.legs[0].amount >= 0 ? "C" : "D";
            transLine.amount = (decimal)Math.Abs(trans.legs[0].amount);


            string valueDate = transLine.valueDate.ToString("yy").PadLeft(2,'0') + transLine.valueDate.Month.ToString().PadLeft(2,'0') + transLine.valueDate.Day.ToString().PadLeft(2,'0');
            string bookingDate = transLine.bookingDate.Value.Month.ToString().PadLeft(2,'0') + transLine.bookingDate.Value.Day.ToString().PadLeft(2,'0');
            string currencyName = trans.legs[0].currency[2].ToString();

            transLine.rawData = valueDate + bookingDate + transLine.debitCreditMark + currencyName + String.Format("{0:0.00}",transLine.amount).Replace('.',',') + "NTRF//";


            return transLine;
        } 
    }
}

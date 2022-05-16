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
        //creditcard 114
        //przelew B22
        //wpłata 0

        IDictionary<string, string> dictPaymentTypes = new Dictionary<string, string>();

        public StatementMT940 Convert(AccJson account, List<TransJson> TransList, string iban, List<string> counterpartyIbanList)
        {
            StatementMT940 output = new StatementMT940();

            dictPaymentTypes["card_payment"] = "114";

            output.transRefNum = CreateTransRefNum(TransList[TransList.Count - 1].completed_at);
            output.rawAccId = iban;


            var TransAndCounterpartyIbans = TransList.Zip(counterpartyIbanList, (t, c) => new { trans = t, counterpartyIban = c });

            foreach (var tc in TransAndCounterpartyIbans)
                output.transactions.Add(CreateTransaction(tc.trans, tc.counterpartyIban));

            output.openBal = CreateBalance(TransList[0].created_at, TransList[0].legs[0].currency, (decimal)TransList[0].legs[0].balance);
            output.closeBal = CreateBalance(TransList[TransList.Count - 1].completed_at, TransList[TransList.Count - 1].legs[0].currency, (decimal)TransList[TransList.Count - 1].legs[0].balance);
            output.curValDatedBal = CreateBalance(TransList[TransList.Count - 1].completed_at, TransList[TransList.Count - 1].legs[0].currency, (decimal)TransList[TransList.Count - 1].legs[0].balance);


            string debitCreditMark = DebitOrCredit(output.openBal, output.closeBal);

            output.openBal.rawData = ":60F:" + debitCreditMark + output.openBal.rawData;
            output.closeBal.rawData = ":62F:" + debitCreditMark + output.closeBal.rawData;
            output.curValDatedBal.rawData = ":64:" + debitCreditMark + output.curValDatedBal.rawData;

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

            balance.rawData = balance.bookDate.ToString("yy").PadLeft(2, '0') + balance.bookDate.ToString("MM").PadLeft(2, '0') + balance.bookDate.ToString("dd").PadLeft(2, '0') + currency + String.Format("{0:0.00}", amount).Replace('.', ',');

            
            return balance;
        }

        private Details CreateDetails(TransJson trans, string counterpartyIban)
        {
            //":86:114<00CARDPAYMENT\n<20Amazon\n<21\n<22\n<23\n<24\n<25\n<26\n<27Amazon\n<27London GBR\n<28\n<29\n<30\n<31\n<32\n<33\n<38" + AmazonIBAN + "\n<63"
            Details details = new Details();

            details.counterpartyName = trans.merchant.name;
            details.counterpartyAcc = counterpartyIban;

            string underField20 = trans.legs[0].description.Length > 0 ? trans.legs[0].description.Length < 35 ? trans.legs[0].description : trans.legs[0].description.Substring(0, 35) : "";
            string underField21 = trans.legs[0].description.Length > 35 ? trans.legs[0].description.Length < 75 ? trans.legs[0].description.Substring(35) : trans.legs[0].description.Substring(35, 70) : "";
            string underField22 = trans.legs[0].description.Length > 70 ? trans.legs[0].description.Length < 110 ? trans.legs[0].description.Substring(70) : trans.legs[0].description.Substring(70, 110) : "";
            string underField23 = trans.legs[0].description.Length > 110 ? trans.legs[0].description.Length < 145 ? trans.legs[0].description.Substring(110) : trans.legs[0].description.Substring(110, 145) : "";
            string underField24 = trans.legs[0].description.Length > 145 ? trans.legs[0].description.Length < 180 ? trans.legs[0].description.Substring(180) : trans.legs[0].description.Substring(145, 180) : "";
            string underField25 = trans.legs[0].description.Length > 180 ? trans.legs[0].description.Length < 215 ? trans.legs[0].description.Substring(215) : trans.legs[0].description.Substring(180, 215) : "";
            string underField26 = trans.legs[0].description.Length > 215 ? trans.legs[0].description.Length < 285 ? trans.legs[0].description.Substring(215) : trans.legs[0].description.Substring(215, 285) : "";

            string temp = trans.merchant.name + " " + trans.merchant.city + " " + trans.merchant.country;

            string underField27 = temp.Length > 0 ? temp.Length < 35 ? temp : temp.Substring(0, 35) : "";
            string underField28 = temp.Length > 35 ? temp.Length < 70 ? temp : temp.Substring(35, 70) : "";
            string underField29 = temp.Length > 70 ? temp.Length < 140 ? temp : temp.Substring(70, 140) : "";


            details.rawData = ":86:" + dictPaymentTypes[trans.type] +
                "<00" + trans.type +
                "\n<20" + underField20 +
                "\n<21" + underField21 +
                "\n<22" + underField22 +
                "\n<23" + underField23 +
                "\n<24" + underField24 +
                "\n<25" + underField25 +
                "\n<26" + underField26 +
                "\n<27" + underField27 +
                "\n<28" + underField28 +
                "\n<29" + underField29 +
                "\n<30" + "" +
                "\n<31" + "" +
                "\n<32" + "" +
                "\n<33" + "" +
                "\n<38" + details.counterpartyAcc +
                "\n<63" + "";


            return details;
        }

        private Transaction CreateTransaction(TransJson trans, string counterpartyIban)
        {
            Transaction transaction = new Transaction();
            transaction.transLine = CreateTransLine(trans); // :61
            transaction.details = CreateDetails(trans, counterpartyIban); // :86
            


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


            string valueDate = transLine.valueDate.ToString("yy").PadLeft(2,'0') + transLine.valueDate.ToString("MM").PadLeft(2,'0') + transLine.valueDate.ToString("dd").PadLeft(2,'0');
            string bookingDate = transLine.bookingDate.Value.ToString("MM").PadLeft(2,'0') + transLine.bookingDate.Value.ToString("dd").PadLeft(2,'0');
            string currencyName = trans.legs[0].currency.ToString();

            transLine.rawData = ":61:" + valueDate + bookingDate + transLine.debitCreditMark + currencyName[2] + String.Format("{0:0.00}",transLine.amount).Replace('.',',') + "NTRF//";


            return transLine;
        }

        private string DebitOrCredit(Balance openBalance, Balance closeBalance)
        {
            string result = closeBalance.amount >= openBalance.amount ? "C" : "D";
            return result;
        }
        
    }
}

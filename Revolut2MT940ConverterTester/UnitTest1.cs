using Core.Model;
using DataConverter;
using MT940;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Revolut2MT940ConverterTester
{
    public class UnitTest1
    {
        //:20: data
        //:25: iban
        //:NS:22 nazwa w³aœciciela
        //:NS:23 nazwa rachunku
        //:60F: debet/kredty, data, waluta, kwota
        //:86: kod operacji, <00typ operacji, <10numer sekwencyjny, <20<26 tytu³ operacji,<27<29 adres kontrachenta, <38rachunek kontrachenta,
        //:62F: debet/kredyt, data salda koñcowego, waluta salda, kwota
        //:64: debet/kredyt, data salda dostêpnego, waluta, kwota
        private AccJson inputAcc;

        private List<TransJson> inputTrans;
        private string inputIban;
        private StatementMT940? output = new StatementMT940();
        private Revolut2MT940Converter converter = new Revolut2MT940Converter();
        private List<string> inputIbanCounterparty = new List<string>();

        [SetUp]
        public void SetUp()
        {
            inputAcc = new AccJson()
            {
                id = "3337a6f8-67ed-451d-90bf-f5eb927508d8",
                name = "European suppliers",
                balance = 28556.01F,
                currency = "GBP"
            };

            inputTrans = new List<TransJson>()
            {
                new TransJson()
                {
                    id = "624301a0-7ce0-ab4c-928a-dce88e39c5d2",
                    type = "card_payment",
                    state = "completed",
                    request_id = "d893d3e6-d351-49fe-947c-7e096f55d220",

                    //assigns year, month, day, hour, min, seconds
                    created_at = new DateTime(2022, 03, 29, 12, 54, 57),
                    updated_at = new DateTime(2022, 03, 29, 12, 54, 57),
                    completed_at = new DateTime(2022, 03, 29, 12, 54, 57),
                    merchant = new Merchant()
                    {
                        name = "Amazon",
                        city = "London",
                        category_code = "5969",
                        country = "GBR"
                    },
                    legs =  new List<Leg>()
                    {
                        new Leg()
                        {
                            leg_id = "098093e4-8d6f-4cea-8c5e-d0a2b8172d50",
                            account_id = "3337a6f8-67ed-451d-90bf-f5eb927508d8",
                            amount = -11F,
                            currency = "GBP",
                            description = "Amazon",
                            balance = 28556.01F
                        }
                    },

                    card = new Card()
                    {
                        card_number = "805719******2246"
                    }
                },
                new TransJson()
                {
                    id = "624301a0-1af9-ae8f-b0e3-f58b3bd16e91",
                    type = "card_payment",
                    state = "completed",
                    request_id = "1322baee-9ec7-42c0-947c-6df0358475c1",

                    //assigns year, month, day, hour, min, seconds
                    created_at = new DateTime(2022, 03, 29, 12, 58, 56),
                    updated_at = new DateTime(2022, 03, 29, 12, 58, 58),
                    completed_at = new DateTime(2022, 03, 29, 12, 58, 58),

                    merchant = new Merchant()
                    {
                        name = "Facebook Ads",
                        city = "MENLO PARK",
                        category_code = "7311",
                        country = "USA"
                    },

                    legs =  new List<Leg>()
                    {
                        new Leg()
                        {
                            leg_id = "8a2098b6-7138-43c0-b930-8200994a2925",
                            account_id = "3337a6f8-67ed-451d-90bf-f5eb927508d8",
                            amount = -21.5F,
                            currency = "GBP",
                            description = "Facebook Ads Facebook Ads Facebook Ads",
                            balance = 28567.01F
                        }
                    },

                    card = new Card()
                    {
                        card_number = "388507******3244"
                    }
                }
            };

            //private string AmazonIBAN = "GB11BARC20040475692433";
            //private string FacebookIBAN = "GB91BARC20038015466316";

            inputIbanCounterparty.Add("GB11BARC20040475692433");
            inputIbanCounterparty.Add("GB91BARC20038015466316");

            inputIban = "PL30116022020000001111111111";

            output = converter?.Convert(inputAcc, inputTrans, inputIban, inputIbanCounterparty);
            Console.WriteLine("test");
        }

        [Test]
        public void LastCompletedDateToTransRefNum()
        {
            string dayOfYear = DateTime.Now.DayOfYear.ToString().PadLeft(3, '0');
            string hh = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string mm = DateTime.Now.Minute.ToString().PadLeft(2, '0');

            Assert.AreEqual(dayOfYear + hh + mm, output.transRefNum);
        }

        [Test]
        public void TransRefNumLengthIs7()
        {
            Assert.IsTrue(output.transRefNum.Length == 7);
        }

        [Test]
        public void IbanTorawAccId()
        {
            Assert.AreEqual(inputIban, output.rawAccId);
        }

        [Test]
        public void FirstCreateDateToOpenDate()
        {
            Assert.AreEqual(inputTrans[0].created_at, output.openBal.bookDate);
        }

        [Test]
        public void LastUpdateDateToCloseDate()
        {
            Assert.AreEqual(inputTrans[inputTrans.Count - 1].completed_at, output.closeBal.bookDate);
        }

        [Test]
        public void TransLineAmountIsAbsolute()
        {
            List<decimal> absolute = new List<decimal>();
            List<decimal> transLinesAmount = new List<decimal>();

            foreach (var transaction in output.transactions)
                transLinesAmount.Add(transaction.transLine.amount);

            foreach (var trans in inputTrans)
                absolute.Add((decimal)Math.Abs(trans.legs[0].amount));

            CollectionAssert.AreEqual(absolute, transLinesAmount);
        }

        [Test]
        public void TransLineValueDateEqualsTransJsonCreated_At()
        {
            List<DateTime> transJsonCreated_AtDates = new List<DateTime>();
            List<DateTime> transLineValueDates = new List<DateTime>();

            foreach (var value in output.transactions)
                transLineValueDates.Add(value.transLine.valueDate);

            foreach (var trans in inputTrans)
                transJsonCreated_AtDates.Add(trans.created_at);

            CollectionAssert.AreEqual(transJsonCreated_AtDates, transLineValueDates);
        }

        [Test]
        public void TransLineBookingDateEqualsTransJsonCompleted_At()
        {
            List<DateTime> transJsonCompleted_AtDates = new List<DateTime>();
            List<DateTime> transLineBookingDates = new List<DateTime>();

            foreach (var value in output.transactions)
                transLineBookingDates.Add(value.transLine.bookingDate.Value);

            foreach (var value in inputTrans)
                transJsonCompleted_AtDates.Add(value.completed_at);

            CollectionAssert.AreEqual(transJsonCompleted_AtDates, transLineBookingDates);
        }

        [Test]
        public void CreditDebitMarkIsRightWithAmountSign()
        {
            List<string> transLines = new List<string>();
            List<string> creditDebit = new List<string>();

            foreach (var trans in inputTrans)
                creditDebit.Add(trans.legs[0].amount >= 0 ? "C" : "D");

            foreach (var value in output.transactions)
                transLines.Add(value.transLine.debitCreditMark);

            CollectionAssert.AreEqual(transLines, creditDebit);
        }

        [Test]
        public void Field61PredictedRawDataIsEqualtoOutputRawData()
        {
            List<string> PredictedRawData = new List<string>();
            List<string> RawData = new List<string>();

            PredictedRawData.Add(":61:2203290329DP11,00NTRF//");
            PredictedRawData.Add(":61:2203290329DP21,50NTRF//");

            foreach (var value in output.transactions)
                RawData.Add(value.transLine.rawData);

            CollectionAssert.AreEqual(RawData, PredictedRawData);
        }

        [Test]
        public void Field62FPredictedRawDataIsEqualToOutputRawData()
        {
            string PredictedRawData = ":62F:C220329GBP28567,01";
            Assert.AreEqual(PredictedRawData, output.closeBal.rawData);
        }

        [Test]
        public void Field64PredictedRawDataIsEqualToOutputRawData()
        {
            string PredictedRawData = ":64:C220329GBP28567,01";
            Assert.AreEqual(PredictedRawData, output.curValDatedBal.rawData);
        }

        [Test]
        public void Field86PredicterRawDataIsEqualToOutputRawData()
        {
            List<string> PredictedRawData = new List<string>();
            List<string> RawData = new List<string>();

            PredictedRawData.Add(":86:114<00card_payment\n<20Amazon\n<21\n<22\n<23\n<24\n<25\n<26\n<27Amazon London GBR\n<28\n<29\n<30\n<31\n<32\n<33\n<38GB11BARC20040475692433\n<63");
            PredictedRawData.Add(":86:114<00card_payment\n<20Facebook Ads Facebook Ads Facebook \n<21Ads\n<22\n<23\n<24\n<25\n<26\n<27Facebook Ads MENLO PARK USA\n<28\n<29\n<30\n<31\n<32\n<33\n<38GB91BARC20038015466316\n<63");

            foreach (var value in output.transactions)
                RawData.Add(value.details.rawData);

            CollectionAssert.AreEqual(PredictedRawData, RawData);
        }

        [Test]
        public void FreeFieldHasCorrectDateFormat()
        {
            Assert.IsTrue(output.freeField.Contains(String.Format("{0:dd-MM-yyyy}", DateTime.Now)));
        }
    }
}
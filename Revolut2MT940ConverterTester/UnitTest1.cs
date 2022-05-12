using Core.Model;
using MT940;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using DataConverter;

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
        AccJson inputAcc;
        List<TransJson> inputTrans;
        string inputIban;
        StatementMT940? output = null;
        Revolut2MT940Converter converter;

        [SetUp]
        public void SetUp()
        {
            inputAcc = new AccJson()
            {
                id = "a78a6740-937d-49e5-a7c2-51d9a486c96d",
                name = "European suppliers",
                balance = 3208.51F,
                currency = "EUR"


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
                            description = "Facebook Ads",
                            balance = 28567.01F
                        }
                    },

                    card = new Card()
                    {
                        card_number = "388507******3244"
                    }
                }
            };


            inputIban = "PL30116022020000001111111111";
            
            output = converter.Convert(inputAcc, inputTrans, inputIban);
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
            Assert.AreEqual(inputTrans[0].updated_at, output.closeBal.bookDate);
        }
    }
}

using Core.Interfaces;
using DataDownloaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using TokenRefresher;
using Xunit.Sdk;

namespace TestDataDownloader
{
    [TestClass]
    public class UnitTest1
    {
        private string[] expectedAccountsJsonArr = File.ReadAllLines(@"Files\accounts.json");
        private string expectedTransactionJson = File.ReadAllText(@"Files\trans.json");

        [TestMethod]
        public async Task TestMethodAccountsJson()
        {
            string expectedAccountsJson = "";
            for (int i = 0; i < expectedAccountsJsonArr.Length; i++)
                expectedAccountsJson += expectedAccountsJsonArr[i];
            using (var sw = new StringWriter())
            {
                ITokenRefresher tokenRefresher = new RevolutTokenRefresher();
                IDataDownloader dataDownloader = new RevolutDataDownloader();
                dataDownloader.SetToken(await tokenRefresher.GetToken());

                Console.SetOut(sw);

                var result = await dataDownloader.GetAccounts();
                Assert.AreEqual(expectedAccountsJson, result);
            }
        }
    }
}
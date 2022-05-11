using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestDataDownloader
{
    [TestClass]
    public class UnitTest1
    {
        private string expectedAccountsJson = File.ReadAllText(@"Files\accounts.json");
        private string expectedTransactionJson = File.ReadAllText(@"Files\trans.json");

        [TestMethod]
        public async Task TestMethodAccountsJson()
        {
            using (var sw = new StringWriter())
            {
                string[] args = { "6" };
                Console.SetOut(sw);
                await MainConsoleApp.Program.Main(args);

                var result = sw.ToString().Trim();
                Assert.AreEqual(expectedAccountsJson, result);
            }
        }
    }
}

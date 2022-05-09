using Core.Exceptions;
using Core.Interfaces;
using DataDownloaders;
using System;
using System.Threading.Tasks;

namespace MainConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IDataDownloader dataDownloader = new RevolutDataDownloader();

            //string grantType = "refresh_token";
            //string refreshToken = "oa_sand_tn2IVETVE-Y-frCxq7xC1ax1_bq4j3u2m4bxtnJATSw";
            //string ClientID = "q5iANB5dr3I9-lxa6yJAKq_8gA0GdFH48mtipR0tQkw";
            //string ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer";
            //string ClientAssertion = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ3d3cuYW5kcmEuY29tLnBsIiwic3ViIjoicTVpQU5CNWRyM0k5LWx4YTZ5SkFLcV84Z0EwR2RGSDQ4bXRpcFIwdFFrdyIsImF1ZCI6Imh0dHBzOi8vcmV2b2x1dC5jb20iLCJleHAiOjE2ODAxNTk4MTV9.EiqM4ZHgphf58Qfs4l7RW3On26hPv31BZArevZwVn5yoXn6Y05XjtogEFKAN21DwXEy89rzS-GBGmY9IF54pZhJ29RzpJmJ6oLZZU28QAVtBtDEwWowqoWVMsBhtrJ53QaFkRA0Kk_hl9JfA6l8wQVLShMYwdWE2jOBzIGScp0s";

            //await dataDownloader.GenerateTokenAsync(grantType, refreshToken, ClientID, ClientAssertionType, ClientAssertion);

            dataDownloader.SetParameter("oa_sand_prjW6jQZKW5ebTtBNghNibg4A0hfdBDyH8KRPIPrQHw");

            string output = string.Empty;
            try
            {

                bool isOn = true;
                int choice = 0;

                while (isOn)
                {

                    Console.WriteLine("Press 1 for accounts, 2 for transactions or 3 to close the program");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {

                        case 1:
                            output = "ACCOUNTS:\n" + await dataDownloader.GetAccounts();
                            Console.WriteLine(output);
                            break;
                        case 2:
                            output = "TRANSACTIONS:\n" + await dataDownloader.GetTransactions();
                            Console.WriteLine(output);
                            break;
                        case 4:

                            break;

                        case 5:
                            isOn = false;
                            break;

                        default:
                            Console.WriteLine("Input error!");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is AuthenticationException)
                {
                    Console.Out.WriteLine("Problem z logowaniem");
                }
                else
                    Console.Out.WriteLine("Inny problem");
            }
            //  stirng data = 
        }
    }
}

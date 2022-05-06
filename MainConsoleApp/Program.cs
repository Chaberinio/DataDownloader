using Core.Exceptions;
using Core.Interfaces;
using DataDownloaders;
using System;

namespace MainConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            IDataDownloader dataDownloader = new RevolutDataDownloaderMock();

            dataDownloader.SetParameter("oa_sand_p1R6DbI5DB2A0tixpahINpj1zAWOAWHL88PbvRdQSWU");

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
                            output = "ACCOUNTS:\n" + dataDownloader.GetAccounts();
                            Console.Write(output);
                            break;
                        case 2:
                            output = "TRANSACTIONS:\n" + dataDownloader.GetTransactions();
                            Console.Write(output);
                            break;

                        case 3:
                            isOn = false;
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

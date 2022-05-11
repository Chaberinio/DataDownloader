using Core.Exceptions;
using Core.Interfaces;
using DataDownloaders;
using DataParsers;
using DataSaver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TokenRefresher;

namespace MainConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<IDataDownloader, RevolutDataDownloader>()
            .AddTransient<IDataParser, RevolutDataParser>()
            .AddTransient<IDataSaver, RevolutDataSaver>()
            .AddTransient<ITokenRefresher, RevolutTokenRefresher>()
            .BuildServiceProvider();

            ITokenRefresher tokenRefresher = serviceProvider.GetService<ITokenRefresher>();
            IDataDownloader dataDownloader = serviceProvider.GetService<IDataDownloader>();
            IDataParser parser = serviceProvider.GetService<IDataParser>();
            IDataSaver dataSaver = serviceProvider.GetService<IDataSaver>();

            dataDownloader.SetToken(await tokenRefresher.GetToken());

            string output = "";
            try
            {
                bool isOn = true;
                int choice = 0;
                int fileFormat = 0;

                while (isOn)
                {
                    Console.WriteLine("Press 1 for accounts, 2 for transactions, 3 to download account list ,4 to download transactions list or 5 to close the program");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            output = "ACCOUNTS:\n" + parser?.AccountsToString(await dataDownloader.GetAccounts());
                            Console.WriteLine(output);
                            break;

                        case 2:
                            output = "TRANSACTIONS:\n" + parser?.TransactionsToString(await dataDownloader.GetTransactions());
                            Console.WriteLine(output);
                            break;

                        case 3:
                            Console.WriteLine("Press 1 for .txt or 2 for .json: ");
                            fileFormat = int.Parse(Console.ReadLine());
                            switch (fileFormat)
                            {
                                case 1:
                                    await dataSaver.SaveAsTxt(parser?.AccountsToString(await dataDownloader.GetAccounts()), "accounts");
                                    break;

                                case 2:
                                    await dataSaver.SaveAsJson(parser?.AccountsToString(await dataDownloader.GetAccounts()), "accounts");
                                    break;

                                default:
                                    Console.WriteLine("Input error!");
                                    break;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Press 1 for .txt or 2 for .json: ");
                            fileFormat = int.Parse(Console.ReadLine());
                            switch (fileFormat)
                            {
                                case 1:
                                    await dataSaver.SaveAsTxt(parser?.TransactionsToString(await dataDownloader.GetTransactions()), "transactions");
                                    break;

                                case 2:
                                    await dataSaver.SaveAsJson(parser?.TransactionsToString(await dataDownloader.GetTransactions()), "transactions");
                                    break;

                                default:
                                    Console.WriteLine("Input error!");
                                    break;
                            }
                            break;

                        case 5:
                            isOn = false;
                            break;

                        case 6:
                            Console.WriteLine(await dataDownloader.GetAccounts());
                            break;
                        
                        case 7:
                            Console.WriteLine(await dataDownloader.GetTransactions());
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
                    Console.WriteLine("Problem z logowaniem");
                else
                    Console.WriteLine("Inny problem");
            }
        }
    }
}
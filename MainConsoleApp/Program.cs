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
            Console.WriteLine("Hello World!");

            IDataDownloader dataDownloader= new RevolutDataDownloaderMock();
           // dataDownloader.SetParameter("AE");

            string output = string.Empty;
            try
            {
                output = dataDownloader.GetAccounts();
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



            Console.Out.Write(output);
          //  stirng data = 
        }
    }
}

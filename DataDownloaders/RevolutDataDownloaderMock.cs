using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloaders
{
    public class RevolutDataDownloaderMock : IDataDownloader
    {
        //string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\accounts.json");


        string parameter = string.Empty;
        string token = "oa_sand_p1R6DbI5DB2A0tixpahINpj1zAWOAWHL88PbvRdQSWU";

        string result = string.Empty;
    



        public string GetAccounts()
        {
            string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\accounts.json");

            if (parameter!=token)            
              throw new AuthenticationException();
            
            for(int i = 0; i < acc.Length; i ++)
                result += acc[i] + "\n";

            return result;
        }
        
        public string GetTransactions()
        {
            string[] acc = File.ReadAllLines(@"C:\Users\chabe\OneDrive\Pulpit\DataDownloader\DataDownloaders\Files\trans.json");

            if (parameter != token)
                throw new AuthenticationException();
            
            for (int i = 0; i < acc.Length; i++)
                result += acc[i] + "\n";

            return result;
        }

        public void SetParameter(string value)
        {
            parameter = value;
        }
    }
}

using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloaders
{
    public class RevolutDataDownloaderMock : IDataDownloader
    {


        string acc = "test";
        string parameter = string.Empty;
    



        public string GetAccounts()
        {
            if (parameter=="AE")            
              throw new AuthenticationException();
            
            return acc;
        }

        public string GetTransactions()
        {
            throw new NotImplementedException();
        }

        public void SetParameter(string value)
        {
            parameter = value;
        }
    }
}

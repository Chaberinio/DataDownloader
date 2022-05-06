using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloaders
{
    public class RevolutDataDownloader : IDataDownloader
    {
        public string GetAccounts()
        {
            return "";
        }

        public string GetTransactions()
        {
            throw new NotImplementedException();
        }

        public void SetParameter(string value)
        {
            throw new NotImplementedException();
        }
    }
}

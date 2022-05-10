﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataDownloader
    {
        Task<string> GetAccounts();
        Task<string> GetTransactions();
        void SetToken(string value);

    }
}

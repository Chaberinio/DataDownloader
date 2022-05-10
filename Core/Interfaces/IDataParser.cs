﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataParser
    {
        string AccountsToString(string json);
        string TransactionsToString(string json);
    }
}

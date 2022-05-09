using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataParser
    {
        Task<string> AccountsToString();
        Task<string> TransactionsToString();
    }
}

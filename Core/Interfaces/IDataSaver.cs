using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataSaver
    {
        Task SaveAsTxt(string value, string name);
        Task SaveAsJson(string json, string name);
    }
}

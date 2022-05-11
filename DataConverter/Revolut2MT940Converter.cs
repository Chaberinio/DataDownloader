using Core.Interfaces;
using Core.Model;
using MT940;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConverter
{
    public class Revolut2MT940Converter : IRevolut2MT940Converter
    {
        public StatementMT940 Convert(AccJson account, List<TransJson> transactions)
        {
            throw new NotImplementedException();
        }
    }
}

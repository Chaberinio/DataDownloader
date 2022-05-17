using Core.Model;
using MT940;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRevolut2MT940Converter
    {
        public StatementMT940 Convert(AccJson account, List<TransJson> transactions, string iban, List<string> counterpartyIbanList);
    }
}
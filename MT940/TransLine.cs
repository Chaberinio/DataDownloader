using System;

namespace MT940
{
    public class TransLine
    {
        public DateTime valueDate;
        public DateTime? bookingDate;
        public string debitCreditMark;
        public decimal amount;  // zawsze liczba dodatnia
        public string bookingCode;
        public string cRef;
        public string bRef;
        public string furtherInfo;
        public string rawData;

        public string ToNiceString()
        {
            if (valueDate == null) throw new Exception("valueDate nie mo¿e byæ null!");
            if (debitCreditMark == null) throw new Exception("debitCreditMark nie mo¿e byæ null!");

            string s = string.Empty;
            s += "Data: " + this.valueDate + " Kwota: " + this.amount + " " + this.debitCreditMark + " ";
            return s;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace MT940
{
    [Serializable()]
    public class StatementMT940
    {
        [XmlIgnore]
        public Type ParserUsed { get; set; }

        public string transRefNum;      // :20:
        public string relRef;           // :21:
        public string rawAccId;         // :25:
        public string staNum;           // :28C:
        public Balance openBal;         // :60F:
        public Balance intermediateOpenBal; // :60M:
        public List<Transaction> transactions = new List<Transaction>();
        public Balance closeBal;        // :62F:
        public Balance intermediateCloseBal; // :62M:
        public Balance curValDatedBal;  // :64:
        public Balance futValDatedBal;  // :65:
        public string freeField;        // :86: na końcu

        [NonSerialized]
        [XmlIgnore]
        private readonly List<Tuple<uint, string>> recordLines;   // pojedyncze wiersze wyciągu

        // waluta rachunku na podstawie waluty salda początkowego
        public string AccCurrency
        {
            get
            {
                if (openBal != null)
                    return openBal.currency;
                else if (intermediateOpenBal != null)
                    return intermediateOpenBal.currency;
                else
                    return null;
            }
        }

        public string accID
        {
            get
            {
                if (rawAccId.StartsWith("/", StringComparison.Ordinal))
                    return rawAccId.Substring(1);
                else
                    return rawAccId;
            }
        }

        public StatementMT940()
        { }

        public StatementMT940(IList<Tuple<uint, string>> recordLines)
        {
            this.recordLines = (List<Tuple<uint, string>>)recordLines;
        }

        public StatementMT940(SerializationInfo info, StreamingContext ctxt)
        {
            //  this.internalID = info.GetInt64("id");
            // this.accID = info.GetString("accID");
            this.transactions = (List<Transaction>)info.GetValue("transaction", typeof(List<Transaction>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //info.AddValue("id", this.internalID);
            info.AddValue("accountNo", this.accID);
            info.AddValue("transaction", this.transactions);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            recordLines.ForEach(r => sb.AppendLine(r.Item2));
            return sb.ToString();
        }

        public string ToNiceString()
        {
            string s = string.Empty;

            s += "Wyciąg z rachunku: ";
            s += (accID != null ? accID : "");
            s += "\n";

            foreach (Transaction t in transactions)
            {
                s += t.ToNiceString() + "\n";
            }

            return s;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace MT940
{
    public class Transaction
    {
        // 51 - class, 52 - type, 53 - contract no., 54 - is contract?, 55 - is multicontract?
        public static readonly string[] myBps = { "51", "52", "53", "54", "55" };

        public TransLine transLine;     // :61:
        public Details details;         // :86:
        public string Gvc;
        public string GvcOrBookingCode;
        public XmlSerializableDictionary<string, string> BP = new XmlSerializableDictionary<string, string>();
        public string rawBP;

        public Transaction()
        {
            Array.ForEach<string>(myBps, k => BP[k] = string.Empty);
        }

        public Transaction(SerializationInfo info, StreamingContext ctxt)
        {
            //this.internalID = info.GetInt64("id");
            //this.details = info.GetString("details");
            //this.transLine = info.GetString("transLine");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //info.AddValue("id", this.internalID);
            info.AddValue("details", this.details);
            info.AddValue("transLine", this.transLine);
        }

        public string ToNiceString()
        {
            string b51;
            string b52;
            if (BP == null || !BP.TryGetValue("51", out b51) || b51 == null) b51 = "<brak>";
            if (BP == null || !BP.TryGetValue("52", out b52) || b52 == null) b52 = "<brak>";
            if (transLine == null) throw new Exception("transLine nie mo¿e byæ null!");
            string s = string.Empty;
            s += "Transakcja: " + transLine.ToNiceString() + " " + (details != null ? details.ToNiceString() : "") +
                 " Klasyfikacja: Klasa=" + b51 +
                 " Typ=" + b52;
            return s;
        }
    }
}

namespace MT940
{
    public class Details
    {
        public string separator;            // details separator
        public string counterpartyAcc;      // IBAN albo NRB przeciwstrony
        public string detailsOnly;          // same szczególy pola :86:
        public string userDetailsOnly;      // szczegóły użytkownika (domyślnie podpola 20-25 pola 86)
        public string counterpartyName;
        public string bookingText;          // Booking text
        public string rawData;              // surowe :86: bez taga

        public string ToNiceString()
        {
            string s = string.Empty;
            s += "Rach przeciwny: " + this.counterpartyAcc;
            return s;
        }
    }
}
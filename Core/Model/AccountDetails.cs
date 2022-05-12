using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class RootobjectAccountDetails
    {
        public AccountDetails[] Property1 { get; set; }
    }

    public class AccountDetails
    {
        public string iban { get; set; }
        public string bic { get; set; }
        public string account_no { get; set; }
        public string sort_code { get; set; }
        public string routing_number { get; set; }
        public string beneficiary { get; set; }
        public Beneficiary_Address beneficiary_address { get; set; }
        public string bank_country { get; set; }
        public bool pooled { get; set; }
        public string unique_reference { get; set; }
        public string[] schemes { get; set; }
        public Estimated_Time estimated_time { get; set; }
    }

    public class Beneficiary_Address
    {
        public string street_line1 { get; set; }
        public string street_line2 { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
    }

    public class Estimated_Time
    {
        public string unit { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }
}
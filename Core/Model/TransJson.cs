using System;
using System.Collections.Generic;

namespace Core.Model
{

    public class Rootobject
    {
        public TransJson[] data { get; set; }
    }

    public class TransJson
    {
        public string id { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string request_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime completed_at { get; set; }
        public Merchant merchant { get; set; }
        public List<Leg> legs { get; set; }
        public Card card { get; set; }
    }

    public class Merchant
    {
        public string name { get; set; }
        public string city { get; set; }
        public string category_code { get; set; }
        public string country { get; set; }
    }

    public class Card
    {
        public string card_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
    }

    public class Leg
    {
        public string leg_id { get; set; }
        public string account_id { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public float balance { get; set; }
        public Counterparty counterparty { get; set; }
    }

    public class Counterparty
    {
        public string account_type { get; set; }
    }


}

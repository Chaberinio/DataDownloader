using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class RootobjectAccJson
    {
        public List<AccJson> data { get; set; }
    }
    public class AccJson
    {
        public string id { get; set; }
        public string name { get; set; }
        public float balance { get; set; }
        public string currency { get; set; }
        public string state { get; set; }
        public bool _public { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

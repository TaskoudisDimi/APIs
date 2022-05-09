using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAuthApp.Models
{
    public class Event
    {
        public string id { get; set; }
        public string type { get; set; }
        public string minute { get; set; }
        public string extra_min { get; set; }
        public string team { get; set; }
        public string player { get; set; }
        public string player_id { get; set; }
        public string assist { get; set; }
        public string assist_id { get; set; }
        public string result { get; set; }

    }
}
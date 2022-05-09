using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAuthApp.Models
{
    public class Match
    {
        public string id { get; set; }
        public string comp_id { get; set; }
        public string formatted_date { get; set; }
        public string season { get; set; }
        public string week { get; set; }
        public string venue { get; set; }
        public string venue_id { get; set; }
        public string venue_city { get; set; }
        public string status { get; set; }
        public string timer { get; set; }
        public string time { get; set; }
        public string localteam_id { get; set; }
        public string localteam_name { get; set; }
        public string localteam_score { get; set; }
        public string visitorteam_id { get; set; }
        public string visitorteam_name { get; set; }
        public string visitorteam_score { get; set; }
        public string ht_score { get; set; }
        public string ft_score { get; set; }
        public object et_score { get; set; }
        public object penalty_local { get; set; }
        public object penalty_visitor { get; set; }
        public List<object> events { get; set; }


    }
}
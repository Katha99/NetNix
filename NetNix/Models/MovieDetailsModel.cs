using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetNix.Models
{
    public class MovieDetailsModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime releaseDate { get; set; }
        public string shortDescription { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Director director { get; set; }

        public class Director
        {
            public string name { get; set; }
            public string dateOfBirth { get; set; }
            public string id { get; set; }
        }
    }
}
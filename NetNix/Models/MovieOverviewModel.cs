using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetNix.Models
{
    public class MovieOverviewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime releaseDate { get; set; }
        public directorModel director { get; set; }

        public class directorModel
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
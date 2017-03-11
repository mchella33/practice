using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurryTry2.Models
{
    public class ViewProfile
    {
        public System.Guid ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public string GenderSeeking { get; set; }
        public string Avatar { get; set; }
        public string AboutMe { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string JsonAttributes { get; set; }
        public System.DateTime Birthdate { get; set; }

    }
}
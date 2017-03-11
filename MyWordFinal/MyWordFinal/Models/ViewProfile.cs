using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWordFinal.Models
{
    public class ViewProfile
    {
        public System.Guid ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
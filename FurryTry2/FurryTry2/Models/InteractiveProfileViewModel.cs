
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurryTry2.Entities;

namespace FurryTry2.Models
{
    public class InteractiveProfileViewModel
    {
        public Profile Profile{ get; set; }
        public List<Shareable> Shareables { get; set; }

    }
}
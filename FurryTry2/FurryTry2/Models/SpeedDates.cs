using FurryTry2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights.Web;

namespace FurryTry2.Models
{
    public class SpeedDates
    {
        public System.Guid UserIdGuid { get; set; }
        public System.Guid SpeedDateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime PostTime { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string JsonAttributes { get; set; }
        
    }
}
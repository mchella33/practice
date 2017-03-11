using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using FurryTry2.Entities;
using FurryTry2.Models;

namespace FurryTry2.Models
{
    public class SpeedDatesViewModel
    {
        public List<SpeedDates> SpeedDates { get; set; }
        //creating a list of speed dates


        public SpeedDatesViewModel()
        {
            var SpeedDates = new List<SpeedDates>();
            //constructor fires off and instantiates SpeedDates..let's us add things in

        }
    }
}

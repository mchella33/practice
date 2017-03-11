using FurryTry2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurryTry2.Models
{
    public class MainPageViewModel//has a property that is a list of 'MainView Profiles....This is the data that I am using to display on my main page cards
    {
        public List<ViewProfile> Profiles { get; set; }
        //ViewProfile consists of list below (created class)

        public MainPageViewModel()//constructor for MainPageViewModel
        {
            Profiles = new List<ViewProfile>(); //instantiating list of profiles to prevent 'null' errors
        }
    }

   

}
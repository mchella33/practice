using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FurryTry2.Entities;
using FurryTry2.Models;

namespace FurryTry2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
//created this to route to Login/Register
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                //this line checks to see if user is authenticated

            {
                //direct to main page
                return RedirectToAction("MainPage", "Home"); //goes to main page
            }
            else
            {
                //direct to login/register
                return RedirectToAction("Index", "Auth"); //goes back to sign in page

            }


        }

        [Authorize]
        public ActionResult MainPage()
        {
            var viewModel = new MainPageViewModel(); //creates new mainpageviewmodel

            using (var db = new FurryEntities())
                //creating a new furryentities called db....allows access to the db            
            {
                List<Profile> profiles = db.Profiles.ToList(); //grabbing all the profiles from the db

                foreach (var profile in profiles) //goes through each profile
                {
                    var viewModelProfile = new ViewProfile
                    {
                        ProfileId = profile.ProfileId,
                        DisplayName = profile.DisplayName,
                        Gender = profile.Gender,
                        GenderSeeking = profile.GenderSeeking,
                        Avatar = profile.Avatar,
                        City = profile.City
                    }; //creates the parameters of the view model





                    viewModel.Profiles.Add(viewModelProfile); //adds this profile to the viewmodel's list
                }



            }

            return View(viewModel); //returns view, passing viewmodel to it

        }

        [Authorize]
        public ActionResult AppUserProfile()
        {
            var cookievalue = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            ViewProfile viewModel;
            using (var db = new FurryEntities())
            {
                var profile = db.Profiles.FirstOrDefault(x => x.ProfileId == new Guid(cookievalue.UserData));
                viewModel = new ViewProfile
                {
                    ProfileId = profile.ProfileId,
                    AboutMe = profile.AboutMe,
                    Avatar = profile.Avatar,
                    Birthdate = profile.Birthdate,
                    City = profile.City,
                    Country = profile.Country,
                    DisplayName = profile.DisplayName,
                    Gender = profile.Gender,
                    GenderSeeking = profile.GenderSeeking,
                    JsonAttributes = profile.JsonAttributes,
                };

            }
            return View(viewModel);

        }

        public ActionResult InteractiveProfile(Guid profileId)
        {
            var viewModel = new InteractiveProfileViewModel();
            using (var db = new FurryEntities())
            {
                var profile = db.Profiles.FirstOrDefault(x => x.ProfileId == profileId);
                var shareables = db.Shareables.ToList();

                viewModel.Profile = profile;
                viewModel.Shareables = shareables;
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult CreateSpeedDate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //security check.... prevents cross-site request forgeries....stack overflow... description
        public ActionResult CreateSpeedDate(SpeedDates input)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FurryEntities())
                {
                    var speedDates = db.SpeedDates.FirstOrDefault(x => x.SpeedDateId == input.SpeedDateId);
                    var newSpeedDates = new SpeedDate();
                    if (speedDates == null)
                    {
                        newSpeedDates.SpeedDateId = Guid.NewGuid();
                        newSpeedDates.PostTime = DateTime.Now;
                        if (input != null)
                        {
                            var cookievalue =
                                FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                            newSpeedDates.UserId = new Guid(cookievalue.UserData);
                            newSpeedDates.Title = input.Title;
                            newSpeedDates.Description = input.Description;
                            newSpeedDates.City = input.City;
                            newSpeedDates.State = input.State;
                            if (input.JsonAttributes != null)
                                newSpeedDates.JsonAttributes = input.JsonAttributes;
                        }
                        //nullable properties that may or may not have a value
                        db.SpeedDates.Add(newSpeedDates); //adds complete 'newSpeedDate' to the table
                        db.SaveChanges(); //saves information in db
                        return RedirectToAction("MainPage", "Home"); //sends them straight to 'home index'
                    }
                } //closing using statement destroys db connection

                //if model state is invalid, returns to register and passes existing 'input' back
            }
            return View(input);
        }

        public ActionResult SpeedDatesList()
        {
            List<SpeedDate> speedDates;
            using (var db = new FurryEntities())
            {
                speedDates = db.SpeedDates.ToList();
            }
            return View(speedDates);

        }
    }
}
        




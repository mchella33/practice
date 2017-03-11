using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurryTry2.Models;
using FurryTry2.Entities;
using System.Web.Security;

namespace FurryTry2.Controllers
{
    public class AuthController : Controller
    {
        private FurryEntities db = new FurryEntities();
        // GET: Auth
        [AllowAnonymous] //because do not know if member or needs to register... need to authenticate to sign in
        public ActionResult Index()
        {
            //sign in

            return View();//goes straight to the view without passing any additional in to it
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//security feature
        public ActionResult Index(AppUser input, string returnUrl = "")
            //This action is taking a post from the User... App user input
        {
            if (ModelState.IsValid)
            {
                using (var db = new FurryEntities()) //creating a new furryentities called db....allows access to the db
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName == input.UserName && x.Password == input.Password);
                    //var user above is a linq statement, checking the Users table in the db and grabbingfirst or default user 
                    //that matches the username and password passed in. In this case 'x' = table of database
                    if (user != null) //if it does result in a user, it's authenticated and gives a cookie.
                    {
                        var persistentCookie = input.RememberMe;

                        var ticket = new FormsAuthenticationTicket(
                            1,
                            input.UserName,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            persistentCookie,
                            user.UserId.ToString()
                            );
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                            FormsAuthentication.Encrypt(ticket))
                        {
                            Path = FormsAuthentication.FormsCookiePath
                        };
                        if (persistentCookie)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);
                        //FormsAuthentication.SetAuthCookie(user.UserName, input.RememberMe);  removed this due to fixing auth

                        return RedirectToAction("Index", "Home");//routes authenticated user over to 'home index' which route to the MainPage
                    }
                     
                }

            }
            ModelState.Remove("Password"); //removes password when not authenticated
            return View(input); 
            //if model state isinvalid, then goes back to login view passing existing input minus password
        }
        [Authorize]
        public ActionResult LogOut()
        {
            //sign out
            return View();

        }
        public ActionResult RemoveAuth() //this removes authentication after confirming LogOut
        {
            
            if (Request.Cookies["userId"] != null)
            {
                Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            }
            FormsAuthentication.SignOut();//invalidates cookie given
            return RedirectToAction("Index","Home"); //sends them back to "home index' which sends them over to the sign in page via Auth controller
        }
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            //password management
            return View();


        }
        [AllowAnonymous]
        public ActionResult Register()//doesn't send anything to view, just loads it
        {
            return View();

        }
        [HttpPost] //post method for 'Register' and 
        [ValidateAntiForgeryToken]//security check.... prevents cross-site request forgeries....stack overflow... description
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel input)//'input' below is information gathered and entered in to the 'RegisterViewModel' once submitted
        {
            if(ModelState.IsValid)
            { 
                using (var db = new FurryEntities())
                {
                    var user = db.Users.FirstOrDefault(x => x.UserName == input.UserName);
                    if(user == null && input.Password == input.RepeatPassword)//put password check in javascript..
                        //if user already exists, sends then this codes does not run.
                    {
                        
                        var newUser = new User();//creating a new user that goes in to the db
                        var newProfile = new Profile();//creating a profile from the info below that goes in to the db

                        newUser.UserId = Guid.NewGuid();
                        newUser.UserName = input.UserName;
                        newUser.Password = input.Password;
                        newUser.EmailId = input.UserName;//EmailId is the UserName
                        newUser.FirstName = input.FirstName;
                        //above is an entity model of 'User'
                       
                                                     
                        newProfile.AboutMe = input.AboutMe;
                        newProfile.Birthdate = new DateTime(input.Year, input.Month, input.Day);
                        newProfile.City = input.City;
                        newProfile.Country = input.Country;
                        newProfile.DisplayName = input.DisplayName;
                        newProfile.Gender = input.Gender.ToString();
                        newProfile.GenderSeeking = input.GenderSeeking.ToString();
                        newProfile.ProfileId = newUser.UserId;
                        //above is an entity model of 'Profile'

                        if(input.JsonAttributes !=null)
                            newProfile.JsonAttributes = input.JsonAttributes;
                        if(input.Avatar !=null)
                            newProfile.Avatar = input.Avatar;
                        if (input.LastName != null)
                            newUser.LastName = input.LastName;
                        //nullable properties that may or may not have a value

                        db.Users.Add(newUser);//adds complete 'newUser' to the Users table
                        db.Profiles.Add(newProfile);//does the same for Profiles table
                        db.SaveChanges();//saves information in db
                        var persistentCookie = input.RememberMe;
                        var ticket = new FormsAuthenticationTicket(
                            1,
                            input.UserName,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            persistentCookie,
                            newProfile.ProfileId.ToString()
                            );
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                            FormsAuthentication.Encrypt(ticket))
                        {
                            Path = FormsAuthentication.FormsCookiePath
                        };
                        if (persistentCookie)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);




                        //FormsAuthentication.SetAuthCookie(input.UserName, input.RememberMe);//adds authentication cookie
                        return RedirectToAction("Index", "Home");//sends them straight to 'home index'

                    }
                }//closing using statement destroys db connection
            }
            return View(input);//if model state is invalid, returns to register and passes existing 'input' back
        }


    }
}
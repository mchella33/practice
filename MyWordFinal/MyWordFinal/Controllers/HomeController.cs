using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyWordFinal.Entities;
using MyWordFinal.Models;

namespace MyWordFinal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]

        public ActionResult Index()
        {
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
        public ActionResult Profile()
        {
            var cookievalue = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            ViewProfile viewModel;
            using (var db = new MyWordEntities())
            {
                var profile = db.Profiles.FirstOrDefault(x => x.ProfileId == new Guid(cookievalue.UserData));
                viewModel = new ViewProfile
                {
                    
                    DisplayName = profile.DisplayName,
                    Comment = profile.Comment,
                    Email = profile.Email,
                };
            }
            return View(viewModel);
        }
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();

        }
    }
      
    }

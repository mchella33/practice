using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MyWordFinal.Entities;
using MyWordFinal.Models;
using Participant = MyWordFinal.Entities.Participant;

namespace MyWordFinal.Controllers
{
    [RoutePrefix("Login")]
    public class AuthController : Controller
    {
        private MyWordEntities db = new MyWordEntities();
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Participant input, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyWordEntities())
                {
                    var participant =
                        db.Participants.FirstOrDefault(x => x.EmailId == input.EmailId && x.Password == input.Password);
                    if (participant != null)
                    {
                        var persistentCookie = true;

                        var ticket = new FormsAuthenticationTicket(
                            1,
                            input.EmailId,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            persistentCookie,
                            participant.ParticipantId.ToString()
                        );
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                            FormsAuthentication.Encrypt(ticket))
                        {
                            Path = FormsAuthentication.FormsCookiePath
                        };
                        if ((bool) persistentCookie)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);
                        //FormsAuthentication.SetAuthCookie(user.UserName, input.RememberMe);  removed this due to fixing auth

                        return RedirectToAction("MainPage", "Home");
                            
                    }

                }

            }
            ModelState.Remove("Password"); //removes password when not authenticated
            return View();
        }

        //if model state isinvalid, then goes back to login view passing existing input minus password
        [Authorize]
        public ActionResult LogOut()
        {
            return View();
        }

        public ActionResult RemoveAuth()
        {
            if (Request.Cookies["participantId"] != null)
            {
                Response.Cookies["participantId"].Expires = DateTime.Now.AddDays(-1);
            }
            FormsAuthentication.SignOut();//invalidates cookie given
            return RedirectToAction("MainPage", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost] //post method for 'Register' and 
        [ValidateAntiForgeryToken]//security check.... prevents cross-site request forgeries....stack overflow... description
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel input)//'input' below is information gathered and entered in to the 'RegisterViewModel' once submitted
        {
            if (ModelState.IsValid)
            {
                using (var db = new MyWordEntities())
                {
                    var participant = db.Participants.FirstOrDefault(x => x.EmailId == input.EmailId);
                    
                    if (participant == null && input.Password == input.RepeatPassword)
                    {
                        var newParticipant = new Participant();
                        newParticipant.ParticipantId = Guid.NewGuid();
                        newParticipant.EmailId = input.EmailId;
                        newParticipant.FirstName = input.FirstName;
                        newParticipant.LastName = input.LastName;
                        newParticipant.DisplayName = input.DisplayName;
                        newParticipant.Password = input.Password;
                       
                        db.Participants.Add(newParticipant);

                        db.SaveChanges();
                        var persistentCookie = input.RememberMe;
                        var ticket = new FormsAuthenticationTicket(
                            1,
                            input.EmailId,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            persistentCookie,
                            newParticipant.ParticipantId.ToString()
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
            return View(input);//if model state is invalid, returns to register and 
        }
           
     
        // GET: Auth
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
    }
}
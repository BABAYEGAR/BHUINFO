using System;
using System.Web.Mvc;
using BhuInfo.Data.Factory;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AppLogin(FormCollection collectedValues)
        {
            var role = typeof(UserType).GetEnumName(int.Parse(collectedValues["Role"]));
            var appUser = new AuthenticationFactory().AuthenticateAppUserLogin(collectedValues["Email"].Trim(),
                collectedValues["Password"].Trim(), role);
            Session["bhuinfologgedinuser"] = appUser;
            if (role == UserType.Administrator.ToString())
            {
                return RedirectToAction("Index", "AppUsers");
            }
            else if(role == UserType.Manager.ToString())
            {
                return RedirectToAction("Index", "News");
            }
            else
            {
                  return RedirectToAction("Index", "Home");
            }
          
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(FormCollection collectedValues)
        {
            if (ModelState.IsValid)
            {
                var user = new AuthenticationFactory().ForgotPasswordRequest(collectedValues["Email"].Trim());
                return RedirectToAction("ResetPassword",new {Id = user.AppUserId});

            }
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(int Id)
        {
            var user = new AppUserFactory().GetAppUserById(Id);
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(FormCollection collectedValues)
        {
            int Id = Convert.ToInt32(collectedValues["Id"]);
                new AuthenticationFactory().ResetUserPassword(collectedValues["Password"], Id);
            return View("Login");
        }
    }
}
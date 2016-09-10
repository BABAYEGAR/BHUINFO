﻿using System;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Factory.BusinessFactory;
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
            Session["bhuinfologgedinuser"] = null;
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
            if (appUser != null)
            {
                Session["bhuinfologgedinuser"] = appUser;
                if (role == UserType.Administrator.ToString())
                {
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "AppUsers");
                }
                if (role == UserType.Manager.ToString())
                {
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "News");
                }
                TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                return RedirectToAction("Index", "Home");
            }
            TempData["login"] = "Check your login details and make sure you selected the correct user type!";
            return RedirectToAction("Login", "Account");
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
            new AuthenticationFactory().ForgotPasswordRequest(collectedValues["Email"].Trim());
                return RedirectToAction("Login");
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
            var userId = Convert.ToInt32(collectedValues["Id"]);
            var password = collectedValues["Password"];
            var confirmPassword = collectedValues["confirmPassword"];
            if (password == confirmPassword)
            {
                new AuthenticationFactory().ResetUserPassword(collectedValues["confirmPassword"], userId);
            }
            else
            {
                TempData["password"] = "Make sure your the password and confirm password are the same!";
                return RedirectToAction("ResetPassword", "Account",new {Id = userId });
            }
            return View("Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["bhuinfologgedinuser"] = null;
            FormsAuthentication.RedirectToLoginPage(null);
            return RedirectToAction("Login", "Account");
        }
    }
}
using System;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Encryption;
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
            var model = Session["newsmodel"] as News;
            var activityModel = Session["activitymodel"] as SchoolDiscussion;
            if (appUser != null)
            {
                Session["bhuinfologgedinuser"] = appUser;
                if (role == UserType.Administrator.ToString())
                {
                    if (model != null)
                    {
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home", new { Id = new Md5Ecryption().EncryptPrimaryKey(model.NewsId.ToString(), true) });
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions", new { Id = schoolDiscussionId });
                    }
                    Session["bhuinfologgedinuser"] = appUser;
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "AppUsers");
                }
                if (role == UserType.Manager.ToString())
                {
                    if (model != null)
                    {
                        var newsId = model.NewsId;
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home", new { Id = newsId });
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions", new { Id = schoolDiscussionId });
                    }
                    Session["bhuinfologgedinuser"] = appUser;
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "News");
                }
                
                if (role == UserType.Student.ToString())
                {
                    if (model != null)
                    {
                        var newsId = model.NewsId;
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home", new { Id = newsId });
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions", new { Id = schoolDiscussionId });
                    }
                    Session["bhuinfologgedinuser"] = appUser;
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "Home");
                }
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
        // GET: /Account/ChangePassword
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        /// <summary>
        ///     This controller method changes the user password
        /// </summary>
        /// <param name="collectedValues"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection collectedValues)
        {
            var oldPassword = collectedValues["OldPassword"];
            var newPassword = collectedValues["NewPassword"];
            var confirmPassword = collectedValues["ConfirmNewPassword"];
            var loggedinuser = Session["bhuinfologgedinuser"] as AppUser;
            if (ModelState.IsValid)
                if (newPassword == confirmPassword)
                {
                    var hashPassword = new Md5Ecryption().ConvertStringToMd5Hash(oldPassword.Trim());
                    if ((loggedinuser != null) && (hashPassword == loggedinuser.Password))
                    {
                        if (new AuthenticationFactory().ChangeUserPassword(Convert.ToInt64(loggedinuser.AppUserId),
                            oldPassword,
                            newPassword))
                        {
                            TempData["password"] = "Your Pasword has been changed successfully,Please Login Again!";
                            TempData["notificationtype"] = NotificationType.Success.ToString();
                            Session["bhuinfologgedinuser"] = null;
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        TempData["password"] = "Wrong password,Try Again!";
                        TempData["notificationtype"] = NotificationType.Danger.ToString();
                        return View("ChangePassword");
                    }
                }
                else
                {
                    TempData["password"] = "Make sure your the password and confirm password are the same!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return View("ChangePassword");
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
                //TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("ResetPassword", "Account", new {Id = userId});
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
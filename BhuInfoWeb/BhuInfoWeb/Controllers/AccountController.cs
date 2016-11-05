using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BhuInfo.Data.Context.DataContext;
using BhuInfo.Data.Factory.BusinessFactory;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Encryption;
using BhuInfo.Data.Service.Enums;

namespace BhuInfoWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppUserDataContext _db = new AppUserDataContext();
        private readonly PasswordResetDataContext _dbc = new PasswordResetDataContext();
        //
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["bhuinfologgedinuser"] = null;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProfileDetails(string Id)
        {
            var userId = Convert.ToInt64(new Md5Ecryption().DecryptPrimaryKey(Id, true));
            var user = new AppUserFactory().GetAppUserById((int) userId);
            Session["viewprofilebyotheruser"] = user;
            return View("ProfileDetails", user);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AppLogin(FormCollection collectedValues)
        {
            var appUser = new AuthenticationFactory().AuthenticateAppUserLogin(collectedValues["Email"].Trim(),
                collectedValues["Password"].Trim());
            var model = Session["newsmodel"] as News;
            var activityModel = Session["activitymodel"] as SchoolDiscussion;
            if (appUser != null)
            {
                Session["bhuinfologgedinuser"] = appUser;
                if (appUser.Role == UserType.Administrator.ToString())
                {
                    if (model != null)
                    {
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(model.NewsId.ToString(), true)});
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(schoolDiscussionId.ToString(), true)});
                    }
                    Session["bhuinfologgedinuser"] = appUser;
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "AppUsers");
                }
                if (appUser.Role == UserType.Manager.ToString())
                {
                    if (model != null)
                    {
                        var newsId = model.NewsId;
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(newsId.ToString(), true)});
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(schoolDiscussionId.ToString(), true)});
                    }
                    Session["bhuinfologgedinuser"] = appUser;
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        appUser.DisplayName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        appUser.RememberMe,
                        "", //roles 
                        "/"
                    );
                    //encrypt the ticket and add it to a cookie
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                    TempData["login"] = "Welcome " + appUser.DisplayName + "!";
                    return RedirectToAction("Index", "News");
                }

                if (appUser.Role == UserType.Student.ToString())
                {
                    if (model != null)
                    {
                        var newsId = model.NewsId;
                        Session["newsmodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("ViewNewsDetails", "Home",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(newsId.ToString(), true)});
                    }
                    if (activityModel != null)
                    {
                        var schoolDiscussionId = activityModel.SchoolDiscussionId;
                        Session["activitymodel"] = null;
                        Session["bhuinfologgedinuser"] = appUser;
                        return RedirectToAction("Activity", "SchoolDiscussions",
                            new {Id = new Md5Ecryption().EncryptPrimaryKey(schoolDiscussionId.ToString(), true)});
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
                var userId = new AppUserFactory().GetAppUserByEmail(collectedValues["Email"].Trim()).AppUserId;
                var passwordReset = new PasswordReset();
                var token  = Membership.GeneratePassword(8, 0);
                passwordReset.ExpiryDate = DateTime.Now.AddMinutes(2);
                passwordReset.AppUserId = userId;
                passwordReset.Token = token;
                _dbc.PasswordResets.Add(passwordReset);
                _dbc.SaveChanges();

                var user = _db.AppUsers.Find(userId);
                user.Token = token;
                Session["token"] = token;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
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
            var token = Session["token"];
            var user = new AppUserFactory().GetAppUserById(Id);
            var passwordReset = _dbc.PasswordResets.ToList();
            var userPasswordReset = passwordReset.Single(n => n.AppUserId == Id && n.Token == (string) token );
            if (userPasswordReset.ExpiryDate < DateTime.Now)
            {
                TempData["password"] = "The password Reset Link has expired!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return View("Login");
            }
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
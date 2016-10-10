using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using BhuInfo.Data.Objects.Entities;
using BhuInfo.Data.Service.Configuration;

namespace BhuInfo.Data.Service.EmailService
{
    public class MailerDaemon
    {
        /// <summary>
        ///     This method sends an email containing a username and password to a newly created user
        /// </summary>
        /// <param name="user"></param>
        public void NewUser(AppUser user)
        {
            var message = new MailMessage
            {
                From = new MailAddress(Config.SupportEmailAddress),
                Subject = "New User Details",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_NewUserCreated(user),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(user.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Html page content for the new user email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewUserCreated(AppUser user)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/NewUserCreated.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", user.Firstname)
                    .Replace("USERNAME", user.Email)
                    .Replace("PASSWORD", user.Password)
                    .Replace("URL", "http://localhost:51301/Account/Login")
                    .Replace("ROLE", user.Role)
                    .Replace("FROM", Config.SupportEmailAddress);
        }
        /// <summary>
        ///     This method sends an email containing a username and password to a newly created user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loggedinuser"></param>
        public void UserDeleted(AppUser user,AppUser loggedinuser)
        {
            var message = new MailMessage
            {
                From = new MailAddress(Config.SupportEmailAddress),
                Subject = "Delete Action Details",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_NewUserCreated(user),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(loggedinuser.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Html page content for the new user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loggedinuser"></param>
        /// <returns></returns>
        private static string GetEmailBody_UserDeleted(AppUser user,AppUser loggedinuser)
        {
            
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/UserDeleted.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", user.DisplayName)
                    .Replace("USERNAME", user.Email)
                    .Replace("URL", "http://localhost:51301/Account/Login")
                    .Replace("DeletedBy", loggedinuser.DisplayName)
                    .Replace("Date", DateTime.Now.ToShortDateString())
                    .Replace("Time", DateTime.Now.ToShortTimeString())
                    .Replace("FROM", Config.SupportEmailAddress);
        }
        /// <summary>
        ///     This method sends an email containing a username and access code to a newly subscribed advert
        /// </summary>
        /// <param name="advertisement"></param>
        public void SuscribeAdvert(Advertisement advertisement)
        {
            var message = new MailMessage
            {
                From = new MailAddress(Config.SupportEmailAddress),
                Subject = "New Adevert Subscribed",
                Priority = MailPriority.High,
                SubjectEncoding = Encoding.UTF8,
                Body = GetEmailBody_NewAdvertSubscribed(advertisement),
                IsBodyHtml = true
            };
            //message.To.Add(Config.DevEmailAddress);
            message.To.Add(advertisement.Email);
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Html page content for the subscribed email
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewAdvertSubscribed(Advertisement advertisement)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SubscribedAdvert.html")).ReadToEnd()
                    .Replace("DISPLAYNAME", advertisement.AdvertCompanyName)
                    .Replace("EMAIL", advertisement.Email)
                    .Replace("PASSWORD", advertisement.AccessCode)
                    .Replace("URL", "http://localhost:51301")
                    .Replace("CODE", advertisement.AccessCode)
                    .Replace("FROM", Config.SupportEmailAddress);
        }
        /// <summary>
        /// This method is used to send password reset link emails
        /// </summary>
        /// <param name="user"></param>

        public void ResetUserPassword(AppUser user)
        {
            var message = new MailMessage();

            message.From = new MailAddress(Config.SupportEmailAddress);
            message.To.Add(user.Email);
            message.Subject = "New Password";
            message.Priority = MailPriority.High;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = GetEmailBody_NewPasswordCreated(user);
            message.IsBodyHtml = true;
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// This contains the html and passed values for the password reset emails
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetEmailBody_NewPasswordCreated(AppUser user)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ResetPassword.html"))
                    .ReadToEnd()
                    .Replace("FROM",Config.SupportEmailAddress)
                    .Replace("DISPLAYNAME", user.Firstname)
                    .Replace("URL","http://localhost:51301/Account/ResetPassword/"+user.AppUserId.ToString());
        }
        /// <summary>
        /// This method sends emails to the support of the bhuinfo application
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderMessage"></param>
        /// <param name="email"></param>
        public void ContactUs(string senderName,string senderMessage,string email)
        {
            var message = new MailMessage();
            message.From = new MailAddress(Config.SupportEmailAddress);
            message.To.Add(email);
            message.Subject = "New Contact";
            message.Priority = MailPriority.High;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = GetEmailBody_ContactUs(senderName, senderMessage);
            message.IsBodyHtml = true;
            try
            {
                new SmtpClient().Send(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// This method contains the html and passed values for the contact us email
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderMessage"></param>
        /// <returns></returns>
        private static string GetEmailBody_ContactUs(string senderName, string senderMessage)
        {
            return
                new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ContactUs.html"))
                    .ReadToEnd()
                    .Replace("DISPLAYNAME", senderName)
                    .Replace("URL", "http://localhost:51301/")
                    .Replace("MESSAGE", senderMessage);
        }
    }
}